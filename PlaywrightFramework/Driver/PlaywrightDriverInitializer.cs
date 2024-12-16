using PlaywrightFramework.Config;
using Microsoft.Playwright;

namespace PlaywrightFramework.Driver;

public class PlaywrightDriverInitializer : IPlaywrightDriverInitializer
{
    public const float DEFAULT_TIMEOUT = 30f;


    public async Task<IBrowser> GetChromeDriverAsync(TestSettings testSettings)
    {
        var options = GetParameters(testSettings.Args, testSettings.Timeout, testSettings.Headless,
            testSettings.SlowMo);
        options.Channel = "chrome";

        return await GetBrowserAsync(DriverType.Chrome, options);
    }

    public async Task<IBrowser> GetFirefoxDriverAsync(TestSettings testSettings)
    {
        var options = GetParameters(testSettings.Args, testSettings.Timeout, testSettings.Headless,
            testSettings.SlowMo);
        options.Channel = "firefox";
        return await GetBrowserAsync(DriverType.Firefox, options);
    }

    public async Task<IBrowser> GetWebKitDriverAsync(TestSettings testSettings)
    {
        var options = GetParameters(testSettings.Args, testSettings.Timeout, testSettings.Headless,
            testSettings.SlowMo);
        options.Channel = "msedge";
        return await GetBrowserAsync(DriverType.Edge, options);
    }

    public async Task<IBrowser> GetChromiumDriverAsync(TestSettings testSettings)
    {
        var options = GetParameters(testSettings.Args, testSettings.Timeout, testSettings.Headless,
            testSettings.SlowMo);
        options.Channel = "chromium";
        return await GetBrowserAsync(DriverType.Chromium, options);
    }


    private async Task<IBrowser> GetBrowserAsync(DriverType driverType, BrowserTypeLaunchOptions options)
    {
        var playwright = await Playwright.CreateAsync();

        return await playwright[driverType.ToString().ToLower()].LaunchAsync(options);
    }

    private BrowserTypeLaunchOptions GetParameters(string[]? args, float? timeout = DEFAULT_TIMEOUT,
        bool? headless = true, float? slowmo = null)
    {
        return new BrowserTypeLaunchOptions
        {
            Args = args,
            Timeout = ToMilliseconds(timeout),
            Headless = headless,
            SlowMo = slowmo
        };
    }


    private static float? ToMilliseconds(float? seconds)
    {
        return seconds * 1000;
    }
}