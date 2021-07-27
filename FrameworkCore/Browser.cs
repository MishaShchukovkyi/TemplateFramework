using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrameworkCore
{
    public static class Browser
    {
        public static T ExecuteJavaScript<T>(string script)
        {
            var ex = (IJavaScriptExecutor)DriverFactory.Driver;
            return (T)ex.ExecuteScript(script);
        }

        public static IReadOnlyCollection<Object> GetNetworkLog()
        {
            return ExecuteJavaScript<IReadOnlyCollection<Object>>("var performance = window.performance || window.mozPerformance || window.msPerformance || window.webkitPerformance || {}; var network = performance.getEntries() || {}; return network;");
        }

        public static List<string> FilterNetwork(IReadOnlyCollection<Object> networkLogs, string param)
        {
            return networkLogs
                .Select(log => (Dictionary<string, object>)log)
                .SelectMany(log => log)
                .Where(log => log.Key.Equals("name"))
                .Where(log => log.Value.ToString().Contains(param))
                .Select(log => log.Value)
                .Cast<string>().ToList();
        }

        public static void AddCookie(Cookie cookie)
        {
            DriverFactory.Driver.Manage().Cookies.AddCookie(cookie);
        }

    }
}
