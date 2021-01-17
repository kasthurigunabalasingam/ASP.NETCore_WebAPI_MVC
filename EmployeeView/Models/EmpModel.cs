using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeView.Models
{
    public class EmpModel
    {
      public List<EmployeeInfo> EmpInfoList { get; set; }
        public List<Department> DepList { get; set; }
        public List<SubDepartment> SubList { get; set; }
    }

    public class Department
    {
        public int Dep_Id { get; set; }
        public string Dep_Name { get; set; }
    }

    public class SubDepartment
    {
        public int SubDep_Id { get; set; }
        public string SubDep_Name { get; set; }
    }
}
