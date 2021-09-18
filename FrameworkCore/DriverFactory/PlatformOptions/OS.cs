using System.ComponentModel;

namespace FrameworkCore.PlatformOptions
{
    public enum OS
    {
        [Description("Windows 7")] Windows7,
        [Description("Windows 8")] Windows8,
        [Description("Windows 8.1")] Windows8_1,
        [Description("Windows 10")] Windows10,
        [Description("Linux")] Linux,
        [Description("macOS 10.13")] MacOS10_13,
        [Description("macOS 10.12")] MacOS10_12,
        [Description("OS X 10.11")] OSX10_11,
    }
}