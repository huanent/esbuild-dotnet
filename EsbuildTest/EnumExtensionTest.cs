using Esbuild;

namespace EsbuildTest;

[TestClass]
public class EnumExtensionTest
{
    [TestMethod]
    public void GetDescription()
    {
        Assert.AreEqual(Loader.Css.GetDescription(), "css");
    }
}