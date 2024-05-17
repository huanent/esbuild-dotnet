#load "common.csx"
using static Common;

var key = Args.FirstOrDefault(f => !string.IsNullOrWhiteSpace(f));
foreach (var package in Packages.Values.ToList())
{
    try
    {
        PublishPackage(package, key);
    }
    catch (Exception)
    {
        WriteLine($"publish {package} error");
        throw;
    }
}