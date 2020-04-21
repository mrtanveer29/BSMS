using ERPApi.Models.StronglyType;
using System.Collections.Generic;

namespace ERPApi.Models.IRepository
{
    public interface ICompanyRepository
    {
        object GetAllCompanies();

        object GetAllBank(int company_id);

        bool InsertBank(CompanyModel oCompany);

        bool UpdateBank(CompanyModel oCompany);

        CompanyModel GetCompanyByID(int company_id);

        string GetCompanyCode(int company_id); // asma

        string GetCompanyName(int company_id);

        string GetCompanyFlagLogo(int company_id);

        bool InsertCompany(CompanyModel oCompany, CompanyAdminModel oAdmin, List<bank> bank_list);

        bool UpdateCompany(CompanyModel oCompany);

        bool DeleteCompany(int company_id);

        bool AddCompanyDocument(CompanyModel oCompany);

        bool CheckDuplicateCompany(string company_name);

        bool DeleteCompnayBank(int bank_id);


    }
}