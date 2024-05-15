using System.ComponentModel;
using System.Reflection;

namespace Esbuild;

public static class EnumExtension
{
    public static string? GetDescription(this Enum @enum)
    {
        var attribute = @enum.GetType()
            ?.GetField(@enum.ToString())
            ?.GetCustomAttribute<DescriptionAttribute>(true);
        return attribute?.Description;
    }
}