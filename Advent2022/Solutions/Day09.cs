namespace Advent2022.Solutions;

internal static class Day09
{
    private static readonly string InputPath = $"{Directory.GetCurrentDirectory()}\\Inputs\\Day09.txt";
    private static readonly string ExampleInputPath = $"{Directory.GetCurrentDirectory()}\\Inputs\\Day09_example.txt";
    private const int NumberOfKnots = 10; // Use 2 for Task 1

    public static void GetTaskResults()
    {
        var useExample = false;
        var lines = File.ReadAllLines(useExample ? ExampleInputPath : InputPath);
        var knots = Enumerable.Range(1, 10).Select(_ => new Knot()).ToList();

        var coveredSpacesByEndKnot = new HashSet<(int, int)> { (0, 0) };

        foreach (var line in lines)
        {
            var splitInput = line.Split(' ', 2);
            var (direction, moveCount) = (splitInput[0], int.Parse(splitInput[1]));

            for (int i = 0; i < moveCount; i++)
            {
                knots[0].Move(direction);

                for (int knot = 1; knot < knots.Count; knot++)
                {
                    knots[knot].Follow(knots[knot - 1]);
                }

                var endKnot = knots[NumberOfKnots - 1];
                coveredSpacesByEndKnot.Add((endKnot.X, endKnot.Y));
            }
        }

        Console.WriteLine($"Task result is: {coveredSpacesByEndKnot.Count}");
    }
}

internal class Knot
{
    public int X { get; set; }
    public int Y { get; set; }
}

internal static class KnotExtensions
{
    internal static void Move(this Knot knot, string direction)
    {
        switch (direction)
        {
            case "U":
                knot.Y++;
                break;
            case "R":
                knot.X++;
                break;
            case "D":
                knot.Y--;
                break;
            case "L":
                knot.X--;
                break;
        }
    }

    internal static void Follow(this Knot knot, Knot knotToFollow)
    {
        if (!IsMoveNeeded(knotToFollow, knot))
        {
            return;
        }

        if (knotToFollow.Y > knot.Y) { knot.Y++; };
        if (knotToFollow.Y < knot.Y) { knot.Y--; };
        if (knotToFollow.X > knot.X) { knot.X++; };
        if (knotToFollow.X < knot.X) { knot.X--; };
    }

    private static bool IsMoveNeeded(Knot knot1, Knot knot2) =>
        Math.Abs(knot1.X - knot2.X) > 1 || Math.Abs(knot1.Y - knot2.Y) > 1;
}