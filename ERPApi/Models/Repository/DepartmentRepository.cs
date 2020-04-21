using ERPApi.Models.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ERPApi.Models.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private ERPEntities _entities;

        public DepartmentRepository()
        {
            this._entities = new ERPEntities();
        }

        public List<department> GetAllDepartmentsOnly()
        {
            List<department> departments = _entities.departments.ToList();
            return departments;
        }

        public object GetAllDepartments()
        {
            var departments = (from dep in _entities.departments
                               join emp in _entities.employees
                               on dep.incharge_employee_id equals emp.emp_id
                               into leftOrder
                               from order in leftOrder.DefaultIfEmpty()

                               select new
                               {
                                   department_id = dep.department_id,
                                   department_name = dep.department_name,
                                   department_abbreviation = dep.department_abbreviation,
                                   parent_department_id = dep.parent_department_id,
                                   incharge_employee_id = (int?)order.emp_id,
                                   location = dep.location,
                                   created_by = dep.created_by,
                                   created_date = dep.created_date,
                                   updated_by = dep.updated_by,
                                   updated_date = dep.updated_date,
                                   company_id = dep.company_id,
                                   is_active = dep.is_active,
                                   //employee_name = order.emp_name
                               }).OrderByDescending(s => s.department_id).ToList();

            return departments;
        }

        public Object GetAllDpmts(int companyid, int branchid)
        {
            try
            {
                using (var db = new ERPEntities())
                {
                    return db.departments.Where(i => i.company_id == companyid && i.branch_id == branchid).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public department GetDepartmentByID(int department_id)
        {
            throw new NotImplementedException();
        }

        public department GetDepartmentByName(string department_name)
        {
            throw new NotImplementedException();
        }

        public bool CheckDepartmentForDuplicateByname(string department_name)
        {
            var checkDuplicateDepartment = _entities.departments.FirstOrDefault(d => d.department_name == department_name);

            if (checkDuplicateDepartment == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool InsertDepartment(department oDepartment)
        {
            try
            {
                department insert_department = new department
                {
                    department_name = oDepartment.department_name,
                    department_abbreviation = oDepartment.department_abbreviation,
                    parent_department_id = oDepartment.parent_department_id,
                    incharge_employee_id = oDepartment.incharge_employee_id,
                    location = oDepartment.location,
                    created_by = oDepartment.created_by,
                    created_date = oDepartment.created_date,
                    updated_by = oDepartment.updated_by,
                    updated_date = oDepartment.updated_date,
                    company_id = oDepartment.company_id,
                    is_active = oDepartment.is_active
                };
                _entities.departments.Add(insert_department);
                _entities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateDepartment(department oDepartment)
        {
            try
            {
                department de = _entities.departments.Find(oDepartment.department_id);
                de.department_name = oDepartment.department_name;
                de.department_abbreviation = oDepartment.department_abbreviation;
                de.parent_department_id = oDepartment.parent_department_id;
                de.incharge_employee_id = oDepartment.incharge_employee_id;
                de.location = oDepartment.location;
                de.updated_by = oDepartment.updated_by;
                de.updated_date = oDepartment.updated_date;
                de.company_id = oDepartment.company_id;
                de.is_active = oDepartment.is_active;

                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteDepartment(int department_id)
        {
            try
            {
                department oDepartment = _entities.departments.FirstOrDefault(d => d.department_id == department_id);
                _entities.departments.Attach(oDepartment);
                _entities.departments.Remove(oDepartment);

                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}