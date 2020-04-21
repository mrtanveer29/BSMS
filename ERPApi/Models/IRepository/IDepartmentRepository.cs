using System;
using System.Collections.Generic;

namespace ERPApi.Models.IRepository
{
    public interface IDepartmentRepository
    {
        List<department> GetAllDepartmentsOnly();

        object GetAllDepartments();

        department GetDepartmentByID(int department_id);

        department GetDepartmentByName(string department_name);

        bool CheckDepartmentForDuplicateByname(string department_name);

        bool InsertDepartment(department oDepartment);

        bool UpdateDepartment(department oDepartment);

        bool DeleteDepartment(int department_id);

        Object GetAllDpmts(int companyid, int branchid);
    }
}