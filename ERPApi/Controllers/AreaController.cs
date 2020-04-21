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

namespace ERPApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AreaController : ApiController
    {
        private IAreaRepository areaRepository;

        public AreaController(IAreaRepository areaRepository)
        {
            this.areaRepository = areaRepository;
        }

        public AreaController()
        {
            areaRepository=new AreaRepository();
        }

        [HttpGet]
        public HttpResponseMessage GetAllAreas(int company_id)
        {
            var data = areaRepository.GetAllAreas(company_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, data, formatter);
        }
        [HttpGet]
        public HttpResponseMessage GetAllAreasByCityId(int city_id, int company_id)
        {
            var data = areaRepository.GetAllAreasByCityId(city_id, company_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, data, formatter);
        } 
        [HttpGet]
        public HttpResponseMessage GetAllAreasByRoute(int route_id)
        {
            var data = areaRepository.GetAllAreasByRoute(route_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, data, formatter);
        }
        [HttpPost]
        public HttpResponseMessage Post([FromBody]area value)
        {
            var data = areaRepository.Post(value);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Data save successfully" }, formatter);
        }
        [HttpPut]
        public HttpResponseMessage Put([FromBody]area value)
        {
            var data = areaRepository.Put(value);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Data Updated successfully" }, formatter);
        }
        [HttpDelete]
        public HttpResponseMessage Delete([FromBody]area value)
        {
            var data = areaRepository.Delete(value);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Data Deleted successfully" }, formatter);
        }
    }
}