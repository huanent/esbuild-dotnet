public static class Common
{
    public static Dictionary<string, string> Packages => new Dictionary<string, string> {
        {"@esbuild/linux-x64","Esbuild.Native.linux-x64"},
        {"@esbuild/linux-arm64","Esbuild.Native.linux-arm64"},
        {"@esbuild/darwin-arm64","Esbuild.Native.osx-arm64"},
        {"@esbuild/darwin-x64","Esbuild.Native.osx-x64"},
        {"@esbuild/win32-x64","Esbuild.Native.win-x64"},
    };

    public static string BasePath => Path.GetFullPath("../");

    public static string PackagePath => Path.GetFullPath("../packages");

    public static void PublishPackage(string project, string key)
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
            Arguments = $"nuget push {nupkgPath} --api-key {key} --source https://api.nuget.org/v3/index.json",
            WorkingDirectory = projectPath
        }).WaitForExit();
    }
}