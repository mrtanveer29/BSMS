using ERPApi.Models.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ERPApi.Models.Repository
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private ERPEntities _entities;

        public CurrencyRepository()
        {
            this._entities = new ERPEntities();
        }

        public List<currency> GetAllCurrency(int company_id)
        {
            List<currency> currency = _entities.currencies.Where(u => u.company_id == company_id).ToList();
            return currency;
        }

        public currency GetCurrencyByID(int currency_id)
        {
            currency currency = _entities.currencies.Find(currency_id);
            return currency;
        }

        public currency GetDefaultCurrencyByCompanyId(int company_id)
        {
            currency currency = _entities.currencies.FirstOrDefault(p => p.company_id == company_id && p.is_default == true);
            return currency;
        }

        public bool InsertCurrency(currency currency)
        {
            try
            {
                if (currency.is_default == true)
                {
                    _entities.Database.ExecuteSqlCommand(" update currency set is_default=false where currency_id<>" + currency.currency_id);
                }

                currency insert_currency = new currency
                {
                    currency_name = currency.currency_name,
                    currency_symbol = currency.currency_symbol,
                    is_active = currency.is_active,
                    is_default = currency.is_default,
                    created_by = currency.created_by,
                    created_date = currency.created_date,
                    company_id = currency.company_id
                };
                _entities.currencies.Add(insert_currency);
                _entities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteCurrency(int currency_id)
        {
            try
            {
                currency currency = _entities.currencies.Find(currency_id);
                _entities.currencies.Attach(currency);
                _entities.currencies.Remove(currency);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateCurrency(currency currency)
        {
            try
            {
                currency con = _entities.currencies.Find(currency.currency_id);
                con.currency_name = currency.currency_name;
                con.currency_symbol = currency.currency_symbol;
                con.is_active = currency.is_active;
                con.is_default = currency.is_default;
                con.updated_by = currency.updated_by;
                con.updated_date = currency.updated_date;
                _entities.SaveChanges();

                if (currency.is_default == true)
                {
                    _entities.Database.ExecuteSqlCommand(" update currency set is_default=false where currency_id<>" + currency.currency_id);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool CheckDuplicateCurrency(currency currency, string action)
        {
            try
            {
                if (action == "add")
                {
                    var item = _entities.currencies.Where(p => p.currency_name == currency.currency_name && p.company_id == currency.company_id);
                    return item.Any();
                }
                else
                {
                    var item = _entities.currencies.Where(p => p.currency_id != currency.currency_id && p.currency_name == currency.currency_name && p.company_id == currency.company_id);
                    return item.Any();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public List<currency> GetAllCurrencyForCompany()
        {
            List<currency> lstCurrencies = _entities.currencies.ToList();
            return lstCurrencies;
            // throw new NotImplementedException();
        }
    }
}