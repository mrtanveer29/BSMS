using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPApi.Models.StronglyType
{
    public class LoginModel
    {
        public int user_id { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }
        public Nullable<int> role_id { get; set; }
        public Nullable<int> employee_id { get; set; }
        public Nullable<int> role_type_id { get; set; }
        public string employee_email { get; set; }
        public string employee_name { get; set; }
        public Nullable<int> company_id { get; set; }
        public Nullable<int> branch_id { get; set; }
        public string user_role_name { get; set; }
        public Nullable<int> customer_id { get; set; }
    }
}