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
    
    public partial class bus_fare
    {
        public int fair_id { get; set; }
        public Nullable<int> route_id { get; set; }
        public Nullable<int> from_area_id { get; set; }
        public Nullable<int> to_area_id { get; set; }
        public Nullable<int> fare { get; set; }
        public string direction { get; set; }
    }
}
