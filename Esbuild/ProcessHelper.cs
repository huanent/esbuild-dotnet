using System.Diagnostics;

namespace Esbuild;
internal class ProcessHelper
{
    public static async Task<string> RunAsync(string bin, string[]? arguments = default, string? input = default, CancellationToken token = default)
    {
        using Process process = Run(bin, arguments);

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

        await process.WaitForExitAsync(token);
        if (process.ExitCode == 0) return result;
        using var standardError = process.StandardError;
        var error = await standardError.ReadToEndAsync(token);
        throw new EsbuildException(error);
    }

    public static string Run(string bin, string[]? arguments = default, string? input = default)
    {
        using Process process = Run(bin, arguments);

        if (input != default)
        {
            using (process.StandardInput)
            {
                process.StandardInput.Write(input);
            }
        }

        string result;

        using (process.StandardOutput)
        {
            result = process.StandardOutput.ReadToEnd();
        }

        process.WaitForExit();
        if (process.ExitCode == 0) return result;
        using var standardError = process.StandardError;
        var error = standardError.ReadToEnd();
        throw new EsbuildException(error);
    }

    private static Process Run(string bin, string[]? arguments)
    {
        var process = new Process();
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
        return process;
    }
}