using ERPApi.Models.IRepository;
using ERPApi.Models.StonglyType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPApi.Models.Repository
{
    public class EmployeeSalaryRepository : IEmployeeSalaryRepository
    {

        private ERPEntities _entities;

        public EmployeeSalaryRepository()
        {

            this._entities = new ERPEntities();

        }

        public object GetAllEmployeeSalary()
        {
            List<hr_employee_salary> empsalary = _entities.hr_employee_salary.ToList();
            return empsalary;
        }

        public hr_employee_salary GetEmployeeSalaryByID(int emp_salary_id)
        {
            throw new NotImplementedException();
        }

        public bool InsertEmployeeSalary(hr_employee_salary oEmployeeSalary)
        {
            try
            {
                hr_employee_salary insert_employee_salary = new hr_employee_salary
                {
                    emp_salary_info_id = oEmployeeSalary.emp_salary_info_id,
                    salary_info = oEmployeeSalary.salary_info,
                    salary_ammount = oEmployeeSalary.salary_ammount,
                    emp_id = oEmployeeSalary.emp_id
                };
                _entities.hr_employee_salary.Add(insert_employee_salary);
                _entities.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateEmployeeSalary(hr_employee_salary oEmployeeSalary)
        {
            try
            {
                //Check if the ID exists
                //If non-existing then, add a new row
                //After succesfully updating, make history in the history table
                List<hr_employee_salary> checkexist = (from es in _entities.hr_employee_salary
                                                    where es.emp_id == oEmployeeSalary.emp_id
                                                    && es.emp_salary_info_id == oEmployeeSalary.emp_salary_info_id
                                                    select es).ToList();
                if (checkexist.Count() > 0)
                {
                    //var listOfData = _entities.employee_salary.Where(a => a.emp_id == oEmployeeSalary.emp_id).ToList();
                    //foreach (var list in listOfData)
                    //{
                    //    employee_salary update = _entities.employee_salary.Find(list.emp_salary_id);
                    //    update.salary_ammount = oEmployeeSalary.salary_ammount;
                    //    _entities.SaveChanges();
                    //}
                   hr_employee_salary empsalary = _entities.hr_employee_salary.SingleOrDefault(a=>a.emp_id == oEmployeeSalary.emp_id && a.emp_salary_info_id == oEmployeeSalary.emp_salary_info_id);
                    empsalary.salary_ammount = oEmployeeSalary.salary_ammount;
                    _entities.SaveChanges();
                    return true;
                }
                else
                {
                    hr_employee_salary insert_employee_salary = new hr_employee_salary
                    {
                        emp_salary_info_id = oEmployeeSalary.emp_salary_info_id,
                        salary_info = oEmployeeSalary.salary_info,
                        salary_ammount = oEmployeeSalary.salary_ammount,
                        emp_id = oEmployeeSalary.emp_id
                    };
                    _entities.hr_employee_salary.Add(insert_employee_salary);
                    _entities.SaveChanges();
                    return true;
                }


            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteEmployeeSalary(int emp_salary_id)
        {
            try
            {
                hr_employee_salary oEmployeeSalary = _entities.hr_employee_salary.FirstOrDefault(es => es.emp_salary_id == emp_salary_id);
                _entities.hr_employee_salary.Attach(oEmployeeSalary);
                _entities.hr_employee_salary.Remove(oEmployeeSalary);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool CheckDuplicateBySalaryInformation(int? emp_id, string salary_info)
        {
            var checkSalaryInfoIsExists = _entities.hr_employee_salary.FirstOrDefault(si => si.emp_id == emp_id && si.salary_info == salary_info);
            return checkSalaryInfoIsExists == null ? false : true;
        }

        //for dynamic table edit
        public List<GetEmployeeSalary> GetEmployeeSalaryByEmployeeID(int emp_id)
        {
            var dataList = (from es in _entities.hr_employee_salary.Where(p => p.emp_id == emp_id)
                            join esh in _entities.hr_payroll_salary_header on es.emp_salary_info_id equals esh.psh_id
                            select new GetEmployeeSalary
                            {
                                psh_title = esh.psh_title,
                                emp_salary_id = es.emp_salary_id,
                                emp_salary_info_id = es.emp_salary_info_id,
                                salary_info = es.salary_info,
                                salary_ammount = es.salary_ammount,
                                emp_id = es.emp_id
                            }
                            ).ToList();

            return dataList;
        }
    }
}