namespace StoreApp.Library.Model
{
    public interface IAddress
    {
        /// <summary>
        /// Primary address line (eg. steet number)
        /// </summary>
        string Address1 { get; set; }

        /// <summary>
        /// The second address line (e.g. apartment number)
        /// </summary>
        string Address2 { get; set; }

        /// <summary>
        /// The city the address resides in.
        /// </summary>
        string City { get; set; }

        /// <summary>
        /// The state or provice the address resides in.
        /// </summary>
        string Country { get; set; }

        /// <summary>
        /// The country the address resides in.
        /// </summary>
        string State { get; set; }

        /// <summary>
        /// The ZIP code of the address.
        /// </summary>
        int ZipCode { get; set; }

        /// <summary>
        /// Converts the address fields into a single multi-line formatted adddress.
        /// </summary>
        /// <returns>Returns the formatted address.</returns>
        string[] FormatToMultiline(bool escapeNewLines = false);
    }
}