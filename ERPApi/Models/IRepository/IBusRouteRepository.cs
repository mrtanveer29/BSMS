using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPApi.Models.StronglyType;

namespace ERPApi.Models.IRepository
{
    public interface IBusRouteRepository
    {
        object Post(BusRouteModel value);
        object GetRouteDetails(int routeId);
        object GetRoutewiseFair(int routeId, string direction);
        object SaveFare(BusFareModel value);
        object GetAllRoutes(int companyId);
        object Put(BusRouteModel value);
        object GetRoutewiseArea();
        object ChangeStatus(int routeId);
        object GetAllAssignedCounterMan(int counterId, string direction);
        object RouteWisEmployeeMapping(List<counter_employee_mapping> value);

        object GetCounter(int routeId, string direction);
    }
}
