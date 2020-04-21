using ERPApi.Models;
using ERPApi.Models.IRepository;
using ERPApi.Models.Repository;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ERPApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DepartmentPartialController : ApiController
    {
        private IDepartmentRepository departmentRepository;

        public DepartmentPartialController()
        {
            this.departmentRepository = new DepartmentRepository();
        }

        public DepartmentPartialController(IDepartmentRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }

        public HttpResponseMessage GetAllDepartments()
        {
            var departments = departmentRepository.GetAllDepartmentsOnly();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, departments, formatter);
        }
    }
}