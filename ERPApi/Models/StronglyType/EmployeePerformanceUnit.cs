using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPApi.Models.StronglyType
{
    public class EmployeePerformanceUnit
    {
        public string emp_image_file_name { get; set; }
        public int emp_id { get; set; }
        public int user_id { get; set; }
        public string emp_firstname { get; set; }
        public string emp_lastname { get; set; }
        public decimal  amount {get; set; }
        public long  sales {get; set; }
 
    }
}