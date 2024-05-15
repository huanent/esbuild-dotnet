using System.Runtime.InteropServices;

namespace Esbuild;

public class TransformException(string? message) : Exception(message)
{
}

public class ExecutableNotFoundException() : Exception($"Can not load esbuild executable file, please ensure nuget package 'Esbuild.Native.{RuntimeInformation.RuntimeIdentifier}' is installed")
{
}