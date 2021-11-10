using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Second.Database
{
    public class Task
    {
        public int TaskId { get; set; }
        public string Description { get; set; }

        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        public int TaskCategoryId { get; set; }
        public virtual TaskCategory TaskCategory { get; set; }

    }
}
