using System.Text.RegularExpressions;

namespace TomLonghurst.AdventOfCode._2022.Day5;

public class Program
{
    public static async Task Main(string[] args)
    {
        await Part1();
        await Part2();
    }

    private static async Task Part1()
    {
        var input = await ParseInput();

        foreach (var instruction in input.Instructions)
        {
            var numbers = Regex.Split(instruction, "\\s")
                .Where(s => int.TryParse(s, out _))
                .Select(int.Parse)
                .ToArray();

            var amountToMove = numbers[0];
            var stackToMoveFrom = numbers[1] - 1;
            var stackToMoveTo = numbers[2] - 1;

            for (var i = 0; i < amountToMove; i++)
            {
                var stackToMove = input.CrateStacks[stackToMoveFrom].Pop();
                input.CrateStacks[stackToMoveTo].Push(stackToMove);
            }
        }

        var cratesAtTheTopOfEachStack = new string(input.CrateStacks.Select(s => s.Peek()).ToArray());

        Console.WriteLine($"The creates at the top of each stack are: {cratesAtTheTopOfEachStack}");
    }
    
    private static async Task Part2()
    {
        var input = await ParseInput();

        foreach (var instruction in input.Instructions)
        {
            var numbers = Regex.Split(instruction, "\\s")
                .Where(s => int.TryParse(s, out _))
                .Select(int.Parse)
                .ToArray();

            var amountToMove = numbers[0];
            var stackToMoveFrom = numbers[1] - 1;
            var stackToMoveTo = numbers[2] - 1;

            var cratesToMove = Enumerable.Range(0, amountToMove)
                .Select(_ => input.CrateStacks[stackToMoveFrom].Pop())
                .Reverse();
            
            foreach (var crateToMove in cratesToMove)
            {
                input.CrateStacks[stackToMoveTo].Push(crateToMove);
            }
        }

        var cratesAtTheTopOfEachStack = new string(input.CrateStacks.Select(s => s.Peek()).ToArray());

        Console.WriteLine($"The creates at the top of each stack are: {cratesAtTheTopOfEachStack}");
    }

    private static async Task<PuzzleInput> ParseInput()
    {
        var lines = await File.ReadAllLinesAsync("input.txt");

        var crates = lines.TakeWhile(x => !string.IsNullOrWhiteSpace(x)).ToArray();
        
        var cratesStack = CreateStacks(crates);

        var instructions = lines.SkipWhile(x => !string.IsNullOrWhiteSpace(x))
            .Skip(1)
            .ToArray();

        return new PuzzleInput
        {
            CrateStacks = cratesStack,
            Instructions = instructions
        };
    }

    private static Stack<char>[] CreateStacks(string[] allCrates)
    {
        var numberOfStacks = allCrates
            .Last()
            .Where(char.IsNumber)
            .Select(c => (int)char.GetNumericValue(c))
            .Max();
        
        var stacks = new Stack<char>[numberOfStacks];

        var cratesFromBottomCrateFirst = allCrates.Reverse().Skip(1).ToArray();
        
        for (var stackIndex = 0; stackIndex < numberOfStacks; stackIndex++)
        {
            var thisStack = stacks[stackIndex] = new Stack<char>();
            
            foreach (var crateRow in cratesFromBottomCrateFirst)
            {
                var indexOfCrate = ((stackIndex + 1) * 4) - 3;
                var crateToAddToStack = crateRow[indexOfCrate];
                if (char.IsLetter(crateToAddToStack))
                {
                    thisStack.Push(crateToAddToStack);
                }
            }
        }

        return stacks;
    }
}

public class PuzzleInput
{
    public Stack<char>[] CrateStacks { get; set; }
    public string[] Instructions { get; set; }
}