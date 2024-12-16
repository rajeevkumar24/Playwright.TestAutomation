using AutoFixture.Xunit2;
using Microsoft.Playwright;
using PlaywrightFramework.Config;
using PlaywrightFramework.Driver;
using PlaywrightTest.Models;
using PlaywrightTest.Pages;

namespace PlaywrightTest;

public class UnitTest1
{
    private readonly IPlaywrightDriver _playwrightDriver;
    private readonly TestSettings _testSettings;
    private readonly IProductListPage _productListPage;
    private readonly IProductPage _productPage;

    public UnitTest1(IPlaywrightDriver playwrightDriver, TestSettings testSettings, IProductListPage productListPage, IProductPage productPage)
    {
        _playwrightDriver = playwrightDriver;
        _testSettings = testSettings;
        _productListPage = productListPage;
        _productPage = productPage;
    }

    [Fact]
    public async Task Test1()
    {
        var page = await _playwrightDriver.Page;

        await page.GotoAsync(_testSettings.ApplicationUrl);

        await page.GetByRole(AriaRole.Link, new PageGetByRoleOptions { Name = "Login" }).ClickAsync();

        await page.GetByLabel("UserName").FillAsync("admin");

        await page.GetByLabel("Password").FillAsync("password");

        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Log in" }).ClickAsync();

        await page.GetByRole(AriaRole.Link, new PageGetByRoleOptions { Name = "Employee List" }).ClickAsync();
    }

    // [Theory]
    // [InlineData("Speaker", "Gaming Speaker", 2000, "2")]
    // [InlineData("USB", "USB 3.0", 300, "3")]
    // [InlineData("Webcam", "Camera", 4000, "2")]
    // [InlineData("Wires", "Wires for life", 1000, "2")]
    // public async Task Test_WithInlineData(string name, string description, int price, string productType)
    // {
    //     var page = await _playwrightDriver.Page;
    //     
    //     await page.GotoAsync("http://localhost:8000/");
    //
    //     ProductListPage productListPage = new ProductListPage(page);
    //     ProductPage productPage = new ProductPage(page);
    //     
    //     
    //     await productListPage.CreateProductAsync();
    //     //await productPage.CreateProduct(name, description, price, productType);
    //     await productPage.ClickCreate();
    //     
    //     await productListPage.ClickProductFromList(name);
    //
    //     
    //     var element = productListPage.IsProductCreated(name);
    //     await Assertions.Expect(element).ToBeVisibleAsync();
    // }
    //
    //
    // [Fact]
    // public async Task TestWithConcreteTypes()
    // {
    //     var page = await _playwrightDriver.Page;
    //
    //     var product = new Product()
    //     {
    //         Name = "Test Product",
    //         Description = "Test Product Description",
    //         Price = 1000,
    //         ProductType = ProductType.CPU,
    //     };
    //     
    //     await page.GotoAsync("http://localhost:8000/");
    //
    //     ProductListPage productListPage = new ProductListPage(page);
    //     ProductPage productPage = new ProductPage(page);
    //     
    //     
    //     await productListPage.CreateProductAsync();
    //     await productPage.CreateProduct(product);
    //     await productPage.ClickCreate();
    //     
    //     await productListPage.ClickProductFromList(product.Name);
    //
    //     
    //     var element = productListPage.IsProductCreated(product.Name);
    //     await Assertions.Expect(element).ToBeVisibleAsync();
    // }
    
    [Theory, AutoData]
    public async Task TestWithAutoFixtureData(Product product)
    {
        var page = await _playwrightDriver.Page;

        await page.GotoAsync("http://localhost:8000/");
        
        await _productListPage.CreateProductAsync();
        await _productPage.CreateProduct(product);
        await _productPage.ClickCreate();
        
        await _productListPage.ClickProductFromList(product.Name);

        
        var element = _productListPage.IsProductCreated(product.Name);
        await Assertions.Expect(element).ToBeVisibleAsync();
    }
}