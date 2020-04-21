using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPApi.Models.StronglyType
{
    public class BusReportModel
    {
        public int trip_master_id { get; set; }
        public int? bus_id { get; set; }
        public string bus_registration_no { get; set; }
        public int? trip_count { get; set; }
        public string date { get; set; }
        public int? from_area { get; set; }
        public int? to_area { get; set; }
        public int? no_of_passenger { get; set; }
        public Nullable<int> fare { get; set; }
    }
}