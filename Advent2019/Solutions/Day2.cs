using System.Collections.Immutable;

namespace Advent2019.Solutions;

internal static class Day2
{
    private static readonly string InputPath = $"{Directory.GetCurrentDirectory()}\\Inputs\\Day2.txt";
    private static readonly string TestInputPath = $"{Directory.GetCurrentDirectory()}\\Inputs\\Day2_example.txt";

    public static void GetTaskResults()
    {
        var useExample = false;
        var input = File.ReadAllText(useExample ? TestInputPath : InputPath)
            .Split(',')
            .Select(x => int.Parse(x))
            .ToImmutableArray();

        var task1Result = HandleProgram(input.ToArray(), 12, 2);
        var task2Result = 0;

        for (var noun = 0; noun < 100 && task2Result == 0; noun++)
        {
            for (var verb = 0; verb < 100; verb++)
            {
                var result = HandleProgram(input.ToArray(), noun, verb);

                if (result == 19690720)
                {
                    task2Result = noun * 100 + verb;
                    break;
                }
            }
        }

        Console.WriteLine($"Task 1 result is: {task1Result}");
        Console.WriteLine($"Task 2 result is: {task2Result}");
    }

    private static int HandleProgram(int[] input, int noun, int verb)
    {
        input[1] = noun;
        input[2] = verb;

        for (int i = 0; i < input.Length; i++)
        {
            var opCode = input[i];
            var param1 = input[i + 1];
            var param2 = input[i + 2];
            var param3 = input[i + 3];

            switch (opCode)
            {
                case 1:
                    input[param3] = input[param1] + input[param2];
                    break;
                case 2:
                    input[param3] = input[param1] * input[param2];
                    break;
                case 99:
                    return input[0];
                default:
                    throw new NotSupportedException();
            }

            i += 3;
        }

        throw new InvalidOperationException();
    }
}
