using System.Collections.Generic;

namespace ERPApi.Models.IRepository
{
    public interface IProductCategoryAttributeMappingRepository
    {
        List<product_category_attribute_mapping> GetAllProductCategoryAttributeMappingByProductCategoryId(int product_category_id);

        bool InsertProductCategoryAttributeMapping(product_category_attribute_mapping oProductCategoryAttributeMapping);

        int DeleteProductCategoryAttributeMappingCommand(int product_category_id);
    }
}