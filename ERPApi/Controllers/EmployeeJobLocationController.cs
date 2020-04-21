using ERPApi.Models;
using ERPApi.Models.IRepository;
using ERPApi.Models.Repository;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ERPApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EmployeeJobLocationController : ApiController
    {
        private IEmployeeJobLocationRepository joblocationRepository;

        public EmployeeJobLocationController()
        {
            this.joblocationRepository = new EmployeeJobLocationRepository();
        }

        public EmployeeJobLocationController(IEmployeeJobLocationRepository joblocationRepository)
        {
            this.joblocationRepository = joblocationRepository;
        }

        public HttpResponseMessage GetAllJobLocation()
        {
            var joblocation = joblocationRepository.GetAllJobLocation();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, joblocation, formatter);
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody]Models.job_location joblocation)
        {
            try
            {
                if (string.IsNullOrEmpty(joblocation.job_location_title))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Job Location is Empty" }, formatter);
                }
                else
                {
                    if (joblocationRepository.CheckDuplicateJobLocation(joblocation.job_location_title))
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Job Location Already Exists" }, formatter);
                    }
                    else
                    {
                        Models.job_location insertJobLocation = new Models.job_location
                        {
                            job_location_title = joblocation.job_location_title
                        };
                        bool save_job_location = joblocationRepository.InsertJobLocation(insertJobLocation);

                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Job Location save successfully" }, formatter);
                    }
                }
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [HttpPut]
        public HttpResponseMessage Put([FromBody]Models.job_location joblocation)
        {
            try
            {
                if (string.IsNullOrEmpty(joblocation.job_location_title))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Job Location Name is Empty" }, formatter);
                }
                else
                {
                    Models.job_location updateJobLocation = new Models.job_location

                    {
                        job_location_id = joblocation.job_location_id,
                        job_location_title = joblocation.job_location_title,
                    };

                    bool irepoUpdate = joblocationRepository.UpdateJobLocation(updateJobLocation);

                    if (irepoUpdate == true)
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Job Location Update successfully" }, formatter);
                    }
                    else
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Update Failed" }, formatter);
                    }
                }
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [HttpDelete]
        public HttpResponseMessage Delete([FromBody]Models.job_location joblocation)
        {
            try
            {
                //int con_id = int.Parse(country_id);
                bool deleteJobLocation = joblocationRepository.DeleteJobLocation(joblocation.job_location_id);

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Job Location Delete Successfully." }, formatter);
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }
    }
}