namespace Advent2022.Solutions
{
    internal static class Day3

    {
        public static void GetTaskResults()
        {
            var lines = File.ReadAllLines($"{Directory.GetCurrentDirectory()}\\Inputs\\Day3.txt");
            var totalScorePart1 = 0;
            var totalScorePart2 = 0;

            var elfGroups = new Dictionary<int, List<string>>();
            var groupKey = 0;

            for (var i = 0; i < lines.Length; i++)
            {
                var line = lines[i];

                var compartmentSizes = line.Length / 2;
                var compartment1 = line[..compartmentSizes];
                var compartment2 = line[compartmentSizes..];

                var dictionary = new Dictionary<char, int>();

                foreach (var letter in compartment1.Where(letter =>
                             compartment2.Contains(letter) && !dictionary.ContainsKey(letter)))
                {
                    dictionary.Add(letter, letter.ToElfPriority());
                }

                totalScorePart1 += dictionary.Values.Sum();

                // Part 2
                if (i % 3 == 0)
                {
                    elfGroups.Add(++groupKey, new List<string>());
                }

                elfGroups[groupKey].Add(line);
            }

            foreach (var elfGroup in elfGroups)
            {
                var parsedChars = new List<char>();
                foreach (var letterFromFirstBag in elfGroup.Value[0].Where(letterFromFirstBag => elfGroup.Value[1].Contains(letterFromFirstBag) &&
                             elfGroup.Value[2].Contains(letterFromFirstBag) &&
                             !parsedChars.Contains(letterFromFirstBag)))
                {
                    totalScorePart2 += letterFromFirstBag.ToElfPriority();
                    parsedChars.Add(letterFromFirstBag);
                }
            }

            Console.WriteLine($"Task 1 result is: {totalScorePart1}");
            Console.WriteLine($"Task 2 result is: {totalScorePart2}");
        }

        private static int ToElfPriority(this char character) => char.IsUpper(character) switch
        {
            true => character - 64 + 26,
            _ => character - 96
        };
    }
}