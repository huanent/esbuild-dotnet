namespace EsbuildTest;

[TestClass]
public class EsbuildTest
{
    [TestMethod]
    public async Task TransformAsync()
    {
        var result = await Esbuild.Esbuild.TransformAsync("""
        function foo():string{
            return "bar"
        }
        """);
        Assert.AreEqual(result, "function foo() {\n  return \"bar\";\n}\n");
    }
}