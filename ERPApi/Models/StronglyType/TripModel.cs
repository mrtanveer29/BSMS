using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPApi.Models.StronglyType
{
    public class TripModel
    {
        public TripMasterModel tripMasterData { get; set; }
        public List<trip_details> tripDetailsData { get; set; }
    }
}