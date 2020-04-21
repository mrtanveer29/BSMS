namespace ERPApi.Models.IRepository
{
    public interface IProductVariantRepository
    {
        object GetAllProductVariantByProductId(int product_id);

        bool InsertProductVariant(product_variant oProductVariant);

        bool DeleteProductVariant(int product_variant_id);

        bool UpdateProductVariant(product_variant oProductVariant);
    }
}