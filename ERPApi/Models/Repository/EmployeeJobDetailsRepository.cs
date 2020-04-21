using ERPApi.Models.IRepository;
using ERPApi.Models.StronglyType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPApi.Models.Repository
{
    public class EmployeeJobDetailsRepository:IEmployeeJobDetailsRepository
    {
         private ERPEntities _entities;

       public EmployeeJobDetailsRepository() {

           this._entities = new ERPEntities();
       
       }

        public object GetAllEmployeeJobDetails()
        {
            var empjobdetails = (from ej in _entities.hr_emp_job_details
                                 join com in _entities.companies
                                     on ej.company_id equals com.company_id
                                 join b in _entities.branches
                                 on ej.branch_id equals b.branch_id
                                 join st in _entities.hr_staffgrade
                                 on ej.staffgrade_id equals st.staffgrade_id
                                 join d in _entities.departments
                                 on ej.department_id equals d.department_id
                                 join des in _entities.designations
                                 on ej.designation_id equals des.designation_id
                                 join sub in _entities.hr_subsection
                                on ej.subsection_id equals sub.subsection_id
                                 join ap in _entities.hr_attendance_policy
                                on ej.attendance_policy_id equals ap.attendance_policy_id
                                join u in _entities.hr_unit
                                on ej.unit_id equals u.unit_id
                                 join e in _entities.employees
                                on ej.emp_id equals e.emp_id

                                 select new

                                 {
                                     emp_job_details_id = ej.emp_job_details_id,
                                     company_id = com.company_id,
                                     company_title = com.company_name,
                                     emp_branch_id = b.branch_id,
                                     branch_name = b.branch_name,
                                     unit_id = u.unit_id,
                                     unit_title = u.unit_title,
                                     emp_staffgrade_id = st.staffgrade_id,
                                     staffgrade_title = st.staffgrade_title,
                                     emp_department_id = d.department_id,
                                     department_name = d.department_name,
                                     emp_designation_id = des.designation_id,
                                     designation_name = des.designation_name,
                                     subsection_id = sub.subsection_id,
                                     subsection_title = sub.subsection_title,
                                     attendance_policy_id = ap.attendance_policy_id,
                                     attendance_policy_title = ap.attendance_policy_title,
                                     start_date = ej.start_date,
                                     is_active = ej.is_active,
                                     emp_id = ej.emp_id,
                                     emp_supervisor = ej.emp_supervisor,
                                     emp_reporting_method = ej.emp_reporting_method,
                                     emp_code = e.emp_code,
                                     emp_dateofjoin = e.emp_dateofjoin,
                                     emp_prop_confirmation_date = ej.emp_prop_confirmation_date
                                 }).ToList();
            return empjobdetails;
        }

        public hr_emp_job_details GetEmployeeJobDetailsByID(int emp_job_details_id)
        {
            throw new NotImplementedException();
        }

        public bool InsertEmployeeJobDetails(EmpJobDetailsModel oEmployeeJobDetails)
        {
            try
            {
                hr_emp_job_details Insert_emp_job_details = new hr_emp_job_details
                {
                    company_id = oEmployeeJobDetails.company_id,
                    branch_id = oEmployeeJobDetails.branch_id,
                    unit_id = oEmployeeJobDetails.unit_id,
                    department_id = oEmployeeJobDetails.department_id,
                    designation_id = oEmployeeJobDetails.designation_id,
                    staffgrade_id = oEmployeeJobDetails.staffgrade_id,
                    subsection_id = oEmployeeJobDetails.subsection_id,
                    start_date = oEmployeeJobDetails.start_date,
                    end_date = oEmployeeJobDetails.end_date,
                    is_active = oEmployeeJobDetails.is_active,
                    created_by = oEmployeeJobDetails.created_by,
                    created_date = oEmployeeJobDetails.created_date,
                    updated_by = oEmployeeJobDetails.updated_by,
                    updated_date = oEmployeeJobDetails.updated_date,
                    emp_prop_confirmation_date = oEmployeeJobDetails.emp_prop_confirmation_date,
                    emp_id = oEmployeeJobDetails.emp_id,
                    emp_supervisor =oEmployeeJobDetails.emp_supervisor,
                    attendance_policy_id = oEmployeeJobDetails.attendance_policy_id,
                    emp_reporting_method = oEmployeeJobDetails.emp_reporting_method

                };

                _entities.hr_emp_job_details.Add(Insert_emp_job_details);
                _entities.SaveChanges();
               int lastemployee_id = (Insert_emp_job_details.emp_id).GetValueOrDefault();


               employee emp = _entities.employees.Find(lastemployee_id);
               emp.emp_code = oEmployeeJobDetails.emp_code;
               emp.emp_dateofjoin = oEmployeeJobDetails.emp_dateofjoin;
               emp.branch_id = oEmployeeJobDetails.branch_id;
               emp.department_id = oEmployeeJobDetails.department_id;
               emp.designation_id = oEmployeeJobDetails.designation_id;
               emp.staffgrade_id = oEmployeeJobDetails.staffgrade_id;
               emp.attendance_policy_id = oEmployeeJobDetails.attendance_policy_id;
               emp.unit_id = oEmployeeJobDetails.unit_id;
               emp.subsection_id = oEmployeeJobDetails.subsection_id;
               _entities.SaveChanges();


                //data save in leave status meta
                //collect year from date
               var years = DateTime.Parse(oEmployeeJobDetails.emp_dateofjoin).Year;
               var leavepolicy = _entities.hr_leave_policy.ToList();
        

                foreach(var leave in leavepolicy)
                {
                    
                    hr_leave_status_meta leave_meta = new hr_leave_status_meta
                    {
                        emp_id = lastemployee_id,
                        leave_type_id = leave.leave_policy_id,
                        total_days = leave.total_days,
                        remaining_days = "0",
                        availed_days = "0",
                        year = years,
                        company_id = leave.company_id,
                        

                    };
                    _entities.hr_leave_status_meta.Add(leave_meta);
                    _entities.SaveChanges();

                }


                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateEmployeeJobDetails(EmpJobDetailsModel oEmployeeJobDetails)
        {
            try
            {
                hr_emp_job_details EmployeeJobDetails = _entities.hr_emp_job_details.SingleOrDefault(j => j.emp_id == oEmployeeJobDetails.emp_id);
                if (EmployeeJobDetails==null)
                {
                    hr_emp_job_details jobDetails=new hr_emp_job_details
                    {
                        company_id = oEmployeeJobDetails.company_id,
                branch_id = oEmployeeJobDetails.branch_id,
                unit_id = oEmployeeJobDetails.unit_id,
                department_id = oEmployeeJobDetails.department_id,
                designation_id = oEmployeeJobDetails.designation_id,
                staffgrade_id = oEmployeeJobDetails.staffgrade_id,
                subsection_id = oEmployeeJobDetails.subsection_id,
                start_date = oEmployeeJobDetails.start_date,
                end_date = oEmployeeJobDetails.end_date,
                is_active = oEmployeeJobDetails.is_active,
                created_by = oEmployeeJobDetails.created_by,
                created_date = oEmployeeJobDetails.created_date,
                updated_by = oEmployeeJobDetails.updated_by,
                updated_date = oEmployeeJobDetails.updated_date,
                emp_prop_confirmation_date = oEmployeeJobDetails.emp_prop_confirmation_date,
                emp_id = oEmployeeJobDetails.emp_id,
                emp_supervisor = oEmployeeJobDetails.emp_supervisor,
                attendance_policy_id = oEmployeeJobDetails.attendance_policy_id,
                emp_reporting_method = oEmployeeJobDetails.emp_reporting_method,
                    };
                    _entities.hr_emp_job_details.Add(jobDetails);
                _entities.SaveChanges();

                employee emp = _entities.employees.Find(oEmployeeJobDetails.emp_id);
                emp.emp_code = oEmployeeJobDetails.emp_code;
                emp.emp_dateofjoin = oEmployeeJobDetails.emp_dateofjoin;
                emp.branch_id = oEmployeeJobDetails.branch_id;
                emp.department_id = oEmployeeJobDetails.department_id;
                emp.designation_id = oEmployeeJobDetails.designation_id;
                emp.staffgrade_id = oEmployeeJobDetails.staffgrade_id;
                emp.attendance_policy_id = oEmployeeJobDetails.attendance_policy_id;
                emp.unit_id = oEmployeeJobDetails.unit_id;
                emp.subsection_id = oEmployeeJobDetails.subsection_id;
                _entities.SaveChanges();
                    
                }
                else
                {
                    EmployeeJobDetails.company_id = oEmployeeJobDetails.company_id;
                    EmployeeJobDetails.branch_id = oEmployeeJobDetails.branch_id;
                    EmployeeJobDetails.unit_id = oEmployeeJobDetails.unit_id;
                    EmployeeJobDetails.department_id = oEmployeeJobDetails.department_id;
                    EmployeeJobDetails.designation_id = oEmployeeJobDetails.designation_id;
                    EmployeeJobDetails.staffgrade_id = oEmployeeJobDetails.staffgrade_id;
                    EmployeeJobDetails.subsection_id = oEmployeeJobDetails.subsection_id;
                    EmployeeJobDetails.start_date = oEmployeeJobDetails.start_date;
                    EmployeeJobDetails.end_date = oEmployeeJobDetails.end_date;
                    EmployeeJobDetails.is_active = oEmployeeJobDetails.is_active;
                    EmployeeJobDetails.created_by = oEmployeeJobDetails.created_by;
                    EmployeeJobDetails.created_date = oEmployeeJobDetails.created_date;
                    EmployeeJobDetails.updated_by = oEmployeeJobDetails.updated_by;
                    EmployeeJobDetails.updated_date = oEmployeeJobDetails.updated_date;
                    EmployeeJobDetails.emp_prop_confirmation_date = oEmployeeJobDetails.emp_prop_confirmation_date;
                    EmployeeJobDetails.emp_id = oEmployeeJobDetails.emp_id;
                    EmployeeJobDetails.emp_supervisor = oEmployeeJobDetails.emp_supervisor;
                    EmployeeJobDetails.attendance_policy_id = oEmployeeJobDetails.attendance_policy_id;
                    EmployeeJobDetails.emp_reporting_method = oEmployeeJobDetails.emp_reporting_method;

                    _entities.SaveChanges();

                    employee emp = _entities.employees.Find(oEmployeeJobDetails.emp_id);
                    emp.emp_code = oEmployeeJobDetails.emp_code;
                    emp.emp_dateofjoin = oEmployeeJobDetails.emp_dateofjoin;
                    emp.branch_id = oEmployeeJobDetails.branch_id;
                    emp.department_id = oEmployeeJobDetails.department_id;
                    emp.designation_id = oEmployeeJobDetails.designation_id;
                    emp.staffgrade_id = oEmployeeJobDetails.staffgrade_id;
                    emp.attendance_policy_id = oEmployeeJobDetails.attendance_policy_id;
                    emp.unit_id = oEmployeeJobDetails.unit_id;
                    emp.subsection_id = oEmployeeJobDetails.subsection_id;
                    _entities.SaveChanges();
                }
                

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteEmployeeJobDetails(int emp_job_details_id)
        {
            try
            {
                hr_emp_job_details oEmployeeJobDetails = _entities.hr_emp_job_details.FirstOrDefault(em => em.emp_job_details_id == emp_job_details_id);
                _entities.hr_emp_job_details.Attach(oEmployeeJobDetails);
                _entities.hr_emp_job_details.Remove(oEmployeeJobDetails);
                _entities.SaveChanges();
                return true;

            }

            catch (Exception)
            {
                return false;
            }
        }


        public List<employee> GetAllEmployeeCode(int company_id)
        {
            List<employee> employee = _entities.employees.Where(e => e.company_id == company_id).ToList();
            return employee;
        }
    }
}