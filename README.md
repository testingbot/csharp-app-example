## TestingBot - C# Mobile App Testing

TestingBot provides a grid of browsers and mobile devices to run Automated tests.
This example demonstrates how to use C# with NUnit and Appium to run an automated test on a mobile device in the TestingBot cloud.

The test opens a sample calculator app, enters two numbers and checks the sum.

* [`android`](android): Android emulator (Pixel 8, Android 14)
* [`ios`](ios): iOS simulator (iPhone 16e, iOS 26.0)

### Requirements

* [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or newer

### Environment Setup

1. Clone this repository

2. TestingBot Credentials
    * Add your TestingBot Key and Secret as environment variables. You can find these in the [TestingBot Dashboard](https://testingbot.com/members/).
    ```
    $ export TESTINGBOT_KEY=<your TestingBot Key>
    $ export TESTINGBOT_SECRET=<your TestingBot Secret>
    ```
    (`TB_KEY`/`TB_SECRET` work too.)

3. Optional: specify `TB_APP_ID`
    By default the tests use our sample apps (https://testingbot.com/appium/sample.apk and https://testingbot.com/appium/sample.zip).
    To test your own app, upload it to [TestingBot Storage](https://testingbot.com/support/app-automate/help#upload) and set `TB_APP_ID` to the `tb://...` id it returns (or to a public URL of an `.apk`, `.ipa` or `.zip`):
    ```
    $ export TB_APP_ID=tb://<your-app-id>
    ```
    iOS simulators need a zipped `.app` simulator build. For a physical device, use an `.ipa` and add `["realDevice"] = true` to `tb:options`.

### Running Tests

```
$ cd android   # or: cd ios
$ dotnet test
```

Each test is marked as passed or failed on TestingBot. You will see the test results in the [TestingBot Dashboard](https://testingbot.com/members/).

### Resources
##### [TestingBot C# Appium Documentation](https://testingbot.com/support/app-automate/appium/csharp)
##### [Appium Documentation](https://appium.io/)
