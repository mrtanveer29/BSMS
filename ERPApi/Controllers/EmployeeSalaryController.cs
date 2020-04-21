using ERPApi.Models;
using ERPApi.Models.IRepository;
using ERPApi.Models.Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    public class EmployeeSalaryController : ApiController
    {
        private IEmployeeSalaryRepository employeesalaryRepository;
        private IEmployeeRepository employeeRepository;

        public EmployeeSalaryController()
        {
            this.employeesalaryRepository = new EmployeeSalaryRepository();
            this.employeeRepository = new EmployeeRepository();
        }

        public EmployeeSalaryController(IEmployeeSalaryRepository employeesalaryRepository, IEmployeeRepository employeeRepository)
        {
            this.employeesalaryRepository = employeesalaryRepository;
            this.employeeRepository = employeeRepository;
        }

        public HttpResponseMessage GetAllEmployeeSalary()
        {

            var emps = employeesalaryRepository.GetAllEmployeeSalary();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, emps, formatter);

        }

        public HttpResponseMessage GetEmployeeSalaryByEmployeeID(int emp_id)
        {
            var emps = employeesalaryRepository.GetEmployeeSalaryByEmployeeID(emp_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, emps, formatter);
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody]object oEmployeeSalary, int emp_id)
        {

            string SingleArrayOfValues = oEmployeeSalary.ToString();
            dynamic jObj = (dynamic)JsonConvert.DeserializeObject(SingleArrayOfValues);
            dynamic data1 = JObject.Parse(SingleArrayOfValues);

            string emp_account_number = data1.emp_account_number;
            string emp_bank_title = data1.emp_bank_title;

            //update employee
            if (emp_account_number != "" || emp_bank_title != "")
            {
                Models.employee emp = new Models.employee
                {
                    emp_id = emp_id,
                    emp_account_number = emp_account_number,
                    emp_bank_title = emp_bank_title
                };
                bool save_emp_salary = employeeRepository.UpdateSalaryByEmloyee(emp);
            }



            //insert salary
            foreach (var salaryHeader in data1)
            {
                string test = salaryHeader.ToString();
                string[] ArrayOfValues1 = test.Split(':');
                string[] ArrayOfValues = test.Split('_');
                string val = ArrayOfValues[0];
                string[] v = val.Split('"');
                string a = v[1];

                string val1 = ArrayOfValues1[1];
                string[] v1 = val1.Split('"');
                string a1 = v1[1];
                //dynamic data = JObject.Parse(salaryHeader);

                if (a == "prop")
                {
                    int pid = int.Parse(ArrayOfValues[1]);
                    var salary_id = 1;
                    Models.hr_employee_salary insertEmployeeSalary = new Models.hr_employee_salary
                    {
                        emp_salary_info_id = pid,
                        salary_info = "",
                        salary_ammount = decimal.Parse(a1),
                        emp_id = emp_id
                    };
                    bool insert_salary_info = employeesalaryRepository.InsertEmployeeSalary(insertEmployeeSalary);

                }

            }
            var formatter2 = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Salary Information  is saved successfully." }, formatter2);



            var formatter3 = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, SingleArrayOfValues, formatter3);

        }


        [HttpPut]
        public HttpResponseMessage UpdateEmployeeSalary([FromBody]object oEmployeeSalary, int emp_id)
        {
          
            string SingleArrayOfValues = oEmployeeSalary.ToString();
            dynamic jObj = (dynamic)JsonConvert.DeserializeObject(SingleArrayOfValues);
            dynamic data1 = JObject.Parse(SingleArrayOfValues);

            string emp_account_number = data1.emp_account_number;
            string emp_bank_title = data1.emp_bank_title;
            bool irepoUpdate = false;

            //update employee
            if (emp_account_number != "" || emp_bank_title != "")
            {
                Models.employee emp = new Models.employee
                {
                    emp_id = emp_id,
                    emp_account_number = emp_account_number,
                    emp_bank_title = emp_bank_title
                };
                bool save_emp_salary = employeeRepository.UpdateSalaryByEmloyee(emp);
            }

            //insert salary
            foreach (var salaryHeader in data1)
            {
                string test = salaryHeader.ToString();
                string[] ArrayOfValues1 = test.Split(':');
                string[] ArrayOfValues = test.Split('_');
                string val = ArrayOfValues[0];
                string[] v = val.Split('"');
                string a = v[1];

                string val1 = ArrayOfValues1[1];
                string[] v1 = val1.Split('"');
                string a1 = v1[1];

                if (a == "prop")
                {
                    int pid = int.Parse(ArrayOfValues[1]);
 
                    Models.hr_employee_salary updateEmployeeSalary = new Models.hr_employee_salary
                    {
                        emp_salary_info_id = pid,
                        salary_ammount = decimal.Parse(a1),
                        emp_id = emp_id
                    };

                    irepoUpdate = employeesalaryRepository.UpdateEmployeeSalary(updateEmployeeSalary);

                }

            }
            if (irepoUpdate)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Salary Information update successfully" }, formatter);
            }
            else
            {


                var formatter2 = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Salary Information update failed" }, formatter2);
            }

        }

        [HttpDelete]
        public HttpResponseMessage Delete([FromBody]Models.hr_employee_salary oEmployeeSalary)
        {
            try
            {
                bool updateEmployeeSalary = employeesalaryRepository.DeleteEmployeeSalary(oEmployeeSalary.emp_salary_id);
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Salary name Delete Successfully." }, formatter);

            }

            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);

            }



        }

    }
}
