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
    
    public partial class branch
    {
        public int branch_id { get; set; }
        public string branch_code { get; set; }
        public string branch_name { get; set; }
        public Nullable<int> company_id { get; set; }
        public Nullable<bool> is_active { get; set; }
        public string branch_location { get; set; }
        public Nullable<int> updated_by { get; set; }
        public string updated_date { get; set; }
        public Nullable<int> branch_incharge { get; set; }
    }
}
