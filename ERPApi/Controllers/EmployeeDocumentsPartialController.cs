using ERPApi.Models;
using ERPApi.Models.IRepository;
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
    public class EmployeeDocumentsPartialController : ApiController
    {
        private IEmployeeDocumentsRepository employeedocumentsRepository;
       
        [HttpPost, Route("EmployeeDocuments/EmployeeDoc")]
        public HttpResponseMessage EmployeeDoc()
        {
            string a = employeedocumentsRepository.Post();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK,
                new Confirmation { output = "success", msg = "Saved" });
        }

        [Route("EmployeeDocuments/AddEmployeeDoc")]
        public HttpResponseMessage AddEmployeeDoc(hr_emp_documents oemployee)
        {
            bool a = employeedocumentsRepository.AddEmployeeDocument(oemployee);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK,
            new Confirmation { output = "success", msg = "Saved" });
        }

        [HttpGet]
        public Object GetBankDocumentGrid(long? empId)
        {
            return employeedocumentsRepository.GetEmpDocumentGrid(empId);
        }
    }
}
