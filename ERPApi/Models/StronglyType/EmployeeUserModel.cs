using System;

namespace ERPApi.Models.StronglyType
{
    public class EmployeeUserModel
    {
        public int employee_id { get; set; }
        public string employee_name { get; set; }
        public string employee_dob { get; set; }
        public string employee_address { get; set; }
        public Nullable<int> city_id { get; set; }
        public Nullable<int> country_id { get; set; }
        public string employee_phone { get; set; }
        public string employee_notes { get; set; }
        public string employee_photo { get; set; }
        public string employee_password { get; set; }
        public string employee_email { get; set; }
        public Nullable<int> created_by { get; set; }
        public string created_date { get; set; }
        public Nullable<int> updated_by { get; set; }
        public string updated_date { get; set; }
        public Nullable<int> company_id { get; set; }
        public Nullable<bool> is_active { get; set; }
        public string employee_status { get; set; }
        public Nullable<int> employee_role { get; set; }

        public int user_id { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }
        public Nullable<int> role_id { get; set; }
        public Nullable<int> role_type_id { get; set; }
        public Nullable<bool> StatusFlag { get; set; }
    }
}