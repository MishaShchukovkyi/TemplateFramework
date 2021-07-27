using System;
using System.Collections.ObjectModel;
using System.Drawing;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace FrameworkCore
{
    public class Element : IWebElement
    {
        private string _xPath;
        //private IWebElement innerElement;

        public Element(string xPathLocator)
        {
            _xPath = xPathLocator;
        }

        private IWebElement _element
        {
            get
            {
                WebDriverWait wait = new WebDriverWait(DriverFactory.Driver, TimeSpan.FromSeconds(5));
                wait.Until(ExpectedConditions.ElementExists((By.XPath(_xPath))));

                return DriverFactory.Driver.FindElement(By.XPath(_xPath));
            }
        }

        public IWebElement FindElement(By @by)
        {
            return _element.FindElement(by);
        }

        public ReadOnlyCollection<IWebElement> FindElements(By @by)
        {
            return _element.FindElements(by);
        }

        public void Clear()
        {
            _element.Clear();
        }

        public void SendKeys(string text)
        {
            _element.SendKeys(text);
        }

        public void Submit()
        {
            _element.Submit();
        }

        public void Click()
        {
            _element.Click();
        }

        public string GetAttribute(string attributeName)
        {
            return _element.GetAttribute(attributeName);
        }

        public string GetProperty(string propertyName)
        {
            return _element.GetProperty(propertyName);
        }

        public string GetCssValue(string propertyName)
        {
            return _element.GetCssValue(propertyName);
        }

        public string TagName { get; }


        public string Text => _element.Text;
        public bool Enabled => _element.Enabled;
        public bool Selected => _element.Selected;
        public Point Location => _element.Location;
        public Size Size => _element.Size;
        public bool Displayed => _element.Displayed;

    }
}
