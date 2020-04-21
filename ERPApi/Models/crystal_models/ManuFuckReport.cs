using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPApi.Models.crystal_models
{
    public class ManuFuckReport
    {
        

        public string manufacturing_order_number { get; set; }
        public string order_date { get; set; }
        public string product_name { get; set; }
        public string order_code { get; set; }
        public decimal? quantity { get; set; }
        public string schedule_date { get; set; }
        public string mo_order_status { get; set; }
        public string emp_firstname { get; set; }
        public string emp_lastname { get; set; }
        public string branch_name { get; set; }
        public string branch_code { get; set; }
        public string proforma_invoice_code { get; set; }
        public decimal? manufacture_order_quantity { get; set; }
        public string company_name { get; set; }
        public string company_code { get; set; }
        public string customer_name { get; set; }
        public string customer_code { get; set; }

        public string size { get; set; }


        public string requested_delivery_date { get; set; }
        public Nullable<decimal> manufacturing_price { get; set; }
        public Nullable<decimal> cutting_price { get; set; }
        public Nullable<decimal> slitting_price { get; set; }
        public string customer_ref_no { get; set; }

      


    }
}