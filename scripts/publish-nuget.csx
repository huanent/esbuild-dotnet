#load "common.csx"
using static Common;

foreach (var package in Packages)
{
    try
    {
        PublishPackage("Esbuild");
        PublishPackage(package.Value);
    }
    catch (System.Exception)
    {
        Console.WriteLine($"publish {package.Key} error");
        throw;
    }
}

void PublishPackage(string project)
{
    var projectPath = Path.Combine(BasePath, project);
    var binPath = Path.Combine(projectPath, "bin");
    var objPath = Path.Combine(projectPath, "obj");
    if (Directory.Exists(binPath)) Directory.Delete(binPath, true);
    if (Directory.Exists(objPath)) Directory.Delete(objPath, true);
    Process.Start(new ProcessStartInfo
    {
        FileName = "dotnet",
        Arguments = $"pack",
        WorkingDirectory = projectPath
    }).WaitForExit();
    var nupkgPath = Directory.GetFiles(projectPath, "*.nupkg", SearchOption.AllDirectories).First();

    Process.Start(new ProcessStartInfo
    {
        FileName = "dotnet",
        Arguments = $"nuget push {nupkgPath} --api-key {Args.First()} --source https://api.nuget.org/v3/index.json",
        WorkingDirectory = projectPath
    }).WaitForExit();
}