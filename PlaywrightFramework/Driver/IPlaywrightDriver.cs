using Microsoft.Playwright;

namespace PlaywrightFramework.Driver;

public interface IPlaywrightDriver
{
    Task<IPage> Page { get; }
    Task<IBrowser> Browser { get; }
    Task<IBrowserContext> BrowserContext { get; }
}