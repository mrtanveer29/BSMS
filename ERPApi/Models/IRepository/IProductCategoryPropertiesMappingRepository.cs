using ERPApi.Models.StronglyType;
using System.Collections.Generic;

namespace ERPApi.Models.IRepository
{
    public interface IProductCategoryPropertiesMappingRepository
    {
        List<product_category_properties_mapping> GetAllProductCategoryPropertiesMappingByProductCategoryId(int product_category_id);

        bool InsertProductProductCategoryPropertiesMapping(product_category_properties_mapping oProductCategoryPropertiesMapping);

        int DeleteProductCategoryPropertiesMappingCommand(int product_category_id);

        List<ProductCategoryPropertiesDetailsModel> GetProductCategoryPropertiesDetails(int product_category_id);
    }
}