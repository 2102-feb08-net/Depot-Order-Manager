using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApp.Web
{
    public static class Connection
    {
        // This is only temporary.
        public static string ConnectionString { get; set; }

        public static Action<string> Logger { get; set; }
    }
}