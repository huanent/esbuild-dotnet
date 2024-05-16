#r "nuget: SharpZipLib, 1.4.2"
#load "common.csx"

using System.Diagnostics;
using System.Text.Json;
using System.Text.RegularExpressions;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Tar;
using static Common;


Directory.Delete(PackagePath, true);
Directory.CreateDirectory(PackagePath);

foreach (var package in Packages)
{
    Process.Start(new ProcessStartInfo
    {
        FileName = "npm",
        Arguments = $"pack {package.Key}",
        WorkingDirectory = PackagePath
    }).WaitForExit();
    var rid = package.Key.Split('/')[1];
    var files = Directory.GetFiles(PackagePath);
    var tgz = files.First(f => f.Contains(rid));
    using var inStream = File.OpenRead(tgz);
    using var gzipStream = new GZipInputStream(inStream);
    using var tarArchive = TarArchive.CreateInputTarArchive(gzipStream, Encoding.Default);
    tarArchive.ExtractContents(Path.Combine(PackagePath, rid));
    var packageJson = File.ReadAllText(Path.Combine(PackagePath, rid, "package", "package.json"));
    var version = JsonSerializer.Deserialize<JsonElement>(packageJson).GetProperty("version").GetString();
    var csprojPath = Path.Combine(BasePath, package.Value, $"{package.Value}.csproj");
    var csproj = File.ReadAllText(csprojPath);
    csproj = Regex.Replace(csproj, "<Version>.*</Version>", $"<Version>{version}</Version>");
    File.WriteAllText(csprojPath, csproj);
}