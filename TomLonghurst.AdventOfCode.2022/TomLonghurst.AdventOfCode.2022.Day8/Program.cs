// See https://aka.ms/new-console-template for more information

public class Program
{
    public static async Task Main(string[] args)
    {
        var grid = await Parse();

        Part1(grid);
        Part2(grid);
    }

    private static void Part1(List<List<int>> grid)
    {
        var visibleTrees = 0;
        for (var rowIndex = 0; rowIndex < grid.Count; rowIndex++)
        {
            var row = grid[rowIndex];
            for (var columnIndex = 0; columnIndex < row.Count; columnIndex++)
            {
                var currentTree = row[columnIndex];

                var visibleFromLeft = !Enumerable.Range(0, columnIndex)
                    .Any(x => grid[rowIndex][x] >= currentTree);

                var visibleFromRight = !Enumerable.Range(columnIndex + 1, grid[rowIndex].Count - columnIndex - 1)
                    .Any(x => grid[rowIndex][x] >= currentTree);

                var visibleFromUp = !Enumerable.Range(0, rowIndex)
                    .Any(x => grid[x][columnIndex] >= currentTree);

                var visibleFromDown = !Enumerable.Range(rowIndex + 1, grid.Count - rowIndex - 1)
                    .Any(x => grid[x][columnIndex] >= currentTree);

                if (visibleFromLeft || visibleFromRight || visibleFromUp || visibleFromDown)
                {
                    visibleTrees++;
                }
            }
        }

        Console.WriteLine($"The amount of trees visible is: {visibleTrees}");
    }
    
    private static void Part2(List<List<int>> grid)
    {
        var highestScenicScore = 0;
        for (var rowIndex = 0; rowIndex < grid.Count; rowIndex++)
        {
            var row = grid[rowIndex];
            for (var columnIndex = 0; columnIndex < row.Count; columnIndex++)
            {
                var currentTree = row[columnIndex];

                var visibleFromLeft = Enumerable.Range(1, columnIndex)
                    .TakeWhile(x => grid[rowIndex][columnIndex - x] < currentTree)
                    .Count();

                var visibleFromRight = Enumerable.Range(1, row.Count - columnIndex - 1)
                    .TakeWhile(x => grid[rowIndex][columnIndex + x] < currentTree)
                    .Count();

                var visibleFromUp = Enumerable.Range(1, rowIndex)
                    .TakeWhile(x => grid[rowIndex - x][columnIndex] < currentTree)
                    .Count();

                var visibleFromDown = Enumerable.Range(1, grid.Count - rowIndex - 1)
                    .TakeWhile(x => grid[rowIndex + x][columnIndex] < currentTree)
                    .Count();

                var scenicScore = visibleFromLeft * visibleFromRight * visibleFromUp * visibleFromDown;
                
                if (scenicScore > highestScenicScore)
                {
                    highestScenicScore = scenicScore;
                }
            }
        }

        Console.WriteLine($"The highest scenic score is: {highestScenicScore}");
    }

    private static async Task<List<List<int>>> Parse()
    {
        var lines = await File.ReadAllLinesAsync("input.txt");

        return lines.Select(line => line.Select(c => (int)char.GetNumericValue(c)).ToList()).ToList();
    }
}