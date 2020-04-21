namespace ERPApi.Models.IRepository
{
    public interface IProductVariantTempRepository
    {
        object GetAllProductVariantTemp(int user_id);

        int DeleteProductVariantTempByUserId(int user_id);

        bool InsertProductVariantTemp(product_variant_temp oProductVariantTemp);

        bool DeleteProductVariantTemp(int product_variant_temp_id);

        bool UpdateProductVariantTemp(product_variant_temp oProductVariantTemp);
    }
}