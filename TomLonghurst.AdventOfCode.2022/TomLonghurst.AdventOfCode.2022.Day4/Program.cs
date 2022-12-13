// See https://aka.ms/new-console-template for more information

namespace TomLonghurst.AdventOfCode._2022.Day4;

public class Program
{
    public static async Task Main(string[] args)
    {
        // Part 1
        var input = await File.ReadAllLinesAsync("input.txt");

        var fullOverlaps = input
            .Select(x => x.Split(','))
            .Select(x => (GetRange(x[0]), GetRange(x[1])))
            .Count(x => OneFullyContainsAnother(x.Item1, x.Item2));
        
        Console.WriteLine($"The count of fully overlapping sections is: {fullOverlaps}");
        
        // Part 2
        var partialOverlaps = input
            .Select(x => x.Split(','))
            .Select(x => (GetRange(x[0]), GetRange(x[1])))
            .Count(x => IsPartialOverlap(x.Item1, x.Item2));
        
        Console.WriteLine($"The count of partial overlapping sections is: {partialOverlaps}");
    }

    private static bool IsPartialOverlap(Range range1, Range range2)
    {
        return range2.Start.Value > range1.Start.Value
            ? range2.Start.Value <= range1.End.Value
            : range1.Start.Value <= range2.End.Value;
    }

    private static bool OneFullyContainsAnother(Range range1, Range range2)
    {
        if (range1.Start.Value >= range2.Start.Value && range1.End.Value <= range2.End.Value)
        {
            return true;
        }
        
        if (range2.Start.Value >= range1.Start.Value && range2.End.Value <= range1.End.Value)
        {
            return true;
        }

        return false;
    }

    private static Range GetRange(string value)
    {
        var splitSection = value.Split('-');
        var first = int.Parse(splitSection[0]);
        var last = int.Parse(splitSection[1]);

        return first..last;
    }
}