using ERPApi.Models.IRepository;
using ERPApi.Models.StonglyType;
using ERPApi.Models.StronglyType;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ERPApi.Models.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private ERPEntities _entities;

        public EmployeeRepository()
        {

            this._entities = new ERPEntities();

        }

        public object GetAllEmployees(int company_id)
        {
            

            var data = (from emp in _entities.employees.Where(e => e.company_id == company_id)
                join empCont in _entities.hr_emp_contact_info on emp.emp_id equals empCont.employee_id into EMPContaTable
                from SubEmpCont in EMPContaTable.DefaultIfEmpty()
                select new
                {
                    emp_id=emp.emp_id,
                    emp_firstname=emp.emp_firstname+" "+emp.emp_lastname,
                    emp_code=emp.emp_code,
                    emp_mobile=SubEmpCont.emp_mobile,
                    emp_phone=SubEmpCont.emp_phone,
                    emp_email=SubEmpCont.emp_email,
                    emp_dateofjoin=emp.emp_dateofjoin

                }).ToList().OrderByDescending(e=>e.emp_id);
            return data;
        }
        // 27 june 2016 changed
        public object GetEmployeeByRoleType(string source_type)
        {
            var sql = "Select e.emp_id,COALESCE(e.emp_firstname, '') || ' ' || COALESCE(e.emp_lastname, '') AS employee_name,e.employee_email from employee as e LEFT join users as u on u.employee_id = e.emp_id LEFT join role as r on r.role_id = u.role_id where r.source_type = '" + source_type + "'";
         // var sql = "Select e.emp_id,COALESCE(e.emp_firstname, '') || ' ' || COALESCE(e.emp_lastname, '') AS employee_name,e.employee_email from employee as e Left join role as r on e.employee_role = r.role_id where r.source_type = '" + source_type + "'";
            var employees = _entities.Database.SqlQuery<EmployeeModel>(sql).ToList();
            return employees;
        }

        public List<EmployeeModel> GetAllDesigners(string role_id)
        {
            var sql = "Select e.emp_id, COALESCE(e.emp_firstname, '') || ' ' || COALESCE(e.emp_lastname, '') AS emp_name,COALESCE(e.employee_email,' ') from employee as e LEFT join users as u on u.employee_id = e.emp_id LEFT join role as r on r.role_id = u.role_id where u.role_id IN (" + role_id + ")";
         // var sql = "Select e.emp_id, COALESCE(e.emp_firstname, '') || ' ' || COALESCE(e.emp_lastname, '') AS emp_name,e.employee_email from employee as e where e.employee_role IN (" + role_id + ")";
            List<EmployeeModel> designers = _entities.Database.SqlQuery<EmployeeModel>(sql).ToList();
            return designers;
        }

        public object GetEmployeeByRoleID(int role_id)
        {
            var employeedata = _entities.users.Where(u=>u.role_id == role_id)
                .Join(_entities.employees, ju => ju.employee_id, ju => ju.emp_id, (ju, je) => new { ju, je })
                .Join(_entities.roles, jr => jr.ju.role_id, jk => jk.role_id, (jr, jk) => new { jr, jk })
                .Select(i => new
                {
                    i.jr.je.emp_id,
                    i.jr.je.emp_firstname,
                    i.jr.je.emp_lastname,
                    i.jr.je.employee_email,
                    i.jk.role_id
                }).ToList().OrderByDescending(d => d.emp_id);
            return employeedata;
        }

        public object GetAccepterInfoByID(int recipient, int actiondoneby)
        {
            var approver_info = "";
            return approver_info;
        }

        public List<employee> GetEmployeeNameEmailByID(int emp_id)
        {
            List<employee> employeedata = _entities.employees.Where(e => e.emp_id == emp_id).ToList();
            return employeedata;
        }

        public object GetEmployeeByID(int emp_id)
        {
            var employees = (from e in _entities.employees
                             join jd in _entities.hr_emp_job_details
                             on e.emp_id equals jd.emp_id into jdetails
                             join ci in _entities.hr_emp_contact_info
                             on e.emp_id equals ci.employee_id into cinfo
                             join de in _entities.departments
                             on e.emp_id equals de.employee_id into dept
                             join di in _entities.designations
                             on e.emp_id equals di.employee_id into desg
                             join sg in _entities.hr_staffgrade
                             on e.emp_id equals sg.employee_id into stgrade
                             join es in _entities.hr_employee_salary
                             on e.emp_id equals es.emp_id into esalary
                             join ed in _entities.hr_emp_documents
                             on e.emp_id equals ed.employee_id into edocuments
                             join us in _entities.users on e.emp_id equals us.employee_id
                             into UserTable from SubUser in UserTable.DefaultIfEmpty()


                             from jd in jdetails.DefaultIfEmpty()
                             from ci in cinfo.DefaultIfEmpty()
                             from de in dept.DefaultIfEmpty()
                             from di in desg.DefaultIfEmpty()
                             from sg in stgrade.DefaultIfEmpty()
                             from es in esalary.DefaultIfEmpty()
                             from ed in edocuments.DefaultIfEmpty()
                             
                             where e.emp_id==emp_id
                             select new
                             {
                                 emp_id = e.emp_id,
                                 emp_firstname = e.emp_firstname,
                                 emp_lastname = e.emp_lastname,
                                 emp_gender = e.emp_gender,
                                 emp_dateofbirth = e.emp_dateofbirth,
                                 emp_blood_group = e.emp_blood_group,
                                 emp_marital_status = e.emp_marital_status,
                                 emp_image_file_name = e.emp_image_file_name,
                                 emp_id_type = e.emp_id_type,
                                 emp_id_no = e.emp_id_no,
                                 emp_account_number = e.emp_account_number,
                                 emp_bank_title = e.emp_bank_title,
                                 company_id = e.company_id,
                                 branch_id = jd.branch_id,
                                 unit_id = jd.unit_id,
                                 department_id = jd.department_id,
                                 designation_id = jd.designation_id,
                                 staffgrade_id = jd.staffgrade_id,
                                 job_location_id = jd.job_location_id,
                                 subsection_id = jd.subsection_id,
                                 effective_form = jd.start_date,
                                 emp_code = e.emp_code,
                                 emp_supervisor = jd.emp_supervisor,
                                 emp_reporting_method = jd.emp_reporting_method,
                                 emp_dateofjoin = e.emp_dateofjoin,
                                 prop_confirmation_date = jd.emp_prop_confirmation_date,
                                 attendance_policy_id = jd.attendance_policy_id,
                                 
                                 //file_name = ed.file_name,
                                 //file_description = ed.file_description,
                                 //file_location = ed.file_location,
                                 //emp_contact_info_id = ci.emp_contact_info_id, 
                                 
                                 country_id = ci.country_id,
                                 city_id = ci.city_id,
                                 emp_zip = ci.zip_code,
                                 emp_mobile = ci.emp_mobile,
                                 emp_phone = ci.emp_phone,
                                 emp_email = ci.emp_email,
                                 emergency_contact_name=ci.emergency_contact_name,
                                 emergency_contact_address=ci.emergency_contact_address,
                                 emergency_contact_email=ci.emergency_contact_email,
                                 emergency_contact_mobile=ci.emergency_contact_mobile,
                                 emergency_contact_id=ci.emergency_contact_id,
                                 emergency_contact_relation=ci.emergency_contact_relation,
                                 permanent_address = ci.permanent_address,
                                 present_address = ci.present_address,
                                 role_id = SubUser.role_id,
                                 role_type_id = SubUser.role_type_id,
                                 password = SubUser.password,
                                 confirm_password = SubUser.confirm_password,
                                 signature = SubUser.signature,
                                 user_name = SubUser.user_name,
                                 customer_id = SubUser.customer_id,
                                 department_name = _entities.departments.FirstOrDefault(a => a.department_id == jd.department_id).department_name,
                                 unit_title = _entities.hr_unit.FirstOrDefault(u => u.unit_id == jd.unit_id).unit_title,
                                 subsection_title = _entities.hr_subsection.FirstOrDefault(s => s.subsection_id == jd.subsection_id).subsection_title,
                                 designation_name = _entities.designations.FirstOrDefault(des => des.designation_id == jd.designation_id).designation_name,
                                 staffgrade_title = _entities.hr_staffgrade.FirstOrDefault(st => st.staffgrade_id == jd.staffgrade_id).staffgrade_title, 
                             }).Distinct().FirstOrDefault();
           // var newEmployee = employees.SingleOrDefault(a => a.emp_id == emp_id);
            return employees;
        }

        #region Insert employee
        public int InsertEmployee(EmployeeModel oEmployee)
        {
            try
            {
                employee Insert_employee = new employee
                {
                    emp_firstname = oEmployee.emp_firstname,
                    emp_lastname = oEmployee.emp_lastname,
                    emp_marital_status = oEmployee.emp_marital_status,
                    emp_blood_group = oEmployee.emp_blood_group,
                    emp_gender = oEmployee.emp_gender,
                    emp_dateofbirth = oEmployee.emp_dateofbirth,
                    emp_image_file_name = oEmployee.emp_image_file_name,
                    emp_id_type = oEmployee.emp_id_type,
                    emp_id_no = oEmployee.emp_id_no,
                    company_id = oEmployee.company_id,
                    emp_code = oEmployee.emp_code,
                    emp_dateofjoin = oEmployee.emp_dateofjoin,
                    attendance_policy_id = oEmployee.attendance_policy_id,
                    branch_id = oEmployee.branch_id,
                    unit_id = oEmployee.unit_id,
                    department_id = oEmployee.department_id,
                    designation_id = oEmployee.designation_id,
                    staffgrade_id = oEmployee.staffgrade_id,
                    subsection_id = oEmployee.subsection_id,
                    //emp_reporting_method = oEmployee.emp_reporting_method
                };

                _entities.employees.Add(Insert_employee);
                _entities.SaveChanges();
                int lastemployee_id = Insert_employee.emp_id;

                hr_emp_job_details Insert_emp_job_details = new hr_emp_job_details
                {
                    company_id = oEmployee.company_id,
                    branch_id = oEmployee.branch_id,
                    unit_id = oEmployee.unit_id,
                    department_id = oEmployee.department_id,
                    designation_id = oEmployee.designation_id,
                    staffgrade_id = oEmployee.staffgrade_id,
                    subsection_id = oEmployee.subsection_id,
                    job_location_id = oEmployee.job_location_id,
                    start_date = oEmployee.start_date,
                    end_date = oEmployee.end_date,
                    emp_prop_confirmation_date = oEmployee.emp_prop_confirmation_date,
                    emp_id = lastemployee_id,
                    emp_supervisor = oEmployee.emp_supervisor,
                    attendance_policy_id = oEmployee.attendance_policy_id,
                    emp_reporting_method = oEmployee.emp_reporting_method,

                   

                };

                _entities.hr_emp_job_details.Add(Insert_emp_job_details);
                _entities.SaveChanges();

                
                return lastemployee_id;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        #endregion

        #region Update employee
        public bool UpdateEmployee(employee oEmployee)
        {
            try
            {

                employee Employee = _entities.employees.Find(oEmployee.emp_id);
                Employee.emp_firstname = oEmployee.emp_firstname;
                Employee.emp_lastname = oEmployee.emp_lastname;
                Employee.emp_marital_status = oEmployee.emp_marital_status;
                Employee.emp_blood_group = oEmployee.emp_blood_group;
                Employee.emp_gender = oEmployee.emp_gender;
                Employee.emp_dateofbirth = oEmployee.emp_dateofbirth;
                Employee.company_id = oEmployee.company_id;
                if (oEmployee.emp_image_file_name == "" || oEmployee.emp_image_file_name==null)
                {
                    Employee.emp_image_file_name = Employee.emp_image_file_name;
                }
                else
                {
                    Employee.emp_image_file_name = oEmployee.emp_image_file_name;
                }
                //Employee.emp_image_file_name = oEmployee.emp_image_file_name;
                Employee.emp_id_type = oEmployee.emp_id_type;
                Employee.emp_id_no = oEmployee.emp_id_no;


                _entities.SaveChanges();

                var userInfo = _entities.users.Where(u => u.employee_id == Employee.emp_id).FirstOrDefault();
                userInfo.user_firstname = Employee.emp_firstname;
                userInfo.user_lastname = Employee.emp_lastname;

                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region Delete employee
        public bool DeleteEmployee(int emp_id)
        {
            try
            {
                employee oEmployee = _entities.employees.FirstOrDefault(e => e.emp_id == emp_id);
                _entities.employees.Attach(oEmployee);
                _entities.employees.Remove(oEmployee);
                _entities.SaveChanges();
                return true;

            }

            catch (Exception)
            {
                return false;
            }
        }
        #endregion


        public List<hr_payroll_salary_header> GetEmployeeByPayrollSalaryID(string salary_head_permission)
        {
            List<hr_payroll_salary_header> oListOfPayrollSalaryHeader = new List<hr_payroll_salary_header>();
            if (salary_head_permission == "employee_module")
            {
                oListOfPayrollSalaryHeader = _entities.hr_payroll_salary_header.Where(p => p.psh_show_in_tmp_mod == "yes").ToList();

            }
            else
            {
                oListOfPayrollSalaryHeader = _entities.hr_payroll_salary_header.Where(p => p.psh_show_in_tmp_mod == "yes").ToList();

            }

            return oListOfPayrollSalaryHeader;
        }


        public string Post()
        {
            return "Done";
        }


        public bool UpdateSalaryByEmloyee(employee oEmployee)
        {
            try
            {
                if (oEmployee.emp_account_number == "")
                {
                    employee Employee = _entities.employees.Find(oEmployee.emp_id);

                    Employee.emp_bank_title = oEmployee.emp_bank_title;
                    _entities.SaveChanges();
                }
                else if (oEmployee.emp_bank_title == "")
                {
                    employee Employee = _entities.employees.Find(oEmployee.emp_id);
                    Employee.emp_account_number = oEmployee.emp_account_number;
                    _entities.SaveChanges();
                }
                else
                {
                    employee Employee = _entities.employees.Find(oEmployee.emp_id);
                    Employee.emp_account_number = oEmployee.emp_account_number;
                    Employee.emp_bank_title = oEmployee.emp_bank_title;
                    _entities.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        //for dropdown
        public object GetAllEmployeesForDrpdown(int company_id)
        {
            List<EmployeeModel> employees = (from e in _entities.employees
                                           where e.company_id == company_id
                                           select new EmployeeModel
                                           {
                                               emp_id = e.emp_id,
                                               emp_firstname = e.emp_firstname,
                                               emp_lastname = e.emp_lastname,
                                               branch_id = e.branch_id,
                                               emp_code_name = e.emp_code + "-" + e.emp_firstname + " " + e.emp_lastname
                                           }).ToList();
            return employees;
        }


        public bool CheckDuplicateEmpCode(string emp_code)
        {
            var checkEmpCodeIsExists = _entities.employees.FirstOrDefault(e=>e.emp_code == emp_code);
            return checkEmpCodeIsExists == null ? false : true;
        }


        public string Getrole_nameByID(int role_id)
        {
            string role_name = _entities.roles.Where(r => r.role_id == role_id).SingleOrDefault().role_name;
            return role_name;
        }


        public object GetEmployeeInformationById(int employee_id)
        {
            try
            {
                var data = _entities.employees.FirstOrDefault(o => o.emp_id == employee_id);
                return data;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public object GetAllEmployeesbyBranch(int company_id, int branchId, int roleId)
        {
            var roles = (_entities.roles.Find(roleId));
            var roleName = roles.role_name;

            if (roleName == "Admin" || roleName == "Management")
            {
                var data = (from emp in _entities.employees.Where(e => e.company_id == company_id)
                            join empCont in _entities.hr_emp_contact_info on emp.emp_id equals empCont.employee_id into EMPContaTable
                            from SubEmpCont in EMPContaTable.DefaultIfEmpty()
                            select new
                            {
                                emp_id = emp.emp_id,
                                emp_firstname = emp.emp_firstname + " " + emp.emp_lastname,
                                emp_code = emp.emp_code,
                                emp_mobile = SubEmpCont.emp_mobile,
                                emp_phone = SubEmpCont.emp_phone,
                                emp_email = SubEmpCont.emp_email,
                                emp_dateofjoin = emp.emp_dateofjoin

                            }).ToList().OrderByDescending(e => e.emp_id);
                return data;
            }
            else
            {
                var data = (from emp in _entities.employees.Where(e => e.company_id == company_id && e.branch_id==branchId)
                            join empCont in _entities.hr_emp_contact_info on emp.emp_id equals empCont.employee_id into EMPContaTable
                            from SubEmpCont in EMPContaTable.DefaultIfEmpty()
                            select new
                            {
                                emp_id = emp.emp_id,
                                emp_firstname = emp.emp_firstname + " " + emp.emp_lastname,
                                emp_code = emp.emp_code,
                                emp_mobile = SubEmpCont.emp_mobile,
                                emp_phone = SubEmpCont.emp_phone,
                                emp_email = SubEmpCont.emp_email,
                                emp_dateofjoin = emp.emp_dateofjoin

                            }).ToList().OrderByDescending(e => e.emp_id);
                return data;
            }


            
        }

        public object GetAllDrivers(int companyId)
        {
            var data = (from emp in _entities.employees.Where(e => e.company_id == companyId)
                     
                        select new
                        {
                            driver_id = emp.emp_id,
                            driver_name = emp.emp_firstname + " " + emp.emp_lastname,
                            emp_code = emp.emp_code,
                            emp_dateofjoin = emp.emp_dateofjoin

                        }).ToList().OrderByDescending(e => e.driver_id);
            return data;
        }

        public object GetAllCounterManForDropdown(int companyId)
        {
            return (from emp in _entities.employees.Where(x => x.company_id == companyId)
                join des in _entities.designations.Where(x => x.designation_name == "Counter Man") on emp.designation_id
                    equals des.designation_id select new
                    {
                        counter_man_id=emp.emp_id,
                        counter_man_name=emp.emp_firstname+" "+emp.emp_lastname,
                        des.designation_id,
                        des.designation_name
                    });
        }
    }
}