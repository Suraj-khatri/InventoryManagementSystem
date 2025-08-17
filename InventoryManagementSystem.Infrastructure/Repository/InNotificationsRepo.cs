using InventoryManagementSystem.Data;
using InventoryManagementSystem.Data.Models;
using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Repository
{
    public class InNotificationsRepo : IInNotificationsRepo
    {
        private Entities _context;

        public InNotificationsRepo()
        {
            _context = new Entities();
        }
        public InNotificationsRepo(Entities context)
        {
            _context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public List<InNotificationsVM> GetAll()
        {
            return Mapper.Convert(_context.IN_Notifications.ToList());
        }
        public List<InNotificationsVM> GetByEmpId(int eid)
        {
            var notifications = _context.IN_Notifications
                .Where(x => x.Forwardedto == eid && x.Status == false)
                .ToList();

            return notifications.Any()
                ? Mapper.Convert(notifications)
                : Enumerable.Empty<InNotificationsVM>().ToList();
        }
        public InNotificationsVM GetById(int id)
        {
            return Mapper.Convert(_context.IN_Notifications.Find(id));
        }
        public InNotificationsVM GetByPOMId(int pomid)
        {
            return Mapper.Convert(_context.IN_Notifications.Where(x=>x.SpecialId==pomid && x.Status==false).FirstOrDefault());
        }
        public void Insert(InNotificationsVM data)
        {
            _context.IN_Notifications.Add(Mapper.Convert(data));
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public void Update(InNotificationsVM data)
        {
            IN_Notifications record = Mapper.Convert(data);
            IN_Notifications existing = _context.IN_Notifications.Find(data.Id);
            if (existing != null)
                _context.Entry(existing).State = EntityState.Detached;
            _context.IN_Notifications.Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }
    }
}
