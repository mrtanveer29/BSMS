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
    
    public partial class contact
    {
        public int contact_id { get; set; }
        public string contact_name { get; set; }
        public string designation { get; set; }
        public string department { get; set; }
        public string phone { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public Nullable<int> source_id { get; set; }
        public string source_type { get; set; }
    }
}