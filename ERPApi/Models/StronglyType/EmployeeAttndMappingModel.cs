using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPApi.Models.StronglyType
{
    public class EmployeeAttndMappingModel
    {
        public Nullable<int> attendance_policy_id { get; set; }
        public List<string> ids { get; set; }
        public int company_id { get; set; }


    }
}