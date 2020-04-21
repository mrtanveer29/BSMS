using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Cors;
using ERPApi.Models.IRepository;
using ERPApi.Models.StronglyType;

namespace ERPApi.Models.Repository
{
     
    public class BusRepository : IBusRepository
     {
         public ERPEntities _entities;

         public BusRepository()
         {
             _entities = new ERPEntities();
         }

         public object GetAllBusType(int companyId)
         {
             try
             {
                 return _entities.bus_type.Where(x=>x.company_id==companyId);
             }
             catch (Exception e)
             {
                 return e.Message;
             }
         }

         public object SaveBusType(bus_type value)
         {
             try
             {
                 _entities.bus_type.Add(value);
                 _entities.SaveChanges();
                 return true;
             }
             catch (Exception e)
             {
                 return false;
             }
         }
         public object UpdateBusType(bus_type value)
         {
             try
             {
                 bus_type busType = _entities.bus_type.Find(value.bus_type_id);
                 busType.bus_type_name = value.bus_type_name;
                 busType.details = value.details;
                 _entities.SaveChanges();
                 return true;
             }
             catch (Exception e)
             {
                 return false;
             }
         }
         public object DeleteBusType(bus_type value)
         {
             try
             {
                 bus_type busType = _entities.bus_type.Find(value.bus_type_id);
                 _entities.bus_type.Remove(busType);
                 _entities.SaveChanges();
                 return true;
             }
             catch (Exception e)
             {
                 return false;
             }
         }

        public object Post(bus bus)
        {
            try
            {
                bus.created_date = DateTime.Now.ToString("dd-MM-yyyy");
                _entities.buses.Add(bus);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public object GetAllBus(int companyId)
        {
            try
            {
                return (from b in _entities.buses.Where(x => x.company_id == companyId)
                            join e in _entities.employees on b.driver_id equals  e.emp_id into tmpe from e in tmpe.DefaultIfEmpty()
                            join bt in _entities.bus_type on b.bus_type_id equals bt.bus_type_id into tmpbt from bt in tmpbt.DefaultIfEmpty()select new
                            {
                                b.bus_id,
                                b.bus_type_id,
                                b.conductor_id,
                                b.driver_id,
                                b.is_active,
                                b.number_of_seats,
                                b.registration_date,
                                b.registration_expire_date,
                                b.route_id,
                                b.bus_registration_no,
                                b.chassis_no,
                                b.created_date,
                                driver_name=e.emp_firstname+" "+e.emp_lastname,
                                bt.bus_type_name
                            }).OrderByDescending(x=>x.bus_id);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public object Put(bus bus)
        {
            try
            {
                bus busInfo = _entities.buses.Find(bus.bus_id);
                busInfo.bus_registration_no = bus.bus_registration_no;
                busInfo.bus_type_id = bus.bus_type_id;
                busInfo.chassis_no = bus.chassis_no;
                busInfo.conductor_id = bus.conductor_id;
                busInfo.driver_id = bus.driver_id;
                busInfo.is_active = bus.is_active;
                busInfo.number_of_seats = bus.number_of_seats;
                busInfo.registration_date = bus.registration_date;
                busInfo.registration_expire_date = bus.registration_expire_date;
                busInfo.route_id = bus.route_id;
                _entities.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public object Delete(bus bus)
        {
            try
            {
                bus busInfo = _entities.buses.Find(bus.bus_id);
                _entities.buses.Remove(busInfo);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public object GetAllBus(int companyId, int routeId)
        {
            return _entities.buses.Where(x => x.company_id == companyId && x.route_id == routeId);
        }

        public object RouteWisBusMapping(BusRouteMappingModel value)
        {
            var route_id = value.route_id;
            string query = "Update bus set route_id=0 where route_id=" + route_id;
            _entities.Database.ExecuteSqlCommand(query);
            foreach (int bus_id in value.buses)
            {
                bus bus = _entities.buses.FirstOrDefault(x => x.bus_id == bus_id);
                bus.route_id = route_id;
                _entities.SaveChanges();
            }
            return 0;
        }
     }
}