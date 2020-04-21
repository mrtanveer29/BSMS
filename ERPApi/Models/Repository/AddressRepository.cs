using ERPApi.Models;
using ERPApi.Models.IRepository;
using ERPApi.Models.StronglyType;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ERPApi.Models.Repository
{
    public class AddressRepository : IAddressRepository
    {
        private ERPEntities _entities;

        public AddressRepository()
        {
            this._entities = new ERPEntities();
        }

        public List<address> GetAllAddress()
        {
            return _entities.addresses.ToList();
        }

        public List<address> GetAllAddressBySource(int source_id, string source_type)
        {
            var dataList = _entities.addresses.Where(p => p.source_id == source_id && p.source_type == source_type).ToList();

            return dataList;
        }

        public List<address> GetAllAddressBySourceAddressType(int source_id, string source_type, string address_type)
        {
            var dataList = _entities.addresses.Where(p => p.source_id == source_id && p.source_type == source_type && p.address_type == address_type).ToList();

            return dataList;
        }

        public address GetAddressById(int address_id)
        {
            return _entities.addresses.Find(address_id);
        }

        public bool InsertAddress(address oAddress)
        {
            try
            {
                address entity = new address
                {
                    source_id = oAddress.source_id,
                    source_type = oAddress.source_type,
                    address_type = oAddress.address_type,
                    address_1 = oAddress.address_1,
                    address_2 = oAddress.address_2,
                    country_id = oAddress.country_id,
                    city_id = oAddress.city_id,
                    zip_code = oAddress.zip_code,
                    email = oAddress.email,
                    phone = oAddress.phone
                };
                _entities.addresses.Add(entity);
                _entities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateAddress(address oAddress)
        {
            try
            {
                address entity = _entities.addresses.Find(oAddress.address_id);
                entity.address_1 = oAddress.address_1;
                entity.address_2 = oAddress.address_2;
                entity.country_id = oAddress.country_id;
                entity.city_id = oAddress.city_id;
                entity.zip_code = oAddress.zip_code;
                entity.email = oAddress.email;
                entity.phone = oAddress.phone;
                _entities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

     
    }
}