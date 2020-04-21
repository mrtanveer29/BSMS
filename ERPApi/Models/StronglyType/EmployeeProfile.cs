using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPApi.Models.StronglyType
{
    public class EmployeeProfile
    {
        public int emp_id { get; set; }
        public int user_id { get; set; }
        public string emp_image_file_name { get; set; }
        public string emp_name { get; set; }
       
        public decimal sales { get; set; }
        public decimal proforma_invoice { get; set; }
        public long total_comission { get; set; }
    }
}