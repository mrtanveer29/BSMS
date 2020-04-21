using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPApi.Models.crystal_models
{
    public class CertificateOfOriginReportModel
    {
        public Nullable<int> commercial_invoice_master_id { get; set; }
        public Nullable<int> customer_id { get; set; }
        public Nullable<int> negotiating_bank_id { get; set; }
        public string customer_name { get; set; }
        public string customer_address { get; set; }
        public string invoice_no { get; set; }
        public string negotiating_bank_name { get; set; }
        public Nullable<decimal> amount { get; set; }
        public string delivery_no { get; set; }
        public string com_tin_no { get; set; }
        public string com_irc_no { get; set; }
        public string com_vat_reg_no { get; set; }
        public Nullable<int> payment_deadline { get; set; }
        public string letter_of_credit { get; set; }
        public Nullable<int> ci_serial { get; set; }
        public string commercial_invoice_date { get; set; }
        public string delivery_date { get; set; }
        public string letter_of_credit_date { get; set; }
        public string ci_status { get; set; }
        public Nullable<int> company_id { get; set; }
        public string export_lc { get; set; }
        public string export_lc_date { get; set; }
        public string erp_number { get; set; }
        public Nullable<int> product_id { get; set; }
        public Nullable<int> unit_id { get; set; }
        public Nullable<decimal> unit_price { get; set; }
        public Nullable<decimal> quantity { get; set; }
        public string customer_po { get; set; }
        public string product_name { get; set; }
        public string unit_name { get; set; }
        public string company_name { get; set; }
        public string order_code { get; set; }
        public string com_address_1 { get; set; }
        public string com_address_2 { get; set; }
        public string com_phone { get; set; }
        public string com_email { get; set; }
        public string com_fax { get; set; }
        public string proforma_inovice_code { get; set; }
        public string negotitaingbank_branch_name { get; set; }

        public string product_category_name { get; set; }

        public Nullable<int> production_unit_id { get; set; }
        public string production_unit_name { get; set; }
        public string central_bank_proc_no { get; set; }
        public string bank_address { get; set; }
        public string proforma_invoice_date { get; set; }
        public Nullable<decimal> total_amount { get; set; }
        public string customer_ref_no { get; set; }

        public string com_hs_code { get; set; }
        public string lcaf_no { get; set; }
        public string expire_no { get; set; }
        public string expire_date { get; set; }
        public Nullable<int> product_category_id { get; set; }
        public string production_unit_address { get; set; }
        public string production_unit_logo { get; set; }
        public string imgpaths { get; set; }

        public int? customer_bank_id { get; set; }
    }
}