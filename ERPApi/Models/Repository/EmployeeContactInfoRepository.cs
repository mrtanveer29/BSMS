using ERPApi.Models.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPApi.Models.Repository
{
    public class EmployeeContactInfoRepository:IEmployeeContactInfoRepository
    {
         private ERPEntities _entities;

         public EmployeeContactInfoRepository()
         {

           this._entities = new ERPEntities();
       
       }
        public object GetAllEmployeeContactInfo()
        {
            var empcontact = (from cont in _entities.hr_emp_contact_info
                              join ci in _entities.cities
                           on cont.city_id equals ci.city_id
                              join co in _entities.countries
                                  on cont.country_id equals co.country_id
                              select new
                              {

                                  emp_contact_info_id = cont.emp_contact_info_id,
                                  city_id = ci.city_id,
                                  city_name = ci.city_name,
                                  country_id = co.country_id,
                                  country_name = co.country_name,
                                  present_address = cont.present_address,
                                  permanent_address = cont.permanent_address,
                                  zip_code = cont.zip_code,
                                  emp_email = cont.emp_email,
                                  emp_phone = cont.emp_phone,
                                  emp_mobile = cont.emp_mobile,
                                  employee_id = cont.employee_id
                              }).ToList();
            return empcontact;
        }

        public hr_emp_contact_info GetEmployeeContactInfoByID(int emp_contact_info_id)
        {
            throw new NotImplementedException();
        }

        public bool InsertEmployeeContactInfo(hr_emp_contact_info oEmployeeContactInfo)
        {
            try
            {
                hr_emp_contact_info Insert_emp_contact_info = new hr_emp_contact_info
                {
                    city_id = oEmployeeContactInfo.city_id,
                    country_id = oEmployeeContactInfo.country_id,
                    present_address = oEmployeeContactInfo.present_address,
                    permanent_address = oEmployeeContactInfo.permanent_address,
                    zip_code = oEmployeeContactInfo.zip_code,
                    emp_email = oEmployeeContactInfo.emp_email,
                    emp_mobile = oEmployeeContactInfo.emp_mobile,
                    emp_phone = oEmployeeContactInfo.emp_phone,
                    employee_id = oEmployeeContactInfo.employee_id,
                    emergency_contact_name = oEmployeeContactInfo.emergency_contact_name,
                    emergency_contact_id = oEmployeeContactInfo.emergency_contact_id,
                    emergency_contact_address = oEmployeeContactInfo.emergency_contact_address,
                    emergency_contact_mobile = oEmployeeContactInfo.emergency_contact_mobile,
                    emergency_contact_email = oEmployeeContactInfo.emergency_contact_email,
                    emergency_contact_relation = oEmployeeContactInfo.emergency_contact_relation

                };

                _entities.hr_emp_contact_info.Add(Insert_emp_contact_info);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateEmployeeContactInfo(hr_emp_contact_info oEmployeeContactInfo)
        {
            try
            {
                 hr_emp_contact_info EmployeeContactInfo = _entities.hr_emp_contact_info.SingleOrDefault(a=>a.employee_id == oEmployeeContactInfo.employee_id);
                 if (EmployeeContactInfo==null)
                 {
                     hr_emp_contact_info hrEmp = new hr_emp_contact_info
                     {
                         city_id = oEmployeeContactInfo.city_id,
                         country_id = oEmployeeContactInfo.country_id,
                         present_address = oEmployeeContactInfo.present_address,
                         permanent_address = oEmployeeContactInfo.permanent_address,
                         zip_code = oEmployeeContactInfo.zip_code,
                         emp_email=oEmployeeContactInfo.emp_email,
                         emp_mobile = oEmployeeContactInfo.emp_mobile,
                         emp_phone = oEmployeeContactInfo.emp_phone,
                         employee_id = oEmployeeContactInfo.employee_id,
                         emergency_contact_name = oEmployeeContactInfo.emergency_contact_name,
                         emergency_contact_id = oEmployeeContactInfo.emergency_contact_id,
                         emergency_contact_address = oEmployeeContactInfo.emergency_contact_address,
                         emergency_contact_mobile = oEmployeeContactInfo.emergency_contact_mobile,
                         emergency_contact_email = oEmployeeContactInfo.emergency_contact_email,
                         emergency_contact_relation = oEmployeeContactInfo.emergency_contact_relation
                     };
                     _entities.hr_emp_contact_info.Add(hrEmp);
                     _entities.SaveChanges();
                 }
                 else
                 {
                     EmployeeContactInfo.city_id = oEmployeeContactInfo.city_id;
                     EmployeeContactInfo.country_id = oEmployeeContactInfo.country_id;
                     EmployeeContactInfo.present_address = oEmployeeContactInfo.present_address;
                     EmployeeContactInfo.permanent_address = oEmployeeContactInfo.permanent_address;
                     EmployeeContactInfo.zip_code = oEmployeeContactInfo.zip_code;
                     EmployeeContactInfo.emp_email = oEmployeeContactInfo.emp_email;
                     EmployeeContactInfo.emp_mobile = oEmployeeContactInfo.emp_mobile;
                     EmployeeContactInfo.emp_phone = oEmployeeContactInfo.emp_phone;
                     EmployeeContactInfo.employee_id = oEmployeeContactInfo.employee_id;
                     EmployeeContactInfo.emergency_contact_name = oEmployeeContactInfo.emergency_contact_name;
                     EmployeeContactInfo.emergency_contact_address = oEmployeeContactInfo.emergency_contact_address;
                     EmployeeContactInfo.emergency_contact_email = oEmployeeContactInfo.emergency_contact_email;
                     EmployeeContactInfo.emergency_contact_mobile = oEmployeeContactInfo.emergency_contact_mobile;
                     EmployeeContactInfo.emergency_contact_relation = oEmployeeContactInfo.emergency_contact_relation;
                     EmployeeContactInfo.emergency_contact_id = oEmployeeContactInfo.emergency_contact_id;


                     _entities.SaveChanges();
                 }
                   
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteEmployeeContactInfo(int emp_contact_info_id)
        {
            try
            {
                hr_emp_contact_info oEmployeeContactInfo = _entities.hr_emp_contact_info.FirstOrDefault(ec => ec.emp_contact_info_id == emp_contact_info_id);
                _entities.hr_emp_contact_info.Attach(oEmployeeContactInfo);
                _entities.hr_emp_contact_info.Remove(oEmployeeContactInfo);
                _entities.SaveChanges();
                return true;

            }

            catch (Exception)
            {
                return false;
            }
        }
    }
}