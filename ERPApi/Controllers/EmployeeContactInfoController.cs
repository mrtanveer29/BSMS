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
    public class EmployeeContactInfoController : ApiController
    {
           private IEmployeeContactInfoRepository employeecontactinfoRepository;

        public EmployeeContactInfoController()
        {
            this.employeecontactinfoRepository = new EmployeeContactInfoRepository();
        }

        public EmployeeContactInfoController(IEmployeeContactInfoRepository employeecontactinfoRepository)
        {
            this.employeecontactinfoRepository = employeecontactinfoRepository;
        }


        public HttpResponseMessage GetAllEmployeeContactInfo()
        {
            var designations = employeecontactinfoRepository.GetAllEmployeeContactInfo();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, designations, formatter);
        }

           [HttpPost]
        public HttpResponseMessage Post([FromBody] Models.hr_emp_contact_info oEmployeeContactInfo)
        {

            try
            {
                //bool save_user;
                if (string.IsNullOrEmpty(oEmployeeContactInfo.city_id.ToString()))
                {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "City Name can not be empty." });
                }
              else  if (string.IsNullOrEmpty(oEmployeeContactInfo.country_id.ToString()))
                {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "Country can not be empty." });
                }
              else  if (string.IsNullOrEmpty(oEmployeeContactInfo.present_address))
                {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "Present Address can not be empty." });
                }
              else  if (string.IsNullOrEmpty(oEmployeeContactInfo.permanent_address))
                {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "Permanent Address can not be empty." });
                }
              else  if (string.IsNullOrEmpty(oEmployeeContactInfo.zip_code.ToString()))
                {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "Zip Code can not be empty." });
                }

               
                else
                {


                    Models.hr_emp_contact_info insertEmployeeContactInfo = new Models.hr_emp_contact_info
                    {
                        city_id = oEmployeeContactInfo.city_id,
                        country_id = oEmployeeContactInfo.country_id,
                        present_address = oEmployeeContactInfo.present_address,
                        permanent_address = oEmployeeContactInfo.permanent_address,
                        zip_code = oEmployeeContactInfo.zip_code,
                        emp_email = oEmployeeContactInfo.emp_email,
                        emp_mobile = oEmployeeContactInfo.emp_mobile,
                        emp_phone = oEmployeeContactInfo.emp_phone,
                        employee_id = oEmployeeContactInfo.employee_id,
                        emergency_contact_name = oEmployeeContactInfo.emergency_contact_name,
                        emergency_contact_id = oEmployeeContactInfo.emergency_contact_id,
                        emergency_contact_address = oEmployeeContactInfo.emergency_contact_address,
                        emergency_contact_mobile = oEmployeeContactInfo.emergency_contact_mobile,
                        emergency_contact_email = oEmployeeContactInfo.emergency_contact_email,
                        emergency_contact_relation = oEmployeeContactInfo.emergency_contact_relation
                        
                    };
                     bool insert_employee_contact_info = employeecontactinfoRepository.InsertEmployeeContactInfo(insertEmployeeContactInfo);
                    //if (save_user == true)
                    if (insert_employee_contact_info == true)
                    {


                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK,
                            new Confirmation { output = "success", msg = "Contact Info is saved successfully." }, formatter);
                    }
                    else
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK,
                            new Confirmation { output = "error", msg = "Contact Info  is not saved succesfully." }, formatter);
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
           public HttpResponseMessage UpdateEmployeeContactInfo([FromBody] Models.hr_emp_contact_info oEmployeeContactInfo)
        {

            try
            {
                if (string.IsNullOrEmpty(oEmployeeContactInfo.city_id.ToString()))
                   {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "City Name can not be empty" });
                }
              else  if (string.IsNullOrEmpty(oEmployeeContactInfo.country_id.ToString()))
                {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "Country can not be empty" });
                }
              else  if (string.IsNullOrEmpty(oEmployeeContactInfo.present_address))
                {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "Present Address can not be empty" });
                }
              else  if (string.IsNullOrEmpty(oEmployeeContactInfo.permanent_address))
                {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "Permanent Address can not be empty" });
                }
              else  if (string.IsNullOrEmpty(oEmployeeContactInfo.zip_code.ToString()))
                {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "Zip Code can not be empty" });
                }



                else
                {

                    Models.hr_emp_contact_info updateEmployeeContactInfo = new Models.hr_emp_contact_info
                    {
                        emp_contact_info_id = oEmployeeContactInfo.emp_contact_info_id,
                        city_id = oEmployeeContactInfo.city_id,
                        country_id = oEmployeeContactInfo.country_id,
                        present_address = oEmployeeContactInfo.present_address,
                        permanent_address = oEmployeeContactInfo.permanent_address,
                        zip_code = oEmployeeContactInfo.zip_code,
                        emp_email = oEmployeeContactInfo.emp_email,
                        emp_mobile = oEmployeeContactInfo.emp_mobile,
                        emp_phone = oEmployeeContactInfo.emp_phone,
                        employee_id = oEmployeeContactInfo.employee_id,
                        emergency_contact_name = oEmployeeContactInfo.emergency_contact_name,
                        emergency_contact_id = oEmployeeContactInfo.emergency_contact_id,
                        emergency_contact_address = oEmployeeContactInfo.emergency_contact_address,
                        emergency_contact_mobile = oEmployeeContactInfo.emergency_contact_mobile,
                        emergency_contact_email = oEmployeeContactInfo.emergency_contact_email,
                        emergency_contact_relation = oEmployeeContactInfo.emergency_contact_relation
                    };
                    bool irepoUpdate = employeecontactinfoRepository.UpdateEmployeeContactInfo(updateEmployeeContactInfo);
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "success", msg = "Contact Info Details Update successfully" }, formatter);
                }
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);

            }


        }

          [HttpDelete]
          public HttpResponseMessage Delete([FromBody] Models.hr_emp_contact_info oEmployeeContactInfo)//, [FromBody] Models.user user
          {
              try
              {
                  bool deleteEmployeeContactInfo = employeecontactinfoRepository.DeleteEmployeeContactInfo(oEmployeeContactInfo.emp_contact_info_id);

                  var formatter = RequestFormat.JsonFormaterString();
                  return Request.CreateResponse(HttpStatusCode.OK,
                      new Confirmation { output = "success", msg = "Contact Info details Delete Successfully." }, formatter);



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
