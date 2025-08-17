using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.DapperRepo;
using InventoryManagementSystem.Infrastructure.Interface;
using InventoryManagementSystem.Infrastructure.Repository;
using InventoryManagementSystem.Infrastructure.Services;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using System.Web.Http;
using System.Web.UI.WebControls;

namespace InventoryManagementSystem.Controllers.Api
{
    public class FuelMovementApiController : ApiController
    {
        private IUnitOfWork _unitofWork;
        private DapperRepoServices _dapperrepo;
        public FuelMovementApiController()
        {
            _unitofWork = new UnitOfWork();
            _dapperrepo = new DapperRepoServices();
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Create()
        {
            var Session = HttpContext.Current.Session;
            var branchid = Convert.ToInt32(Session["BranchId"]);
            var httpRequests = HttpContext.Current.Request;

            var inreqmess = httpRequests.Form["inreqmes"];
            FuelRequestsMessageVM inreqmes = JsonConvert.DeserializeObject<FuelRequestsMessageVM>(inreqmess);
            try
            {
                if (ModelState.IsValid)
                {
                    if (inreqmes.Requested_Message != "")
                    {
                        if (inreqmes.Priority == "Normal")
                        {
                            inreqmes.Priority = "N";
                        }
                        else if (inreqmes.Priority == "High")
                        {
                            inreqmes.Priority = "H";
                        }
                        else if (inreqmes.Priority == "Low")
                        {
                            inreqmes.Priority = "L";
                        }
                        if (inreqmes.inreqList.Count >= 1)
                        {
                            using (TransactionScope Scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                            {
                                var compshortname = CodeService.GetCompanyShortName();
                                var fiscalyear = CodeService.GetFiscalYear();
                                var fuelrequestmessageid = CodeService.GetFuelRequisitionMessageId();
                                inreqmes.FuelRequestNo = compshortname + "-" + "FR" + "-" + fiscalyear + "-" + fuelrequestmessageid;
                                inreqmes.Requested_By = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
                                inreqmes.Requested_Date = DateTime.Now.Date;
                                inreqmes.Branch_id = branchid;
                                inreqmes.Status = "Requested";
                                inreqmes.Requested_Message = inreqmes.Requested_Message;
                                _unitofWork.FuelRequestMessage.Create(inreqmes /*inreqmes.FilePath*/);
                                _unitofWork.Save();

                                var inreq = new FuelRequestsVM();
                                var reqmesid = CodeService.GetFuelRequestMessageId();
                                var i = 0;
                                foreach (var itm in inreqmes.inreqList.Where(x => x.Id >= 0))
                                {
                                    inreq.Fuel_Category = itm.Fuel_Category;
                                    inreq.Vehicle_Category = itm.Vehicle_Category;
                                    inreq.Coupon_No = itm.Coupon_No;
                                    inreq.Vendor = itm.Vendor;
                                    inreq.Vehicle_No = itm.Vehicle_No;
                                    inreq.Previous_KM_Run = itm.Previous_KM_Run;
                                    inreq.KM_Run = itm.KM_Run;
                                    inreq.Unit = itm.Unit;
                                    inreq.Fuel_Requests_Message_Id = reqmesid;
                                    inreq.Requested_Quantity = itm.Requested_Quantity;

                                    var httpRequest = HttpContext.Current.Request;
                                    var file = httpRequest.Files[$"file{i}"];

                                    if (branchid != 12)
                                    {
                                        if (file != null && file.ContentLength > 0)
                                        {
                                            try
                                            {
                                                string fileName = Path.GetFileName(file.FileName);
                                                string uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
                                                string uploadsFolder = HttpContext.Current.Server.MapPath("~/Content/FuelUploads");
                                                if (!Directory.Exists(uploadsFolder))
                                                {
                                                    Directory.CreateDirectory(uploadsFolder);
                                                }

                                                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                                                // Save the file
                                                using (var fileStream = System.IO.File.Create(filePath))
                                                {
                                                    file.InputStream.CopyTo(fileStream);

                                                    // Verify if the file was saved successfully
                                                    if (System.IO.File.Exists(filePath))
                                                    {
                                                        inreq.FilePath = filePath;
                                                    }
                                                    else
                                                    {
                                                        // Log error if file was not saved
                                                        throw new Exception("File save failed.");
                                                    }
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                // Log the exception
                                                System.Diagnostics.Debug.WriteLine($"File Upload Error: {ex.Message}");
                                            }
                                        }

                                    }
                                    _unitofWork.FuelRequest.Create(inreq);
                                    _unitofWork.Save();
                                    i++;
                                }

                                var imsn = new InNotificationsVM();
                                imsn.CreatedDate = DateTime.Now;
                                imsn.Subject = "Fuel Requisition Recommendation Request";
                                imsn.Forwardedby = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;// int.Parse(pomh.from_user);
                                imsn.Forwardedto = inreqmes.Recommended_By;
                                imsn.URL = "/FuelRecommend/Index" + "/" + reqmesid;
                                imsn.SpecialId = reqmesid;
                                imsn.Status = false;
                                _unitofWork.InNotifications.Insert(imsn);
                                _unitofWork.Save();

                                var email = CodeService.GetOfficialEmail(inreqmes.Recommended_By);
                                var fromuser = CodeService.GetEmployeeFullName(_unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name);
                                var touser = CodeService.GetEmployeeFirstName(inreqmes.Recommended_By);
                                var reqQty = _unitofWork.FuelRequest.GetItemById(reqmesid);
                                var tdqty = reqQty.Sum(x => x.Requested_Quantity);
                                int sn = 1;

                                string tabledata = "<thead><tr><th>SN</th><th>Fuel Cat</th><th>Vehicle Cat</th><th>Vendor</th><th>Vehicle No.</th><th>Coupon No.</th><th>Prev. KM Run</th><th>Current. KM Run</th><th>Total KM Run</th><th>Unit</th><th>Req. Qty</th></tr></thead><tbody>";
                                foreach (var item in reqQty.OrderBy(x => x.Coupon_No))
                                {
                                    tabledata += @"<tr>
                                  <td style='border:1px solid #000; padding:10px'>" + sn + @"</td>
                                  <td style='border:1px solid #000; padding:10px'>" + item.Fuel_Category + @"</td>
                                  <td style='border:1px solid #000; padding:10px'>" + item.Vehicle_Category + @"</td>
                                  <td style='border:1px solid #000; padding:10px; text-align:center;'>" + item.Vendor + @"</td>
                                  <td style='border:1px solid #000; padding:10px; text-align:center;'>" + item.Vehicle_No + @"</td>
                                  <td style='border:1px solid #000; padding:10px; text-align:center;'>" + item.Coupon_No + @"</td>
                                  <td style='border:1px solid #000; padding:10px; text-align:center;'>" + item.Previous_KM_Run + @"</td>
                                  <td style='border:1px solid #000; padding:10px; text-align:center;'>" + item.KM_Run + @"</td>
                                  <td style='border:1px solid #000; padding:10px; text-align:center;'>" + (Convert.ToInt32(item.KM_Run) - Convert.ToInt32(item.Previous_KM_Run)) + @"</td>
                                  <td style='border:1px solid #000; padding:10px; text-align:center;'>" + item.Unit + @"</td>
                                  <td style='border:1px solid #000; padding:10px; text-align:center;'>" + item.Requested_Quantity + @"</td>
                                  </tr>";
                                    sn++;
                                }
                                string HtmlMail = @"<html><table border='1' cellspacing='0' cellpadding='0' >" + tabledata + "</tbody></table></html>";

                                if (email != "")
                                {
                                    try
                                    {
                                        await EmailService.SendMailAsync(email, "Fuel Requisition Recommendation Request",
                                        EmailService.EmailMessage.FuelRequestRecommendation(touser, fromuser, HtmlMail, inreqmes.Requested_Message));
                                    }
                                    catch (Exception)
                                    {
                                        //wrong email
                                    }
                                }

                                Scope.Complete();

                                var newUrl = this.Url.Link("Default", new
                                {
                                    Controller = "FuelRequest",
                                    Action = "Index"
                                });
                                return Request.CreateResponse(HttpStatusCode.OK, new { Success = true, RedirectUrl = newUrl });
                            }
                        }
                        else
                        {
                            var message = "Please Add Records to Continue.";
                            HttpError err = new HttpError(message);
                            return Request.CreateResponse(HttpStatusCode.NotFound, err);
                        }
                    }
                    else
                    {
                        var message = "Please Give Request Message.";
                        HttpError err = new HttpError(message);
                        return Request.CreateResponse(HttpStatusCode.NotFound, err);
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Failed...");
                }
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine($"Exception: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");

                // Return a more detailed error response
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"An error occur: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<HttpResponseMessage> RecommendRequisition(FuelRequestsMessageVM inreqmes)//approve requisition
        {
            try
            {
                if (inreqmes.Recommended_Message != "")
                {
                    if (inreqmes.inreqList.Count >= 1)
                    {
                        using (TransactionScope Scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                        {
                            var Item = _unitofWork.FuelRequestMessage.GetById(inreqmes.Id);
                            Item.Recommended_Date = DateTime.Now.Date;
                            Item.Recommended_Message = inreqmes.Recommended_Message;
                            Item.Status = "Recommended";
                            Item.Approved_By = inreqmes.Approved_By;
                            _unitofWork.FuelRequestMessage.Update(Item);
                            _unitofWork.Save();

                            foreach (var itm in inreqmes.inreqList.Where(x => x.Item > 0))
                            {
                                var data = _unitofWork.FuelRequest.GetByMessageIdandProductId(inreqmes.Id, itm.Item);
                                data.Recommended_Quantity = itm.Recommended_Quantity;
                                _unitofWork.FuelRequest.Update(data);
                                _unitofWork.Save();
                            }

                            var notificationdata = _unitofWork.InNotifications.GetByPOMId(inreqmes.Id);
                            if (notificationdata != null)
                            {
                                notificationdata.Status = true;
                                _unitofWork.InNotifications.Update(notificationdata);
                                _unitofWork.Save();
                            }

                            var imsn = new InNotificationsVM();
                            imsn.CreatedDate = DateTime.Now;
                            imsn.Subject = "Fuel Approval Request";
                            imsn.Forwardedby = (int)Item.Recommended_By;
                            imsn.Forwardedto = Convert.ToInt32(inreqmes.Approved_By);
                            imsn.URL = "/FuelApprove/Index" + "/" + inreqmes.Id;
                            imsn.SpecialId = inreqmes.Id;
                            imsn.Status = false;
                            _unitofWork.InNotifications.Insert(imsn);
                            _unitofWork.Save();

                            var email = CodeService.GetOfficialEmail((int)inreqmes.Approved_By);
                            var fromuser = CodeService.GetEmployeeFullName(Item.Recommended_By);
                            var touser = CodeService.GetEmployeeFirstName((int)inreqmes.Approved_By);
                            var recommendeddata = _unitofWork.FuelRequest.GetItemById(inreqmes.Id);
                            var tdqty = recommendeddata.Sum(x => x.Recommended_Quantity);
                            int sn = 1;
                            string tabledata = "<thead><tr><th>SN</th><th>Fuel Cat</th><th>Vehicle Cat</th><th>Vendor</th><th>Vehicle No.</th><th>Coupon No.</th><th>Prev. KM Run</th><th>Current. KM Run</th><th>Total KM Run</th><th>Unit</th><th>Req. Qty</th><th>Rec. Qty</th></tr></thead><tbody>";
                            foreach (var item in recommendeddata.OrderBy(x => x.Coupon_No))
                            {
                                tabledata += @"<tr>
                                    <td style='border:1px solid #000; padding:10px'>" + sn + @"</td>
                                    <td style='border:1px solid #000; padding:10px'>" + item.Fuel_Category + @"</td>
                                    <td style='border:1px solid #000; padding:10px'>" + item.Vehicle_Category + @"</td>
                                    <td style='border:1px solid #000; padding:10px; text-align:center;'>" + item.Vendor + @"</td>
                                    <td style='border:1px solid #000; padding:10px; text-align:center;'>" + item.Vehicle_No + @"</td>
                                    <td style='border:1px solid #000; padding:10px; text-align:center;'>" + item.Coupon_No + @"</td>
                                    <td style='border:1px solid #000; padding:10px; text-align:center;'>" + item.Previous_KM_Run + @"</td>
                                    <td style='border:1px solid #000; padding:10px; text-align:center;'>" + item.KM_Run + @"</td>
                                    <td style='border:1px solid #000; padding:10px; text-align:center;'>" + (Convert.ToInt32(item.KM_Run) - Convert.ToInt32(item.Previous_KM_Run)) + @"</td>
                                    <td style='border:1px solid #000; padding:10px; text-align:center;'>" + item.Unit + @"</td>
                                    <td style='border:1px solid #000; padding:10px; text-align:center;'>" + item.Requested_Quantity + @"</td>
                                    <td style='border:1px solid #000; padding:10px; text-align:center;'>" + item.Recommended_Quantity + @"</td>
                                    </tr>";
                                sn++;
                            }
                            string HtmlMail = @"<html><table border='1' cellspacing='0' cellpadding='0' >" + tabledata + "</tbody></table></html>";


                            if (email != "")
                            {
                                try
                                {
                                    await EmailService.SendMailAsync(email, "Fuel Approval Request",
                                    EmailService.EmailMessage.FuelApprovalRequest(touser, fromuser, HtmlMail, inreqmes.Recommended_Message));
                                }
                                catch (Exception)
                                {
                                    //wrong email
                                }
                            }


                            Scope.Complete();
                            var newUrl = this.Url.Link("Default", new
                            {
                                Controller = "FuelRecommend",
                                Action = "Index"
                            });
                            return Request.CreateResponse(HttpStatusCode.OK, new { Success = true, RedirectUrl = newUrl });
                        }

                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Failed...");
                    }
                }
                else
                {
                    var message = string.Format("Please Give Recommended Message.");
                    HttpError err = new HttpError(message);
                    return Request.CreateResponse(HttpStatusCode.NotFound, err);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpPost]
        public async Task<HttpResponseMessage> ApproveRequisition(FuelRequestsMessageVM inreqmes)//approve requisition
        {
            var Session = HttpContext.Current.Session;
            try
            {
                if (inreqmes.Approved_Message != "")
                {
                    if (inreqmes.inreqList.Count >= 1)
                    {
                        using (TransactionScope Scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                        {
                            //inrequisitionmessage update
                            var Item = _unitofWork.FuelRequestMessage.GetById(inreqmes.Id);
                            Item.Approved_Date = DateTime.Now.Date;
                            Item.Status = "Approved";
                            Item.Approved_Message = inreqmes.Approved_Message;
                            _unitofWork.FuelRequestMessage.Update(Item);
                            _unitofWork.Save();

                            //inrequisition update
                            foreach (var itm in inreqmes.inreqList.Where(x => x.Item > 0))
                            {
                                var data = _unitofWork.FuelRequest.GetByMessageIdandProductId(inreqmes.Id, itm.Item);
                                data.Approved_Quantity = itm.Approved_Quantity;
                                _unitofWork.FuelRequest.Update(data);
                                _unitofWork.Save();
                            }

                            var notificationdata = _unitofWork.InNotifications.GetByPOMId(inreqmes.Id);
                            if (notificationdata != null)
                            {
                                notificationdata.Status = true;
                                _unitofWork.InNotifications.Update(notificationdata);
                                _unitofWork.Save();
                            }

                          
                            var imsn = new InNotificationsVM();
                            imsn.CreatedDate = DateTime.Now;
                            imsn.Subject = "Fuel Request Approved";
                            imsn.Forwardedby = (int)Item.Approved_By;
                            imsn.Forwardedto = (Item.Requested_By);
                            imsn.URL = "/FuelRequest/Index/"+ inreqmes.Id;
                            imsn.SpecialId = inreqmes.Id;
                            imsn.Status = false;
                            _unitofWork.InNotifications.Insert(imsn);
                            _unitofWork.Save(); 
               

                            var email = CodeService.GetOfficialEmail(Item.Requested_By);
                            var fromuser = CodeService.GetEmployeeFullName((int)Item.Approved_By);
                            var touser = CodeService.GetEmployeeFirstName(Item.Requested_By);
                            var approveddata = _unitofWork.FuelRequest.GetItemById(inreqmes.Id);
                            var tdqty = approveddata.Sum(x => x.Approved_Quantity);
                            int sn = 1;
                            string tabledata = "<thead><tr><th>SN</th><th>Fuel Cat</th><th>Vehicle Cat</th><th>Vendor</th><th>Vehicle No.</th><th>Coupon No.</th><th>Prev. KM Run</th><th>Current. KM Run</th><th>Total KM Run</th><th>Unit</th><th>Req. Qty</th><th>Rec. Qty</th></tr></thead><tbody>";
                            foreach (var item in approveddata.OrderBy(x => x.Coupon_No))
                            {
                                tabledata += @"<tr>
                                    <td style='border:1px solid #000; padding:10px'>" + sn + @"</td>
                                    <td style='border:1px solid #000; padding:10px'>" + item.Fuel_Category + @"</td>
                                    <td style='border:1px solid #000; padding:10px'>" + item.Vehicle_Category + @"</td>
                                    <td style='border:1px solid #000; padding:10px; text-align:center;'>" + item.Vendor + @"</td>
                                    <td style='border:1px solid #000; padding:10px; text-align:center;'>" + item.Vehicle_No + @"</td>
                                    <td style='border:1px solid #000; padding:10px; text-align:center;'>" + item.Coupon_No + @"</td>
                                    <td style='border:1px solid #000; padding:10px; text-align:center;'>" + item.Previous_KM_Run + @"</td>
                                    <td style='border:1px solid #000; padding:10px; text-align:center;'>" + item.KM_Run + @"</td>
                                    <td style='border:1px solid #000; padding:10px; text-align:center;'>" + (Convert.ToInt32(item.KM_Run) - Convert.ToInt32(item.Previous_KM_Run)) + @"</td>
                                    <td style='border:1px solid #000; padding:10px; text-align:center;'>" + item.Unit + @"</td>
                                    <td style='border:1px solid #000; padding:10px; text-align:center;'>" + item.Requested_Quantity + @"</td>
                                    <td style='border:1px solid #000; padding:10px; text-align:center;'>" + item.Recommended_Quantity + @"</td>
                                    </tr>";
                                sn++;
                            }
                            string HtmlMail = @"<html><table border='1' cellspacing='0' cellpadding='0' >" + tabledata + "</tbody></table></html>";

                            Scope.Complete();
                            if (email != "")
                            {
                                try
                                {
                                    await EmailService.SendMailAsync(email, "Fuel Request Approved",
                                    EmailService.EmailMessage.FuelRequestApproved(touser, fromuser, HtmlMail, inreqmes.Approved_Message));
                                }
                                catch (Exception)
                                {
                                    //wrong email
                                }
                            }

                            var newUrl = this.Url.Link("Default", new
                            {
                                Controller = "FuelApprove",
                                Action = "Index"
                            });
                            return Request.CreateResponse(HttpStatusCode.OK, new { Success = true, RedirectUrl = newUrl });
                        }
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Failed...");
                    }
                }
                else
                {
                    var message = string.Format("Please Give Approval Message.");
                    HttpError err = new HttpError(message);
                    return Request.CreateResponse(HttpStatusCode.NotFound, err);
                }
            }
#pragma warning disable CS0168 // The variable 'Ex' is declared but never used
            catch (Exception Ex)
#pragma warning restore CS0168 // The variable 'Ex' is declared but never used
            {
                throw;
            }
        }


        [HttpPost]
        public async Task<HttpResponseMessage> RejectRequest(FuelRequestsMessageVM inreqmes)
        {
            var Session = HttpContext.Current.Session;
            var Item = _unitofWork.FuelRequestMessage.GetById(inreqmes.Id);
            Item.Rejected_By = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
            Item.Rejected_Date = DateTime.Now.Date;
            Item.Status = "Rejected";
            Item.Rejected_Message = inreqmes.Rejected_Message;
            _unitofWork.FuelRequestMessage.Update(Item);

            var notificationdata = _unitofWork.InNotifications.GetByPOMId(inreqmes.Id);
            if (notificationdata != null)
            {
                notificationdata.Status = true;
                _unitofWork.InNotifications.Update(notificationdata);
            }

            _unitofWork.Save();

            string defaultController = "Index"; // Default controller name

            // Get the referrer URL from the headers
            string referrerUrl = Request.Headers.Referrer?.AbsolutePath;
            string controllerName = defaultController;
            if (!string.IsNullOrEmpty(referrerUrl))
            {
                string[] segments = referrerUrl.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                if (segments.Length >= 1)
                {
                    controllerName = segments[0];// Using the first segment
                }
            }
            var imsn = new InNotificationsVM();
            imsn.CreatedDate = DateTime.Now;
            imsn.Subject = "Fuel Request Rejected";
            imsn.Forwardedby = (Item.Approved_By.HasValue && Item.Approved_By.Value != 0)
                             ? Item.Approved_By.Value
                             : Item.Recommended_By;

            imsn.Forwardedto = (Item.Requested_By);
            imsn.URL = "/FuelRequest/Index/" + inreqmes.Id;
            imsn.SpecialId = inreqmes.Id;
            imsn.Status = false;
            _unitofWork.InNotifications.Insert(imsn);
            _unitofWork.Save();


            var email = CodeService.GetOfficialEmail((int)Item.Requested_By);
            var fromuser = CodeService.GetEmployeeFullName(
                             Item.Approved_By.HasValue && Item.Approved_By.Value != 0
                             ? Item.Approved_By.Value
                             : Item.Recommended_By);
            var touser = CodeService.GetEmployeeFirstName(Item.Requested_By);
            var recommendeddata = _unitofWork.FuelRequest.GetItemById(inreqmes.Id);
            var tdqty = recommendeddata.Sum(x => x.Approved_Quantity);
            int sn = 1;
            string tabledata = "<thead><tr><th>SN</th><th>Fuel Cat</th><th>Vehicle Cat</th><th>Vendor</th><th>Vehicle No.</th><th>Coupon No.</th><th>Prev. KM Run</th><th>Current. KM Run</th><th>Total KM Run</th><th>Unit</th><th>Req. Qty</th><th>Rec. Qty</th></tr></thead><tbody>";
            foreach (var item in recommendeddata.OrderBy(x => x.Coupon_No))
            {
                tabledata += @"<tr>
                                    <td style='border:1px solid #000; padding:10px'>" + sn + @"</td>
                                    <td style='border:1px solid #000; padding:10px'>" + item.Fuel_Category + @"</td>
                                    <td style='border:1px solid #000; padding:10px'>" + item.Vehicle_Category + @"</td>
                                    <td style='border:1px solid #000; padding:10px; text-align:center;'>" + item.Vendor + @"</td>
                                    <td style='border:1px solid #000; padding:10px; text-align:center;'>" + item.Vehicle_No + @"</td>
                                    <td style='border:1px solid #000; padding:10px; text-align:center;'>" + item.Coupon_No + @"</td>
                                    <td style='border:1px solid #000; padding:10px; text-align:center;'>" + item.Previous_KM_Run + @"</td>
                                    <td style='border:1px solid #000; padding:10px; text-align:center;'>" + item.KM_Run + @"</td>
                                    <td style='border:1px solid #000; padding:10px; text-align:center;'>" + (Convert.ToInt32(item.KM_Run) - Convert.ToInt32(item.Previous_KM_Run)) + @"</td>
                                    <td style='border:1px solid #000; padding:10px; text-align:center;'>" + item.Unit + @"</td>
                                    <td style='border:1px solid #000; padding:10px; text-align:center;'>" + item.Requested_Quantity + @"</td>
                                    <td style='border:1px solid #000; padding:10px; text-align:center;'>" + item.Recommended_Quantity + @"</td>
                                    </tr>";
                sn++;
            }
            string HtmlMail = @"<html><table border='1' cellspacing='0' cellpadding='0' >" + tabledata + "</tbody></table></html>";

            if (email != "")
            {
                try
                {
                    await EmailService.SendMailAsync(email, "Fuel Request Rejected",
                    EmailService.EmailMessage.FuelRequestRejected(touser, fromuser, HtmlMail, inreqmes.Rejected_Message));
                }
                catch (Exception)
                {
                    //wrong email
                }
            }
            var uri = new Uri(Url.Link("Default", new { controller = controllerName, action = "Index" }));

            var response = new
            {
                Message = "Rejected",
                RedirectUrl = uri.AbsoluteUri
            };

            return Request.CreateResponse(HttpStatusCode.OK, response, "application/json");
        }
    }
}