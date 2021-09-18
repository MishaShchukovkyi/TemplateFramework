using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using FrameworkCore.PlatformOptions;

namespace FrameworkCore
{
    public static class GlobalExtensions
    {
        public static string GetDescription<T>(this Enum enumerationValue) where T : struct
        {
            MemberInfo[] member = enumerationValue.GetType().GetMember(enumerationValue.ToString());
            if (member != null && (uint)member.Length > 0U)
            {
                object[] customAttributes = member[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (customAttributes != null && (uint)customAttributes.Length > 0U)
                    return ((DescriptionAttribute)customAttributes[0]).Description;
            }
            return enumerationValue.ToString();
        }

        public static Dictionary<string, string> GetRuntimeAttribs(object[] args)
        {
            return GlobalExtensions.AddAllAttribs(args[0] as Enum, args[1] as Enum, new Dictionary<string, string>());
        }

        public static Dictionary<string, string> GetRuntimeAttribs(OS os, Browser browser)
        {
            Dictionary<string, string> runtimeArgs = new Dictionary<string, string>();
            return GlobalExtensions.AddAllAttribs((Enum)os, (Enum)browser, runtimeArgs);
        }

        private static Dictionary<string, string> AddAllAttribs(
          Enum os,
          Enum browser,
          Dictionary<string, string> runtimeArgs)
        {
            runtimeArgs = GlobalExtensions.AddOsAttrib(os, runtimeArgs);
            runtimeArgs = GlobalExtensions.AddBrowserAttribs(browser, runtimeArgs);
            return runtimeArgs;
        }

        private static Dictionary<string, string> AddBrowserAttribs(
          Enum browser,
          Dictionary<string, string> runtimeArgs)
        {
            string[] strArray = browser.GetDescription<Browser>().Split(' ');
            int length = strArray.Length;
            if (string.IsNullOrEmpty(strArray[0]))
                throw new InvalidOperationException("Browser Enum string not available");
            runtimeArgs.Add("BrowserName", strArray[0]);
            if (strArray.Length > 1)
                runtimeArgs.Add("BrowserVersion", strArray[1]);
            return runtimeArgs;
        }

        private static Dictionary<string, string> AddOsAttrib(
          Enum os,
          Dictionary<string, string> runtimeArgs)
        {
            string description = os.GetDescription<OS>();
            if (string.IsNullOrEmpty(description))
                throw new InvalidOperationException("OS Enum string not available");
            runtimeArgs.Add("OpSys", description);
            return runtimeArgs;
        }
    }
}