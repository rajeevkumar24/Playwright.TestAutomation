using AutoFixture.Xunit2;
using Microsoft.Playwright;
using PlaywrightTest.Fixture;
using PlaywrightTest.Models;
using PlaywrightTest.Pages;

namespace PlaywrightTest;

public class CreateProductTest
{
    private readonly ITestFixtureBase _testFixtureBase;
    private readonly IProductListPage _productListPage;
    private readonly IProductPage _productPage;

    public CreateProductTest(ITestFixtureBase testFixtureBase, IProductListPage productListPage, IProductPage productPage)
    {
        _testFixtureBase = testFixtureBase;
        _productListPage = productListPage;
        _productPage = productPage;
    }

    [Theory, AutoData]
    public async Task TestWithAutoFixtureData(Product product)
    {
        // Arrange
        await _testFixtureBase.NavigateToUrl();
        await _productListPage.CreateProductAsync();
        await _productPage.CreateProduct(product);
        await _productPage.ClickCreate();
        
        // Act
        await _productListPage.ClickProductFromList(product.Name);

        // Assert
        var element = _productListPage.IsProductCreated(product.Name);
        await Assertions.Expect(element).ToBeVisibleAsync();
    }
}