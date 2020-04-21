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
    public class CurrencyController : ApiController
    {
        private ICurrencyRepository currencyRepository;

        public CurrencyController()
        {
            this.currencyRepository = new CurrencyRepository();
        }

        public CurrencyController(ICurrencyRepository currencyRepository)
        {
            this.currencyRepository = currencyRepository;
        }
        [ActionName("GetAllCurrencyForCompany")]
        [HttpGet]
        public HttpResponseMessage GetAllCurrencyForCompany()
        {
            List<currency> data = currencyRepository.GetAllCurrencyForCompany();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, data, formatter);
        }

        [ActionName("GetAllCurrency")]
        [HttpGet]
        public HttpResponseMessage GetAllCurrency(int company_id)
        {
            var data = currencyRepository.GetAllCurrency(company_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, data, formatter);
        }

        [ActionName("GetDefaultCurrencyByCompanyId")]
        [HttpGet]
        public HttpResponseMessage GetDefaultCurrencyByCompanyId(int company_id)
        {
            var data = currencyRepository.GetDefaultCurrencyByCompanyId(company_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, data, formatter);
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage Post([FromBody] currency currency)
        {
            try
            {
                if (string.IsNullOrEmpty(currency.currency_name))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Currency Name is Empty" }, formatter);
                }
                if (string.IsNullOrEmpty(currency.currency_symbol))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Currency Symbol is Empty" }, formatter);
                }
                else
                {
                    if (currencyRepository.CheckDuplicateCurrency(currency, "add"))
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Currency Name Already Exists" }, formatter);
                    }
                    else
                    {
                        currency obj = new currency
                        {
                            currency_name = currency.currency_name,
                            currency_symbol = currency.currency_symbol,
                            is_active = currency.is_active,
                            is_default = currency.is_default,
                            created_by = currency.created_by,
                            created_date = DateTime.Now,
                            company_id = currency.company_id
                        };
                        bool save = currencyRepository.InsertCurrency(obj);

                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Currency save successfully" }, formatter);
                    }
                }
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [System.Web.Http.HttpPut]
        public HttpResponseMessage Put([FromBody]currency currency)
        {
            try
            {
                if (string.IsNullOrEmpty(currency.currency_name))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Currency Name is Empty" }, formatter);
                }
                if (string.IsNullOrEmpty(currency.currency_symbol))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Currency Symbol is Empty" }, formatter);
                }
                else
                {
                    if (currencyRepository.CheckDuplicateCurrency(currency, "edit"))
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Currency Name Already Exists" }, formatter);
                    }
                    else
                    {
                        currency update = new currency

                        {
                            currency_id = currency.currency_id,
                            currency_name = currency.currency_name,
                            currency_symbol = currency.currency_symbol,
                            is_active = currency.is_active,
                            is_default = currency.is_default,
                            updated_by = currency.created_by,
                            updated_date = DateTime.Now
                        };

                        bool irepoUpdate = currencyRepository.UpdateCurrency(update);

                        if (irepoUpdate == true)
                        {
                            var formatter = RequestFormat.JsonFormaterString();
                            return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Currency Update successfully" }, formatter);
                        }
                        else
                        {
                            var formatter = RequestFormat.JsonFormaterString();
                            return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Update Failed" }, formatter);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [System.Web.Http.HttpDelete]
        public HttpResponseMessage Delete([FromBody] currency currency)
        {
            try
            {
                bool updat = currencyRepository.DeleteCurrency(currency.currency_id);

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Currency Delete Successfully." }, formatter);
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }
    }
}