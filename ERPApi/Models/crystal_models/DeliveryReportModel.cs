using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPApi.Models.crystal_models
{
    public class DeliveryReportModel
    {
        public int dispatch_id { get; set; }
        public int product_id { get; set; }
        public string order_code { get; set; }
        public string product_name { get; set; }
        public string proforma_invoice_code { get; set; }
        public decimal order_quantity { get; set; }
        public decimal delivery_quantity { get; set; }
        public decimal duo_quantity { get; set; }
        public string status { get; set; }
        public string size { get; set; }
        public string product_category_id { get; set; }
        public string product_category_name { get; set; }
        public int customer_id { get; set; }
        public string customer_name { get; set; }
        public string shipping_address_1 { get; set; }
        public string shipping_address_2 { get; set; }
        public string billing_address_1 { get; set; }
        public string billing_address_2 { get; set; }
        public string delivery_address_1 { get; set; }
        public string delivery_address_2 { get; set; }

    }
}