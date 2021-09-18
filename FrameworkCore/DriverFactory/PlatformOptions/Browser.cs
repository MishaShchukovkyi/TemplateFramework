using System.ComponentModel;

namespace FrameworkCore.PlatformOptions
{
    public enum Browser
    {
        [Description("Chrome")] Chrome,
        [Description("Chrome 81.0")] Chrome81,
        [Description("Firefox")] Firefox,
    }
}