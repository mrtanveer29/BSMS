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
    
    public partial class user
    {
        public int user_id { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }
        public Nullable<int> role_id { get; set; }
        public Nullable<int> employee_id { get; set; }
        public Nullable<int> role_type_id { get; set; }
        public Nullable<int> company_id { get; set; }
        public string confirm_password { get; set; }
        public Nullable<int> customer_id { get; set; }
        public string user_firstname { get; set; }
        public string user_lastname { get; set; }
        public string signature { get; set; }
    }
}
