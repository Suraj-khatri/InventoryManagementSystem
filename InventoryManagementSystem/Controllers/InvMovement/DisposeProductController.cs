using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.DapperRepo;
using InventoryManagementSystem.Infrastructure.Filter;
using InventoryManagementSystem.Infrastructure.Interface;
using InventoryManagementSystem.Infrastructure.Repository;
using InventoryManagementSystem.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers.InvMovement
{
    [UserAuthorize]
    public class DisposeProductController : Controller
    {
        // GET: DisposeProduct
        private IUnitOfWork _unitofWork;
        private IDropDownList _dropDownList;
        private DapperRepoServices _dapperrepo;
        public DisposeProductController()
        {
            _unitofWork = new UnitOfWork();
            _dropDownList = new DropDownList();
            _dapperrepo = new DapperRepoServices();
        }
        [UserAuthorize(menuId:1081)]
        public ActionResult Index()
        {
            var userid = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
            var branchid = Convert.ToInt32((Session["BranchId"]));
            var list = _unitofWork.InDisposeMessage.GetAll(userid,branchid);
            return View(list);
        }
        public ActionResult RequestForDispose()
        {
            var userid = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
            var branchid = Convert.ToInt32((Session["BranchId"]));
            var StatusList = CodeService.getStatusListByBranchId(branchid,userid);
            if (StatusList.Contains("Requested") || StatusList.Contains("Approved"))
            {
                TempData["Fail"] = "Please Complete Previous Dispose Pending Procedure To Proceed Forward.";
                TempData["Title"] = " <strong>Error</strong> <br />";
                TempData["Icon"] = "fa fa-exclamation-circle";
                return RedirectToAction("Index");
            }
            var record = new InDisposeMessageVM();
            record.RequestDate = DateTime.Now.Date;
            record.ProductNameList = _dropDownList.ProductList();
            record.ProductNameList.Insert(0, new SelectListItem { Text = "--Select--", Value = "--Select--" });
            record.EmployeeNameList = _dropDownList.EmployeeList();
            return View(record);
        }

        [HttpPost]
        public ActionResult RequestForDispose(InDisposeMessageVM disposemes)
        {

            foreach (var itm1 in disposemes.disposedetailsList.Where(x => x.ProductId > 0 && x.Rate > 0))
            {
                var serialstatus = CodeService.GetSerialStatusForProductById(itm1.ProductId);
                var TempDisposeId = CodeService.GetTempDisposeIdByPId(itm1.ProductId);
                var TempDisposeOtherId = CodeService.GetTempDisposeOtherId(TempDisposeId);
                //  var data = _unitofWork.TempPurchaseOther.GetById(TempPurId);
                if (serialstatus == true && TempDisposeOtherId == 0)
                {
                    return Json(new { success = false, err = "Please Give Serial No For Serialized Product", JsonRequestBehavior.AllowGet });
                }
                if (TempDisposeId > 0 && serialstatus == false)
                {
                    _unitofWork.TempDispose.Delete(TempDisposeId);
                    _unitofWork.Save();
                }
            }
            try
            {
                if (disposemes.DisposeReason != null && disposemes.DisposeReason.Trim() != "")
                {
                    if (disposemes.disposedetailsList.Count > 1)
                    {
                        using (TransactionScope Scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                        {

                            disposemes.Status = "Requested";
                            disposemes.DisposingBranchId = Convert.ToInt32(Session["BranchId"]);
                            disposemes.DisposingDepartmentId = Convert.ToInt32(Session["DepartmentId"]);
                            disposemes.RequestBy = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
                            disposemes.RequestDate = DateTime.Now.Date;
                            _unitofWork.InDisposeMessage.Insert(disposemes);
                            _unitofWork.Save();

                            //purchaseorder insert
                            var disposedetails = new InDisposeDetailsVM();
                            var disposemesid = CodeService.GeDisposeMessageId();
                            foreach (var itm in disposemes.disposedetailsList.Where(x => x.ProductId > 0 && x.Rate > 0))
                            {
                                disposedetails.DisposeMessageId = disposemesid;
                                disposedetails.ProductId = itm.ProductId;
                                disposedetails.RequestedQty = itm.RequestedQty;
                                disposedetails.Rate = itm.Rate;
                                disposedetails.Amount = itm.Amount;
                                _unitofWork.InDisposeDetails.Insert(disposedetails);
                                _unitofWork.Save();
                            }


                            foreach (var itm in disposemes.disposedetailsList.Where(x => x.ProductId > 0 && x.Rate > 0))
                            {
                                var serialstatus = CodeService.GetSerialStatusForProductById(itm.ProductId);

                                //SerialProductStock insert
                                if (serialstatus == true)
                                {
                                    var tddata = _dapperrepo.GetTempDisposeDataByTPO(itm.ProductId);
                                    var tdodata = _dapperrepo.GetTempDisposeOtherDataByTDO(tddata);
                                    var dmesId = CodeService.GeDisposeMessageId();
                                    foreach (var item in tdodata)
                                    {
                                        _dapperrepo.UpdateSerialProductStockForDispose(dmesId, itm.ProductId, item.sn_from, item.sn_to);
                                    }
                                    
                                    //for (int i = snfrom; i <= snto; i++)
                                    //{
                                    //    var spsdata = _unitofWork.SerialProductStock.GetByPIdAndSequenceNo(itm.ProductId, i);
                                    //    spsdata.disposemesid = CodeService.GeDisposeMessageId();
                                    //    _unitofWork.SerialProductStock.Update(spsdata);
                                    //    _unitofWork.Save();
                                    //}

                                    var tddataremove = _unitofWork.TempDispose.GetById(tddata);
                                    _unitofWork.TempDispose.Delete(tddataremove.Id);

                                    var tdodataremove = _unitofWork.TempDisposeOther.GetById(tddata);
                                    _unitofWork.TempDisposeOther.Delete(tddataremove.Id);
                                }
                            }

                            //UserNotification
                            var imsn = new InNotificationsVM();
                            imsn.CreatedDate = DateTime.Now;
                            imsn.Subject = "Dispose Approval Request";
                            imsn.Forwardedby = (int)disposemes.RequestBy;
                            imsn.Forwardedto = disposemes.ForwardedForApproval;
                            imsn.URL = "/DisposeProduct/ApproveForDispose" + "/" + disposemesid;
                            imsn.SpecialId = disposemesid;
                            imsn.Status = false;
                            _unitofWork.InNotifications.Insert(imsn);
                            _unitofWork.Save();
                            Scope.Complete();
                            return Json(new { success = true, mes = "Successfully Requested For Disposed", JsonRequestBehavior.AllowGet });
                        }
                    }
                    else
                    {
                        return Json(new { success = false, err = "Error Occured !!", JsonRequestBehavior.AllowGet });
                    }
                }
                else
                {
                    return Json(new { success = false, err = "Disposed Reason Is Required !!", JsonRequestBehavior.AllowGet });
                }
            }
#pragma warning disable CS0168 // The variable 'Ex' is declared but never used
            catch (Exception Ex)
#pragma warning restore CS0168 // The variable 'Ex' is declared but never used
            {
                throw;
            }
        }
        [UserAuthorize(menuId:1082)]
        public ActionResult ApproveDisposeIndex()
        {
            var userid = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
            var branchid = Convert.ToInt32((Session["BranchId"]));
            var list = _unitofWork.InDisposeMessage.GetAllRequested(userid,branchid);
            return View(list);
        }
        public ActionResult ApproveForDispose(int id)
        {
            var record = _unitofWork.InDisposeMessage.GetById(id);
            record.disposedetailsList = _unitofWork.InDisposeDetails.GetItemById(id);
            return View(record);
        }

        [HttpPost]
        public ActionResult ApproveForDispose(InDisposeMessageVM disposemes)
        {
            try
            {
                if (disposemes.DisposeReason != null && disposemes.DisposeReason.Trim() != "")
                {
                    if (disposemes.disposedetailsList.Count >= 1)
                    {
                        using (TransactionScope Scope = new TransactionScope(TransactionScopeOption.Suppress))
                        {
                            var Item = _unitofWork.InDisposeMessage.GetById(disposemes.Id);
                            Item.ApprovedDate = DateTime.Now.Date;
                            Item.ApprovedBy = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
                            Item.Status = "Approved";
                            Item.DisposeReason = disposemes.DisposeReason;
                            _unitofWork.InDisposeMessage.Update(Item);
                            _unitofWork.Save();

                            foreach (var itm in disposemes.disposedetailsList.Where(x => x.ProductId > 0))
                            {
                                var data = _unitofWork.InDisposeDetails.GetByMessageIdandProductId(disposemes.Id, itm.ProductId);
                                data.DisposeQty = itm.DisposeQty;
                                _unitofWork.InDisposeDetails.Update(data);
                                _unitofWork.Save();
                            }

                            var notificationdata = _unitofWork.InNotifications.GetByPOMId(disposemes.Id);
                            notificationdata.Status = true;
                            _unitofWork.InNotifications.Update(notificationdata);
                            _unitofWork.Save();

                            Scope.Complete();
                            return Json(new { success = true, mes = "Successfully Approved", JsonRequestBehavior.AllowGet });
                        }
                    }
                    else
                    {
                        return Json(new { success = false, err = "Error Occured !!", JsonRequestBehavior.AllowGet });
                    }
                }
                else
                {
                    return Json(new { success = false, err = "Disposed Reason Is Required !!", JsonRequestBehavior.AllowGet });
                }
            }
#pragma warning disable CS0168 // The variable 'Ex' is declared but never used
            catch (Exception Ex)
#pragma warning restore CS0168 // The variable 'Ex' is declared but never used
            {
                throw;
            }
        }
        public ActionResult RejectForDispose(int id)
        {
            var Item = _unitofWork.InDisposeMessage.GetById(id);
            Item.RejectedBy = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
            Item.RejectionDate = DateTime.Now.Date;
            Item.Status = "Rejected";
            _unitofWork.InDisposeMessage.Update(Item);
            _unitofWork.Save();

            var notificationdata = _unitofWork.InNotifications.GetByPOMId(id);
            notificationdata.Status = true;
            _unitofWork.InNotifications.Update(notificationdata);
            _unitofWork.Save();

            TempData["Success"] = "<p>Data :  Succesfully Rejected </p>";
            TempData["Title"] = "<strong>Data Rejected</strong> <br />";
            TempData["Icon"] = "fa fa-check";
            return RedirectToAction("Index");
        }
        [UserAuthorize(menuId:1083)]
        public ActionResult ProductDisposeIndex()
        {
            var userid = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
            var branchid = Convert.ToInt32((Session["BranchId"]));
            var list = _unitofWork.InDisposeMessage.GetAllApproved(userid,branchid);
            return View(list);
        }
        public ActionResult ProductDispose(int id)
        {
            var record = _unitofWork.InDisposeMessage.GetById(id);
            record.disposedetailsList = _unitofWork.InDisposeDetails.GetItemById(id);
            return View(record);
        }

        [HttpPost]
        public ActionResult ProductDispose(InDisposeMessageVM disposemes)
        {
            var branchid = Convert.ToInt32(Session["BranchId"]);
            try
            {
                if (disposemes.DisposeReason != null && disposemes.DisposeReason.Trim() != "")
                {
                    if (disposemes.disposedetailsList.Count >= 1)
                    {
                        using (TransactionScope Scope = new TransactionScope(TransactionScopeOption.Suppress))
                        {
                            var data = _unitofWork.InDisposeMessage.GetById(disposemes.Id);
                            data.DisposedDate = DateTime.Now.Date;
                            data.DisposedBy = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
                            data.Status = "Disposed";
                            _unitofWork.InDisposeMessage.Update(data);
                            _unitofWork.Save();

                            foreach (var item in disposemes.disposedetailsList)
                            {
                                 var inbranchdata = _unitofWork.InBranchAssign.GetItemById(item.ProductId, branchid);
                                 inbranchdata.stock_in_hand = inbranchdata.stock_in_hand -(int)item.DisposeQty;
                                _unitofWork.InBranchAssign.Update(inbranchdata);
                                _unitofWork.Save();

                                var inPurchaseData = _unitofWork.InPurchase.GetAllByBranchIdAndProdId(branchid, item.ProductId);
                                int remainQtyToDispatch = (int)item.DisposeQty;

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
                                //var InPurchaseData = _unitofWork.InPurchase.GetAllByBranchIdAndProdId(branchid, item.ProductId);
                                //var totalstockremain = 0;
                                //foreach (var record in InPurchaseData)
                                //{
                                //    var InPurchaseDataUpdate = _unitofWork.InPurchase.GetById(record.pur_id);
                                //    if (item.DisposeQty == record.p_stk_remain)
                                //    {
                                //        InPurchaseDataUpdate.p_stk_remain = 0;
                                //        _unitofWork.InPurchase.Update(InPurchaseDataUpdate);
                                //        _unitofWork.Save();
                                //        break;
                                //    }
                                //    else if (item.DisposeQty < record.p_stk_remain)
                                //    {
                                //        InPurchaseDataUpdate.p_stk_remain = (InPurchaseDataUpdate.p_stk_remain - (int)item.DisposeQty);
                                //        _unitofWork.InPurchase.Update(InPurchaseDataUpdate);
                                //        _unitofWork.Save();
                                //        break;
                                //    }
                                //    else
                                //    {

                                //        totalstockremain = totalstockremain + InPurchaseDataUpdate.p_stk_remain;
                                //        var totalreceiveqty = (int)item.DisposeQty;
                                //        if (totalstockremain <= totalreceiveqty)
                                //        {
                                //            InPurchaseDataUpdate.p_stk_remain = 0;
                                //            _unitofWork.InPurchase.Update(InPurchaseDataUpdate);
                                //            _unitofWork.Save();
                                //        }
                                //        else
                                //        {
                                //            InPurchaseDataUpdate.p_stk_remain = (totalstockremain - totalreceiveqty);
                                //            _unitofWork.InPurchase.Update(InPurchaseDataUpdate);
                                //            _unitofWork.Save();
                                //            break;
                                //        }
                                //    }
                                //}


                                _dapperrepo.DeleteSerialProductStock(branchid, item.ProductId, disposemes.Id);
                            }

                            Scope.Complete();
                            return Json(new { success = true, mes = "Successfully Disposed", JsonRequestBehavior.AllowGet });
                        }
                    }
                    else
                    {
                        return Json(new { success = false, err = "Error Occured !!", JsonRequestBehavior.AllowGet });
                    }
                }
                else
                {
                    return Json(new { success = false, err = "Disposed Reason Is Required !!", JsonRequestBehavior.AllowGet });
                }
            }
#pragma warning disable CS0168 // The variable 'Ex' is declared but never used
            catch (Exception Ex)
#pragma warning restore CS0168 // The variable 'Ex' is declared but never used
            {
                throw;
            }
        }
        public void RemoveTempDispose(int id)
        {
            _unitofWork.TempDisposeOther.Delete(id);
            _unitofWork.TempDispose.Delete(id);
            _unitofWork.Save();
        }
        [HttpPost]
        public ActionResult TempDisposeCreate(TempDisposeVM td)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _unitofWork.TempDispose.Insert(td);
                    _unitofWork.Save();
                    var TempDipsoseId = CodeService.GetTempDisposeId();
                    var serialstatus = CodeService.GetSerialStatusForProductById(td.ProductId);
                    return Json(new { success = true, todisposeid = TempDipsoseId, serialstatus = serialstatus, JsonRequestBehavior.AllowGet });
                }
                return Json(new { success = false, err = "Error Occured !!", JsonRequestBehavior.AllowGet });
            }
#pragma warning disable CS0168 // The variable 'Ex' is declared but never used
            catch (Exception Ex)
#pragma warning restore CS0168 // The variable 'Ex' is declared but never used
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult TempDiposeOtherCreate(TempDisposeOtherVM tdo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _unitofWork.TempDisposeOther.Insert(tdo);
                    _unitofWork.Save();
                    return Json(new { success = true, JsonRequestBehavior.AllowGet });
                }
                return Json(new { success = false, err = "Error Occured !!", JsonRequestBehavior.AllowGet });
            }
#pragma warning disable CS0168 // The variable 'Ex' is declared but never used
            catch (Exception Ex)
#pragma warning restore CS0168 // The variable 'Ex' is declared but never used
            {

                throw;
            }
        }
        public JsonResult GetTempDisposeData()
        {
            var data = _unitofWork.TempDispose.GetAll();
            return Json(data);
        }
        public JsonResult GetTempDiposeIdFromTempDisposeOther(int tempid)
        {
            var data = _unitofWork.TempDisposeOther.GetAllTempDisposeOtherFromTempDisposeId(tempid).ToList();
            return Json(data);
        }
    }
}