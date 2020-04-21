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
    public class RolePartialController : ApiController
    {
        //
        // GET: /RolePartial/
        private IRoleRepository roleRepository;

        public RolePartialController()
        {
            this.roleRepository = new RoleRepository();
        }

        public RolePartialController(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public HttpResponseMessage GetAllRolesByType()
        {
            var roles = roleRepository.GetAllRolesByType();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, roles, formatter);
        }

        [HttpGet]
        [Route("RolePartial/GetRolenameByID?emp_id={emp_id}")] 
        public HttpResponseMessage GetRolenameByID(int emp_id)            //// GetRolenameByID([FromBody]Models.employee oEmployee)
        {
            var employee = roleRepository.GetRolenameByID(emp_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, employee, formatter);
        }
    }
}