using InventoryManagementSystem.Infrastructure.DapperRepo;
using InventoryManagementSystem.Infrastructure.Filter;
using InventoryManagementSystem.Infrastructure.Interface;
using InventoryManagementSystem.Infrastructure.Repository;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers.InvPurchase
{
 
    public class PaidBillsController : Controller
    {
        // GET: PaidBills
        private IUnitOfWork _unitofWork;
        private DapperRepoServices _dapperrepo;

        public PaidBillsController()
        {
            _unitofWork = new UnitOfWork();
            _dapperrepo = new DapperRepoServices();
        }
        [UserAuthorize(menuId: 24)]
        public ActionResult Index()
        {
            var list = _dapperrepo.GetAllPaidBill();
            return View(list);
        }
    }
}