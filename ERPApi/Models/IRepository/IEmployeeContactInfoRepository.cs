using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPApi.Models.IRepository
{
   public interface IEmployeeContactInfoRepository
    {
        object GetAllEmployeeContactInfo();

        hr_emp_contact_info GetEmployeeContactInfoByID(int emp_contact_info_id);

        bool InsertEmployeeContactInfo(hr_emp_contact_info oEmployeeContactInfo);
        bool UpdateEmployeeContactInfo(hr_emp_contact_info oEmployeeContactInfo);
        bool DeleteEmployeeContactInfo(int emp_contact_info_id);
    }
}
