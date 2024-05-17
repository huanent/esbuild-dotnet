using System.Diagnostics;

namespace Esbuild;
internal class ProcessHelper
{
    public static async Task<string> RunAsync(string bin, string[]? arguments = default, string? input = default, CancellationToken token = default)
    {
        using var process = new Process();
        process.StartInfo.FileName = bin;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardInput = true;
        process.StartInfo.RedirectStandardError = true;

        if (arguments != default)
        {
            foreach (var item in arguments)
            {
                process.StartInfo.ArgumentList.Add(item);
            }
        }

        process.Start();

        if (input != default)
        {
            using (process.StandardInput)
            {
                await process.StandardInput.WriteAsync(input);
            }
        }

        string result;

        using (process.StandardOutput)
        {
            result = await process.StandardOutput.ReadToEndAsync(token);
        }
        
        if (process.ExitCode == 0) return result;
        using var standardError = process.StandardError;
        var error = await standardError.ReadToEndAsync(token);
        throw new EsbuildException(error);
    }
}