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
    
    public partial class sts_approval_chain
    {
        public int ap_chain_id { get; set; }
        public Nullable<int> role_id { get; set; }
        public Nullable<int> sequence_id { get; set; }
        public Nullable<int> route_id { get; set; }
        public string status { get; set; }
        public string is_optional { get; set; }
        public Nullable<int> on_reject_sequence { get; set; }
        public Nullable<int> company_id { get; set; }
    }
}
