using ERPApi.Models.StonglyType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPApi.Models.IRepository
{
   public interface IEmployeeSalaryRepository
    {
       object GetAllEmployeeSalary();

       hr_employee_salary GetEmployeeSalaryByID(int emp_salary_id);
       List<GetEmployeeSalary> GetEmployeeSalaryByEmployeeID(int emp_id);
       

       bool InsertEmployeeSalary(hr_employee_salary oEmployeeSalary);
       bool UpdateEmployeeSalary(hr_employee_salary oEmployeeSalary);
       bool DeleteEmployeeSalary(int emp_salary_id);

       bool CheckDuplicateBySalaryInformation(int? emp_id,string salary_info);
      
    }
}
