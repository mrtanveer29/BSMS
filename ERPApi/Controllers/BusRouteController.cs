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
    public class BusRouteController : ApiController
    {
        public IBusRouteRepository busRouteRepository;

        public BusRouteController(IBusRouteRepository busRouteRepository)
        {
            this.busRouteRepository = busRouteRepository;
        }

        public BusRouteController()
        {
            busRouteRepository = new BusRouteRepository();
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody]BusRouteModel value)
        {
            var data = busRouteRepository.Post(value);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Data save successfully" }, formatter);
        }
        [HttpPut]
        public HttpResponseMessage Put([FromBody]BusRouteModel value)
        {
            var data = busRouteRepository.Put(value);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Data Updated successfully" }, formatter);
        }
        [HttpPost]
        public HttpResponseMessage SaveFare([FromBody]BusFareModel value)
            {
                var data = busRouteRepository.SaveFare(value);
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Data save successfully" }, formatter);
            }
         [HttpPost]
        public HttpResponseMessage RouteWisEmployeeMapping([FromBody]List<counter_employee_mapping> value)
            {
                var data = busRouteRepository.RouteWisEmployeeMapping(value);
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Data save successfully" }, formatter);
            }

        [HttpGet]
        public HttpResponseMessage GetRouteDetails(int route_id)
        {
            var data = busRouteRepository.GetRouteDetails(route_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, data, formatter);
        }
        
        [HttpGet]
        public HttpResponseMessage GetCounter(int route_id,string direction)
        {
            var data = busRouteRepository.GetCounter(route_id, direction);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, data, formatter);
        }
        [HttpGet]
        public HttpResponseMessage GetAllAssignedCounterMan(int counter_id, string direction)
        {
            var data = busRouteRepository.GetAllAssignedCounterMan(counter_id, direction);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, data, formatter);
        }
        [HttpGet]
        public HttpResponseMessage ChangeStatus(int route_id)
        {
            var data = busRouteRepository.ChangeStatus(route_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, data, formatter);
        }
        [HttpGet]
        public HttpResponseMessage GetRoutewiseArea()
        {
            var data = busRouteRepository.GetRoutewiseArea();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, data, formatter);
        } 
        [HttpGet]
        public HttpResponseMessage GetAllRoutes(int company_id)
        {
            var data = busRouteRepository.GetAllRoutes(company_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, data, formatter);
        }
        [HttpGet]
        public HttpResponseMessage GetRoutewiseFair(int route_id,string direction)
        {
            var data = busRouteRepository.GetRoutewiseFair(route_id, direction);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, data, formatter);
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

       
    }
}