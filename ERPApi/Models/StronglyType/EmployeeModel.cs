using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPApi.Models.StronglyType
{
    public class EmployeeModel:hr_emp_job_details
    {

        public string emp_code { get; set; }
        public string emp_firstname { get; set; }
        public string emp_lastname { get; set; }
        public string employee_name { get; set; }
        public string employee_email { get; set; }
        public string emp_info { get; set; }
        public string user_name { get; set; }
        public int user_id { get; set; }
        public string password { get; set; }
        public string confirm_password { get; set; }
        public string emp_name { get; set; }
        public string employee_role { get; set; }
        public int employee_id { get; set; }
       
        public int? owner_id { get; set; }
        public int? role_type_id { get; set; }
        public string supplier_name { get; set; }
        public string customer_name { get; set; }
        public int? role_id { get; set; }
        public string emp_code_name { get; set; }


        public string emp_marital_status { get; set; }
        public string emp_blood_group { get; set; }
        public string emp_gender { get; set; }
        public string emp_dateofbirth { get; set; }
        public string emp_image_file_name { get; set; }
        public string emp_id_type { get; set; }
        public string emp_id_no { get; set; }
        public string emp_dateofjoin { get; set; }

    }
}