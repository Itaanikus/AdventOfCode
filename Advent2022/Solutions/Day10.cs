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
            if (cycleNumber % 40 - 20 == 0)
            {
                task1Result += cycleNumber * cycleValue;
            }
            if (Math.Abs((crtPosition % 40) - spritePosition) <= 1)
            {
                imageString.Add('#');
            }
            else
            {
                imageString.Add('.');
            }

            if (lines[lineIndex] == "noop")
            {
                cycleNumber++;
                crtPosition++;
            }

            if (lines[lineIndex].StartsWith("addx"))
            {
                var addValue = int.Parse(lines[lineIndex].Split(' ')[1]);

                // First cycle
                cycleNumber++;
                crtPosition++;
                if (cycleNumber % 40 - 20 == 0)
                {
                    task1Result += cycleNumber * cycleValue;
                }

                if (Math.Abs((crtPosition % 40) - spritePosition) <= 1)
                {
                    imageString.Add('#');
                }
                else
                {
                    imageString.Add('.');
                }

                // Second cycle
                cycleNumber++;
                crtPosition++;
                cycleValue += addValue;
                spritePosition = cycleValue;
            }
        }

        Console.WriteLine($"Task 1 result is: {task1Result}");

        for (int i = 0; i < imageString.Count; i++)
        {
            Console.WriteLine(new string(imageString.Skip(i).Take(40).ToArray()));
            i += 39;
        }
    }
}