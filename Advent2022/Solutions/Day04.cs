using System.Runtime.CompilerServices;

namespace Advent2022.Solutions
{
    internal static class Day04
    {
        public static void GetTaskResults()
        {
            var input = File.ReadAllLines($"{Directory.GetCurrentDirectory()}\\Inputs\\Day04.txt");
            var task1Result = 0;
            var task2Result = 0;

            foreach (var line in input)
            {
                var pairs = line.Trim().Split(',');

                if (pairs.Length != 2)
                {
                    throw new NotSupportedException("Dude, you cannot have a pair with less/more than two elements! :')");
                }

                var first = pairs[0].Split('-');
                var second = pairs[1].Split('-');

                if (first.Length != 2 || second.Length != 2)
                {
                    throw new NotSupportedException("Pair had more than start and end number?! Elf-sections do not make sense at all!");
                }

                var firstPair = new CleaningSectionPair(int.Parse(first[0]), int.Parse(first[1]));
                var secondPair = new CleaningSectionPair(int.Parse(second[0]), int.Parse(second[1]));

                switch (firstPair - secondPair)
                {
                    case ( <= 0, >= 0):
                        task1Result += 1;
                        task2Result += 1;
                        break;
                    case ( >= 0, <= 0):
                        task1Result += 1;
                        task2Result += 1;
                        break;
                    case (_, _) when firstPair.DoesOverlapWith(secondPair):
                        task2Result += 1;
                        break;
                }
            }

            Console.WriteLine($"Task 1 result is: {task1Result}");
            Console.WriteLine($"Task 2 result is: {task2Result}");
        }
    }

    internal readonly record struct CleaningSectionPair(int StartSection, int EndSection)
    {
        public static CleaningSectionPair operator -(CleaningSectionPair pair1, CleaningSectionPair pair2) =>
            new(pair1.StartSection - pair2.StartSection, pair1.EndSection - pair2.EndSection);
    }

    internal static class CleaningSectionPairExtensions
    {
        internal static bool DoesOverlapWith(this CleaningSectionPair pair1, CleaningSectionPair pair2) =>
            (pair1.StartSection <= pair2.EndSection && pair1.EndSection >= pair2.StartSection) ||
            (pair1.StartSection >= pair2.EndSection && pair1.EndSection <= pair2.StartSection);
    }
}