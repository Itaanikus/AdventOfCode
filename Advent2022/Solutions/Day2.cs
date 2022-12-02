namespace Advent2022.Solutions
{
    internal static class Day2
    {
        public static void GetTaskResults()
        {
            #region Part1
            var lines = File.ReadAllLines($"{Directory.GetCurrentDirectory()}\\Inputs\\Day2.txt");
            var totalScore = 0;

            foreach (var line in lines)
            {
                var strategyGuideResults = line.Split(' ', 2);
                var opponentChoice = strategyGuideResults[0];
                var bestChoice = strategyGuideResults[1];

                totalScore += (opponentChoice, bestChoice) switch
                {
                    ("A", "X") => 3 + 1,
                    ("B", "X") => 0 + 1,
                    ("C", "X") => 6 + 1,
                    ("A", "Y") => 6 + 2,
                    ("B", "Y") => 3 + 2,
                    ("C", "Y") => 0 + 2,
                    ("A", "Z") => 0 + 3,
                    ("B", "Z") => 6 + 3,
                    ("C", "Z") => 3 + 3,
                    _ => throw new NotSupportedException()
                };
            }

            var partOneResult = totalScore;
            Console.WriteLine($"Task 1 result is: {partOneResult}");
            #endregion

            #region Part2
            var totalScorePart2 = 0;

            foreach (var line in lines)
            {
                var strategyGuideResults = line.Split(' ', 2);
                var opponentChoice = strategyGuideResults[0];
                var bestChoice = strategyGuideResults[1];

                totalScorePart2 += (opponentChoice, bestChoice) switch
                {
                    ("A", "X") => 3 + 0,
                    ("B", "X") => 1 + 0,
                    ("C", "X") => 2 + 0,
                    ("A", "Y") => 1 + 3,
                    ("B", "Y") => 2 + 3,
                    ("C", "Y") => 3 + 3,
                    ("A", "Z") => 2 + 6,
                    ("B", "Z") => 3 + 6,
                    ("C", "Z") => 1 + 6,
                    _ => throw new NotSupportedException()
                };
            }

            var partTwoResult = totalScorePart2;
            Console.WriteLine($"Task 2 result is: {partTwoResult}");
            #endregion
        }
    }
}