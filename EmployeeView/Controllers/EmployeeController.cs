using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmployeeView.Models;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeView.Controllers
{
    public class EmployeeController : Controller
    {
        public async Task<IActionResult> Index(string searchString, string DepList,string SubDepList)
        {
            List<EmployeeInfo> EmployeeInfoList = new List<EmployeeInfo>();
          
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44369/api/employee?Name="+  searchString + "&DepartmentId="+ DepList+ "&SubDepartmentId="+SubDepList))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    EmployeeInfoList = JsonConvert.DeserializeObject<List<EmployeeInfo>>(apiResponse);
                }
            }
            List<Department> depList = new List<Department>();
            List<SubDepartment> subDepList = new List<SubDepartment>();

            foreach (EmployeeInfo item in EmployeeInfoList)
            {
                Department depObj = new Department();
                depObj.Dep_Id = item.DepartmentId;
                depObj.Dep_Name = item.DepartmentName;

                SubDepartment subdepObj = new SubDepartment();
                subdepObj.SubDep_Id = item.SubDepartmentId;
                subdepObj.SubDep_Name = item.SubDepartmentName;             
                 depList.Add(depObj);
                
                subDepList.Add(subdepObj);
            }
            var distictDeps = depList.GroupBy(x => x.Dep_Id).Select(y => y.First());
            var subDistictDeps = subDepList.GroupBy(x => x.SubDep_Id).Select(y => y.First());

            var depSelectList = from Department t in distictDeps
                                select new SelectListItem { Value = t.Dep_Id.ToString(), Text = t.Dep_Name };
            var subDepSelectList = from SubDepartment t in subDistictDeps
                                   select new SelectListItem { Value = t.SubDep_Id.ToString(), Text = t.SubDep_Name };
            ViewData["DepList"] = depSelectList;
            ViewData["SubDepList"] = subDepSelectList;


            return View(EmployeeInfoList);
        }    
    }
}
