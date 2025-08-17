using InventoryManagementSystem.Data;
using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.Repository;

namespace InventoryManagementSystem.Infrastructure.Services
{
    public class LoginService
    {
        public static AdminVM ValidateCredentials(string uname, string pwd)
        {
            using (var userAuthentication = new AdminRepo(new Entities()))
            {
                var record = userAuthentication.GetUserForIMS(uname);                                
                    if (record != null && (record.UserPassword == pwd))
                        return record;                
                return null;
            }
        }
    }
}
