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
    public class DesignationController : ApiController
    {
        private IDesignationRepository designationRepository;

        public DesignationController()
        {
            this.designationRepository = new DesignationRepository();
        }

        public DesignationController(IDesignationRepository designationRepository)
        {
            this.designationRepository = designationRepository;
        }

        public HttpResponseMessage GetAllDesignations()
        {
            var designations = designationRepository.GetAllDesignations();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, designations, formatter);
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage Post([FromBody]Models.designation designation)
        {
            try
            {
                if (string.IsNullOrEmpty(designation.designation_name))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Designation Name is Empty" }, formatter);
                }
                else
                {
                    if (designationRepository.CheckDesignationForDuplicateByname(designation.designation_name))
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Designation Already Exists" }, formatter);
                    }
                    else
                    {
                        Models.designation insertDesignation = new Models.designation
                        {
                            designation_name = designation.designation_name,
                            designation_abbreviation = designation.designation_abbreviation,
                            department_id = designation.department_id,
                            is_active = designation.is_active,
                            created_by = 1,
                            created_date = DateTime.Now.ToString(),
                            updated_by = 1,
                            updated_date = DateTime.Now.ToString(),
                            company_id = designation.company_id
                        };
                        bool save_designation = designationRepository.InsertDesignation(insertDesignation);

                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Designation save successfully" }, formatter);
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
        public HttpResponseMessage Put([FromBody]Models.designation designation)
        {
            try
            {
                if (string.IsNullOrEmpty(designation.designation_name))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Designation Name is Empty" }, formatter);
                }
                else
                {
                    Models.designation updateDesignation = new Models.designation
                    {
                        designation_id = designation.designation_id,
                        designation_name = designation.designation_name,
                        designation_abbreviation = designation.designation_abbreviation,
                        department_id = designation.department_id,
                        is_active = designation.is_active,
                        updated_by = 1,
                        updated_date = DateTime.Now.ToString(),
                        company_id = 12
                    };

                    bool designationUpdate = designationRepository.UpdateDesignation(updateDesignation);

                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Designation Update successfully" }, formatter);
                }
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [System.Web.Http.HttpDelete]
        public HttpResponseMessage Delete([FromBody]Models.designation designation)
        {
            try
            {
                bool deleteDesignation = designationRepository.DeleteDesignation(designation.designation_id);

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Designation Delete Successfully." }, formatter);
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }
    }
}