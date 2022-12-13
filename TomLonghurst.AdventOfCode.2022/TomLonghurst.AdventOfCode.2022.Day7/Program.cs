// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;
using TomLonghurst.AdventOfCode._2022.Day7;
using Directory = TomLonghurst.AdventOfCode._2022.Day7.Directory;
using File = System.IO.File;

public class Program
{
    public static async Task Main(string[] args)
    {
        var input = new Queue<string>(await File.ReadAllLinesAsync("input.txt"));

        var currentDirectory = Directory.Root;
        
        while (input.Count != 0)
        {
            Execute(ref currentDirectory, input.Dequeue());
        }

        var allDirectories = Directory.Root.DescendantDirectories.ToArray();

        var directoriesUnder100k = allDirectories.Where(x => x.Size <= 100000);

        var sizeOfDirectoriesUnder100kEach = directoriesUnder100k.Sum(x => x.Size);

        Console.WriteLine($"The total size of all directories under 100k each is: {sizeOfDirectoriesUnder100kEach}");

        // Part 2
        var totalSpace = 70000000;
        var neededSpace = 30000000;
        var freeSpace = totalSpace - Directory.Root.Size;

        var smallestDirectoryToDelete = allDirectories.Where(d => freeSpace + d.Size > neededSpace)
            .MinBy(d => d.Size);
        
        Console.WriteLine($"The smallest directory you can delete to do the update is: {smallestDirectoryToDelete.Name} at size {smallestDirectoryToDelete.Size}");
    }

    private static void Execute(ref Directory currentDirectory, string output)
    {
        var (outputType, remainingOutput) = ParseOutputType(output);

        switch (outputType)
        {
            case OutputType.Directory:
                AddDirectory(currentDirectory, remainingOutput);
                break;
            case OutputType.File:
                AddFile(currentDirectory, remainingOutput);
                break;
            case OutputType.Command:
                ParseCommand(ref currentDirectory, remainingOutput);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private static void ParseCommand(ref Directory currentDirectory, string output)
    {
        var (commandType, remainingOutput) = ParseCommandType(output);

        switch (commandType)
        {
            case Command.ChangeDirectory:
                currentDirectory = currentDirectory.FindDirectory(remainingOutput);
                break;
            case Command.List:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private static (Command ChangeDirectory, string remainingString) ParseCommandType(string output)
    {
        if (TryGetType(output, "cd", out var remainingString))
        {
            return (Command.ChangeDirectory, remainingString);
        }
        
        if (output.StartsWith("ls"))
        {
            return (Command.List, string.Empty);
        }

        throw new ArgumentException("Unknown command");
    }

    private static void AddFile(Directory currentDirectory, string output)
    {
        var split = Regex.Split(output, "\\s");
        currentDirectory.AddFile(split[1], long.Parse(split[0]));
    }

    private static void AddDirectory(Directory currentDirectory, string directoryName)
    {
        currentDirectory.AddDirectory(directoryName);
    }

    private static (OutputType outputType, string remainingCommand) ParseOutputType(string output)
    {
        if (TryGetType(output, "$", out var remainingString))
        {
            return (OutputType.Command, remainingString);
        }

        if (TryGetType(output, "dir", out remainingString))
        {
            return (OutputType.Directory, remainingString);
        }
        
        return (OutputType.File, output);
    }

    private static bool TryGetType(string input, string splitOn, out string remainingString)
    {
        if (input.StartsWith(splitOn))
        {
            remainingString = input.Split($"{splitOn} ")[1];
            return true;
        }

        remainingString = null;
        return false;
    }
}

public enum Command
{
    ChangeDirectory,
    List
}