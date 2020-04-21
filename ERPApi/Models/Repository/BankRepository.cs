using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ERPApi.Models.IRepository;

namespace ERPApi.Models.Repository
{
    public class BankRepository : IBankRepository
    {
        private ERPEntities _entities;

        public BankRepository()
        {
            this._entities = new ERPEntities();
        }

        public bank GetBankById(int bankId)
        {
            return _entities.banks.Find(bankId);
        }

        public List<bank> GetBankBySourceType(string sourceType)
        {
            return _entities.banks.Where(p => p.source_type == sourceType).ToList();
        }

        public List<bank> GetBankBySourceTypeAndId(string sourceType, int sourceId)
        {
            return _entities.banks.Where(p => p.source_type == sourceType && p.source_id == sourceId).ToList();
        }
    }
}