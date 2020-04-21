using ERPApi.Models;
using ERPApi.Models.IRepository;
using ERPApi.Models.Repository;
using ERPApi.Models.StronglyType;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ERPApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserPermissionPartialController : ApiController
    {
        private IUserPermissionRepository userPermissionRepository;
        private IControlRepository controlRepository;

        public UserPermissionPartialController()
        {
            this.userPermissionRepository = new UserPremissionRepository();
            this.controlRepository = new ControlRepository();
        }

        public UserPermissionPartialController(IUserPermissionRepository userPermissionRepository,
            IControlRepository controlRepository)
        {
            this.userPermissionRepository = userPermissionRepository;
            this.controlRepository = controlRepository;
        }

        [HttpPost]
        public HttpResponseMessage GetFormPermissionMenuRole([FromBody]Models.StronglyType.UserPermissionRoleUserIDModel userPermissionRoleUserIdModel)
        {
            try
            {
                var controlList = controlRepository.GetAllControlForPermission();
                var userPermissionList = userPermissionRepository.GetAllUserPermissionByRoleId(userPermissionRoleUserIdModel.role_id, userPermissionRoleUserIdModel.user_au_id);

                List<UserPermissionModel> models = new List<UserPermissionModel>();
                if (userPermissionRoleUserIdModel.role_id == null)
                {
                    foreach (control con in controlList)
                    {
                        UserPermissionModel tempUserPermission = new UserPermissionModel();
                        tempUserPermission.control_id = con.control_id;
                        tempUserPermission.control_name = con.control_name;
                        tempUserPermission.control_type_id = con.control_type_id;
                        tempUserPermission.control_parent_id = con.control_parent_id;
                        tempUserPermission.control_controller = con.control_controller;
                        tempUserPermission.control_action = con.control_action;
                        tempUserPermission.control_status = false;
                        tempUserPermission.icon = con.icon;
                        tempUserPermission.control_alias = con.control_alias;
                        models.Add(tempUserPermission);
                    }
                }
                else
                {
                    foreach (control con in controlList)
                    {
                        UserPermissionModel tempUserPermission = new UserPermissionModel();
                        tempUserPermission.control_id = con.control_id;
                        tempUserPermission.control_name = con.control_name;
                        tempUserPermission.control_type_id = con.control_type_id;
                        tempUserPermission.control_parent_id = con.control_parent_id;
                        tempUserPermission.control_controller = con.control_controller;
                        tempUserPermission.control_action = con.control_action;
                        tempUserPermission.icon = con.icon;
                        tempUserPermission.control_alias = con.control_alias;
                        foreach (user_permission usp in userPermissionList)
                        {
                            if (usp.user_control_id == con.control_id)
                            {
                                tempUserPermission.control_status = true;
                            }
                        }
                        models.Add(tempUserPermission);
                    }
                }

                var format_type = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, models, format_type);
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetFormPermissionMenuRole(string role_id, string user_id)
        {
            try
            {
                var controlList = controlRepository.GetAllControlForPermission();
                var userPermissionList = userPermissionRepository.GetAllUserPermissionByRoleId(int.Parse(role_id), int.Parse(user_id));
                if (userPermissionList.Count != 0)
                {
                    List<UserPermissionModel> models = new List<UserPermissionModel>();
                    if (role_id == null)
                    {
                        foreach (control con in controlList)
                        {
                            UserPermissionModel tempUserPermission = new UserPermissionModel();
                            tempUserPermission.control_id = con.control_id;
                            tempUserPermission.control_name = con.control_name;
                            tempUserPermission.control_type_id = con.control_type_id;
                            tempUserPermission.control_parent_id = con.control_parent_id;
                            tempUserPermission.control_status = false;
                            tempUserPermission.control_controller = con.control_controller;
                            tempUserPermission.control_action = con.control_action;
                            tempUserPermission.icon = con.icon;
                            tempUserPermission.control_alias = con.control_alias;
                            models.Add(tempUserPermission);
                        }
                    }
                    else
                    {
                        foreach (control con in controlList)
                        {
                            UserPermissionModel tempUserPermission = new UserPermissionModel();
                            tempUserPermission.control_id = con.control_id;
                            tempUserPermission.control_name = con.control_name;
                            tempUserPermission.control_type_id = con.control_type_id;
                            tempUserPermission.control_parent_id = con.control_parent_id;
                            tempUserPermission.control_controller = con.control_controller;
                            tempUserPermission.control_action = con.control_action;
                            tempUserPermission.icon = con.icon;
                            tempUserPermission.control_alias = con.control_alias;
                            foreach (user_permission usp in userPermissionList)
                            {
                                if (usp.user_control_id == con.control_id)
                                {
                                    tempUserPermission.control_status = true;
                                }
                            }
                            models.Add(tempUserPermission);
                        }
                    }

                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, models, format_type);
                }
                else
                {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "there is no permission from this role" }, format_type);
                }
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }
        [HttpGet]
        public HttpResponseMessage GetFormPermissionMenuRoleByRoleID(string role_id)
        {
            try
            {
                var controlList = controlRepository.GetAllControlForPermission();
                var userPermissionList = userPermissionRepository.GetAllUserPermissionByRoleIdOnly(int.Parse(role_id));
                List<UserPermissionModel> models = new List<UserPermissionModel>();
                if (role_id == null)
                {
                    foreach (control con in controlList)
                    {
                        UserPermissionModel tempUserPermission = new UserPermissionModel();
                        tempUserPermission.control_id = con.control_id;
                        tempUserPermission.control_name = con.control_name;
                        tempUserPermission.control_type_id = con.control_type_id;
                        tempUserPermission.control_parent_id = con.control_parent_id;
                        tempUserPermission.control_status = false;
                        tempUserPermission.control_controller = con.control_controller;
                        tempUserPermission.control_action = con.control_action;
                        tempUserPermission.icon = con.icon;
                        tempUserPermission.control_alias = con.control_alias;
                        models.Add(tempUserPermission);
                    }
                }
                else
                {
                    foreach (control con in controlList)
                    {
                        UserPermissionModel tempUserPermission = new UserPermissionModel();
                        tempUserPermission.control_id = con.control_id;
                        tempUserPermission.control_name = con.control_name;
                        tempUserPermission.control_type_id = con.control_type_id;
                        tempUserPermission.control_parent_id = con.control_parent_id;
                        tempUserPermission.control_controller = con.control_controller;
                        tempUserPermission.control_action = con.control_action;
                        tempUserPermission.icon = con.icon;
                        tempUserPermission.control_alias = con.control_alias;
                        foreach (user_permission usp in userPermissionList)
                        {
                            if (usp.user_control_id == con.control_id)
                            {
                                tempUserPermission.control_status = true;
                            }
                        }
                        models.Add(tempUserPermission);
                    }
                }

                var format_type = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, models, format_type);
                
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }
    }
}