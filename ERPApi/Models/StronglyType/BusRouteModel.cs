using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPApi.Models.StronglyType
{
    public class BusRouteModel
    {
        public int user_id { get; set; }
        public int company_id { get; set; }
        public int route_id { get; set; }
        public string route_name { get; set; }
        public string city_id_input { get; set; }
        public int city_id { get; set; }
        public string property_list_string { get; set; }
        public string down_route_list_string { get; set; }
    }
}