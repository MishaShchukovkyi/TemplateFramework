using System.ComponentModel;

namespace FrameworkCore.PlatformOptions
{
    public enum DeviceName
    {
        Undefined,
        [Description("Android GoogleAPI Emulator")] AndroidEmulator,
        [Description("Samsung Galaxy S8 GoogleAPI Emulator")] SamsungGalaxyS8GoogleAPIEmulator,
        [Description("Samsung Galaxy Tab S3 GoogleAPI Emulator")] SamsungGalaxyTabS3GoogleAPIEmulator,
        [Description("iPhone 7 Simulator")] IPhone7Simulator,
        [Description("iPhone 7 Plus Simulator")] IPhone7PlusSimulator,
        [Description("iPhone XS Simulator")] IPhoneXSSimulator,
        [Description("iPad Pro (12.9 inch) Simulator")] IPadPro12InchesSimulator,
    }
}