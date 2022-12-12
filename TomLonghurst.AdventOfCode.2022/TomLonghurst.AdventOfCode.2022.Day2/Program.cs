// See https://aka.ms/new-console-template for more information

namespace TomLonghurst.AdventOfCode._2022.Day2;

public class Program
{
    public static readonly LinkedList<GameMove> Moves = new();

    static Program()
    {
        Moves.AddFirst(GameMove.Rock);
        Moves.AddLast(GameMove.Paper);
        Moves.AddLast(GameMove.Scissors);
    }
    
    public static async Task Main(string[] args)
    {
        // Part 1
        var input = await File.ReadAllLinesAsync("input.txt");

        var totalScore = input
            .Select(x => (GetMove(x[0]), GetMove(x[2])))
            .Select(x => GetScore(x.Item1, x.Item2))
            .Sum();
        
        Console.WriteLine($"Your total score is: {totalScore}");
        
        // Part 2
        var totalScoreWithRealRules = input
            .Select(x => (GetMove(x[0]), GetResult(x[2])))
            .Select(x => (x.Item1, GetMove(x.Item1, x.Item2)))
            .Select(x => GetScore(x.Item1, x.Item2))
            .Sum();
        
        Console.WriteLine($"Your real total score is: {totalScoreWithRealRules}");
    }


    static GameMove GetMove(char c)
    {
        return c switch
        {
            'A' or 'X' => GameMove.Rock,
            'B' or 'Y' => GameMove.Paper,
            _ => GameMove.Scissors
        };
    }

    static int GetScore(GameMove opponentsMove, GameMove yourMove)
    {
        if (opponentsMove == yourMove)
        {
            return (int)(yourMove + (int)GameResult.Draw);
        }

        if (yourMove == GetMove(opponentsMove, GameResult.Win))
        {
            return (int)(yourMove + (int)GameResult.Win);
        }

        return (int)yourMove;
    }

    static GameMove GetMove(GameMove opponentsMove, GameResult gameResult)
    {
        if (gameResult == GameResult.Draw)
        {
            return opponentsMove;
        }

        return gameResult == GameResult.Win
            ? Moves.Find(opponentsMove)!.Next?.Value ?? Moves.First!.Value
            : Moves.Find(opponentsMove)!.Previous?.Value ?? Moves.Last!.Value;
    }
    
    public enum GameResult
    {
        Lose = 0,
        Draw = 3,
        Win = 6
    }

    private static GameResult GetResult(char c)
    {
        return c switch
        {
            'X' => GameResult.Lose,
            'Y' => GameResult.Draw,
            _ => GameResult.Win
        };
    }
}