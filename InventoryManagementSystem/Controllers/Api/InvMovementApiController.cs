using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Helper;
using InventoryManagementSystem.Infrastructure.DapperRepo;
using InventoryManagementSystem.Infrastructure.Filter;
using InventoryManagementSystem.Infrastructure.Interface;
using InventoryManagementSystem.Infrastructure.Repository;
using InventoryManagementSystem.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using System.Web.Http;

namespace InventoryManagementSystem.Controllers.Api
{
    [UserAuthorize]
    public class InvMovementApiController : ApiController
    {
        private IUnitOfWork _unitofWork;
        private DapperRepoServices _dapperrepo;
        public InvMovementApiController()
        {
            _unitofWork = new UnitOfWork();
            _dapperrepo = new DapperRepoServices();
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Create(InRequisitionMessageVM inreqmes)//place requisition
        {
            var Session = HttpContext.Current.Session;
            var branchid = Convert.ToInt32(Session["BranchId"]);
            try
            {
                if (ModelState.IsValid)
                {
                    if (inreqmes.Requ_Message != "")
                    {
                        if (inreqmes.priority == "Normal")
                        {
                            inreqmes.priority = "N";
                        }
                        else if (inreqmes.priority == "High")
                        {
                            inreqmes.priority = "H";
                        }
                        else if (inreqmes.priority == "Low")
                        {
                            inreqmes.priority = "L";
                        }
                        if (inreqmes.inreqList.Count >= 1)
                        {
                            using (TransactionScope Scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                            {
                                //inrequisitionmessage insert
                                var compshortname = CodeService.GetCompanyShortName();
                                var fiscalyear = CodeService.GetFiscalYear();
                                var requisitionmessageid = CodeService.GetRequisitionMessageId();
                                string prefix = "RN";
                                switch (inreqmes.ProdGroupId)
                                {
                                    case 2:
                                        prefix = "SNP";
                                        break;
                                    case 3:
                                        prefix = "SPI";
                                        break;
                                    case 229:
                                        prefix = "SS";
                                        break;
                                    case 3554:
                                        prefix = "SLH";
                                        break;
                                    default:
                                        prefix = "RN";
                                        break;
                                }
                                inreqmes.status = "Recommended";
                                inreqmes.Req_no = $"{compshortname}-{prefix}-{fiscalyear}-{requisitionmessageid}";
                                inreqmes.Requ_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
                                inreqmes.Requ_date = DateTime.Now.Date;
                                inreqmes.recommed_date = DateTime.Now.Date;
                                //inreqmes.recommed_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
                                inreqmes.recommed_message = "Recommended";
                                inreqmes.branch_id = branchid;
                                inreqmes.dept_id = Convert.ToInt32(Session["DepartmentId"]);
                                inreqmes.IS_SCHEDULE = "Yes";
                                _unitofWork.InRequisitionMessage.Insert(inreqmes);
                                _unitofWork.Save();

                                //inrequisition insert
                                var reqmesid = CodeService.GetInRequisitionMessageId();
                                var inreq = new InRequisitionVM();
                                foreach (var itm in inreqmes.inreqList.Where(x => x.item > 0))
                                {
                                    inreq.unit = itm.unit;
                                    inreq.quantity = itm.quantity;
                                    inreq.item = itm.item;
                                    inreq.Requistion_message_id = reqmesid;
                                    inreq.Approved_Quantity = itm.quantity;
                                    _unitofWork.InRequisition.Insert(inreq);
                                    _unitofWork.Save();

                                    //inrequisitiondetail insert
                                    var inreqdet = new InRequisitionDetailVM();
                                    inreqdet.item = itm.item;
                                    inreqdet.quantity = itm.quantity;
                                    inreqdet.unit = CodeService.GetUnitForProductName(itm.item);
                                    inreqdet.Requistion_message_id = reqmesid;
                                    inreqdet.Approved_Quantity = itm.quantity;
                                    inreqdet.Delivered_Quantity = itm.quantity;
                                    inreqdet.Received_Quantity = 0;
                                    inreqdet.remain = 0;
                                    inreqdet.session_id = "ysipwan4vrrcqxggx5eeyitd";//need to generate
                                    _unitofWork.InRequisitionDetail.Insert(inreqdet);
                                    _unitofWork.Save();
                                }

                                //UserNotification and Email
                                var imsn = new InNotificationsVM();
                                imsn.CreatedDate = DateTime.Now;
                                imsn.Subject = "Place Requisition Approval Request";
                                imsn.Forwardedby = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;// int.Parse(pomh.from_user);
                                imsn.Forwardedto = inreqmes.recommed_by;
                                imsn.URL = "/ApproveRequisition/Index" + "/" + reqmesid;
                                imsn.SpecialId = reqmesid;
                                imsn.Status = false;
                                _unitofWork.InNotifications.Insert(imsn);
                                _unitofWork.Save();

                                var email = CodeService.GetOfficialEmail(inreqmes.recommed_by);
                                var fromuser = CodeService.GetEmployeeFullName(_unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name);
                                var touser = CodeService.GetEmployeeFirstName(inreqmes.recommed_by);
                                var approveddata = _unitofWork.InRequisition.GetItemById(reqmesid);
                                var tdqty = approveddata.Sum(x => x.Approved_Quantity);
                                int sn = 1;
                                string tabledata = "<thead><tr><th>SN</th> <th>Product Name </th> <th>UNIT </th> <th>Requested Qty</th> </tr> </thead><tbody>";
                                foreach (var item in approveddata.OrderBy(x => x.ProductName))
                                {
                                    tabledata += @"<tr>
                                    <td style='border:1px solid #000; padding:10px'>" + sn + @"</td>
                                    <td style='border:1px solid #000; padding:10px'>" + item.ProductName + @"</td>
                                    <td style='border:1px solid #000; padding:10px'>" + item.unit + @"</td>
                                    <td style='border:1px solid #000; padding:10px; text-align:center;'>" + item.Approved_Quantity + @"</td>
                                    </tr>";
                                    sn++;
                                }
                                string HtmlMail = @"<html><table border='1' cellspacing='0' cellpadding='0' >" + tabledata + "</tbody>" + @"<tfoot>
                                                        <tr><td></td><td style='text-align:center; font-weight: bold'>" + "Total :" + @"</td ><td></td><td style='text-align:center; font-weight: bold'>" + tdqty + @"</td></ tr >
                                                        </ tfoot > </table></html>";

                                if (email != "")
                                {
                                    try
                                    {
                                        await EmailService.SendMailAsync(email, "Place Requisition Approval Request",
                                        EmailService.EmailMessage.ApprovalRequestOfProductDetails(touser, fromuser, HtmlMail, inreqmes.Requ_Message));
                                    }
                                    catch (Exception)
                                    {
                                        //wrong email
                                    }
                                }
                                //check inserted requisition item
                                var checkReqItemList = _unitofWork.InRequisition.GetItemById(reqmesid);
                                //now check duplicate item exists or not
                                var duplicateItems = checkReqItemList.GroupBy(item => item.item).Where(a => a.Count() > 1).Select(a => a.Key).ToList();
                                //now validate requisition items inserted and not duplicated.
                                if (reqmesid > 0 && checkReqItemList.Count() > 0 && !duplicateItems.Any())
                                {
                                    Scope.Complete();
                                    var newUrl = this.Url.Link("Default", new
                                    {
                                        Controller = "PlaceRequisition",
                                        Action = "Index"
                                    });
                                    return Request.CreateResponse(HttpStatusCode.OK, new { Success = true, RedirectUrl = newUrl });
                                }
                                else
                                {
                                    var message = string.Format("Please refresh page and try again.");
                                    HttpError err = new HttpError(message);
                                    return Request.CreateResponse(HttpStatusCode.NotFound, err);
                                }

                            }
                        }
                        else
                        {
                            var message = string.Format("Please add item to request!");
                            HttpError err = new HttpError(message);
                            return Request.CreateResponse(HttpStatusCode.NotFound, err);
                        }
                    }
                    else
                    {
                        var message = string.Format("Please Give Request Message.");
                        HttpError err = new HttpError(message);
                        return Request.CreateResponse(HttpStatusCode.NotFound, err);
                    }
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Failed...");
            }
            catch (Exception Ex)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<HttpResponseMessage> ApproveRequisition(InRequisitionMessageVM inreqmes)//approve requisition
        {
            var Session = HttpContext.Current.Session;
            try
            {
                if (inreqmes.Approver_message != "")
                {
                    if (inreqmes.inreqList.Count >= 1)
                    {
                        using (TransactionScope Scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                        {
                            //inrequisitionmessage update
                            var Item = _unitofWork.InRequisitionMessage.GetById(inreqmes.id);
                            Item.Approved_Date = DateTime.Now.Date;
                            Item.Approver_id = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
                            Item.status = "Approved";
                            Item.Approver_message = inreqmes.Approver_message;
                            _unitofWork.InRequisitionMessage.Update(Item);
                            _unitofWork.Save();

                            //inrequisition update
                            foreach (var itm in inreqmes.inreqList.Where(x => x.item > 0))
                            {
                                var data = _unitofWork.InRequisition.GetByMessageIdandProductId(inreqmes.id, itm.item);
                                data.Approved_Quantity = itm.Approved_Quantity;
                                _unitofWork.InRequisition.Update(data);
                                _unitofWork.Save();

                                //inrequisitiondetail update
                                var data1 = _unitofWork.InRequisitionDetail.GetByMessageIdandProductId(inreqmes.id, itm.item);
                                data1.Approved_Quantity = itm.Approved_Quantity;
                                _unitofWork.InRequisitionDetail.Update(data1);
                                _unitofWork.Save();
                            }

                            var notificationdata = _unitofWork.InNotifications.GetByPOMId(inreqmes.id);
                            if (notificationdata != null)
                            {
                                notificationdata.Status = true;
                                _unitofWork.InNotifications.Update(notificationdata);
                            }
                            _unitofWork.Save();

                            if (Item.Forwarded_To == 12)
                            {
                                var email = CodeService.GetOfficialEmail(1211); //1211=binod thapa
                                var fromuser = CodeService.GetEmployeeFullName(Item.Requ_by);
                                var touser = CodeService.GetEmployeeFirstName(1211);
                                var branchname = CodeService.GetBranchNameFromEmployee(Item.Requ_by);
                                var approveddata = _unitofWork.InRequisition.GetItemById(inreqmes.id);
                                var tdqty = approveddata.Sum(x => x.Approved_Quantity);
                                int sn = 1;
                                string tabledata = "<thead><tr><th>SN</th> <th>Product Name </th> <th>UNIT </th> <th>Requested Qty</th> </tr> </thead><tbody>";
                                foreach (var item in approveddata.OrderBy(x => x.ProductName))
                                {
                                    tabledata += @"<tr>
                                    <td style='border:1px solid #000; padding:10px'>" + sn + @"</td>
                                    <td style='border:1px solid #000; padding:10px'>" + item.ProductName + @"</td>
                                    <td style='border:1px solid #000; padding:10px'>" + item.unit + @"</td>
                                    <td style='border:1px solid #000; padding:10px; text-align:center;'>" + item.Approved_Quantity + @"</td>
                                    </tr>";
                                    sn++;
                                }
                                string HtmlMail = @"<html><table border='1' cellspacing='0' cellpadding='0' >" + tabledata + "</tbody>" + @"<tfoot>
                                                        <tr><td></td><td style='text-align:center; font-weight: bold'>" + "Total :" + @"</td ><td></td><td style='text-align:center; font-weight: bold'>" + tdqty + @"</td></ tr >
                                                        </ tfoot > </table></html>";

                                if (email != "")
                                {
                                    try
                                    {
                                        await EmailService.SendMailAsync(email, "Items Requested",
                                   EmailService.EmailMessage.ApprovedProductDetails(touser, fromuser, HtmlMail, branchname, Item.Requ_Message, Item.Approver_message));
                                    }
                                    catch (Exception ex)
                                    {
                                        throw ex;
                                    }

                                }
                            }

                            Scope.Complete();
                            var newUrl = this.Url.Link("Default", new
                            {
                                Controller = "ApproveRequisition",
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
                    var message = string.Format("Please Give Approve Message.");
                    HttpError err = new HttpError(message);
                    return Request.CreateResponse(HttpStatusCode.NotFound, err);
                }
            }
            catch (Exception Ex)
            {
                throw;
            }
        }




        [Authorize]
        [HttpPost]
        public async Task<HttpResponseMessage> DispatchRequisition([FromBody] InRequisitionMessageVM inreqmes)
        {
            var Session = HttpContext.Current.Session;
            foreach (var itm1 in inreqmes.indispList.Where(x => x.productname != "" && x.dispatched_qty > 0 && x.IsRowCheck == true))
            {
                var serialstatus = CodeService.GetSerialStatusForProduct(itm1.productname.Trim());
                var data = CodeService.INRequisitionDetailOtherData(itm1.productname.Trim(), inreqmes.id);
                if (serialstatus == true && data == 0)
                {
                    var message = string.Format("Please Give Serial No. For Serialized Product.");
                    HttpError err = new HttpError(message);
                    return Request.CreateResponse(HttpStatusCode.NotFound, err);
                }
            }
            foreach (var itm1 in inreqmes.indispList.Where(x => x.productname != "" && x.dispatched_qty > 0 && x.IsRowCheck == true))
            {
                var bid = CodeService.getBranchIdFromInRequisitionMessage(inreqmes.id);
                var stockinhand = CodeService.GetStockInHand(itm1.productname.Trim(), bid/*Convert.ToInt32(Session["BranchId"])*/);
                if (itm1.dispatched_qty > stockinhand || stockinhand < 0)
                {
                    var message = string.Format("Insufficient Stock In Hand.");
                    HttpError err = new HttpError(message);
                    return Request.CreateResponse(HttpStatusCode.NotFound, err);
                }
            }
            try
            {
                if (inreqmes.Delivery_Message != "")
                {
                    if (inreqmes.indispList.Count >= 1)
                    {
                        var disptchcount = inreqmes.indispList.Where(x => x.IsRowCheck == true).Count();
                        if (disptchcount > 0)
                        {
                            using (TransactionScope Scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                            {
                                //inrequisitionmes update
                                var Item = _unitofWork.InRequisitionMessage.GetById(inreqmes.id);
                                Item.Delivered_Date = inreqmes.Delivered_Date;
                                Item.Delivery_Message = inreqmes.Delivery_Message;
                                Item.Delivered_By = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
                                Item.status = "Full Dispatched";
                                _unitofWork.InRequisitionMessage.Update(Item);
                                _unitofWork.Save();

                                //indispatchmessage insert
                                var indispatchmes = new InDispatchedMessageVM();
                                indispatchmes.req_id = inreqmes.id;
                                indispatchmes.dispatch_message = inreqmes.Delivery_Message;
                                indispatchmes.dispatched_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
                                indispatchmes.dispatched_date = inreqmes.Delivered_Date;
                                indispatchmes.status = "Not Acknowledge";
                                indispatchmes.stkFlag = "Y";
                                var insertedDispatch = _unitofWork.InDispatchMessage.InsertData(indispatchmes);

                                //indispatch insert
                                var indispatch = new InDispatchedVM();
                                // var disptmesid1 = CodeService.GetInDispatchMessageId();
                                var disptmesid = insertedDispatch.id;
                                //check dispatch list have any list or not.
                                var disptList = inreqmes.indispList.Where(x => x.product_id > 0 && x.dispatched_qty > 0 && x.IsRowCheck == true);
                                foreach (var itm in disptList)
                                {

                                    indispatch.dispatch_msg_id = disptmesid;
                                    indispatch.product_id = itm.product_id;
                                    indispatch.dispatched_qty = itm.dispatched_qty;
                                    indispatch.remain = itm.remain;
                                    indispatch.from_branch = Item.Forwarded_To;
                                    indispatch.to_branch = Item.branch_id;
                                    indispatch.rate = itm.p_rate;
                                    indispatch.dispatched_date = inreqmes.Delivered_Date;
                                    _unitofWork.InDispatch.Insert(indispatch);
                                    _unitofWork.Save();

                                    var serialstatus = CodeService.GetSerialStatusForProductById(itm.product_id);
                                    if (serialstatus == true)
                                    {
                                        var serialproductdata = new SerialProductTransferBranchVM();
                                        var sndata = _unitofWork.InRequisitionDetailOther.GetAllInRequisitionDetailOtherFromDetailIdandPurId(inreqmes.id, itm.product_id);
                                        foreach (var item in sndata)
                                        {
                                            serialproductdata.reqid = inreqmes.id;
                                            serialproductdata.sn_from = Convert.ToInt32(item.sn_from);
                                            serialproductdata.sn_to = Convert.ToInt32(item.sn_to);
                                            serialproductdata.fbranchid = Item.Forwarded_To;
                                            serialproductdata.tbranchid = Item.branch_id;
                                            serialproductdata.productid = itm.product_id;
                                            serialproductdata.qty = itm.dispatched_qty;
                                            serialproductdata.TransferDate = DateTime.Now;
                                            _unitofWork.SerialProductTransferBranch.Insert(serialproductdata);
                                            _unitofWork.Save();

                                            if (Item.branch_id == 12)//12==corporate office
                                            {
                                                _dapperrepo.DeleteSerialProductStockForSameBranchDispatch(Convert.ToInt32(item.sn_from), Convert.ToInt32(item.sn_to), Item.branch_id, itm.product_id);
                                            }
                                            else
                                            {
                                                _dapperrepo.UpdateSerialProductStockDispatchRequisition(inreqmes.id, itm.product_id, Convert.ToInt32(item.sn_from), Convert.ToInt32(item.sn_to));
                                            }
                                        }

                                        _dapperrepo.DeleteByReqMesId(inreqmes.id, itm.product_id);
                                    }
                                    if (Item.branch_id != Item.Forwarded_To || Item.branch_id == 12)
                                    {
                                        //in branch update for from branch
                                        var inbranchfrom = _unitofWork.InBranchAssign.GetItemById(itm.product_id, Item.Forwarded_To);
                                        if (inbranchfrom == null)
                                        {
                                            var messagea = string.Format("Branch not Assigned for " + itm.productname + "");
                                            HttpError err = new HttpError(messagea);
                                            return Request.CreateResponse(HttpStatusCode.NotFound, err);
                                        }
                                        inbranchfrom.stock_in_hand = (inbranchfrom.stock_in_hand >= itm.dispatched_qty) ? inbranchfrom.stock_in_hand - itm.dispatched_qty : 0;
                                        _unitofWork.InBranchAssign.Update(inbranchfrom);
                                        _unitofWork.Save();

                                        var inPurchaseData = _unitofWork.InPurchase.GetAllByBranchIdAndProdId(Item.Forwarded_To, itm.product_id);
                                        var remainQtyToDispatch = itm.dispatched_qty;

                                        foreach (var record in inPurchaseData)
                                        {
                                            var inPurchaseDataUpdate = _unitofWork.InPurchase.GetById(record.pur_id);
                                            var totalDispQty = record.p_stk_remain;

                                            if (remainQtyToDispatch == 0)
                                            {
                                                break;
                                            }

                                            if (totalDispQty >= remainQtyToDispatch)
                                            {
                                                inPurchaseDataUpdate.p_stk_remain -= remainQtyToDispatch;
                                                remainQtyToDispatch = 0;
                                            }
                                            else
                                            {
                                                inPurchaseDataUpdate.p_stk_remain = 0;
                                                remainQtyToDispatch -= totalDispQty;
                                            }

                                            _unitofWork.InPurchase.Update(inPurchaseDataUpdate);
                                            _unitofWork.Save();
                                        }
                                    }
                                    else // in this case will decrease only on acknowledge
                                    {
                                        //this is only for department update only,
                                        var inbranchfrom = _unitofWork.InBranchAssign.GetItemById(itm.product_id, Item.Forwarded_To);
                                        if (inbranchfrom == null)
                                        {
                                            var messagea = string.Format("Branch not Assigned for " + itm.productname + "");
                                            HttpError err = new HttpError(messagea);
                                            return Request.CreateResponse(HttpStatusCode.NotFound, err);
                                        }
                                        inbranchfrom.departmentid = inreqmes.dept_id;
                                        _unitofWork.InBranchAssign.Update(inbranchfrom);
                                        _unitofWork.Save();

                                        var inPurchaseData = _unitofWork.InPurchase.GetAllByBranchIdAndProdId(Item.Forwarded_To, itm.product_id);
                                        var totalStockRemain = 0;

                                        foreach (var record in inPurchaseData)
                                        {
                                            var inPurchaseDataUpdate = _unitofWork.InPurchase.GetById(record.pur_id);
                                            var dispatchedQty = itm.dispatched_qty;

                                            if (dispatchedQty >= record.p_stk_remain)
                                            {
                                                totalStockRemain += inPurchaseDataUpdate.p_stk_remain;

                                                if (totalStockRemain <= dispatchedQty)
                                                {
                                                    inPurchaseDataUpdate.departmentid = inreqmes.dept_id;
                                                    _unitofWork.InPurchase.Update(inPurchaseDataUpdate);
                                                    _unitofWork.Save();
                                                }
                                                else
                                                {
                                                    break;
                                                }
                                            }
                                            else
                                            {
                                                inPurchaseDataUpdate.departmentid = inreqmes.dept_id;
                                                _unitofWork.InPurchase.Update(inPurchaseDataUpdate);
                                                _unitofWork.Save();
                                                break;
                                            }
                                        }

                                        _dapperrepo.UpdateSerialProductStockDepartmentId(Item.branch_id, itm.product_id, inreqmes.id, inreqmes.dept_id);
                                    }
                                }
                                //UserNotification and Email
                                var imsn = new InNotificationsVM();
                                imsn.CreatedDate = DateTime.Now;
                                imsn.Subject = "Acknowledge Requisition";
                                imsn.Forwardedby = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;// int.Parse(pomh.from_user);
                                imsn.Forwardedto = Item.Requ_by;
                                imsn.URL = "/AcknowledgeRequisition/Index" + "/" + inreqmes.id;
                                imsn.SpecialId = inreqmes.id;
                                imsn.Status = false;
                                _unitofWork.InNotifications.Insert(imsn);
                                _unitofWork.Save();

                                // if (Item.branch_id != Item.Forwarded_To)

                                //email to dispatch and received
                                var receiveremail = CodeService.GetOfficialEmail(Item.Requ_by);
                                var senderemail = CodeService.GetOfficialEmail(_unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name);
                                var touser = CodeService.GetEmployeeFirstName(Item.Requ_by);
                                var fromuser = CodeService.GetEmployeeFullName(_unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name);
                                var branchname = CodeService.GetBranchNameFromEmployee(Item.Requ_by);
                                var dispatchdata = _unitofWork.InDispatch.GetAllByDispMesId(disptmesid);
                                var tdqty = dispatchdata.Sum(x => x.dispatched_qty);

                                int sn = 1;
                                string tabledata = "<thead><tr><th>SN</th> <th>Product Name </th> <th>UNIT </th> <th>Dispatched Qty</th> </tr> </thead><tbody>";
                                foreach (var item in dispatchdata.OrderBy(x => x.productname))
                                {
                                    tabledata += @"<tr>
                                    <td style='border:1px solid #000; padding:10px'>" + sn + @"</td>
                                    <td style='border:1px solid #000; padding:10px'>" + item.productname + @"</td>
                                    <td style='border:1px solid #000; padding:10px'>" + item.unit + @"</td>
                                    <td style='border:1px solid #000; padding:10px; text-align:center;'>" + item.dispatched_qty + @"</td>
                                    </tr>";
                                    sn++;
                                }
                                string HtmlMail = @"<html><table border='1' cellspacing='0' cellpadding='0' >" + tabledata + "</tbody>" + @"<tfoot>
                                                        <tr><td></td><td style='text-align:center; font-weight: bold'>" + "Total :" + @"</td ><td></td><td style='text-align:center; font-weight: bold'>" + tdqty + @"</td></ tr >
                                                        </ tfoot > </table></html>";

                                if (receiveremail != "")
                                {
                                    try
                                    {
                                        await EmailService.SendMailAsync(receiveremail, "Items Dispatched",
                                    EmailService.EmailMessage.ReceiverDispatchProductDetails(touser, fromuser, HtmlMail, inreqmes.Delivery_Message));
                                    }
                                    catch (Exception ex)
                                    {
                                        throw new Exception(ex.Message);
                                    }
                                }
                                if (senderemail != "")
                                {
                                    try
                                    {
                                        await EmailService.SendMailAsync(senderemail, "Items Dispatched",
                                        EmailService.EmailMessage.SenderDispatchProductDetails(touser, fromuser, HtmlMail, branchname, inreqmes.Delivery_Message));
                                    }
                                    catch (Exception ex)
                                    {
                                        throw new Exception(ex.Message);
                                    }
                                }

                                _unitofWork.Save();


                                //  _unitofWork.Save();

                                //last verification.
                                var details = _unitofWork.InRequisitionMessage.GetById(inreqmes.id);
                                //to check dispatch item inserted or not.
                                var checkInsertedDispatchItem = _unitofWork.InDispatch.GetAllByDispMesId(disptmesid);
                                //requisition Items
                                //var requisitionItems = _unitofWork.InRequisition.GetItemById(inreqmes.id);
                                var requisitionItems = _unitofWork.InRequisition.GetItemById(inreqmes.id);
                                // Get a list of item IDs from the requisition
                                var requisitionItemIds = requisitionItems.Select(item => item.item).ToList();
                                // Check for any dispatch item productId that does not exist in the requisition list
                                var invalidDispatchItems = checkInsertedDispatchItem
                                    .Where(dispatchItem => !requisitionItemIds.Contains(dispatchItem.product_id))
                                    .ToList();
                                //now check if any item dispatch quantity is greater than approved quantity
                                // Create a dictionary to map item ID to approved quantity from requisition items
                                var approvedQuantities = requisitionItems.ToDictionary(item => item.item, item => item.Approved_Quantity);
                                // Check for any dispatch item where the quantity is greater than the approved quantity
                                var invalidDispatchQuantities = checkInsertedDispatchItem
                                    .Where(dispatchItem =>
                                        approvedQuantities.ContainsKey(dispatchItem.product_id) &&
                                        dispatchItem.dispatched_qty > approvedQuantities[dispatchItem.product_id])
                                    .ToList();

                                //now check duplicate dispatch item exists or not
                                var duplicateDispatchItems = checkInsertedDispatchItem.GroupBy(item => item.product_id).Where(a => a.Count() > 1).Select(a => a.Key).ToList();
                                string reqInfo = $"Req_id={inreqmes.id}, req_branchId={details.branch_id}, To_branchid={details.Forwarded_To}, req_No={details.Req_no}";
                                string reqItemInfo = String.Join(",\n", requisitionItems.Select(item => $"{item.item}-{item.ProductName}-qty={item.quantity}-appr_qty={item.Approved_Quantity}"));
                                string disInfo = $"Req_id={indispatchmes.req_id}, Dis_id={disptmesid}";
                                string disITem = String.Join(",\n", disptList.Select(item => $"Dis_prod_code={item.product_id},{item.productname.Trim()},qty={item.dispatched_qty},frombranch={indispatch.from_branch},to_branch={indispatch.to_branch},dispatch_date={indispatch.dispatched_date}"));

                                string logMessage = $"{reqInfo} \n {reqItemInfo} \n ############################### \n{disInfo} \n {disITem} \n";

                                //now validate if item is dispatch according to requisition or not as well as if there is any item where dispatch qty is greater than approved quantity and no duplicate items.
                                if (checkInsertedDispatchItem.Count() > 0 && disptList.Count() == checkInsertedDispatchItem.Count()
                                    && !invalidDispatchItems.Any() && !invalidDispatchQuantities.Any() && !duplicateDispatchItems.Any())
                                {
                                    Scope.Complete();

                                    ErrorLogger.DispatchedLog("Success Inserted: \n" + logMessage);
                                    var newUrl = this.Url.Link("Default", new
                                    {
                                        Controller = "DispatchRequisition",
                                        Action = "Index"
                                    });
                                    return Request.CreateResponse(HttpStatusCode.OK, new { Success = true, RedirectUrl = newUrl });
                                }
                                else
                                {
                                    ErrorLogger.DispatchedLog("Failed Validation: \n" + logMessage);

                                    var message = string.Format("Please refresh page and try again.");
                                    HttpError err = new HttpError(message);
                                    return Request.CreateResponse(HttpStatusCode.NotFound, err);
                                }

                            }
                        }
                        else
                        {
                            var message = string.Format("Please Select At Least One Item To Dispatch.");
                            HttpError err = new HttpError(message);
                            return Request.CreateResponse(HttpStatusCode.NotFound, err);
                        }
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Failed...");
                    }
                }
                else
                {
                    var message = string.Format("Please Give Dispatch Message.");
                    HttpError err = new HttpError(message);
                    return Request.CreateResponse(HttpStatusCode.NotFound, err);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);

                if (ex.InnerException != null)
                {
                    throw new Exception($"{ex.Message} | Inner Exception: {ex.InnerException.Message}", ex.InnerException);
                }

                else
                {
                    throw;
                }
            }
        }




        [HttpPost]
        public HttpResponseMessage AcknowledgeRequisition(InRequisitionMessageVM inreqmes)
        {
            var Session = HttpContext.Current.Session;
            try
            {
                if (ModelState.IsValid)
                {
                    if (inreqmes.Acknowledged_Message != "")
                    {

                        if (inreqmes.inrecList.Count >= 1)
                        {
                            using (TransactionScope Scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                            {
                                //inrequisitionmes update
                                var Item = _unitofWork.InRequisitionMessage.GetById(inreqmes.id);
                                Item.Acknowledged_Date = DateTime.Now.Date;
                                Item.Acknowledged_By = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
                                Item.Acknowledged_Message = inreqmes.Acknowledged_Message;
                                Item.status = "Full Acknowledged";
                                _unitofWork.InRequisitionMessage.Update(Item);
                                _unitofWork.Save();

                                //in dispatch message update
                                var dispatchItem = _unitofWork.InDispatchMessage.GetByInReqMesId(inreqmes.id);
                                dispatchItem.status = "Full Acknowledged";
                                _unitofWork.InDispatchMessage.Update(dispatchItem);
                                _unitofWork.Save();

                                //inreceivemessage insert
                                var inrecmes = new InReceivedMessageVM();
                                inrecmes.req_msg_id = inreqmes.id;
                                inrecmes.dis_msg_id = CodeService.GetInDispatchMessageIdFromInRequisitionMesId(inreqmes.id);
                                inrecmes.received_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
                                inrecmes.received_date = DateTime.Now.Date;
                                inrecmes.received_msg = inreqmes.Acknowledged_Message;
                                _unitofWork.InReceivedMessage.Insert(inrecmes);
                                _unitofWork.Save();

                                if (Item.branch_id != 12)//12==corporate office(999)
                                {
                                    foreach (var itm in inreqmes.inrecList.Where(x => x.product_id > 0))
                                    {
                                        if (Item.branch_id != Item.Forwarded_To)
                                        {
                                            //in branch update for to branch
                                            var inbranchto = _unitofWork.InBranchAssign.GetItemById(itm.product_id, Item.branch_id);
                                            if (inbranchto == null)
                                            {
                                                var messagea = string.Format("Branch not Assigned for " + CodeService.GetInProductAllName(itm.product_id) + "");
                                                HttpError err = new HttpError(messagea);
                                                return Request.CreateResponse(HttpStatusCode.NotFound, err);
                                            }
                                            inbranchto.stock_in_hand = inbranchto.stock_in_hand + itm.received_qty;
                                            _unitofWork.InBranchAssign.Update(inbranchto);

                                            var inPurchaseInsert = new InPurchaseVM();
                                            inPurchaseInsert.prod_code = itm.product_id;
                                            inPurchaseInsert.p_qty = itm.received_qty;
                                            inPurchaseInsert.p_rate = itm.p_rate;
                                            inPurchaseInsert.p_stk_remain = itm.received_qty;
                                            inPurchaseInsert.branch_id = Item.branch_id;
                                            inPurchaseInsert.entered_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
                                            inPurchaseInsert.entered_date = DateTime.Now.Date;
                                            inPurchaseInsert.req_id = inreqmes.id;
                                            _unitofWork.InPurchase.Insert(inPurchaseInsert);
                                            _unitofWork.Save();

                                            _dapperrepo.UpdateSerialProductStockBranchId(Item.branch_id, itm.product_id, inreqmes.id);

                                            //inreceived insert
                                            var inrec = new InReceivedVM();
                                            var recmesid = CodeService.GetInReceivedMessageId();
                                            inrec.received_msg_id = recmesid;
                                            inrec.product_id = itm.product_id;
                                            inrec.received_qty = itm.received_qty;
                                            _unitofWork.InReceived.Insert(inrec);
                                            _unitofWork.Save();
                                        }
                                        else
                                        {
                                            //update for the forwarding branch
                                            var inbranchfrom = _unitofWork.InBranchAssign.GetItemById(itm.product_id, Item.Forwarded_To);
                                            if (inbranchfrom == null)
                                            {
                                                var messagea = string.Format("Branch not Assigned for " + CodeService.GetInProductAllName(itm.product_id) + "");
                                                HttpError err = new HttpError(messagea);
                                                return Request.CreateResponse(HttpStatusCode.NotFound, err);
                                            }
                                            inbranchfrom.stock_in_hand = (inbranchfrom.stock_in_hand >= itm.received_qty) ? inbranchfrom.stock_in_hand - itm.received_qty : 0;
                                            _unitofWork.InBranchAssign.Update(inbranchfrom);
                                            _unitofWork.Save();

                                            var inPurchaseData = _unitofWork.InPurchase.GetAllByBranchIdAndProdId(Item.Forwarded_To, itm.product_id);
                                            var remainQtyToDispatch = itm.received_qty;

                                            foreach (var record in inPurchaseData)
                                            {
                                                var inPurchaseDataUpdate = _unitofWork.InPurchase.GetById(record.pur_id);
                                                var totalDispQty = record.p_stk_remain;

                                                if (remainQtyToDispatch == 0)
                                                {
                                                    break;
                                                }

                                                if (totalDispQty >= remainQtyToDispatch)
                                                {
                                                    inPurchaseDataUpdate.p_stk_remain -= remainQtyToDispatch;
                                                    remainQtyToDispatch = 0;
                                                }
                                                else
                                                {
                                                    inPurchaseDataUpdate.p_stk_remain = 0;
                                                    remainQtyToDispatch -= totalDispQty;
                                                }

                                                _unitofWork.InPurchase.Update(inPurchaseDataUpdate);
                                            }

                                            _unitofWork.Save();

                                            var serialstatus = CodeService.GetSerialStatusForProductById(itm.product_id);
                                            if (serialstatus == true)
                                            {
                                                _dapperrepo.DeleteSerialProductStockAcknowledgeRequisition(Item.branch_id, itm.product_id, inreqmes.id);
                                            }
                                        }
                                    }
                                }
                                var notificationdata = _unitofWork.InNotifications.GetByPOMId(inreqmes.id);
                                if (notificationdata != null)
                                {
                                    notificationdata.Status = true;
                                    _unitofWork.InNotifications.Update(notificationdata);
                                }
                                _unitofWork.Save();//save all changes
                                Scope.Complete();
                                var newUrl = this.Url.Link("Default", new
                                {
                                    Controller = "AcknowledgeRequisition",
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
                        var message = string.Format("Please Give Acknowledge Message.");
                        HttpError err = new HttpError(message);
                        return Request.CreateResponse(HttpStatusCode.NotFound, err);
                    }
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Failed...");
            }
            catch (Exception Ex)
            {
                throw;
            }
        }

        [HttpPost]
        public HttpResponseMessage DirectDispatchCreate(InRequisitionMessageVM inreqmes)
        {
            var Session = HttpContext.Current.Session;
            try
            {

                var assignedRole = Session["AssignRole"]?.ToString();
                var branchId = Session["BranchId"]?.ToString();

                // Define allowed roles for Head Office access
                string[] headOfficeAllowedRoles = new[]
                {
                     "Admin_User",
                     "StoreKeeper(HeadOffice)",
                     "Administrator"
                  };

                // Check if the user is trying to direct dispatch to  Head Office (Branch ID 12) staying in same branch and without proper roles
                if (branchId == "12" &&
                    (!headOfficeAllowedRoles.Contains(assignedRole?.Trim())))
                {
                    var errorMessage = "You are not allowed to perform this action to HeadOffice(999) with your current role.";
                    HttpError err = new HttpError(errorMessage);
                    return Request.CreateResponse(HttpStatusCode.Forbidden, err);
                }
                if (inreqmes.Requ_Message != "")
                {
                    //if (ModelState.IsValid)
                    //{
                    if (inreqmes.priority == "Normal")
                    {
                        inreqmes.priority = "N";
                    }
                    else if (inreqmes.priority == "High")
                    {
                        inreqmes.priority = "H";
                    }
                    else if (inreqmes.priority == "Low")
                    {
                        inreqmes.priority = "L";
                    }
                    if (inreqmes.inreqList.Count >= 1)
                    {
                        using (TransactionScope Scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                        {
                            //inrequisitionmessage insert
                            inreqmes.status = "Approved";
                            inreqmes.Req_no = "Direct Dispatch For Branch";
                            inreqmes.Requ_date = DateTime.Now.Date;
                            inreqmes.recommed_message = "Recommended";
                            inreqmes.recommed_date = DateTime.Now.Date;
                            inreqmes.Approver_id = inreqmes.recommed_by;
                            inreqmes.Approved_Date = DateTime.Now.Date;
                            inreqmes.Approver_message = "Approved";
                            inreqmes.IS_SCHEDULE = "Yes";
                            _unitofWork.InRequisitionMessage.Insert(inreqmes);
                            _unitofWork.Save();

                            var inreq = new InRequisitionVM();

                            //inrequisition insert
                            var reqmesid = CodeService.GetInRequisitionMessageId();
                            foreach (var itm in inreqmes.inreqList.Where(x => x.item > 0 && x.quantity > 0))
                            {
                                inreq.unit = itm.unit;
                                inreq.quantity = itm.quantity;
                                inreq.item = itm.item;
                                inreq.Requistion_message_id = reqmesid;
                                inreq.Approved_Quantity = itm.quantity;
                                inreq.Delivered_Quantity = itm.quantity;
                                _unitofWork.InRequisition.Insert(inreq);
                                _unitofWork.Save();
                            }
                            //check inserted item
                            var checkInsertedRequestedItem = _unitofWork.InRequisition.GetItemById(reqmesid);
                            //now check duplicate item exists or not
                            var duplicateItems = checkInsertedRequestedItem.GroupBy(item => item.item).Where(a => a.Count() > 1).Select(a => a.Key).ToList();
                            //now validate if requisition item inserted but not dublicate
                            if (checkInsertedRequestedItem.Count() > 0 && !duplicateItems.Any())
                            {
                                Scope.Complete();
                                var newUrl = this.Url.Link("Default", new
                                {
                                    Controller = "DirectDispatchForBranch",
                                    Action = "Create"
                                });
                                return Request.CreateResponse(HttpStatusCode.OK, new { Success = true, RedirectUrl = newUrl });
                            }
                            else
                            {
                                var message = string.Format("Please refresh page and try again.");
                                HttpError err = new HttpError(message);
                                return Request.CreateResponse(HttpStatusCode.NotFound, err);
                            }

                        }
                    }
                    else
                    {
                        var message = string.Format("Please add at least one item to request.");
                        HttpError err = new HttpError(message);
                        return Request.CreateResponse(HttpStatusCode.NotFound, err);
                    }
                }
                else
                {
                    var message = string.Format("Please Give Dispatch Message.");
                    HttpError err = new HttpError(message);
                    return Request.CreateResponse(HttpStatusCode.NotFound, err);
                }
                //}
                //return Request.CreateResponse(HttpStatusCode.BadRequest, "Failed...");
            }
            catch (Exception Ex)
            {
                throw;
            }
        }

        [HttpPost]
        public HttpResponseMessage InTempRequisitionCreate(InTempRequitionVM itr)
        {
            var Session = HttpContext.Current.Session;
            try
            {
                if (ModelState.IsValid)
                {
                    itr.session_id = "testseeid";//need to generate sessionid
                    itr.created_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
                    itr.created_date = DateTime.Now.Date;
                    _unitofWork.InTempRequisition.Insert(itr);
                    _unitofWork.Save();
                    var serialstatus = CodeService.GetSerialStatusForProduct(itr.productname);
                    var TemReqId = CodeService.GetTempRequisitionId();
                    return Request.CreateResponse(HttpStatusCode.OK, (serialstatus, TemReqId));
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Failed...");
            }

            catch (Exception Ex)

            {
                throw;
            }
        }

        [HttpPost]
        public HttpResponseMessage InRequisitionDetailOtherCreate(InRequisitionDetailOtherVM irdo)
        {
            int pid = CodeService.GetProductIdFromProductName(irdo.productname.Trim());
            try
            {
                if (ModelState.IsValid)
                {
                    irdo.session_id = "testseeid";//need to generate sessionid
                    irdo.is_approved = "y";
                    irdo.productid = pid;
                    _unitofWork.InRequisitionDetailOther.Insert(irdo);
                    _unitofWork.Save();
                    return Request.CreateResponse(HttpStatusCode.OK, "Success...");
                }
                return Request.CreateResponse(HttpStatusCode.OK, "Failed...");
            }

            catch (Exception Ex)

            {
                throw;
            }
        }

        [HttpPost]
        public HttpResponseMessage TempReturnProductCreate(TempReturnProductVM tro)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _unitofWork.TempReturnProduct.Insert(tro);
                    _unitofWork.Save();
                    return Request.CreateResponse(HttpStatusCode.OK, "Success...");
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Failed...");
            }

            catch (Exception Ex)

            {

                throw;
            }
        }
        [HttpPost]
        public HttpResponseMessage ProductReturnCreate(InRequisitionMessageVM inreqmes)
        {
            var Session = HttpContext.Current.Session;
            foreach (var itm1 in inreqmes.inreqList.Where(x => x.ProductName != "" && x.quantity > 0))
            {
                var serialstatus = CodeService.GetSerialStatusForProductById(itm1.item);
                var data = CodeService.TempReturnedProductData(itm1.item);
                if (serialstatus == true && data == 0)
                {
                    var message = string.Format("Please Give Serial No. For Serialized Product.");
                    HttpError err = new HttpError(message);
                    return Request.CreateResponse(HttpStatusCode.NotFound, err);
                }
            }
            try
            {
                if (inreqmes.Requ_Message != "" && inreqmes.Requ_Message != null)
                {
                    if (inreqmes.inreqList.Count >= 1)
                    {
                        using (TransactionScope Scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                        {
                            inreqmes.status = "Full Acknowledged";
                            inreqmes.Req_no = "Returned Dispatch For Branch";
                            inreqmes.Requ_Message = "Requested";
                            inreqmes.Requ_date = DateTime.Now.Date;
                            inreqmes.recommed_by = inreqmes.Requ_by;
                            inreqmes.recommed_message = "Recommended";
                            inreqmes.recommed_date = DateTime.Now.Date;
                            inreqmes.Approver_id = inreqmes.Requ_by;
                            inreqmes.Approved_Date = DateTime.Now.Date;
                            inreqmes.Approver_message = "Approved";
                            inreqmes.Delivered_Date = DateTime.Now.Date;
                            inreqmes.Delivery_Message = "Returned Back";
                            inreqmes.Acknowledged_By = inreqmes.Requ_by;
                            inreqmes.Acknowledged_Date = DateTime.Now.Date;
                            inreqmes.Acknowledged_Message = "Returned Received";
                            inreqmes.IS_SCHEDULE = "NO";
                            inreqmes.priority = "N";
                            inreqmes.dept_id = CodeService.GetDepartmentIdFromEmployee(inreqmes.Requ_by);
                            inreqmes.Delivered_Date = DateTime.Now.Date;
                            _unitofWork.InRequisitionMessage.Insert(inreqmes);
                            _unitofWork.Save();

                            var inreq = new InRequisitionVM();
                            var reqmesid = CodeService.GetInRequisitionMessageId();

                            foreach (var itm in inreqmes.inreqList.Where(x => x.item > 0 && x.quantity > 0 && x.unit != null))
                            {
                                //inrequisition insert
                                inreq.unit = itm.unit;
                                inreq.quantity = itm.quantity;
                                inreq.item = itm.item;
                                inreq.Requistion_message_id = reqmesid;
                                inreq.Approved_Quantity = itm.quantity;
                                inreq.Delivered_Quantity = itm.quantity;
                                _unitofWork.InRequisition.Insert(inreq);
                                _unitofWork.Save();
                            }
                            var indispatchmes = new InDispatchedMessageVM();
                            indispatchmes.req_id = reqmesid;
                            indispatchmes.dispatch_message = inreqmes.Delivery_Message;
                            indispatchmes.dispatched_by = inreqmes.Delivered_By.ToString();
                            indispatchmes.dispatched_date = inreqmes.Delivered_Date;
                            indispatchmes.status = "Full Acknowledged";
                            indispatchmes.stkFlag = "Y";
                            _unitofWork.InDispatchMessage.Insert(indispatchmes);
                            _unitofWork.Save();

                            var indispatch = new InDispatchedVM();
                            var disptmesid = CodeService.GetInDispatchMessageId();

                            foreach (var itm in inreqmes.inreqList.Where(x => x.item > 0 && x.quantity > 0 && x.unit != null))
                            {

                                indispatch.dispatch_msg_id = disptmesid;
                                indispatch.product_id = itm.item;
                                indispatch.dispatched_qty = itm.quantity;
                                indispatch.received_qty = itm.quantity;
                                indispatch.remain = 0;
                                indispatch.from_branch = inreqmes.Forwarded_To;
                                indispatch.to_branch = inreqmes.branch_id;
                                indispatch.dispatched_date = inreqmes.Delivered_Date;
                                _unitofWork.InDispatch.Insert(indispatch);
                                _unitofWork.Save();

                                var serialstatus = CodeService.GetSerialStatusForProductById(itm.item);
                                if (serialstatus == true)
                                {
                                    var trdata = _unitofWork.TempReturnProduct.GetByProductId(itm.item);
                                    _dapperrepo.UpdateSerialProductStockReturnBranch(inreqmes.Forwarded_To, itm.item, Convert.ToInt32(trdata.sn_from), Convert.ToInt32(trdata.sn_to));
                                }
                                //in branch update for sending branch
                                var inbranchfrom = _unitofWork.InBranchAssign.GetItemById(itm.item, inreqmes.Forwarded_To);
                                if (inbranchfrom == null)
                                {
                                    var messagea = string.Format("Branch not Assigned for " + itm.ProductName + "");
                                    HttpError err = new HttpError(messagea);
                                    return Request.CreateResponse(HttpStatusCode.NotFound, err);
                                }
                                inbranchfrom.stock_in_hand = (inbranchfrom.stock_in_hand - itm.quantity) > 0 ? (inbranchfrom.stock_in_hand - itm.quantity) : 0;
                                _unitofWork.InBranchAssign.Update(inbranchfrom);

                                var inPurchaseData = _unitofWork.InPurchase.GetAllByBranchIdAndProdId(inreqmes.Forwarded_To, itm.item);
                                var remainQtyToDispatch = itm.quantity;

                                foreach (var record in inPurchaseData)
                                {
                                    var inPurchaseDataUpdate = _unitofWork.InPurchase.GetById(record.pur_id);
                                    var totalDispQty = record.p_stk_remain;

                                    if (remainQtyToDispatch == 0)
                                    {
                                        break;
                                    }

                                    if (totalDispQty >= remainQtyToDispatch)
                                    {
                                        inPurchaseDataUpdate.p_stk_remain -= remainQtyToDispatch;
                                        remainQtyToDispatch = 0;
                                    }
                                    else
                                    {
                                        inPurchaseDataUpdate.p_stk_remain = 0;
                                        remainQtyToDispatch -= totalDispQty;
                                    }

                                    _unitofWork.InPurchase.Update(inPurchaseDataUpdate);
                                }

                                _unitofWork.Save();

                                //in branch update for receiving branch
                                var inbranchto = _unitofWork.InBranchAssign.GetItemById(itm.item, inreqmes.branch_id);
                                inbranchto.stock_in_hand = inbranchto.stock_in_hand + itm.quantity;
                                _unitofWork.InBranchAssign.Update(inbranchto);

                                var inPurchaseInsert = new InPurchaseVM();
                                inPurchaseInsert.prod_code = itm.item;
                                inPurchaseInsert.p_qty = itm.quantity;
                                inPurchaseInsert.p_rate = itm.p_rate;
                                inPurchaseInsert.p_stk_remain = itm.quantity;
                                inPurchaseInsert.branch_id = inreqmes.branch_id;
                                inPurchaseInsert.entered_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
                                inPurchaseInsert.entered_date = DateTime.Now.Date;
                                inPurchaseInsert.req_id = reqmesid;
                                _unitofWork.InPurchase.Insert(inPurchaseInsert);

                                _unitofWork.Save();//save all
                            }

                            Scope.Complete();
                            var newUrl = this.Url.Link("Default", new
                            {
                                Controller = "ReturnDispatchForBranch",
                                Action = "Create"
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
                    var message = string.Format("Please Give Returned Message.");
                    HttpError err = new HttpError(message);
                    return Request.CreateResponse(HttpStatusCode.NotFound, err);
                }
            }

            catch (Exception Ex)

            {
                throw;
            }
        }

        [HttpPost]
        public HttpResponseMessage TempStaticTempDispatchCreate(StaticTempDispatchVM std)
        {
            var Session = HttpContext.Current.Session;
            try
            {
                if (ModelState.IsValid)
                {
                    std.CreatedBy = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
                    std.CreatedDate = DateTime.Now.Date;
                    std.ProductGroupId = CodeService.GetProductGroupIdByPId((int)std.ProductId);
                    _unitofWork.StaticTempDispatch.Insert(std);
                    _unitofWork.Save();
                    return Request.CreateResponse(HttpStatusCode.OK, "Success");
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Failed...");
            }

            catch (Exception Ex)

            {
                throw;
            }
        }

        [HttpPost]
        public async Task<HttpResponseMessage> DispatchRequisitionForNewBranch(InRequisitionMessageVM inreqmes)
        {
            var Session = HttpContext.Current.Session;
            foreach (var itm1 in inreqmes.indispList.Where(x => x.productname != "" && x.dispatched_qty > 0 && x.IsRowCheck == true))
            {
                var stockinhand = CodeService.GetStockInHand(itm1.productname.Trim(), 12);//12 corporate(999)
                if (itm1.dispatched_qty > stockinhand || stockinhand < 0)
                {
                    var message = string.Format("Insufficient Stock In Hand.");
                    HttpError err = new HttpError(message);
                    return Request.CreateResponse(HttpStatusCode.NotFound, err);
                }
            }
            try
            {
                if (inreqmes.Delivery_Message != "")
                {
                    if (inreqmes.indispList.Count >= 1)
                    {
                        var disptchcount = inreqmes.indispList.Where(x => x.IsRowCheck == true).Count();
                        if (disptchcount > 0)
                        {
                            using (TransactionScope Scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                            {
                                inreqmes.status = "Full Dispatched";
                                inreqmes.Req_no = "Direct Dispatch For Branch";
                                inreqmes.Requ_Message = "Requested";
                                inreqmes.Requ_date = DateTime.Now.Date;
                                inreqmes.recommed_by = inreqmes.Requ_by; ;
                                inreqmes.recommed_message = "Recommended";
                                inreqmes.recommed_date = DateTime.Now.Date;
                                inreqmes.Approver_id = inreqmes.Requ_by;
                                inreqmes.Approved_Date = DateTime.Now.Date;
                                inreqmes.Approver_message = "Approved";
                                inreqmes.IS_SCHEDULE = "NO";
                                inreqmes.priority = "N";
                                inreqmes.dept_id = CodeService.GetDepartmentIdFromEmployee(inreqmes.Requ_by);
                                inreqmes.Delivered_Date = DateTime.Now.Date;
                                _unitofWork.InRequisitionMessage.Insert(inreqmes);
                                _unitofWork.Save();

                                var inreq = new InRequisitionVM();
                                var reqmesid = CodeService.GetInRequisitionMessageId();

                                foreach (var itm in inreqmes.indispList.Where(x => x.product_id > 0 && x.dispatched_qty > 0 && x.IsRowCheck == true))
                                {
                                    //inrequisition insert
                                    inreq.unit = itm.unit;
                                    inreq.quantity = itm.dispatched_qty;
                                    inreq.item = itm.product_id;
                                    inreq.Requistion_message_id = reqmesid;
                                    inreq.Approved_Quantity = itm.dispatched_qty;
                                    inreq.Delivered_Quantity = itm.dispatched_qty;
                                    _unitofWork.InRequisition.Insert(inreq);
                                    _unitofWork.Save();
                                }

                                //indispatchmessage insert
                                var indispatchmes = new InDispatchedMessageVM();
                                indispatchmes.req_id = reqmesid;
                                indispatchmes.dispatch_message = inreqmes.Delivery_Message;
                                indispatchmes.dispatched_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
                                indispatchmes.dispatched_date = inreqmes.Delivered_Date;
                                indispatchmes.status = "Not Acknowledge";
                                indispatchmes.stkFlag = "Y";
                                _unitofWork.InDispatchMessage.Insert(indispatchmes);
                                _unitofWork.Save();

                                //indispatch insert
                                var indispatch = new InDispatchedVM();
                                var disptmesid = CodeService.GetInDispatchMessageId();
                                foreach (var itm in inreqmes.indispList.Where(x => x.product_id > 0 && x.dispatched_qty > 0 && x.IsRowCheck == true))
                                {

                                    indispatch.dispatch_msg_id = disptmesid;
                                    indispatch.product_id = itm.product_id;
                                    indispatch.dispatched_qty = itm.dispatched_qty;
                                    indispatch.received_qty = 0;
                                    indispatch.remain = 0;
                                    indispatch.from_branch = inreqmes.Forwarded_To;
                                    indispatch.to_branch = inreqmes.branch_id;
                                    indispatch.dispatched_date = inreqmes.Delivered_Date;
                                    _unitofWork.InDispatch.Insert(indispatch);
                                    _unitofWork.Save();

                                    if (inreqmes.branch_id != inreqmes.Forwarded_To && inreqmes.branch_id != 12)
                                    {
                                        //in branch update for from branch
                                        var inbranchfrom = _unitofWork.InBranchAssign.GetItemById(itm.product_id, inreqmes.Forwarded_To);
                                        if (inbranchfrom == null)
                                        {
                                            var messagea = string.Format("Branch not Assigned for " + itm.productname + "");
                                            HttpError err = new HttpError(messagea);
                                            return Request.CreateResponse(HttpStatusCode.NotFound, err);
                                        }
                                        inbranchfrom.stock_in_hand = (inbranchfrom.stock_in_hand >= itm.dispatched_qty) ? inbranchfrom.stock_in_hand - itm.dispatched_qty : 0;
                                        _unitofWork.InBranchAssign.Update(inbranchfrom);
                                        //_unitofWork.Save();

                                        var inPurchaseData = _unitofWork.InPurchase.GetAllByBranchIdAndProdId(inreqmes.Forwarded_To, itm.product_id);
                                        var remainQtyToDispatch = itm.received_qty;

                                        foreach (var record in inPurchaseData)
                                        {
                                            var inPurchaseDataUpdate = _unitofWork.InPurchase.GetById(record.pur_id);
                                            var totalDispQty = record.p_stk_remain;

                                            if (remainQtyToDispatch == 0)
                                            {
                                                break;
                                            }

                                            if (totalDispQty >= remainQtyToDispatch)
                                            {
                                                inPurchaseDataUpdate.p_stk_remain -= remainQtyToDispatch;
                                                remainQtyToDispatch = 0;
                                            }
                                            else
                                            {
                                                inPurchaseDataUpdate.p_stk_remain = 0;
                                                remainQtyToDispatch -= totalDispQty;
                                            }

                                            _unitofWork.InPurchase.Update(inPurchaseDataUpdate);
                                        }

                                        _unitofWork.Save();
                                    }
                                }
                                //UserNotification and Email
                                var imsn = new InNotificationsVM();
                                imsn.CreatedDate = DateTime.Now;
                                imsn.Subject = "Acknowledge Requisition";
                                imsn.Forwardedby = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;// int.Parse(pomh.from_user);
                                imsn.Forwardedto = inreqmes.Requ_by;
                                imsn.URL = "/AcknowledgeRequisition/Index" + "/" + reqmesid;
                                imsn.SpecialId = reqmesid;
                                imsn.Status = false;
                                _unitofWork.InNotifications.Insert(imsn);
                                _unitofWork.Save();

                                if (inreqmes.branch_id != inreqmes.Forwarded_To)
                                {
                                    //email to dispatch and received
                                    var receiveremail = CodeService.GetOfficialEmail(inreqmes.Requ_by);
                                    var senderemail = CodeService.GetOfficialEmail(_unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name);

                                    var touser = CodeService.GetEmployeeFirstName(inreqmes.Requ_by);
                                    var fromuser = CodeService.GetEmployeeFullName(_unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name);
                                    var branchname = CodeService.GetBranchNameFromEmployee(inreqmes.Requ_by);
                                    var dispatchdata = _unitofWork.InDispatch.GetAllByDispMesId(disptmesid);
                                    var tdqty = dispatchdata.Sum(x => x.dispatched_qty);

                                    int sn = 1;
                                    string tabledata = "<thead><tr><th>SN</th> <th>Product Name </th> <th>UNIT </th> <th>Dispatched Qty</th> </tr> </thead><tbody>";
                                    foreach (var item in dispatchdata.OrderBy(x => x.productname))
                                    {
                                        tabledata += @"<tr>
                                    <td style='border:1px solid #000; padding:10px'>" + sn + @"</td>
                                    <td style='border:1px solid #000; padding:10px'>" + item.productname + @"</td>
                                    <td style='border:1px solid #000; padding:10px'>" + item.unit + @"</td>
                                    <td style='border:1px solid #000; padding:10px; text-align:center;'>" + item.dispatched_qty + @"</td>
                                    </tr>";
                                        sn++;
                                    }
                                    string HtmlMail = @"<html><table border='1' cellspacing='0' cellpadding='0' >" + tabledata + "</tbody>" + @"<tfoot>
                                                        <tr><td></td><td style='text-align:center; font-weight: bold'>" + "Total :" + @"</td ><td></td><td style='text-align:center; font-weight: bold'>" + tdqty + @"</td></ tr >
                                                        </ tfoot > </table></html>";

                                    if (receiveremail != "")
                                    {
                                        try
                                        {
                                            await EmailService.SendMailAsync(receiveremail, "Items Dispatched",
                                      EmailService.EmailMessage.ReceiverDispatchProductDetails(touser, fromuser, HtmlMail, inreqmes.Delivery_Message));
                                        }
                                        catch (Exception ex)
                                        {

                                        }

                                    }
                                    if (senderemail != "")
                                    {
                                        try
                                        {
                                            await EmailService.SendMailAsync(senderemail, "Items Dispatched",
                                       EmailService.EmailMessage.SenderDispatchProductDetails(touser, fromuser, HtmlMail, branchname, inreqmes.Delivery_Message));
                                        }
                                        catch (Exception ex)
                                        {

                                        }
                                    }
                                }

                                Scope.Complete();
                                var newUrl = this.Url.Link("Default", new
                                {
                                    Controller = "DispatchRequisition",
                                    Action = "Index"
                                });
                                return Request.CreateResponse(HttpStatusCode.OK, new { Success = true, RedirectUrl = newUrl });
                            }
                        }
                        else
                        {
                            var message = string.Format("Please Select At Least One Item To Dispatch.");
                            HttpError err = new HttpError(message);
                            return Request.CreateResponse(HttpStatusCode.NotFound, err);
                        }
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Failed...");
                    }
                }
                else
                {
                    var message = string.Format("Please Give Dispatch Message.");
                    HttpError err = new HttpError(message);
                    return Request.CreateResponse(HttpStatusCode.NotFound, err);
                }
            }

            catch (Exception Ex)

            {
                throw;
            }
        }
    }
}