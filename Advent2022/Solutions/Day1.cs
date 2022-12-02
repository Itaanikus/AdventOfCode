namespace Advent2022.Solutions
{
    internal static class Day1
    {
        public static void GetTaskResults()
        {
            #region Part1
            var elfDictionary = new Dictionary<int, long>();
            var elfKey = 0;
            var accumulated = 0;

            var lines = File.ReadAllLines($"{Directory.GetCurrentDirectory()}\\Inputs\\Day1.txt");

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    elfDictionary.Add(elfKey, accumulated);
                    elfKey++;
                    accumulated = 0;
                    continue;
                }

                accumulated += int.Parse(line.Trim());
            }

            var maxCalorieValue = elfDictionary.Values.Max();
            Console.WriteLine($"Task 1: The maximum calories an elf has is {maxCalorieValue}");
            #endregion

            #region Part2
            var caloriesFromTopThreeElves = elfDictionary.Values.OrderByDescending(x => x).Take(3).Sum();
            Console.WriteLine($"Task 2: The top three elves have in total {caloriesFromTopThreeElves} calories.");
            #endregion
        }
    }
}