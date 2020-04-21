using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPApi.Models.StronglyType
{
    public class EmployeeCouterMappingModel
    {
        public int route_id { get; set; }
        public string direction { get; set; }
        public List<counter_employee_mapping> fare_list { get; set; }
    }
}