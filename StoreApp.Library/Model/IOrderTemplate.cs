using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Library.Model
{
    public interface IOrderTemplate
    {
        int CustomerId { get; }
        int StoreLocationId { get; }

        List<OrderLineTemplate> OrderLines { get; }
    }
}