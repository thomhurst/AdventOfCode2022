namespace TomLonghurst.AdventOfCode._2022.Day7;

public class File : IFilesystemType
{
    public string Name { get; }

    public long Size { get; }

    public File(string name, long size)
    {
        Name = name;
        Size = size;
    }
}