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
    
    public partial class sts_tbldesigndetails
    {
        public long file_detail_id { get; set; }
        public Nullable<long> f_id { get; set; }
        public string file_name { get; set; }
        public Nullable<int> version { get; set; }
        public Nullable<int> status { get; set; }
        public Nullable<long> uploadby { get; set; }
        public string path { get; set; }
        public Nullable<int> is_archieved { get; set; }
        public Nullable<int> isdownloaded { get; set; }
        public string approvedlayout { get; set; }
        public Nullable<long> archievedby { get; set; }
        public Nullable<int> emailby { get; set; }
        public Nullable<short> is_cancelled { get; set; }
        public Nullable<long> cancelby { get; set; }
        public string time_taken { get; set; }
        public string b_status { get; set; }
        public string pass_protected_file_path { get; set; }
        public string cancel_date { get; set; }
        public Nullable<int> is_writeoff { get; set; }
        public Nullable<int> route_id { get; set; }
        public Nullable<int> sequence_id { get; set; }
        public string password { get; set; }
        public Nullable<int> sent_for_msgflag { get; set; }
        public Nullable<int> sent_to { get; set; }
        public string customer_po { get; set; }
        public string erp_number { get; set; }
        public string uploaddate { get; set; }
        public string archievedate { get; set; }
        public Nullable<int> is_rejected { get; set; }
        public Nullable<int> is_jobbag_created { get; set; }
        public string emaildate { get; set; }
        public Nullable<int> company_id { get; set; }
        public Nullable<int> branch_id { get; set; }
        public Nullable<int> for_production { get; set; }
        public Nullable<int> is_forwarded { get; set; }
        public Nullable<int> wkorder_id { get; set; }
        public Nullable<int> downloadby { get; set; }
        public string download_date { get; set; }
        public Nullable<int> download_role_id { get; set; }
        public Nullable<int> reject_by { get; set; }
    }
}
