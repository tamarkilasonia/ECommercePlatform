using System.ComponentModel.DataAnnotations;
using System.Linq;
using eShop.Basket.API.Model;

namespace eShop.Basket.UnitTests;

[TestClass]
public class BasketItemValidationTests
{
    [TestMethod]
    [DataRow(0)]
    [DataRow(-1)]
    [DataRow(int.MinValue)]
    public void QuantityBelowOneIsInvalid(int quantity)
    {
        var item = new BasketItem { ProductId = 1, Quantity = quantity };

        var results = item.Validate(new ValidationContext(item)).ToList();

        Assert.HasCount(1, results);
        Assert.AreEqual("Invalid number of units", results[0].ErrorMessage);
        Assert.Contains("Quantity", results[0].MemberNames);
    }

    [TestMethod]
    [DataRow(1)]
    [DataRow(5)]
    [DataRow(int.MaxValue)]
    public void QuantityOfOneOrMoreIsValid(int quantity)
    {
        var item = new BasketItem { ProductId = 1, Quantity = quantity };

        var results = item.Validate(new ValidationContext(item)).ToList();

        Assert.IsEmpty(results);
    }

    [TestMethod]
    public void NewCustomerBasketStartsEmpty()
    {
        var basket = new CustomerBasket("buyer-1");

        Assert.AreEqual("buyer-1", basket.BuyerId);
        Assert.IsEmpty(basket.Items);
    }
}
