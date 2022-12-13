namespace TomLonghurst.AdventOfCode._2022.Day7;

public interface IFilesystemType
{
    string Name { get; }
    long Size { get; }
}