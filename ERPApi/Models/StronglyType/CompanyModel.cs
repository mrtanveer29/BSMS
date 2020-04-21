using System;
namespace ERPApi.Models.StronglyType
{
    public class CompanyModel
    {
        public string address_1 { get; set; }
        public string address_2 { get; set; }
        public int country_id { get; set; }
        public int city_id { get; set; }
        public string zip_code { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string fax { get; set; }
        public string web { get; set; }
        public string mobile { get; set; }
        public string company_name { get; set; }
        public string company_code { get; set; }
        public string is_active { get; set; }
        public string logo_path { get; set; }
        public string flag_path { get; set; }
        public int company_id { get; set; }
        public string is_parent_company { get; set; }
        public string bank_name { get; set; }
        public string bank_acc_no { get; set; }
        public string bank_acc_id { get; set; }
        public string bank_branch_name { get; set; }
        public string swift_code { get; set; }
        public int bank_id { get; set; }
        public int currency_id { get; set; }

        public string emp_firstname { get; set; }

        public string emp_lastname { get; set; }

        public string user_name { get; set; }

        public string password { get; set; }

        public string role_id { get; set; }
    }
}