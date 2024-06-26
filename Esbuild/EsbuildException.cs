using System.Runtime.InteropServices;

namespace Esbuild;

public class EsbuildException(string? message) : Exception(message)
{
}

public class ExecutableNotFoundException(string rid) : EsbuildException($"Can not load esbuild executable file, please ensure nuget package 'Esbuild.Native.{rid}' is installed")
{
}