using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using FrameworkCore.Configuration;
//LOX

namespace FrameworkCore
{
    public static class DriverFactory
    {
        private static ConcurrentDictionary<string, IWebDriver> DriverDictionary = new ConcurrentDictionary<string, IWebDriver>();
        private static DriverInitLevel driverInitLevel;
        private static string GetTestIdentifier()
        {
            return ((IEnumerable<string>)TestContext.CurrentContext.Test.ClassName.Split('.')).Last<string>() + " " + TestContext.CurrentContext.Test.Name;
        }
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

        //Method for init driver, can contain additional logic, parse incoming 'string' tp customBrowser
        public static void InitDriver(Platform platform)
        {
            string browserType = platform.GetBrowserTypeString();

            DriverDictionary.TryAdd(DriverKey, GetDriverInstance(browserType, platform.GetDriverSettings()));

        }

        private static IWebDriver GetDriverInstance(string browser, Dictionary<string, string> settings = null)
        {
            switch (browser)
            {
                case "browserstack":
                    return GetBrowserStackDriver(settings);
                case "chrome":
                    return GetChromeDriver();

                default:
                    throw new Exception("Unrecognized driver type");
            }
        }

        private static IWebDriver GetChromeDriver()
        {
            ChromeDriver driver = new ChromeDriver();
            return driver;
        }

        private static IWebDriver GetBrowserStackDriver(Dictionary<string, string> settings = null, DriverOptions options = null)
        {
            DesiredCapabilities desiredCapabilities = new DesiredCapabilities();
            string uriString = ActiveConfiguration.bsServer;

            foreach (KeyValuePair<string, string> setting in settings)
                desiredCapabilities.SetCapability(setting.Key, setting.Value);

            desiredCapabilities.SetCapability("name", (object)GetTestIdentifier());
            RemoteWebDriver remoteWebDriver = new RemoteWebDriver(new Uri(uriString), (ICapabilities)desiredCapabilities);

            if (!settings.ContainsKey("deviceName"))
                remoteWebDriver.Manage().Window.Maximize();
            return (IWebDriver)remoteWebDriver;
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
