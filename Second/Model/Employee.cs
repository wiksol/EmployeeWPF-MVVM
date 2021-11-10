using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Second
{
    public class Employee : Base
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }

        public bool IsChecked { get; set; }

        public int NumberOfUnfinishedTasks { get; set; }
    }
}
