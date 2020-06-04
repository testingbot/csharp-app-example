## TestingBot - C# Mobile App Testing

TestingBot provides a grid of browsers and mobile devices to run Automated tests.
This example demonstrates how to use C# to run an Automated Test on a Mobile Device, located in the TestingBot Cloud.

### Environment Setup

1. Setup
	* Clone this repository

2. TestingBot Credentials
    * Add your TestingBot Key and Secret as environmental variables. You can find these in the [TestingBot Dashboard](https://testingbot.com/members/).
    ```
    $ export TB_KEY=<your TestingBot Key>
    $ export TB_SECRET=<your TestingBot Secret>
    ```
3. Specify `TB_APP_ID`
	Let us know which app you want to test by specifying the `TB_APP_ID` environment variable. This should point to a public URL and contain a `.apk`, `.ipa` or `.zip` file.
	For more information, please see our [Mobile App Documentation](https://testingbot.com/support/mobile/help.html).

	If you do not have an `.ipa` or `.apk` file and just want to test, you can use https://testingbot.com/appium/sample.apk or https://testingbot.com/appium/sample.ipa

	To test with ipa files, you'll need to edit the `DesiredCapabilities` to include `realDevice: true`

## Running Tests
* To run a test, please use Visual Studio or Mono.

You will see the test result in the [TestingBot Dashboard](https://testingbot.com/members/)

### Resources
##### [TestingBot Documentation](https://testingbot.com/support/mobile/csharp.html)
##### [Appium Documentation](http://appium.io/)