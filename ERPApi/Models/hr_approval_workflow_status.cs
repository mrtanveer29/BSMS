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
    
    public partial class hr_approval_workflow_status
    {
        public int aws_id { get; set; }
        public Nullable<int> aws_department_id { get; set; }
        public string aws_emp_code { get; set; }
        public string aws_sup_emp_code { get; set; }
        public string aws_status { get; set; }
        public Nullable<int> leave_application_master_id { get; set; }
        public string created_at { get; set; }
        public string last_updated_at { get; set; }
        public Nullable<int> created_by { get; set; }
        public Nullable<int> last_updated_by { get; set; }
        public string aws_status_updated_by { get; set; }
        public string aws_status_updated_at { get; set; }
        public string is_reviewed { get; set; }
        public Nullable<int> aws_emp_id { get; set; }
        public Nullable<int> aws_super_id { get; set; }
        public Nullable<int> aws_step { get; set; }
    }
}
