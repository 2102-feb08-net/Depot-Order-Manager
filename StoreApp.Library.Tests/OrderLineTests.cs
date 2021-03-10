using Xunit;
using Moq;
using StoreApp.Library.Model;

namespace StoreApp.Library.Tests
{
    public class OrderLineTests
    {
        [Fact]
        public void OrderLine_LineTotal()
        {
            // arrange
            const decimal PRODUCT_PRICE = 1.49m;
            const int QUANTITY = 7;
            var product = new Mock<IProduct>();
            product.Setup(p => p.UnitPrice).Returns(PRODUCT_PRICE);
            IOrderLine orderLine = new OrderLine(product.Object, QUANTITY);

            // act
            decimal totalPrice = orderLine.LineTotalPrice;

            // assert
            Assert.Equal(0, (PRODUCT_PRICE * QUANTITY) - totalPrice, 3);
        }
    }
}