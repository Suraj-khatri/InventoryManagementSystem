using InventoryManagementSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Interface
{
    public interface INotificationTypeRepo: IDisposable
    {
        List<NotificationTypeVM> GetAll();
        NotificationTypeVM GetById(int id);
        void Insert(NotificationTypeVM data);
        void Update(NotificationTypeVM data);
        void Save();
    }
}
