namespace Advent2022.Solutions;

internal static class Day08
{
    private static readonly string InputPath = $"{Directory.GetCurrentDirectory()}\\Inputs\\Day08.txt";
    private static readonly string ExampleInputPath = $"{Directory.GetCurrentDirectory()}\\Inputs\\Day08_example.txt";

    public static void GetTaskResults()
    {
        var useExample = false;
        var lines = File.ReadAllLines(useExample ? ExampleInputPath : InputPath);
        var (dimension1Length, dimension2Length) = (lines.Length, lines[0].Length);
        var treeGrid = new int[dimension1Length, dimension2Length];
        var rows = new string[dimension2Length];
        var columns = new string[dimension1Length];

        for (int x = 0; x < dimension1Length; x++)
        {
            var line = lines[x];
            rows[x] = line;

            for (int y = 0; y < dimension2Length; y++)
            {
                columns[y] += line[y].ToString();
                treeGrid[x, y] = int.Parse(line[y].ToString());
            }
        }

        var task1Result = 0;
        var task2Result = 0;

        for (int x = 0; x < dimension1Length; x++)
        {
            for (int y = 0; y < dimension2Length; y++)
            {
                if (x == 0 || x == dimension1Length - 1 || y == 0 || y == dimension2Length - 1)
                {
                    task1Result++;
                    continue;
                }

                // Part 1
                var treeGridValue = treeGrid[x, y];
                if (!columns[y].Where((val, idx) => idx < x).Any(val => int.Parse(val.ToString()) >= treeGridValue) ||
                    !columns[y].Where((val, idx) => idx > x).Any(val => int.Parse(val.ToString()) >= treeGridValue) ||
                    !rows[x].Where((val, idx) => idx < y).Any(val => int.Parse(val.ToString()) >= treeGridValue) ||
                    !rows[x].Where((val, idx) => idx > y).Any(val => int.Parse(val.ToString()) >= treeGridValue))
                {
                    task1Result++;
                }

                // Part 2
                var up = 0;
                var left = 0;
                var down = 0;
                var right = 0;
                for (int i = y - 1; i >= 0; i--)
                {
                    var treeInSight = treeGrid[x, i];
                    if (treeInSight == 0)
                    {
                        continue;
                    }
                    left += 1;
                    if (treeGridValue <= treeInSight)
                    {
                        break;
                    }
                }
                for (int i = y + 1; i < dimension1Length; i++)
                {
                    var treeInSight = treeGrid[x, i];
                    if (treeInSight == 0)
                    {
                        continue;
                    }
                    right += 1;
                    if (treeGridValue <= treeInSight)
                    {
                        break;
                    }
                }
                for (int i = x - 1; i >= 0; i--)
                {
                    var treeInSight = treeGrid[i, y];
                    if (treeInSight == 0)
                    {
                        continue;
                    }
                    up += 1;
                    if (treeGridValue <= treeInSight)
                    {
                        break;
                    }
                }
                for (int i = x + 1; i < dimension2Length; i++)
                {
                    var treeInSight = treeGrid[i, y];
                    if (treeInSight == 0)
                    {
                        continue;
                    }
                    down += 1;
                    if (treeGridValue <= treeInSight)
                    {
                        break;
                    }
                }

                var scenicScore = left * right * up * down;
                if (task2Result < scenicScore) { task2Result = scenicScore; }
            }
        }

        Console.WriteLine($"Task 1 result is: {task1Result}");
        Console.WriteLine($"Task 2 result is: {task2Result}");
    }
}
