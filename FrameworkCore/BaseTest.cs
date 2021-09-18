using System.Linq;
using FrameworkCore.Configuration;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace FrameworkCore
{
    public class BaseTest
    {

        [SetUp]
        public static void SetUp()
        {
            var args = TestExecutionContext.CurrentContext.CurrentTest.Arguments;
            var platform = (Platform)args.SingleOrDefault(a => a is Platform);
            platform = platform ?? Platforms.Default;
            


            DriverFactory.InitDriver(platform);
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
