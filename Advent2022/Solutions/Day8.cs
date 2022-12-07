namespace Advent2022.Solutions;

internal static class Day8
{
    private static readonly string InputPath = $"{Directory.GetCurrentDirectory()}\\Inputs\\Day8.txt";
    private static readonly string ExampleInputPath = $"{Directory.GetCurrentDirectory()}\\Inputs\\Day8_example.txt";

    public static void GetTaskResults()
    {
        var useExample = false;
        var input = File.ReadAllLines(useExample ? ExampleInputPath : InputPath);

        var task1Result = 0;
        var task2Result = 0;

        Console.WriteLine($"Task 1 result is: {task1Result}");
        Console.WriteLine($"Task 2 result is: {task2Result}");
    }
}
