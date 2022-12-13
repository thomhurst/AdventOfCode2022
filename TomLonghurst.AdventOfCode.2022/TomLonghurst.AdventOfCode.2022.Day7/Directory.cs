namespace TomLonghurst.AdventOfCode._2022.Day7;

public class Directory : IFilesystemType
{
    public static readonly Directory Root = new("/", null!);
    public Directory(string name, Directory parent)
    {
        Name = name;
        Parent = parent;
    }

    public IEnumerable<Directory> DescendantDirectories
    {
        get
        {
            var childDirectories = Children.OfType<Directory>().ToArray();
            var descendantDirectories = childDirectories.SelectMany(c => c.DescendantDirectories);
            return childDirectories.Concat(descendantDirectories);
        }
    }

    public Directory FindDirectory(string identifier)
    {
        if (identifier == "/")
        {
            return Root;
        }

        if (identifier == "..")
        {
            return Parent;
        }

        return Children.OfType<Directory>().First(x => x.Name == identifier);
    }

    public string Name { get; }

    public Directory Parent { get; }
    private List<IFilesystemType> Children { get; } = new();
    public long Size => Children.Sum(c => c.Size);

    public void AddDirectory(string name)
    {
        var directory = new Directory(name, this);
        Children.Add(directory);
    }

    public void AddFile(string name, long size)
    {
        Children.Add(new File(name, size));
    }
}