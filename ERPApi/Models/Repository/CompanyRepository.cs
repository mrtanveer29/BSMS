using ERPApi.Models.IRepository;
using ERPApi.Models.StronglyType;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ERPApi.Models.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private ERPEntities _entities;

        public CompanyRepository()
        {
            this._entities = new ERPEntities();
        }

        public object GetAllCompanies()
        {
            var company = (from c in _entities.companies
                           join a in _entities.addresses
                               on c.company_id equals a.source_id
                           where a.source_type == "Company"
                           select new

                           {
                               company_id = c.company_id,
                               company_name = c.company_name,
                               address_1 = a.address_1,
                               phone = a.phone,
                               email = a.email,
                               is_active = c.is_active

                           }).ToList();
            //var company = _entities.companies.ToList();
            return company;
        }

        public CompanyModel GetCompanyByID(int company_id)
        {
            try
            {
                CompanyModel comModel = new CompanyModel();
                var company = _entities.companies.SingleOrDefault(a => a.company_id == company_id);
                var address = _entities.addresses.FirstOrDefault(a => a.source_id == company_id && a.source_type == "Company");

                comModel.company_code = company.company_code;
                comModel.company_id = company.company_id;
                comModel.company_name = company.company_name;
                comModel.logo_path = company.logo_path;
                comModel.flag_path = company.flag_path;
                comModel.is_active = company.is_active.ToString();
                comModel.is_parent_company = company.is_parent_company.ToString();

                comModel.address_1 = address.address_1;
                comModel.address_2 = address.address_2;
                comModel.city_id = Convert.ToInt32(address.city_id);
                comModel.country_id = Convert.ToInt32(address.country_id);
                comModel.currency_id = company.currency_id??0;
                comModel.email = address.email;
                comModel.fax = address.fax;
                comModel.mobile = address.mobile;
                comModel.phone = address.phone;
                comModel.web = address.web;
                comModel.zip_code = address.zip_code;
             

                return comModel;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool InsertCompany(CompanyModel oCompany, CompanyAdminModel oAdmin, List<bank> bank_list)
        {
            try
            {
                company insert_company = new company
                {
                    company_name = oCompany.company_name,
                    company_code = oCompany.company_code.ToUpper(),
                    is_active = Convert.ToBoolean(oCompany.is_active),
                    logo_path = oCompany.logo_path,
                    flag_path = oCompany.flag_path,
                    is_parent_company = Convert.ToBoolean(oCompany.is_parent_company)
                };
                _entities.companies.Add(insert_company);
                int success = _entities.SaveChanges();

                if (success != 0)
                {
                    var last_insert_id = insert_company.company_id;

                    address addresses = new address

                    {
                        source_id = last_insert_id,
                        source_type = "Company",
                        address_type = "Main Address",
                        address_1 = oCompany.address_1,
                        address_2 = oCompany.address_2,
                        country_id = oCompany.country_id,
                        city_id = oCompany.city_id,
                        zip_code = oCompany.zip_code,
                        email = oCompany.email,
                        phone = oCompany.phone,
                        fax = oCompany.fax,
                        web = oCompany.web,
                        mobile = oCompany.mobile
                    };
                    _entities.addresses.Add(addresses);
                    _entities.SaveChanges();
                }

                if (success != 0)
                {
                    var last_insert_id = insert_company.company_id;

                    foreach (var item in bank_list)
                    {
                        bank banks = new bank
                        {
                            source_id = last_insert_id,
                            source_type = "Company",
                            bank_name = item.bank_name,
                            bank_acc_no = item.bank_acc_no,
                            bank_acc_id = item.bank_acc_id,
                            bank_branch_name = item.bank_branch_name,
                            swift_code = item.swift_code
                        };

                        _entities.banks.Add(banks);
                    }

                    _entities.SaveChanges();
                }
                var role_id = 0;
                if (success != 0)
                {
                    var last_insert_id = insert_company.company_id;
                    //var checkRolename = _entities.roles.FirstOrDefault(o=>o.role_name==oCompany.role_id);
                    //if (checkRolename ==null)
                    //{
                        role tempRole = new role
                        {
                            role_name = "Admin",
                            company_id = last_insert_id,
                            is_active = true
                        };
                        _entities.roles.Add(tempRole);
                        _entities.SaveChanges();
                        role_id = _entities.roles.Max(o => o.role_id);
                    //}
                    //role_id = checkRolename.role_id;


                }
                if (success != 0)
                {
                    var last_insert_id = insert_company.company_id;
                    employee tempemEmployee = new employee
                    {
                        emp_firstname = oAdmin.first_name,
                        emp_lastname = oAdmin.last_name,
                        emp_dateofbirth = oAdmin.dob,
                        emp_gender = oAdmin.sex,
                        employee_email = oAdmin.admin_email,
                        company_id = last_insert_id

                    };
                    _entities.employees.Add(tempemEmployee);
                    _entities.SaveChanges();
                }
                if (success != 0)
                {
                    var last_emp_id = _entities.employees.Max(x => x.employee_id);
                    var last_insert_id = insert_company.company_id;

                    user adminuser = new user
                    {
                        user_name = oAdmin.user_name,
                        password = oAdmin.password,
                        user_firstname = oAdmin.first_name,
                        user_lastname = oAdmin.last_name,
                        role_id = role_id,
                        company_id = last_insert_id,
                        employee_id = _entities.employees.Max(i=>i.emp_id)
                    };
                    _entities.users.Add(adminuser);
                    _entities.SaveChanges();

                    hr_emp_contact_info emp_contact=new hr_emp_contact_info
                    {
                        emp_email =oAdmin.admin_email,
                        emp_mobile = oAdmin.admin_mobile,
                        emp_phone = oAdmin.admin_phone,
                        zip_code = int.Parse(oAdmin.admin_zip_code),
                        permanent_address = oAdmin.admin_address_1,
                        present_address = oAdmin.admin_address_2,
                        
                    };
                    _entities.hr_emp_contact_info.Add(emp_contact);
                    _entities.SaveChanges();
                }

                var tempRoleID = _entities.roles.FirstOrDefault(o => o.role_name == "Admin").role_id;
                var tempRolePermission = _entities.user_permission.Where(u => u.user_role_id == tempRoleID).ToList();

                foreach (var itemUserpermission in tempRolePermission)
                {
                    user_permission permission = new user_permission
                    {
                        user_control_id = itemUserpermission.user_control_id,
                        user_role_id = role_id,
                        user_au_id = _entities.users.Max(u=>u.user_id)
                    };
                    _entities.user_permission.Add(permission);
                    _entities.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateCompany(CompanyModel oCompany)
        {
            try
            {
                company Company = _entities.companies.Find(oCompany.company_id);
                Company.company_name = oCompany.company_name;
                Company.company_code = oCompany.company_code.ToUpper();
                Company.is_active = Convert.ToBoolean(oCompany.is_active);
                Company.logo_path = oCompany.logo_path;
                Company.is_parent_company = Convert.ToBoolean(oCompany.is_parent_company);
                Company.flag_path = oCompany.flag_path;
                Company.currency_id = oCompany.currency_id;

                int success = _entities.SaveChanges();

                if (success != 0)
                {
                    //var last_insert_id = insert_company.company_id;

                    address Addresses = _entities.addresses.FirstOrDefault(a => a.source_id == oCompany.company_id);

                    Addresses.source_type = "Company";
                    Addresses.address_type = "Main Address";
                    Addresses.address_1 = oCompany.address_1;
                    Addresses.address_2 = oCompany.address_2;
                    Addresses.country_id = oCompany.country_id;
                    Addresses.city_id = oCompany.city_id;
                    Addresses.zip_code = oCompany.zip_code;
                    Addresses.email = oCompany.email;
                    Addresses.phone = oCompany.phone;
                    Addresses.fax = oCompany.fax;
                    Addresses.web = oCompany.web;
                    Addresses.mobile = oCompany.mobile;

                    _entities.SaveChanges();
                }

                if (success != 0)
                {
                    //var last_insert_id = insert_company.company_id;

                    bank banks = _entities.banks.Find(oCompany.company_id);

                    banks.source_type = "Company";
                    banks.bank_name = oCompany.bank_name;
                    banks.bank_acc_no = oCompany.bank_acc_no;
                    banks.bank_acc_id = oCompany.bank_acc_id;
                    banks.bank_branch_name = oCompany.bank_branch_name;
                    banks.swift_code = oCompany.swift_code;

                    _entities.SaveChanges();
                }


                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteCompany(int company_id)
        {
            try
            {
                company oCompany = _entities.companies.FirstOrDefault(co => co.company_id == company_id);
                _entities.companies.Attach(oCompany);
                _entities.companies.Remove(oCompany);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool DeleteCompnayBank(int bank_id)
        {
            try
            {
                bank oBank = _entities.banks.Find(bank_id);
                _entities.banks.Attach(oBank);
                _entities.banks.Remove(oBank);
                _entities.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //public bool CheckDuplicateCompany(string company_title)
        //{
        //    var checkDplicateCompany = _entities.companies.FirstOrDefault(co => co.company_name == company_name);

        //    bool return_type = checkDplicateCompany == null ? false : true;
        //    return return_type;
        //}

        public bool AddCompanyDocument(CompanyModel oCompany)
        {
            try
            {
                var insertCompanyDocument = new company
                {
                    company_id = oCompany.company_id,
                    company_name = oCompany.company_name,
                    company_code = oCompany.company_code.ToUpper(),
                    is_active = Convert.ToBoolean(oCompany.is_active),
                    logo_path = oCompany.logo_path,
                    is_parent_company = Convert.ToBoolean(oCompany.is_parent_company)
                };

                _entities.companies.Add(insertCompanyDocument);
                _entities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public string GetCompanyCode(int company_id)
        {
            try
            {
                string companyCode = _entities.companies.Where(c => c.company_id == company_id).FirstOrDefault().company_code;
                return companyCode;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string GetCompanyName(int company_id)
        {
            try
            {
                string companyCode = _entities.companies.Where(c => c.company_id == company_id).FirstOrDefault().company_name;
                return companyCode;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string GetCompanyFlagLogo(int company_id)
        {
            try
            {
                string companyFlag = _entities.companies.Where(c => c.company_id == company_id).FirstOrDefault().flag_path;
                return companyFlag;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public object GetAllBank(int company_id)
        {
            var Bank = _entities.banks.Where(a => a.source_id == company_id && a.source_type == "Company").ToList();
            return Bank;
        }


        public bool UpdateBank(CompanyModel oCompany)
        {
            try
            {
                //var last_insert_id = insert_company.company_id;

                bank banks = _entities.banks.Find(oCompany.bank_id);

                banks.source_type = "Company";
                banks.bank_name = oCompany.bank_name;
                banks.bank_acc_no = oCompany.bank_acc_no;
                banks.bank_acc_id = oCompany.bank_acc_id;
                banks.bank_branch_name = oCompany.bank_branch_name;
                banks.swift_code = oCompany.swift_code;

                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public bool InsertBank(CompanyModel oCompany)
        {

            try
            {
                bank insert_bank = new bank
                 {
                     source_id = oCompany.company_id,
                     source_type = "Company",
                     bank_name = oCompany.bank_name,
                     bank_acc_no = oCompany.bank_acc_no,
                     bank_acc_id = oCompany.bank_acc_id,
                     bank_branch_name = oCompany.bank_branch_name,
                     swift_code = oCompany.swift_code
                 };
                _entities.banks.Add(insert_bank);
                _entities.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public bool CheckDuplicateCompany(string company_name)
        {
            var checkDplicateCompany = _entities.companies.FirstOrDefault(co => co.company_name == company_name);

                bool return_type = checkDplicateCompany == null ? false : true;
               return return_type;
        }


        
    }
}