using PlaywrightFramework.Config;
using Microsoft.Playwright;

namespace PlaywrightFramework.Driver;

public class PlaywrightDriver : IDisposable, IPlaywrightDriver
{
    private readonly AsyncLazy<IBrowser> _browser;
    private readonly AsyncLazy<IBrowserContext> _browserContext;
    private readonly AsyncLazy<IPage> _page;
    private readonly IPlaywrightDriverInitializer _playwrightDriverInitializer;
    private readonly TestSettings _testSettings;
    private bool _isDisposed;

    public PlaywrightDriver(TestSettings testSettings, IPlaywrightDriverInitializer playwrightDriverInitializer)
    {
        _testSettings = testSettings;
        _playwrightDriverInitializer = playwrightDriverInitializer;

        _browser = new AsyncLazy<IBrowser>(InitializePlaywright);
        _browserContext = new AsyncLazy<IBrowserContext>(CreateBrowserContext);
        _page = new AsyncLazy<IPage>(CreatePageAsync);
    }

    public Task<IPage> Page => _page.Value;

    public Task<IBrowser> Browser => _browser.Value;

    public Task<IBrowserContext> BrowserContext => _browserContext.Value;

    public void Dispose()
    {
        if (_isDisposed) return;

        if (_browser.IsValueCreated)
            Task.Run(async () =>
            {
                await (await Browser).CloseAsync();
                await (await Browser).DisposeAsync();
            });

        _isDisposed = true;
    }


    private async Task<IBrowser> InitializePlaywright()
    {
        return _testSettings.DriverType switch
        {
            DriverType.Chrome => await _playwrightDriverInitializer.GetChromeDriverAsync(_testSettings),
            DriverType.Firefox => await _playwrightDriverInitializer.GetFirefoxDriverAsync(_testSettings),
            DriverType.Edge => await _playwrightDriverInitializer.GetWebKitDriverAsync(_testSettings),
            DriverType.Chromium => await _playwrightDriverInitializer.GetChromiumDriverAsync(_testSettings),
            _ => await _playwrightDriverInitializer.GetChromiumDriverAsync(_testSettings)
        };
    }


    private async Task<IPage> CreatePageAsync()
    {
        return await (await _browser).NewPageAsync();
    }

    private async Task<IBrowserContext> CreateBrowserContext()
    {
        return await (await _browser).NewContextAsync();
    }
}