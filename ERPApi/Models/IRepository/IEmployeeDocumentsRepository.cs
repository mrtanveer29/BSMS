using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPApi.Models.IRepository
{
   public interface IEmployeeDocumentsRepository
    {
        List<hr_emp_documents> GetAllEmpDocuments();
        List<hr_emp_documents> GetDocumentsByEmployee(int? employee_id);
        hr_emp_documents GetEmpDoumentsByID(int emp_documents_id);
        bool InsertEmpDocuments(hr_emp_documents oEmpDocuments);
        bool UpdateEmpDocuments(hr_emp_documents oEmpDocuments);
        bool DeleteEmpDocuments(int emp_documents_id);

        bool CheckDuplicateByFileName(int? employee_id, string file_name);
       //for upload documents
        string Post();
        bool AddEmployeeDocument(hr_emp_documents oemployee);
        Object GetEmpDocumentGrid(long? empId);
    }
}
