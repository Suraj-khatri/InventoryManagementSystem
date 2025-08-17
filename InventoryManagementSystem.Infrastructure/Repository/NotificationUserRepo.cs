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
    public class NotificationUserRepo : INotificationUserRepo
    {
        private Entities _context;

        public NotificationUserRepo()
        {
            _context = new Entities();
        }
        public NotificationUserRepo(Entities context)
        {
            _context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public List<NotificationUserVM> GetAll()
        {
            return Mapper.Convert(_context.NotificationUser.ToList());
        }

        public NotificationUserVM GetById(int id)
        {
            return Mapper.Convert(_context.NotificationUser.Find(id));
        }

        public void Insert(NotificationUserVM data)
        {
            _context.NotificationUser.Add(Mapper.Convert(data));
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(NotificationUserVM data)
        {
            NotificationUser record = Mapper.Convert(data);
            NotificationUser existing = _context.NotificationUser.Find(data.Id);
            if (existing != null)
                _context.Entry(existing).State = EntityState.Detached;
            _context.NotificationUser.Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }
    }
}
