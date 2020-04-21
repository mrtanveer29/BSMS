using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPApi.Models.crystal_models
{
    public class CustomerDeliveryChallanReportModel
    {
        public int? company_id { get; set; }
        public int? customer_id { get; set; }
        public string customer_name { get; set; }
        public string customer_address { get; set; }
        public string company_name { get; set; }
        public int? product_id { get; set; }
        public string product_name { get; set; }
        public decimal? warehouse_qty { get; set; }
        public decimal? delivery_qty { get; set; }
        public decimal? due_quantity { get; set; }
        public string delivery_date { get; set; }
        public string bank_name { get; set; }
        public string bank_branch_name { get; set; }
        public int del_jour_serial { get; set; }

        public string order_code { get; set; }
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
        public string customer_ref_no { get; set; }
        public string erp_number { get; set; }
        public string customer_deliver_code { get; set; }
        public string hs_code { get; set; }
        public string style_no { get; set; }
        public string size { get; set; }
        public string color { get; set; }
        public string customer_po { get; set; }
        public string carton { get; set; }
        public string net_weight { get; set; }
        public string actual_weight { get; set; }
        public string transportation { get; set; }
        public string transport_to { get; set; }
        public string transport_from { get; set; }
        public string quote { get; set; }
        public string retail_price { get; set; }

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

        public int delivery_master_info_id { get; set; }
        public int delivery_details_info_id { get; set; }
        public int delivery_journal_id { get; set; }

        public string file_code { get; set; }
    }
}