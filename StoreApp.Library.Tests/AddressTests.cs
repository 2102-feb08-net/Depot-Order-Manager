using StoreApp.Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StoreApp.Library.Tests
{
    public class AddressTests
    {
        [Fact]
        public void Address_Construct_Success()
        {
            // arrange

            // act
            Address address = new Address("123 Street", "Apt 2", "Newcity", "Newzona", "Country", 12345);

            // assert
            Assert.NotNull(address);
        }

        [Fact]
        public void Address_ConstructNoAddress2_Success()
        {
            // arrange

            // act
            Address address = new Address("123 Street", null, "Newcity", "Newzona", "Country", 12345);

            // assert
            Assert.NotNull(address);
        }

        [Fact]
        public void Address_ConstructNoCountry_Success()
        {
            // arrange

            // act
            Address address = new Address("123 Street", "Apt 2", "Newcity", "Newzona", null, 12345);

            // assert
            Assert.NotNull(address);
        }

        [Fact]
        public void Address_ConstructNoStreet1_Fail()
        {
            // arrange

            // act
            static Address address() => new Address(string.Empty, "Apt 2", "Newcity", "Newzona", "Country", 12345);

            // assert
            Assert.Throws<ArgumentException>(address);
        }

        [Fact]
        public void Address_ConstructNoCity_Fail()
        {
            // arrange

            // act
            static Address address() => new Address("123 Address", "Apt 2", string.Empty, "Newzona", "Country", 12345);

            // assert
            Assert.Throws<ArgumentException>(address);
        }

        [Fact]
        public void Address_ConstructNoState_Fail()
        {
            // arrange

            // act
            static Address address() => new Address("123 Address", "Apt 2", "Newcity", string.Empty, "Country", 12345);

            // assert
            Assert.Throws<ArgumentException>(address);
        }

        [Fact]
        public void Address_ConstructDefaultZip_Fail()
        {
            // arrange

            // act
            static Address address() => new Address("123 Address", "Apt 2", "Newcity", "Newzona", "Country", default);

            // assert
            Assert.Throws<ArgumentException>(address);
        }
    }
}