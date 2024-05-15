# esbuild for dotnet platform

usage
1. install nuget ```Esbuild```
1. install nuget native esbuild binary package for you platform,like ```Esbuild.Native.osx-arm64```

```csharp
var result = await Esbuild.Bundler.TransformAsync("""
function foo():string{
  return "bar"
}
""", new Esbuild.TransformOptions
{
    Loader = Esbuild.Loader.Ts
});
Assert.AreEqual(result, "function foo() {\n  return \"bar\";\n}\n");
```