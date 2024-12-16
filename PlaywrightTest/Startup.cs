
using Microsoft.Extensions.DependencyInjection;
using PlaywrightFramework.Config;
using PlaywrightFramework.Driver;
using PlaywrightTest.Fixture;
using PlaywrightTest.Pages;

namespace PlaywrightTest;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddSingleton(ConfigReader.ReadConfig())
            .AddScoped<IPlaywrightDriver, PlaywrightDriver>()
            .AddScoped<IPlaywrightDriverInitializer, PlaywrightDriverInitializer>()
            .AddScoped<IProductPage, ProductPage>()
            .AddScoped<IProductListPage, ProductListPage>()
            .AddScoped<ITestFixtureBase, TestFixtureBase>();
    }
}