using System.ComponentModel;

namespace FrameworkCore.PlatformOptions
{
    public enum ScreenResolution
    {
        [Description("")] Undefined,
        [Description("1024x768")] W1024H768,
        [Description("1280x960")] W1280H960,
        [Description("1280x1024")] W1280H1024,
        [Description("1600x1200")] W1600H1200,
        [Description("1920x1080")] W1920H1080,
        [Description("1920x1440")] W1920H1440,
    }
}