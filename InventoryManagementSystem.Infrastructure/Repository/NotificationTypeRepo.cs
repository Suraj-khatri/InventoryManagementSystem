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
    public class NotificationTypeRepo : INotificationTypeRepo
    {
        private Entities _context;

        public NotificationTypeRepo()
        {
            _context = new Entities();
        }
        public NotificationTypeRepo(Entities context)
        {
            _context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public List<NotificationTypeVM> GetAll()
        {
            return Mapper.Convert(_context.NotificationType.ToList());
        }

        public NotificationTypeVM GetById(int id)
        {
            return Mapper.Convert(_context.NotificationType.Find(id));
        }

        public void Insert(NotificationTypeVM data)
        {
            _context.NotificationType.Add(Mapper.Convert(data));
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(NotificationTypeVM data)
        {
            NotificationType record = Mapper.Convert(data);
            NotificationType existing = _context.NotificationType.Find(data.Id);
            if (existing != null)
                _context.Entry(existing).State = EntityState.Detached;
            _context.NotificationType.Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }
    }
}
