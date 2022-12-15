using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;

namespace Advent2022.Solutions;

internal static class Day14
{
    private static readonly string InputPath = $"{Directory.GetCurrentDirectory()}\\Inputs\\Day13.txt";
    private static readonly string ExampleInputPath = $"{Directory.GetCurrentDirectory()}\\Inputs\\Day14ex.txt";

    public static void GetTaskResults()
    {
        var useExample = true;
        var lines = File.ReadAllLines(useExample ? ExampleInputPath : InputPath);

        var rocks = new List<Rock>();

        foreach (var line in lines)
        {
            var rockTrajectoryInput = line.Split(" -> ");
            rocks.AddFrom(rockTrajectoryInput);
        }

        var grid = InitializeGridWithRocks(rocks);

        var canAddMoreSand = true;
        while (canAddMoreSand)
        {
            canAddMoreSand = grid.AddSand(rocks);
        }
    }

    private static bool AddSand(this char[,] grid, List<Rock> rocks)
    {
        var sandX = 500;
        var sandY = 0;
        while (grid[sandX, sandY + 1] == '.')
        {
            if (sandY == grid.GetLength(1))
            {
                return false;
            }

            if (grid[sandX, sandY + 1] != '.')
            {
                if (sandX == 0)
                {
                    return false;
                }
                if (grid[sandX - 1, sandY + 1] != '.')
                {
                    if (sandX == grid.GetLength(0))
                    {
                        return false;
                    }
                    if (grid[sandX + 1, sandY + 1] != '.')
                    {
                        if (sandX == grid.GetLength(0))
                        {
                            return false;
                        }
                        grid[sandX, sandY] = 'o';

                        for (int y = 0; y < rocks.Max(r => r.Y) + 1; y++)
                        {
                            var rowString = string.Empty;
                            for (int x = rocks.Min(r => r.X); x < rocks.Max(r => r.X) + 1; x++)
                            {
                                rowString += grid[x, y];
                            }
                            Console.WriteLine(rowString);
                        }
                        grid.AddSand(rocks);
                    }
                    sandX++;
                    sandY++;

                    for (int y = 0; y < rocks.Max(r => r.Y) + 1; y++)
                    {
                        var rowString = string.Empty;
                        for (int x = rocks.Min(r => r.X); x < rocks.Max(r => r.X) + 1; x++)
                        {
                            rowString += grid[x, y];
                        }
                        Console.WriteLine(rowString);
                    }
                    continue;
                }
                sandX--;
                sandY++;

                for (int y = 0; y < rocks.Max(r => r.Y) + 1; y++)
                {
                    var rowString = string.Empty;
                    for (int x = rocks.Min(r => r.X); x < rocks.Max(r => r.X) + 1; x++)
                    {
                        rowString += grid[x, y];
                    }
                    Console.WriteLine(rowString);
                }
                continue;
            }

            sandY++;
            continue;
        }
        return true;
    }

    private static char[,] InitializeGridWithRocks(List<Rock> rocks)
    {
        var grid = new char[rocks.Max(r => r.X) + 1, rocks.Max(r => r.Y) + 1];

        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                grid[x, y] = '.';
            }
        }

        grid[500, 0] = '+';
        foreach (var rock in rocks)
        {
            grid[rock.X, rock.Y] = '#';
        }
        return grid;
    }

    private record struct Rock(int X, int Y);

    private static void AddFrom(this List<Rock> rocks, string[] rockInput)
    {
        var firstRock = rockInput[0].Split(',');
        var rocksToAdd = new Stack<Rock>();
        rocksToAdd.Push(new Rock(int.Parse(firstRock[0]), int.Parse(firstRock[1])));

        for (int i = 1; i < rockInput.Length; i++)
        {
            var endpoint = rockInput[i].Split(',');
            var endpointX = int.Parse(endpoint[0]);
            var endpointY = int.Parse(endpoint[1]);

            var previousEndpoint = rocksToAdd.Peek();
            var xDiff = endpointX - previousEndpoint.X;
            var yDiff = endpointY - previousEndpoint.Y;
            switch (xDiff, yDiff)
            {
                case ( > 0, _):
                    {
                        for (int j = 0; j < endpointX - previousEndpoint.X; j++)
                        {
                            rocksToAdd.Push(previousEndpoint with { X = previousEndpoint.X + j + 1 });
                        }

                        break;
                    }
                case ( < 0, _):
                    {
                        for (int j = 0; j < previousEndpoint.X - endpointX; j++)
                        {
                            rocksToAdd.Push(previousEndpoint with { X = previousEndpoint.X - j - 1 });
                        }

                        break;
                    }
                case (_, > 0):
                    {
                        for (int j = 0; j < endpointY - previousEndpoint.Y; j++)
                        {
                            rocksToAdd.Push(previousEndpoint with { Y = previousEndpoint.Y + j + 1 });
                        }

                        break;
                    }
                case (_, < 0):
                    {
                        for (int j = 0; j < previousEndpoint.Y - endpointY; j++)
                        {
                            rocksToAdd.Push(previousEndpoint with { Y = previousEndpoint.Y - j - 1 });
                        }

                        break;
                    }

                default:
                    throw new NotSupportedException();
            }
        }

        rocks.AddRange(rocksToAdd.ToArray());
    }
}
