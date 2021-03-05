using StoreApp.Library;
using StoreApp.Library.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StoreApp.Library.Model
{
    public class OrderTemplate : IOrderTemplate
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int CustomerId { get; init; }

        [Required]
        [Range(1, int.MaxValue)]
        public int StoreLocationId { get; init; }

        [Required]
        [MinLength(1)]
        public List<OrderLineTemplate> OrderLines { get; init; }
    }
}