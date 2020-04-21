using ERPApi.Models.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ERPApi.Models.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private ERPEntities _entities;

        public CountryRepository()
        {
            this._entities = new ERPEntities();
        }

        public List<country> GetAllCountries()
        {
            List<country> countries = _entities.countries.ToList();
            return countries;
        }

        public country GetCountryByID(int country_id)
        {
            throw new NotImplementedException();
        }

        public bool InsertCountry(country oCountry)
        {
            try
            {
                country insert_country = new country
                {
                    country_name = oCountry.country_name,
                    country_details = oCountry.country_details,
                    created_by = oCountry.created_by,
                    created_date = oCountry.created_date,
                    updated_by = oCountry.updated_by,
                    updated_date = oCountry.updated_date,
                    company_id = oCountry.company_id,
                    is_active = oCountry.is_active
                };
                _entities.countries.Add(insert_country);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteCountry(int country_id)
        {
            try
            {
                country oCountry = _entities.countries.FirstOrDefault(c => c.country_id == country_id);
                _entities.countries.Attach(oCountry);
                _entities.countries.Remove(oCountry);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateCountry(country oCountry)
        {
            try
            {
                country con = _entities.countries.Find(oCountry.country_id);
                con.country_name = oCountry.country_name;
                con.country_details = oCountry.country_details;
                con.updated_by = oCountry.updated_by;
                con.updated_date = oCountry.updated_date;
                con.company_id = oCountry.company_id;
                con.is_active = oCountry.is_active;
                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool CheckDuplicateCountry(string CountryName)
        {
            var checkDuplicateCountry = _entities.countries.FirstOrDefault(c => c.country_name == CountryName);

            bool return_type = checkDuplicateCountry == null ? false : true;
            return return_type;
        }

        public object GetAllRBOMappingmaster()
        {
            throw new NotImplementedException();
        }
    }
}