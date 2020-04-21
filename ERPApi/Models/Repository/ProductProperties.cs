using ERPApi.Models.IRepository;
using ERPApi.Models.StronglyType;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ERPApi.Models.Repository
{
    public class ProductProperties : IProductProperties
    {
        private ERPEntities _entities;

        public ProductProperties()
        {
            this._entities = new ERPEntities();
        }

        public List<ProductPropertiesModel> GetSingleProductPropertiesId(int product_properties_id)
        {
            List<ProductPropertiesModel> singleProductProperties = (from p in _entities.product_properties
                                                                    where p.product_properties_id == product_properties_id
                                                                    join dt in _entities.data_type on p.data_type_id equals dt.data_type_id
                                                                    join pt in _entities.property_type on p.property_type_id equals pt.property_type_id
                                                                    select new ProductPropertiesModel
                                                                {
                                                                    product_properties_id = p.product_properties_id,
                                                                    properties_name = p.properties_name,
                                                                    property_type_id = pt.property_type_id,
                                                                    property_type_name = pt.property_type_title,
                                                                    data_type_id = dt.data_type_id,
                                                                    data_type_name = dt.data_type_title,
                                                                    validation = p.validation
                                                                }).ToList();
            return singleProductProperties;
        }

        public List<ProductPropertiesModel> GetComboProductProperties(int product_category_id, int property_type_id)
        {
            List<ProductPropertiesModel> singleProductProperties = (from pcpm in _entities.product_category_properties_mapping
                                                                    join pp in _entities.product_properties on pcpm.product_properties_id equals pp.product_properties_id
                                                                    join pt in _entities.property_type on pp.property_type_id equals pt.property_type_id
                                                                    join dt in _entities.data_type on pp.data_type_id equals dt.data_type_id
                                                                    where pcpm.product_category_id == product_category_id && pp.property_type_id == property_type_id
                                                                    select new ProductPropertiesModel
                                                                    {
                                                                        product_properties_id = pp.product_properties_id,
                                                                        properties_name = pp.properties_name,
                                                                        property_type_id = pp.property_type_id,
                                                                        property_type_name = pt.property_type_title,
                                                                        data_type_id = pp.data_type_id,
                                                                        data_type_name = dt.data_type_title,
                                                                        validation = pp.validation
                                                                    }).ToList();
            return singleProductProperties;
        }

        public object GetAllProductProperties()
        {
            var product_properties = (from p in _entities.product_properties
                                      join d in _entities.data_type
                                      on p.data_type_id equals d.data_type_id
                                      join pt in _entities.property_type
                                      on p.property_type_id equals pt.property_type_id
                                      select new
                                      {
                                          product_properties_id = p.product_properties_id,
                                          properties_name = p.properties_name,
                                          property_type_id = pt.property_type_id,
                                          property_type_name = pt.property_type_title,
                                          data_type_id = d.data_type_id,
                                          data_type_name = d.data_type_title,
                                          validation = p.validation,
                                      }).OrderByDescending(p => p.product_properties_id).ToList();

            return product_properties;
        }

        public bool CheckProductPropertiesForDuplicateByName(string product_properties_name)
        {
            var checkProductPropertiesIsExist = _entities.product_properties.FirstOrDefault(att => att.properties_name == product_properties_name);
            bool return_type = checkProductPropertiesIsExist == null ? false : true;
            return return_type;
        }

        public bool InsertProductProperties(product_properties oProductProperties)
        {
            try
            {
                product_properties insert_product_properties = new product_properties
                {
                    properties_name = oProductProperties.properties_name,
                    property_type_id = oProductProperties.property_type_id,
                    data_type_id = oProductProperties.data_type_id,
                    validation = oProductProperties.validation,
                    created_by = oProductProperties.created_by,
                    created_date = oProductProperties.created_date,
                    updated_by = oProductProperties.updated_by,
                    updated_date = oProductProperties.updated_date,
                    company_id = oProductProperties.company_id
                };
                _entities.product_properties.Add(insert_product_properties);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateProductProperties(product_properties oProductProperties)
        {
            try
            {
                product_properties ci = _entities.product_properties.Find(oProductProperties.product_properties_id);
                ci.properties_name = oProductProperties.properties_name;
                ci.property_type_id = oProductProperties.property_type_id;
                ci.data_type_id = oProductProperties.data_type_id;
                ci.validation = oProductProperties.validation;
                ci.updated_by = oProductProperties.updated_by;
                ci.updated_date = oProductProperties.updated_date;
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return true;
            }
        }

        public bool DeleteProductProperties(int product_properties_id)
        {
            try
            {
                product_properties oProductProperties = _entities.product_properties.FirstOrDefault(att => att.product_properties_id == product_properties_id);
                _entities.product_properties.Attach(oProductProperties);
                _entities.product_properties.Remove(oProductProperties);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}