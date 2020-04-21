using ERPApi.Models;
using ERPApi.Models.IRepository;
using ERPApi.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ERPApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EducationController : ApiController
    {
            private IEducationRepository educationRepository;

        public EducationController()
        {
            this.educationRepository = new EducationRepository();
        }

        public EducationController(IEducationRepository educationRepository)
        {
            this.educationRepository = educationRepository;
        }

        public HttpResponseMessage GetAllEducation(int? employee_id)
        {
            List<hr_education> educations = educationRepository.GetEducationByEmployee(employee_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, educations, formatter);
        }
        [HttpPost]
        public HttpResponseMessage Post([FromBody] Models.hr_education oEducation)
        {
            Regex regex = new Regex("^[0-9]+$");
            //post/get employee id in education
            var urlForRequest = Request.RequestUri.ParseQueryString();
            int employee_id = int.Parse(urlForRequest["employee_id"].ToString());

            try
            {
                //bool save_user;
    
                if (string.IsNullOrEmpty(oEducation.degree_name))
                {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "Degree can not be empty" });
                }
                else if (string.IsNullOrEmpty(oEducation.institute))
                {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "Institution Name can not be empty" });
                }
                //else if (string.IsNullOrEmpty(oEducation.passing_year.ToString()))
                //{
                //    var format_type = RequestFormat.JsonFormaterString();
                //    return Request.CreateResponse(HttpStatusCode.OK,
                //        new Confirmation { output = "error", msg = "Passing Year can not be empty" });
                //}
                else if (!regex.IsMatch(oEducation.passing_year.ToString()))
                {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "Passing Year should be Numeric" });
                }
                else if (string.IsNullOrEmpty(oEducation.result))
                {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "Result can not be empty" });
                }


                else
                {


                    Models.hr_education insertEdcation = new Models.hr_education
                    {
                        employee_id = employee_id,
                        degree_name = oEducation.degree_name,
                        institute = oEducation.institute,
                        passing_year = oEducation.passing_year,
                        result = oEducation.result
                    };
                    bool insert_education = educationRepository.InsertEducation(insertEdcation);
                    //if (save_user == true)
                    if (insert_education == true)
                    {


                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK,
                            new Confirmation { output = "success", msg = "Education  Details  is saved successfully." }, formatter);
                    }
                    else
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK,
                            new Confirmation { output = "error", msg = "Education Details  is not saved succesfully." }, formatter);
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
        public HttpResponseMessage Put([FromBody] Models.hr_education oEducation)
        {

            try
            {

               
                 if (string.IsNullOrEmpty(oEducation.degree_name))
                {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "Degree can not be empty" });
                }
                else if (string.IsNullOrEmpty(oEducation.institute))
                {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "Institution Name can not be empty" });
                }
                else if (string.IsNullOrEmpty(oEducation.passing_year.ToString()))
                {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "Passing Year can not be empty" });
                }
                else if (string.IsNullOrEmpty(oEducation.result))
                {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "Result can not be empty" });
                }
                else
                {

                    Models.hr_education updateEducation = new Models.hr_education
                    {
                        education_id = oEducation.education_id,
                        employee_id = oEducation.employee_id,
                        degree_name = oEducation.degree_name,
                        institute = oEducation.institute,
                        passing_year = oEducation.passing_year,
                        result = oEducation.result
                    };
                    bool irepoUpdate = educationRepository.UpdateEducation(updateEducation);
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "success", msg = "Education Details Update successfully" }, formatter);
                }
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);

            }


        }
        [HttpDelete]
        public HttpResponseMessage Delete([FromBody] Models.hr_education oEducation)//, [FromBody] Models.user user
        {
            try
            {
                bool deleteEducation = educationRepository.DeleteEducation(oEducation.education_id);

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK,
                    new Confirmation { output = "success", msg = "Education details Delete Successfully." }, formatter);



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
