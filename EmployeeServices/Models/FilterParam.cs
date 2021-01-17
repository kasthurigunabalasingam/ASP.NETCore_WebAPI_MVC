using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeServices.Models
{
    public class FilterParam
    {
        public string Name { get; set; }
        public string DepartmentId { get; set; }
        public string SubDepartmentId { get; set; }

    }
}
