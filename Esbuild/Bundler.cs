using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Esbuild;

public static class Bundler
{
    private static readonly string bin;
    private static readonly string binFileName;
    private static readonly Dictionary<string, string> ridMapping = new()
    {
        {"win-x64", "win-x64"},
        {"linux-x64", "linux-x64"},
        {"linux-musl-x64", "linux-x64"},
        {"linux-arm64", "linux-arm64"},
        {"linux-musl-arm64", "linux-arm64"},
        {"osx-x64", "osx-x64"},
        {"osx-arm64", "osx-arm64"},
    };

    static Bundler()
    {
        var rid = ridMapping[RuntimeInformation.RuntimeIdentifier];
        binFileName = rid.StartsWith("win-") ? "esbuild.exe" : "esbuild";
        bin = Path.Combine(AppContext.BaseDirectory, binFileName);
        if (File.Exists(bin)) return;
        bin = Path.Combine(AppContext.BaseDirectory, "runtimes", rid, "native", binFileName);
        if (!File.Exists(bin))
        {
            throw new ExecutableNotFoundException();
        }
    }

    public static async Task<string> TransformAsync(string code, TransformOptions? options = null, CancellationToken token = default)
    {
        var arguments = options?.ToArguments();
        return await ProcessHelper.RunAsync(bin, arguments?.ToArray(), code, token);
    }

    public static string Transform(string code, TransformOptions? options = null)
    {
        var arguments = options?.ToArguments();
        return ProcessHelper.Run(bin, arguments?.ToArray(), code);
    }
}