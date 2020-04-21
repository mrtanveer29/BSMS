using Newtonsoft.Json.Serialization;
using System.Net.Http.Formatting;

namespace ERPApi.Models
{
    public static class RequestFormat
    {
        public static JsonMediaTypeFormatter JsonFormaterString()
        {
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;

            json.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat;
            json.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
            json.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore; //   Include makes the null value appear in result 
            //json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ContractResolver = new CamelCasePropertyNamesContractResolver();
            //json.Culture = new CultureInfo("it-IT");
            return formatter;
        }
    }
}