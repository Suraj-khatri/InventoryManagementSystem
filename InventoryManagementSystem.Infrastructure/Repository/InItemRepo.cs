using InventoryManagementSystem.Data;
using InventoryManagementSystem.Data.Models;
using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.Interface;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace InventoryManagementSystem.Infrastructure.Repository
{
    public class InItemRepo : IInItemRepo
    {
        private Entities _context;

        public InItemRepo()
        {
            _context = new Entities();
        }
        public InItemRepo(Entities context)
        {
            _context = context;
        }
        public void Delete(int id)
        {
            _context.IN_ITEM.Find(id).Is_Active = false;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public List<In_ItemVM> GetAll()
        {
            return Mapper.Convert(_context.IN_ITEM.Where(x => x.parent_id == 1).ToList());
        }

        public In_ItemVM GetById(int id)
        {
            return Mapper.Convert(_context.IN_ITEM.Find(id));
        }

        public List<In_ItemVM> GetByParentId(int id)
        {
            return Mapper.Convert(_context.IN_ITEM.Where(x => x.parent_id == id).ToList());
        }
        public List<In_ItemVM> GetAllByParentId()
        {
            return Mapper.Convert(_context.IN_ITEM.Where(x => x.parent_id == 1 && x.is_product == false).ToList());
        }
        public List<In_ItemVM> GetInActiveProductByParentId(int id)
        {
            return Mapper.Convert(_context.IN_ITEM.Where(x => x.parent_id == id && x.Is_Active == false).ToList());
        }
        public void Insert(In_ItemVM data)
        {
            _context.IN_ITEM.Add(Mapper.Convert(data));
        }

        public bool IsDescExists(In_ItemVM data)
        {
            if (_context.IN_ITEM.Where(x => x.item_desc == data.item_desc && x.id != data.id).Count() > 0)
                return true;
            return false;
        }

        public bool IsGroupNameExists(In_ItemVM data)
        {
            if (_context.IN_ITEM.Where(x => x.item_name == data.item_name && x.id != data.id).Count() > 0)
                return true;
            return false;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(In_ItemVM data)
        {
            IN_ITEM record = Mapper.Convert(data);
            IN_ITEM existing = _context.IN_ITEM.Find(data.id);
            if (existing != null)
                _context.Entry(existing).State = EntityState.Detached;
            _context.IN_ITEM.Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }
    }
}
