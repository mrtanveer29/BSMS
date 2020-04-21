namespace ERPApi.Models.IRepository
{
    public interface IProductSalesMappingRepository
    {
        object GetAllProductSalesMappingByProductId(int product_id);

        bool InsertProductSalesMapping(product_sales_mapping oProductSalesMapping);

        bool DeleteProductSalesMapping(int product_sales_mapping_id);

        bool UpdateProductSalesMapping(product_sales_mapping oProductSalesMapping);
    }
}