using InventoryManagementSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Interface
{
   public interface IInNotificationsRepo:IDisposable
    {
        List<InNotificationsVM> GetAll();
        InNotificationsVM GetById(int id);
        InNotificationsVM GetByPOMId(int id);
        List<InNotificationsVM> GetByEmpId(int eid);
        void Insert(InNotificationsVM data);
        void Update(InNotificationsVM data);
        void Save();
    }
}
