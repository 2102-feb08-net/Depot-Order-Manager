using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Library
{
    public interface IOrderLineTemplate
    {
        int ProductId { get; }
        int Quantity { get; }
    }
}