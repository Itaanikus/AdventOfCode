using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;

namespace Advent2022.Solutions;

internal static class Day7
{
    private static readonly string InputPath = $"{Directory.GetCurrentDirectory()}\\Inputs\\Day7.txt";
    private static readonly string TestInputPath = $"{Directory.GetCurrentDirectory()}\\Inputs\\Day7test.txt";

    public static void GetTaskResults()
    {
        var fileSystem = GetSystemDirectoryFromInput(true);

        var unusedSpace = 70000000 - fileSystem.Size;
        var spaceToClean = 30000000 - unusedSpace;

        var flattenedSystem = fileSystem.Flatten();
        var task1Result = flattenedSystem.Where(x => x.Size <= 100000).Select(x => x.Size).Sum();
        var task2Result = flattenedSystem.OrderBy(x => x.Size).First(x => x.Size >= spaceToClean).Size;

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

                currentDir = changeToDir switch
                {
                    ".." => currentDir.Parent!,
                    "/" => currentDir.GetRoot(),
                    _ => currentDir.Directories.Single(x => x.Name == changeToDir),
                };
            }
            else if (line.StartsWith("$ ls"))
            {
                continue;
            }
            else if (line.StartsWith("dir "))
            {
                if (!currentDir.Directories.Any(x => x.Name == line[4..]))
                {
                    currentDir.Directories.Add(new SystemDirectory(line[4..], currentDir));
                }
            }
            else
            {
                var fileLine = line.Split(' ');
                var size = long.Parse(fileLine[0]);
                var name = fileLine[1];

                currentDir.AddFile(name, size);
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

    private readonly List<SystemFile> files = new();

    internal string Name { get; }
    internal long Size { get; set; }
    internal SystemDirectory? Parent { get; }
    internal List<SystemDirectory> Directories { get; set; } = new();
    internal IImmutableList<SystemFile> Files => files.ToImmutableList();

    internal SystemDirectory GetRoot()
    {
        var parent = Parent;

        while (parent != null)
        {
            parent = parent.Parent;
        }

        return parent ?? this;
    }

    internal void AddFile(string name, long size)
    {
        files.Add(new SystemFile(this, name, size));

        // Re-calculate size of folder + all parent folders
        var currentDirectory = this;
        while (currentDirectory != null)
        {
            currentDirectory.Size += size;
            currentDirectory = currentDirectory.Parent;
        }
    }
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

internal record struct SystemFile(SystemDirectory Directorystring, string Name, long Size);
