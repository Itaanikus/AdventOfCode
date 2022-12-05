namespace Advent2022.Solutions;

internal static class Day6
{
    private static readonly string InputPath = $"{Directory.GetCurrentDirectory()}\\Inputs\\Day6.txt";
    private static readonly string TestInputPath = $"{Directory.GetCurrentDirectory()}\\Inputs\\Day6test.txt";

    public static void GetTaskResults(bool isRealInput = false)
    {
        var input = File.ReadAllLines(isRealInput ? InputPath : TestInputPath);

        foreach (var line in input)
        {

        }

        var task1Result = 0;
        var task2Result = 0;

        Console.WriteLine($"Task 1 result is: {task1Result}");
        Console.WriteLine($"Task 2 result is: {task2Result}");
    }
}