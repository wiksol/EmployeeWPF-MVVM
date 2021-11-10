using Second.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Second
{
    public class DatabaseLocator
    {
        public static SecondDbContext Database { get; set; }
    }
}
