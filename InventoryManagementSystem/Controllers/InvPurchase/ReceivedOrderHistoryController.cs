using InventoryManagementSystem.Infrastructure.DapperRepo;
using InventoryManagementSystem.Infrastructure.Filter;
using InventoryManagementSystem.Infrastructure.Interface;
using InventoryManagementSystem.Infrastructure.Repository;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers.InvPurchase
{
   
    public class ReceivedOrderHistoryController : Controller
    {
        // GET: ReceivedOrderHistory
        private IUnitOfWork _unitofWork;
        private DapperRepoServices _dapperrepo;
        public ReceivedOrderHistoryController()
        {
            _unitofWork = new UnitOfWork();
            _dapperrepo = new DapperRepoServices();
        }

        [UserAuthorize(menuId: 22)]
        public ActionResult Index()
        {
            var list = _dapperrepo.GetAllReceiveOrderHistory();
            return View(list);
        }
    }
}