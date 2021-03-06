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
    /// <summary>
    /// An order template to be placed and submitted to a server to be processed.
    /// </summary>
    public class OrderTemplate : IOrderTemplate, IValidatableObject
    {
        /// <summary>
        /// The customer Id that the order is for.
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int CustomerId { get; init; }

        /// <summary>
        /// The store location Id that the order is from.
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int StoreLocationId { get; init; }

        /// <summary>
        /// The orderlines inside of the order.
        /// </summary>
        [Required]
        [MinLength(1)]
        public List<OrderLineTemplate> OrderLines { get; init; }

        /// <summary>
        /// Validates whether the order template is valid to be sent to a server.
        /// </summary>
        /// <param name="validationContext">The context for validation.</param>
        /// <returns>Returns the validation results.</returns>
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

                int numOfInvalidQuantities = OrderLines.Count(l => l.Quantity <= 0);
                if (numOfInvalidQuantities > 0)
                    result.Add(new ValidationResult("No orderline may contain a quantity of zero of below."));
            }

            return result;
        }
    }
}