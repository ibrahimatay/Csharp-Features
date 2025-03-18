// Channel.CreateUnboundedPrioritized Method
// https://learn.microsoft.com/en-us/dotnet/api/system.threading.channels.channel.createunboundedprioritized?view=net-9.0

// [API Proposal] Channel.CreateUnboundedPrioritized #62761
// https://github.com/dotnet/runtime/issues/62761

// Concurrent collection with priority
// https://stackoverflow.com/questions/23470196/concurrent-collection-with-priority

using System.Threading.Channels;

var channel = Channel.CreateUnboundedPrioritized<(int priority, string message)>();

// Write sample messages
await channel.Writer.WriteAsync((2, "Emergency service dispatch"));
await channel.Writer.WriteAsync((1, "Routine check: Blood pressure measurement"));
await channel.Writer.WriteAsync((3, "Heart attack alert!"));
await channel.Writer.WriteAsync((3, "Fire alarm!"));

channel.Writer.Complete();

// Read and process messages based on priority
await foreach (var item in channel.Reader.ReadAllAsync())
{
    Console.WriteLine($"Processing: {item.message} (Priority: {item.priority})");
}