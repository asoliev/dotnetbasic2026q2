namespace Module3.FSVisitorLib;

public class FileSystemVisitorEventArgs(string path) : EventArgs
{
    public string Path { get; } = path;
    public bool Abort { get; set; } = false;
    public bool Exclude { get; set; } = false;
}
