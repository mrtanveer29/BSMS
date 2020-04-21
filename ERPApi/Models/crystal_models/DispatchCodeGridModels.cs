using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPApi.Models.crystal_models
{
    public class DispatchCodeGridModels
    {


        public string dispatch_code { get; set; }
        public string proforma_invoice_code { get; set; }
        public decimal? order_quantity { get; set; }
        public decimal? delivery_quantity { get; set; }

        public string dispatch_date { get; set; }



    }
}