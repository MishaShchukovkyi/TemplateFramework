using NUnit.Framework;

namespace FrameworkCore
{
    public class BaseTest
    {

        [SetUp]
        public static void SetUp()
        {
            DriverFactory.InitDriver("Chrome");
        }

        [TearDown]
        public static void TestTearDown()
        {
            DriverFactory.CloseApp();
            DriverFactory.CleanUpDriver();
        }
    }
}
