namespace Advent2022.Solutions;

internal static class Day12
{
    private static readonly string InputPath = $"{Directory.GetCurrentDirectory()}\\Inputs\\Day12.txt";
    private static readonly string ExampleInputPath = $"{Directory.GetCurrentDirectory()}\\Inputs\\Day12ex.txt";
    private static readonly string CharSet = "Sabcdefghijklmnopqrstuvwxyz";

    public static void GetTaskResults()
    {
        var useExample = false;
        var lines = File.ReadAllLines(useExample ? ExampleInputPath : InputPath);
        var rowCount = lines.Length;
        var columnCount = lines[0].Length;
        var maze = new Node[columnCount, rowCount];

        var startNode = (0, 0);
        var startNodes = new List<Node>();

        for (int y = 0; y < rowCount; y++)
        {
            for (int x = 0; x < columnCount; x++)
            {
                maze[x, y] = new Node
                {
                    X = x,
                    Y = y,
                    Height = CharSet.IndexOf(lines[y][x]),
                    Char = lines[y][x],
                };

                if (lines[y][x] == 'a')
                {
                    startNodes.Add(maze[x, y]);
                }

                if (lines[y][x] == 'S')
                {
                    startNode = (x, y);
                    maze[x, y].Visited = true;
                    startNodes.Add(maze[x, y]);
                }

                if (lines[y][x] == 'E')
                {
                    maze[x, y].Height = CharSet.IndexOf('z');
                }
            }
        }

        // Part 1. Remove for part 2
        var isPartOne = false;
        if (isPartOne)
        {
            startNodes = new List<Node> { maze[startNode.Item1, startNode.Item2] };
        }

        var lowestEndResult = int.MaxValue;
        foreach (var item in startNodes)
        {
            var copyMaze = maze.Clone() as Node[,];
            var endResult = 0;
            var hasFoundEnd = false;
            var roundQueue = new PriorityQueue<Node, int>();
            roundQueue.QueueNeighbours(copyMaze![item.X, item.Y], copyMaze, rowCount, columnCount);
            while (!hasFoundEnd && roundQueue.TryPeek(out _, out _))
            {
                var node = roundQueue.Dequeue();

                roundQueue.QueueNeighbours(node, copyMaze, rowCount, columnCount);
                if (node.Char == 'E')
                {
                    hasFoundEnd = true;
                    endResult = node.Distance;
                }
            }

            if (endResult != 0 && lowestEndResult > endResult)
            {
                lowestEndResult = endResult;
            }
        }

        Console.WriteLine(lowestEndResult);
    }

    private static void QueueNeighbours(this PriorityQueue<Node, int> queue, Node node, Node[,] maze, int maxRow, int maxColumn)
    {
        if (node.X != (maxColumn - 1) && !maze[node.X + 1, node.Y].Visited && maze[node.X + 1, node.Y].Height - node.Height <= 1)
        {
            // Right
            maze[node.X + 1, node.Y].Distance += node.Distance + 1;
            maze[node.X + 1, node.Y].Visited = true;
            queue.Enqueue(maze[node.X + 1, node.Y], maze[node.X + 1, node.Y].Distance);
        }

        if (node.X != 0 && !maze[node.X - 1, node.Y].Visited && maze[node.X - 1, node.Y].Height - node.Height <= 1)
        {
            // Left
            maze[node.X - 1, node.Y].Distance += node.Distance + 1;
            maze[node.X - 1, node.Y].Visited = true;
            queue.Enqueue(maze[node.X - 1, node.Y], maze[node.X - 1, node.Y].Distance);
        }

        if (node.Y != (maxRow - 1) && !maze[node.X, node.Y + 1].Visited && maze[node.X, node.Y + 1].Height - node.Height <= 1)
        {
            maze[node.X, node.Y + 1].Distance += node.Distance + 1;
            maze[node.X, node.Y + 1].Visited = true;
            queue.Enqueue(maze[node.X, node.Y + 1], maze[node.X, node.Y + 1].Distance);
        }

        if (node.Y != 0 && !maze[node.X, node.Y - 1].Visited && maze[node.X, node.Y - 1].Height - node.Height <= 1)
        {
            maze[node.X, node.Y - 1].Distance += node.Distance + 1;
            maze[node.X, node.Y - 1].Visited = true;
            queue.Enqueue(maze[node.X, node.Y - 1], maze[node.X, node.Y - 1].Distance);
        }
    }

    private record struct Node
    {
        public int X { get; set; }
        public int Y { get; set; }

        public int Height { get; set; }

        public bool Visited { get; set; }

        public int Distance { get; set; }

        public char Char { get; set; }
    }
}

