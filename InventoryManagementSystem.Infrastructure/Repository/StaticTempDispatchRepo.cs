using InventoryManagementSystem.Data;
using InventoryManagementSystem.Data.Models;
using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Repository
{
  public  class StaticTempDispatchRepo: IStaticTempDispatchRepo
    {
        private Entities _context;

        public StaticTempDispatchRepo()
        {
            _context = new Entities();
        }
        public StaticTempDispatchRepo(Entities context)
        {
            _context = context;
        }

        public void Delete(int id)
        {
            try
            {
                var data = _context.In_Static_Temp_Dispatch.Where(x => x.Id == id).FirstOrDefault();
                if (data != null)
                {
                    _context.In_Static_Temp_Dispatch.Remove(data);
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

        public List<StaticTempDispatchVM> GetAllNonPrintingItem()
        {
            return Mapper.Convert(_context.In_Static_Temp_Dispatch.Where(x=>x.ProductGroupId==2).ToList());
        }
        public List<StaticTempDispatchVM> GetAllPrintingItem()
        {
            return Mapper.Convert(_context.In_Static_Temp_Dispatch.Where(x => x.ProductGroupId == 3).ToList());
        }
        public StaticTempDispatchVM GetById(int id)
        {
            return Mapper.Convert(_context.In_Static_Temp_Dispatch.Find(id));
        }

        public void Insert(StaticTempDispatchVM data)
        {
            _context.In_Static_Temp_Dispatch.Add(Mapper.Convert(data));
        }
        public void Update(StaticTempDispatchVM data)
        {
            In_Static_Temp_Dispatch record = Mapper.Convert(data);
            In_Static_Temp_Dispatch existing = _context.In_Static_Temp_Dispatch.Find(data.Id);
            if (existing != null)
                _context.Entry(existing).State = EntityState.Detached;
            _context.In_Static_Temp_Dispatch.Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
