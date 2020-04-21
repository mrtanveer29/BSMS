using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPApi.Models
{
    public static class LmsEnum
    {
        public enum Role
        {
            Admin = 53,    /// below data will be automatically indexed... 
            CustomerSales,
            Designer,
            QualityControl,
            PD,
            Planning,
            Production,
            Sales,
            FactoryDesigner
        }
    }
}