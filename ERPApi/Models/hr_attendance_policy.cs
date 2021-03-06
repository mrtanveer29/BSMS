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
    
    public partial class hr_attendance_policy
    {
        public int attendance_policy_id { get; set; }
        public string attendance_policy_title { get; set; }
        public string week_start_day { get; set; }
        public string week_end_day { get; set; }
        public string weekend_one { get; set; }
        public string weekend_two { get; set; }
        public string office_start_time { get; set; }
        public string office_end_time { get; set; }
        public string working_hours { get; set; }
        public Nullable<int> shift_id { get; set; }
        public string half_day { get; set; }
        public string half_day_office_start_time { get; set; }
        public string half_day_office_end_time { get; set; }
        public string half_day_office_hours { get; set; }
        public Nullable<bool> is_late_policy_applicable { get; set; }
        public Nullable<bool> is_late_cut_applicable { get; set; }
        public string allowed_buffer_minutes_for_late { get; set; }
        public string allowed_maximum_late_days { get; set; }
        public string late_cut_percentage { get; set; }
        public Nullable<bool> is_attn_bonus_applicable { get; set; }
        public Nullable<bool> is_ot_applicable { get; set; }
        public string ot_persentage { get; set; }
        public Nullable<int> company_id { get; set; }
        public Nullable<int> department_id { get; set; }
        public Nullable<bool> is_night_shift { get; set; }
    }
}
