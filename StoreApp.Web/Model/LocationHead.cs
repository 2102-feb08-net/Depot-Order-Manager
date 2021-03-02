﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApp.Web.Model
{
    public class LocationHead
    {
        /// <summary>
        /// The display name of the location
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// The formatted address of the location
        /// </summary>
        public string Address { get; init; }

        /// <summary>
        /// The ID of the location
        /// </summary>
        public int Id { get; init; }
    }
}