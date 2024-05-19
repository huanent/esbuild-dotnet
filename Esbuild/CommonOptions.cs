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

    /// <summary>
    /// Documentation: https://esbuild.github.io/api/#jsx
    /// </summary>
    public Jsx Jsx { get; set; }

    /// <summary>
    /// Documentation: https://esbuild.github.io/api/#jsx-dev
    /// </summary>
    public bool JsxDev { get; set; }

    /// <summary>
    /// Documentation: https://esbuild.github.io/api/#jsx-factory
    /// </summary>
    public string? JsxFactory { get; set; }

    /// <summary>
    /// Documentation: https://esbuild.github.io/api/#jsx-fragment
    /// </summary>
    public string? JsxFragment { get; set; }

    /// <summary>
    /// Documentation: https://esbuild.github.io/api/#jsx-import-source
    /// </summary>
    public string? JsxImportSource { get; set; }

    /// <summary>
    /// Documentation: https://esbuild.github.io/api/#jsx-side-effects
    /// </summary>
    public bool JsxSideEffects { get; set; }

    /// <summary>
    /// Documentation: https://esbuild.github.io/api/#target
    /// </summary>
    public string[]? Target { get; set; }

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
            result.Add($"--charset={Charset?.GetDescription()}");
        }

        if (Jsx != default)
        {
            result.Add($"--jsx={Jsx.GetDescription()}");
        }

        if (JsxDev)
        {
            result.Add($"--jsx-dev");
        }

        if (JsxFactory != default)
        {
            result.Add($"--jsx-factory={JsxFactory}");
        }

        if (JsxFragment != default)
        {
            result.Add($"--jsx-fragment={JsxFragment}");
        }

        if (JsxImportSource != default)
        {
            result.Add($"--jsx-import-source={JsxImportSource}");
        }

        if (JsxSideEffects)
        {
            result.Add($"--jsx-side-effects");
        }

        if (Target != default)
        {
            var value = string.Join(',', Target);
            result.Add($"--target={value}");
        }

        return result;
    }
}

public enum Jsx
{
    [Description("transform")]
    Transform,
    [Description("preserve")]
    Preserve,
    [Description("automatic")]
    Automatic
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