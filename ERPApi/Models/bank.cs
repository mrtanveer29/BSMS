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
    
    public partial class bank
    {
        public int bank_id { get; set; }
        public string bank_name { get; set; }
        public string bank_acc_no { get; set; }
        public string bank_acc_id { get; set; }
        public string bank_branch_name { get; set; }
        public Nullable<int> source_id { get; set; }
        public string source_type { get; set; }
        public string swift_code { get; set; }
        public string bank_address { get; set; }
    }
}
