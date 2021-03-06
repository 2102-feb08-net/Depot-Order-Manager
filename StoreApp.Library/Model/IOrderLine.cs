namespace StoreApp.Library.Model
{
    /// <summary>
    /// An orderline inside of an order.
    /// </summary>
    public interface IOrderLine
    {
        /// <summary>
        /// The product in the oderline.
        /// </summary>
        IProduct Product { get; }

        /// <summary>
        /// The quantity of the product in the orderline.
        /// </summary>
        int Quantity { get; }

        /// <summary>
        /// The total price of the orderline.
        /// </summary>
        decimal LineTotalPrice { get; }
    }
}