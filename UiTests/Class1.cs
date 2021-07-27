using FrameworkCore;
using NUnit.Framework;
using System;

namespace UiTests
{
    [TestFixture]
    public class Class1 : BaseTest
    {
        [Test]
        public static void test1()
        {
            DriverFactory.Driver.Navigate().GoToUrl("https://executeautomation.com/");
            Console.WriteLine("1");
        }
    }
}
