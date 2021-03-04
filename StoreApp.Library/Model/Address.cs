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
            if (string.IsNullOrWhiteSpace(address1))
                throw new System.ArgumentNullException(nameof(address1));

            // Address 2 is optional

            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentNullException(nameof(city));
            if (string.IsNullOrWhiteSpace(state))
                throw new ArgumentNullException(nameof(state));

            // Country isn't required in the United States

            if (zipCode == default)
                throw new ArgumentNullException(paramName: nameof(zipCode), message: $"Zip Code cannot be {default(int)}.");

            Address1 = address1;
            Address2 = address2;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;
        }
    }
}