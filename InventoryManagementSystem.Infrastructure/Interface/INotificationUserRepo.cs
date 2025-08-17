using InventoryManagementSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Interface
{
   public interface INotificationUserRepo:IDisposable
    {
        List<NotificationUserVM> GetAll();
        NotificationUserVM GetById(int id);
        void Insert(NotificationUserVM data);
        void Update(NotificationUserVM data);
        void Save();
    }
}
