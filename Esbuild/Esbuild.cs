using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Esbuild;

public class Esbuild
{
    public static async Task<string> TransformAsync(string code, TransformOptions? options = null, CancellationToken token = default)
    {
        var path = Path.Combine(AppContext.BaseDirectory, "runtimes", RuntimeInformation.RuntimeIdentifier, "native", "esbuild");
        var process = new Process();
        process.StartInfo.FileName = path;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardInput = true;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.ArgumentList.Add("--loader=ts");
        process.Start();
        await process.StandardInput.WriteAsync(code);
        process.StandardInput.Close();
        return await process.StandardOutput.ReadToEndAsync(token);
    }
}