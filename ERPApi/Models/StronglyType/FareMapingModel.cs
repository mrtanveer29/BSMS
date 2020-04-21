using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPApi.Models.StronglyType
{
    public class FareMapingModel
    {
        public int from_area_id { get; set; }
        public string from_area_name{ get; set; }
        public int to_area_id { get; set; }
        public int fare { get; set; }
        public string direction { get; set; }
        public string to_area_name { get; set; }

    }
}