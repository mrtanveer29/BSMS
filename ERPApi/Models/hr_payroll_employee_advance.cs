//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ERPApi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class hr_payroll_employee_advance
    {
        public int pea_id { get; set; }
        public string pea_employee_id { get; set; }
        public string pea_advance_amount { get; set; }
        public string total_installment_to_pay { get; set; }
        public string pea_start_from { get; set; }
        public string pea_paid_amount { get; set; }
        public string pea_remain_amount { get; set; }
        public string pea_status { get; set; }
        public string pea_created_on { get; set; }
        public Nullable<int> pea_created_by { get; set; }
        public Nullable<int> company_id { get; set; }
        public Nullable<int> pea_last_installment_no_paid { get; set; }
        public string last_updated_by { get; set; }
        public Nullable<int> last_updated_at { get; set; }
        public Nullable<int> year { get; set; }
        public Nullable<int> month { get; set; }
        public string amount_per_installment { get; set; }
    }
}
