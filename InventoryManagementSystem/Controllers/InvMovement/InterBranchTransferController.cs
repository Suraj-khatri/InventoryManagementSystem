using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.DapperRepo;
using InventoryManagementSystem.Infrastructure.Filter;
using InventoryManagementSystem.Infrastructure.Interface;
using InventoryManagementSystem.Infrastructure.Repository;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using InventoryManagementSystem.Data.Models;
using InventoryManagementSystem.Infrastructure.Services;
using System.Threading.Tasks;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using System.Security.Cryptography;
using Microsoft.Ajax.Utilities;

namespace InventoryManagementSystem.Controllers.InvMovement
{
    [UserAuthorize]
    public class InterBranchTransferController : Controller
    {
        private IUnitOfWork _unitofWork;
        private readonly IDropDownList _dropDownList;
        private readonly DapperRepoServices _dapperRepoServices;
        private bool hasAccess;
        private List<InterBranchTransferACKVM> List;

        public InterBranchTransferController()
        {
            _unitofWork = new UnitOfWork();
            _dropDownList = new DropDownList();
            _dapperRepoServices = new DapperRepoServices();
            hasAccess = false;
            List = new List<InterBranchTransferACKVM>();
        }
        public ActionResult Index()
        {

            List.Clear();
            var userid = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
            var branchid = Convert.ToInt32((Session["BranchId"]));
            var assignedrole = Session["AssignRole"].ToString();

            var list = assignedrole == "Admin_User" || assignedrole.Trim() == "Administrator"
                        ? _dapperRepoServices.GetAllInterBranchTransfer(0, 0)
                        : _dapperRepoServices.GetAllInterBranchTransfer(branchid, userid);
            if (list.Any())
            {
                hasAccess = true;
                List = list;
            }
            return View(list);
        }

        public ActionResult ViewTransferDetails(int id)
        {
            if (hasAccess)
            {
                var record = _dapperRepoServices.GetAllInterBranchTransferDetails(id);
                ViewBag.Narration = record[0].Narration;
                ViewBag.Tid = id;
                return View(record);
            }
            else
            {
                var userid = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
                var branchid = Convert.ToInt32((Session["BranchId"]));
                var assignedrole = Session["AssignRole"].ToString();
                var List = assignedrole == "Admin_User" || assignedrole.Trim() == "Administrator"
                        ? _dapperRepoServices.GetAllInterBranchTransfer(0, 0)
                        : _dapperRepoServices.GetAllInterBranchTransfer(branchid, userid);
                if (List.Any() && List.Any(x => x.Id == id))
                {
                    var record = _dapperRepoServices.GetAllInterBranchTransferDetails(id);
                    ViewBag.Narration = record[0].Narration;
                    ViewBag.Tid = id;
                    List.Clear();
                    return View(record);
                }
            }



            return RedirectToAction("Index", "Error");

        }

        [UserAuthorize(menuId: 1110)]
        public ActionResult Create()
        {
            var model = new InterBranchTransferVM
            {
                BranchList = _dropDownList.BranchList().Where(x => x.Value != "0").ToList(),
                ProductGroupList = _dropDownList.ProductGroupList().ToList(),
                ProductList = _dropDownList.ProductList(),
                EmployeeList = _dropDownList.EmployeeList(),
                //DepartmentList = _dropDownList.DepartmentList()
            };
            model.ProductList.Insert(0, new SelectListItem { Text = "All", Value = "0" });
            model.BranchList.Insert(0, new SelectListItem { Text = "--SELECT--" });
            model.ProductGroupList.Insert(0, new SelectListItem { Text = "--SELECT--" });
            model.SenderFrom = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
            model.FromBranchId = Convert.ToInt32(Session["BranchId"]);
            model.FromDeptId = Convert.ToInt32(Session["DepartmentId"]);
            model.DepartmentList = _dropDownList.DepartmentFromBranchList(model.FromBranchId).ToList();


            return View(model);
        }
        [UserAuthorize(menuId: 1110)]
        [HttpPost]
        public ActionResult Create(string MvJson)
        {
            return ProcessInterBranchTransfer(MvJson, "Transfer successful");
        }

        [UserAuthorize(menuId: 1111)]
        [HttpPost]
        public ActionResult Acknowledge(string MvJson)
        {
            return ProcessInterBranchTransfer(MvJson, "Acknowledgement successful");
        }

        private ActionResult ProcessInterBranchTransfer(string MvJson, string successMessage)
        {
            try
            {
                var response = _dapperRepoServices.InterBranchTransfer(MvJson);
                if (response > 0)
                {
                    return Json(new { status = "success", message = successMessage, data = response });
                }

                return Json(new { status = "error", message = "Something went wrong" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");

                return Json(new { status = "error", message = "An unexpected error occurred", details = ex.Message });
            }
        }


        public JsonResult CheckSerial(string model)
        {
            try
            {
                var response = _dapperRepoServices.CheckSerialNumber(model);

                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");

                return Json(new
                {

                    details = ex.Message
                });
            }
        }

        [HttpPost]
        public void SendNotification(int Forwardedby, int Forwardedto, int Tid)
        {
            var notification = new InNotificationsVM();
            notification.CreatedDate = DateTime.Now;
            notification.Subject = "Inter Branch Transfer Approve Request";
            notification.Forwardedby = Forwardedby;
            notification.Forwardedto = Forwardedto;
            notification.URL = "/InterBranchTransfer/ViewTransferDetails" + "/" + Tid;
            notification.SpecialId = Tid;
            notification.Status = false;
            _unitofWork.InNotifications.Insert(notification);
            _unitofWork.Save();


        }


        public JsonResult GetDepartmentForBranchList(int branchid)
        {
            var data = _dropDownList.DepartmentFromBranchList(branchid).ToList();
            return Json(data);
        }

        public JsonResult GetEmployeeForBranchList(int branchid)
        {
            var data = _dropDownList.EmployeeListBranchWise(branchid);
            return Json(data);
        }
        public JsonResult GetEmployeeNameForBranchAndDepartmentList(int branchid, int deptid)
        {
            var data = _dropDownList.GetEmployeeNameForBranchAndDepartmentList(branchid, deptid).ToList();
            return Json(data);
        }

        public JsonResult GetProductGroupName()
        {
            var data = _dropDownList.ProductGroupList().ToList();
            data.Insert(0, new SelectListItem { Text = "--Select--", Value = "--Select--" });
            return Json(data);
        }
        public JsonResult GetProductNameFromGroupName(int groupid)
        {
            var data = _dropDownList.ProductWithProductGroupList(groupid).ToList();
            return Json(data);
        }
        public JsonResult GetStockData(int branchid, int groupid, int productid)
        {
            var data = _dapperRepoServices.GetAllProductStock(branchid, groupid, productid);
            return Json(data);
        }

        [HttpPost]
        public async Task<JsonResult> SendEmail(int Forwardedby, int Forwardedto, int Tid, int fromBranch)
        {


            try
            {
                var email = CodeService.GetOfficialEmail(Forwardedto);
                var fromuser = CodeService.GetEmployeeFullName(Convert.ToInt32(_unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name));
                var touser = CodeService.GetEmployeeFirstName(Forwardedto);
                var data = _dapperRepoServices.GetAllInterBranchTransferDetails(Tid);
                var msg = string.Empty;
                var tqty = 0;
                int sn = 1;
                var frombranch = CodeService.GetBranchName(fromBranch);

                // Define the table header
                string tabledata = @"<thead>
                        <tr>
                            <th style='border:1px solid #000; padding:10px'>SN</th>
                            <th style='border:1px solid #000; padding:10px'>Product Name</th>
                            <th style='border:1px solid #000; padding:10px'>UNIT</th>
                            <th style='border:1px solid #000; padding:10px; text-align:center;'>Transferred Qty</th>
                        </tr>
                     </thead>
                     <tbody>";

                // Iterate through the data and build table rows
                foreach (var item in data.OrderBy(x => x.ProductName))
                {
                    tqty += item.Qty;
                    msg = item.Narration;
                    tabledata += $@"<tr>
                        <td style='border:1px solid #000; padding:10px'>{sn}</td>
                        <td style='border:1px solid #000; padding:10px'>{item.ProductName}</td>
                        <td style='border:1px solid #000; padding:10px'>{item.Unit}</td>
                        <td style='border:1px solid #000; padding:10px; text-align:center;'>{item.Qty}</td>
                    </tr>";
                    sn++;
                }

                // Add the table footer
                string HtmlMail = $@"<html>
                        <table border='1' cellspacing='0' cellpadding='0' style='border-collapse:collapse; width:100%;'>
                            {tabledata}
                        </tbody>
                        <tfoot>
                            <tr>
                                <td style='border:1px solid #000; padding:10px'></td>
                                <td style='border:1px solid #000; padding:10px; text-align:center; font-weight: bold;'>Total:</td>
                                <td style='border:1px solid #000; padding:10px'></td>
                                <td style='border:1px solid #000; padding:10px; text-align:center; font-weight: bold;'>{tqty}</td>
                            </tr>
                        </tfoot>
                        </table>
                     </html>";

                if (email != "")
                {
                    try
                    {
                        await EmailService.SendMailAsync(email, "Inter Branch Transfer Acknowledge Request",
                        EmailService.EmailMessage.ApprovalRequestOfProductDetail(touser, fromuser, HtmlMail, msg, frombranch));
                    }
                    catch (Exception)
                    {
                        //wrong email
                    }
                }

                return Json(new { status = "success" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");

                return Json(new
                {

                    status = "error"
                });
            }


        }


    }
}