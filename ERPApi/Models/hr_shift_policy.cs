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
    
    public partial class hr_shift_policy
    {
        public int shift_id { get; set; }
        public string shift_title { get; set; }
        public Nullable<int> start_day { get; set; }
        public Nullable<int> end_day { get; set; }
        public Nullable<bool> status { get; set; }
        public Nullable<int> company_id { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
    }
}
