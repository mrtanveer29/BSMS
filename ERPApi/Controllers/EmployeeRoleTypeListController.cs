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
    public class EmployeeRoleTypeListController : ApiController
    {
        //
        // GET: /EmployeeRoleTypeList/

        private IRoleRepository roleRepository;

        public EmployeeRoleTypeListController()
        {
            this.roleRepository = new RoleRepository();
        }

        public EmployeeRoleTypeListController(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public HttpResponseMessage GetAllSupplierRole()
        {
            var role = roleRepository.GetEmployeeRoleType();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, role, formatter);
        }
    }
}