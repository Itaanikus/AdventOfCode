namespace Advent2022.Solutions;

internal static class Day9
{
    private static readonly string InputPath = $"{Directory.GetCurrentDirectory()}\\Inputs\\Day9.txt";
    private static readonly string ExampleInputPath = $"{Directory.GetCurrentDirectory()}\\Inputs\\Day9_example.txt";
    private const int NumberOfKnots = 10; // Use 2 for Task 1

    public static void GetTaskResults()
    {
        var useExample = false;
        var lines = File.ReadAllLines(useExample ? ExampleInputPath : InputPath);
        var knots = GenerateKnots(NumberOfKnots);

        var coveredSpacesByEndKnot = new HashSet<(int, int)> { (0, 0) };

        foreach (var line in lines)
        {
            var splitInput = line.Split(' ', 2);
            var (direction, moveCount) = (splitInput[0], int.Parse(splitInput[1]));

            for (int moveNumber = 0; moveNumber < moveCount; moveNumber++)
            {
                knots[0].Move(direction);

                for (int knotNumber = 1; knotNumber < knots.Count; knotNumber++)
                {
                    knots[knotNumber].Follow(knots[knotNumber-1]);
                }

                var endKnot = knots[NumberOfKnots - 1];
                coveredSpacesByEndKnot.Add((endKnot.X, endKnot.Y));
            }

        }

        Console.WriteLine($"Task result is: {coveredSpacesByEndKnot.Count}");
    }

    private static List<Knot> GenerateKnots(int numberOfKnots)
    {
        var knots = new List<Knot>();

        for (int i = 0; i < numberOfKnots; i++)
        {
            knots.Add(new());
        }

        return knots;
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
        if (!MoveIsNeeded(knotToFollow, knot))
        {
            return;
        }

        if (knotToFollow.Y > knot.Y)
        {
            knot.Y++;
        }

        if (knotToFollow.Y < knot.Y)
        {
            knot.Y--;
        }

        if (knotToFollow.X > knot.X)
        {
            knot.X++;
        }

        if (knotToFollow.X < knot.X)
        {
            knot.X--;
        }
    }

    private static bool MoveIsNeeded(Knot knot1, Knot knot2)
        => Math.Abs(knot1.X - knot2.X) > 1 || Math.Abs(knot1.Y - knot2.Y) > 1;
}