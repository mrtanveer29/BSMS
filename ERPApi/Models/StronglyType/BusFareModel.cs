using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPApi.Models.StronglyType
{
    public class BusFareModel
    {
        public int route_id { get; set; }
        public List<bus_fare> fare_list { get; set; }
        public List<bus_fare> down_fare { get; set; }
    
    }
}