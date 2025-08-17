using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Domain.RoleSetupVM;
using InventoryManagementSystem.Infrastructure.DapperRepo;
using InventoryManagementSystem.Infrastructure.Interface;
using InventoryManagementSystem.Infrastructure.Repository;
using InventoryManagementSystem.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers
{
    public class SharedController : Controller
    {
        // GET: Shared
        private IUserRoleRepo _userRole;
        private IUserFunctionRepo _userfunction;
        private IRoleDetailsRepo _roledetail;
        private DapperRepoServices _dapperrepo;
        public SharedController()
        {
            _userRole = new UserRoleRepo();
            _userfunction = new UserFunctionRepo();
            _roledetail = new RoleDetailsRepo();
            _dapperrepo = new DapperRepoServices();
        }
        // GET: Shared
        [ChildActionOnly]
        public ActionResult _SideMenu()
        {
            var userid = Convert.ToInt32(Session["AuthId"]);
            var roleid = _dapperrepo.GetRoleId(userid);
            var privilage = _roledetail.GetAll().Where(x=>x.role_id==roleid).ToList();
            List<int> function_ids = new List<int>();
            privilage.ForEach(x => function_ids.Add(x.function_id));
            var menuItems = new SideMenuVM();
            menuItems.RoleDetails = privilage;
            menuItems.UserFunction = _userfunction.GetAll().Where(x=> function_ids.Contains(x.sno)).ToList();
            return PartialView("_SideMenu", menuItems);
        }

        [HttpPost]
        public JsonResult UserNotifications()
        {
            List<InNotificationsVM> item = new List<InNotificationsVM>();
            int authId = int.Parse(Session["AuthId"]?.ToString());
            var empid = CodeService.GetEmployeeIdFromAdmin(authId);
            using (var repo = new InNotificationsRepo())
            {
                item = repo.GetByEmpId(empid).ToList();
            }
            return Json(item);
        }
    }
}