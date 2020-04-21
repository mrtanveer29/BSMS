using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using ERPApi.Models;
using ERPApi.Models.IRepository;
using ERPApi.Models.Repository;

namespace ERPApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BankController : ApiController
    {
        private IBankRepository bankRepository;
        public BankController()
        {
            this.bankRepository = new BankRepository();
        }
        public BankController(IBankRepository bankRepository)
        {
            this.bankRepository = bankRepository;
        }

        [ActionName("GetBankById")]
        [HttpGet]
        public HttpResponseMessage GetBankById(int bankId)
        {
            var cities = bankRepository.GetBankById(bankId);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, cities, formatter);
        }

        [ActionName("GetBankBySourceType")]
        [HttpGet]
        public HttpResponseMessage GetBankBySourceType(string sourceType)
        {
            var cities = bankRepository.GetBankBySourceType(sourceType);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, cities, formatter);
        }

        [ActionName("GetBankBySourceTypeAndId")]
        [HttpGet]
        public HttpResponseMessage GetBankBySourceTypeAndId(string sourceType, int sourceId)
        {
            var cities = bankRepository.GetBankBySourceTypeAndId(sourceType, sourceId);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, cities, formatter);
        }
    }
}