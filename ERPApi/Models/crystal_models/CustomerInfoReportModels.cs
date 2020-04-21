using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPApi.Models.crystal_models
{
    public class CustomerInfoReportModels
    {



        public int customer_id { get; set; }
        public string customer_name { get; set; }
        public string customer_code { get; set; }
        public string cus_mobile { get; set; }
        public string cus_email { get; set; }
        public string cus_phone { get; set; }
        public Nullable<int> city_id { get; set; }
        public Nullable<int> country_id { get; set; }
        public string Cus_city_name { get; set; }
        public string Cus_country_name { get; set; }
        public string fax { get; set; }
        public string address_1 { get; set; }
        public string address_2 { get; set; }
        public string zip_code { get; set; }
        public string web { get; set; }
        public Nullable<bool> is_active { get; set; }
        public Nullable<int> payment_terms { get; set; }
        public Nullable<decimal> credit_limit { get; set; }
        public Nullable<int> payment_method { get; set; }
        public Nullable<int> chart_of_account { get; set; }
        public Nullable<int> businees_posting_group { get; set; }
        public Nullable<int> bill_to_customer { get; set; }
        public Nullable<decimal> total_debit { get; set; }
        public Nullable<decimal> total_credit { get; set; }
        public Nullable<decimal> balance { get; set; }
        public Nullable<int> sales_person { get; set; }
        public Nullable<int> invoicing_type { get; set; }
        public Nullable<int> create_by { get; set; }
        public Nullable<System.DateTime> create_date { get; set; }
        public Nullable<int> update_by { get; set; }
        public Nullable<System.DateTime> update_date { get; set; }
        public string order_code { get; set; }

        //customer_shipping_address
        public int? shipping_address_id { get; set; }

        public string shipping_country_id { get; set; }
        public string shipping_city_id { get; set; }
        public string shipping_zip_code { get; set; }
        public string shipping_address_1 { get; set; }
        public string shipping_address_2 { get; set; }
        public string shipping_email { get; set; }
        public string shipping_phone { get; set; }
        public Nullable<int> shipping_source_id { get; set; }
        public string shipping_source_type { get; set; }
        public string shipping_address_type { get; set; }

        //customer_billing_address
        public int? billing_address_id { get; set; }

        public Nullable<int> billing_country_id { get; set; }
        public Nullable<int> billing_city_id { get; set; }
        public string billing_zip_code { get; set; }
        public string billing_address_1 { get; set; }
        public string billing_address_2 { get; set; }
        public string billing_email { get; set; }
        public string billing_phone { get; set; }
        public Nullable<int> billing_source_id { get; set; }
        public string billing_source_type { get; set; }
        public string billing_address_type { get; set; }

        //others
        public string city_name { get; set; }

        public string country_name { get; set; }
        public string billing_city_name { get; set; }
        public string billing_country_name { get; set; }
        public string shipping_city_name { get; set; }
        public string shipping_country_name { get; set; }

           public List<contact> CustomerContacts { get; set; }
        public List<bank> CustomerBanks { get; set; }
        //For Customer Contact
        //public int contact_id { get; set; }
        //public string contact_name { get; set; }
        //public string designation { get; set; }
        //public string department { get; set; }
        //public string phone { get; set; }
        //public string mobile { get; set; }
        //public string email { get; set; }
        // cus_ added for Reducing duplicacy !! kiron
        public Nullable<int> cus_source_id { get; set; }
        public string cus_source_type { get; set; }

        ////For Bank
        //public int bank_id { get; set; }
        //public string bank_name { get; set; }
        //public string bank_acc_no { get; set; }
        //public string bank_acc_id { get; set; }
        //public string bank_branch_name { get; set; }
        //public Nullable<int> source_id { get; set; }
        //public string source_type { get; set; }
        //public string swift_code { get; set; }

        public string factory_location { get; set; }
        public string major_product { get; set; }
        public string major_brand { get; set; }
        public string total_capacity { get; set; }
        public string total_employment { get; set; }
        public string sister_concerns { get; set; }
        public string special_instruction { get; set; }
        public string client_history { get; set; }
        public int supplier_id { get; set; }

    }
}