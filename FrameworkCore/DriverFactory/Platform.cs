using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using FrameworkCore.Configuration;
using FrameworkCore.PlatformOptions;

namespace FrameworkCore
{
    public class Platform
    {
        private readonly OS? _os = new OS?();
        private readonly Browser? _browser = new Browser?();
        private readonly ScreenResolution? _screenResolution = new ScreenResolution?();
        private readonly LaunchTarget? _launchTarget = new LaunchTarget?();
        private readonly DeviceName? _deviceName = new DeviceName?();
        private readonly DeviceOrientation? _deviceOrientation = new DeviceOrientation?();
        private readonly MobilePlatform? _mobilePlatform = new MobilePlatform?();
        private readonly bool _isLocalTestingMode = false;
        private readonly string _localRunIdentifier;

        /// <summary>Configure Desktop driver with specified parameters</summary>
        /// <param name="os">OS to be used for execution</param>
        /// <param name="browser">Browser to be used for execution</param>
        /// <param name="screenResolution">Screen resolution to be used for execution</param>
        public Platform(OS? os = null, Browser? browser = null, ScreenResolution? screenResolution = null)
        {
            this._os = os;
            this._browser = browser;
            this._screenResolution = screenResolution;
            this.IsDesktop = true;
        }

        /// <summary>
        /// Configure Desktop driver with specified parameters using specified target platform
        /// </summary>
        /// <param name="target">Target Platform to be used for execution</param>
        /// <param name="os">OS to be used for execution</param>
        /// <param name="browser">Browser to be used for execution</param>
        /// <param name="screenResolution">Screen resolution to be used for execution</param>
        /// <param name="isLocalTestingMode">Determines if the Local testing mode turned On for Browser Stack</param>
        /// <param name="localRunIdentifier">Unique connection name for Local testing mode that is required when running multiple builds with Local Testing mode at the same time.</param>
        public Platform(
          LaunchTarget target,
          OS? os = null,
          Browser? browser = null,
          ScreenResolution? screenResolution = null,
          bool isLocalTestingMode = false,
          string localRunIdentifier = null)
          : this(os, browser, screenResolution)
        {
            this._launchTarget = new LaunchTarget?(target);
            this._isLocalTestingMode = isLocalTestingMode;
            this._localRunIdentifier = localRunIdentifier;
        }

        /// <summary>
        /// Configure Mobile driver with specified parameters using specified target platform
        /// </summary>
        /// <param name="deviceName">Device to be used for execution</param>
        /// <param name="mobilePlatform">Mobile platform to be used for execution</param>
        /// <param name="deviceOrientation">Device orientation to be used for execution</param>
        public Platform(
          DeviceName deviceName,
          MobilePlatform mobilePlatform,
          DeviceOrientation? deviceOrientation = null)
        {
            this.IsMobile = true;
            this._launchTarget = new LaunchTarget?(LaunchTarget.SauceLabs);
            this._deviceName = new DeviceName?(deviceName);
            this._deviceOrientation = deviceOrientation;
            this._mobilePlatform = new MobilePlatform?(mobilePlatform);
        }

        /// <summary>String representation of Platform configuration</summary>
        /// <remarks>
        /// It's included in Test Case name
        /// It should contain all the meaningful parameters for the platform
        /// </remarks>
        /// <returns>String</returns>
        public override string ToString()
        {
            string str1 = this.LaunchTarget.ToString();
            string str2;
            if (this.IsDesktop)
            {
                str2 = str1 + string.Format(";{0};{1}", (object)this.OS, (object)this.Browser);
                if ((uint)this.ScreenResolution > 0U)
                    str2 += string.Format(";{0}", (object)this.ScreenResolution);
            }
            else
                str2 = str1 + string.Format(";{0};{1};{2};{3}", (object)this.DeviceName, (object)this.MobilePlatformName, (object)this.MobilePlatformVersion, (object)this.DeviceOrientation);
            return str2;
        }

        /// <summary>
        /// Gets Browser/Driver type string suitable for WebDriverFactory
        /// </summary>
        /// <returns>Browser/Driver type string suitable for WebDriverFactory</returns>
        public string GetBrowserTypeString()
        {
            switch (this.LaunchTarget)
            {
                case LaunchTarget.Local:
                    return ((IEnumerable<string>)this.Browser.GetDescription<Browser>().ToLower().Split(' ')).First<string>();
                case LaunchTarget.SauceLabs:
                    return this.IsMobile ? "sauce_appium" : "sauce";
                case LaunchTarget.BrowserStack:
                    return "browserstack";
                default:
                    throw new InvalidOperationException("Launch target should be defined to get BrowserTypeString.");
            }
        }

        /// <summary>
        /// Gets driver settings dictionary suitable for WebDriverFactory
        /// </summary>
        /// <returns>Driver settings dictionary suitable for WebDriverFactory</returns>
        public Dictionary<string, string> GetDriverSettings()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            if (this.IsDesktop)
            {
                switch (this.LaunchTarget)
                {
                    case LaunchTarget.Local:
                        break;
                    case LaunchTarget.SauceLabs:
                        dictionary.Add("platform", this.OS.GetDescription<OS>());
                        dictionary.Add("browserName", this.BrowserType);
                        dictionary.Add("version", this.BrowserVersion);
                        dictionary.Add("maxDuration", "3000");
                        dictionary.Add("idleTimeout", "200");
                        dictionary.Add("screenResolution", this.ScreenResolution.GetDescription<ScreenResolution>());
                        break;
                    case LaunchTarget.BrowserStack:
                        dictionary.Add("os", this.OsType);
                        dictionary.Add("os_version", this.OsVersion);
                        dictionary.Add("browser", this.BrowserType);
                        dictionary.Add("browser_version", this.BrowserVersion);
                        dictionary.Add("resolution", this.ScreenResolution.GetDescription<ScreenResolution>());
                        dictionary.Add("browserstack.local", this._isLocalTestingMode.ToString());
                        dictionary.Add("browserstack.user", ActiveConfiguration.bsUsername);
                        dictionary.Add("browserstack.key", ActiveConfiguration.bsKey);
                        dictionary.Add("project", ActiveConfiguration.bsProject);
                        //dictionary.Add("build", ConfigurationManager.AppSettings.Get("bsBuild"));
                        dictionary.Add("browserstack.selenium_version", "3.14.0");
                        dictionary.Add("browserstack.use_w3c", "true");
                        dictionary.Add("browserstack.console", ConfigurationManager.AppSettings.Get("bsConsole") ?? "errors");
                        string s = ConfigurationManager.AppSettings.Get("bsIdleTimeout");
                        dictionary.Add("browserstack.idleTimeout", (s != null ? int.Parse(s) : 90).ToString());
                        if (this._isLocalTestingMode && !string.IsNullOrEmpty(this._localRunIdentifier))
                        {
                            dictionary.Add("browserstack.localIdentifier", this._localRunIdentifier);
                            break;
                        }
                        break;
                    default:
                        throw new InvalidOperationException("Launch target should be defined to get DriverSettings.");
                }
            }
            else
            {
                dictionary.Add("appiumVersion", "1.9.1");
                dictionary.Add("deviceName", this.DeviceName.GetDescription<DeviceName>());
                dictionary.Add("deviceOrientation", this.DeviceOrientation.GetDescription<DeviceOrientation>());
                dictionary.Add("platformVersion", this.MobilePlatformVersion);
                dictionary.Add("platformName", this.MobilePlatformName);
                dictionary.Add("browserName", this.Browser.GetDescription<Browser>());
            }
            return dictionary;
        }

        public bool IsDesktop { get; private set; }

        public bool IsMobile
        {
            get
            {
                return !this.IsDesktop;
            }
            private set
            {
                this.IsDesktop = !value;
            }
        }

        public OS OS
        {
            get
            {
                return this._os ?? OS.Windows10;
            }
        }

        public Browser Browser
        {
            get
            {
                if (this.IsDesktop)
                {
                    Browser? browser = this._browser;
                    return browser.HasValue ? browser.GetValueOrDefault() : Browser.Chrome;
                }
                switch (this.MobilePlatform)
                {
                    case MobilePlatform.Android__7_1:
                        return Browser.Chrome;
                    default:
                        throw new InvalidOperationException("Mobile platform should be defined for Mobile device.");
                }
            }
        }

        public string BrowserType
        {
            get
            {
                string str1 = this.Browser.GetDescription<Browser>().Split(' ')[0];
                string str2 = str1 == "IE" ? "Internet Explorer" : str1;
                return str2 == "Edge" ? "MicrosoftEdge" : str2;
            }
        }

        public string BrowserVersion
        {
            get
            {
                string[] strArray = this.Browser.GetDescription<Browser>().Split(' ');
                string str = strArray.Length > 1 ? strArray[1] : "";
                return !string.IsNullOrEmpty(str) ? str : "latest";
            }
        }

        public string OsType
        {
            get
            {
                string[] strArray = this.OS.GetDescription<OS>().Split(' ');
                string str = strArray[0];
                if (strArray.Length == 3)
                    str = strArray[0] + " " + strArray[1];
                else if (string.IsNullOrEmpty(strArray[0]))
                    throw new Exception("OS is not recognized");
                return str;
            }
        }

        public string OsVersion
        {
            get
            {
                string[] strArray = this.OS.GetDescription<OS>().Split(' ');
                string str = strArray[1];
                if (strArray.Length == 3)
                    str = strArray[2];
                else if (string.IsNullOrEmpty(strArray[0]))
                    throw new Exception("OS is not recognized");
                return str;
            }
        }

        public ScreenResolution ScreenResolution
        {
            get
            {
                return this._screenResolution ?? ScreenResolution.Undefined;
            }
        }

        public LaunchTarget LaunchTarget
        {
            get
            {
                return this._launchTarget ?? LaunchTarget.Local;
            }
        }

        public DeviceName DeviceName
        {
            get
            {
                return this._deviceName ?? DeviceName.Undefined;
            }
        }

        public DeviceOrientation DeviceOrientation
        {
            get
            {
                return this._deviceOrientation ?? DeviceOrientation.Portrait;
            }
        }

        public MobilePlatform MobilePlatform
        {
            get
            {
                return this._mobilePlatform ?? MobilePlatform.Undefined;
            }
        }

        public string MobilePlatformName
        {
            get
            {
                return this.MobilePlatform.GetDescription<MobilePlatform>().Split(';')[0];
            }
        }

        public string MobilePlatformVersion
        {
            get
            {
                return this.MobilePlatform.GetDescription<MobilePlatform>().Split(';')[1];
            }
        }
    }
}