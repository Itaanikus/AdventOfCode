using System.Collections.Immutable;

namespace Advent2022.Solutions
{
    internal static class Day01
    {
        public static void GetTaskResults()
        {
            #region Part1
            var elfKey = 0;
            var elfDictionary = new Dictionary<int, long> { { elfKey, 0 } };

            var lines = File.ReadAllLines(@$"{Directory.GetCurrentDirectory()}\Inputs\Day01.txt");

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    elfDictionary.Add(++elfKey, 0);
                    continue;
                }

                elfDictionary[elfKey] += int.Parse(line.Trim());
            }

            var orderedDictionaryValues = elfDictionary
                .Values
                .OrderByDescending(x => x)
                .ToImmutableList();

            Console.WriteLine($"Task 1: The maximum calories an elf has is {orderedDictionaryValues.First()}");
            #endregion

            #region Part2
            var caloriesFromTopThreeElves = orderedDictionaryValues.Take(3).Sum();
            Console.WriteLine($"Task 2: The top three elves have in total {caloriesFromTopThreeElves} calories.");
            #endregion
        }
    }
}