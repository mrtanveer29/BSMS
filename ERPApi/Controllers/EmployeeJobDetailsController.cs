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
    public class EmployeeJobDetailsController : ApiController
    {
          private IEmployeeJobDetailsRepository employeejobdetailsRepository;

        public EmployeeJobDetailsController()
        {
            this.employeejobdetailsRepository = new EmployeeJobDetailsRepository();
        }

        public EmployeeJobDetailsController(IEmployeeJobDetailsRepository employeejobdetailsRepository)
        {
            this.employeejobdetailsRepository = employeejobdetailsRepository;
        }

        //Company id is fetched but not used
        //To make sure the route path explicitely called works properly
        //Without making a duplicate method type exception
        [Route("EmployeeJobDetails/GetAllEmployeeCode?com_id={com_id}&company_id={company_id}")]
        public HttpResponseMessage GetAllEmployeeCode(int com_id,int company_id)
        {
            var designations = employeejobdetailsRepository.GetAllEmployeeCode(company_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, designations, formatter);
        }

        [Route("EmployeeJobDetails/GetAllEmployeeJobDetails")]
        public HttpResponseMessage GetAllEmployeeJobDetails()
        {
            var designations = employeejobdetailsRepository.GetAllEmployeeJobDetails();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, designations, formatter);
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] Models.StronglyType.EmpJobDetailsModel oEmployeeJobDetails)
        {

            try
            {
                //bool save_user;
                if (string.IsNullOrEmpty(oEmployeeJobDetails.company_id.ToString()))
                {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "Company Name can not be empty" });
                }
              else  if (string.IsNullOrEmpty(oEmployeeJobDetails.branch_id.ToString()))
                {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "Branch can not be empty" });
                }
              else  if (string.IsNullOrEmpty(oEmployeeJobDetails.department_id.ToString()))
                {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "Department Name can not be empty" });
                }

              
                else if (string.IsNullOrEmpty(oEmployeeJobDetails.emp_dateofjoin))
                {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "Joining Date can not be empty" });
                }
                else if (string.IsNullOrEmpty(oEmployeeJobDetails.emp_code))
                {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "Employee Code can not be empty" });
                }
              
               
                else
                {


                    Models.StronglyType.EmpJobDetailsModel insertEmployeeJobDetails = new Models.StronglyType.EmpJobDetailsModel
                    {
                        company_id = oEmployeeJobDetails.company_id,
                        branch_id = oEmployeeJobDetails.branch_id,
                        unit_id = oEmployeeJobDetails.unit_id,
                        department_id = oEmployeeJobDetails.department_id,
                        designation_id = oEmployeeJobDetails.designation_id,
                        staffgrade_id = oEmployeeJobDetails.staffgrade_id,
                        job_location_id = oEmployeeJobDetails.job_location_id,
                        subsection_id = oEmployeeJobDetails.subsection_id,
                        start_date = oEmployeeJobDetails.start_date,
                        end_date = oEmployeeJobDetails.end_date,
                        is_active = oEmployeeJobDetails.is_active,
                        created_by = oEmployeeJobDetails.created_by,
                        created_date = oEmployeeJobDetails.created_date,
                        updated_by = oEmployeeJobDetails.updated_by,
                        updated_date = oEmployeeJobDetails.updated_date,
                        emp_code = oEmployeeJobDetails.emp_code,
                        emp_dateofjoin = oEmployeeJobDetails.emp_dateofjoin,
                        emp_prop_confirmation_date = oEmployeeJobDetails.emp_prop_confirmation_date,
                        emp_id = oEmployeeJobDetails.emp_id,
                        emp_supervisor = oEmployeeJobDetails.emp_supervisor,
                        attendance_policy_id = oEmployeeJobDetails.attendance_policy_id,
                        emp_reporting_method = oEmployeeJobDetails.emp_reporting_method
                    };
                     bool insert_employee_job_details = employeejobdetailsRepository.InsertEmployeeJobDetails(insertEmployeeJobDetails);
                    //if (save_user == true)
                    if (insert_employee_job_details == true)
                    {


                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK,
                            new Confirmation { output = "success", msg = "Employee job Details  is saved successfully." }, formatter);
                    }
                    else
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK,
                            new Confirmation { output = "error", msg = "Employee job Details  is not saved succesfully." }, formatter);
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


        [ActionName("UpdateEmployeeJobDetails")]
          [HttpPut]
        public HttpResponseMessage UpdateEmployeeJobDetails([FromBody] Models.StronglyType.EmpJobDetailsModel oEmployeeJobDetails)
        {

            try
            {

              
                if (string.IsNullOrEmpty(oEmployeeJobDetails.branch_id.ToString()))
                {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "Branch can not be empty." });
                }
                if (string.IsNullOrEmpty(oEmployeeJobDetails.department_id.ToString()))
                {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "Department Name can not be empty." });
                }

                if (string.IsNullOrEmpty(oEmployeeJobDetails.emp_dateofjoin))
                {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "Joining Date can not be empty." });
                }
                if (string.IsNullOrEmpty(oEmployeeJobDetails.emp_code))
                {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "Employee Code can not be empty." });
                }
             
                else
                {

                    Models.StronglyType.EmpJobDetailsModel updateEmployeeJobDetails = new Models.StronglyType.EmpJobDetailsModel
                    {
                        emp_job_details_id = oEmployeeJobDetails.emp_job_details_id,
                        company_id = oEmployeeJobDetails.company_id,
                        branch_id = oEmployeeJobDetails.branch_id,
                        unit_id = oEmployeeJobDetails.unit_id,
                        department_id = oEmployeeJobDetails.department_id,
                        designation_id = oEmployeeJobDetails.designation_id,
                        staffgrade_id = oEmployeeJobDetails.staffgrade_id,
                        subsection_id = oEmployeeJobDetails.subsection_id,
                        start_date = oEmployeeJobDetails.start_date,
                        end_date = oEmployeeJobDetails.end_date,
                        is_active = oEmployeeJobDetails.is_active,
                        created_by = oEmployeeJobDetails.created_by,
                        created_date = oEmployeeJobDetails.created_date,
                        updated_by = oEmployeeJobDetails.updated_by,
                        updated_date = oEmployeeJobDetails.updated_date,
                        emp_code = oEmployeeJobDetails.emp_code,
                        emp_dateofjoin = oEmployeeJobDetails.emp_dateofjoin,
                        emp_prop_confirmation_date = oEmployeeJobDetails.emp_prop_confirmation_date,
                        emp_id = oEmployeeJobDetails.emp_id,
                        emp_supervisor = oEmployeeJobDetails.emp_supervisor,
                        attendance_policy_id = oEmployeeJobDetails.attendance_policy_id,
                        emp_reporting_method = oEmployeeJobDetails.emp_reporting_method
                    };
                      bool irepoUpdate = employeejobdetailsRepository.UpdateEmployeeJobDetails(updateEmployeeJobDetails);
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "success", msg = "Employee Job Details Update successfully" }, formatter);
                }
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);

            }


        }

          [HttpDelete]
          public HttpResponseMessage Delete([FromBody] Models.hr_emp_job_details employeejobdetails)//, [FromBody] Models.user user
          {
              try
              {
                  bool deleteEmployeeJobDetails = employeejobdetailsRepository.DeleteEmployeeJobDetails(employeejobdetails.emp_job_details_id);

                  var formatter = RequestFormat.JsonFormaterString();
                  return Request.CreateResponse(HttpStatusCode.OK,
                      new Confirmation { output = "success", msg = "Employee Job details Delete Successfully." }, formatter);



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
