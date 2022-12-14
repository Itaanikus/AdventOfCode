using System.Text.Json;

namespace Advent2022.Solutions;

internal static class Day13
{
    private static readonly string InputPath = $"{Directory.GetCurrentDirectory()}\\Inputs\\Day13.txt";
    private static readonly string ExampleInputPath = $"{Directory.GetCurrentDirectory()}\\Inputs\\Day13ex.txt";

    public static void GetTaskResults()
    {
        var useExample = false;
        var lines = File.ReadAllLines(useExample ? ExampleInputPath : InputPath);

        var pairs = new List<bool>();
        var partTwoPairs = new List<JsonElement>()
        {
            JsonSerializer.Deserialize<JsonElement>("[[2]]"),
            JsonSerializer.Deserialize<JsonElement>("[[6]]"),
        };

        for (int i = 0; i < lines.Length; i += 3)
        {
            var packet1 = JsonSerializer.Deserialize<JsonElement>(lines[i]);
            var packet2 = JsonSerializer.Deserialize<JsonElement>(lines[i + 1]);

            pairs.Add(packet1.ShouldBeBefore(packet2));

            partTwoPairs.Add((packet1));
            partTwoPairs.Add((packet2));
        }

        partTwoPairs.Sort((first, second) => first.PacketComparer(second));

        var sum = 0;
        for (int i = 0; i < pairs.Count; i++)
        {
            var pairNumber = i + 1;
            sum += pairs[i] ? pairNumber : 0;
        }

        var indices = (0, 0);
        for (int i = 0; i < partTwoPairs.Count; i++)
        {
            if (partTwoPairs[i].ToString() == "[[2]]")
            {
                indices.Item1 = i + 1;
            }
            else if (partTwoPairs[i].ToString() == "[[6]]")
            {
                indices.Item2 = i + 1;
            }
        }


        Console.WriteLine(sum);
        Console.WriteLine(indices.Item1 * indices.Item2);
    }

    private static int PacketComparer(this JsonElement leftPacket, JsonElement rightPacket) =>
        leftPacket.ShouldBeBefore(rightPacket) ? -1 : 1;

    private static bool ShouldBeBefore(this JsonElement leftPacket, JsonElement rightPacket) => (leftPacket.ValueKind, rightPacket.ValueKind) switch
    {
        (JsonValueKind.Undefined, not JsonValueKind.Undefined) => false,
        (not JsonValueKind.Undefined, JsonValueKind.Undefined) => true,
        (JsonValueKind.Number, JsonValueKind.Number) => leftPacket.GetInt32() < rightPacket.GetInt32(),
        (JsonValueKind.Number, JsonValueKind.Array) => JsonSerializer.Deserialize<JsonElement>($"[{leftPacket.GetInt32()}]").ShouldBeBefore(rightPacket),
        (JsonValueKind.Array, JsonValueKind.Number) => leftPacket.ShouldBeBefore(JsonSerializer.Deserialize<JsonElement>($"[{rightPacket.GetInt32()}]")),
        (JsonValueKind.Array, JsonValueKind.Array) => CompareArrays(leftPacket, rightPacket),
        _ => throw new NotImplementedException()
    };

    private static bool CompareArrays(JsonElement leftPacket, JsonElement rightPacket)
    {
        var leftArray = leftPacket.EnumerateArray();
        var rightArray = rightPacket.EnumerateArray();

        while (leftArray.MoveNext() && rightArray.MoveNext())
        {
            var leftElement = leftArray.Current;
            var rightElement = rightArray.Current;

            if (leftElement.ValueKind == JsonValueKind.Number && rightElement.ValueKind == JsonValueKind.Number
                && leftElement.GetInt32() == rightElement.GetInt32())
            {
                continue;
            }

            if (leftElement.ValueKind == JsonValueKind.Undefined && rightElement.ValueKind == JsonValueKind.Undefined)
            {
                continue;
            }

            if (leftElement.ValueKind == JsonValueKind.Array && rightElement.ValueKind == JsonValueKind.Array)
            {
                var value = CompareArraysInternal(leftElement, rightElement);
                if (value == null)
                {
                    continue;
                }
            }

            return leftArray.Current.ShouldBeBefore(rightArray.Current);
        }

        return leftArray.Count() < rightArray.Count();
    }

    private static bool? CompareArraysInternal(JsonElement leftPacket, JsonElement rightPacket)
    {
        var leftArray = leftPacket.EnumerateArray();
        var rightArray = rightPacket.EnumerateArray();

        while (leftArray.MoveNext() && rightArray.MoveNext())
        {
            var leftElement = leftArray.Current;
            var rightElement = rightArray.Current;

            if (leftElement.ValueKind == JsonValueKind.Number && rightElement.ValueKind == JsonValueKind.Number
                && leftElement.GetInt32() == rightElement.GetInt32())
            {
                continue;
            }

            if (leftElement.ValueKind == JsonValueKind.Undefined && rightElement.ValueKind == JsonValueKind.Undefined)
            {
                continue;
            }

            return leftArray.Current.ShouldBeBefore(rightArray.Current);
        }

        var leftArrayCount = leftArray.Count();
        var rightArrayCount = rightArray.Count();
        if (leftArrayCount == rightArrayCount)
        {
            return null;
        }

        return leftArrayCount < rightArrayCount;
    }
}

