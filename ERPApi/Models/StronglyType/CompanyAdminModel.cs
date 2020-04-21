using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPApi.Models.StronglyType
{
    public class CompanyAdminModel
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string admin_mobile { get; set; }
        public string admin_phone { get; set; }
        public string admin_address_1 { get; set; }
        public string admin_address_2 { get; set; }
        public string admin_zip_code { get; set; }
        public string admin_fax { get; set; }
        public string admin_email { get; set; }
        public string admin_web { get; set; }
        public string sex { get; set; }
        public string dob { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }
    }
}