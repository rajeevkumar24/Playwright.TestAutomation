using Microsoft.Playwright;
using PlaywrightFramework.Driver;

namespace PlaywrightTest.Pages;

public interface IProductListPage
{
    Task CreateProductAsync();
    Task ClickProductFromList(string name);
    ILocator IsProductCreated(string product);
}

public class ProductListPage : IProductListPage
{
    private readonly IPage _page;

    public ProductListPage(IPlaywrightDriver playwrightDriver) => _page = playwrightDriver.Page.Result;

    private ILocator _lnkProductList => _page.GetByRole(AriaRole.Link, new() { Name = "Product" });
    private ILocator _lnkCreate => _page.GetByRole(AriaRole.Link, new() { Name = "Create" });
    public async Task CreateProductAsync()
    {
        await _lnkProductList.ClickAsync();
        await _lnkCreate.ClickAsync();
    }

    public async Task ClickProductFromList(string name)
    {
        await _page.GetByRole(AriaRole.Row, new() { Name = name })
            .GetByRole(AriaRole.Link, new() { Name = "Details" }).ClickAsync();
    }
    
    public ILocator IsProductCreated(string product)
    {
        return _page.GetByText(product, new() { Exact = true });
    }
}