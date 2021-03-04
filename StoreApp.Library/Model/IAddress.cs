namespace StoreApp.Library.Model
{
    public interface IAddress
    {
        string Address1 { get; set; }
        string Address2 { get; set; }
        string City { get; set; }
        string Country { get; set; }
        string State { get; set; }
        int ZipCode { get; set; }

        string[] FormatToMultiline(bool escapeNewLines = false);
    }
}