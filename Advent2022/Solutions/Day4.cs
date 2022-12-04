namespace Advent2022.Solutions
{
    internal static class Day4
    {
        public static void GetTaskResults()
        {
            var input = File.ReadAllLines($"{Directory.GetCurrentDirectory()}\\Inputs\\Day4.txt");
            var task1Result = 0;
            var task2Result = 0;

            foreach (var line in input)
            {
                var pairs = line.Trim().Split(',');
                var first = pairs[0].Split('-');
                var second = pairs[1].Split('-');
                var (firstPairStart, firstPairEnd) = (first[0].ToNumber(), first[1].ToNumber());
                var (secondPairStart, secondPairEnd) = (second[0].ToNumber(), second[1].ToNumber());

                if (first.Length != 2 || second.Length != 2)
                {
                    throw new NotSupportedException("Pair had more than start and end number.");
                }

                if (firstPairStart <= secondPairStart && firstPairEnd >= secondPairEnd)
                {
                    task1Result += 1;
                    task2Result += 1;
                }
                else if (firstPairStart >= secondPairStart && firstPairEnd <= secondPairEnd)
                {
                    task1Result += 1;
                    task2Result += 1;
                }
                else if (firstPairStart <= secondPairStart && firstPairEnd >= secondPairStart)
                {
                    task2Result += 1;
                }
                else if (secondPairStart <= firstPairStart && secondPairEnd >= firstPairStart)
                {
                    task2Result += 1;
                }
            }

            Console.WriteLine($"Task 1 result is: {task1Result}");
            Console.WriteLine($"Task 2 result is: {task2Result}");
        }

        private static int ToNumber(this string input) => int.Parse(input);
    };
}