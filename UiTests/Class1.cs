using BusinessLayer;
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
            ExecuteAutomationPage.ClickButton();
            ExecuteAutomationPage.ClickButton2();
            Console.WriteLine("1");
            
        }
    }
}
