using System;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace TestingBotAppiumSingleTest
{
    class MainClass
    {
        readonly static string key = "TB_KEY";
        readonly static string secret = "TB_SECRET";

        public static void Main(string[] args)
        {
            AppiumOptions caps = new AppiumOptions();
            caps.AddAdditionalCapability("key", System.Environment.GetEnvironmentVariable(MainClass.key));
            caps.AddAdditionalCapability("secret", System.Environment.GetEnvironmentVariable(MainClass.secret));
            caps.AddAdditionalCapability("deviceName", "Galaxy S9");
            caps.AddAdditionalCapability("version", "9.0");
            caps.AddAdditionalCapability("app", System.Environment.GetEnvironmentVariable("TB_APP_ID"));

            var driver = new AndroidDriver<AndroidElement>(new Uri("https://hub.testingbot.com/wd/hub"), caps, TimeSpan.FromSeconds(120));
            AndroidElement inputA = (AndroidElement)new WebDriverWait(driver,TimeSpan.FromSeconds(30)).Until(
                SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(MobileBy.AccessibilityId("inputA"))
            );
            inputA.SendKeys("10");
            AndroidElement inputB = (AndroidElement)new WebDriverWait(driver, TimeSpan.FromSeconds(30)).Until(
                SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(MobileBy.AccessibilityId("inputB"))
            );
            inputB.SendKeys("5");
            System.Threading.Thread.Sleep(2000);

            driver.Quit();
        }
    }
}
