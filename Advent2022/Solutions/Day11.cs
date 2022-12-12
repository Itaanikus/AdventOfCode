using System.Linq.Expressions;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Advent2022.Solutions;

internal static class Day11
{
    private static readonly string InputPath = $"{Directory.GetCurrentDirectory()}\\Inputs\\Day11.txt";
    private static readonly string ExampleInputPath = $"{Directory.GetCurrentDirectory()}\\Inputs\\Day11_example.txt";
    private const int AmountOfRounds = 10000; // Use 20|10000 for Part 1|2
    private const int ReliefFactor = 1; // Use 3|1 for Part 1|2

    public static void GetTaskResults()
    {
        var useExample = false;
        var lines = File.ReadAllLines(useExample ? ExampleInputPath : InputPath);

        var monkeys = SetupMonkeysFromInput(lines);
        long commonMultiple = 1;
        monkeys.ForEach(x => commonMultiple *= x.Divisible);

        for (int round = 0; round < AmountOfRounds; round++)
        {
            foreach (var monkey in monkeys)
            {
                var initialItemCount = monkey.Items.Count;
                for (int i = 0; i < initialItemCount; i++)
                {
                    var dequeuedItemWorry = monkey.Items.Dequeue();
                    var newWorryLevel = monkey.Operate(dequeuedItemWorry);
                    newWorryLevel %= commonMultiple;

                    var relievedWorryLevel = newWorryLevel / ReliefFactor;
                    monkey.Throw(relievedWorryLevel, monkeys);
                }
            }
        }

        var twoBusiestMonkeys = monkeys.OrderByDescending(x => x.Business).Take(2).ToList();
        Console.WriteLine($"Task 1: {twoBusiestMonkeys[0].Business * twoBusiestMonkeys[1].Business}");
    }

    private static List<Monkey> SetupMonkeysFromInput(string[] lines)
    {
        var monkeys = new List<Monkey>();
        for (int i = 0; i < lines.Length; i += 7)
        {
            var startingItems = lines[i + 1].Split(':')[1].Trim().Split(", ");
            var operation = GetOperationFromInput(lines[i + 2]);
            var divisibleValue = int.Parse(lines[i + 3].Split(' ').Last());
            var monkeyPartner1 = int.Parse(lines[i + 4].Split(' ').Last());
            var monkeyPartner2 = int.Parse(lines[i + 5].Split(' ').Last());

            var monkey = new Monkey(divisibleValue, (monkeyPartner1, monkeyPartner2), operation);

            foreach (string item in startingItems)
            {
                monkey.Items.Enqueue(int.Parse(item));
            }

            monkeys.Add(monkey);
        }
        return monkeys;
    }

    private static Func<long, long> GetOperationFromInput(string inputLine)
    {
        var operationInput = inputLine.Split(':')[1].Trim().Split("= ")[1].Split(' ');
        var hasVal1 = int.TryParse(operationInput[0], out var val1);
        var hasVal2 = int.TryParse(operationInput[2], out var val2);

        return operationInput[1] switch
        {
            "+" => (old) => (hasVal1 ? val1 : old) + (hasVal2 ? val2 : old),
            "-" => (old) => (hasVal1 ? val1 : old) - (hasVal2 ? val2 : old),
            "*" => (old) => (hasVal1 ? val1 : old) * (hasVal2 ? val2 : old),
            "/" => (old) => (hasVal1 ? val1 : old) / (hasVal2 ? val2 : old),
            _ => throw new NotSupportedException(),
        };
    }
}

internal class Monkey
{
    public Monkey(int divisible, (int, int) monkeyPartners, Func<long, long> operation)
    {
        Divisible = divisible;
        Partners = monkeyPartners;
        Operation = operation;
    }

    public Queue<long> Items { get; set; } = new();

    public Func<long, long> Operation { get; }

    public int Divisible { get; }

    public long Business { get; set; }

    public (int, int) Partners { get; }

    internal void Throw(long itemWorryLevel, List<Monkey> monkeys)
    {
        var monkeyIndex = itemWorryLevel % Divisible == 0 ? Partners.Item1 : Partners.Item2;
        monkeys[monkeyIndex].Items.Enqueue(itemWorryLevel);
    }
}

internal static class MonkeyExtensions
{
    internal static long Operate(this Monkey monkey, long worry)
    {
        monkey.Business++;
        return Operate(monkey.Operation, worry);
    }

    private static long Operate(Func<long, long> function, long worry) => function(worry);
}