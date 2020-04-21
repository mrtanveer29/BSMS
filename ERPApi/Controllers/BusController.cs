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
    public class BusController : ApiController
    {
        public IBusRepository busRepository;

         public BusController(IBusRepository busRepository)
         {
             this.busRepository = busRepository;
         }

         public BusController()
         {
             busRepository=new BusRepository();
         }
       
        [HttpGet]
        public HttpResponseMessage GetAllBus(int company_id)
         {
             var data = busRepository.GetAllBus(company_id);
             var formatter = RequestFormat.JsonFormaterString();
             return Request.CreateResponse(HttpStatusCode.OK, data, formatter);
         }
        [HttpGet]
        public HttpResponseMessage GetAllBus(int company_id,int route_id)
         {
             var data = busRepository.GetAllBus(company_id, route_id);
             var formatter = RequestFormat.JsonFormaterString();
             return Request.CreateResponse(HttpStatusCode.OK, data, formatter);
         }
        [HttpPost]
        public HttpResponseMessage Post([FromBody]bus value)
        {
            var data = busRepository.Post(value);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Data save successfully" }, formatter);
        }
        [HttpPost]
        public HttpResponseMessage RouteWisBusMapping([FromBody]BusRouteMappingModel value)
        {
            var data = busRepository.RouteWisBusMapping(value);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Data save successfully" }, formatter);
        }
        [HttpPut]
        public HttpResponseMessage Put([FromBody]bus value)
        {
            var data = busRepository.Put(value);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Data Updated successfully" }, formatter);
        }
        [HttpDelete]
        public HttpResponseMessage Delete([FromBody]bus value)
        {
            var data = busRepository.Delete(value);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Data Deleted successfully" }, formatter);
        }
        // Bus Type
        [HttpGet]
        public HttpResponseMessage GetAllBusType(int company_id)
        {
            var data = busRepository.GetAllBusType(company_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, data, formatter);
        } 
        [HttpPost]
         public HttpResponseMessage SaveBusType([FromBody]bus_type value)
        {
            var data = busRepository.SaveBusType(value);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Data save successfully" }, formatter);
        }
       

        [HttpPut]
        public HttpResponseMessage UpdateBusType([FromBody]bus_type value)
        {
            var data = busRepository.UpdateBusType(value);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Data Updated successfully" }, formatter);
        }
        [HttpDelete]
        public HttpResponseMessage DeleteBusType([FromBody]bus_type value)
        {
            var data = busRepository.DeleteBusType(value);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Data Deleted successfully" }, formatter);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}