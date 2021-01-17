using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeView.Models
{
    public class EmployeeInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
        public string ProfileImage { get; set; }
        public string FBProfileLink { get; set; }
        public string TwitterProfileLink { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int SubDepartmentId { get; set; }

        public string SubDepartmentName { get; set; }

    }
}
