using FrameworkCore.Configuration;
using NUnit.Framework;

namespace FrameworkCore
{
    public class BaseTest
    {

        [SetUp]
        public static void SetUp()
        {
            DriverFactory.InitDriver("Chrome");
            DriverFactory.Driver.Navigate().GoToUrl(ActiveConfiguration.AppUrl);
        }

        [TearDown]
        public static void TestTearDown()
        {
            DriverFactory.CloseApp();
            DriverFactory.CleanUpDriver();
        }
    }
}
