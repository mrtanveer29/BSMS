using ERPApi.Models;
using ERPApi.Models.IRepository;
using ERPApi.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ERPApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BranchController : ApiController
    {
        private IBranchRepository branchRepository;

        public BranchController()
        {
            this.branchRepository = new BranchRepository();
        }

        public BranchController(IBranchRepository branchRepository)
        {
            this.branchRepository = branchRepository;
        }

        public HttpResponseMessage GetAllBranches(int company_id)
        {

            var rbos = branchRepository.GetAllBranches(company_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, rbos, formatter);
        }
        

        [Route("Branch/GetBranchByID?branch_id={branch_id}")]
        public HttpResponseMessage GetBranchByID(int branch_id)
        {

            var rbos = branchRepository.GetBranchByID(branch_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, rbos, formatter);
        }

        [Route("Branch/GetBranchCodeByID?branch_id={branch_id}")]
        public HttpResponseMessage GetBranchCodeByID(int branch_id)
        {

            var branchcode = branchRepository.GetBranchCodeByID(branch_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, branchcode, formatter);
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody]Models.StronglyType.BranchModel oBranch)
        {
            try
            {
                //bool save_user;
                if (string.IsNullOrEmpty(oBranch.branch_code))
                {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "Branch Code can not be empty" });
                }
                else if (string.IsNullOrEmpty(oBranch.branch_name))
                {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "Branch Name not be empty" });
                }
                //else if(string.IsNullOrEmpty(oBranch.branch_location))
                //{
                //    var format_type = RequestFormat.JsonFormaterString();
                //    return Request.CreateResponse(HttpStatusCode.OK,
                //        new Confirmation { output = "error", msg = "Branch Location can not be empty" });
                //}
                else if (string.IsNullOrEmpty(oBranch.branch_incharge.ToString()))
                {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "Branch Incharge can not be empty" });
                }
                else if (string.IsNullOrEmpty(oBranch.company_id.ToString()))
                {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "Company can not be empty" });
                }
                else
                {

                    bool CheckDuplicateForBranchname = branchRepository.CheckDuplicateForBranchName(oBranch.company_id, oBranch.branch_name);
                    bool CheckDuplicateForBranchCode = branchRepository.CheckDuplicateByBranchCode(oBranch.company_id, oBranch.branch_code);
                    if (CheckDuplicateForBranchname == true)
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Branch Name Already Exists" }, formatter);
                    }
                    else if (CheckDuplicateForBranchCode == true)
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Branch Code Already Exists" }, formatter);
                    }
                    else
                    {

                        Models.StronglyType.BranchModel insertBranch = new Models.StronglyType.BranchModel
                        {
                            branch_code = oBranch.branch_code,
                            branch_name = oBranch.branch_name,
                            branch_location = oBranch.branch_location,
                            branch_incharge = oBranch.branch_incharge,
                            company_id = oBranch.company_id,
                            is_active = oBranch.is_active,
                            address_1 = oBranch.address_1,
                            address_2 = oBranch.address_2,
                            country_id = oBranch.country_id,
                            city_id = oBranch.city_id,
                            zip_code = oBranch.zip_code,
                            email = oBranch.email,
                            phone = oBranch.phone,
                            fax = oBranch.fax,
                            web = oBranch.web,
                            mobile = oBranch.mobile,
                            emp_firstname = oBranch.emp_firstname,
                            emp_lastname = oBranch.emp_lastname,
                            user_name = oBranch.user_name,
                            password = oBranch.password
                        };
                        bool insert_branch = branchRepository.InsertBranch(insertBranch);
                        //if (save_user == true)
                        if (insert_branch == true)
                        {


                            var formatter = RequestFormat.JsonFormaterString();
                            return Request.CreateResponse(HttpStatusCode.OK,
                                new Confirmation { output = "success", msg = "Branch Information  is saved successfully." }, formatter);
                        }
                        else
                        {
                            var formatter = RequestFormat.JsonFormaterString();
                            return Request.CreateResponse(HttpStatusCode.OK,
                                new Confirmation { output = "error", msg = "Branch Information  is not saved succesfully." }, formatter);
                        }
                    }
                }
            }


            catch (Exception ex)
            {

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK,
                    new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [ActionName("UpdateBranch")]
        [HttpPut]
        public HttpResponseMessage Put([FromBody] Models.StronglyType.BranchModel oBranch)
        {

            try
            {
                if (string.IsNullOrEmpty(oBranch.branch_code))
                {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "Branch Code can not be empty" });
                }
                else if (string.IsNullOrEmpty(oBranch.branch_name))
                {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "Branch Name not be empty" });
                }
                else if (string.IsNullOrEmpty(oBranch.branch_incharge.ToString()))
                {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "Branch Incharge can not be empty" });
                }
                else if (string.IsNullOrEmpty(oBranch.company_id.ToString()))
                {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "Company can not be empty" });
                }
                else if (string.IsNullOrEmpty(oBranch.is_active.ToString()))
                {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "Select the Active or Inactive" });
                }
                else
                {


                    Models.StronglyType.BranchModel updateBranch = new Models.StronglyType.BranchModel
                    {
                        branch_id = oBranch.branch_id,
                        branch_code = oBranch.branch_code,
                        branch_name = oBranch.branch_name,
                        branch_location = oBranch.branch_location,
                        branch_incharge = oBranch.branch_incharge,
                        company_id = oBranch.company_id,
                        is_active = oBranch.is_active,
                        address_1 = oBranch.address_1,
                        address_2 = oBranch.address_2,
                        country_id = oBranch.country_id,
                        city_id = oBranch.city_id,
                        zip_code = oBranch.zip_code,
                        email = oBranch.email,
                        phone = oBranch.phone,
                        fax = oBranch.fax,
                        web = oBranch.web,
                        mobile = oBranch.mobile
                    };
                    bool irepoUpdate = branchRepository.UpdateBranch(updateBranch);
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "success", msg = "Branch Information Update successfully" }, formatter);
                }
            }

            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);

            }


        }
        [HttpDelete]
        public HttpResponseMessage Delete([FromBody] Models.branch oBranch)//, [FromBody] Models.user user
        {
            try
            {
                bool deleteBranch = branchRepository.DeleteBranch(oBranch.branch_id);

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK,
                    new Confirmation { output = "success", msg = "Branch Information Delete Successfully." }, formatter);



            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK,
                    new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [HttpGet, ActionName("GetAllBranchesById")]
        public HttpResponseMessage GetAllBranchesById(int companyid)
        {
            var data = branchRepository.GetAllBranchesById(companyid);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, data, formatter);
        }


    }
}

