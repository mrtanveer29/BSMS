using ERPApi.Models;
using ERPApi.Models.IRepository;
using ERPApi.Models.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web;
using System.Globalization;

namespace ERPApi.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EmployeeController : ApiController
    {
        private IEmployeeRepository employeeRepository;

        public EmployeeController()
        {
            this.employeeRepository = new EmployeeRepository();

        }

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        [ActionName("GetAllEmployees")]
        [HttpGet]
        public HttpResponseMessage GetAllEmployees(int company_id)
        {
            var designations = employeeRepository.GetAllEmployees(company_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, designations, formatter);
        } 
        [HttpGet]
        public HttpResponseMessage GetAllDrivers(int company_id)
        {
            var data = employeeRepository.GetAllDrivers(company_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, data, formatter);
        }
        [HttpGet]
        public HttpResponseMessage GetAllCounterManForDropdown(int company_id)
        {
            var data = employeeRepository.GetAllCounterManForDropdown(company_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, data, formatter);
        }
        [ActionName("GetAllEmployeesbyBranch")]  // Created on 10.18.2017 by Tanveer
        [HttpGet]
        public HttpResponseMessage GetAllEmployeesbyBranch(int company_id,int branch_id ,int role_id)
        {
            var designations = employeeRepository.GetAllEmployeesbyBranch(company_id,branch_id, role_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, designations, formatter);
        }
        [Route("Employee/GetEmployeeByRoleType?source_type={source_type}")]
        public HttpResponseMessage GetEmployeeByRoleType(string source_type)
        {
            var employees = employeeRepository.GetEmployeeByRoleType(source_type);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, employees, formatter);
        }

        [Route("Employee/GetAllEmpByRole?role_id={role_id}")]
        public HttpResponseMessage GetAllEmpByRole(string role_id)
        {
            var designers = employeeRepository.GetAllDesigners(role_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, designers, formatter);
        }

    
        [Route("Employee/GetEmployeeByID?employee_id={employee_id}")]
        public HttpResponseMessage GetEmployeeByID(int employee_id)
        {
            var designations = employeeRepository.GetEmployeeByID(employee_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, designations, formatter);
        }

        [HttpGet]
        public HttpResponseMessage GetEmployeeInformationById(int employee_id)
        {
            var createuser = employeeRepository.GetEmployeeInformationById(employee_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, createuser, formatter);
        }

        [Route("Employee/GetAllEmployeesForDrpdown?company_id={company_id}")]
        public HttpResponseMessage GetAllEmployeesForDrpdown(int company_id)
        {
            var designations = employeeRepository.GetAllEmployeesForDrpdown(company_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, designations, formatter);
        }
        [Route("Employee/Getrole_nameByID?role_id={role_id}")]
        public HttpResponseMessage Getrole_nameByID(int role_id)
        {
            var designations = employeeRepository.Getrole_nameByID(role_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, designations, formatter);
        }

        #region for multiple get & for dynamic form



        [Route("Employee/GetEmployeeByPayrollSalaryID?salary_head_permission={salary_head_permission}")]
        public HttpResponseMessage GetEmployeeByPayrollSalaryID(string salary_head_permission)
        {
            var designations = employeeRepository.GetEmployeeByPayrollSalaryID(salary_head_permission);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, designations, formatter);
        }
        #endregion

        //[Route("Employee/GetEmployeeByPayrollSalaryID")]
        //public HttpResponseMessage GetEmployeeByPayrollSalaryID(int a)
        //{
        //    a = 0;
        //    var designations = employeeRepository.GetEmployeeByPayrollSalaryID(a);
        //    var formatter = RequestFormat.JsonFormaterString();
        //    return Request.CreateResponse(HttpStatusCode.OK, designations, formatter);
        //}


        [HttpPost]
        public HttpResponseMessage Post()
        {
            //image upload
            System.Web.HttpRequest rsk = System.Web.HttpContext.Current.Request;

            //int emp_id = int.Parse(rsk.Form["emp_id"].ToString());
            string emp_firstname = rsk.Form["emp_firstname"].ToString();
            string emp_lastname = rsk.Form["emp_lastname"].ToString();
            string emp_dateofbirth = rsk.Form["emp_dateofbirth"].ToString();
            string emp_gender = rsk.Form["emp_gender"].ToString();
            string emp_blood_group = rsk.Form["emp_blood_group"].ToString();
            string emp_marital_status = rsk.Form["emp_marital_status"].ToString();
            string emp_id_type = rsk.Form["emp_id_type"].ToString();
            string emp_id_no = rsk.Form["emp_id_no"].ToString();
            string company_id = rsk.Form["company_id"].ToString();
            //string emp_image_file_name = rsk.Form["emp_image_file_name"].ToString();
            string branch_id = rsk.Form["branch_id"].ToString();
            string unit_id = rsk.Form["unit_id"].ToString();
            string department_id = rsk.Form["department_id"].ToString();
            string designation_id = rsk.Form["designation_id"].ToString();
            string staffgrade_id = rsk.Form["staffgrade_id"].ToString();
            string emp_start_date = rsk.Form["emp_start_date"].ToString();
            string emp_code = rsk.Form["emp_code"].ToString();
            string job_location_id = rsk.Form["job_location_id"].ToString();
            string subsection_id = rsk.Form["subsection_id"].ToString();
            string emp_dateofjoin = rsk.Form["emp_dateofjoin"].ToString();
            string emp_prop_confirmation_date = rsk.Form["emp_prop_confirmation_date"].ToString();
            string emp_supervisor = rsk.Form["emp_supervisor"].ToString();
            string emp_reporting_method = rsk.Form["emp_reporting_method"].ToString();
            string attendance_policy_id = rsk.Form["attendance_policy_id"].ToString();
            string emp_id_Job = rsk.Form["emp_id_Job"].ToString();

            //split space from emp_code
            string test = emp_code;
            string[] ArrayOfValues = test.Split(' ');




            if (string.IsNullOrEmpty(emp_firstname))
            {
                var format_type = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK,
                    new Confirmation { output = "error", msg = "First Name can not be empty." });
            }
            else if (string.IsNullOrEmpty(emp_lastname))
            {
                var format_type = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK,
                    new Confirmation { output = "error", msg = "Last Name can not be empty." });
            }
            else if (string.IsNullOrEmpty(emp_gender))
            {
                var format_type = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK,
                    new Confirmation { output = "error", msg = "Gender can not be empty." });
            }

            else if (string.IsNullOrEmpty(branch_id))
                {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "Branch Name can not be empty." });
                }
                else if (string.IsNullOrEmpty(department_id))
                {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "Department Name can not be empty." });
                }

                
                else if (string.IsNullOrEmpty(emp_dateofjoin))
                {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "Joining Date can not be empty." });
                }
               else if (string.IsNullOrEmpty(emp_code))
               {
                   var format_type = RequestFormat.JsonFormaterString();
                   return Request.CreateResponse(HttpStatusCode.OK,
                       new Confirmation { output = "error", msg = "Employee Code can not be empty." });
               }
             
              else if (ArrayOfValues.Count() > 1)
               {
                   var format_type = RequestFormat.JsonFormaterString();
                   return Request.CreateResponse(HttpStatusCode.OK,
                       new Confirmation { output = "error", msg = "Employee Code can not have space." });
               }



               else
               {
                   if (employeeRepository.CheckDuplicateEmpCode(emp_code))
                   {

                       var formatter = RequestFormat.JsonFormaterString();
                       return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Employee Code Already Exists" }, formatter);

                   }
                   else
                   {

                       var httpPostedFile = rsk.Files["UploadedImage"];
                       
                       string ActualFileName = "";
                       //if (httpPostedFile == null)
                       //{
                       //    var formatter = RequestFormat.JsonFormaterString();
                       //    return Request.CreateResponse(HttpStatusCode.OK,
                       //                    new Confirmation { output = "error", msg = "Image file empty." }, formatter);
                       //}

                       //else
                       //{
                       /** save the File to Server Path **/
                       if (httpPostedFile != null)
                       {
                           ActualFileName = rsk.Files["UploadedImage"].FileName;
                           var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Images/Uploads/"), ActualFileName);

                           bool checkFileSave = false;
                           try
                           {
                               // Save the uploaded file to "UploadedFiles" folder
                               httpPostedFile.SaveAs(fileSavePath);
                               /** end Save file to Server path */
                               checkFileSave = true;
                           }
                           catch
                           {
                               checkFileSave = false;
                           }
                       }

                       int companyid = Convert.ToInt32(company_id);
                       if (branch_id == null || branch_id == "") { branch_id = "0";}
                       if (department_id == null || department_id == "") { department_id = "0"; }
                       if (designation_id == null || designation_id == "") { designation_id = "0"; }
                       if (staffgrade_id == null || staffgrade_id == "") { staffgrade_id = "0"; }
                       if (unit_id == null || unit_id == "") { unit_id = "0"; }
                   
                       if (subsection_id == null || subsection_id == "") { subsection_id = "0"; }
                       if (attendance_policy_id == null || attendance_policy_id == "") { attendance_policy_id = "0"; }

                       /** insert record to database **/
                       Models.StronglyType.EmployeeModel insertEmployee = new Models.StronglyType.EmployeeModel
                       {
                           emp_firstname = emp_firstname,
                           emp_lastname = emp_lastname,
                           emp_marital_status = emp_marital_status,
                           emp_blood_group = emp_blood_group,
                           emp_gender = emp_gender,
                           emp_dateofbirth = emp_dateofbirth,
                           emp_image_file_name = ActualFileName,
                           emp_id_type = emp_id_type,
                           emp_id_no = emp_id_no,
                           company_id = int.Parse(company_id),
                           branch_id = int.Parse(branch_id),
                        
                           department_id = int.Parse(department_id),
                           designation_id = int.Parse(designation_id),
                          
                           emp_code = emp_code,
                           emp_dateofjoin = emp_dateofjoin,
                           emp_prop_confirmation_date = emp_prop_confirmation_date,
                           emp_supervisor = emp_supervisor,
                         
                           emp_reporting_method = emp_reporting_method

                       };
                       int insert_employee = employeeRepository.InsertEmployee(insertEmployee);
                       var formatter = RequestFormat.JsonFormaterString();

                       return Request.CreateResponse(HttpStatusCode.OK,
                                     new Confirmation { output = "success", msg = "Employee  is saved succesfully.", returnvalue = insert_employee }, formatter);
                   }
                   //}
               }
        }

        [ActionName("UpdateEmployee")]
        [HttpPut]
        public HttpResponseMessage UpdateEmployee()
        {


            System.Web.HttpRequest rsk = System.Web.HttpContext.Current.Request;

            int emp_id = int.Parse(rsk.Form["emp_id"].ToString());
            string emp_firstname = rsk.Form["emp_firstname"].ToString();
            string emp_lastname = rsk.Form["emp_lastname"].ToString();
            string emp_dateofbirth = rsk.Form["emp_dateofbirth"].ToString();
            string emp_gender = rsk.Form["emp_gender"].ToString();
            string emp_blood_group = rsk.Form["emp_blood_group"].ToString();
            string emp_marital_status = rsk.Form["emp_marital_status"].ToString();
            string emp_id_type = rsk.Form["emp_id_type"].ToString();
            string emp_id_no = rsk.Form["emp_id_no"].ToString();
            string company_id = rsk.Form["company_id"].ToString();


            if (string.IsNullOrEmpty(emp_firstname))
            {
                var format_type = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK,
                    new Confirmation { output = "error", msg = "First Name can not be empty." });
            }
            else if (string.IsNullOrEmpty(emp_lastname))
            {
                var format_type = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK,
                    new Confirmation { output = "error", msg = "Last Name can not be empty." });
            }
            else if (string.IsNullOrEmpty(emp_gender))
            {
                var format_type = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK,
                    new Confirmation { output = "error", msg = "Gender can not be empty." });
            }

            else
            {
                var httpPostedFile = rsk.Files["UploadedImage"];
                string ActualFileName = "";
                if (httpPostedFile == null)
                {
                    Models.employee updateEmployee = new Models.employee
                    {
                        emp_id = emp_id,
                        emp_firstname = emp_firstname,
                        emp_lastname = emp_lastname,
                        emp_marital_status = emp_marital_status,
                        emp_blood_group = emp_blood_group,
                        emp_gender = emp_gender,
                        emp_dateofbirth = emp_dateofbirth,
                       // emp_image_file_name = "",
                        emp_id_type = emp_id_type,
                        emp_id_no = emp_id_no,
                        company_id = int.Parse(company_id)

                    };


                    bool irepoUpdate = employeeRepository.UpdateEmployee(updateEmployee);
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "success", msg = "Employee Update successfully" }, formatter);
                    //var formatter = RequestFormat.JsonFormaterString();
                    //return Request.CreateResponse(HttpStatusCode.OK,
                    //                new Confirmation { output = "error", msg = "Employee  is not Update succesfully." }, formatter);
                }

                else
                {
                    ActualFileName = rsk.Files["UploadedImage"].FileName;
                    var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Images/Uploads/"), ActualFileName);

                    bool checkFileSave = false;
                    try
                    {
                        // Save the uploaded file to "UploadedFiles" folder
                        httpPostedFile.SaveAs(fileSavePath);
                        /** end Save file to Server path */
                        checkFileSave = true;
                    }
                    catch
                    {
                        checkFileSave = false;
                    }


                    Models.employee updateEmployee = new Models.employee
                    {
                        emp_id = emp_id,
                        emp_firstname = emp_firstname,
                        emp_lastname = emp_lastname,
                        emp_marital_status = emp_marital_status,
                        emp_blood_group = emp_blood_group,
                        emp_gender = emp_gender,
                        emp_dateofbirth = emp_dateofbirth,
                        emp_image_file_name = ActualFileName,
                        emp_id_type = emp_id_type,
                        emp_id_no = emp_id_no,
                        company_id = int.Parse(company_id)

                    };


                    bool irepoUpdate = employeeRepository.UpdateEmployee(updateEmployee);
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "success", msg = "Employee Update successfully" }, formatter);

                }

            }

        }


        [HttpDelete]
        public HttpResponseMessage Delete([FromBody] Models.employee employee)//, [FromBody] Models.user user
        {
            try
            {
                bool deleteEmployee = employeeRepository.DeleteEmployee(employee.emp_id);

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK,
                    new Confirmation { output = "success", msg = "Employee Delete Successfully." }, formatter);



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



