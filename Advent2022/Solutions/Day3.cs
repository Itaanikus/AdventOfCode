namespace Advent2022.Solutions
{
    internal static class Day3
    {
        public static void GetTaskResults()
        {
            var lines = File.ReadAllLines($"{Directory.GetCurrentDirectory()}\\Inputs\\Day3.txt");
            var totalScorePart1 = 0;

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

                // Part 2 pre-work
                if (i % 3 == 0)
                {
                    elfGroups.Add(++groupKey, new List<string>());
                }

                elfGroups[groupKey].Add(line);
            }

            // Part 2 (without pre-work done in previous loop)
            var totalScorePart2 = 0;
            foreach (var elfGroup in elfGroups)
            {
                if (elfGroup.Value.Count != 3) throw new InvalidOperationException("Elves have to be in groups of three.");

                var groupBadge = elfGroup.Value[0].First(letterFromFirstBag =>
                    elfGroup.Value[1].Contains(letterFromFirstBag) &&
                    elfGroup.Value[2].Contains(letterFromFirstBag));

                totalScorePart2 += groupBadge.ToElfPriority();
            }

            Console.WriteLine($"Task 1 result is: {totalScorePart1}");
            Console.WriteLine($"Task 2 result is: {totalScorePart2}");
        }

        // a-z => 1-26
        // A-Z => 27-52
        private static int ToElfPriority(this char character) => char.IsUpper(character) ? character - 64 + 26 : character - 96;
    };
}