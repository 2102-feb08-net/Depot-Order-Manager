using System.ComponentModel.DataAnnotations;

namespace StoreApp.Web.Model
{
    public class LocationHead : ILocationHead
    {
        /// <summary>
        /// The ID of the location
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int Id { get; init; }

        /// <summary>
        /// The display name of the location
        /// </summary>
        [Required]
        public string Name { get; init; }

        /// <summary>
        /// The formatted address of the location
        /// </summary>
        [Required]
        [MinLength(2)]
        public string[] AddressLines { get; init; }
    }
}