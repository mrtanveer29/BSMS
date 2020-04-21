using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPApi.Models.crystal_models
{
    public class ProformaInvoiceReportModel
    {
        public int? company_id { get; set; }
        public int? customer_id { get; set; }
        public int? proforma_invoice_details_id { get; set; }
        public string customer_name { get; set; }
        public string customer_address { get; set; }
        public string company_name { get; set; }
        public int? product_id { get; set; }
        public string product_name { get; set; }
        public decimal? quantity { get; set; }
        public decimal? unit_price { get; set; }
        public decimal? line_total { get; set; }
        public string currency { get; set; }
        public string bank_name { get; set; }
        public string bank_branch_name { get; set; }
        public string bank_acc_no{ get; set; }

        public string swift_code { get; set; }
        public string hs_code_name { get; set; }
        public string product_category_name { get; set; }
        public string product_category_code { get; set; }

        //public string proforma_invoice_no { get; set; }
        public string proforma_invoice_date { get; set; }
        public string proforma_invoice_code { get; set; }
        public string com_address_1 { get; set; }
        public string com_address_2 { get; set; }
        public string com_phone { get; set; }
        public string com_email { get; set; }
        public string com_fax { get; set; }
        public string validity { get; set; }
        public string uom_name { get; set; }
        public string order_no { get; set; }
        public string payment_method_name { get; set; }
        public string payment_terms_name { get; set; }
        public decimal? discount_percentage { get; set; }
        public decimal? discount_amount { get; set; }
        public decimal? adjustment_amount { get; set; }
        public string hs_code { get; set; }
        public string style_no { get; set; }
        public string size { get; set; }
        public string color { get; set; }
        public string customer_po { get; set; }
        public string item_measurement { get; set; }

        public string cus_del_address_1 { get; set; }
        public string cus_del_address_2 { get; set; }
        public string cus_del_phone { get; set; }
        public string cus_del_email { get; set; }
        public string cus_del_fax { get; set; }
        public string cus_del_web { get; set; }
        public string cus_del_zip_code { get; set; }

        public string cus_address_1 { get; set; }
        public string cus_address_2 { get; set; }
        public string cus_phone { get; set; }
        public string cus_email { get; set; }
        public string cus_fax { get; set; }
        public string cus_web { get; set; }
        public string cus_zip_code { get; set; }


        public string bin_number { get; set; }

        public string file_code { get; set; }
    }
}