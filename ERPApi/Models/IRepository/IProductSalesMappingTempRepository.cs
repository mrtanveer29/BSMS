namespace ERPApi.Models.IRepository
{
    public interface IProductSalesMappingTempRepository
    {
        object GetAllProductSalesMappingTemp(int user_id);

        int DeleteProductSalesMappingTempByUserId(int user_id);

        bool InsertProductSalesMappingTemp(product_sales_mapping_temp oProductSalesMappingTemp);

        bool DeleteProductSalesMappingTemp(int product_sales_mapping_temp_id);

        bool UpdateProductSalesMappingTemp(product_sales_mapping_temp oProductSalesMappingTemp);
    }
}