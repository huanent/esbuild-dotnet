namespace Esbuild;

public class BuildOptions : CommonOptions
{
    /// <summary>
    /// Documentation: https://esbuild.github.io/api/#bundl
    /// </summary>
    public bool Bundle { get; set; }

    public override IEnumerable<string> ToArguments()
    {
        var result = new List<string>(base.ToArguments());

        if (Bundle) result.Add($"--bundle");

        return result;
    }
}