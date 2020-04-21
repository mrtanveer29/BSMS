using System.Collections.Generic;

namespace ERPApi.Models.IRepository
{
    public interface ICurrencyRepository
    {
        List<currency> GetAllCurrency(int company_id);

        List<currency> GetAllCurrencyForCompany();

        currency GetDefaultCurrencyByCompanyId(int company_id);

        bool InsertCurrency(currency currency);

        bool DeleteCurrency(int currency_id);

        bool UpdateCurrency(currency currency);

        bool CheckDuplicateCurrency(currency currency, string action);
    }
}