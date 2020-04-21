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
    public class DepartmentController : ApiController
    {
        private IDepartmentRepository departmentRepository;

        public DepartmentController()
        {
            this.departmentRepository = new DepartmentRepository();
        }

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }
        
        public HttpResponseMessage GetAllDepartments()
        {
            var departments = departmentRepository.GetAllDepartments();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, departments, formatter);
        }

        public HttpResponseMessage GetAllDpmts(int companyid, int branchid)
        {
            var departments = departmentRepository.GetAllDpmts(companyid, branchid);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, departments, formatter);
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage Post([FromBody]Models.department department)
        {
            try
            {
                if (string.IsNullOrEmpty(department.department_name))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Department Name is Empty" }, formatter);
                }
                else
                {
                    if (departmentRepository.CheckDepartmentForDuplicateByname(department.department_name))
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Department Already Exists" }, formatter);
                    }
                    else
                    {
                        Models.department insertDepartment = new Models.department
                        {
                            department_name = department.department_name,
                            department_abbreviation = department.department_abbreviation,
                            parent_department_id = department.parent_department_id,
                            incharge_employee_id = department.incharge_employee_id,
                            location = department.location,
                            is_active = department.is_active,
                            created_by = 1,
                            created_date = DateTime.Now.ToString(),
                            updated_by = 1,
                            updated_date = DateTime.Now.ToString(),
                            company_id = 12
                        };
                        bool save_department = departmentRepository.InsertDepartment(insertDepartment);

                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Department save successfully" }, formatter);
                    }
                }
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [System.Web.Http.HttpPut]
        public HttpResponseMessage Put([FromBody]Models.department department)
        {
            try
            {
                if (string.IsNullOrEmpty(department.department_name))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Department Name is Empty" }, formatter);
                }
                else
                {
                    Models.department updateDepartment = new Models.department
                    {
                        department_id = department.department_id,
                        department_name = department.department_name,
                        department_abbreviation = department.department_abbreviation,
                        parent_department_id = department.parent_department_id,
                        incharge_employee_id = department.incharge_employee_id,
                        location = department.location,
                        is_active = department.is_active,
                        updated_by = 1,
                        updated_date = DateTime.Now.ToString(),
                        company_id = 12
                    };

                    bool departmentUpdate = departmentRepository.UpdateDepartment(updateDepartment);

                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Department Update successfully" }, formatter);
                }
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [System.Web.Http.HttpDelete]
        public HttpResponseMessage Delete([FromBody]Models.department department)
        {
            try
            {
                bool deleteDepartment = departmentRepository.DeleteDepartment(department.department_id);

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Department Delete Successfully." }, formatter);
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }
    }
}