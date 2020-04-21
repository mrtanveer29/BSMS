using ERPApi.Models;
using ERPApi.Models.IRepository;
using ERPApi.Models.Repository;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ERPApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ControlTypeController : ApiController
    {
        private IControlTypeRepository controltypeRepository;

        public ControlTypeController()
        {
            this.controltypeRepository = new ControlTypeRepository();
        }

        public ControlTypeController(IControlTypeRepository controltypeRepository)
        {
            this.controltypeRepository = controltypeRepository;
        }

        public HttpResponseMessage GetAllControlTypes()
        {
            var control_type = controltypeRepository.GetAllControlTypes();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, control_type, formatter);
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody]Models.control_type control_type)
        {
            try
            {
                if (string.IsNullOrEmpty(control_type.control_type_name))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Control Type Name is Empty" }, formatter);
                }
                else
                {
                    if (controltypeRepository.CheckControlTypeForDuplicateByName(control_type.control_type_name))
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Control Type Name already exist" }, formatter);
                    }
                    else
                    {
                        Models.control_type insertControl_Type = new Models.control_type
                        {
                            control_type_name = control_type.control_type_name,
                            is_active = control_type.is_active,
                            created_by = 1,
                            created_date = DateTime.Now.ToString(),
                            updated_by = 1,
                            updated_date = DateTime.Now.ToString(),
                            company_id = control_type.company_id
                        };
                        bool save_control_type = controltypeRepository.InsertControleType(insertControl_Type);
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Control Type Name Save Successfully" }, formatter);
                    }
                }
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [HttpPut]
        public HttpResponseMessage Put([FromBody]Models.control_type control_type)
        {
            try
            {
                if (string.IsNullOrEmpty(control_type.control_type_name))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Control Type Name is Empty" }, formatter);
                }
                else
                {
                    Models.control_type updateControl_Type = new Models.control_type

                    {
                        control_type_id = control_type.control_type_id,
                        control_type_name = control_type.control_type_name,
                        is_active = control_type.is_active,
                        updated_by = 1,
                        updated_date = DateTime.Now.ToString(),
                        company_id = control_type.company_id
                    };

                    bool irepoUpdate = controltypeRepository.UpdateControlType(updateControl_Type);
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Control Type Update successfully" }, formatter);
                }
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [HttpDelete]
        public HttpResponseMessage Delete([FromBody]Models.control_type control_type)
        {
            try
            {
                if (control_type.control_type_id == 1 || control_type.control_type_id == 2 || control_type.control_type_id == 3)
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "You can not delete coustom control" }, formatter);
                }
                else
                {
                    bool updatCountry = controltypeRepository.DeleteControlType(control_type.control_type_id);

                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Control Type name Delete Successfully." }, formatter);
                }
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }
    }
}