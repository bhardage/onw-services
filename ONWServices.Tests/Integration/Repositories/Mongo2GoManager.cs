using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ONWServices.Tests.Integration.Repositories
{
    [TestClass]
    public class Mongo2GoManager
    {
        [AssemblyInitialize]
        public static void BeforeAll(TestContext context)
        {
            BaseMongoIntegrationTest.InitializeDbContext();
        }

        [AssemblyCleanup]
        public static void AfterAll()
        {
            BaseMongoIntegrationTest.DestroyDbContext();
        }
    }
}
