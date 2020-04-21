using ERPApi.Models;
using ERPApi.Models.IRepository;
using ERPApi.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ERPApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EmpUserController : ApiController
    {
        private IEmpUserRepository empuserRepository;
        public EmpUserController()
        {
            this.empuserRepository = new EmpUserRepository();
        }
        public EmpUserController(IEmpUserRepository empuserRepository)
        {
            this.empuserRepository = empuserRepository;
        }
        [HttpGet]
        public HttpResponseMessage GetUserId(int emp_id)
        {
            var createuser = empuserRepository.GetUserId(emp_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, createuser, formatter);
        }

        [HttpGet]
        public HttpResponseMessage GetUserInformation(int emp_id)
        {
            var createuser = empuserRepository.GetUserInformation(emp_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, createuser, formatter);
        }
        [HttpGet]
        public HttpResponseMessage GetEmployeeInformationByUserId(int user_id)
        {
            var createuser = empuserRepository.GetEmployeeInformationByUserId(user_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, createuser, formatter);
        }

        [HttpPost]
        public HttpResponseMessage Post()
        {
            try
            {
                bool SuccessFlag = false;
                //image upload
                System.Web.HttpRequest rsk = System.Web.HttpContext.Current.Request;

                //int emp_id = int.Parse(rsk.Form["emp_id"].ToString());
                string user_id = rsk.Form["user_id"].ToString();
                string user_name = rsk.Form["user_name"].ToString();
                string password = rsk.Form["password"].ToString();
                //string role_id = rsk.Form["role_id"].ToString();
                string company_id = rsk.Form["company_id"].ToString();
                string confirm_password = rsk.Form["confirm_password"].ToString();
                string employee_id = rsk.Form["employee_id"].ToString();
                string customer_id = rsk.Form["customer_id"].ToString();
                string roleData = rsk.Form["roleData"].ToString();
                if (password != confirm_password)
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Password don't match." }, formatter);

                }

                else
                {

                    int uId = Convert.ToInt32(user_id);
                    var httpPostedFile = rsk.Files["user_signature"];

                    string ActualFileName = "";

                    if (httpPostedFile != null)
                    {
                        ActualFileName = rsk.Files["user_signature"].FileName.Replace(".", uId + ".");
                        var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/App_Data/Signature/"), ActualFileName);

                        bool checkFileSave = false;
                        try
                        {
                            // Save the uploaded file to "UploadedFiles" folder
                            httpPostedFile.SaveAs(fileSavePath);
                            /** end Save file to Server path */
                            checkFileSave = true;
                        }
                        catch
                        {
                            checkFileSave = false;
                        }
                    }

                    //int roleId = Convert.ToInt32(role_id);
                    int empId = Convert.ToInt32(employee_id);
                    int comId = Convert.ToInt32(company_id);
                    int cusId = Convert.ToInt32(customer_id);
                    string[] SingleOfValues = roleData.Split(';');
                    int role_id = int.Parse(SingleOfValues[0].ToString());
                    var insert_emp_user = new user
                    {
                        user_name = user_name,
                        password = password,
                        role_id = role_id,
                        employee_id = empId,
                        company_id = comId,
                        customer_id = cusId,
                        signature = ActualFileName

                    };
                    bool save_user = empuserRepository.InsertEmpUser(insert_emp_user);
                    if (save_user)
                    {
                        string[] SingleArrayOfValues = roleData.Split(';');
                        int userId = empuserRepository.lastUserId();
                        foreach (string SingleRole in SingleArrayOfValues)
                        {
                            var role = SingleRole;
                            int? roleId = int.Parse(role);
                            user_role_mapping InsertUserRoleMapping = new Models.user_role_mapping
                            {
                                user_id = userId,
                                role_id = roleId
                            };
                            bool InserUserRoleMapping = empuserRepository.InserUserRoleMapping(InsertUserRoleMapping);
                            if (InserUserRoleMapping == true)
                            {
                                SuccessFlag = true;
                            }
                        }
                    }
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "User Info save successfully" }, formatter);

                }

                //}


            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);

            }


        }
        [HttpPut]
        public HttpResponseMessage UpdateEmpUser()
        {


            bool SuccessFlag = false;
            //image upload
            System.Web.HttpRequest rsk = System.Web.HttpContext.Current.Request;

            //int emp_id = int.Parse(rsk.Form["emp_id"].ToString());
            string user_id = rsk.Form["user_id"].ToString();
            string user_name = rsk.Form["user_name"].ToString();
            string password = rsk.Form["password"].ToString();
            //string role_id = rsk.Form["role_id"].ToString();
            string company_id = rsk.Form["company_id"].ToString();
            string confirm_password = rsk.Form["confirm_password"].ToString();
            string employee_id = rsk.Form["employee_id"].ToString();
            string customer_id = rsk.Form["customer_id"].ToString();
            string roleData = rsk.Form["roleData"].ToString();
            if (password != confirm_password)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Password don't match." }, formatter);

            }

            else
            {

                //var httpPostedFile = rsk.Files["user_signature"];

                //string ActualFileName = "";

                //if (httpPostedFile == null)
                //{

                //}
                //else
                //{
                //    ActualFileName = rsk.Files["user_signature"].FileName;
                //    var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/App_Data/Signature/"), ActualFileName);

                //    bool checkFileSave = false;
                //    try
                //    {
                //        // Save the uploaded file to "UploadedFiles" folder
                //        httpPostedFile.SaveAs(fileSavePath);
                //        /** end Save file to Server path */
                //        checkFileSave = true;
                //    }
                //    catch
                //    {
                //        checkFileSave = false;
                //    }
                //}
                int uId = Convert.ToInt32(user_id);
                var httpPostedFile = rsk.Files["user_signature"];

                string ActualFileName = "";

                if (httpPostedFile != null)
                {
                    ActualFileName = rsk.Files["user_signature"].FileName.Replace(".", uId + ".");
                    var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/App_Data/Signature/"), ActualFileName);

                    if (empuserRepository.DeleteExistiingSignature(uId))
                    {
                        bool checkFileSave = false;
                        try
                        {
                            // Save the uploaded file to "UploadedFiles" folder
                            httpPostedFile.SaveAs(fileSavePath);
                            /** end Save file to Server path */
                            checkFileSave = true;
                        }
                        catch
                        {
                            checkFileSave = false;
                        }
                    }
                }

                int empId = Convert.ToInt32(employee_id);
                int comId = Convert.ToInt32(company_id);
                int cusId = Convert.ToInt32(customer_id);
                string[] SingleOfValues = roleData.Split(';');
                int role_id = int.Parse(SingleOfValues[0].ToString());

                var update_emp_user = new user
                {
                    user_id = int.Parse(user_id),
                    user_name = user_name,
                    password = password,
                    role_id = role_id,
                    employee_id = empId,
                    company_id = comId,
                    customer_id = cusId,
                    signature = ActualFileName

                };
                bool save_user = empuserRepository.UpdateEmpUser(update_emp_user);
                if (save_user)
                {
                    string[] SingleArrayOfValues = roleData.Split(';');
                    int userId = int.Parse(user_id);
                    if (empuserRepository.delete(userId))
                    {
                        foreach (string SingleRole in SingleArrayOfValues)
                        {
                            var role = SingleRole;
                            int? role_id1 = int.Parse(role);
                            Models.user_role_mapping InsertUserRoleMapping = new Models.user_role_mapping
                            {
                                user_id = userId,
                                role_id = role_id1
                            };
                            bool InserUserRoleMapping = empuserRepository.InserUserRoleMapping(InsertUserRoleMapping);
                            if (InserUserRoleMapping == true)
                            {
                                SuccessFlag = true;
                            }
                        }
                    }

                }
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "User Info Update successfully" }, formatter);

            }

            //}
        }



    }
}
