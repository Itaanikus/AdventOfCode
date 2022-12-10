namespace Advent2022.Solutions;

internal static class Day11
{
    private static readonly string InputPath = $"{Directory.GetCurrentDirectory()}\\Inputs\\Day11.txt";
    private static readonly string ExampleInputPath = $"{Directory.GetCurrentDirectory()}\\Inputs\\Day11_example.txt";

    public static void GetTaskResults()
    {
        var useExample = false;
        var lines = File.ReadAllLines(useExample ? ExampleInputPath : InputPath);
    }
}