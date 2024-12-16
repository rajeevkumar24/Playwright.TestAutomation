using Microsoft.Playwright;
using PlaywrightFramework.Driver;
using PlaywrightTest.Models;

namespace PlaywrightTest.Pages;

public interface IProductPage
{
    Task CreateProduct(Product product);
    Task ClickCreate();
}

public class ProductPage : IProductPage
{
    private readonly IPage _page;

    public ProductPage(IPlaywrightDriver playwrightDriver) => _page = playwrightDriver.Page.Result;

    private ILocator _txtName => _page.GetByLabel("Name");

    private ILocator _txtDescription => _page.GetByLabel("Description");

    private ILocator _txtPrice => _page.Locator("#Price");

    private ILocator _selectProduct => _page.GetByRole(AriaRole.Combobox, new() { Name = "ProductType" });

    private ILocator _lnkCreate => _page.GetByRole(AriaRole.Button, new() { Name = "Create" });


    public async Task CreateProduct(Product product)
    {
        await _txtName.FillAsync(product.Name);
        await _txtDescription.FillAsync(product.Description);
        await _txtPrice.FillAsync(product.Price.ToString());
        await _selectProduct.SelectOptionAsync(product.ProductType.ToString());
    }
    
    public async Task ClickCreate() => await _lnkCreate.ClickAsync();

}