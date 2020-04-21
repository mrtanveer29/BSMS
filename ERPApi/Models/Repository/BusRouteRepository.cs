using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ERPApi.Models.IRepository;
using ERPApi.Models.StronglyType;

namespace ERPApi.Models.Repository
{
    public class BusRouteRepository :IBusRouteRepository
    {
         public ERPEntities _entities;

         public BusRouteRepository()
         {
             _entities = new ERPEntities();
         }
        public object Post(BusRouteModel value)
        {
            try
            {
                bus_route route = new bus_route
                {
                    route_name = value.route_name,
                    company_id = value.company_id,
                    is_active = true
                   // city_id = value.city_id
                };
                _entities.bus_route.Add(route);
                int returnVal = _entities.SaveChanges();
                if (returnVal > 0)
                {
                    string[] routes = value.property_list_string.Split(';');
                    string[] down_routes = value.down_route_list_string.Split(';');
                    int count = 0;
                    foreach (var val in routes)
                    {
                        count++;
                        route_mapping mapping = new route_mapping
                        {
                            route_id = route.route_id,
                            area_id = int.Parse(val),
                            serial_no = count,
                            direction = "Up"
                        };
                        _entities.route_mapping.Add(mapping);
                        _entities.SaveChanges();

                    }
                    count = 0;
                    foreach (var val in down_routes)
                    {
                        count++;
                        route_mapping mapping = new route_mapping
                        {
                            route_id = route.route_id,
                            area_id = int.Parse(val),
                            serial_no = count,
                            direction = "Down"
                        };
                        _entities.route_mapping.Add(mapping);
                        _entities.SaveChanges();

                    }
                }
                return true;
            }
            catch (Exception e)
            {
                return e.Message;
            }
           
           
        }

        public object GetRouteDetails(int routeId)
        {
            var data = (from br in _entities.bus_route.Where(x => x.route_id == routeId)
                        
                join rm in _entities.route_mapping on br.route_id equals rm.route_id into tmp_rm from rm in tmp_rm.DefaultIfEmpty()
                join a in _entities.areas on rm.area_id equals a.area_id into tmp from a in tmp.DefaultIfEmpty()
                
                select new
                {
                    br.route_id,
                    br.route_name,
                    
                    area_id=rm.area_id??0,
                    rm.serial_no,
                    rm.direction,
                    a.area_name,
                }).OrderBy(x=>x.serial_no);

            var returnData = new
            {
                up_route=data.Where(x=>x.direction=="Up"),
                down_route=data.Where(x=>x.direction=="Down")
            };
            return returnData;
            
            
        }

        public object GetRoutewiseFair(int routeId, string direction)
        {
            var bus_fair = (from bf in _entities.bus_fare.Where(x => x.route_id == routeId && x.direction==direction)
                                join from_area in _entities.areas on bf.from_area_id equals from_area.area_id
                                join to_area in _entities.areas on bf.to_area_id equals to_area.area_id select new
                                {
                                    bf.route_id,
                                    bf.from_area_id,
                                    bf.to_area_id,
                                    bf.fare,
                                    bf.direction,
                                    from_area_name=from_area.area_name,
                                    to_area_name=to_area.area_name
                                });
            
           
            // Make Route combination for fare
            var routes = (from rm in _entities.route_mapping.Where(x => x.route_id == routeId&& x.direction==direction)
                join a in _entities.areas on rm.area_id equals a.area_id
                select new
                {
                    area_id=rm.area_id,
                    area_name=a.area_name,
                    serial_no = rm.serial_no
                }).OrderBy(x => x.serial_no);
            var tmp = routes.OrderBy(x=>x.serial_no);
            List<FareMapingModel> mapList=new List<FareMapingModel>();
            List<FareMapingModel> fromArea=new List<FareMapingModel>();
            List<FareMapingModel> toArea=new List<FareMapingModel>();
            foreach (var item in routes)
            {
                FareMapingModel map = new FareMapingModel
                {
                    from_area_id = (int)item.area_id,
                    from_area_name = item.area_name,
                    
                };
                fromArea.Add(map);
                toArea.Add(map);
            }
            for (int i=0; i<fromArea.Count;i++)
            {
                var item = fromArea.ToArray()[i];
                for (int j=i+1; j<toArea.Count;j++)
                {
                   var seconditem = toArea.ToArray()[j];
                    FareMapingModel map = new FareMapingModel
                    {
                        
                        from_area_id = (int)item.from_area_id,
                        from_area_name = item.from_area_name,
                        direction = item.direction,
                        to_area_id = seconditem.from_area_id,
                        to_area_name = seconditem.from_area_name,

                    };
                    mapList.Add(map);
                }
            }
            // If previously saved fares
            
            
            foreach (var item in mapList)
            {
                foreach (var existingfair in bus_fair)
                {
                    if (existingfair.from_area_id == item.from_area_id && existingfair.to_area_id == item.to_area_id)
                    {
                        item.fare = (int) existingfair.fare;
                        item.direction = existingfair.direction;
                    }
                }
                
            }
            return mapList;
        }

        public object SaveFare(BusFareModel value)
        {
            string query = "delete from bus_fare where route_id=" + value.route_id;
            int returndata=_entities.Database.ExecuteSqlCommand(query);
            
            var routeId = value.route_id;
            List<bus_fare> fareList = value.fare_list;
            foreach (var item in fareList)
            {
                item.route_id = routeId;
                item.direction = "Up";
                _entities.bus_fare.Add(item);

            }
            _entities.SaveChanges();
            List<bus_fare> downfair = value.down_fare ;
            foreach (var item in downfair)
            {
                item.route_id = routeId;
                item.direction = "Down";
                _entities.bus_fare.Add(item);

            }
            _entities.SaveChanges();
            return true;
        }

        public object GetAllRoutes(int companyId)
        {
            var routes = from br in _entities.bus_route.Where(x => x.company_id == companyId)
                             select new
                             {
                                 br.city_id,
                                 br.company_id,
                                 br.route_id,
                                 br.route_name,
                                 is_active=br.is_active??false,
                             };
            return routes.OrderBy(x=>x.route_id);
        }

        public object Put(BusRouteModel value)
        {
            try
            {
                string query = "delete from route_mapping where route_id=" + value.route_id;
                _entities.Database.ExecuteSqlCommand(query);

                bus_route route = _entities.bus_route.Find(value.route_id);
                {
                    route.route_name = value.route_name;
                    //route.company_id = value.company_id;
                    //city_id = value.city_id
                };
                _entities.SaveChanges();
                int returnVal = _entities.SaveChanges();

                string[] routes = value.property_list_string.Split(';');
                string[] down_routes = value.down_route_list_string.Split(';');
                int count = 0;
                foreach (var val in routes)
                {
                    count++;
                    route_mapping mapping = new route_mapping
                    {
                        route_id = route.route_id,
                        area_id = int.Parse(val),
                        serial_no = count,
                        direction = "Up"
                    };
                    _entities.route_mapping.Add(mapping);
                    _entities.SaveChanges();

                }
                count = 0;
                foreach (var val in down_routes)
                {
                    count++;
                    route_mapping mapping = new route_mapping
                    {
                        route_id = route.route_id,
                        area_id = int.Parse(val),
                        serial_no = count,
                        direction = "Down"
                    };
                    _entities.route_mapping.Add(mapping);
                    _entities.SaveChanges();

                }
                
                return true;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public object GetRoutewiseArea()
        {
            return (from rm in _entities.route_mapping
                        join area in _entities.areas on rm.area_id equals area.area_id select new
                        {
                            rm.area_id,
                            area.area_name,
                            rm.route_id
                        })
                ;
        }

        public object ChangeStatus(int routeId)
        {
            bus_route route = _entities.bus_route.Find(routeId);
            if (route.is_active==true || route.is_active==null)
            {
                route.is_active = false;
            }
            else
            {
                route.is_active = true;
            }
            _entities.SaveChanges();
            return "Success";
        }

        public object GetAllAssignedCounterMan(int counterId, string direction)
        {
            return (_entities.counter_employee_mapping.Where(x => x.area_id == counterId && x.direction == direction));
        }

        public object RouteWisEmployeeMapping(List<counter_employee_mapping> value)
        {
            var area_id = value[0].area_id;
            var route_id = value[0].route_id;
            var direction = value[0].direction;
            string query = "Delete from counter_employee_mapping where area_id=" + area_id + "and route_id=" + route_id +
                           "and direction='" + direction+"'";
            _entities.Database.ExecuteSqlCommand(query);
            try
            {
                foreach (counter_employee_mapping item in value)
                {
                    _entities.counter_employee_mapping.Add(item);
                    _entities.SaveChanges();
                   
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return true;
        }

        public object GetCounter(int routeId, string direction)
        {
            var data = (from br in _entities.bus_route.Where(x => x.route_id == routeId)

                        join rm in _entities.route_mapping on br.route_id equals rm.route_id into tmp_rm
                        from rm in tmp_rm.DefaultIfEmpty()
                        join a in _entities.areas on rm.area_id equals a.area_id into tmp
                        from a in tmp.DefaultIfEmpty()

                        select new
                        {
                            br.route_id,
                            br.route_name,

                            area_id = rm.area_id ?? 0,
                            rm.serial_no,
                            rm.direction,
                            a.area_name,
                        }).OrderBy(x => x.serial_no);

            return data.Where(x => x.direction == direction);
        }
    }
}