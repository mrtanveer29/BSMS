using ERPApi.Models;
using ERPApi.Models.IRepository;
using ERPApi.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ERPApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ExperienceController : ApiController
    {
        private IExperienceRepository experienceRepository;

        public ExperienceController()
        {
            this.experienceRepository = new ExperienceRepository();
        }

        public ExperienceController(IExperienceRepository experienceRepository)
        {
            this.experienceRepository = experienceRepository;
        }

        
        public HttpResponseMessage GetAllExperiences()
        {
            var experinces = experienceRepository.GetAllExperiences();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, experinces, formatter);
        }

        [ActionName("GetExperienceByEmployee")]
        public HttpResponseMessage GetExperienceByEmployee(int? employee_id)
        {
            List<hr_experience> experinces = experienceRepository.GetExperienceByEmployee(employee_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, experinces, formatter);
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] Models.hr_experience oExperience)
        {
            var urlForRequest = Request.RequestUri.ParseQueryString();
            int employee_id = int.Parse(urlForRequest["employee_id"].ToString());

            try
            {
                if (string.IsNullOrEmpty(oExperience.company))
                {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "Company can not be empty" });
                }
                else if (string.IsNullOrEmpty(oExperience.job_title))
                {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "Job Title Name can not be empty" });
                }
                else if (string.IsNullOrEmpty(oExperience.from_date))
                {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "From Date Year can not be empty" });
                }
                else if (string.IsNullOrEmpty(oExperience.to_date))
                {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "To Date can not be empty" });
                }

                else if (string.IsNullOrEmpty(oExperience.responsibilities))
                {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "Responsibilities can not be empty" });
                }

                else
                {
                    //kendo date formate to general formate
                    string ftime = oExperience.from_date.ToString();
                    string frdate = ftime.Substring(4, 12);

                    string totime = oExperience.to_date.ToString();
                    string Trdate = totime.Substring(4, 12);
                    
                    
                    //string totime = oExperience.to_date.ToString();
                    //string[] twords = totime.Split(' ');
                    //string tot = ewords[4];

                    Models.hr_experience insertExperience = new Models.hr_experience
                    {
                        employee_id =employee_id,
                        company = oExperience.company,
                        job_title = oExperience.job_title,
                        from_date = frdate,
                        to_date = Trdate,
                        responsibilities = oExperience.responsibilities
                    };
                    bool insert_experience = experienceRepository.InsertExperience(insertExperience);
                    //if (save_user == true)
                    if (insert_experience == true)
                    {


                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK,
                            new Confirmation { output = "success", msg = "Experience  Details  is saved successfully." }, formatter);
                    }
                    else
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK,
                            new Confirmation { output = "error", msg = "Experience Details  is not saved succesfully." }, formatter);
                    }

                }
            }

            catch (Exception ex)
            {

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK,
                    new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }
        [HttpPut]
        public HttpResponseMessage Put([FromBody] Models.hr_experience oExperience)
        {

            try
            {

                if (string.IsNullOrEmpty(oExperience.company))
                {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "Company can not be empty" });
                }
                else if (string.IsNullOrEmpty(oExperience.job_title))
                {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "Job Title Name can not be empty" });
                }
                else if (string.IsNullOrEmpty(oExperience.from_date))
                {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "From Date Year can not be empty" });
                }
                else if (string.IsNullOrEmpty(oExperience.to_date))
                {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "To Date can not be empty" });
                }

                else if (string.IsNullOrEmpty(oExperience.responsibilities))
                {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "Responsibilities can not be empty" });
                }
                else
                {

                    string ftime = oExperience.from_date.ToString();
                    string frdate = ftime.Substring(4, 12);

                    string totime = oExperience.to_date.ToString();
                    string Trdate = totime.Substring(4, 12);


                    Models.hr_experience updateExperience = new Models.hr_experience
                    {
                        experience_id = oExperience.experience_id,
                        employee_id = oExperience.employee_id,
                        company = oExperience.company,
                        job_title = oExperience.job_title,
                        from_date = frdate,
                        to_date = Trdate,
                        responsibilities = oExperience.responsibilities
                    };
                    bool irepoUpdate = experienceRepository.UpdateExperience(updateExperience);
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "success", msg = "Experience Details Update successfully" }, formatter);
                }
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);

            }


        }
        [HttpDelete]
        public HttpResponseMessage Delete([FromBody] Models.hr_experience oExperience)//, [FromBody] Models.user user
        {
            try
            {
                bool deleteExperience = experienceRepository.DeleteExperience(oExperience.experience_id);

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK,
                    new Confirmation { output = "success", msg = "Experience Delete Successfully." }, formatter);



            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK,
                    new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }

        }
    }
}
