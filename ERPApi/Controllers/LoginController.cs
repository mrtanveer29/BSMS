using ERPApi.Models;
using ERPApi.Models.IRepository;
using ERPApi.Models.Repository;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ERPApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LoginController : ApiController
    {
        private IEmployeeRepository employeeRepository;
        private ILoginRepository loginRepository;

        public LoginController()
        {
            this.employeeRepository = new EmployeeRepository();
            this.loginRepository = new LoginRepository();
        }

        [HttpPost]
        public HttpResponseMessage GetUserLogin([FromBody] Models.StronglyType.EmployeeUserModel oemployee)
        {
            try
            {
                if (string.IsNullOrEmpty(oemployee.user_name))
                {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "User Name can not be empty" });
                }
                if (string.IsNullOrEmpty(oemployee.password))
                {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "password can not be empty" });
                }
                else
                {
                    var login = loginRepository.LoginInformation(oemployee.user_name, oemployee.password);

                    if (login != null)
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Login Successfully", returnvalue = login }, formatter);
                    }
                    else
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK,
                            new Confirmation { output = "error", msg = "Please enter valid username or password" }, formatter);
                    }
                }
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Login is not succesfull" }, formatter);
            }
        }
        [HttpPost]
        public HttpResponseMessage UserLogin(string user_name, string password)
        {
            var data = loginRepository.UserLogin(user_name, password);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, data, formatter);
        }
    }
}