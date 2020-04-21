using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPApi.Models.StronglyType;

namespace ERPApi.Models.IRepository
{
   public interface ITripRepository
    {
       object Post(TripModel trip);
       object GetBusUpdates(int routeId, string direction, int counterPosition);
       object GetdailySalesUpdates(int companyId, string date);
    }
}
