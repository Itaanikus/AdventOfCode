using System.Diagnostics;

namespace Advent2022.Solutions;

internal static class Day7
{
    private static readonly string InputPath = $"{Directory.GetCurrentDirectory()}\\Inputs\\Day7.txt";
    private static readonly string TestInputPath = $"{Directory.GetCurrentDirectory()}\\Inputs\\Day7test.txt";

    public static void GetTaskResults()
    {
        var fileSystem = GetSystemDirectoryFromInput(true);

        var unusedSpace = 70000000 - fileSystem.Size;
        var sizeToBeDeleted = 30000000 - unusedSpace;

        var flattenedSystem = fileSystem.Flatten();
        var task1Result = flattenedSystem.Where(x => x.Size <= 100000).Select(x => x.Size).Sum();
        var task2Result = flattenedSystem.OrderBy(x => x.Size).First(x => x.Size >= sizeToBeDeleted).Size;

        Console.WriteLine($"Task 1 result is: {task1Result}");
        Console.WriteLine($"Task 2 result is: {task2Result}");
    }

    private static SystemDirectory GetSystemDirectoryFromInput(bool finalAnswer)
    {
        var input = File.ReadAllLines(finalAnswer ? InputPath : TestInputPath);
        var fileSystem = new SystemDirectory("/");
        SystemDirectory currentDir = fileSystem;

        foreach (var line in input)
        {
            if (line.StartsWith("$ cd"))
            {
                var changeToDir = line[5..];

                switch (changeToDir)
                {
                    case "..":
                        currentDir = currentDir.Parent!;
                        break;
                    case "/":
                        while (currentDir.Parent != null)
                        {
                            currentDir = currentDir.Parent;
                        }
                        break;
                    default:
                        currentDir = currentDir.Directories.Single(x => x.Name == changeToDir);
                        break;
                }
            }
            else if (line.StartsWith("$ ls"))
            {
                continue;
            }
            else if (line.StartsWith("dir "))
            {
                var directoryName = line[4..];
                if (!currentDir.Directories.Any(x => x.Name == directoryName))
                {
                    currentDir.Directories.Add(new SystemDirectory(directoryName, currentDir));
                }
            }
            else
            {
                var fileLine = line.Split(' ');
                var size = long.Parse(fileLine[0]);
                var name = fileLine[1];
                currentDir.Files.Add(new Files(currentDir, name, size));

                var tempCurrentDir = currentDir;
                while (tempCurrentDir != null)
                {
                    tempCurrentDir.Size += size;
                    tempCurrentDir = tempCurrentDir.Parent;
                }
            }
        }

        return fileSystem;
    }
}

internal class SystemDirectory
{
    internal SystemDirectory(string name, SystemDirectory? parent = default)
    {
        Name = name;
        Parent = parent;
    }
    internal string Name { get; }
    internal long Size { get; set; }
    internal SystemDirectory? Parent { get; }
    internal List<SystemDirectory> Directories { get; set; } = new();
    internal List<Files> Files { get; set; } = new();
};

internal static class SystemDirectoryExtensions
{
    internal static IEnumerable<SystemDirectory> Flatten(this SystemDirectory directory)
    {
        var result = new List<SystemDirectory>();

        foreach (var item in directory.Directories)
        {
            result.AddRange(item.Flatten());
            if (!result.Contains(item))
                result.Add(item);
        }

        return result;
    }
}

internal record struct Files(SystemDirectory Directorystring, string Name, long Size);
