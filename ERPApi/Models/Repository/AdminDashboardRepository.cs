using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ERPApi.Models.IRepository;

namespace ERPApi.Models.Repository
{
    
    public class AdminDashboardRepository :IAdminDashboardRepository
    {
        private ERPEntities _entities;

        public AdminDashboardRepository()
        {
            _entities=new  ERPEntities();
        }

        public object GetAllAdminDashboardData(int companyId)
        {
            var bus = _entities.buses.Count(x=>x.company_id==companyId);
            var routes = _entities.bus_route.Count(x => x.company_id == companyId);
            var employees = _entities.employees.Count(x => x.company_id == companyId);

            var data =new
            {
                total_bus=bus,
                total_employees=employees,
                total_routes = routes
            };
            return data;
        }
    }
}