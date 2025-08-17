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
    public class TempDisposeRepo : ITempDisposeRepo
    {
        private Entities _context;

        public TempDisposeRepo()
        {
            _context = new Entities();
        }
        public TempDisposeRepo(Entities context)
        {
            _context = context;
        }

        public void Delete(int id)
        {
            try
            {
                var data = _context.Temp_Dispose.Where(x => x.Id == id).FirstOrDefault();
                if (data != null)
                {
                    _context.Temp_Dispose.Remove(data);
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


        public List<TempDisposeVM> GetAll()
        {
            return Mapper.Convert(_context.Temp_Dispose.ToList());
        }

        public TempDisposeVM GetById(int id)
        {
            return Mapper.Convert(_context.Temp_Dispose.Find(id));
        }

        public void Insert(TempDisposeVM data)
        {
            _context.Temp_Dispose.Add(Mapper.Convert(data));
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(TempDisposeVM data)
        {
            Temp_Dispose record = Mapper.Convert(data);
            Temp_Dispose existing = _context.Temp_Dispose.Find(data.Id);
            if (existing != null)
                _context.Entry(existing).State = EntityState.Detached;
            _context.Temp_Dispose.Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }
    }
}
