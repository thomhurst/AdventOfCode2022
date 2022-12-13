// See https://aka.ms/new-console-template for more information

namespace TomLonghurst.AdventOfCode._2022.Day3;

public class Program
{
    private static readonly List<char> Characters = new List<char>();

    static Program()
    {
        for (var letter = 'a'; letter <= 'z'; letter++)
        {
            Characters.Add(letter);
        }
        
        for (var letter = 'A'; letter <= 'Z'; letter++)
        {
            Characters.Add(letter);
        }
    }
    
    public static async Task Main(string[] args)
    {
        // Part 1
        var input = await File.ReadAllLinesAsync("input.txt");

        var prioritySum = input.Select(x => x.Chunk(x.Length / 2).ToArray())
            .Select(x => x[0].Intersect(x[1]))
            .Select(x => x.First())
            .Sum(c => Characters.IndexOf(c) + 1);
        
        Console.WriteLine($"The sum of priority items is: {prioritySum}");
        
        // Part 2
        
        var prioritySumOfGroupsOfThree = input
            .Chunk(3)
            .Select(x => x[0].Intersect(x[1]).Intersect(x[2]))
            .Select(x => x.First())
            .Sum(c => Characters.IndexOf(c) + 1);
        
        Console.WriteLine($"The sum of priority items is: {prioritySumOfGroupsOfThree}");
    }
}