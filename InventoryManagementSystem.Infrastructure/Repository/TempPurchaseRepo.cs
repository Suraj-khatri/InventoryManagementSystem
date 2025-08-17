using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using InventoryManagementSystem.Data;
using InventoryManagementSystem.Data.Models;
using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.Interface;

namespace InventoryManagementSystem.Infrastructure.Repository
{
    public class TempPurchaseRepo : ITempPurchaseRepo
    {
        private Entities _context;

        public TempPurchaseRepo()
        {
            _context = new Entities();
        }
        public TempPurchaseRepo(Entities context)
        {
            _context = context;
        }

        public void Delete(int id)
        {
            try
            {
                var data = _context.Temp_Purchase.Where(x => x.id == id).FirstOrDefault();
                if (data!=null)
                {
               _context.Temp_Purchase.Remove(data);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public List<TempPurchaseVM> GetAll()
        {
            return Mapper.Convert(_context.Temp_Purchase.ToList());
        }

        public TempPurchaseVM GetById(int id)
        {
            return Mapper.Convert(_context.Temp_Purchase.Find(id));
        }

        public void Insert(TempPurchaseVM data)
        {
            _context.Temp_Purchase.Add(Mapper.Convert(data));
        }
        public void Update(TempPurchaseVM data)
        {
            Temp_Purchase record = Mapper.Convert(data);
            Temp_Purchase existing = _context.Temp_Purchase.Find(data.id);
            if (existing != null)
                _context.Entry(existing).State = EntityState.Detached;
            _context.Temp_Purchase.Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
