using System.Diagnostics;

namespace Advent2022.Solutions;

internal static class Day6
{
    private static readonly string InputPath = $"{Directory.GetCurrentDirectory()}\\Inputs\\Day6.txt";
    private static readonly string TestInputPath = $"{Directory.GetCurrentDirectory()}\\Inputs\\Day6test.txt";

    public static void GetTaskResults()
    {
        var finalAnswer = true;
        var input = File.ReadAllText(finalAnswer ? InputPath : TestInputPath);

        var task1Result = 0;
        var task2Result = 0;

        const int QueueLimit = 14; // Change to 14 for part 2...
        var fourLetterQueue = new Queue<char>(QueueLimit + 1);

        for (int i = 0; i < input.Length; i++)
        {
            var letter = input[i];
            fourLetterQueue.Enqueue(letter);

            if (fourLetterQueue.Count > QueueLimit)
            {
                fourLetterQueue.Dequeue();
            }

            if (fourLetterQueue.Count == QueueLimit && IsUnique(fourLetterQueue))
            {
                task1Result = i + 1;
                break;
            }
        }


        Console.WriteLine($"Task 1 result is: {task1Result}");
        Console.WriteLine($"Task 2 result is: {task2Result}");
    }

    private static bool IsUnique(Queue<char> queue)
    {
        var diffChecker = new HashSet<char>();
        return queue.All(diffChecker.Add);
    }
}