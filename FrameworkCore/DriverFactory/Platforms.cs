using System.Linq;
using FrameworkCore.PlatformOptions;
using OpenQA.Selenium;

namespace FrameworkCore
{
    public static class Platforms
    {
        public static Platform Default
        {
            get
            {
#if DEBUG
                return LocalChrome.Single();
#else
                return RemoteChrome.Single();
#endif
            }
        }

        public static readonly Platform[] LocalChrome =  {
            new Platform(OS.Windows10, Browser.Chrome81, ScreenResolution.W1920H1080),
        };

        public static readonly Platform[] RemoteChrome = {
            new Platform(LaunchTarget.BrowserStack, OS.Windows10, Browser.Chrome81, ScreenResolution.W1920H1080),
        };
    };
}