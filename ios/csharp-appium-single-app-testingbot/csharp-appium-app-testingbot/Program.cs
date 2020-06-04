using System;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;
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
            DesiredCapabilities caps = new DesiredCapabilities();
            caps.SetCapability("key", System.Environment.GetEnvironmentVariable(MainClass.key));
            caps.SetCapability("secret", System.Environment.GetEnvironmentVariable(MainClass.secret));
            caps.SetCapability("deviceName", "iPhone 11");
            caps.SetCapability("version", "13.4");
            caps.SetCapability("app", System.Environment.GetEnvironmentVariable("TB_APP_ID"));

            IOSDriver<IOSElement> driver = new IOSDriver<IOSElement> (new Uri("http://hub.testingbot.com/wd/hub"), caps, TimeSpan.FromSeconds(300));

            IOSElement inputA = (IOSElement) new WebDriverWait(driver, TimeSpan.FromSeconds(30)).Until(
                ExpectedConditions.ElementToBeClickable(MobileBy.AccessibilityId("inputA"))
            );
            inputA.SendKeys("10");
            IOSElement inputB = (IOSElement)new WebDriverWait(driver, TimeSpan.FromSeconds(30)).Until(
                ExpectedConditions.ElementToBeClickable(MobileBy.AccessibilityId("inputB"))
            );
            inputB.SendKeys("5");

            IOSElement sum = (IOSElement)new WebDriverWait(driver, TimeSpan.FromSeconds(30)).Until(
                ExpectedConditions.ElementToBeClickable(MobileBy.AccessibilityId("sum"))
            );

            Console.WriteLine(sum);
            driver.Quit();
        }
    }
}
