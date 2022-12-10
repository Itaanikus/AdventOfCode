using System.Diagnostics;

namespace Advent2022.Solutions;

internal static class Day06
{
    private static readonly string InputPath = $"{Directory.GetCurrentDirectory()}\\Inputs\\Day6.txt";
    private static readonly string TestInputPath = $"{Directory.GetCurrentDirectory()}\\Inputs\\Day6test.txt";

    public static void GetTaskResults()
    {
        var finalAnswer = true;
        var input = File.ReadAllText(finalAnswer ? InputPath : TestInputPath);

        var taskResult = 0;

        const int DistinctCharsInARow = 14; // Use value 4|14 for part 1|2

        for (int i = DistinctCharsInARow; i < input.Length; i++)
        {
            var startIndex = i - DistinctCharsInARow;
            var charsToCheckForUniqueness = input[startIndex..i];
            var uniqueSet = new HashSet<char>();

            if (charsToCheckForUniqueness.All(uniqueSet.Add))
            {
                taskResult = i;
                break;
            }
        }

        Console.WriteLine($"Task result is: {taskResult}");
    }
}