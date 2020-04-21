using ERPApi.Models.IRepository;
using ERPApi.Models.StronglyType;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ERPApi.Models.Repository
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private ERPEntities _entities;

        public ProductCategoryRepository()
        {
            this._entities = new ERPEntities();
        }

        public object GetAllProductCategory()
        {
            var productCategory = _entities.product_category.Where(p => p.product_category_type == "G").ToList();

            return productCategory;
        }

       // public List<product_category> GetAllProductCategoryListOnly()
       // {
            //var productCategory = _entities.product_category.OrderByDescending(p => p.parent_category_id).ToList();
           // return productCategory;
       // }

        public bool CheckProductCategoryForDuplicateByName(string product_category_name)
        {
            var checkProductCategoryIsExist = _entities.product_category.FirstOrDefault(p => p.product_category_name == product_category_name);
            bool return_type = checkProductCategoryIsExist == null ? false : true;
            return return_type;
        }

        public int InsertProductCategory(product_category oProductCategory)
        {
            try
            {
                product_category insert_product_category = new product_category
                {
                    product_category_name = oProductCategory.product_category_name,
                    //parent_category_id = oProductCategory.parent_category_id,
                    is_active = oProductCategory.is_active,
                   // product_type_id = oProductCategory.product_type_id,
                    company_id = oProductCategory.company_id
                };
                _entities.product_category.Add(insert_product_category);
                _entities.SaveChanges();
                return insert_product_category.product_category_id;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public bool UpdateProductCategory(product_category oProductCategory)
        {
            try
            {
                product_category ci = _entities.product_category.Find(oProductCategory.product_category_id);
                ci.product_category_name = oProductCategory.product_category_name;
               // ci.parent_category_id = oProductCategory.parent_category_id;
                ci.is_active = oProductCategory.is_active;
               // ci.product_type_id = oProductCategory.product_type_id;
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return true;
            }
        }

        public bool DeleteProductCategory(int product_category_id)
        {
            try
            {
                product_category oProductCategory = _entities.product_category.Find(product_category_id);
                _entities.product_category.Attach(oProductCategory);
                _entities.product_category.Remove(oProductCategory);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public product_category GetProductCategoryById(int product_category_id)
        {
            var productCategory = _entities.product_category.Find(product_category_id);
            return productCategory;
        }


        public List<product_category> GetAllProductCategoryListOnly()
        {
            throw new NotImplementedException();
        }
    }
}