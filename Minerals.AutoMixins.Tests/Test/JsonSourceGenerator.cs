using Hachi.Data;

[TestClass]
public class JsonSourceGeneratorTest : VerifyBase
{
    public JsonSourceGeneratorTest()
    {
        var references = VerifyExtensions.GetAppReferences
        (
            typeof(object),
            typeof(JsonSourceGenerator),
            typeof(Assembly)
        );
        VerifyExtensions.Initialize(references);
    }

    [TestMethod]
    public Task JsonSourceGeneratorTest_Attribute_ShouldGenerate()
    {
        return this.VerifyIncrementalGenerators(new JsonSourceGenerator());
    }
}