using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.DataAccess
{
    public partial class Address
    {
        internal Library.Model.IAddress ConvertAddress() => new Library.Model.Address(Address1, Address2, City, State, Country, ZipCode);
    }
}