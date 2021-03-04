namespace StoreApp.Web.Model
{
    public interface ILocationHead
    {
        string[] AddressLines { get; init; }
        int Id { get; init; }
        string Name { get; init; }
    }
}