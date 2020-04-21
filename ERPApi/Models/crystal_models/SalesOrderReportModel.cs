using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPApi.Models.crystal_models
{
    public class SalesOrderReportModel
    {

        public int company_id { get; set; }
        public string company_name { get; set; }
        public int company_code { get; set; }
        public string order_date { get; set; }
        public string sales_order_code { get; set; }
        public int product_id { get; set; }
        public int label_reference_id { get; set; }
        //public string label_reference_name { get; set; }
        public string product_name { get; set; }
        public string customer_po { get; set; }
        public string order_code { get; set; }
        public decimal quantity { get; set; }
        public int uom_id { get; set; }
        public string uom_name { get; set; }
        public decimal sales_price { get; set; }
        public decimal vat_percentage { get; set; }
        public decimal tax_percentage { get; set; }
        public int customer_id { get; set; }
        public string customer_name { get; set; }
        public string customer_address { get; set; }
        public decimal line_total { get; set; }
    }
}