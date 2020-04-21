using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using ERPApi.Models;
using ERPApi.Models.IRepository;
using ERPApi.Models.Repository;

namespace ERPApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PasswordController : ApiController
    {
        public IPasswordRepository PasswordRepository;

        public PasswordController(IPasswordRepository passwordRepository)
        {
            PasswordRepository = passwordRepository;
        }

        public PasswordController()
        {
            PasswordRepository=new PasswordRepository();
        }

       
        [HttpGet]
        public HttpResponseMessage ChangePassword(string userName,string oldPass,string updatedPassword ,string rePass)
        {
            object message = PasswordRepository.ChangePassword(userName, oldPass, updatedPassword, rePass);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, message, formatter);
        }
    }
}