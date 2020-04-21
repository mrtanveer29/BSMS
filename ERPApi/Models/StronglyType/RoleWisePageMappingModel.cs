using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPApi.Models.StronglyType
{
    public class RoleWisePageMappingModel
    {
        //public int role_action_id { get; set; }
        public Nullable<int> role_id { get; set; }
        //public Nullable<int> page_id { get; set; }
        //public Nullable<int> process_id { get; set; }
        //public string status { get; set; }
        //public string is_allowed { get; set; }

        public List<string> pages { get; set; }
        public List<string> pagebutton { get; set; }
    }
}