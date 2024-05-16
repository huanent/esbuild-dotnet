#r "nuget: SharpZipLib, 1.4.2"

using System.Diagnostics;
using System.Text.Json;
using System.Text.RegularExpressions;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Tar;

var basePath = Path.GetFullPath("../");
var packagePath = Path.GetFullPath("../packages");

Directory.Delete(packagePath, true);
Directory.CreateDirectory(packagePath);

var packages = new Dictionary<string, string> {
    {"@esbuild/linux-x64","Esbuild.Native.linux-x64"},
    {"@esbuild/linux-arm64","Esbuild.Native.linux-arm64"},
    {"@esbuild/darwin-arm64","Esbuild.Native.osx-arm64"},
    {"@esbuild/darwin-x64","Esbuild.Native.osx-x64"},
    {"@esbuild/win32-x64","Esbuild.Native.win-x64"},
};

foreach (var package in packages)
{
    Process.Start(new ProcessStartInfo
    {
        FileName = "npm",
        Arguments = $"pack {package.Key}",
        WorkingDirectory = packagePath
    }).WaitForExit();
    var rid = package.Key.Split('/')[1];
    var files = Directory.GetFiles(packagePath);
    var tgz = files.First(f => f.Contains(rid));
    using var inStream = File.OpenRead(tgz);
    using var gzipStream = new GZipInputStream(inStream);
    using var tarArchive = TarArchive.CreateInputTarArchive(gzipStream, Encoding.Default);
    tarArchive.ExtractContents(Path.Combine(packagePath, rid));
    var packageJson = File.ReadAllText(Path.Combine(packagePath, rid, "package", "package.json"));
    var version = JsonSerializer.Deserialize<JsonElement>(packageJson).GetProperty("version").GetString();
    var csprojPath = Path.Combine(basePath, package.Value, $"{package.Value}.csproj");
    var csproj = File.ReadAllText(csprojPath);
    csproj = Regex.Replace(csproj, "<Version>.*</Version>", $"<Version>{version}</Version>");
    File.WriteAllText(csprojPath, csproj);
}