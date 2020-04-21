using System.Collections.Generic;

namespace ERPApi.Models.IRepository
{
    public interface IProductPropertiesValueRepository
    {
        List<product_properties_value> GetProductPropertiesValuesByProductPropertiesId(int product_properties_id);

        int InsertProductPropertiesValue(product_properties_value oProductPropertiesValue);

        bool UpdateProductPropertiesValue(product_properties_value oProductPropertiesValue);

        bool DeleteProductPropertiesValue(int oProductPropertiesValue);

        bool UpdateProductPropertiesValueOrder(string allIds);
    }
}