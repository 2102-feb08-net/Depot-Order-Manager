using StoreApp.Library.Model;

namespace StoreApp.Web.Model
{
    public interface IOrderLine
    {
        IProduct Product { get; set; }
        decimal LineTotalPrice { get; set; }
        int Quantity { get; set; }
    }
}