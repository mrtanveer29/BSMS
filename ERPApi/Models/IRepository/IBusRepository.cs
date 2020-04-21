using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPApi.Models.StronglyType;

namespace ERPApi.Models.IRepository
{
   public interface IBusRepository
    {
       object GetAllBusType(int companyId);
       object SaveBusType(bus_type value);
       object UpdateBusType(bus_type value);
       object DeleteBusType(bus_type value);
       object Post(bus value);
       object GetAllBus(int companyId);
       object Put(bus value);
       object Delete(bus value);
       object GetAllBus(int companyId, int routeId);
       object RouteWisBusMapping(BusRouteMappingModel value);
    }
}
