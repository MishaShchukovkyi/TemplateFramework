using BusinessLayer;
using FrameworkCore;
using NUnit.Framework;
using System;

namespace UiTests
{
    [TestFixture]
    public class Class1 : BaseTest
    {
        [Test][Parallelizable(ParallelScope.Self)]
        public static void test1()
        {
            ExecuteAutomationPage.ClickButton();
            ExecuteAutomationPage.ClickButton2();
            Console.WriteLine("1");
            
        }

        [Test]
        [Parallelizable(ParallelScope.Self)]
        public static void test2()
        {
            ExecuteAutomationPage.ClickButton();
            ExecuteAutomationPage.ClickButton2();
            Console.WriteLine("1");
        }

        [Test]
        [Parallelizable(ParallelScope.Self)]
        public static void test3()
        {
            ExecuteAutomationPage.ClickButton();
            ExecuteAutomationPage.ClickButton2();
            Console.WriteLine("1");
        }

        [Test]
        [Parallelizable(ParallelScope.Self)]
        public static void test4()
        {
            ExecuteAutomationPage.ClickButton();
            ExecuteAutomationPage.ClickButton2();
            Console.WriteLine("1");
        }

        [Test]
        [Parallelizable(ParallelScope.Self)]
        public static void test5()
        {
            ExecuteAutomationPage.ClickButton();
            ExecuteAutomationPage.ClickButton2();
            Console.WriteLine("1");
        }
        //main_ 1 commit
    }
}
