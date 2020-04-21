using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPApi.Models.StronglyType
{
    public class DeliveryChallanRptModel
    {

        public string proforma_invoice_no { get; set; }
        public string proforma_invoice_code { get; set; }
        public DateTime? proforma_invoice_date { get; set; }
        public string advising_bank { get; set; }
        public string customer_ref_no { get; set; }
        public string company_name { get; set; }
        public string company_address { get; set; }
        public string customer_name { get; set; }
        public string customer_address { get; set; }
        public string remarks { get; set; }
        public DateTime? l_c_date { get; set; }
        public string l_c_note { get; set; }
        public string products { get; set; }
        public decimal quantity { get; set; }
        public decimal unit_price { get; set; }

        public string total_of_each_product { get; set; }
        public DateTime? delivery_date { get; set; }
        public string carrier { get; set; }
        public DateTime? carrier_date { get; set; }

    }
}