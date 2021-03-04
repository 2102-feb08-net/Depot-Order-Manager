using System;
using Xunit;
using StoreApp.Library.Model;
using System.Collections.Generic;

namespace StoreApp.Library.Tests
{
    public class LocationTests
    {
        private readonly IProduct apple = new Product(name: "Apple", category: "Food", unitPrice: 1.29m, id: 1);
        private static readonly IAddress address = new Address("1234 Address", "Apt. 2", "Newcity", "Nezona", "United States", 12345);

        [Fact]
        public void Location_Constructor_Success()
        {
            // arrange

            // act
            ILocation location = new Location("Location Name", address, new Dictionary<IProduct, int>(), id: 1);

            // assert
            Assert.NotNull(location);
        }

        [Fact]
        public void Location_NullName_Fail()
        {
            // arrange

            // act
            static ILocation CreateLocation() => new Location(name: null, address, new Dictionary<IProduct, int>(), id: 1);

            // assert
            Assert.Throws<ArgumentNullException>(CreateLocation);
        }

        [Fact]
        public void Location_NullAddress_Fail()
        {
            // arrange

            // act
            static ILocation CreateLocation() => new Location("Location Name", address: null, new Dictionary<IProduct, int>(), id: 1);

            // assert
            Assert.Throws<ArgumentNullException>(CreateLocation);
        }

        [Fact]
        public void Location_NullInventory_Fail()
        {
            // arrange

            // act
            static ILocation CreateLocation() => new Location("Location Name", address, inventory: null, id: 1);

            // assert
            Assert.Throws<ArgumentNullException>(CreateLocation);
        }

        [Fact]
        public void Location_WhiteSpaceName_Fail()
        {
            // arrange

            // act
            static ILocation CreateLocation() => new Location("    ", address, new Dictionary<IProduct, int>(), id: 1);

            // assert
            Assert.Throws<ArgumentException>(CreateLocation);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-4)]
        public void Location_IdLessThanEquals0_Fail(int idValue)
        {
            // arrange
            int value = idValue;

            // act
            ILocation CreateLocation() => new Location("Location Name", address, new Dictionary<IProduct, int>(), id: value);

            // assert
            Assert.Throws<ArgumentException>(CreateLocation);
        }

        [Fact]
        public void Location_IsProductAvailable_Success()
        {
            // arrange
            ILocation location = new Location("Location Name", address, new Dictionary<IProduct, int>() { { apple, 3 } }, id: 1);

            // act
            bool isAvailable = location.IsProductAvailable(apple, 3);

            // assert
            Assert.True(isAvailable);
        }

        [Fact]
        public void Location_IsProductAvailable_TooFew_Fail()
        {
            // arrange
            ILocation location = new Location("Location Name", address, new Dictionary<IProduct, int>() { { apple, 3 } }, id: 1);

            // act
            bool isAvailable = location.IsProductAvailable(apple, 4);

            // assert
            Assert.False(isAvailable);
        }

        [Fact]
        public void Location_IsProductAvailable_ProductDoesNotExist_Fail()
        {
            // arrange
            ILocation location = new Location("Location Name", address, new Dictionary<IProduct, int>(), id: 1);

            // act
            bool isAvailable = location.IsProductAvailable(apple, 3);

            // assert
            Assert.False(isAvailable);
        }
    }
}