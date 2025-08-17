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
    public class NotificationRepo : INotificationRepo
    {
        private Entities _context;

        public NotificationRepo()
        {
            _context = new Entities();
        }
        public NotificationRepo(Entities context)
        {
            _context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public List<NotificationVM> GetAll()
        {
            return Mapper.Convert(_context.Notification.ToList());
        }

        public NotificationVM GetById(int id)
        {
            return Mapper.Convert(_context.Notification.Find(id));
        }

        public void Insert(NotificationVM data)
        {
            _context.Notification.Add(Mapper.Convert(data));
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(NotificationVM data)
        {
            Notification record = Mapper.Convert(data);
            Notification existing = _context.Notification.Find(data.Id);
            if (existing != null)
                _context.Entry(existing).State = EntityState.Detached;
            _context.Notification.Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }
    }
}
