using System.Collections;

namespace Module3.FSVisitorLib;

public class FileSystemVisitor : IEnumerable<string>
{
    #region fields and properties

    private bool _abort;
    private readonly string _rootPath;
    private readonly Func<string, bool>? _filter;

    public int AllFilesFoundCount { get; private set; }
    public int AllDirectoriesFoundCount { get; private set; }

    // Events
    public event EventHandler<EventArgs>? SearchStarted;
    public event EventHandler<EventArgs>? SearchFinished;
    public event EventHandler<FileSystemVisitorEventArgs>? FileFound;
    public event EventHandler<FileSystemVisitorEventArgs>? DirectoryFound;
    public event EventHandler<FileSystemVisitorEventArgs>? FilteredFileFound;
    public event EventHandler<FileSystemVisitorEventArgs>? FilteredDirectoryFound;

    #endregion

    // Constructor with filter
    public FileSystemVisitor(string rootPath, Func<string, bool>? filter = null)
    {
        if (string.IsNullOrEmpty(rootPath))
            throw new ArgumentException("Root path cannot be null or empty.", nameof(rootPath));
        if (!Directory.Exists(rootPath))
            throw new DirectoryNotFoundException($"Directory not found: {rootPath}");

        _rootPath = rootPath;
        _filter = filter;
    }

    // Custom iterator using yield
    public IEnumerator<string> GetEnumerator()
    {
        _abort = false;
        AllFilesFoundCount = 0;
        AllDirectoriesFoundCount = 0;

        SearchStarted?.Invoke(this, EventArgs.Empty);

        foreach (string item in Traverse(_rootPath))
        {
            if (_abort) break;
            yield return item;
        }

        SearchFinished?.Invoke(this, EventArgs.Empty);
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private IEnumerable<string> Traverse(string currentPath)
    {
        foreach (string dirResult in ProcessDirectory(currentPath))
            yield return dirResult;

        foreach (string fileResult in ProcessFiles(currentPath))
            yield return fileResult;

        foreach (string subDir in Directory.GetDirectories(currentPath))
        {
            if (_abort) yield break;
            foreach (string item in Traverse(subDir))
            {
                if (_abort) yield break;
                yield return item;
            }
        }
    }

    private IEnumerable<string> ProcessDirectory(string dirPath)
    {
        AllDirectoriesFoundCount++;
        if (ShouldContinue(DirectoryFound, dirPath, out bool dirExclude)) yield break;

        if (dirExclude || (_filter != null && !_filter(dirPath))) yield break;
        if (ShouldContinue(FilteredDirectoryFound, dirPath, out bool filteredDirExclude)) yield break;
        if (!filteredDirExclude) yield return dirPath;
    }

    private IEnumerable<string> ProcessFiles(string dirPath)
    {
        foreach (string file in Directory.GetFiles(dirPath))
        {
            AllFilesFoundCount++;
            if (!ShouldContinue(FileFound, file, out bool fileExclude)) yield break;

            if (fileExclude || (_filter != null && !_filter(file))) continue;
            if (!ShouldContinue(FilteredFileFound, file, out bool filteredFileExclude)) yield break;
            if (!filteredFileExclude)
                yield return file;
        }
    }

    private bool ShouldContinue(EventHandler<FileSystemVisitorEventArgs>? handler, string path, out bool exclude)
    {
        exclude = false;
        if (handler == null) return true;

        var args = new FileSystemVisitorEventArgs(path);
        handler(this, args);
        if (args.Abort)
        {
            _abort = true;
            return false;
        }
        exclude = args.Exclude;
        return true;
    }
}
