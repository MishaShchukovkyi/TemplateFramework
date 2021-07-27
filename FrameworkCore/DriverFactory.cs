using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Concurrent;

namespace FrameworkCore
{
    public static class DriverFactory
    {
        private static ConcurrentDictionary<string, IWebDriver> DriverDictionary = new ConcurrentDictionary<string, IWebDriver>();
        private static DriverInitLevel driverInitLevel;

        private static string DriverKey
        {
            get
            {
                switch (driverInitLevel)
                {
                    case DriverInitLevel.TEST:
                        return TestContext.CurrentContext.Test.FullName;
                    case DriverInitLevel.FIXTURE:
                        return TestContext.CurrentContext.Test.ClassName;
                    default:
                        throw new Exception("Unrecognized driver initialization level");
                }
            }
        }
        public static IWebDriver Driver
        {
            get
            {
                try
                {
                    return DriverDictionary[DriverKey];
                }
                catch
                {
                    throw new Exception("Driver is not initialized and doesn't exist in Driver Dictionary.");
                }
            }
            internal set
            {
            }
        }

        //Method for init driver, can contain additional logic, parse incoming 'string' tp browser
        public static void InitDriver(string browser)
        {
            DriverDictionary.TryAdd(DriverKey, new ChromeDriver());//add logic for creating instances of different browsers FF,Chrome,SauceLab
        }

        public static void CleanUpDriver()
        {
            IWebDriver driver = null;
            DriverDictionary.TryRemove(DriverKey, out driver);
        }
        public static void CloseApp()
        {
            Driver.Quit();
        }

        public enum DriverInitLevel
        {
            TEST,
            FIXTURE
        }

    }

}
