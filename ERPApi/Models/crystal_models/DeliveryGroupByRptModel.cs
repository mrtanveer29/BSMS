using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPApi.Models.crystal_models
{
    public class DeliveryGroupByRptModel
    {

        public string company_name { get; set; }
        public string ComAddress { get; set; }
        public string ComAddress2 { get; set; }
        public string zip_code { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string mobile { get; set; }
        public string fax { get; set; }
        public string web { get; set; }
        public string city_name { get; set; }
        public string country_name { get; set; }
        public string logo_path { get; set; }
        public int? dispatch_id { get; set; }
        public string customer_name { get; set; }
        public string address_1 { get; set; }
        public string product_name { get; set; }
        public int? product_id { get; set; }
        public int? product_category_id { get; set; }
        public string product_category_name { get; set; }
        public string order_code { get; set; }
        public string proforma_invoice_code { get; set; }
        public string dispatched_date { get; set; }
        public decimal? order_quantity { get; set; }
        public decimal? delivery_quantity { get; set; }
        public decimal? duo_quantity { get; set; }
        public string status { get; set; }
        public string size { get; set; }
        public string user_name { get; set; }
        public string role_type_name { get; set; }
        public decimal? RemainQuantity { get; set; }

        public string shipping_address { get; set; }
        public string billing_address { get; set; }
        public string delivery_address { get; set; }


    }
}