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
    public class OrderTemplate : IOrderTemplate, IValidatableObject
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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> result = new List<ValidationResult>();

            if (OrderLines == null || OrderLines.Count < 1)
                result.Add(new ValidationResult("An order must contain at least 1 order line."));

            if (OrderLines != null)
            {
                bool allProductsAreUnique = OrderLines.Select(l => l.ProductId).Distinct().Count() == OrderLines.Count;
                if (!allProductsAreUnique)
                    result.Add(new ValidationResult("An order must not contain duplicate products in order lines."));

                int numOfInvalidQuantities = OrderLines.Where(l => l.Quantity <= 0).Count();
                if (numOfInvalidQuantities > 0)
                    result.Add(new ValidationResult("No orderline may contain a quantity of zero of below."));
            }

            return result;
        }
    }
}