using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace AsynchronousStreams
{
    /// <summary>
    /// Async Streams
    /// https://blog.jetbrains.com/dotnet/2019/09/16/async-streams-look-new-language-features-c-8/
    /// https://docs.microsoft.com/en-us/dotnet/csharp/tutorials/generate-consume-asynchronous-stream
    /// </summary>
    class Program
    {
        static async Task Main(string[] args)
        {
           //await AsynchronousStreamsWithCancellationToken();
           await PrintSequence();
        }

        static async IAsyncEnumerable<int> GenerateSequence()
        {
            foreach (var item in Enumerable.Range(0, 100))
            {
                await Task.Delay(100);
                yield return item;
            }
        }

        static async Task PrintSequence()
        {
            await foreach (var number in GenerateSequence())
            {
                Console.WriteLine(number);
            }
        }
        
        static async Task AsynchronousStreamsWithCancellationToken()
        {
            var cancellationToken = new CancellationTokenSource(millisecondsDelay: 1000);
            await foreach (var result in GetTopSearchResults("dotnet").WithCancellation(cancellationToken.Token))
            {
                Console.WriteLine(result);
            }
        }

        static async IAsyncEnumerable<string> GetTopSearchResults(string term, [EnumeratorCancellation]CancellationToken token = default)
        {
            while (!token.IsCancellationRequested)
            {
                using var client = new HttpClient();
                yield return await client.GetStringAsync($"https://www.google.com?q={term}");
                yield return await client.GetStringAsync($"https://www.bing.com?q={term}"); 
            }
        }
    }
}