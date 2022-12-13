namespace TomLonghurst.AdventOfCode._2022.Day6;

public class Program
{
    public static async Task Main(string[] args)
    {
        // Part 2
        var input = await File.ReadAllTextAsync("input.txt");
        
        var indexOfStartOfPacket = GetIndexOfConsecutiveUniqueCharacters(input, 4);
        
        Console.WriteLine($"Index of Start of Packet is: {indexOfStartOfPacket}");
        
        // Part 2
        var indexOfStartOfMessage = GetIndexOfConsecutiveUniqueCharacters(input, 14);
        
        Console.WriteLine($"Index of Start of Message is: {indexOfStartOfMessage}");
    }

    private static int GetIndexOfConsecutiveUniqueCharacters(string input, int uniqueConsecutiveCharacterCount)
    {
        var circularQueue = new RollingCollection<char>(uniqueConsecutiveCharacterCount);

        for (var i = 0; i < input.Length; i++)
        {
            var c = input[i];
            circularQueue.Add(c);
            if (circularQueue.Distinct().Count() == uniqueConsecutiveCharacterCount)
            {
                return i + 1;
            }
        }

        return -1;
    }
}