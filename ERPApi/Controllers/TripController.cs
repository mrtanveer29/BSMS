using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using ERPApi.Models;
using ERPApi.Models.IRepository;
using ERPApi.Models.Repository;
using ERPApi.Models.StronglyType;

namespace ERPApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TripController : ApiController
    {

        public ITripRepository tripRepository;

        public TripController(ITripRepository tripRepository)
        {
            tripRepository = tripRepository;
        }

        public TripController()
        {
            tripRepository = new TripRepository();
        }

        [HttpPost]
        public HttpResponseMessage Post(TripModel trip)
        {
            var data = tripRepository.Post(trip);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Data save successfully" }, formatter);
        }

        

        [HttpGet]
        public HttpResponseMessage GetBusUpdates(int route_id,string direction,int counter_position)
        {
            var data = tripRepository.GetBusUpdates(route_id, direction, counter_position);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, data, formatter);
        } 
        [HttpGet]
        public HttpResponseMessage GetdailySalesUpdates(int company_id, string date)
        {
            var data = tripRepository.GetdailySalesUpdates(company_id, date);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, data, formatter);
        }

    }
}