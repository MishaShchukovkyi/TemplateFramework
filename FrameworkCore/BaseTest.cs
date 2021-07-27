using NUnit.Framework;

namespace FrameworkCore
{
    public class BaseTest
    {

        [SetUp]
        public static void SetUp()
        {
            DriverFactory.InitDriver("Chrome");
            DriverFactory.Driver.Navigate().GoToUrl("https://executeautomation.com/");
        }

        [TearDown]
        public static void TestTearDown()
        {
            //DriverFactory.CloseApp();
            //DriverFactory.CleanUpDriver();
        }
    }
}
