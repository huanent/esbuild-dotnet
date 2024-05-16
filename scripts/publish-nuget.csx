#load "common.csx"
using static Common;

var packages = Packages.Values.ToList();
packages.Add("Esbuild");

foreach (var package in packages)
{
    try
    {
        PublishPackage(package);
    }
    catch (Exception)
    {
        WriteLine($"publish {package} error");
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