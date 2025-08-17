using InventoryManagementSystem.Data.Models;
using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure;
using InventoryManagementSystem.Infrastructure.DapperRepo;
using InventoryManagementSystem.Infrastructure.Interface;
using InventoryManagementSystem.Infrastructure.Repository;
using InventoryManagementSystem.Infrastructure.Services;
using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;

namespace InventoryManagementSystem.Controllers.Api
{
    [System.Web.Mvc.Route("Api/{controller}/{action}")]
    public class PurchaseApiController : ApiController
    {
        private IUnitOfWork _unitofWork;
        private DapperRepoServices _dapperrepo;
        public PurchaseApiController()
        {
            _unitofWork = new UnitOfWork();
            _dapperrepo = new DapperRepoServices();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<HttpResponseMessage> Create(PurchaseOrderMessageVM pom)
        {
            var Session = HttpContext.Current.Session;

            foreach (var itm1 in pom.poList.Where(x => x.productname != "" && x.rate > 0))
            {
                var serialstatus = CodeService.GetSerialStatusForProduct(itm1.productname.Trim());
                var TempPurId = CodeService.GetTempPurchaseIdForProduct(itm1.productname.Trim(), (int)itm1.bill_id);
                var TempPurOtherId = CodeService.GetTempPurchaseOtherId(TempPurId);
                if (serialstatus == true && TempPurOtherId == 0)
                {
                    var message = string.Format("Please Give Serial No For Serialized Product");
                    HttpError err = new HttpError(message);
                    return Request.CreateResponse(HttpStatusCode.NotFound, err);
                }
                if (TempPurId > 0 && serialstatus == false)
                {
                    _unitofWork.TempPurchase.Delete(TempPurId);
                    _unitofWork.Save();
                }
            }
            try
            {
                if (ModelState.IsValid)
                {
                    if (pom.poList.Count > 1)
                    {
                        using (TransactionScope Scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                        {

                            //purchaseordermessage insert
                            var compshortname = CodeService.GetCompanyShortName();
                            var fiscalyear = CodeService.GetFiscalYear();
                            var purchaseordermessageid = CodeService.GetPurchaseOrderMessageId();
                            pom.status = "Requested";
                            pom.order_no = compshortname + "-" + "PO" + "-" + fiscalyear + "-" + purchaseordermessageid;
                            pom.branch_id = Convert.ToInt32(Session["BranchId"]);
                            pom.department_id = Convert.ToInt32(Session["DepartmentId"]);
                            pom.created_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
                            pom.created_date = DateTime.Now;
                            _unitofWork.PurchaseOrderMessage.Insert(pom);
                            _unitofWork.Save();

                            //purchaseorder insert
                            var po = new PurchaseOrderVM();
                            foreach (var itm in pom.poList.Where(x => x.productname != "" && x.rate > 0))
                            {
                                var pid = CodeService.GetProductIdFromProductName(itm.productname.Trim());
                                po.order_message_id = CodeService.GetPurchaseOrderMessageIdforpurchaseorder();
                                po.product_code = pid;
                                po.qty = itm.qty;
                                po.rate = itm.rate;
                                po.amount = itm.amount;
                                _unitofWork.PurchaseOrder.Insert(po);
                                _unitofWork.Save();
                            }

                            //PurchaseOrderMessageHistory insert
                            var OrdId = CodeService.GetPurchaseOrderMessageIdforpurchaseorder();
                            var pomh = new PurchaseOrderMessageHistoryVM();
                            pomh.Ord_id = OrdId;
                            pomh.Requ_date = pom.order_date;
                            pomh.from_user = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
                            pomh.to_user = Convert.ToString(pom.forwarded_to);
                            _unitofWork.PurchaseOrderMessageHistory.Insert(pomh);
                            _unitofWork.Save();


                            //UserNotification and Email
                            var imsn = new InNotificationsVM();
                            imsn.CreatedDate = DateTime.Now;
                            imsn.Subject = "P.O. Approval Request";
                            imsn.Forwardedby = int.Parse(pomh.from_user);
                            imsn.Forwardedto = int.Parse(pomh.to_user);
                            imsn.URL = "/ApprovePurchaseOrder/Index" + "/" + OrdId;
                            imsn.SpecialId = OrdId;
                            imsn.Status = false;
                            _unitofWork.InNotifications.Insert(imsn);
                            _unitofWork.Save();

                            var email = CodeService.GetOfficialEmail(int.Parse(pomh.to_user));
                            if (email != "")
                            {
                                try
                                {
                                    var touser = CodeService.GetEmployeeFirstName(int.Parse(pomh.to_user));
                                    var fromuser = CodeService.GetEmployeeFullName(int.Parse(pomh.from_user));
                                    await EmailService.SendMailAsync(email, "Requisiton Sent",
                                    EmailService.EmailMessage.ApprovedRequest(touser, fromuser));
                                }
                                catch (Exception ex)
                                {

                                }
                            }

                            //await Task.Delay(100).ConfigureAwait(false);
                            Scope.Complete();

                            var newUrl = this.Url.Link("Default", new
                            {
                                Controller = "PurchaseOrderMessage",
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
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Failed...");

            }

            catch (Exception Ex)

            {
                throw;
            }
        }



        //        [HttpPost]
        //        public async  Task<HttpResponseMessage> DirectPurchaseOrderCreate(BillInfoVM bi)
        //        {
        //            if (bi.billno != "" && bi.bill_notes != "")
        //            {
        //                bool billNoExists =  _unitofWork.BillInfo.BillNoExists(bi.billno.Trim());
        //                if (billNoExists)
        //                {
        //                    var message = string.Format("Bill No. '{0}' already exists.", bi.billno);
        //                    HttpError err = new HttpError(message);
        //                    return Request.CreateResponse(HttpStatusCode.BadRequest, err);
        //                }
        //                var Session = HttpContext.Current.Session;
        //                foreach (var itm1 in bi.poList.Where(x => x.productname != "" && x.rate > 0))
        //                {
        //                    var serialstatus = CodeService.GetSerialStatusForProduct(itm1.productname);
        //                    var TempPurId = CodeService.GetTempPurchaseIdForProduct(itm1.productname);
        //                    var TempPurOtherId = CodeService.GetTempPurchaseOtherId(TempPurId);
        //                    //  var data = _unitofWork.TempPurchaseOther.GetById(TempPurId);
        //                    if (serialstatus == true && TempPurOtherId == 0)
        //                    {
        //                        var message = string.Format("Please Give Serial No For Serialized Product");
        //                        HttpError err = new HttpError(message);
        //                        return Request.CreateResponse(HttpStatusCode.NotFound, err);
        //                    }
        //                    if (TempPurId > 0 && serialstatus == false)
        //                    {
        //                        _unitofWork.TempPurchase.Delete(TempPurId);
        //                        _unitofWork.Save();
        //                    }
        //                }
        //                try
        //                {
        //                    if (bi.poList.Count > 1)
        //                    {
        //                        using (TransactionScope Scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        //                        {
        //                            bi.bill_type = "p";
        //                            bi.entered_date = DateTime.Now;
        //                            bi.entered_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
        //                            bi.forwarded_to = bi.forwarded_to;
        //                            bi.Status = "Direct PO Request"; // Set initial status
        //                            _unitofWork.BillInfo.Insert(bi);
        //                            _unitofWork.Save();

        //                            // declare variable to insert in purchaseordermessage 
        //                            var compshortname = CodeService.GetCompanyShortName();
        //                            var fiscalyear = CodeService.GetFiscalYear();
        //                            var purchaseordermessageid = CodeService.GetPurchaseOrderMessageId();

        //                            // Initialize and assign properties to PurchaseOrderMessageVM
        //                            var pom = new PurchaseOrderMessageVM
        //                            {

        //                                status = "DirectPurchaseRequested",
        //                                order_no = compshortname + "-" + "PO" + "-" + fiscalyear + "-" + purchaseordermessageid,
        //                                order_date = DateTime.Now,
        //                                branch_id = Convert.ToInt32(Session["BranchId"]),
        //                                forwarded_to = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name,
        //                                remarks = "Direct Purchase Order",
        //                                department_id = Convert.ToInt32(Session["DepartmentId"]),
        //                                created_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name,
        //                                created_date = DateTime.Now
        //                            };

        //                            _unitofWork.PurchaseOrderMessage.Insert(pom);
        //                            _unitofWork.Save();

        //                            //purchaseorder insert
        //                            var po = new PurchaseOrderVM();
        //                            foreach (var itm in bi.poList.Where(x => x.productname != "" && x.rate > 0))
        //                            {
        //                                var pid = CodeService.GetProductIdFromProductName(itm.productname.Trim());
        //                                po.order_message_id = CodeService.GetPurchaseOrderMessageIdforpurchaseorder();
        //                                po.product_code = pid;
        //                                po.qty = itm.qty;
        //                                po.rate = itm.rate;
        //                                po.amount = itm.amount;
        //                                _unitofWork.PurchaseOrder.Insert(po);
        //                                _unitofWork.Save();
        //                            }

        //                            //PurchaseOrderMessageHistory insert
        //                            var OrdId = CodeService.GetPurchaseOrderMessageIdforpurchaseorder();
        //                            var pomh = new PurchaseOrderMessageHistoryVM();
        //                            pomh.Ord_id = OrdId;
        //                            pomh.Requ_date = pom.order_date;
        //                            pomh.from_user = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
        //                            pomh.to_user = Convert.ToString(pom.forwarded_to);
        //                            _unitofWork.PurchaseOrderMessageHistory.Insert(pomh);
        //                            _unitofWork.Save();

        //                            //UserNotification and Email
        //                            var imsn = new InNotificationsVM();
        //                            imsn.CreatedDate = DateTime.Now;
        //                            imsn.Subject = "P.O. Approval Request";
        //                            imsn.Forwardedby = int.Parse(pomh.from_user);
        //                            imsn.Forwardedto = int.Parse(pomh.to_user);
        //                            imsn.URL = "/ApprovePurchaseOrder/Index" + "/" + OrdId;
        //                            imsn.SpecialId = OrdId;
        //                            imsn.Status = false;
        //                            _unitofWork.InNotifications.Insert(imsn);
        //                            _unitofWork.Save();

        //                            var email = CodeService.GetOfficialEmail(int.Parse(pomh.to_user));
        //                            if (email != "")
        //                            {
        //                                try
        //                                {
        //                                    var touser = CodeService.GetEmployeeFirstName(int.Parse(pomh.to_user));
        //                                    var fromuser = CodeService.GetEmployeeFullName(int.Parse(pomh.from_user));
        //                                    await EmailService.SendMailAsync(email, "Requisiton Sent",
        //                                    EmailService.EmailMessage.ApprovedRequest(touser, fromuser));
        //                                }
        //                                catch (Exception ex)
        //                                {

        //                                }
        //                            }

        //                            ////get billid
        //                            //var billid = CodeService.GetBillInfoId();
        //                            //var inpurchase = new InPurchaseVM();
        //                            //foreach (var itm in bi.poList.Where(x => x.product_code > 0 && x.rate > 0))
        //                            //{
        //                            //    inpurchase.bill_id = billid;
        //                            //    inpurchase.prod_code = itm.product_code;
        //                            //    inpurchase.p_qty = itm.qty;
        //                            //    inpurchase.p_rate = itm.rate;
        //                            //    inpurchase.p_stk_remain = itm.qty;
        //                            //    inpurchase.entered_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
        //                            //    inpurchase.entered_date = DateTime.Now.Date;
        //                            //    inpurchase.branch_id = Convert.ToInt32(Session["BranchId"]);
        //                            //    _unitofWork.InPurchase.Insert(inpurchase);
        //                            //    _unitofWork.Save();

        //                            //    var serialstatus = CodeService.GetSerialStatusForProductById(itm.product_code);
        //                            //    var purid = CodeService.GetPurchaseId();
        //                            //    var branchid = Convert.ToInt32(Session["BranchId"]);
        //                            //    if (serialstatus == true)
        //                            //    {
        //                            //        var tpdata = _dapperrepo.GetTempPurchaseDataByTPO(itm.product_code);
        //                            //        var tpodata = _dapperrepo.GetTempPurchaseOtherDataByTPO(tpdata);
        //                            //        int snfrom = Convert.ToInt32(tpodata.sn_from);
        //                            //        int snto = Convert.ToInt32(tpodata.sn_to);
        //                            //        var spsdata = new SerialProductStockVM();
        //                            //        _dapperrepo.InsertSerialProductStockPurchaseVoucher(branchid, itm.product_code, purid, snfrom, snto);
        //                            //        var inpurdata = _dapperrepo.getInPurchaseDataByPIdAndBillId(itm.product_code, billid);
        //                            //        inpurdata.p_sn_from = snfrom.ToString();
        //                            //        inpurdata.p_sn_to = snto.ToString();
        //                            //        _unitofWork.InPurchase.Update(inpurdata);

        //                            //        var tpdataremove = _unitofWork.TempPurchase.GetById(tpdata);
        //                            //        _unitofWork.TempPurchase.Delete(tpdataremove.id);

        //                            //        var tpodataremove = _unitofWork.TempPurchaseOther.GetById(tpdata);
        //                            //        _unitofWork.TempPurchaseOther.Delete(tpdataremove.id);
        //                            //    }
        //                            //    // in branch update
        //                            //    var inbranch = _unitofWork.InBranchAssign.GetItemById(itm.product_code, Convert.ToInt32(Session["BranchId"]));
        //                            //    if (inbranch == null)
        //                            //    {
        //                            //        var messagea = string.Format("Branch not Assigned for " + itm.productname + "");
        //                            //        HttpError err = new HttpError(messagea);
        //                            //        return Request.CreateResponse(HttpStatusCode.NotFound, err);
        //                            //    }
        //                            //    inbranch.stock_in_hand = inbranch.stock_in_hand + itm.qty;
        //                            //    _unitofWork.InBranchAssign.Update(inbranch);
        //                            //    _unitofWork.Save();
        //                            //}

        //                            var VoucherNo = _dapperrepo.getVoucherNo();
        //                            _dapperrepo.UpdateVoucherNo();

        //                            Scope.Complete();
        //                            var newUrl = this.Url.Link("Default", new
        //                            {
        //                                Controller = "DirectPurchaseOrder",
        //                                Action = "Create"
        //                            });

        //                            var message = string.Format("Voucher Save Successfully, Voucher No : " + VoucherNo + "");

        //                            return Request.CreateResponse(HttpStatusCode.OK, new { Success = true, RedirectUrl = newUrl, VoucherNo = message });
        //                        }
        //                    }
        //                    else
        //                    {
        //                        return Request.CreateResponse(HttpStatusCode.NotFound, "Failed...");
        //                    }
        //                }
        //#pragma warning disable CS0168 // The variable 'Ex' is declared but never used
        //                catch (Exception Ex)
        //#pragma warning restore CS0168 // The variable 'Ex' is declared but never used
        //                {

        //                    throw;
        //                }
        //            }
        //            else
        //            {
        //                return Request.CreateResponse(HttpStatusCode.NotFound, "Failed...");
        //            }
        //        }
        [HttpPost]
        public async Task<HttpResponseMessage> DirectPurchaseOrderCreate(BillInfoVM bi)
        {
            try
            {
                if (string.IsNullOrEmpty(bi.VendorName))
                {
                    var message = "Vendor Name is required";
                    HttpError err = new HttpError(message);
                    return Request.CreateResponse(HttpStatusCode.BadRequest, err);
                }
                if (string.IsNullOrEmpty(bi.billno) || string.IsNullOrEmpty(bi.bill_notes))
                {
                    var message = "Bill number and notes are required";
                    HttpError err = new HttpError(message);
                    return Request.CreateResponse(HttpStatusCode.BadRequest, err);
                }

                bool billNoExists = _unitofWork.BillInfo.BillNoExists(bi.billno.Trim(), bi.VendorName.Trim());
                if (billNoExists)
                {
                    var message = $"Bill No.{bi.billno} from Vendor '{bi.VendorName}' already exists!";
                    HttpError err = new HttpError(message);
                    return Request.CreateResponse(HttpStatusCode.BadRequest, err);
                }

                var Session = HttpContext.Current.Session;

                // Validate and update TempPurchase for each product
                foreach (var itm1 in bi.poList.Where(x => !string.IsNullOrEmpty(x.productname) && x.rate > 0))
                {
                    var serialstatus = CodeService.GetSerialStatusForProduct(itm1.productname);
                    var tempPurId = CodeService.GetTempPurchaseIdForProduct(itm1.productname);
                    var tempPurOtherId = CodeService.GetTempPurchaseOtherId(tempPurId);

                    if (serialstatus == true && tempPurOtherId == 0)
                    {
                        var message = "Please Give Serial No For Serialized Product";
                        HttpError err = new HttpError(message);
                        return Request.CreateResponse(HttpStatusCode.NotFound, err);
                    }

                    if (tempPurId > 0 && serialstatus == false)
                    {
                        _unitofWork.TempPurchase.Delete(tempPurId);
                        _unitofWork.Save();
                    }
                }

                if (bi.poList == null || !bi.poList.Any(x => !string.IsNullOrEmpty(x.productname) && x.rate > 0))
                {
                    var message = "At least one valid product must be included in the purchase order.";
                    HttpError err = new HttpError(message);
                    return Request.CreateResponse(HttpStatusCode.BadRequest, err);
                }

                using (TransactionScope Scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    // Insert BillInfo
                    var billInfo = Mapper.Convert(bi);
                    billInfo.bill_type = "p";
                    billInfo.entered_date = DateTime.Now;
                    billInfo.entered_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
                    billInfo.Status = "Direct P.O Request";
                    billInfo.branch_id = bi.branch_id;
                    billInfo.forwarded_to = bi.forwarded_to;
                    billInfo.bill_amount = bi.bill_amount;
                    billInfo.vat_amt = bi.vat_amt;
                    billInfo.VendorName = bi.VendorName;
                    billInfo.bill_notes = bi.bill_notes;
                    billInfo.billno = bi.billno;
                    billInfo.bill_date = DateTime.Now;

                    _unitofWork.BillInfo.Insert(billInfo);
                    _unitofWork.Save();

                    var billId = billInfo.bill_id; // Fetching just inserted bill_id

                    // Update TempPurchase with bill_id for each product
                    foreach (var itm1 in bi.poList.Where(x => !string.IsNullOrEmpty(x.productname) && x.rate > 0))
                    {
                        var tempPurId = CodeService.GetTempPurchaseIdForProduct(itm1.productname);
                        var temp = _unitofWork.TempPurchase.GetById(tempPurId);

                        if (temp != null)
                        {
                            temp.bill_id = billId;
                            _unitofWork.TempPurchase.Update(temp);
                            _unitofWork.Save();
                        }
                    }

                    // Insert Purchase Order for each product
                    foreach (var itm in bi.poList.Where(x => !string.IsNullOrEmpty(x.productname) && x.rate > 0))
                    {
                        var pid = CodeService.GetProductIdFromProductName(itm.productname.Trim());
                        var purchaseOrder = new PurchaseOrderVM
                        {
                            order_message_id = CodeService.GetPurchaseOrderMessageIdforpurchaseorder(),
                            product_code = pid,
                            qty = itm.qty,
                            rate = itm.rate,
                            amount = itm.qty * itm.rate,
                            Received_Qty = itm.qty,
                            bill_id = billId
                        };
                        _unitofWork.PurchaseOrder.Insert(purchaseOrder);
                    }

                    // Insert Notification
                    var imsn = new InNotificationsVM
                    {
                        CreatedDate = DateTime.Now,
                        Subject = "Direct P.O. Approval Request",
                        Forwardedby = int.Parse(billInfo.entered_by),
                        Forwardedto = (int)bi.forwarded_to,
                        URL = "/ApproveDirectPurchaseOrder/Index" + "/" + billId,
                        SpecialId = billId,
                        Status = false
                    };
                    _unitofWork.InNotifications.Insert(imsn);

                    _unitofWork.Save();
                    Scope.Complete();

                    var newUrl = this.Url.Link("Default", new
                    {
                        Controller = "DirectPurchaseOrder",
                        Action = "Create"
                    });

                    var message = "Successfully Requested and Sent for approval";
                    return Request.CreateResponse(HttpStatusCode.OK, new { Success = true, RedirectUrl = newUrl, VoucherNo = message });
                }
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, $"An unexpected error occurred. Please try again later.");
            }
        }




        [HttpPost]
        public HttpResponseMessage TempPurchaseCreate(TempPurchaseVM tp)
        {
            var Session = HttpContext.Current.Session;
            try
            {
                if (ModelState.IsValid)
                {
                    //temp purchase insert
                    tp.session_id = "testseeid";//need to generate sessionid
                    tp.flag = "o";
                    tp.created_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
                    tp.created_date = DateTime.Now.ToShortDateString();
                    tp.IsActive = true;
                    tp.order_message_id = 0;
                    tp.bill_id = null;
                    _unitofWork.TempPurchase.Insert(tp);
                    _unitofWork.Save();
                    var TempPurchaseId = CodeService.GetTempPurchaseId();
                    var serialstatus = CodeService.GetSerialStatusForProduct(tp.productname);
                    return Request.CreateResponse(HttpStatusCode.OK, (TempPurchaseId, serialstatus));
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Failed...");
            }
#pragma warning disable CS0168 // The variable 'Ex' is declared but never used
            catch (Exception Ex)
#pragma warning restore CS0168 // The variable 'Ex' is declared but never used
            {
                throw;
            }
        }

        [HttpPost]
        public HttpResponseMessage TempPurchaseOtherCreate(TempPurchaseOtherVM tpo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    tpo.session_id = "testseeid";//need to generate sessionid
                    tpo.is_approved = "y";
                    tpo.IsActive = true;
                    _unitofWork.TempPurchaseOther.Insert(tpo);
                    _unitofWork.Save();
                    return Request.CreateResponse(HttpStatusCode.OK, "Success...");
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Failed...");
            }
#pragma warning disable CS0168 // The variable 'Ex' is declared but never used
            catch (Exception Ex)
#pragma warning restore CS0168 // The variable 'Ex' is declared but never used
            {

                throw;
            }
        }

        //[HttpPost]
        //public async Task<ActionResult> CleanupTempData()
        //{
        //    var data = _unitofWork.TempPurchase.GetAll();

        //    foreach (var item in data)
        //    {
        //        _unitofWork.TempPurchaseOther.Delete(item.id);
        //        _unitofWork.TempPurchase.Delete(item.id);

        //    }

        //    await _unitofWork.SaveAsync();

        //    return new HttpStatusCodeResult(204); 
        //}




        //[HttpPost]
        //public async Task<HttpResponseMessage> DirectPurchaseOrderCreate(BillInfoVM bi)
        //{
        //    try

        //    {
        //        int temppId = 0;
        //        if (string.IsNullOrEmpty(bi.VendorName))
        //        {
        //            var message = string.Format("Vendor Name is required");
        //            HttpError err = new HttpError(message);
        //            return Request.CreateResponse(HttpStatusCode.BadRequest, err);
        //        }
        //        if (string.IsNullOrEmpty(bi.billno) || string.IsNullOrEmpty(bi.bill_notes))
        //        {
        //            var message = string.Format("Bill number and notes are required");
        //            HttpError err = new HttpError(message);
        //            return Request.CreateResponse(HttpStatusCode.BadRequest, err);
        //        }

        //        bool billNoExists = _unitofWork.BillInfo.BillNoExists(bi.billno.Trim(), bi.VendorName.Trim());
        //        if (billNoExists)
        //        {
        //            var message = ($"Bill No.{bi.billno} from Vendor'{bi.VendorName}' already exists!");
        //            HttpError err = new HttpError(message);
        //            return Request.CreateResponse(HttpStatusCode.BadRequest, err);
        //        }

        //        var Session = HttpContext.Current.Session;
        //        foreach (var itm1 in bi.poList.Where(x => !string.IsNullOrEmpty(x.productname) && x.rate > 0))
        //        {
        //            var serialstatus = CodeService.GetSerialStatusForProduct(itm1.productname);
        //            var TempPurId = CodeService.GetTempPurchaseIdForProduct(itm1.productname);
        //            temppId = TempPurId;
        //            var TempPurOtherId = CodeService.GetTempPurchaseOtherId(TempPurId);
        //            if (serialstatus == true && TempPurOtherId == 0)
        //            {
        //                var message = string.Format("Please Give Serial No For Serialized Product");
        //                HttpError err = new HttpError(message);
        //                return Request.CreateResponse(HttpStatusCode.NotFound, err);
        //            }

        //            if (TempPurId > 0 && serialstatus == false)
        //            {
        //                _unitofWork.TempPurchase.Delete(TempPurId);
        //                _unitofWork.Save();
        //            }
        //        }

        //        if (bi.poList.Count > 1)
        //        {
        //            using (TransactionScope Scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        //            {
        //                //mapped directly to know the current bill_id
        //                var billInfo = Mapper.Convert(bi);

        //                billInfo.bill_type = "p";
        //                billInfo.entered_date = DateTime.Now;
        //                billInfo.entered_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
        //                billInfo.Status = "Direct P.O Request";
        //                billInfo.branch_id = bi.branch_id;
        //                billInfo.forwarded_to = bi.forwarded_to;
        //                billInfo.bill_amount = bi.bill_amount;
        //                billInfo.vat_amt = bi.vat_amt;
        //                billInfo.VendorName = bi.VendorName;
        //                billInfo.bill_notes = bi.bill_notes;
        //                billInfo.billno = bi.billno;
        //                billInfo.bill_date = DateTime.Now;
        //                _unitofWork.BillInfo.Insert(billInfo);
        //                _unitofWork.Save();

        //                var billId = billInfo.bill_id;//Fetching just inserted bill id

        //                var temp = _unitofWork.TempPurchase.GetById(temppId);
        //                if (temp == null)
        //                {

        //                }
        //                else
        //                {

        //                    temp.bill_id = billId;
        //                    _unitofWork.TempPurchase.Update(temp);
        //                    _unitofWork.Save();
        //                }


        //                //insert purchase order
        //                var po = new PurchaseOrderVM();
        //                foreach (var itm in bi.poList.Where(x => x.productname != "" && x.rate > 0))
        //                {
        //                    var pid = CodeService.GetProductIdFromProductName(itm.productname.Trim());
        //                    po.order_message_id = CodeService.GetPurchaseOrderMessageIdforpurchaseorder();
        //                    po.product_code = pid;
        //                    po.qty = itm.qty;
        //                    po.rate = itm.rate;
        //                    po.amount = itm.qty * itm.rate;
        //                    po.Received_Qty = itm.qty;
        //                    po.bill_id = billId;
        //                    _unitofWork.PurchaseOrder.Insert(po);
        //                }
        //                // Insert Notification and send Email
        //                var imsn = new InNotificationsVM();
        //                imsn.CreatedDate = DateTime.Now;
        //                imsn.Subject = " Direct P.O. Approval Request";
        //                imsn.Forwardedby = int.Parse(billInfo.entered_by);
        //                imsn.Forwardedto = (int)bi.forwarded_to;
        //                imsn.URL = "/ApproveDirectPurchaseOrder/Index" + "/" + billId;
        //                imsn.SpecialId = billId;
        //                imsn.Status = false;
        //                _unitofWork.InNotifications.Insert(imsn);
        //                // _unitofWork.Save();

        //                //var email = CodeService.GetOfficialEmail(int.Parse(bi.forwarded_to.ToString()));
        //                //if (!string.IsNullOrEmpty(email))
        //                //{
        //                //    try
        //                //    {
        //                //        var touser = CodeService.GetEmployeeFirstName(int.Parse(bi.forwarded_to.ToString()));
        //                //        var fromuser = CodeService.GetEmployeeFullName(int.Parse(billInfo.entered_by.ToString()));
        //                //        await EmailService.SendMailAsync(email, "Requisiton Sent",
        //                //            EmailService.EmailMessage.ApprovedRequest(touser, fromuser));
        //                //    }
        //                //    catch (Exception ex)
        //                //    {
        //                //        throw new Exception(ex.Message);
        //                //    }
        //                //}

        //                _unitofWork.Save();
        //                Scope.Complete();

        //                var newUrl = this.Url.Link("Default", new
        //                {
        //                    Controller = "DirectPurchaseOrder",
        //                    Action = "Create"
        //                });

        //                var message = string.Format("Successfully Requested and Sent for approval");

        //                return Request.CreateResponse(HttpStatusCode.OK, new { Success = true, RedirectUrl = newUrl, VoucherNo = message });
        //            }
        //        }
        //        else
        //        {
        //            var message = string.Format("At least one valid product must be included in the purchase order.");
        //            HttpError err = new HttpError(message);
        //            return Request.CreateResponse(HttpStatusCode.BadRequest, err);
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.InternalServerError, $"An unexpected error occurred. Please try again later.");
        //    }
        //}

        //        [HttpPost]
        //        public HttpResponseMessage PurchaseVoucherCreate(BillInfoVM bi)
        //        {
        //            if (bi.billno != "" && bi.bill_notes != "")
        //            {
        //                bool billNoExists = _unitofWork.BillInfo.BillNoExists(bi.billno.Trim());
        //                if (billNoExists)
        //                {
        //                    var mes = string.Format($"Bill No. {bi.billno} already exists!");
        //                    HttpError err = new HttpError(mes);
        //                    return Request.CreateResponse(HttpStatusCode.NotFound, err);
        //                }
        //                var Session = HttpContext.Current.Session;
        //                foreach (var itm1 in bi.poList.Where(x => x.productname != "" && x.rate > 0))
        //                {
        //                    var serialstatus = CodeService.GetSerialStatusForProduct(itm1.productname);
        //                    var TempPurId = CodeService.GetTempPurchaseIdForProduct(itm1.productname);
        //                    var TempPurOtherId = CodeService.GetTempPurchaseOtherId(TempPurId);
        //                    //  var data = _unitofWork.TempPurchaseOther.GetById(TempPurId);
        //                    if (serialstatus == true && TempPurOtherId == 0)
        //                    {
        //                        var message = string.Format("Please Give Serial No For Serialized Product");
        //                        HttpError err = new HttpError(message);
        //                        return Request.CreateResponse(HttpStatusCode.NotFound, err);
        //                    }
        //                    if (TempPurId > 0 && serialstatus == false)
        //                    {
        //                        _unitofWork.TempPurchase.Delete(TempPurId);
        //                        _unitofWork.Save();
        //                    }
        //                }
        //                try
        //                {
        //                    if (bi.poList.Count > 1)
        //                    {
        //                        using (TransactionScope Scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        //                        {
        //                            bi.bill_type = "p";
        //                            bi.entered_date = DateTime.Now;
        //                            bi.entered_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
        //                            _unitofWork.BillInfo.Insert(bi);
        //                            _unitofWork.Save();

        //                            //get billid
        //                            var billid = CodeService.GetBillInfoId();
        //                            var inpurchase = new InPurchaseVM();
        //                            foreach (var itm in bi.poList.Where(x => x.product_code > 0 && x.rate > 0))
        //                            {
        //                                inpurchase.bill_id = billid;
        //                                inpurchase.prod_code = itm.product_code;
        //                                inpurchase.p_qty = itm.qty;
        //                                inpurchase.p_rate = itm.rate;
        //                                inpurchase.p_stk_remain = itm.qty;
        //                                inpurchase.entered_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
        //                                inpurchase.entered_date = DateTime.Now.Date;
        //                                inpurchase.branch_id = Convert.ToInt32(Session["BranchId"]);
        //                                _unitofWork.InPurchase.Insert(inpurchase);
        //                                _unitofWork.Save();

        //                                var serialstatus = CodeService.GetSerialStatusForProductById(itm.product_code);
        //                                var purid = CodeService.GetPurchaseId();
        //                                var branchid = Convert.ToInt32(Session["BranchId"]);
        //                                if (serialstatus == true)
        //                                {
        //                                    var tpdata = _dapperrepo.GetTempPurchaseDataByTPO(itm.product_code);
        //                                    var tpodata = _dapperrepo.GetTempPurchaseOtherDataByTPO(tpdata);
        //                                    int snfrom = Convert.ToInt32(tpodata.sn_from);
        //                                    int snto = Convert.ToInt32(tpodata.sn_to);
        //                                    var spsdata = new SerialProductStockVM();
        //                                    _dapperrepo.InsertSerialProductStockPurchaseVoucher(branchid, itm.product_code, purid, snfrom, snto);
        //                                    var inpurdata = _dapperrepo.getInPurchaseDataByPIdAndBillId(itm.product_code, billid);
        //                                    inpurdata.p_sn_from = snfrom.ToString();
        //                                    inpurdata.p_sn_to = snto.ToString();
        //                                    _unitofWork.InPurchase.Update(inpurdata);

        //                                    var tpdataremove = _unitofWork.TempPurchase.GetById(tpdata);
        //                                    _unitofWork.TempPurchase.Delete(tpdataremove.id);

        //                                    var tpodataremove = _unitofWork.TempPurchaseOther.GetById(tpdata);
        //                                    _unitofWork.TempPurchaseOther.Delete(tpdataremove.id);
        //                                }
        //                                // in branch update
        //                                var inbranch = _unitofWork.InBranchAssign.GetItemById(itm.product_code, branchid);
        //                                if (inbranch == null)
        //                                {
        //                                    var messagea = string.Format("Branch not Assigned for " + itm.productname + "");
        //                                    HttpError err = new HttpError(messagea);
        //                                    return Request.CreateResponse(HttpStatusCode.NotFound, err);
        //                                }
        //                                inbranch.stock_in_hand = inbranch.stock_in_hand + itm.qty;
        //                                _unitofWork.InBranchAssign.Update(inbranch);
        //                                _unitofWork.Save();
        //                            }
        //                            //_unitofWork.Save();
        //                            var VoucherNo = _dapperrepo.getVoucherNo();
        //                            _dapperrepo.UpdateVoucherNo();

        //                            Scope.Complete();
        //                            var newUrl = this.Url.Link("Default", new
        //                            {
        //                                Controller = "PurchaseVoucher",
        //                                Action = "Create"
        //                            });
        //                            var message = string.Format("Voucher Save Successfully, Voucher No : " + VoucherNo + "");
        //                            return Request.CreateResponse(HttpStatusCode.OK, new { Success = true, RedirectUrl = newUrl, VoucherNo = message });
        //                        }
        //                    }
        //                    else
        //                    {
        //                        return Request.CreateResponse(HttpStatusCode.NotFound, "Failed...");
        //                    }
        //                }
        //#pragma warning disable CS0168 // The variable 'Ex' is declared but never used
        //                catch (Exception Ex)
        //#pragma warning restore CS0168 // The variable 'Ex' is declared but never used
        //                {

        //                    throw;
        //                }
        //            }
        //            else
        //            {
        //                var message = string.Format("Bill No. and Bill Notes are required.");
        //                HttpError err = new HttpError(message);
        //                return Request.CreateResponse(HttpStatusCode.NotFound, err);
        //            }
        //        }

        [HttpPost]
        public async Task<HttpResponseMessage> PurchaseVoucherCreate(BillInfoVM bi)
        {
            try
            {
                int temppId = 0;
                if (string.IsNullOrEmpty(bi.billno) || string.IsNullOrEmpty(bi.bill_notes))
                {
                    var message = string.Format("Bill number and notes are required");
                    HttpError err = new HttpError(message);
                    return Request.CreateResponse(HttpStatusCode.BadRequest, err);
                }

                bool billNoExists = _unitofWork.BillInfo.BillNoExists(bi.billno.Trim(), bi.VendorName.Trim());
                if (billNoExists)
                {
                    var message = ($"Bill No.{bi.billno} from Vendor'{bi.VendorName}' already exists!");
                    HttpError err = new HttpError(message);
                    return Request.CreateResponse(HttpStatusCode.BadRequest, err);
                }

                var Session = HttpContext.Current.Session;
                foreach (var itm1 in bi.poList.Where(x => !string.IsNullOrEmpty(x.productname) && x.rate > 0))
                {
                    var serialstatus = CodeService.GetSerialStatusForProduct(itm1.productname);
                    var TempPurId = CodeService.GetTempPurchaseIdForProduct(itm1.productname);
                    temppId = TempPurId;
                    var TempPurOtherId = CodeService.GetTempPurchaseOtherId(TempPurId);
                    if (serialstatus == true && TempPurOtherId == 0)
                    {

                        var message = string.Format("Please Give Serial No For Serialized Product");
                        HttpError err = new HttpError(message);
                        return Request.CreateResponse(HttpStatusCode.NotFound, err);
                    }
                    if (TempPurId > 0 && serialstatus == false)
                    {
                        _unitofWork.TempPurchase.Delete(TempPurId);
                        _unitofWork.Save();
                    }
                }

                if (bi.poList.Count > 1)
                {
                    using (TransactionScope Scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {

                        var VendorId = CodeService.GetVendorIdFromVendorName(bi.VendorName);


                        //mapped directly to know the current bill_id
                        var billInfo = Mapper.Convert(bi);
                        billInfo.party_code = VendorId.ToString();
                        billInfo.bill_type = "p";
                        billInfo.entered_date = DateTime.Now;
                        billInfo.entered_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
                        billInfo.Status = "PurchaseVoucher Request";
                        billInfo.branch_id = bi.branch_id;
                        billInfo.forwarded_to = bi.forwarded_to;
                        billInfo.bill_amount = bi.bill_amount;
                        billInfo.vat_amt = bi.vat_amt;
                        billInfo.VendorName = bi.VendorName;
                        billInfo.bill_notes = bi.bill_notes;
                        billInfo.billno = bi.billno;
                        billInfo.bill_date = DateTime.Now;
                        _unitofWork.BillInfo.Insert(billInfo);
                        _unitofWork.Save();

                        var billId = billInfo.bill_id; // recently added bill_id fetching

                        foreach (var itm in bi.poList.Where(x => !string.IsNullOrEmpty(x.productname) && x.rate > 0))
                        {
                            var TempPurId = CodeService.GetTempPurchaseIdForProduct(itm.productname);  
                            var temp = _unitofWork.TempPurchase.GetById(TempPurId);
                            if (temp != null)
                            {
                                temp.bill_id = billId; 
                                _unitofWork.TempPurchase.Update(temp); 
                                _unitofWork.Save(); 
                            }
                        }
                        //insert purchase order
                        foreach (var itm in bi.poList.Where(x => !string.IsNullOrEmpty(x.productname) && x.rate > 0))
                        {
                            var pid = CodeService.GetProductIdFromProductName(itm.productname.Trim());
                            var po = new PurchaseOrderVM
                            {
                                order_message_id = CodeService.GetPurchaseOrderMessageIdforpurchaseorder(),
                                product_code = pid,
                                qty = itm.qty,
                                rate = itm.rate,
                                amount = itm.qty * itm.rate,  
                                Received_Qty = itm.qty,
                                bill_id = billId
                            };

                            _unitofWork.PurchaseOrder.Insert(po);  
                        }
                        // Insert Notification and send Email
                        var imsn = new InNotificationsVM();
                        imsn.CreatedDate = DateTime.Now;
                        imsn.Subject = " Purchase Voucher Approval Request";
                        imsn.Forwardedby = int.Parse(billInfo.entered_by);
                        imsn.Forwardedto = (int)bi.forwarded_to;
                        imsn.URL = "/ApprovePurchaseVoucher/Index" + "/" + billId;
                        imsn.SpecialId = billId;
                        imsn.Status = false;
                        _unitofWork.InNotifications.Insert(imsn);
                 

                        //var email = CodeService.GetOfficialEmail(int.Parse(bi.forwarded_to.ToString()));
                        //if (!string.IsNullOrEmpty(email))
                        //{
                        //    try
                        //    {
                        //        var touser = CodeService.GetEmployeeFirstName(int.Parse(bi.forwarded_to.ToString()));
                        //        var fromuser = CodeService.GetEmployeeFullName(int.Parse(billInfo.entered_by.ToString()));
                        //        await EmailService.SendMailAsync(email, "Purchase Voucher Approval Requisiton Sent",
                        //            EmailService.EmailMessage.ApprovedRequest(touser, fromuser));
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        throw new Exception(ex.InnerException.Message);
                        //    }
                        //}

                        await _unitofWork.SaveAsync();
                        Scope.Complete();

                        var newUrl = this.Url.Link("Default", new
                        {
                            Controller = "PurchaseVoucher",
                            Action = "Create"
                        });

                        var message = string.Format("Successfully Requested and Sent for approval");

                        return Request.CreateResponse(HttpStatusCode.OK, new { Success = true, RedirectUrl = newUrl, VoucherNo = message });
                    }
                }
                else
                {
                    var message = string.Format("At least one valid product must be included in the purchase order.");
                    HttpError err = new HttpError(message);
                    return Request.CreateResponse(HttpStatusCode.BadRequest, err);
                }
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, $"An unexpected error occurred. Please try again later.");
            }
        }



        [HttpPost]
        public HttpResponseMessage DirectPurchaseFromDirectDispatch(BillInfoVM bi)
        {
            if (bi.vendor_code != 0)
            {
                if (bi.billno != "" && bi.bill_notes != "")
                {
                    var Session = HttpContext.Current.Session;
                    try
                    {
                        var purcount = bi.poList.Where(x => x.IsRowCheck == true).Count();
                        if (purcount > 0)
                        {
                            if (bi.poList.Count > 1)
                            {
                                using (TransactionScope Scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                                {
                                    bi.bill_type = "p";
                                    bi.entered_date = DateTime.Now;
                                    bi.entered_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
                                    _unitofWork.BillInfo.Insert(bi);
                                    _unitofWork.Save();

                                    //get billid
                                    var billid = CodeService.GetBillInfoId();
                                    var inpurchase = new InPurchaseVM();
                                    foreach (var itm in bi.poList.Where(x => x.product_code > 0 && x.rate > 0 && x.IsRowCheck == true))
                                    {
                                        inpurchase.bill_id = billid;
                                        inpurchase.prod_code = itm.product_code;
                                        inpurchase.p_qty = itm.qty;
                                        inpurchase.p_rate = itm.rate;
                                        inpurchase.p_stk_remain = itm.qty;
                                        inpurchase.entered_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
                                        inpurchase.entered_date = DateTime.Now.Date;
                                        inpurchase.branch_id = Convert.ToInt32(Session["BranchId"]);
                                        _unitofWork.InPurchase.Insert(inpurchase);
                                        _unitofWork.Save();

                                        var branchid = Convert.ToInt32(Session["BranchId"]);
                                        // in branch update
                                        var inbranch = _unitofWork.InBranchAssign.GetItemById(itm.product_code, branchid);
                                        inbranch.stock_in_hand = inbranch.stock_in_hand + itm.qty;
                                        _unitofWork.InBranchAssign.Update(inbranch);
                                        _unitofWork.Save();
                                    }
                                    //_unitofWork.Save();
                                    var VoucherNo = _dapperrepo.getVoucherNo();
                                    _dapperrepo.UpdateVoucherNo();

                                    Scope.Complete();
                                    var newUrl = this.Url.Link("Default", new
                                    {
                                        Controller = "DirectDispatchForNewBranch",
                                        Action = "Create"
                                    });
                                    var message = string.Format("Voucher Save Successfully, Voucher No : " + VoucherNo + "");
                                    return Request.CreateResponse(HttpStatusCode.OK, new { Success = true, RedirectUrl = newUrl, VoucherNo = message });
                                }
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.NotFound, "Failed...");
                            }
                        }
                        else
                        {
                            var message = string.Format("Please Select At Least One Item to Purchase.");
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
                else
                {
                    var message = string.Format("Bill No. and Bill Notes are required.");
                    HttpError err = new HttpError(message);
                    return Request.CreateResponse(HttpStatusCode.NotFound, err);
                }
            }
            else
            {
                var message = string.Format("Please Select Vendor.");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
        }

        [HttpPost]
        public HttpResponseMessage PurchaseReturnCreate(InPurchaseReturnVM inpurreturn)
        {
            var Session = HttpContext.Current.Session;
            var vendorid = CodeService.GetVendorIdFromVendorName(inpurreturn.VendorName.Trim());
            try
            {
                using (TransactionScope Scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    //purchasereturn insert
                    var compshortname = CodeService.GetCompanyShortName();
                    var fiscalyear = CodeService.GetFiscalYear();
                    var purchasereturnid = CodeService.GetPurchaseReturnId();
                    inpurreturn.PR_No = compshortname + "-" + "PR" + "-" + fiscalyear + "-" + purchasereturnid;
                    inpurreturn.GrandTotal = inpurreturn.InPurRetDetailList.Sum(x => x.Amount);
                    inpurreturn.Bill_No = inpurreturn.Bill_No.Trim();
                    inpurreturn.Vendor_Id = vendorid;
                    inpurreturn.Narration = inpurreturn.Narration.Trim();
                    inpurreturn.CreatedBy = Convert.ToString(_unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name);
                    inpurreturn.CreatedDate = DateTime.Now;
                    _unitofWork.PurchaseReturn.Insert(inpurreturn);
                    _unitofWork.Save();

                    //purchasereturndetails insert
                    var inpurretdet = new InPurchaseReturnDetailsVM();
                    foreach (var item in inpurreturn.InPurRetDetailList.Where(x => x.Qty > 0))
                    {
                        inpurretdet.PR_Id = CodeService.GetPurchaseReturnForDetailsId();
                        var pid = CodeService.GetProductIdFromProductName(item.ProductName.Trim());
                        inpurretdet.ReturnQty = item.ReturnQty;
                        if (inpurreturn.ReturnAll == true)
                        {
                            inpurretdet.ProductId = pid;
                            inpurretdet.Qty = item.Qty;
                            inpurretdet.Rate = item.Rate;
                            inpurretdet.Amount = item.Rate * item.Qty;
                        }
                        else
                        {
                            inpurretdet.ProductId = pid;
                            inpurretdet.Qty = item.ReturnQty;
                            inpurretdet.Rate = item.Rate;
                            inpurretdet.Amount = item.Rate * item.ReturnQty;
                        }
                        _unitofWork.PurchaseReturnDetails.Insert(inpurretdet);
                        _unitofWork.Save();

                        //-------------Update In_Branch and In_Purchase---------------//
                        if (inpurreturn.ReturnAll == false)
                        {
                            var inpurchase = _unitofWork.InPurchase.GetByBillIdandPid(pid, inpurreturn.Bill_Id);
                            inpurchase.p_qty = (inpurchase.p_qty - item.ReturnQty);
                            inpurchase.p_stk_remain = (inpurchase.p_stk_remain - item.ReturnQty);
                            _unitofWork.InPurchase.Update(inpurchase);
                            _unitofWork.Save();

                            var inbranch = _unitofWork.InBranchAssign.GetItemById(pid, inpurchase.branch_id);
                            inbranch.stock_in_hand = (inbranch.stock_in_hand - item.ReturnQty);
                            _unitofWork.InBranchAssign.Update(inbranch);
                            _unitofWork.Save();
                        }
                    }
                    Scope.Complete();
                    var newUrl = this.Url.Link("Default", new
                    {
                        Controller = "PurchasePendingBills",
                        Action = "Index"
                    });
                    return Request.CreateResponse(HttpStatusCode.OK, new { Success = true, RedirectUrl = newUrl });
                }
            }

#pragma warning disable CS0168 // The variable 'Ex' is declared but never used
            catch (Exception Ex)
#pragma warning restore CS0168 // The variable 'Ex' is declared but never used
            {
                throw;
            }
        }
    }
}
