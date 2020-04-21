using System.Collections.Generic;

namespace ERPApi.Models.IRepository
{
    public interface ICountryRepository
    {
        List<country> GetAllCountries();

        country GetCountryByID(int country_id);

        bool InsertCountry(country oCountry);

        bool DeleteCountry(int country_id);

        bool UpdateCountry(country oCountry);

        bool CheckDuplicateCountry(string CountryName);

        object GetAllRBOMappingmaster();
    }
}