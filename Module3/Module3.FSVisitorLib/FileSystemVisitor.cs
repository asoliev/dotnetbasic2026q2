using System.Collections;

namespace Module3.FSVisitorLib;

public class FileSystemVisitor : IEnumerable<string>
{
    private readonly string _rootPath;
    private readonly Func<string, bool> _filter;

    // Constructor without filter (returns all files and folders)
    public FileSystemVisitor(string rootPath) : this(rootPath, null) { }

    // Constructor with filter
    public FileSystemVisitor(string rootPath, Func<string, bool> filter)
    {
        if (string.IsNullOrEmpty(rootPath))
            throw new ArgumentException("Root path cannot be null or empty.", nameof(rootPath));
        if (!Directory.Exists(rootPath))
            throw new DirectoryNotFoundException($"Directory not found: {rootPath}");

        _rootPath = rootPath;
        _filter = filter;
    }

    // Custom iterator using yield
    public IEnumerator<string> GetEnumerator() => Traverse(_rootPath).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private IEnumerable<string> Traverse(string currentPath)
    {
        // Yield current directory if it matches filter (or if no filter)
        if (_filter == null || _filter(currentPath))
            yield return currentPath;

        // Yield files
        foreach (string file in Directory.GetFiles(currentPath))
        {
            if (_filter == null || _filter(file))
                yield return file;
        }

        // Recurse into subdirectories
        foreach (string dir in Directory.GetDirectories(currentPath))
        {
            foreach (string item in Traverse(dir))
            {
                yield return item;
            }
        }
    }
}
