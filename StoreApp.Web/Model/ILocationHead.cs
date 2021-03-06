using System.ComponentModel.DataAnnotations;

namespace StoreApp.Web.Model
{
    public interface ILocationHead
    {
        /// <summary>
        /// The Id of the location.
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        int Id { get; init; }

        /// <summary>
        /// The name of the location.
        /// </summary>
        [Required]
        string Name { get; init; }

        /// <summary>
        /// The lines of a formatted address.
        /// </summary>
        [Required]
        [MinLength(2)]
        string[] AddressLines { get; init; }
    }
}