﻿using ERPApi.Models;
using ERPApi.Models.IRepository;
using ERPApi.Models.Repository;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ERPApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RoleController : ApiController
    {
        private IRoleRepository roleRepository;

        public RoleController()
        {
            this.roleRepository = new RoleRepository();
        }

        public RoleController(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        [ActionName("GetAllRoles")]
        [HttpGet]
        public HttpResponseMessage GetAllRoles(int companyId)
        {
            var roles = roleRepository.GetAllRoles(companyId);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, roles, formatter);
        }
        [ActionName("GetAssignedRoleForUser")]
        [HttpGet]
        public HttpResponseMessage GetAssignedRoleForUser(int user_id)
        {
            var roles = roleRepository.GetAssignedRoleForUser(user_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, roles, formatter);
        }
        [ActionName("GetUnassignedRoleForUser")]
        [HttpGet]
        public HttpResponseMessage GetUnassignedRoleForUser(int user_id)
        {
            var roles = roleRepository.GetUnassignedRoleForUser(user_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, roles, formatter);
        }
        [ActionName("GetAllEmployeRoles")]
        [HttpGet]
        public HttpResponseMessage GetAllEmployeRoles(int companyId)
        {
            var roles = roleRepository.GetEmployeeRoleTypeBySource(companyId);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, roles, formatter);
        }

        [ActionName("GetAllRolesForEmpUser")]
        [HttpGet]
        public HttpResponseMessage GetAllRolesForEmpUser(int company_id)
        {
            var roles = roleRepository.GetAllRolesForEmpUser(company_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, roles, formatter);
        }
        [ActionName("GetRoleByRoleId")]
        [HttpGet]
        public HttpResponseMessage GetRoleByRoleId(int roleId)
        {
            var roles = roleRepository.GetRoleByRoleId(roleId);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, roles, formatter);
        }
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Post([FromBody]Models.role role, string company_id)
        {
          
            try
            {
                
                if (string.IsNullOrEmpty(role.role_name))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Role Name is Empty" }, formatter);
                }
                else
                {
                    if (roleRepository.CheckRoleForDuplicateByname(role.role_name,company_id))
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Role Already Exists" }, formatter);
                    }
                    else
                    {

                        int companyId = int.Parse(Request.GetQueryNameValuePairs().SingleOrDefault(com=>com.Key =="company_id").Value);
                      
                    
                        Models.role insertRole = new Models.role
                        {
                            role_name = role.role_name,
                            is_active = role.is_active,
                            created_by = 1,
                            created_date = DateTime.Now.ToString(),
                            updated_by = 1,
                            updated_date = DateTime.Now.ToString(),
                            company_id = companyId,
                            is_fixed = role.is_fixed,
                            role_type_id = role.role_type_id
                        };
                        bool save_role = roleRepository.InsertRole(insertRole);

                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Role save successfully" }, formatter);
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
        public HttpResponseMessage Put([FromBody]Models.role role)
        {
            try
            {
                if (string.IsNullOrEmpty(role.role_name))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Role Name is Empty" }, formatter);
                }
                else
                {
                    Models.role updateRole = new Models.role
                    {
                        role_id = role.role_id,
                        role_name = role.role_name,
                        is_active = role.is_active,
                        updated_by = 1,
                        updated_date = DateTime.Now.ToString(),
                        is_fixed = role.is_fixed,
                        role_type_id = role.role_type_id
                    };

                    bool roleUpdate = roleRepository.UpdateRole(updateRole);

                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Role Update successfully" }, formatter);
                }
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [System.Web.Http.HttpDelete]
        public HttpResponseMessage Delete([FromBody]Models.role role)
        {
            try
            {
                bool deleteRole = roleRepository.DeleteRole(role.role_id);

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Role Delete Successfully." }, formatter);
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }
    }
}