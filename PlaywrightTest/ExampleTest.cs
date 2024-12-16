using Microsoft.Playwright;
using PlaywrightFramework.Config;
using PlaywrightFramework.Driver;
using PlaywrightTest.Pages;
using System.Text.RegularExpressions;

namespace PlaywrightTests
{

    public class ExampleTest 
    {
        private readonly IPlaywrightDriver _playwrightDriver;
        private readonly TestSettings _testSettings;
        private readonly IProductListPage _productListPage;
        private readonly IProductPage _productPage;

        public ExampleTest(IPlaywrightDriver playwrightDriver, TestSettings testSettings, IProductListPage productListPage, IProductPage productPage)
        {
            _playwrightDriver = playwrightDriver;
            _testSettings = testSettings;
            _productListPage = productListPage;
            _productPage = productPage;
        }
        [Fact]
        public async Task HasTitle()
        {
            var page = await _playwrightDriver.Page;
            await page.GotoAsync("https://playwright.dev");
            await Assertions.Expect(page).ToHaveTitleAsync(new Regex("Playwright"));
        }
    }
}
