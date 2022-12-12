// See https://aka.ms/new-console-template for more information

// Part 1

var input = await File.ReadAllLinesAsync("input.txt");

var dictionary = new Dictionary<int, int>();

var elfNumber = 0;
var totalCalories = 0;

foreach (var caloriesOrEmpty in input)
{
    if (int.TryParse(caloriesOrEmpty, out var calories))
    {
        totalCalories += calories;
    }
    else
    {
        dictionary.Add(++elfNumber, totalCalories);
        totalCalories = 0;
    }
}

dictionary.Add(++elfNumber, totalCalories);

var mostCalories = dictionary.Max(x => x.Value);

Console.WriteLine($"The most calories held by an elf is: {mostCalories}");

// Part 2

var sumOfThreeMostHighestCalories = dictionary
    .Select(x => x.Value)
    .OrderByDescending(x => x)
    .Take(3)
    .Sum();
    
Console.WriteLine($"The calories held by the top three elves is: {sumOfThreeMostHighestCalories}");