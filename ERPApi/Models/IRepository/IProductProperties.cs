using ERPApi.Models.StronglyType;
using System.Collections.Generic;

namespace ERPApi.Models.IRepository
{
    public interface IProductProperties
    {
        object GetAllProductProperties();


       
        List<ProductPropertiesModel> GetSingleProductPropertiesId(int product_properties_id);

        List<ProductPropertiesModel> GetComboProductProperties(int product_category_id, int property_type_id);

        bool CheckProductPropertiesForDuplicateByName(string product_properties_name);

        bool InsertProductProperties(product_properties oProductProperties);

        bool UpdateProductProperties(product_properties oProductProperties);

        bool DeleteProductProperties(int product_properties_id);
    }
}