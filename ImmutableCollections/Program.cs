using System.Collections.Generic;

namespace ImmutableCollections
{
    /// <summary>
    /// https://devblogs.microsoft.com/dotnet/immutable-collections-ready-for-prime-time/
    /// https://docs.microsoft.com/en-us/archive/msdn-magazine/2017/march/net-framework-immutable-collections
    /// </summary>
    class Program
    {
        //private static List<int> numberList = new List<int>() {1, 2, 3, 4, 5}; // reinitialize
        //private static readonly List<int> numberList = new List<int>() {1, 2, 3, 4, 5}; // can't reinitialize 
        //private static ImmutableList<int> numberList = new List<int>() { 1, 2, 3, 4, 5 }.ToImmutableList<int>();
        private static readonly IReadOnlyList<int> numberList = new[] { 1, 2, 3, 4, 5 };

        //private static Dictionary<int, string> numberDictionary = new Dictionary<int, string>()
        //{
        //    {1, "1"},
        //    {2, "2"},
        //    {3, "3"},
        //    {4, "4"},
        //    {5, "5"}
        //};

        private static IReadOnlyDictionary<int, string> numberDictionary = new Dictionary<int, string>()
        {
            {1, "1"},
            {2, "2"},
            {3, "3"},
            {4, "4"},
            {5, "5"}
        };


        static void Main(string[] args)
        {
            //numberList = new List<int>() {1, 2, 3}; // can't reinitialize, compilation error because use "readonly" keyword
            //numberList[0] = -1; // IReadonlyList

            //numberList.Add(10); // IReadonlyList
            //numberList.Remove(2); // IReadonlyList
        }
    }
}