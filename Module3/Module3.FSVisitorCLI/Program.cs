using Module3.FSVisitorLib;

var visitor = new FileSystemVisitor(
    @"/Users/Abdukodir_Soliev/Downloads",
    path => path.EndsWith(".txt") || path.Contains("github")
);

foreach (string item in visitor)
{
    Console.WriteLine(item);
}
