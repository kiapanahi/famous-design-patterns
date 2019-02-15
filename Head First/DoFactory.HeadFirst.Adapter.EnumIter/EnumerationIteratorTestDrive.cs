using System;
using System.Collections.Generic;

namespace DoFactory.HeadFirst.Adapter.EnumIter
{
    class EnumerationIteratorTestDrive
    {
        static void Main(string[] args)
        {
            var list = new List<string> { "One", "Two", "Three" };

            IEnumerator<string> enumerator = list.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current);
            }
            Console.WriteLine();

            // Using the foreach construct. It's easier to read and maintain.
            // .NET generates the enumerator code under the hood for this loop

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
            
            Console.WriteLine();

            // Generic Dictionary using collection initialization
            var dictionary =
                new Dictionary<string, string>
                {
                  {"Key1", "One"},
                  {"Key2", "Two"},
                  {"Key3", "Three"},
                  {"Key4", "Four"}
                };


            foreach (var key in dictionary.Keys)
            {
                Console.WriteLine(key + ": " + dictionary[key]);
            }

            Console.WriteLine();

            // Generic SortedDictionary using collection initialization
            // Items are added in random order
            var sortedList =
                new SortedDictionary<string, string>
                {
                    {"Key4", "Four"},
                    {"Key1", "One"},
                    {"Key3", "Three"},
                    {"Key2", "Two" }
                };

            foreach (var key in sortedList.Keys)
            {
                Console.WriteLine(key + ": " + sortedList[key]);
            }

            // Wait for user
            Console.ReadKey();
        }
    }
}
