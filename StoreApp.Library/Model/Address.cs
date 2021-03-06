using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Library.Model
{
    public class Address : IAddress
    {
        /// <summary>
        /// Primary address line (eg. steet number)
        /// </summary>
        public string Address1 { get; set; }

        /// <summary>
        /// The second address line (e.g. apartment number)
        /// </summary>
        public string Address2 { get; set; }

        /// <summary>
        /// The city the address resides in.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// The state or provice the address resides in.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// The country the address resides in.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// The ZIP code of the address.
        /// </summary>
        public int ZipCode { get; set; }

        /// <summary>
        /// Converts the address fields into a single multi-line formatted adddress.
        /// </summary>
        /// <returns>Returns the formatted address.</returns>
        public string[] FormatToMultiline(bool escapeNewLines = false)
        {
            List<string> addressLines = new List<string>
            {
                Address1
            };

            if (!string.IsNullOrWhiteSpace(Address2))
                addressLines.Add(Address2);

            addressLines.Add($"{City}, {State} {ZipCode}");

            if (!string.IsNullOrEmpty(Country))
                addressLines.Add(Country);

            return addressLines.ToArray();
        }

        public Address(string address1, string address2, string city, string state, string country, int zipCode)
        {
            if (address1 is null)
                throw new ArgumentNullException(nameof(address1));
            if (string.IsNullOrWhiteSpace(address1))
                throw new ArgumentException(message: "Address 1 cannot be empty.", nameof(address1));

            // Address 2 is optional

            if (city is null)
                throw new ArgumentNullException(nameof(city));
            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentException(message: "City cannot be empty.", nameof(city));

            if (state is null)
                throw new ArgumentNullException(nameof(state));
            if (string.IsNullOrWhiteSpace(state))
                throw new ArgumentException(message: "State cannot be empty.", nameof(state));

            // Country isn't required in the United States

            if (zipCode == default)
                throw new ArgumentException(message: $"Zip Code cannot be {default(int)}.", paramName: nameof(zipCode));

            Address1 = address1.Trim();
            Address2 = address2?.Trim();
            City = city.Trim();
            State = state.Trim();
            Country = country?.Trim();
            ZipCode = zipCode;
        }
    }
}