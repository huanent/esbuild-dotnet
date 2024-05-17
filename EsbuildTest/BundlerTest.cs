namespace EsbuildTest;

[TestClass]
public class BundlerTest
{
    [TestMethod]
    public async Task TransformAsync()
    {
        var result = await Esbuild.Bundler.TransformAsync("""
        function foo():string{
          return "bar"
        }
        """, new Esbuild.TransformOptions
        {
            Loader = Esbuild.Loader.Ts
        });
        Assert.AreEqual(result, "function foo() {\n  return \"bar\";\n}\n");
    }

    [TestMethod]
    public void Transform()
    {
        var result = Esbuild.Bundler.Transform("""
        function foo():string{
          return "bar"
        }
        """, new Esbuild.TransformOptions
        {
            Loader = Esbuild.Loader.Ts
        });
        Assert.AreEqual(result, "function foo() {\n  return \"bar\";\n}\n");
    }
}