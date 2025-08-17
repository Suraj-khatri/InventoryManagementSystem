using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Domain.ViewModel;
using InventoryManagementSystem.Infrastructure.DapperRepo;
using InventoryManagementSystem.Infrastructure.Filter;
using InventoryManagementSystem.Infrastructure.Repository;
using InventoryManagementSystem.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers
{
    [UserAuthorize]
    public class HomeController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        private DashboardDapperRepoServices _dashboarddapperrepo;
        public HomeController()
        {
            _unitOfWork = new UnitOfWork();
            _dashboarddapperrepo = new DashboardDapperRepoServices();
        }
        public async Task<ActionResult> Index()
        {
            DashboardVM data = new DashboardVM();
            data.recentApprovedRequisitionList = await _dashboarddapperrepo.GetRecentApprovedRequisition();
            data.recentDispatchRequisitionList = await _dashboarddapperrepo.GetRecentDispatchRequisition();
            data.recentAcknowledgeRequisitionList = await _dashboarddapperrepo.GetRecentAcknowledgeRequisition();
            data.recentPurchasesList = await _dashboarddapperrepo.GetRecentPurchases();
            data.stockLevelList = await _dashboarddapperrepo.GetLowStockProducts();

            var assignedRole = Session["AssignRole"].ToString();
            var validRoles = new List<string> { "StoreKeeper(HeadOffice)", "Admin_User", "Administrator" };

            if (validRoles.Contains(assignedRole))
            {
                var email = CodeService.GetOfficialEmail(1211);
                var toUser = CodeService.GetEmployeeFullName(1211);

                int sn = 1;
                StringBuilder tableData = new StringBuilder("<thead><tr><th>SN</th> <th>Product Name </th> <th>Unit </th> <th>Stock In Hand </th> <th>Re-Order Level</th> </tr> </thead><tbody>");

                foreach (var item in data.stockLevelList)
                {
                    tableData.Append($@"<tr>
                                <td style='border:1px solid #000; padding:10px'>{sn}</td>
                                <td style='border:1px solid #000; padding:10px'>{item.ProductName}</td>
                                <td style='border:1px solid #000; padding:10px'>{item.Unit}</td>
                                <td style='border:1px solid #000; padding:10px'>{item.stock_in_hand}</td>
                                <td style='border:1px solid #000; padding:10px; text-align:center;'>{item.REORDER_LEVEL}</td>
                                </tr>");
                    sn++;
                }

                string htmlMail = $@"<html><table border='1' cellspacing='0' cellpadding='0'>{tableData}</tbody></table></html>";

                if (!string.IsNullOrEmpty(email))
                {
                    try
                    {
                        await EmailService.SendMailAsync(email, "Low Level Stock", EmailService.EmailMessage.LowStockLevelProductDetails(toUser, htmlMail));
                    }
                    catch (Exception ex)
                    {
                        // Log or handle the exception appropriately
                    }
                }
            }

            return View(data);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult ChangePassword()
        {
            ChangePasswordVM record = new ChangePasswordVM();
            return View(record);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordVM data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = _unitOfWork.Admin.GetUser(Session["UserId"].ToString());
                    var newPwd = HashService.Hash(data.NewPassword);
                    if (user != null)
                    {
                        if (user.UserPassword == HashService.Hash(data.OldPassword))
                        {
                            if (user.UserPassword != newPwd)
                            {
                                user.UserPassword = newPwd;
                                _unitOfWork.Admin.Update(user);
                                _unitOfWork.Save();
                                TempData["Success"] = "<p>Password succesfully changed</p>";
                                TempData["Title"] = "<strong>Password Changed</strong><br />";
                                TempData["Icon"] = "fa fa-lock fa-2x";
                                return RedirectToAction("Login", "Login");
                            }
                            else if (data.OldPassword.Trim() == null)
                            {
                                ModelState.AddModelError("OldPassword", "The Old Password field is required");
                            }
                            else
                            {
                                ModelState.AddModelError("NewPassword", "Cannot be same as the old password.");
                                return View(data);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("OldPassword", "Invalid Current Password");
                        }
                    }
                }
                return View(data);
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                ModelState.AddModelError(string.Empty, "The password cannot be changed at the moment. Please try again later.");
            }
            return View(data);
        }
    }
}