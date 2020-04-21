using ERPApi.Models;
using ERPApi.Models.IRepository;
using ERPApi.Models.Repository;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERPApi.Controllers
{
    public class CountryPartialController : ApiController
    {
        //
        // GET: /CountryPartial/
        private ICountryRepository countryRepository;

        public CountryPartialController()
        {
            this.countryRepository = new CountryRepository();
        }

        public CountryPartialController(ICountryRepository countryRepository)
        {
            this.countryRepository = countryRepository;
        }

        public HttpResponseMessage GetAllRBOmaster()
        {
            var countries = countryRepository.GetAllRBOMappingmaster();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, countries, formatter);
        }
    }
}