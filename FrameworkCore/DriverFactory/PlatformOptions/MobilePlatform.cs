using System.ComponentModel;

namespace FrameworkCore.PlatformOptions
{
    public enum MobilePlatform
    {
        Undefined,
        [Description("Android;7.1")] Android__7_1,
        [Description("iOS;10.3")] IOS__10_3,
        [Description("iOS;12.0")] IOS__12,
    }
}