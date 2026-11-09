using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;

namespace TestingBotAppiumAndroid;

[TestFixture]
public class AndroidAppTest
{
    private AndroidDriver? _driver;

    [SetUp]
    public void SetUp()
    {
        var options = new AppiumOptions
        {
            PlatformName = "Android",
            AutomationName = "UiAutomator2",
            DeviceName = "Pixel 8",
            PlatformVersion = "14.0",
            // TB_APP_ID: a tb:// id from TestingBot Storage or a public URL; defaults to our sample app
            App = Environment.GetEnvironmentVariable("TB_APP_ID") ?? "https://testingbot.com/appium/sample.apk",
        };
        options.AddAdditionalAppiumOption("tb:options", new Dictionary<string, object>
        {
            ["key"] = Credential("TESTINGBOT_KEY", "TB_KEY"),
            ["secret"] = Credential("TESTINGBOT_SECRET", "TB_SECRET"),
            ["name"] = TestContext.CurrentContext.Test.Name,
        });

        _driver = new AndroidDriver(new Uri("https://hub.testingbot.com/wd/hub"), options, TimeSpan.FromSeconds(300));
    }

    [Test]
    public void SumOfTwoNumbers()
    {
        var wait = new WebDriverWait(_driver!, TimeSpan.FromSeconds(30));

        var inputA = wait.Until(d => d.FindElement(MobileBy.AccessibilityId("inputA")));
        inputA.SendKeys("10");

        var inputB = wait.Until(d => d.FindElement(MobileBy.AccessibilityId("inputB")));
        inputB.SendKeys("5");

        var sum = wait.Until(d => d.FindElement(MobileBy.AccessibilityId("sum")));
        wait.Until(_ => sum.Text == "15");
        Assert.That(sum.Text, Is.EqualTo("15"));
    }

    [TearDown]
    public void TearDown()
    {
        if (_driver == null) return;
        try
        {
            var passed = TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed;
            ((IJavaScriptExecutor)_driver).ExecuteScript("tb:test-result=" + (passed ? "passed" : "failed"));
        }
        finally
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }

    private static string Credential(string name, string fallback) =>
        Environment.GetEnvironmentVariable(name)
        ?? Environment.GetEnvironmentVariable(fallback)
        ?? throw new InvalidOperationException($"Set the {name} environment variable");
}
