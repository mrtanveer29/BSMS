using ERPApi.Models.StonglyType;
using ERPApi.Models.StronglyType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPApi.Models.IRepository
{
    public interface IEmployeeRepository
    {
        object GetAllEmployees( int company_id);
        object GetEmployeeByID(int emp_id);
        object GetAllEmployeesForDrpdown(int company_id);
        bool UpdateSalaryByEmloyee(employee oEmployee);
        List<hr_payroll_salary_header> GetEmployeeByPayrollSalaryID(string salary_head_permission);
        //object GetEmployeeByPayrollSalaryID(int a);
        bool CheckDuplicateEmpCode(string emp_code);
        int InsertEmployee(EmployeeModel oEmployee);
        bool UpdateEmployee(employee oEmployee);
        bool DeleteEmployee(int emp_id);
        string Post();

        List<employee> GetEmployeeNameEmailByID(int emp_id);

        object GetEmployeeByRoleType(string source_type);
        object GetEmployeeByRoleID(int role_id); //List<employee>

      //  int companyId(int emp_id);
      //  int SaveUserInfoForEmployee(UserInfoModel userInfo);
      //  bool UpdateEmployeeRole(int? emp_id, int? role_id);

        object GetAccepterInfoByID(int uploadby, int approveby);

        List<EmployeeModel> GetAllDesigners(string role_id);

        string Getrole_nameByID(int role_id);

        object GetEmployeeInformationById(int employee_id);
        object GetAllEmployeesbyBranch(int companyId, int branchId, int roleId);
        object GetAllDrivers(int companyId);
        object GetAllCounterManForDropdown(int companyId);
    }
}
