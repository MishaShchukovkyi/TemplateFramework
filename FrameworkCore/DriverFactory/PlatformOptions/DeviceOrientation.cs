using System.ComponentModel;

namespace FrameworkCore.PlatformOptions
{
    public enum DeviceOrientation
    {
        Undefined,
        [Description("portrait")] Portrait,
        [Description("landscape")] Landscape,
    }
}