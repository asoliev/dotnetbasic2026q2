using Module3.FSVisitorLib;

string userHome = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
string userDocuments = Path.Combine(userHome, "Documents");

var visitor = new FileSystemVisitor(userDocuments, path => path.EndsWith(".txt"));

visitor.SearchStarted += (s, e) => Console.WriteLine("Search started!");
visitor.SearchFinished += (s, e) => Console.WriteLine("Search finished!");
visitor.FileFound += (s, e) => Console.WriteLine($"File found: {e.Path}");
visitor.DirectoryFound += (s, e) => Console.WriteLine($"Directory found: {e.Path}");
visitor.FilteredFileFound += (s, e) =>
{
    Console.WriteLine($"Filtered file: {e.Path}");
    // Example: abort if a certain file is found
    if (e.Path.Contains("AutoHubAppRealizationPlan.txt"))
        e.Abort = true;
};
visitor.FilteredDirectoryFound += (s, e) => Console.WriteLine($"Filtered directory: {e.Path}");

foreach (string item in visitor)
    Console.WriteLine($"Result: {item}");

Console.WriteLine($"All files found: {visitor.AllFilesFoundCount}");
Console.WriteLine($"All directories found: {visitor.AllDirectoriesFoundCount}");
