using InventoryManagementSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Interface
{
   public interface INotificationRepo:IDisposable
    {
        List<NotificationVM> GetAll();
        NotificationVM GetById(int id);
        void Insert(NotificationVM data);
        void Update(NotificationVM data);
        void Save();
    }
}
