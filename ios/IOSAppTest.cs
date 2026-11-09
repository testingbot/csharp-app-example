using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Support.UI;

namespace TestingBotAppiumIOS;

[TestFixture]
public class IOSAppTest
{
    private IOSDriver? _driver;

    [SetUp]
    public void SetUp()
    {
        var options = new AppiumOptions
        {
            PlatformName = "iOS",
            AutomationName = "XCUITest",
            DeviceName = "iPhone 16e",
            PlatformVersion = "26.0",
            // TB_APP_ID: a tb:// id from TestingBot Storage or a public URL; defaults to our sample
            // simulator app (use an .ipa and "realDevice" in tb:options for physical devices)
            App = Environment.GetEnvironmentVariable("TB_APP_ID") ?? "https://testingbot.com/appium/sample.zip",
        };
        options.AddAdditionalAppiumOption("tb:options", new Dictionary<string, object>
        {
            ["key"] = Credential("TESTINGBOT_KEY", "TB_KEY"),
            ["secret"] = Credential("TESTINGBOT_SECRET", "TB_SECRET"),
            ["name"] = TestContext.CurrentContext.Test.Name,
        });

        _driver = new IOSDriver(new Uri("https://hub.testingbot.com/wd/hub"), options, TimeSpan.FromSeconds(300));
    }

    [Test]
    public void SumOfTwoNumbers()
    {
        var wait = new WebDriverWait(_driver!, TimeSpan.FromSeconds(30));

        // On iOS the accessibility id also matches the label next to each field,
        // so select the text fields explicitly.
        var inputA = wait.Until(d => d.FindElement(MobileBy.IosNSPredicate("name == 'inputA' AND type == 'XCUIElementTypeTextField'")));
        inputA.Clear(); // the field starts with "0"
        inputA.SendKeys("10");

        var inputB = wait.Until(d => d.FindElement(MobileBy.IosNSPredicate("name == 'inputB' AND type == 'XCUIElementTypeTextField'")));
        inputB.Clear();
        inputB.SendKeys("5");

        var sum = wait.Until(d => d.FindElement(MobileBy.IosNSPredicate("name == 'sum' AND type == 'XCUIElementTypeTextField'")));
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
