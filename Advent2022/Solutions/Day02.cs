namespace Advent2022.Solutions
{
    internal static class Day02
    {
        public static void GetTaskResults()
        {
            #region Part1 & Part2
            var lines = File.ReadAllLines($"{Directory.GetCurrentDirectory()}\\Inputs\\Day02.txt");
            var totalScorePart1 = 0;
            var totalScorePart2 = 0;

            foreach (var line in lines)
            {
                var strategyGuideResults = line.Split(' ', 2);
                var opponentChoice = strategyGuideResults[0];
                var bestChoice = strategyGuideResults[1];

                totalScorePart1 += CalculatePart1(opponentChoice, bestChoice);
                totalScorePart2 += CalculatePart2(opponentChoice, bestChoice);
            }

            Console.WriteLine($"Task 1 result is: {totalScorePart1}");
            Console.WriteLine($"Task 2 result is: {totalScorePart2}");
            #endregion
        }

        private static int CalculatePart1(string opponentChoice, string bestChoice) => (opponentChoice, bestChoice) switch
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

        private static int CalculatePart2(string opponentChoice, string bestChoice) => (opponentChoice, bestChoice) switch
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
}