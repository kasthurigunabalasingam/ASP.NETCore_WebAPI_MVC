using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmployeeServices.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace EmployeeServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly string _connectionString;
        public EmployeeController(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }
        [HttpGet]
        public List<EmployeeInfo> Get([FromQuery] FilterParam param)
        {
            //List<EmployeeInfo> emp1 = new List<EmployeeInfo>();
            DataTable dt = GetData("dbo.spGetEmployeeInfo", param);
            List<EmployeeInfo> empInfo = dt.AsEnumerable().Select(row =>
            new EmployeeInfo
            {
                FirstName = row.Field<string>("FirstName"),
                LastName = row.Field<string>("LastName"),
                Bio = row.Field<string>("Bio"),
                ProfileImage = row.Field<string>("ProfileImage"),
                FBProfileLink = row.Field<string>("FBProfileLink"),
                TwitterProfileLink = row.Field<string>("TwitterProfileLink"),
                SubDepartmentId= row.Field<int>("SubDepartmentID"),
                SubDepartmentName = row.Field<string>("SubDepartmentName"),
                DepartmentId = row.Field<int>("DepartmentID"),
                DepartmentName = row.Field<string>("DepartmentName"),
                
            }).ToList();
            return empInfo;
        }

        public DataTable GetData(string str, FilterParam param)
        {
            DataTable objresutl = new DataTable();
            try
            {
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(_connectionString))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(str, myCon))
                    {
                        myCommand.CommandType = CommandType.StoredProcedure;
                        if (param.Name != null)
                        {
                            myCommand.Parameters.Add("@pFirstName", SqlDbType.VarChar).Value = param.Name;
                        }
                        if (param.DepartmentId != null)
                        {
                            myCommand.Parameters.Add("@pDepartmentId", SqlDbType.Int).Value = param.DepartmentId;
                        }
                        if (param.SubDepartmentId != null)
                        {
                            myCommand.Parameters.Add("@pSubDepartmentId", SqlDbType.Int).Value = param.SubDepartmentId;
                        }
                        myReader = myCommand.ExecuteReader();
                        objresutl.Load(myReader);

                        myReader.Close();
                        myCon.Close();
                        return objresutl;
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
