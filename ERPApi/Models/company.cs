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
    
    public partial class company
    {
        public int company_id { get; set; }
        public string company_name { get; set; }
        public string company_code { get; set; }
        public Nullable<bool> is_active { get; set; }
        public Nullable<int> updated_by { get; set; }
        public string updated_date { get; set; }
        public string logo_path { get; set; }
        public Nullable<bool> is_parent_company { get; set; }
        public string flag_path { get; set; }
        public Nullable<int> address_id { get; set; }
        public Nullable<int> currency_id { get; set; }
    }
}
