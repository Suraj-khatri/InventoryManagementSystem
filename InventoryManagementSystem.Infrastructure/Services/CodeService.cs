using Dapper;
using InventoryManagementSystem.Data;
using InventoryManagementSystem.Data.Models;
using InventoryManagementSystem.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;

namespace InventoryManagementSystem.Infrastructure.Services
{
    public class CodeService
    {
        public static string GetCompanyShortName()
        {
            using (var _context = new Entities())
            {
                string sompsname = _context.COMPANY.Select(x => x.COMP_SHORT_NAME.Trim()).FirstOrDefault();
                return sompsname;
            }
        }
        public static string GetFiscalYear()
        {
            using (var _context = new Entities())
            {
                string fiscyear = _context.FiscalYear.Where(x => x.FLAG == true).Select(x => x.FISCAL_YEAR_NEPALI.Trim()).FirstOrDefault();
                return fiscyear;
            }
        }
        public static string GetInItemName(int id)
        {
            using (var _context = new Entities())
            {
                string inItemName = _context.IN_ITEM.FirstOrDefault(x => x.id == id).item_name.Trim().ToString();
                return inItemName;
            }
        }
        public static int GetInItemId(int id)
        {
            using (var _context = new Entities())
            {
                int inItemid = _context.IN_ITEM.FirstOrDefault(x => x.id == id).id;
                return inItemid;
            }
        }
        public static int GetInProductId(int itemid)
        {
            using (var _context = new Entities())
            {
                int inItemid = _context.IN_PRODUCT.FirstOrDefault(x => x.item_id == itemid).id;
                return inItemid;
            }
        }
        public static int GetInItemId()
        {
            using (var _context = new Entities())
            {
                if (_context.IN_ITEM.ToList().Count > 0)
                {
                    int inItemid = _context.IN_ITEM.Max(x => x.id);
                    return inItemid;
                }
                else
                {
                    return 1;
                }
            }
        }
        //for edit in product
        public static string GetInItemNameByParentId(int id)
        {
            using (var _context = new Entities())
            {
                int inparentid = (int)_context.IN_ITEM.FirstOrDefault(x => x.id == id).parent_id;
                string inItemName = _context.IN_ITEM.FirstOrDefault(x => x.id == inparentid).item_name;
                return inItemName.Trim();
            }
        }

        //for save edit in product
        public static int GetParentId(int id)
        {
            using (var _context = new Entities())
            {
                int inParentid = (int)_context.IN_ITEM.FirstOrDefault(x => x.id == id).parent_id;
                return inParentid;
            }
        }
        public static int GetParentByProductId(int pid)
        {
            using (var _context = new Entities())
            {
                int itemid = _context.IN_PRODUCT.FirstOrDefault(x => x.id == pid && x.is_active == true).item_id;
                int inParentid = (int)_context.IN_ITEM.FirstOrDefault(x => x.id == itemid).parent_id;
                return inParentid;
            }
        }
        //generate vendorcode
        public static string GenerateVendorCode()
        {
            using (var _context = new Entities())
            {
                if (_context.Customer.ToList().Count > 0)
                {
                    int vendortid = _context.Customer.Max(x => x.ID + 1);
                    string vendorCode = "VEN-" + vendortid;
                    return vendorCode.Trim();
                }
                else
                {
                    string vendorCode = "VEN-" + 1;
                    return vendorCode.Trim();
                }

            }
        }

        public static string GetDepartmentShortName(int deptname)
        {
            using (var _context = new Entities())
            {
                var data = _context.StaticDataDetail.FirstOrDefault(x => x.ROWID == deptname);
                if (data != null)
                {
                    return data.DETAIL_DESC.Trim();
                }
                return null;
            }
        }
        public static string GetVendorCode(int id)
        {
            using (var _context = new Entities())
            {
                string vendorcode = _context.Customer.FirstOrDefault(x => x.ID == id).CustomerCode.ToString();
                return vendorcode.Trim();
            }
        }

        public static string GetCustomerId(int id)
        {
            using (var _context = new Entities())
            {
                string vendorcode = _context.Customer.FirstOrDefault(x => x.ID == id).ToString();
                return vendorcode.Trim();
            }
        }
        public static int GetApproveQtyByPIdAndDispacthId(int pid, int dispid)
        {
            using (var _context = new Entities())
            {
                int reqmesid = _context.IN_DISPATCH_MESSAGE.FirstOrDefault(x => x.id == dispid).req_id;
                var data = _context.IN_Requisition.FirstOrDefault(x => x.item == pid && x.Requistion_message_id == reqmesid);
                if (data != null)
                {
                    int approveqty = _context.IN_Requisition.FirstOrDefault(x => x.item == pid && x.Requistion_message_id == reqmesid).Approved_Quantity;
                    return approveqty;
                }
                return 0;
            }
        }
        public static string GetInProductName(int id)
        {
            using (var _context = new Entities())
            {
                var inProductName = _context.IN_PRODUCT.FirstOrDefault(x => x.id == id && x.is_active == true);
                if (inProductName != null)
                {
                    string inProdName = _context.IN_PRODUCT.FirstOrDefault(x => x.id == id && x.is_active == true).porduct_code.Trim().ToString();
                    return inProdName;
                }
                else
                {
                    return "";
                }
            }
        }
        public static string GetInProductAllName(int id)
        {
            using (var _context = new Entities())
            {
                var inProductName = _context.IN_PRODUCT.FirstOrDefault(x => x.id == id);
                if (inProductName != null)
                {
                    string inProdName = _context.IN_PRODUCT.FirstOrDefault(x => x.id == id).porduct_code.ToString();
                    return inProdName;
                }
                else
                {
                    return "";
                }
            }
        }
        public static bool GetInItemStatus(int id)
        {
            using (var _context = new Entities())
            {
                bool isactive = _context.IN_ITEM.FirstOrDefault(x => x.id == id).Is_Active;
                return isactive;
            }
        }

        public static string GetStaticDataTypeName(int id)
        {
            using (var _context = new Entities())
            {
                string inTitleName = _context.StaticDataType.FirstOrDefault(x => x.ROWID == id).TYPE_TITLE.ToString();
                return inTitleName.Trim();
            }
        }
        public static int GetStaticDataTypeId(int id)
        {
            using (var _context = new Entities())
            {
                int inrowid = _context.StaticDataType.FirstOrDefault(x => x.ROWID == id).ROWID;
                return inrowid;
            }
        }
        public static int GetStaticDataDetailTypeeId(int id)
        {
            using (var _context = new Entities())
            {
                int typeid = _context.StaticDataDetail.FirstOrDefault(x => x.ROWID == id).TYPE_ID;
                return typeid;
            }
        }
        public static int GetInItemIdByVendorAssignId(int id)
        {
            using (var _context = new Entities())
            {
                int inproducttid = (int)_context.Vendor_Bid_Price.FirstOrDefault(x => x.id == id).product_id;
                int inItemId = _context.IN_PRODUCT.FirstOrDefault(x => x.id == inproducttid && x.is_active == true).item_id;
                return inItemId;
            }
        }

        public static int GetInItemIdByBranchAssignId(int id)
        {
            using (var _context = new Entities())
            {
                int inproducttid = (int)_context.IN_BRANCH.FirstOrDefault(x => x.ID == id).PRODUCT_ID;
                int inItemId = _context.IN_PRODUCT.FirstOrDefault(x => x.id == inproducttid).item_id;
                return inItemId;
            }
        }

        public static List<In_ItemVM> GetAllInItems()
        {
            using (var _context = new Entities())
            {
                var list = _context.IN_ITEM.Where(x => x.is_product == true).ToList();
                var selectList = new List<In_ItemVM>();
                list.ForEach(x => selectList.Add(new In_ItemVM { item_name = x.item_name, id = x.id }));
                return selectList;
            }
        }
        public static List<In_ItemVM> GetAllInItemswithParentId(int parentid)
        {
            using (var _context = new Entities())
            {
                var list = _context.IN_ITEM.Where(x => x.parent_id == parentid).ToList();
                var selectList = new List<In_ItemVM>();
                list.ForEach(x => selectList.Add(new In_ItemVM { item_name = x.item_name, id = x.id }));
                return selectList;
            }
        }
        public static Tuple<string, decimal> GetUnitandRate(int pcode, int vcode)
        {
            using (var _context = new Entities())
            {
                string unit = _context.IN_PRODUCT.FirstOrDefault(x => x.id == pcode && x.is_active == true).package_unit.ToString();
                decimal rate;
                if (vcode == 0)
                {
                    rate = _context.Vendor_Bid_Price.Where(x => x.product_id == pcode).OrderByDescending(x => x.ModifiedDate).Take(1).Select(x => x.price).FirstOrDefault();
                }
                else
                {
                    //rate = _context.Vendor_Bid_Price.FirstOrDefault(x => x.product_id == pcode && x.vendor_id == vcode).price;
                    rate = _context.Vendor_Bid_Price.Where(x => x.product_id == pcode && x.vendor_id == vcode).OrderByDescending(x => x.ModifiedDate).Take(1).Select(x => x.price).FirstOrDefault();
                }
                return Tuple.Create(unit, rate);
            }

        }
        public static string GetUnitForProductName(int id)
        {
            using (var _context = new Entities())
            {
                string unit = _context.IN_PRODUCT.FirstOrDefault(x => x.id == id).package_unit.ToString();
                return unit.Trim();
            }
        }
        public static decimal GetRateForProductId(int id)
        {
            using (var _context = new Entities())
            {
                var data = _context.Vendor_Bid_Price.FirstOrDefault(x => x.product_id == id);
                if (data != null)
                {
                    decimal rate = _context.Vendor_Bid_Price.Where(x => x.product_id == id).OrderByDescending(x => x.ModifiedDate).Take(1).Select(x => x.price).FirstOrDefault();
                    //decimal rate = _context.Vendor_Bid_Price.FirstOrDefault(x => x.product_id == id).price;
                    return rate;
                }
                return 0;
            }
        }

        public static int GetPurchaseOrderMessageId()
        {
            using (var _context = new Entities())
            {
                if (_context.Purchase_Order_Message.ToList().Count > 0)
                {
                    int inpurormesid = _context.Purchase_Order_Message.Max(x => x.id);
                    return inpurormesid + 1;
                }
                return 1;
            }
        }

        public static int GetPurchaseReturnId()
        {
            using (var _context = new Entities())
            {
                if (_context.In_PurchaseReturn.ToList().Count > 0)
                {
                    int inpurreturnid = _context.In_PurchaseReturn.Max(x => x.Id);
                    return inpurreturnid + 1;
                }
                return 1;
            }
        }

        public static int GetRequisitionMessageId()
        {
            using (var _context = new Entities())
            {
                if (_context.IN_Requisition_Message.ToList().Count > 0)
                {
                    int inpurormesid = _context.IN_Requisition_Message.Max(x => x.id);
                    return inpurormesid + 1;
                }
                return 1;
            }
        }

        public static int GetFuelRequisitionMessageId()
        {
            using (var _context = new Entities())
            {
                if (_context.Fuel_Requests_Message.ToList().Count > 0)
                {
                    int inpurormesid = _context.Fuel_Requests_Message.Max(x => x.Id);
                    return inpurormesid + 1;
                }
                return 1;
            }
        }
        public static int GetPurchaseOrderMessageIdforpurchaseorder()
        {
            using (var _context = new Entities())
            {
                if (_context.Purchase_Order_Message.ToList().Count > 0)
                {
                    int inpurormesid = _context.Purchase_Order_Message.Max(x => x.id);
                    return inpurormesid;
                }
                return 1;
            }
        }
        public static int GeDisposeMessageId()
        {
            using (var _context = new Entities())
            {
                if (_context.In_Dispose_Message.ToList().Count > 0)
                {
                    int indisposemesid = _context.In_Dispose_Message.Max(x => x.Id);
                    return indisposemesid;
                }
                return 1;
            }
        }
        public static int GetProductIdFromProductName(string pname)
        {
            string[] productname = pname.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            pname = productname[0];
            using (var _context = new Entities())
            {
                int inproductid = _context.IN_PRODUCT.FirstOrDefault(x => x.porduct_code.Trim() == pname.Trim() && x.is_active == true).id;
                return inproductid;
            }
        }

        public static int GetProductIdByItemId(int itemid)
        {
            using (var _context = new Entities())
            {
                var data = _context.IN_PRODUCT.Where(x => x.item_id == itemid && x.is_active == true).FirstOrDefault();
                if (data != null)
                {
                    int inproductid = _context.IN_PRODUCT.FirstOrDefault(x => x.item_id == itemid && x.is_active == true).id;
                    return inproductid;
                }
                return 0;
            }
        }
        public static int GetProductGroupIdByPId(int pId)
        {
            using (var _context = new Entities())
            {
                int itemid = _context.IN_PRODUCT.Where(x => x.id == pId).FirstOrDefault().item_id;
                int productgroupid = Convert.ToInt32(_context.IN_ITEM.FirstOrDefault(x => x.id == itemid).parent_id);
                return productgroupid;
            }
        }
        public static int GetInItemIdFromProductId(int pId)
        {
            using (var _context = new Entities())
            {
                int itemid = _context.IN_PRODUCT.FirstOrDefault(x => x.id == pId).item_id;
                return itemid;
            }
        }
        public static bool GetSerialStatusForProduct(string pname)
        {
            string[] productname = pname.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            pname = productname[0];
            using (var _context = new Entities())
            {
                bool serialstatus = _context.IN_PRODUCT.Where(x => x.porduct_code.Trim() == pname.Trim() && x.is_active == true).Select(x => x.serial_no).FirstOrDefault();
                return serialstatus;
            }
        }
        public static bool GetSerialStatusForProductById(int pid)
        {
            using (var _context = new Entities())
            {
                bool serialstatus = _context.IN_PRODUCT.Where(x => x.id == pid && x.is_active == true).Select(x => x.serial_no).FirstOrDefault();
                return serialstatus;
            }
        }
        public static Tuple<bool, int> GetSerialStatusandTempPurchaseId(string pname, int tppid)
        {
            using (var _context = new Entities())
            {
                bool serialstatus = _context.IN_PRODUCT.FirstOrDefault(x => x.porduct_code.Trim() == pname.Trim() && x.is_active == true).serial_no;
                int tempid = _context.Temp_Purchase.FirstOrDefault(x => x.product_code == tppid).id;
                return Tuple.Create(serialstatus, tempid);
            }

        }
        public static int GetTempPurchaseId()
        {
            using (var _context = new Entities())
            {
                if (_context.Temp_Purchase.ToList().Count > 0)
                {
                    int intemppurid = _context.Temp_Purchase.Max(x => x.id);
                    return intemppurid;
                }
                return 1;
            }
        }
        public static int GetTempDisposeId()
        {
            using (var _context = new Entities())
            {
                if (_context.Temp_Dispose.ToList().Count > 0)
                {
                    int intempdisposeid = _context.Temp_Dispose.Max(x => x.Id);
                    return intempdisposeid;
                }
                return 1;
            }
        }
        public static int GetPurchaseId()
        {
            using (var _context = new Entities())
            {
                if (_context.IN_PURCHASE.ToList().Count > 0)
                {
                    int purid = _context.IN_PURCHASE.Max(x => x.pur_id);
                    return purid;
                }
                return 1;
            }
        }
        public static int GetTempPurchaseOtherId(int temppurid)
        {
            using (var _context = new Entities())
            {
                int intemppurotherid = _context.Temp_Purchase_Other.Where(x => x.temp_purchase_id == temppurid).Select(x => x.id).FirstOrDefault();
                return intemppurotherid;
            }
        }
        public static int GetTempDisposeOtherId(int tempdisposeid)
        {
            using (var _context = new Entities())
            {
                int intempdisposeotherid = _context.Temp_Dispose_Other.Where(x => x.TempDisposedId == tempdisposeid).Select(x => x.Id).FirstOrDefault();
                return intempdisposeotherid;
            }
        }
        public static int GetTempPurchaseIdForProduct(string pname, int bill_id)
        {
            string[] productname = pname.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            pname = productname[0];
            using (var _context = new Entities())
            {
                int pid = _context.IN_PRODUCT.FirstOrDefault(x => x.porduct_code.Trim() == pname.Trim() && x.is_active == true).id;
                int intemppurid = _context.Temp_Purchase.Where(x => x.product_code == pid && x.IsActive == true && x.bill_id == bill_id).Select(x => x.id).FirstOrDefault();
                return intemppurid;
            }
        }
        public static int GetTempPurchaseIdForProduct(string pname)
        {
            string[] productname = pname.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            pname = productname[0];
            using (var _context = new Entities())
            {
                int pid = _context.IN_PRODUCT.FirstOrDefault(x => x.porduct_code.Trim() == pname.Trim() && x.is_active == true).id;
                int intemppurid = _context.Temp_Purchase.Where(x => x.product_code == pid && x.IsActive == true).OrderByDescending(x => x.id).Select(x => x.id).FirstOrDefault();
                return intemppurid;
            }
        }
        public static int GetTempDisposeIdByPId(int pid)
        {
            using (var _context = new Entities())
            {
                int intempid = _context.Temp_Dispose.Where(x => x.ProductId == pid).Select(x => x.Id).FirstOrDefault();
                return intempid;
            }
        }
        public static int GetTempPurchaseIdForSerial(string pname)
        {
            string[] productname = pname.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            pname = productname[0];
            using (var _context = new Entities())
            {
                int pid = _context.IN_PRODUCT.FirstOrDefault(x => x.porduct_code.Trim() == pname.Trim() && x.is_active == true).id;
                int intemppurid = _context.Temp_Purchase.Where(x => x.product_code == pid && x.order_message_id == 0).Select(x => x.id).FirstOrDefault();
                return intemppurid;
            }
        }
        public static int GetTempPurchaseIdFromOrdMesid(int ordermesid)
        {
            using (var _context = new Entities())
            {
                int intemppurid = _context.Temp_Purchase.Where(x => x.order_message_id == ordermesid).Select(x => x.id).FirstOrDefault();
                return intemppurid;
            }
        }
        public static List<int> GetProductIdFromTemPur(int ordermesid)
        {
            using (var _context = new Entities())
            {
                var list = _context.Temp_Purchase.Where(x => x.order_message_id == ordermesid).Select(x => x.product_code).ToList();
                return list;
            }
        }
        public static string GetVendorNameFromPurchaseOrderMessage(int id)
        {
            using (var _context = new Entities())
            {
                int venderid = _context.Purchase_Order_Message.FirstOrDefault(x => x.id == id).vendor_code;
                var customer = _context.Customer.FirstOrDefault(x => x.ID == venderid);
                string vname = customer != null ? customer.CustomerName.ToString() : "";
                return vname.Trim();
            }
        }
        public static int GetVendorIdFromPurchaseOrderMessage(int id)
        {
            using (var _context = new Entities())
            {
                int venderid = _context.Purchase_Order_Message.FirstOrDefault(x => x.id == id).vendor_code;
                return venderid;
            }
        }

        public static string GetVendorNameFromBillInfo(int id)
        {
            using (var _context = new Entities())
            {
                int partycode = Convert.ToInt32(_context.Bill_info.FirstOrDefault(x => x.bill_id == id).party_code);
                if (partycode != 0)
                {
                    string vname = _context.Customer.FirstOrDefault(x => x.ID == partycode).CustomerName.ToString();
                    return vname.Trim();
                }
                else
                {
                    string vname = _context.Bill_info.FirstOrDefault(x => x.bill_id == id).VendorName.ToString();
                    return vname.Trim();
                }
            }
        }
        public static int GetBillInfoId()
        {
            using (var _context = new Entities())
            {
                if (_context.Bill_info.ToList().Count > 0)
                {
                    int billid = _context.Bill_info.Max(x => x.bill_id);
                    return billid;
                }
                return 1;
            }
        }
        public static string GetBillNoFromBillId(int billid)
        {
            using (var _context = new Entities())
            {
                if (billid > 0)
                {
                    var data = _context.Bill_info.FirstOrDefault(x => x.bill_id == billid);
                    if (data != null)
                    {
                        string billno = _context.Bill_info.FirstOrDefault(x => x.bill_id == billid).billno.ToString();
                        return billno.Trim();
                    }
                    return "chalan";
                }
                return null;
            }
        }
        public static int GetPurchaseReturnForDetailsId()
        {
            using (var _context = new Entities())
            {
                if (_context.In_PurchaseReturn.ToList().Count > 0)
                {
                    int inpurretid = _context.In_PurchaseReturn.Max(x => x.Id);
                    return inpurretid;
                }
                return 1;
            }
        }
        public static int GetVendorIdFromVendorName(string vendor)
        {
            using (var _context = new Entities())
            {
                int vendorid = _context.Customer.FirstOrDefault(x => x.CustomerName.Trim() == vendor.Trim()).ID;
                return vendorid;
            }
        }
        public static string GetVendorNameFromBillId(int billid)
        {
            using (var _context = new Entities())
            {
                if (billid > 0)
                {
                    var data = _context.Bill_info.FirstOrDefault(x => x.bill_id == billid)?.party_code;
                    if (data != null)
                    {
                        int vendorcode = Convert.ToInt32(_context.Bill_info.FirstOrDefault(x => x.bill_id == billid).party_code);
                        string vendorname = _context.Customer.FirstOrDefault(x => x.ID == vendorcode).CustomerName.ToString();
                        return vendorname.Trim();
                    }
                    return "";
                }
                return null;
            }
        }
        public static int GetInRequisitionMessageId()
        {
            using (var _context = new Entities())
            {
                if (_context.IN_Requisition_Message.ToList().Count > 0)
                {
                    int inreqmesid = _context.IN_Requisition_Message.Max(x => x.id);
                    return inreqmesid;
                }
                return 1;
            }
        }
        public static int GetFuelRequestMessageId()
        {
            using (var _context = new Entities())
            {
                if (_context.Fuel_Requests_Message.ToList().Count > 0)
                {
                    int inreqmesid = _context.Fuel_Requests_Message.Max(x => x.Id);
                    return inreqmesid;
                }
                return 1;
            }
        }
        public static string GetBranchName(int id)
        {
            using (var _context = new Entities())
            {
                var data = _context.Branches.FirstOrDefault(x => x.BRANCH_ID == id);
                if (data != null)
                {
                    string branchname = _context.Branches.FirstOrDefault(x => x.BRANCH_ID == id).BRANCH_NAME.ToString();
                    return branchname.Trim();
                }
                return "";
            }
        }
        public static int GetBranchIdFromEmployee(int id)
        {
            using (var _context = new Entities())
            {
                int branchid = _context.Employee.FirstOrDefault(x => x.EMPLOYEE_ID == id).BRANCH_ID;
                return branchid;
            }
        }

        public static string GetRequestingBranchContactNoByBranchId(int branchid)
        {
            using (var _context = new Entities())
            {
                string contactno = _context.Branches.FirstOrDefault(x => x.BRANCH_ID == branchid).BRANCH_PHONE;
                return contactno;
            }
        }
        public static string GetBranchNameFromEmployee(int id)
        {
            using (var _context = new Entities())
            {
                int branchid = _context.Employee.FirstOrDefault(x => x.EMPLOYEE_ID == id).BRANCH_ID;
                string branchname = _context.Branches.FirstOrDefault(x => x.BRANCH_ID == branchid).BRANCH_NAME;
                return branchname.Trim();
            }
        }
        public static string GetDepartmentNameFromEmployee(int id)
        {
            using (var _context = new Entities())
            {
                int deptid = _context.Employee.FirstOrDefault(x => x.EMPLOYEE_ID == id).DEPARTMENT_ID;
                string deptname = _context.departments.FirstOrDefault(x => x.DEPARTMENT_ID == deptid).DEPARTMENT_NAME;
                return deptname.Trim();
            }
        }
        public static int GetDepartmentIdFromEmployee(int id)
        {
            using (var _context = new Entities())
            {
                int deptid = _context.Employee.FirstOrDefault(x => x.EMPLOYEE_ID == id).DEPARTMENT_ID;
                return deptid;
            }
        }
        public static string GetEmployeeFullName(int id)
        {
            using (var _context = new Entities())
            {
                var data = _context.Employee.FirstOrDefault(x => x.EMPLOYEE_ID == id);
                if (data != null)
                {
                    string firstname = _context.Employee.FirstOrDefault(x => x.EMPLOYEE_ID == id).FIRST_NAME;
                    string midname = _context.Employee.FirstOrDefault(x => x.EMPLOYEE_ID == id).MIDDLE_NAME;
                    string lastname = _context.Employee.FirstOrDefault(x => x.EMPLOYEE_ID == id).LAST_NAME;
                    var FullName = firstname + " " + midname + " " + lastname;
                    return FullName.Trim();
                }
                return "";
            }
        }
        public static string GetEmployeeFirstName(int id)
        {
            using (var _context = new Entities())
            {
                string firstname = _context.Employee.FirstOrDefault(x => x.EMPLOYEE_ID == id).FIRST_NAME;
                return firstname.Trim();
            }
        }
        public static string GetOfficialEmail(int id)
        {
            using (var _context = new Entities())
            {
                var data = _context.Employee.FirstOrDefault(x => x.EMPLOYEE_ID == id).OFFICIAL_EMAIL;
                if (data != null)
                {
                    string email = _context.Employee.FirstOrDefault(x => x.EMPLOYEE_ID == id).OFFICIAL_EMAIL.ToString();
                    return email.Trim();
                }
                return "";
            }
        }
        public static string GetEmployeeAssignRole(int empid)
        {
            using (var _context = new Entities())
            {
                var role = _context.user_role.FirstOrDefault(x => x.user_id == empid);
                if (role != null)
                {
                    int roleid = _context.user_role.FirstOrDefault(x => x.user_id == empid).role_id;
                    var assignrole = _context.StaticDataDetail.FirstOrDefault(x => x.ROWID == roleid).DETAIL_TITLE;
                    return assignrole.Trim();
                }
                return "";
            }
        }
        public static int GetInDispatchMessageId()
        {
            using (var _context = new Entities())
            {
                if (_context.IN_DISPATCH_MESSAGE.ToList().Count > 0)
                {
                    int indisptmesid = _context.IN_DISPATCH_MESSAGE.Max(x => x.id);
                    return indisptmesid;
                }
                return 1;
            }
        }
        public static int GetInDispatchMessageIdWithReqMesId(int reqmesid)
        {
            using (var _context = new Entities())
            {
                int indisptmesid = _context.IN_DISPATCH_MESSAGE.Where(x => x.req_id == reqmesid).Select(x => x.id).FirstOrDefault();
                return indisptmesid;
            }
        }
        public static int GetInDispatchMessageIdFromInRequisitionMesId(int inreqmesid)
        {
            using (var _context = new Entities())
            {
                int indisptmesid = _context.IN_DISPATCH_MESSAGE.FirstOrDefault(x => x.req_id == inreqmesid).id;
                return indisptmesid;
            }
        }
        public static int GetInReceivedMessageId()
        {
            using (var _context = new Entities())
            {
                if (_context.IN_RECEIVED_MESSAGE.ToList().Count > 0)
                {
                    int inrecmesid = _context.IN_RECEIVED_MESSAGE.Max(x => x.id);
                    return inrecmesid;
                }
                return 1;
            }
        }
        public static string GetDepartmentName(int deptid)
        {
            using (var _context = new Entities())
            {
                if (deptid > 0)
                {
                    string departname = _context.departments.FirstOrDefault(x => x.DEPARTMENT_ID == deptid).DEPARTMENT_NAME;
                    return departname.Trim();
                }
                else
                {
                    return "";
                }
            }
        }
        public static string GetDepartmentNamewithStaticId(int deptid)
        {
            using (var _context = new Entities())
            {
                string departname = _context.StaticDataDetail.FirstOrDefault(x => x.ROWID == deptid).DETAIL_TITLE;
                return departname.Trim();
            }
        }
        public static string GetDesignation(int posid)
        {
            using (var _context = new Entities())
            {
                var degnameall = _context.StaticDataDetail.FirstOrDefault(x => x.ROWID == posid);
                if (degnameall != null)
                {
                    string degname = _context.StaticDataDetail.FirstOrDefault(x => x.ROWID == posid).DETAIL_DESC;
                    return degname.Trim();
                }
                else
                {
                    return "Assistant";
                }

            }
        }
        public static int GetEmployeeId()
        {
            using (var _context = new Entities())
            {
                if (_context.Employee.ToList().Count > 0)
                {
                    int empid = _context.Employee.Max(x => x.EMPLOYEE_ID);
                    return empid;
                }
                return 1;
            }
        }
        public static int GetEmployeeIdFromAdmin(int eid)
        {
            using (var _context = new Entities())
            {
                int empid = _context.Admins.FirstOrDefault(x => x.AdminID == eid).Name;
                return empid;
            }
        }
        public static int GetAdminId()
        {
            using (var _context = new Entities())
            {
                if (_context.Admins.ToList().Count > 0)
                {
                    int empid = _context.Admins.Max(x => x.AdminID);
                    return empid;
                }
                return 1;
            }
        }
        public static int GetStaticDataDetailRowId()
        {
            using (var _context = new Entities())
            {
                int empid = _context.StaticDataDetail.FirstOrDefault(x => x.DETAIL_TITLE.Trim().Equals("General/Basic User") || x.DETAIL_TITLE.Trim().Equals("Inventory - Branch User")).ROWID;
                return empid;
            }
        }
        public static string GetStaticDataDetailDetailTitle(int roleid)
        {
            using (var _context = new Entities())
            {
                string detailtitle = _context.StaticDataDetail.FirstOrDefault(x => x.ROWID == roleid).DETAIL_TITLE;
                return detailtitle.Trim();
            }
        }
        public static bool GetSerialStatusForProductInDispatchRequisition(int pid)
        {
            using (var _context = new Entities())
            {
                var ss = _context.IN_PRODUCT.FirstOrDefault(x => x.id == pid && x.is_active == true);
                if (ss != null)
                {
                    bool serialstatus = _context.IN_PRODUCT.FirstOrDefault(x => x.id == pid && x.is_active == true).serial_no;
                    return serialstatus;
                }
                return false;
            }
        }
        public static int InRequisitionDetailId()
        {
            using (var _context = new Entities())
            {
                if (_context.IN_Requisition_Detail.ToList().Count > 0)
                {
                    int inreqdetid = _context.IN_Requisition_Detail.Max(x => x.id);
                    return inreqdetid;
                }
                return 1;
            }
        }

        public static int INRequisitionDetailOtherData(string pname, int reqid)
        {
            string[] productname = pname.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            pname = productname[0];
            using (var _context = new Entities())
            {
                int pid = _context.IN_PRODUCT.Where(x => x.porduct_code.Trim() == pname.Trim() && x.is_active == true).Select(x => x.id).FirstOrDefault();
                var data = _context.IN_Requisition_Detail_Other.Where(x => x.productid == pid && x.detail_id == reqid).Select(x => x.id).FirstOrDefault();
                if (data > 0)
                {
                    return data;
                }
                return 0;
            }
        }
        public static int TempReturnedProductData(int pid)
        {
            using (var _context = new Entities())
            {
                var data = _context.Temp_Return_Product.Where(x => x.productid == pid).Select(x => x.id).FirstOrDefault();
                if (data > 0)
                {
                    return data;
                }
                return 0;
            }
        }
        public static int GetTempRequisitionId()
        {
            using (var _context = new Entities())
            {
                if (_context.IN_Temp_Requisition.ToList().Count > 0)
                {
                    int intemppurid = _context.IN_Temp_Requisition.Max(x => x.id);
                    return intemppurid;
                }
                return 1;
            }
        }

        public static double GetStockInHand(string pname, int bid)
        {
            string[] productname = pname.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            pname = productname[0];
            using (var _context = new Entities())
            {
                int pid = _context.IN_PRODUCT.Where(x => x.porduct_code.Trim() == pname.Trim() && x.is_active == true).Select(x => x.id).FirstOrDefault();
                double stockinhand = _context.IN_BRANCH.Where(x => x.PRODUCT_ID == pid && x.BRANCH_ID == bid).Select(x => x.stock_in_hand).FirstOrDefault();
                return stockinhand;
            }
        }
        public static int getBranchIdFromInRequisitionMessage(int inreqmesid)
        {
            using (var _context = new Entities())
            {
                int branchid = _context.IN_Requisition_Message.Where(x => x.id == inreqmesid).Select(x => x.Forwarded_To).FirstOrDefault();
                return branchid;
            }
        }
        public static int getReqBranchIdFromInRequisitionMessage(int inreqmesid)
        {
            using (var _context = new Entities())
            {
                int branchid = _context.IN_Requisition_Message.Where(x => x.id == inreqmesid).Select(x => x.branch_id).FirstOrDefault();
                return branchid;
            }
        }

        public static int getVehicleIdFromFuelRequestsMessage(int fuelreqvid)
        {
            using (var _context = new Entities())
            {
                int vehicleid = _context.Fuel_Requests_Message.Where(x => x.Id == fuelreqvid).Select(x => x.Id).FirstOrDefault();
                return vehicleid;
            }
        }
        public static int getFuelIdFromFuelRequestsMessage(int fuelreqfuelid)
        {
            using (var _context = new Entities())
            {
                int fuelid = _context.Fuel_Requests_Message.Where(x => x.Id == fuelreqfuelid).Select(x => x.Id).FirstOrDefault();
                return fuelid;
            }
        }
        public static double GetStockInHandfromPId(int pid, int bid)
        {
            using (var _context = new Entities())
            {
                double stockinhand = _context.IN_BRANCH.Where(x => x.PRODUCT_ID == pid && x.BRANCH_ID == bid).Select(x => x.stock_in_hand).FirstOrDefault();
                return stockinhand;
            }
        }

        public static int GetReOrderQtyForPid(int id, int bid, int userid)
        {
            using (var _context = new Entities())
            {
                if (bid != 12)//&& (userid==1000 || userid==1211)
                {
                    var reorderqty = _context.IN_BRANCH.FirstOrDefault(x => x.PRODUCT_ID == id && x.BRANCH_ID == bid).REORDER_QTY;
                    if (reorderqty > 0)
                    {
                        return ((int)reorderqty);
                    }
                    else
                    {
                        return 1000000000;
                    }
                }
                else
                {
                    return 1000000000;
                }
            }
        }
        public static int GetMaxHoldingQtyForPid(int id, int bid, int userid)
        {
            using (var _context = new Entities())
            {
                if (bid != 12)//&& (userid == 1000 || userid == 1211)
                {
                    var maxholdingqty = _context.IN_BRANCH.FirstOrDefault(x => x.PRODUCT_ID == id && x.BRANCH_ID == bid).MAX_HOLDING_QTY;
                    int stockinhand = (int)_context.IN_BRANCH.FirstOrDefault(x => x.PRODUCT_ID == id && x.BRANCH_ID == bid).stock_in_hand;
                    if (maxholdingqty > 0)
                    {
                        return ((int)maxholdingqty - stockinhand);
                    }
                    else
                    {
                        return 1000000000;
                    }
                }
                else
                {
                    return 1000000000;
                }

            }
        }
        public static int GetBranchIdFromBranchName(string bname)
        {
            using (var _context = new Entities())
            {
                var branch = _context.Branches.AsEnumerable()  // Fetch all branches into memory
                    .FirstOrDefault(x =>
                        x.BRANCH_NAME != null &&
                        NormalizeWhitespace(x.BRANCH_NAME).Equals(NormalizeWhitespace(bname), StringComparison.OrdinalIgnoreCase));

                if (branch != null)
                {
                    return branch.BRANCH_ID;
                }
                else
                {
                    // Handle case where branch name is not found
                    // For example, returning -1 or throwing an exception
                    throw new ArgumentException("Branch name not found.");
                }
            }
        }




        public static int GetEmpIdFromSuperVisorAssignment(int sId)
        {
            using (var _context = new Entities())
            {
                int empId = Convert.ToInt32(_context.SuperVisroAssignment.FirstOrDefault(x => x.SV_ASSIGN_ID == sId).EMP);
                return empId;
            }
        }

        //Fixed Asset
        public static string GetAssetItemName(int id)
        {
            using (var _context = new Entities())
            {
                string assetItemName = _context.ASSET_ITEM.FirstOrDefault(x => x.id == id).item_name.ToString();
                return assetItemName.Trim();
            }
        }
        public static int GetAssetItemId(int id)
        {
            using (var _context = new Entities())
            {
                int assetItemid = _context.ASSET_ITEM.FirstOrDefault(x => x.id == id).id;
                return assetItemid;
            }
        }
        public static bool GetAssetItemStatus(int id)
        {
            using (var _context = new Entities())
            {
                bool isactive = _context.ASSET_ITEM.FirstOrDefault(x => x.id == id).Is_Active;
                return isactive;
            }
        }
        public static string GetAssetItemNameByParentId(int id)
        {
            using (var _context = new Entities())
            {
                int parentid = Convert.ToInt32(_context.ASSET_ITEM.Where(x => x.id == id).Select(x => x.parent_id).FirstOrDefault());
                string assetItemName = _context.ASSET_ITEM.FirstOrDefault(x => x.id == parentid).item_name;
                return assetItemName.Trim();
            }
        }
        public static int GetAssetItemParentId(int id)
        {
            using (var _context = new Entities())
            {
                int parentid = Convert.ToInt32(_context.ASSET_ITEM.Where(x => x.id == id).Select(x => x.parent_id).FirstOrDefault());
                return parentid;
            }
        }
        public static int GetAssetItemId()
        {
            using (var _context = new Entities())
            {
                if (_context.ASSET_ITEM.ToList().Count > 0)
                {
                    int inItemid = _context.ASSET_ITEM.Max(x => x.id);
                    return inItemid;
                }
                return 1;
            }
        }
        public static int GetAssetProductId(int itemid)
        {
            using (var _context = new Entities())
            {
                int assetItemid = _context.ASSET_PRODUCT.FirstOrDefault(x => x.item_id == itemid).id;
                return assetItemid;
            }
        }
        public static string GetAssetProductName(int pid)
        {
            using (var _context = new Entities())
            {
                string aasetproductname = _context.ASSET_PRODUCT.FirstOrDefault(x => x.id == pid).porduct_code;
                return aasetproductname.Trim();
            }
        }
        public static int GetAssetItemIdByBranchAssignId(int id)
        {
            using (var _context = new Entities())
            {
                int assetproducttid = (int)_context.ASSET_BRANCH.FirstOrDefault(x => x.ID == id).PRODUCT_ID;
                int assetItemId = _context.ASSET_PRODUCT.FirstOrDefault(x => x.id == assetproducttid).item_id;
                return assetItemId;
            }
        }
        public static int GetAssetRequisitionMessageId()
        {
            using (var _context = new Entities())
            {
                if (_context.ASSET_REQUISITION_MESSAGE.ToList().Count > 0)
                {
                    int assetreqmesid = _context.ASSET_REQUISITION_MESSAGE.Max(x => x.id);
                    return assetreqmesid;
                }
                return 1;
            }
        }
        public static string GetAssetTypeName(int id)
        {
            using (var _context = new Entities())
            {
                string assetProductName = _context.ASSET_PRODUCT.FirstOrDefault(x => x.id == id).porduct_code.ToString();
                return assetProductName.Trim();
            }
        }

        public static int GetAssetInvTemId()
        {
            using (var _context = new Entities())
            {
                if (_context.ASSET_INVENTORY_TEMP.ToList().Count > 0)
                {
                    int inItemid = _context.ASSET_INVENTORY_TEMP.Max(x => x.id);
                    return inItemid;
                }
                return 1;
            }
        }
        public static int GetUserIdFromUserRole(int rowid)
        {
            using (var _context = new Entities())
            {
                int userid = _context.user_role.FirstOrDefault(x => x.row_id == rowid).user_id;
                return userid;
            }
        }

        public static List<string> getStatusListByBranchId(int branchid, int userid)
        {
            using (var _context = new Entities())
            {
                var list = _context.In_Dispose_Message.Where(x => x.DisposingBranchId == branchid && x.RequestBy == userid).Select(x => x.Status.Trim()).ToList();
                return list;
            }
        }
        //public static bool IsRequisitionSent(int AuthId)
        //  {
        //    var empid = GetEmployeeIdFromAdmin(AuthId);
        //    Purchase_Order_Message purordmes;
        //    using (var _context = new Entities())
        //    {
        //        purordmes = _context.Purchase_Order_Message.Where(x => x.forwarded_to == empid ).FirstOrDefault();
        //    }
        //    return purordmes != null ? true : false;
        //}
        public static List<string> getInRequisitionMessageStatusByBranchId(int branchid, int userid)
        {
            var Date = Convert.ToDateTime("2021-09-10");//new system start date so
            using (var _context = new Entities())
            {
                var list = _context.IN_Requisition_Message.Where(x => x.branch_id == branchid && x.Requ_date > Date && x.Requ_by == userid).Select(x => x.status.Trim()).ToList();
                return list;
            }
        }

        public static bool HasPendingAcknowledgmentsForBranch(int branchId)
        {
            //var date = Convert.ToDateTime("2021-09-10"); // Example system start date
            using (var _context = new Entities())
            {
                // Check if there are any requisition messages with "Full Dispatched" status for the branch
                return _context.IN_Requisition_Message
                               .Any(x => x.branch_id == branchId && x.status.Trim() == "Full Dispatched");
            }
        }

        public static int GetEmployeeRoleId(int empid)
        {
            using (var _context = new Entities())
            {
                var role = _context.user_role.FirstOrDefault(x => x.user_id == empid);
                if (role != null)
                {
                    return role.role_id;
                }
                return 0;

            }
        }

        private static string NormalizeWhitespace(string input)
        {
            if (input == null)
                return null;

            // Replace all whitespace characters with a single space
            return Regex.Replace(input, @"\s+", " ").Trim();
        }
    }
}
