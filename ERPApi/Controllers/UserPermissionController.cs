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
    public class UserPermissionController : ApiController
    {
        private IUserPermissionRepository userPermissionRepository;
        private IControlRepository controlRepository;

        public UserPermissionController()
        {
            this.userPermissionRepository = new UserPremissionRepository();
            this.controlRepository = new ControlRepository();
        }

        public UserPermissionController(IUserPermissionRepository userPermissionRepository, IControlRepository controlRepository)
        {
            this.userPermissionRepository = userPermissionRepository;
            this.controlRepository = controlRepository;
        }

        [HttpGet, ActionName("GetUserByRoleId")]
        public HttpResponseMessage GetUserByRoleId()
        {
            var data = userPermissionRepository.GetUserByRoleId();
            var format_type = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, data, format_type);
        }
        [HttpPost]
        public HttpResponseMessage Post([FromBody]Models.StronglyType.UserPermissionModel userPermissionModel)
        {
            try
            {
                bool insertuserPermission = false;
                if (userPermissionModel.user_role_id == null && userPermissionModel.user_au_id == null)
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "Role is Empty Or User is Empty.Please select a role or user" }, formatter);
                }
                else
                {
                    if (userPermissionModel.user_au_id == null)
                    {
                        //int userid = int.Parse(userPermissionModel.user_au_id.ToString());
                        int roleid = int.Parse(userPermissionModel.user_role_id.ToString());
                        var uPermissionList = userPermissionRepository.GetAllUserPermissionByUserId(roleid);
                        if (uPermissionList.Count > 1)
                        {
                            userPermissionRepository.DeleteUserPermissionByUser(roleid, uPermissionList);
                        }
                    }
                    if (userPermissionModel.user_role_id != null && userPermissionModel.user_au_id !=null)
                    {
                        int roleid = int.Parse(userPermissionModel.user_role_id.ToString());
                        int userid = int.Parse(userPermissionModel.user_au_id.ToString());
                        var permissionList = userPermissionRepository.GetAllUserPermissionByRoleId(roleid, userid);
                        if (permissionList.Count > 1)
                        {
                            userPermissionRepository.DeleteUserPermissionByRole(roleid, permissionList);
                        }
                    }

                    foreach (string per in userPermissionModel.permissions)
                    {
                        decimal cont_id = Convert.ToDecimal(per);
                        Models.user_permission insert_userPermission = new user_permission
                        {
                            user_au_id = userPermissionModel.user_au_id,
                            user_control_id = int.Parse(per),
                            user_role_id = userPermissionModel.user_role_id,
                        };
                        insertuserPermission = userPermissionRepository.InsertUserPermission(insert_userPermission);
                    }
                    if (insertuserPermission == true)
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "User Permission is saved Successfully" }, formatter);
                    }
                    else
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Some thing Wrong with user permission entry" }, formatter);
                    }
                }
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Some thing Wrong with user permission entry" }, formatter);
            }
        }
    }
}