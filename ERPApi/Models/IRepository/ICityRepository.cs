namespace ERPApi.Models.IRepository
{
    public interface ICityRepository
    {
        object GetAllCities();

        city GetCityByID(int city_id);

        city GetCityBYName(string city_name);

        city GetCityByDetails(string city_details);

        city GetCityByCountryID(int country_id);

        bool CheckCityForDuplicateByName(string city_name);

        bool InsertCity(city ocity);

        bool UpdateCity(city ocity);

        bool DeleteCity(int city_id);
    }
}