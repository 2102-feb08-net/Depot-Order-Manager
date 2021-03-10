using Moq;
using StoreApp.Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StoreApp.Library.Tests
{
    public class OrderLineTests
    {
        [Fact]
        public void OrderLine_LineTotal()
        {
            // arrange
            const decimal PRODUCT_PRICE = 1.29m;
            const int QUANTITY = 4;
            var product = new Mock<IProduct>();
            product.Setup(p => p.UnitPrice).Returns(PRODUCT_PRICE);
            IOrderLine orderLine = new OrderLine(product.Object, QUANTITY);

            // act
            decimal totalPrice = orderLine.LineTotalPrice;

            // assert
            Assert.Equal(PRODUCT_PRICE * QUANTITY, totalPrice, 3);
        }
    }
}