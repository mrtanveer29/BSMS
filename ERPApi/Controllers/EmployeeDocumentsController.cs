using ERPApi.Models;
using ERPApi.Models.IRepository;
using ERPApi.Models.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ERPApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EmployeeDocumentsController : ApiController
    {
        private IEmployeeDocumentsRepository employeedocumentsRepository;

        public EmployeeDocumentsController()
        {
            this.employeedocumentsRepository = new EmployeeDocumentsRepository();
        }

        public EmployeeDocumentsController(IEmployeeDocumentsRepository employeedocumentsRepository)
        {
            this.employeedocumentsRepository = employeedocumentsRepository;
        }

        public HttpResponseMessage GetAllEmpDocuments(int? employee_id)
        {
            List<hr_emp_documents> documents = employeedocumentsRepository.GetDocumentsByEmployee(employee_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, documents, formatter);
        }

        [HttpPost]
        public HttpResponseMessage Post()
        {
            //post/get employee id in education
            // var urlForRequest = Request.RequestUri.ParseQueryString();
            //int employee_id = int.Parse(urlForRequest["employee_id"].ToString());
            /** call the Http request which is send by Web Form **/
            System.Web.HttpRequest rsk = System.Web.HttpContext.Current.Request;

 
            string file_name = rsk.Form["file_name"].ToString();
            string file_description = rsk.Form["file_description"].ToString();
            int employee_id = int.Parse(rsk.Form["emp_id"].ToString());
            /** get the File Informaiton from http context **/
            var httpPostedFile = rsk.Files["UploadedImage"];
            string ActualFileName = "";
            if (httpPostedFile == null)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK,
                                new Confirmation { output = "error", msg = "Documents  file empty." }, formatter);
            }
            else
            {
                /** save the File to Server Path **/
                ActualFileName = rsk.Files["UploadedImage"].FileName;
                var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Images/Uploads/documents"), ActualFileName);
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

                if (checkFileSave == true)
                {


                    bool CheckDuplicateForFileName = employeedocumentsRepository.CheckDuplicateByFileName(employee_id, file_name);
                    if (CheckDuplicateForFileName == true)
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "File Name Already Exists" }, formatter);
                    }
                    else
                    {
                        /** insert record to database **/
                        Models.hr_emp_documents insertEmpDocuments = new Models.hr_emp_documents
                      {
                          employee_id = employee_id,
                          file_name = file_name,
                          file_description = file_description,
                          file_location = ActualFileName
                      };
                        bool insert_documents = employeedocumentsRepository.InsertEmpDocuments(insertEmpDocuments);
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK,
                                      new Confirmation { output = "success", msg = "Documents  is saved succesfully.", returnvalue = employee_id }, formatter);
                    }
                }
                else
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                                  new Confirmation { output = "error", msg = "Documents  is not saved succesfully." }, formatter);
                }





            }


        }


        [HttpPut]
        public HttpResponseMessage Put([FromBody] Models.hr_emp_documents oEmpDocuments)
        {

            try
            {


                if (string.IsNullOrEmpty(oEmpDocuments.file_name))
                {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "File Name can not be empty" });
                }
                else if (string.IsNullOrEmpty(oEmpDocuments.file_description))
                {
                    var format_type = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new Confirmation { output = "error", msg = "Description Name can not be empty" });
                }
                else
                {
                    System.Web.HttpRequest rsk = System.Web.HttpContext.Current.Request;

                    int employee_id = int.Parse(rsk.Form["emp_id"].ToString());
                    string file_name = rsk.Form["file_name"].ToString();
                    string file_description = rsk.Form["file_description"].ToString();

                    /** get the File Informaiton from http context **/
                    var httpPostedFile = rsk.Files["UploadedImage"];
                    string ActualFileName = "";
                    if (httpPostedFile == null)
                    {
                        var formatter = RequestFormat.JsonFormaterString();
                        return Request.CreateResponse(HttpStatusCode.OK,
                                        new Confirmation { output = "error", msg = "Documents  file empty." }, formatter);
                    }
                    else
                    {
                        /** save the File to Server Path **/
                        ActualFileName = rsk.Files["UploadedImage"].FileName;
                        var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Images/Uploads/documents"), ActualFileName);
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

                        if (checkFileSave == true)
                        {

                            Models.hr_emp_documents updateEmpDoc = new Models.hr_emp_documents
                            {
                                emp_documents_id = oEmpDocuments.emp_documents_id,
                                employee_id = oEmpDocuments.employee_id,
                                file_name = oEmpDocuments.file_name,
                                file_description = oEmpDocuments.file_description,
                                file_location = oEmpDocuments.file_location
                            };
                            bool irepoUpdate = employeedocumentsRepository.UpdateEmpDocuments(updateEmpDoc);
                            var formatter = RequestFormat.JsonFormaterString();
                            return Request.CreateResponse(HttpStatusCode.OK,
                                new Confirmation { output = "success", msg = "Documents Update successfully" }, formatter);

                        }
                        else {
                            var formatter = RequestFormat.JsonFormaterString();
                            return Request.CreateResponse(HttpStatusCode.OK,
                                new Confirmation { output = "success", msg = "Documents Update failed" }, formatter);
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
        [HttpDelete]
        public HttpResponseMessage Delete([FromBody] Models.hr_emp_documents oEmpDocuments)//, [FromBody] Models.user user
        {
            try
            {
                bool deleteDocuments = employeedocumentsRepository.DeleteEmpDocuments(oEmpDocuments.emp_documents_id);

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK,
                    new Confirmation { output = "success", msg = "Documents Delete Successfully." }, formatter);



            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK,
                    new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }

        }

    }
}
