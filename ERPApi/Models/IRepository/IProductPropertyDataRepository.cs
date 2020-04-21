using System.Collections.Generic;

namespace ERPApi.Models.IRepository
{
    public interface IProductPropertyDataRepository
    {
        List<product_property_data> GetProductPropertyDataByProductId(int product_id);
    }
}