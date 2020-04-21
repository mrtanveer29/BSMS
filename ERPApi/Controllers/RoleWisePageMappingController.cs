using ERPApi.Models.IRepository;
using ERPApi.Models.Repository;
using ERPApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using ERPApi.Models.StronglyType;

namespace ERPApi.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]

    public class RoleWisePageMappingController : ApiController
    {
        private IRoleWisePageMappingRepository rolewisepagemappingRepository;

        public RoleWisePageMappingController()
        {
            this.rolewisepagemappingRepository = new RoleWisePageMappingRepository();
        }

        public RoleWisePageMappingController(IRoleWisePageMappingRepository rolewisepagemappingRepository)
        {
            this.rolewisepagemappingRepository = rolewisepagemappingRepository;
        }

        [ActionName("GetAllPages")]
        [HttpGet]
        public HttpResponseMessage GetAllPages()
        {
            var pagelist = rolewisepagemappingRepository.GetAllPages();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, pagelist, formatter);
        }
        [ActionName("GetAllPagesByRole")]
        [HttpGet]
        public HttpResponseMessage GetAllPagesByRole(int? role_id)
        {
            var pagelistbyrole = rolewisepagemappingRepository.GetAllPagesByRole(role_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, pagelistbyrole, formatter);
        }

        [ActionName("GetAllButtons")]
        [HttpGet]
        public HttpResponseMessage GetAllButtons()
        {
            var buttonlist = rolewisepagemappingRepository.GetAllButtons();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, buttonlist, formatter);
        }

        //[ActionName("GetAllRoleWisePageMapping")]
        //[HttpGet]
        //public HttpResponseMessage GetAllRoleWisePageMapping()
        //{
        //    var rolemap = rolewisepagemappingRepository.GetAllRoleWisePageMapping();
        //    var formatter = RequestFormat.JsonFormaterString();
        //    return Request.CreateResponse(HttpStatusCode.OK, rolemap, formatter);
        //}

        [ActionName("GetAllbuttonsByPageid")]
        [HttpGet]
        public HttpResponseMessage GetAllbuttonsByPageid(int page_id)
        {
            var buttonlist = rolewisepagemappingRepository.GetAllbuttonsByPageid(page_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, buttonlist, formatter);
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] RoleWisePageMappingModel oRoleWisePage)
        {
            try
            {
                bool save_pagemapping = false;
             
                if (string.IsNullOrEmpty(oRoleWisePage.role_id.ToString()))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Please select Role" }, formatter);
                }
                else
                {
                    save_pagemapping = rolewisepagemappingRepository.InsertRoleWisePage(oRoleWisePage);

                    if (save_pagemapping == true)
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Role wise Page Mapping data is saved successfully" }, formatter);
                    }
                    else
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Error adding Mapping data to database." }, formatter);
                    }
                }
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [HttpDelete]
        public HttpResponseMessage Delete([FromBody] sts_pagewise_button oRoleWisePage)
        {
            try
            {
                bool updatRoleWise = rolewisepagemappingRepository.DeleteRoleWisePage(oRoleWisePage.pagewise_button_id);

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Page Delete Successfully." }, formatter);

            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        //[HttpPut]
        //public HttpResponseMessage Put([FromBody] sts_role_pagewise_button oRoleWisePage)
        //{
        //    try
        //    {
        //        //if (string.IsNullOrEmpty(country.country_name))
        //        //{
        //        //    var formatter = RequestFormat.JsonFormaterString();
        //        //    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Country Name is Empty" }, formatter);
        //        //}
        //        //else
        //        //{
        //        sts_role_pagewise_button updateRoleWise = new sts_role_pagewise_button
        //            {
        //           //   pagewise_button_id = oRoleWisePage.pagewise_button_id,
        //                process_id = oRoleWisePage.process_id,
        //                page_id = oRoleWisePage.page_id,
        //                status = oRoleWisePage.status
        //            };
        //        bool irepoUpdate = rolewisepagemappingRepository.UpdateRoleNPagewiseButton(updateRoleWise);
        //        var formatter = RequestFormat.JsonFormaterString();
        //        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Page Update successfully" }, formatter);
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        var formatter = RequestFormat.JsonFormaterString();
        //        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
        //    }
        //}
    }
}
