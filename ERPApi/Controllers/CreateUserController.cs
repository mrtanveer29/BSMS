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
    public class CreateUserController : ApiController
    {
        private ICreateUserRepository createuserRepository;

        public CreateUserController()
        {
            this.createuserRepository = new CreateUserRepository();
        }

        public CreateUserController(ICreateUserRepository createuserRepository)
        {
            this.createuserRepository = createuserRepository;
        }

        public HttpResponseMessage GetAllCreateUser()
        {
            var createuser = createuserRepository.GetAllCreateUser();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, createuser, formatter);
        }

        [Route("CreateUser/GetEmployeeByCreateUser?user_id={user_id}")]
        public HttpResponseMessage GetEmployeeByCreateUser(int user_id)
        {
            var createuser = createuserRepository.GetEmployeeByCreateUser(user_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, createuser, formatter);
        }

        [Route("CreateUser/GetAllEmployeeCodes?fake_params={1}")]
        public HttpResponseMessage GetAllEmployeeCodes(int fake_params)
        {
            var createuser = createuserRepository.GetEmployees();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, createuser, formatter);
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody]Models.user oCreateUser)
        {
            try
            {
                if (oCreateUser.password != oCreateUser.confirm_password)
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Password don't match" }, formatter);
                }
                else
                {
                    Models.user insertUser = new Models.user
                    {
                        user_name = oCreateUser.user_name,
                        password = oCreateUser.password,
                        role_id = 50,
                        employee_id = oCreateUser.employee_id,
                        role_type_id = 1,
                        company_id = oCreateUser.company_id,
                        confirm_password = oCreateUser.confirm_password
                    };
                    bool save_user = createuserRepository.InsertCreateUser(insertUser);
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "User save successfully" }, formatter);
                }
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [HttpPut]
        public HttpResponseMessage Put([FromBody]Models.user oCreateUser, int user_id)
        {
            try
            {
                if (oCreateUser.password != oCreateUser.confirm_password)
                {
                    var formatter1 = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Password don't match" }, formatter1);
                }
                else
                {
                    Models.user updateCity = new Models.user

                    {
                        user_id = user_id,
                        user_name = oCreateUser.user_name,
                        password = oCreateUser.password,
                        role_id = oCreateUser.role_id,
                        employee_id = oCreateUser.employee_id,
                        role_type_id = oCreateUser.role_type_id,
                        company_id = oCreateUser.company_id,
                        confirm_password = oCreateUser.confirm_password
                    };

                    bool irepoUpdate = createuserRepository.UpdateCreateUser(updateCity);

                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "User Update successfully" }, formatter);
                }
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [HttpDelete]
        public HttpResponseMessage Delete([FromBody]Models.user oCreateUser)
        {
            try
            {
                bool updateUser = createuserRepository.DeleteCreateUser(oCreateUser.user_id);
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "User Delete Successfully." }, formatter);
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }
    }
}