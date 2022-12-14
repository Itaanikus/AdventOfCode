namespace Advent2022.Solutions;

internal static class Day10
{
    private static readonly string InputPath = $"{Directory.GetCurrentDirectory()}\\Inputs\\Day10.txt";
    private static readonly string ExampleInputPath = $"{Directory.GetCurrentDirectory()}\\Inputs\\Day10_example.txt";

    public static void GetTaskResults()
    {
        var useExample = false;
        var lines = File.ReadAllLines(useExample ? ExampleInputPath : InputPath);
        var cycleValue = 1;
        var cycleNumber = 1;
        var crtPosition = 0;
        var task1Result = 0;
        var imageString = new List<char>();
        var spritePosition = 1;

        for (int lineIndex = 0; lineIndex < lines.Length; lineIndex++)
        {
            imageString.Add(Math.Abs((crtPosition % 40) - spritePosition) <= 1 ? '#' : '.');

            if (cycleNumber % 40 - 20 == 0)
            {
                task1Result += cycleNumber * cycleValue;
            }

            if (lines[lineIndex] == "noop")
            {
                cycleNumber++;
                crtPosition++;
            }
            else if (lines[lineIndex].StartsWith("addx"))
            {
                var addValue = int.Parse(lines[lineIndex].Split(' ')[1]);

                // First cycle
                cycleNumber++;
                crtPosition++;

                imageString.Add(Math.Abs((crtPosition % 40) - spritePosition) <= 1 ? '#' : '.');

                if (cycleNumber % 40 - 20 == 0)
                {
                    task1Result += cycleNumber * cycleValue;
                }

                // Second cycle
                cycleNumber++;
                crtPosition++;
                cycleValue += addValue;
                spritePosition = cycleValue;
            }
        }

        Console.WriteLine($"Task 1 result is: {task1Result}");

        for (int i = 0; i < imageString.Count; i+=40)
        {
            Console.WriteLine(new string(imageString.Skip(i).Take(40).ToArray()));
        }
    }
}