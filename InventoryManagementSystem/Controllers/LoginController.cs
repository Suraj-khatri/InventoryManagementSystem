using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.Interface;
using InventoryManagementSystem.Infrastructure.Repository;
using InventoryManagementSystem.Infrastructure.Services;
using System;
using System.Security.Principal;
using System.Web.Mvc;
using System.Web.Security;


namespace InventoryManagementSystem.Controllers
{    
    public class LoginController : Controller
    {
        private IUnitOfWork _unitOfWork;        
        public LoginController()
        {
            _unitOfWork = new UnitOfWork();            
        }
        public LoginController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: Login
        public ActionResult Login(string returnUrl)
        {
            EnsureLogOut();
            LoginVM record = new LoginVM();
            record.ReturnURL = returnUrl;
            return View(record);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginVM record)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    record.Password = HashService.Hash(record.Password);
                    var user = LoginService.ValidateCredentials(record.Username, record.Password);
                    if (user != null)
                    {
                        if (user.IsTemporary == true)
                            return RedirectToAction("ChangePassword", new { userId = user.AdminID });
                        SignInRemember(record.Username, record.IsRemember); //not working properly yet !!                        
                        Session["UserId"] = user.UserName;
                        Session["AuthId"] = user.AdminID;
                        Session["BranchId"] = CodeService.GetBranchIdFromEmployee(user.Name);
                        Session["DepartmentId"] = CodeService.GetDepartmentIdFromEmployee(user.Name);
                        Session["BranchName"] = CodeService.GetBranchNameFromEmployee(user.Name);
                        Session["DepartmentName"] = CodeService.GetDepartmentNameFromEmployee(user.Name);
                        Session["FullName"] = CodeService.GetEmployeeFullName(user.Name);
                        Session["AssignRole"] = CodeService.GetEmployeeAssignRole(user.AdminID);
                        Session["RoleID"] = CodeService.GetEmployeeRoleId(user.AdminID);
                        TempData["Success"] = "<p>Currently Logged In as " + user.UserName + "</p>";
                        TempData["Title"] = "<strong>Login Successful</strong> <br />";
                        TempData["Icon"] = "fa fa-unlock fa-2x";

                        return RedirectToLocal(record.ReturnURL);

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "username/password is invalid");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
               // ModelState.AddModelError(string.Empty, "Error : Cannot Login at the moment. Please try again later");
            }
            return View(record);
        }
        public ActionResult ChangePassword(int? userId)
        {
            if(userId != null) { 
            ChangePasswordVM record = new ChangePasswordVM();
            record.AuthId = userId;
            return View(record);
            }                        
                return RedirectToAction("Login");            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordVM data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = _unitOfWork.Admin.GetById((int) data.AuthId);
                    string newPwd = HashService.Hash(data.ConfirmNewPassword);
                    if (user.UserPassword != newPwd)
                    {
                        user.UserPassword = newPwd;
                        user.IsTemporary = false;
                        _unitOfWork.Admin.Update(user);
                        _unitOfWork.Save();
                        SignInRemember(user.UserName);
                        Session["UserId"] = user.UserName;
                        Session["AuthId"] = user.AdminID;
                        Session["BranchId"] = CodeService.GetBranchIdFromEmployee(user.Name);
                        Session["DepartmentId"] = CodeService.GetBranchIdFromEmployee(user.Name);
                        Session["BranchName"] = CodeService.GetBranchNameFromEmployee(user.Name);
                        Session["DepartmentName"] = CodeService.GetDepartmentNameFromEmployee(user.Name);
                        Session["FullName"] = CodeService.GetEmployeeFullName(user.Name);
                        TempData["Success"] = "<p>Currently Logged In as " + user.UserName + "</p>";
                        TempData["Title"] = "<strong>Login Successful</strong> <br />";
                        TempData["Icon"] = "fa fa-unlock fa-2x";
                        return RedirectToLocal("/Login/Login");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Your new password cannot be the same as the old password");
                    }
                }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            }catch(Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                ModelState.AddModelError(string.Empty, "Error : Cannot change password at the moment. Please try again later");
            }
            return View(data);
        }
        public ActionResult ForgotPassword()
        {
            ForgotPasswordVM record = new ForgotPasswordVM();
            return View(record);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ForgotPasswordVM data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _unitOfWork.Employee.GetByEmail(data.Email);
                    AdminVM activeUser = null;
                    if(result != null) 
                        activeUser = _unitOfWork.Admin.GetUser(result.OFFICIAL_EMAIL);                    
                    if (result != null && (activeUser !=null /*&& activeUser.IsActive == true*/))
                    {
                        string newPwd = Membership.GeneratePassword(6, 1);
                        EmailService.SendMail(data.Email, "Password Reset",
                              EmailService.EmailMessage.ForgotPassword(result.FIRST_NAME + " " + result.LAST_NAME, newPwd));
                        AdminVM record = _unitOfWork.Admin
                            .GetByEmployeeSetupId(result.EMPLOYEE_ID);
                        record.UserPassword = HashService.Hash(newPwd);
                        record.IsTemporary = true;
                        _unitOfWork.Admin.Update(record);
                        _unitOfWork.Save();
                        //EmailService.SendMail("sillyvizard@gmail.com", "Created dummy account"
                        //    , EmailService.EmailMessage.AccountCreated("Bishesh", "bishesh", "abc"));
                        TempData["Success"] = "<p>Email sent to " + result.OFFICIAL_EMAIL + "</p>";
                        TempData["Title"] = "<strong>Email Sent</strong> <br />";
                        TempData["Icon"] = "fa fa-mail-forward fa-2x";
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "This Email Address is not registered as user.");
                    }
                }
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                ModelState.AddModelError(string.Empty, "Error : The request cannot be processed at the moment.");
            }
            return View(data);
            }        
        public ActionResult LogOut()
        {
            try
            {
                FormsAuthentication.SignOut();
                HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);
                Session.Clear();
                System.Web.HttpContext.Current.Session.RemoveAll();
                return RedirectToLocal("/Login/Login");
            }
            catch
            {
                throw;
            }
        }
//        Does not work yet.. but set auth cookie is required!!
        private void SignInRemember(string username, bool isPersistent = true)
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.SetAuthCookie(username, isPersistent);
        }

        private ActionResult RedirectToLocal(string returnURL = "")
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(returnURL) && Url.IsLocalUrl(returnURL) && returnURL != "/")
                    return Redirect(returnURL);

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                throw;
            }
        }
        private void EnsureLogOut()
        {
            if (Request.IsAuthenticated)
                LogOut();
        }
        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();            
            base.Dispose(disposing);
        }

        public JsonResult ResetPassword(string username)
        {
            try
            {
                var result = _unitOfWork.Admin.GetUser(username);
                if (result != null /*&& result.IsActive==true*/)
                {
                    string newPwd = Membership.GeneratePassword(6, 1);
                    EmailService.SendMail(result.Employees.OFFICIAL_EMAIL, "Password Reset",
                            EmailService.EmailMessage.ForgotPassword(result.Employees.FIRST_NAME + " " + result.Employees.LAST_NAME, newPwd));
                    result.UserPassword = HashService.Hash(newPwd);
                  //  result.IsTemporary = true;
                    _unitOfWork.Admin.Update(result);
                    _unitOfWork.Save();
                    //EmailService.SendMail("sillyvizard@gmail.com", "Created dummy account"
                    //    , EmailService.EmailMessage.AccountCreated("Bishesh", "bishesh", "abc"));
                    return Json(new { status = "Success", reason = "New Password has been sent to your Email. /n Please change your password immediately." });
                }
                else
                {
                    return Json(new { status = "Fail", reason = "No such Email or Username exists!" });
                }
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                return Json(new { status = "Fail", reason = "Some Unknown error ocurred!" });
            }
        }
    }
}