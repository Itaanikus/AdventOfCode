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
        var fourLetterQueue = new Queue<char>();

        for (int i = 0; i < input.Length; i++)
        {
            var letter = input[i];
            fourLetterQueue.Enqueue(letter);

            if (fourLetterQueue.Count == 15)
            {
                fourLetterQueue.Dequeue();
            }

            if (fourLetterQueue.Count == 14 && fourLetterQueue.Distinct().Count() == 14)
            {
                task1Result = i + 1;
                break;
            }
        }


        Console.WriteLine($"Task 1 result is: {task1Result}");
        Console.WriteLine($"Task 2 result is: {task2Result}");
    }
}