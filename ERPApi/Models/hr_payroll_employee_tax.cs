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
    
    public partial class hr_payroll_employee_tax
    {
        public int pet_id { get; set; }
        public Nullable<bool> pet_employee_tax_status { get; set; }
        public string pet_employee_tax_amount { get; set; }
        public Nullable<System.DateTime> pet_created_on { get; set; }
        public string pet_created_by { get; set; }
        public Nullable<System.DateTime> pet_updated_on { get; set; }
        public string pet_updated_by { get; set; }
        public Nullable<int> company_id { get; set; }
        public Nullable<int> pet_employee_id { get; set; }
    }
}