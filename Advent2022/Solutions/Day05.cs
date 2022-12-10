using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Advent2022.Solutions
{
    internal static class Day05
    {
        public static void GetTaskResults()
        {
            var input = File.ReadAllLines($"{Directory.GetCurrentDirectory()}\\Inputs\\Day5.txt");
            var stacks = new Dictionary<int, List<Crate>>();

            foreach (var line in input)
            {
                while (line.Contains('['))
                {
                    for (int i = 0; i < line.Length; i++)
                    {
                        var value = line[i];
                        var stackNumber = i < 4 ? i : i / 4 + 1;
                        if (i % 4 == 1 && value != ' ')
                        {
                            if (!stacks.ContainsKey(stackNumber))
                            {
                                stacks.Add(stackNumber, new List<Crate>());
                            }

                            stacks[stackNumber] = IncrementValues(stacks[stackNumber]);
                            stacks[stackNumber].Add(new Crate(value, 1));
                        }
                    }
                    break;
                }

                while (line.Contains("move"))
                {
                    var matches = Regex.Matches(line, @"\d+");

                    if (matches.Count != 3)
                    {
                        throw new NotSupportedException();
                    }

                    var moveAmount = int.Parse(matches[0].Value);
                    var moveFrom = int.Parse(matches[1].Value);
                    var moveTo = int.Parse(matches[2].Value);

                    // Part 1
                    //for (int i = 0; i < moveAmount; i++)
                    //{
                    //    var crateToMove = stacks[moveFrom].OrderByDescending(x => x.Order).First();
                    //    stacks[moveFrom].Remove(crateToMove);
                    //    stacks[moveTo].Add(crateToMove with
                    //    {
                    //        Order = stacks[moveTo].Any() ? stacks[moveTo].Max(x => x.Order) + 1 : 1
                    //    });
                    //}

                    // Part 2
                    var cratesToMove = stacks[moveFrom].OrderByDescending(x => x.Order).Take(moveAmount).ToList();

                    foreach (var crate in cratesToMove)
                    {
                        stacks[moveFrom].Remove(crate);
                    }

                    stacks[moveTo].AddRange(cratesToMove.OrderBy(x => x.Order).Select(crateToMove => crateToMove with
                    {
                        Order = stacks[moveTo].Any() ? stacks[moveTo].Max(x => x.Order) + 1 : 1
                    }));

                    break;
                }
            }

            //var task1Result = string.Concat(stacks.OrderBy(x => x.Key).Select(x => x.Value.OrderByDescending(v => v.Order).Select(x => x.Id).FirstOrDefault()));
            var task2Result = string.Concat(stacks.OrderBy(x => x.Key).Select(x => x.Value.OrderByDescending(v => v.Order).Select(x => x.Id).FirstOrDefault()));


            //Console.WriteLine($"Task 1 result is: {task1Result}");
            Console.WriteLine($"Task 2 result is: {task2Result}");
        }

        internal static List<Crate> IncrementValues(List<Crate> crates) =>
            crates.Select(x => x with { Order = x.Order + 1 }).ToList();
    }

    internal readonly record struct Crate(char Id, int Order)
    {
    }
}