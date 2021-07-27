using FrameworkCore;

namespace BusinessLayer
{
    public static class ExecuteAutomationPage
    {
        private static Element button => new Element("(//ul[@class='ct-ul']//a[text()='Courses'])[1]");

        private static Element button2 => new Element("//div[@id='course-appium-with-c-']");

        public static void ClickButton()
        {
            button.Click();
        }

        public static void ClickButton2()
        {
            button2.Click();
        }
    }
}
