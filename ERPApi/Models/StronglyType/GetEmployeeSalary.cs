using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPApi.Models.StonglyType
{
    public class GetEmployeeSalary
    {
        public string psh_title { get; set; }
        public int emp_salary_id { get; set; }
        public Nullable<int> emp_salary_info_id { get; set; }
        public string salary_info { get; set; }
        public Nullable<decimal> salary_ammount { get; set; }
        public Nullable<int> emp_id { get; set; }
    }
}