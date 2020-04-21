using ERPApi.Models.StronglyType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPApi.Models.IRepository
{
    public interface IEmployeeJobDetailsRepository
    {
        object GetAllEmployeeJobDetails();

        hr_emp_job_details GetEmployeeJobDetailsByID(int emp_job_details_id);

        bool InsertEmployeeJobDetails(EmpJobDetailsModel oEmployeeJobDetails);
        bool UpdateEmployeeJobDetails(EmpJobDetailsModel oEmployeeJobDetails);
        bool DeleteEmployeeJobDetails(int emp_job_details_id);

        List<employee> GetAllEmployeeCode(int company_id);
    }
}
