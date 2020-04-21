using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ERPApi.Models.IRepository;

namespace ERPApi.Models.Repository
{
    public class AreaRepository : IAreaRepository
    {
        private ERPEntities _entities;

        public AreaRepository()
        {
            _entities=new ERPEntities();
        }

        public object GetAllAreas(int companyId)
        {
            var data = (from a in _entities.areas.Where(x=>x.created_company==companyId)
                join c in _entities.cities on a.city_id equals c.city_id
                join emp in _entities.employees on a.counter_man_id equals emp.emp_id into tmp_emp from emp in tmp_emp.DefaultIfEmpty()
                select new
                {
                    area_id=a.area_id,
                    a.area_name,
                    a.area_details,
                    a.city_id,
                    counter_man_id=a.counter_man_id??0,
                    counter_man_name=emp.emp_firstname+" "+emp.emp_lastname??"",
                    c.city_name
                }).OrderByDescending(x=>x.area_id);
            return data;
        }

        public object Post(area value)
        {
            try
            {
                _entities.areas.Add(value);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public object Put(area area)
        {
            try
            {
                area areaInfo = _entities.areas.Find(area.area_id);
                areaInfo.area_details = area.area_details;
                areaInfo.area_name = area.area_name;
                //areaInfo.counter_man_id = area.counter_man_id;
                areaInfo.city_id = area.city_id;
                _entities.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public object Delete(area area)
        {
            try
            {
                area areaInfo = _entities.areas.Find(area.area_id);
                _entities.areas.Remove(areaInfo);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public object GetAllAreasByCityId(int cityId, int companyId)
        {
            var data = (from a in _entities.areas.Where(x=>x.city_id==cityId && x.created_company==companyId)
                        join c in _entities.cities on a.city_id equals c.city_id
                        select new
                        {
                            area_id = a.area_id,
                            a.area_name,
                            a.area_details,
                            a.city_id,
                            c.city_name
                        }).OrderByDescending(x => x.area_id);
            return data;
        }

        public object GetAllAreasByRoute(int routeId)
        {
            var cityId = _entities.bus_route.FirstOrDefault(x => x.route_id == routeId).city_id;
            var data = (from ar in _entities.areas.Where(x => x.city_id == cityId)
                select new
                {
                    from_area_id=ar.area_id,
                    from_area_name=ar.area_name,
                    to_area_id = ar.area_id,
                    to_area_name = ar.area_name,
                });
            return data;
        }
    }
}