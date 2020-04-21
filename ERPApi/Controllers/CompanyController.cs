using Newtonsoft.Json;
using ERPApi.Models;
using ERPApi.Models.IRepository;
using ERPApi.Models.Repository;
using ERPApi.Models.StronglyType;

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Script.Serialization;

namespace ERPApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CompanyController : ApiController
    {
        private ICompanyRepository companyRepository;

        public CompanyController()
        {
            this.companyRepository = new CompanyRepository();
        }

        public CompanyController(ICompanyRepository companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        public HttpResponseMessage GetAllCompanies()
        {
            var companies = companyRepository.GetAllCompanies();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, companies, formatter);
        }

        [Route("Company/GetCompanyByID?company_id={company_id}")]
        public HttpResponseMessage GetCompanyByID(int company_id)
          {
            var companies = companyRepository.GetCompanyByID(company_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, companies, formatter);
        }

        [Route("Company/GetCompanyCode?company_id={company_id}")]
        public HttpResponseMessage GetCompanyCode(int company_id)
        {
            var companies = companyRepository.GetCompanyCode(company_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, companies, formatter);
        }

        [Route("Company/GetCompanyName?company_id={company_id}")]
        public HttpResponseMessage GetCompanyName(int company_id)
        {
            var companies = companyRepository.GetCompanyName(company_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, companies, formatter);
        }

        [Route("Company/GetCompanyFlagLogo?company_id={company_id}")]
        public HttpResponseMessage GetCompanyFlagLogo(int company_id)
        {
            var companies = companyRepository.GetCompanyFlagLogo(company_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, companies, formatter);
        }

        [Route("Company/GetAllBank?company_id={company_id}")]
        public HttpResponseMessage GetAllBank(int company_id)
        {
            var companies = companyRepository.GetAllBank(company_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, companies, formatter);
        }

        [HttpPost]
        public HttpResponseMessage Post()
        {
            System.Web.HttpRequest form_contents = System.Web.HttpContext.Current.Request;
            /** get the File Informaiton from http context **/
            var company_name = form_contents.Form["company_name"].ToString();
            var company_code = form_contents.Form["company_code"].ToString();
            var is_active = form_contents.Form["is_active"].ToString();
            var is_parent_company = form_contents.Form["is_parent_company"].ToString();
            var address_1 = form_contents.Form["address_1"].ToString();
            var address_2 = form_contents.Form["address_2"].ToString();
            var country_id = form_contents.Form["country_id"].ToString()??"";
            var city_id = form_contents.Form["city_id"].ToString()??"";
            var zip_code = form_contents.Form["zip_code"].ToString();
            var email = form_contents.Form["email"].ToString();
            var phone = form_contents.Form["phone"].ToString();
            var fax = form_contents.Form["fax"].ToString();
            var web = form_contents.Form["web"].ToString();
            var mobile = form_contents.Form["mobile"].ToString();
            // admin Info
            var first_name = form_contents.Form["first_name"].ToString();
            var last_name = form_contents.Form["last_name"].ToString();
            var admin_mobile = form_contents.Form["admin_mobile"].ToString();
            var admin_phone = form_contents.Form["admin_phone"].ToString();
            var admin_address_1 = form_contents.Form["admin_address_1"].ToString();
            var admin_address_2 = form_contents.Form["admin_address_2"].ToString();
            var admin_zip_code = form_contents.Form["admin_zip_code"].ToString();
            var admin_fax = form_contents.Form["admin_fax"].ToString();
            var admin_email = form_contents.Form["admin_email"].ToString();
            var admin_web = form_contents.Form["admin_web"].ToString();
            var sex = form_contents.Form["sex"].ToString();
            var dob = form_contents.Form["dob"].ToString();

            var user_name = form_contents.Form["user_name"].ToString();
            var password = form_contents.Form["password"].ToString();
            

            List<bank> bank_list = (List<bank>)Newtonsoft.Json.JsonConvert.DeserializeObject(form_contents.Form["bank_grid"], typeof(List<bank>));

            var httpPostedFile = form_contents.Files["UploadedImage"];
            var httpPostedFlagFile = form_contents.Files["UploadedFlagImage"];
            string ActualFileName = "";
            string ActualFlagFile = "";

            if (companyRepository.CheckDuplicateCompany(company_name) == true)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Company Already Exists" }, formatter);
            }

            
            else
            {
                /** save the File to Server Path **/
                bool checkFileSave = false;
                string docfileName = "";
                string flagFileName = "";
                if (httpPostedFile != null || httpPostedFlagFile != null)
                {
                    
                    ActualFileName = form_contents.Files["UploadedImage"].FileName;
                    ActualFlagFile = form_contents.Files["UploadedFlagImage"].FileName;
                    var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Images/Uploads/documents/"), ActualFileName);
                    var fileFlagSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Images/Uploads/flag/"), ActualFlagFile);
                    
                    try
                    {
                        docfileName = "/Images/Uploads/documents/" + ActualFileName;
                        flagFileName = "/Images/Uploads/flag/" + ActualFlagFile;

                        httpPostedFile.SaveAs(fileSavePath);
                        httpPostedFlagFile.SaveAs(fileFlagSavePath);
                        checkFileSave = true;
                    }
                    catch
                    {
                        checkFileSave = false;
                    }
                }
                if (httpPostedFile == null || httpPostedFlagFile == null)
                {
                    checkFileSave = true;
                }

                if (checkFileSave == true)
                {
                    Models.StronglyType.CompanyModel insertCompany = new Models.StronglyType.CompanyModel
                    {
                        company_name = company_name,
                        company_code = company_code.ToUpper(),
                        is_active = is_active,
                        logo_path = docfileName,
                        is_parent_company = is_parent_company,
                        address_1 = address_1,
                        address_2 = address_2,
                        country_id = int.Parse(country_id),
                        city_id = int.Parse(city_id),
                        zip_code = zip_code,
                        email = email,
                        phone = phone,
                        fax = fax,
                        web = web,
                        mobile = mobile,
                        flag_path = flagFileName,
                       
                        user_name = user_name,
                        password = password,
                        
                    };
                    Models.StronglyType.CompanyAdminModel adminInfo = new Models.StronglyType.CompanyAdminModel
                    {
                        first_name = first_name,
                        last_name = last_name,
                        admin_address_1 = admin_address_1,
                        admin_address_2 = admin_address_2,
                        admin_email = admin_email,
                        admin_fax = admin_fax,
                        admin_mobile = admin_mobile,
                        admin_phone = admin_phone,
                        admin_web = admin_web,
                        admin_zip_code = admin_zip_code,
                        sex = sex,
                        dob = dob,
                        user_name = user_name,
                        password = password
                    };

                    bool save_company = companyRepository.InsertCompany(insertCompany,adminInfo, bank_list);

                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Company save successfully" }, formatter);
                }
                else
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                                  new Confirmation { output = "error", msg = "Company  is not saved succesfully." }, formatter);
                }
            }

            //}
            //catch (Exception ex)
            //{
            //    var formatter = RequestFormat.JsonFormaterString();
            //    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            //}
        }


   [ActionName("InsertBank")]
        [HttpPost]
        public HttpResponseMessage InsertBank([FromBody]Models.StronglyType.CompanyModel oCompany)
        {
            try
            {
                bool irepoInsert = companyRepository.InsertBank(oCompany);

                if (irepoInsert == true)
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "" }, formatter);
                }
                else
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "" }, formatter);
                }
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [HttpPut]
        public HttpResponseMessage UpdateCompany()
        {
            System.Web.HttpRequest form_contents = System.Web.HttpContext.Current.Request;

            var company_id = int.Parse(form_contents.Form["company_id"].ToString());
            var company_name = form_contents.Form["company_name"].ToString();
            var company_code = form_contents.Form["company_code"].ToString();
            var is_active = form_contents.Form["is_active"].ToString();
            var is_parent_company = form_contents.Form["is_parent_company"].ToString();
            var address_1 = form_contents.Form["address_1"].ToString();
            var address_2 = form_contents.Form["address_2"].ToString();
            var country_id = form_contents.Form["country_id"].ToString();
            var city_id = form_contents.Form["city_id"].ToString();
            var zip_code = form_contents.Form["zip_code"].ToString();
            var email = form_contents.Form["email"].ToString();
            var phone = form_contents.Form["phone"].ToString();
            var fax = form_contents.Form["fax"].ToString();
            var web = form_contents.Form["web"].ToString();
            var mobile = form_contents.Form["mobile"].ToString();
            int emp_id = int.Parse(form_contents.Form["emp_id"]);
            int currency_id = int.Parse(form_contents.Form["currency_id"]);
            List<bank> bank_list = (List<bank>)Newtonsoft.Json.JsonConvert.DeserializeObject(form_contents.Form["bank_grid"], typeof(List<bank>));

            /** get the File Informaiton from http context **/
            var httpPostedFile = form_contents.Files["UploadedImage"];
            var httpPostedFlagFile = form_contents.Files["UploadedFlagImage"];

            string ActualFileName = "";
            string ActualFlagFile = "";
            string docfileName = "";
            string flagFileName = "";
            if (string.IsNullOrEmpty(company_name))
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Company Name is Empty" }, formatter);
            }
            //if (httpPostedFlagFile == null)
            //{
            //    var formatter = RequestFormat.JsonFormaterString();
            //    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Flag Logo Path  is not saved succesfully." }, formatter);
            //}
            else
            {
                if (httpPostedFile == null)
                {
                    Models.StronglyType.CompanyModel updateCompany = new Models.StronglyType.CompanyModel
                     {
                         company_id = company_id,
                         company_name = company_name,
                         company_code = company_code.ToUpper(),
                         is_active = is_active,
                    //     updated_by = emp_id,

                         logo_path = "",
                         is_parent_company = is_parent_company,
                         address_1 = address_1,
                         address_2 = address_2,
                         country_id = int.Parse(country_id),
                         city_id = int.Parse(city_id),
                         zip_code = zip_code,
                         email = email,
                         phone = phone,
                         fax = fax,
                         web = web,
                         mobile = mobile,
                         flag_path = "",
                         currency_id = currency_id
                     };

                    bool irepoUpdate = companyRepository.UpdateCompany(updateCompany);
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Company is Updated successfully" }, formatter);
                }
                else
                {
                    /** save the File to Server Path **/
                    ActualFileName = form_contents.Files["UploadedImage"].FileName;
                    //ActualFlagFile = form_contents.Files["UploadedFlagImage"].FileName;
                    var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Images/Uploads/documents"), ActualFileName);
                    var fileFlagSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Images/Uploads/flag"), ActualFlagFile);
                    bool checkFileSave = false;
                    try
                    {
                        docfileName = "/Images/Uploads/documents/" + ActualFileName;
                        flagFileName = "/Images/Uploads/flag/" + ActualFlagFile;

                        // Save the uploaded file to "UploadedFiles" folder
                        httpPostedFile.SaveAs(fileSavePath);
                        httpPostedFlagFile.SaveAs(fileFlagSavePath);
                        /** end Save file to Server path */
                        checkFileSave = true;
                    }
                    catch
                    {
                        checkFileSave = false;
                    }

                    Models.StronglyType.CompanyModel updateCompany = new Models.StronglyType.CompanyModel
                    {
                        company_name = company_name,
                        company_code = company_code.ToUpper(),
                        is_active = is_active,
                        logo_path = docfileName,
                        is_parent_company = is_parent_company,
                        address_1 = address_1,
                        address_2 = address_2,
                        country_id = int.Parse(country_id),
                        city_id = int.Parse(city_id),
                        zip_code = zip_code,
                        email = email,
                        phone = phone,
                        fax = fax,
                        web = web,
                        mobile = mobile,
                        flag_path = flagFileName,
                        currency_id = currency_id
                    };
                    bool irepoUpdate = companyRepository.UpdateCompany(updateCompany);
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Company Update successfully" }, formatter);
                }
            }
        }


         [ActionName("UpdateBank")]
        [HttpPut]
        public HttpResponseMessage UpdateBank([FromBody]Models.StronglyType.CompanyModel oCompany)
        {
            try
            {
                bool irepoUpdate = companyRepository.UpdateBank(oCompany);

                if (irepoUpdate == true)
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "" }, formatter);
                }
                else
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "" }, formatter);
                }
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

         [HttpDelete]
         public HttpResponseMessage DeleteBank([FromBody]Models.StronglyType.CompanyModel company)
        {
            try
            {
                //int con_id = int.Parse(country_id);
                bool updatCompany = companyRepository.DeleteCompnayBank(company.bank_id);

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Company Delete Successfully." }, formatter);
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }
        
    }
}