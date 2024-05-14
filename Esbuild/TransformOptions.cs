using System.ComponentModel;

namespace Esbuild;

public class TransformOptions
{
    /// <summary>
    /// Documentation: https://esbuild.github.io/api/#sourcefile *
    /// </summary>
    public string? Sourcefile { get; set; }
    /// <summary>
    /// Documentation: https://esbuild.github.io/api/#loader
    /// </summary>
    public Loader? Loader { get; set; }
    /// <summary>
    /// Documentation: https://esbuild.github.io/api/#banner 
    /// </summary>
    public string? Banner { get; set; }
    /// <summary>
    /// Documentation: https://esbuild.github.io/api/#footer
    /// </summary>
    public string? Footer { get; set; }
}

public enum Loader
{
    [Description("base64")]
    Base64,
    [Description("binary")]
    Binary,
    [Description("copy")]
    Copy,
    [Description("css")]
    Css,
    [Description("dataurl")]
    DataUrl,
    [Description("default")]
    Default,
    [Description("empty")]
    Empty,
    [Description("file")]
    File,
    [Description("js")]
    Js,
    [Description("json")]
    Json,
    [Description("jsx")]
    Jsx,
    [Description("local-css")]
    LocalCss,
    [Description("text")]
    Text,
    [Description("ts")]
    Ts,
    [Description("tsx")]
    Tsx
}