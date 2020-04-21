using ERPApi.Models.IRepository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ERPApi.Models.Repository
{
    public class EmployeeDocumentsRepository: IEmployeeDocumentsRepository
    {
        private ERPEntities _entities;

       public EmployeeDocumentsRepository() {

           this._entities = new ERPEntities();
       
       }

        public List<hr_emp_documents> GetAllEmpDocuments()
        {
             List<hr_emp_documents> documents = _entities.hr_emp_documents.ToList();
            return documents;
        }

        public hr_emp_documents GetEmpDoumentsByID(int emp_documents_id)
        {
            throw new NotImplementedException();
        }

        public bool InsertEmpDocuments(hr_emp_documents oEmpDocuments)
        {
            try
            {
                hr_emp_documents Insert_emp_documents = new hr_emp_documents
                {
                    employee_id = oEmpDocuments.employee_id,
                    file_name = oEmpDocuments.file_name,
                    file_description = oEmpDocuments.file_description,
                    file_location = oEmpDocuments.file_location

                };

                _entities.hr_emp_documents.Add(Insert_emp_documents);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateEmpDocuments(hr_emp_documents oEmpDocuments)
        {
            try
            {
                hr_emp_documents Documents = _entities.hr_emp_documents.Find(oEmpDocuments.emp_documents_id);
                Documents.employee_id = oEmpDocuments.employee_id;
                Documents. file_name = oEmpDocuments.file_name;
                Documents.file_description = oEmpDocuments.file_description;
                Documents.file_location = oEmpDocuments.file_location;

                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteEmpDocuments(int emp_documents_id)
        {
            try
            {
                hr_emp_documents oDocuments = _entities.hr_emp_documents.FirstOrDefault(e => e.emp_documents_id == emp_documents_id);
                _entities.hr_emp_documents.Attach(oDocuments);
                _entities.hr_emp_documents.Remove(oDocuments);
                _entities.SaveChanges();
                return true;

            }

            catch (Exception)
            {
                return false;
            }
        }


        public List<hr_emp_documents> GetDocumentsByEmployee(int? employee_id)
        {
            List<hr_emp_documents> documents = _entities.hr_emp_documents.Where(d => d.employee_id == employee_id).ToList();
            return documents;
        }


        public bool CheckDuplicateByFileName(int? employee_id, string file_name)
        {
            var checkFileNameIsExists = _entities.hr_emp_documents.FirstOrDefault(e => e.employee_id == employee_id && e.file_name == file_name);
            return checkFileNameIsExists == null ? false : true;
        }


        public string Post() {
           // HttpRequest httpRequests = HttpContext.Current.Request;

            return "Done";
        
        }

        //public string Post()
        //{

        //    try
        //    {
        //        var httpRequest = HttpContext.Current.Request;

        //        // Check if files are available
        //        if (httpRequest.Files.Count > 0)
        //        {
                    
        //            var tempfiles = new List<string>();
        //            // interate the files and save on the server
        //            foreach (string file in httpRequest.Files)
        //            {
        //                // interate the files and save on the server
        //                string d = DateTime.Now.ToString("MM_dd_yyyy_hh_mm_ss") + "_";
        //                var postedFile = httpRequest.Files[file];
        //                var filePath =
        //                    HttpContext.Current.Server.MapPath("~/App_Data/EMP_DOCUMENT/" + d +
        //                                                       postedFile.FileName);
        //                postedFile.SaveAs(filePath);
        //                tempfiles.Add(filePath);
        //                //filePath spliting--shawon(30-09-2015)
        //                string fullPath = filePath;
        //                string fileName = Path.GetFileName(fullPath);
        //                string filePathh = Path.GetDirectoryName(fullPath);
        //                string filePathhh = Path.GetDirectoryName(Path.GetDirectoryName(fullPath));
        //                string shortPath = Path.Combine(Path.GetFileName(filePathhh), Path.GetFileName(filePathh), fileName);
        //                //
        //                return shortPath;
        //            }
        //            return "";
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        public bool AddEmployeeDocument(hr_emp_documents oemployee)
        {
            try
            {
                var insertEmployeeDocument = new hr_emp_documents
                {
                    employee_id = oemployee.employee_id,
                    file_name = oemployee.file_name,
                    file_description = oemployee.file_description,
                    file_location = oemployee.file_location
                    //

                };

                _entities.hr_emp_documents.Add(insertEmployeeDocument);
                _entities.SaveChanges();

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public object GetEmpDocumentGrid(long? empId)
        {
            try
            {
                var x = (from obd in _entities.hr_emp_documents
                         where obd.employee_id == empId
                         orderby obd.emp_documents_id
                         select new
                         {
                             emp_documents_id = obd.emp_documents_id,
                             file_name = obd.file_name,
                             file_description = obd.file_description
                         }).ToList();
                return x;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}