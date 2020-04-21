using System.Collections.Generic;

namespace ERPApi.Models.IRepository
{
    public interface IRoleRepository
    {
        object GetAllRoles(int companyId);

        role GetRoleByID(int role_id);

        role GetRoleByName(string role_name);

        object GetRolenameByID(int emp_id);

        bool CheckRoleForDuplicateByname(string role_name,string company_id);

        bool InsertRole(role oRole);

        bool UpdateRole(role oRole);

        bool DeleteRole(int role_id);

        object GetAllRolesByType();

        List<role> GetEmployeeRoleType();

        List<role> GetsupllierRoleType();

        List<role> GetEmployeeRoleTypeBySource(int companyId);
        List<role> GetAllRolesForEmpUser(int company_id);
       // int

        List<role> GetAssignedRoleForUser(int user_id);
        List<role> GetUnassignedRoleForUser(int user_id);
        object GetRoleByRoleId(int roleId);
    }
}