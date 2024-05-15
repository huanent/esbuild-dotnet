using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Esbuild;

public static class Bundler
{
    private static readonly string bin;
    private static readonly string binFileName = "esbuild";
    private static readonly string rid = RuntimeInformation.RuntimeIdentifier;
    static Bundler()
    {
        bin = Path.Combine(AppContext.BaseDirectory, binFileName);
        if (File.Exists(bin)) return;
        bin = Path.Combine(AppContext.BaseDirectory, "runtimes", rid, "native", "esbuild");
        if (!File.Exists(bin))
        {
            throw new ExecutableNotFoundException();
        }
    }

    public static async Task<string> TransformAsync(string code, TransformOptions? options = null, CancellationToken token = default)
    {
        using var process = new Process();
        process.StartInfo.FileName = bin;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardInput = true;
        process.StartInfo.RedirectStandardError = true;

        if (options != default)
        {
            var arguments = options.ToArguments();
            foreach (var item in arguments)
            {
                process.StartInfo.ArgumentList.Add(item);
            }
        }

        process.Start();

        using (process.StandardInput)
        {
            await process.StandardInput.WriteAsync(code);
        }

        var result = await process.StandardOutput.ReadToEndAsync(token);
        process.StandardOutput.Close();
        if (string.IsNullOrWhiteSpace(result) && process.ExitCode != 0)
        {
            var error = await process.StandardError.ReadToEndAsync();
            process.StandardError.Close();
            throw new TransformException(error);
        }
        return result;
    }
}