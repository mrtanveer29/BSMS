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
    public class AdminDashboardController : ApiController
     {
         public IAdminDashboardRepository adminDashboardRepository;

         public AdminDashboardController(IAdminDashboardRepository adminDashboardRepository)
         {
             this.adminDashboardRepository = adminDashboardRepository;
         }

         public AdminDashboardController()
         {
             adminDashboardRepository=new AdminDashboardRepository();
         }

         [HttpGet]
         public HttpResponseMessage GetAllAdminDashboardData(int company_id)
         {
             var data = adminDashboardRepository.GetAllAdminDashboardData(company_id);
             var formatter = RequestFormat.JsonFormaterString();
             return Request.CreateResponse(HttpStatusCode.OK, data, formatter);
         }

        
        
    }
}