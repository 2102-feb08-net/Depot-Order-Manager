﻿using System;
using System.Collections.Generic;

#nullable disable

namespace StoreApp.DataAccess
{
    public partial class Address
    {
        public int Id { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int ZipCode { get; set; }

        public virtual StoreLocation StoreLocation { get; set; }
    }
}