using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.DapperRepo;
using InventoryManagementSystem.Infrastructure.Filter;
using InventoryManagementSystem.Infrastructure.Interface;
using InventoryManagementSystem.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers.InvMovement
{
    [UserAuthorize]
    public class BranchWiseHistoryController : Controller
    {
        // GET: BranchWiseHistory
        private IUnitOfWork _unitofWork;
        private IDropDownList _dropDownList;
        private DapperRepoServices _dapperrepo;
        public BranchWiseHistoryController()
        {
            _unitofWork = new UnitOfWork();
            _dropDownList = new DropDownList();
            _dapperrepo = new DapperRepoServices();
        }

        [AllowAnonymous]
        [UserAuthorize(menuId:48)]
        public ActionResult Index()
        {
            var assignedRole = Session["AssignRole"].ToString();
            var branchId = Convert.ToInt32(Session["BranchId"]);
            var record = new InRequisitionMessageVM();

            if (assignedRole == "StoreKeeper(HeadOffice)" || assignedRole == "Admin_User" || assignedRole.Trim() == "Administrator")
            {
                record.BranchNameList = _dropDownList.BranchList();  
                record.InRequisitionMessageVMList = _dapperrepo.GetAllBranchWiseHistory(0);
            }
            else
            {
                record.BranchNameList = _dropDownList.BranchList(branchId);  
                record.branch_id = branchId; 
                record.InRequisitionMessageVMList = _dapperrepo.GetAllBranchWiseHistory(branchId); 
            }

            return View(record);
        }

        [HttpPost]
        public ActionResult Index(InRequisitionMessageVM data)
        {
           
            var assignedRole = Session["AssignRole"].ToString();
            var branchId = Convert.ToInt32(Session["BranchId"]);

            if (assignedRole == "StoreKeeper(HeadOffice)" || assignedRole == "Admin_User" || assignedRole.Trim() == "Administrator")
            {
                data.BranchNameList = _dropDownList.BranchList();  
                data.InRequisitionMessageVMList = _dapperrepo.GetAllBranchWiseHistory(data.branch_id); 
            }
            else
            {
                data.BranchNameList = _dropDownList.BranchList(branchId); 
                data.branch_id = branchId; 
                data.InRequisitionMessageVMList = _dapperrepo.GetAllBranchWiseHistory(branchId); 
            }

            return View(data);
        }

        public ActionResult ViewRequestedDetails(int id)
        {
            var record = _unitofWork.InRequisitionMessage.GetById(id);
            record.inreqList = _unitofWork.InRequisition.GetItemById(id);
            return View(record);
        }

        public ActionResult ViewRecommendDetails(int id)
        {
            var record = _unitofWork.InRequisitionMessage.GetById(id);
            record.inreqList = _unitofWork.InRequisition.GetItemById(id);
            return View(record);
        }
        public ActionResult ViewApprovedDetails(int id)
        {
            var record = _unitofWork.InRequisitionMessage.GetById(id);
            record.inreqList = _unitofWork.InRequisition.GetItemById(id);
            return View(record);
        }

        public ActionResult ViewDispatchedDetails(int id)
        {
            var record = _unitofWork.InRequisitionMessage.GetById(id);
            record.indispList = _unitofWork.InDispatch.GetAllByDispMesId(record.dispatchedid);
            return View(record);
        }
        public ActionResult ViewAcknowledgedDetails(int id)
        {
            var record = _unitofWork.InRequisitionMessage.GetById(id);
            record.indispList = _unitofWork.InDispatch.GetAllByDispMesId(record.dispatchedid);
            return View(record);
        }
        public ActionResult PrintApprovedDetails(int id)
        {
            var record = _unitofWork.InRequisitionMessage.GetById(id);
            record.inreqList = _unitofWork.InRequisition.GetItemById(id);
            return Json(record, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PrintDispatchedDetails(int id)
        {
            var record = _unitofWork.InRequisitionMessage.GetById(id);
            record.indispList = _unitofWork.InDispatch.GetAllByDispMesId(record.dispatchedid);
            return Json(record, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PODetails(int id)
        {
            var record = _unitofWork.InRequisitionMessage.GetById(id);
            record.VendorList = _dropDownList.VendorList();
            record.VendorList.Insert(0, new SelectListItem { Text = "--Select--", Value = "0" });
            record.EmployeeList = _dropDownList.EmployeeList();
            record.OrderDate = DateTime.Now.Date;
            record.LastDateOfDelivery = DateTime.Now.Date.AddDays(7);
            record.PONotes = "Please state P.O. No. in your delivery challan and invoice.~" + "\r" +
                  "The Bank reserves the right to reject the items delivered beyond the delivery period mentioned above.~" + "\r" +
                  "The Bank reserves the right to reject the items delivered that do not match the sample provided with Bid or " +
                  "do not match the quality standard.";
            record.inreqList = _unitofWork.InRequisition.GetItemById(id);
            return View(record);
        }
    }
}