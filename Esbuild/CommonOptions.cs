using System.ComponentModel;

namespace Esbuild;

public class CommonOptions
{
    /// <summary>
    /// Documentation: https://esbuild.github.io/api/#format
    /// </summary>
    public Format? Format { get; set; }
    
    /// <summary>
    /// Documentation: https://esbuild.github.io/api/#platform
    /// </summary>
    public Platform? Platform { get; set; }

    /// <summary>
    /// Documentation: https://esbuild.github.io/api/#tsconfig-raw
    /// </summary>
    public string? TsconfigRaw { get; set; }

    /// <summary>
    /// Documentation: https://esbuild.github.io/api/#charset
    /// </summary>
    public Charset? Charset { get; set; }

    public virtual IEnumerable<string> ToArguments()
    {
        var result = new List<string>();

        if (Format != default)
        {
            result.Add($"--format={Format?.GetDescription()}");
        }

        if (Platform != default)
        {
            result.Add($"--platform={Platform?.GetDescription()}");
        }

        if (TsconfigRaw != default)
        {
            result.Add($"--tsconfig-raw='{TsconfigRaw}'");
        }

        if (Charset != default)
        {
            result.Add($"--charset='{Charset?.GetDescription()}'");
        }

        return result;
    }
}

public enum Format
{
    [Description("iife")]
    Iife,
    [Description("cjs")]
    Cjs,
    [Description("esm")]
    Esm
}

public enum Platform
{
    [Description("browser")]
    Browser,
    [Description("node")]
    Node,
    [Description("neutral")]
    Neutral
}

public enum Charset
{
    [Description("ascii")]
    Ascii,
    [Description("utf8")]
    Utf8,
}