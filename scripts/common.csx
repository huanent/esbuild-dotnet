public static class Common
{
    public static Dictionary<string, string> Packages => new Dictionary<string, string> {
        {"@esbuild/linux-x64","Esbuild.Native.linux-x64"},
        {"@esbuild/linux-arm64","Esbuild.Native.linux-arm64"},
        {"@esbuild/darwin-arm64","Esbuild.Native.osx-arm64"},
        {"@esbuild/darwin-x64","Esbuild.Native.osx-x64"},
        {"@esbuild/win32-x64","Esbuild.Native.win-x64"},
    };

    public static string BasePath => Path.GetFullPath("../");

    public static string PackagePath => Path.GetFullPath("../packages");
}