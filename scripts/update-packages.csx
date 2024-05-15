#r "nuget: SharpZipLib, 1.4.2"

using System.Diagnostics;
using System.Text.Json;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Tar;

var basePath = Path.GetFullPath("../");
var packagePath = Path.GetFullPath("../packages");

Directory.Delete(packagePath, true);
Directory.CreateDirectory(packagePath);

var packages = new[] {
    "@esbuild/linux-x64",
    "@esbuild/linux-arm64",
    "@esbuild/darwin-arm64",
    "@esbuild/darwin-x64",
    "@esbuild/win32-x64",
};

foreach (var package in packages)
{
    Process.Start(new ProcessStartInfo
    {
        FileName = "npm",
        Arguments = $"pack {package}",
        WorkingDirectory = packagePath
    }).WaitForExit();
    var rid = package.Split('/')[1];
    var files = Directory.GetFiles(packagePath);
    var tgz = files.First(f => f.Contains(rid));
    using var inStream = File.OpenRead(tgz);
    using var gzipStream = new GZipInputStream(inStream);
    using var tarArchive = TarArchive.CreateInputTarArchive(gzipStream, Encoding.Default);
    tarArchive.ExtractContents(Path.Combine(packagePath, rid));
    // var packageJson = File.ReadAllText(Path.Combine(packagePath, rid, "package", "package.json"));
    // var version= JsonSerializer.Deserialize<JsonElement>(packageJson).GetProperty("version").GetString();
}