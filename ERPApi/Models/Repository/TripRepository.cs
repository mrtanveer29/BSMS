using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
using System.Web.UI.WebControls;
using ERPApi.Models.IRepository;
using ERPApi.Models.StronglyType;

namespace ERPApi.Models.Repository
{
    public class TripRepository :ITripRepository
    {
        private ERPEntities _entities;

        public TripRepository()

        {
            _entities=new ERPEntities();
        }

        public object Post(TripModel trip)
        {
            TripMasterModel tmpMasterData= trip.tripMasterData;
            var today = DateTime.Now.Date.ToString("MM/dd/yyyy");
            try
            {
                int ?maxTrip = 0;
                // Get counter position
//                var counter_position =
//                    _entities.route_mapping
//                        .FirstOrDefault(x => x.route_id == tmpMasterData.route_id && x.area_id == tmpMasterData.area_id&& x.direction==tmpMasterData.direction);
               
                // Get max trip for a day
                var trips =
                    (from tm in
                        _entities.trip_master.Where(
                            x =>
                                x.date == today && x.bus_id == tmpMasterData.bus_id &&
                                x.route_id == tmpMasterData.route_id) select new
                                {
                                    tm.trip_master_id,
                                   tm.bus_id,
                                   tm.trip_count
                                }).OrderByDescending(x=>x.trip_master_id);
                if (tmpMasterData != null)
                {
                    maxTrip = trips.Select(x => x.trip_count).FirstOrDefault();
                }
                if (tmpMasterData.counter_position == 1)
                {
                    maxTrip++;
                }
                
                trip_master masterData = new trip_master
                {
                    route_id = tmpMasterData.route_id,
                    area_id = tmpMasterData.area_id,
                    bus_id = tmpMasterData.bus_id,
                    date = today,
                    direction = tmpMasterData.direction,
                    updated_by = tmpMasterData.updated_by,
                    time = ( DateTime.Now.Ticks-621355968000000000) / 10000000,
                    trip_count = maxTrip
                   
                    
                };
                _entities.trip_master.Add(masterData);
                _entities.SaveChanges();
                int trip_master_id = masterData.trip_master_id;

                List<trip_details> details=new List<trip_details>();
                foreach (trip_details item in trip.tripDetailsData)
                {
                    item.trip_master_id = trip_master_id;
                    _entities.trip_details.Add(item);
                }
                _entities.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }

        public object GetBusUpdates(int routeId, string direction, int counterPosition)
        {
            int previous_serial = counterPosition - 1;
            var currentDate = DateTime.Now.Date.ToString("MM/dd/yyyy");

            var previous_counter =
                _entities.route_mapping.FirstOrDefault(
                    x => x.route_id == routeId && x.direction == direction && x.serial_no == (previous_serial));

            var busdata = (from tm in
                _entities.trip_master.Where(
                    x => x.route_id == routeId && x.direction == direction && x.date == currentDate )
                select new
                {
                    tm.bus_id,
                    tm.trip_master_id,
                    tm.area_id, //Wrong data
                    tm.date,
                    tm.trip_count
                }).GroupBy(g => g.bus_id).Select(x => new
                {
                    bus_id=x.Key,

                    trip_count=x.Select(y=>y.trip_count).Max(),
                    area_id=x.Select(y=>y.area_id).FirstOrDefault(),
                    trip_master_id=x.Select(y=>y.trip_master_id).Max()

                }).ToList();

//            foreach (var singleBusData in busdata)
//            {
//                var passengerCount = (from td in _entities.trip_details.Where(x=>x.trip_master_id==singleBusData.trip_master_id) 
//                    select new
//                    {
//                        td.trip_master_id,
//                        td.from_area,
//                        td.to_area,
//                        td.no_of_passenger
//
//                    });
//
//
//            }


            return busdata;
        }

        public object GetdailySalesUpdates(int companyId, string date)
        {
            var busdata = (from tm in
                _entities.trip_master.Where(x => x.date == date)
                join td in _entities.trip_details on tm.trip_master_id equals td.trip_master_id
                join b in _entities.buses.Where(c => c.company_id == companyId) on tm.bus_id equals b.bus_id

                select new
                {
                    tm.trip_master_id,
                    tm.bus_id,
                    bus_registration_no = b.bus_registration_no,
                    tm.route_id,
                    tm.trip_count,
                    tm.date,
                    td.from_area, //Wrong data
                    td.to_area,
                    td.no_of_passenger,
                 
                }).ToList();
            List<BusReportModel> list=new List<BusReportModel>();
            foreach (var item in busdata)
            {
                var fare =
                    (from bf in
                        _entities.bus_fare.Where(x => x.from_area_id == item.from_area && x.to_area_id == item.to_area&& x.route_id==item.route_id)
                        select new
                        {
                            fare=bf.fare??0
                        }).FirstOrDefault();
                 BusReportModel model=new BusReportModel();
                model.trip_master_id = item.trip_master_id;
                model.bus_id = item.bus_id;
                model.bus_registration_no = item.bus_registration_no;
                model.date = item.bus_registration_no;
                model.from_area = item.from_area;
                model.to_area = item.to_area;
                model.no_of_passenger = item.no_of_passenger;
                model.trip_count = item.trip_count;
                if (fare == null)
                {
                    model.fare = 0;
                }else
                {
                    model.fare = fare.fare;
                }
                

                list.Add(model);

            }

            return list.GroupBy(g => g.bus_id).Select(x => new
            {
                bus_id = x.Key,

                trip = x.Select(y => y.trip_count).Count(),
                fare = x.Select(y => y.fare).Sum(),
              

            }).ToList(); 
        }
    }
}