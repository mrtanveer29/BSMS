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
    
    public partial class experience
    {
        public int experience_id { get; set; }
        public Nullable<int> employee_id { get; set; }
        public string company { get; set; }
        public string job_title { get; set; }
        public string from_date { get; set; }
        public string to_date { get; set; }
        public string responsibilities { get; set; }
    }
}
