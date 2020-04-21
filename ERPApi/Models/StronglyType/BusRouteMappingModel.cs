using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPApi.Models.StronglyType
{
    public class BusRouteMappingModel
    {
        public int route_id { get; set; }
        public List<int> buses { get; set; }
    }
}