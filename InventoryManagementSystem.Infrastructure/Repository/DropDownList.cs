using InventoryManagementSystem.Data;
using InventoryManagementSystem.Data.Models;
using InventoryManagementSystem.Infrastructure.Interface;
using InventoryManagementSystem.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagementSystem.Infrastructure.Repository
{
    public class DropDownList : IDropDownList
    {
        private Entities _context;
        public DropDownList()
        {
            _context = new Entities();
        }

        #region FiscalYearList
        public List<SelectListItem> FiscalYearList()
        {
            var list = _context.FiscalYear.ToList();
            var selectList = new List<SelectListItem>();
            list.ForEach(x => selectList.Add(new SelectListItem { Text = x.FISCAL_YEAR_NEPALI, Value = x.FISCAL_YEAR_ID.ToString() }));
            return selectList;
        }
        #endregion

        #region EmployeeList
        public List<SelectListItem> EmployeeList()
        {
            var Session = HttpContext.Current.Session;
            var bid = Convert.ToInt32(Session["BranchId"]);
            var adminid = Convert.ToInt32(Session["AuthId"]);
            var empid = CodeService.GetEmployeeIdFromAdmin(adminid);
            var empstatus = _context.StaticDataDetail.OrderByDescending(x => x.ROWID).FirstOrDefault(x => x.DETAIL_TITLE.Trim() == "Active").ROWID.ToString();

            if (empid == 1211 || empid == 1000)
            {
                var list = _context.Employee.Where(x => x.BRANCH_ID == bid && x.EMP_STATUS == empstatus).ToList();//458
                var selectList = new List<SelectListItem>();
                list.ForEach(x => selectList.Add(new SelectListItem { Text = x.FIRST_NAME + " " + x.MIDDLE_NAME + " " + x.LAST_NAME, Value = x.EMPLOYEE_ID.ToString() }));
                return selectList;
            }
            else
            {
                var list = _context.Employee.Where(x => x.BRANCH_ID == bid && x.EMPLOYEE_ID != empid && x.EMP_STATUS == empstatus).ToList();
                var selectList = new List<SelectListItem>();
                list.ForEach(x => selectList.Add(new SelectListItem { Text = x.FIRST_NAME + " " + x.MIDDLE_NAME + " " + x.LAST_NAME, Value = x.EMPLOYEE_ID.ToString() }));
                return selectList;
            }
        }
        #endregion
        #region EmployeeListFuel
        public List<SelectListItem> EmployeeListFuel(int empid)
        {
            var empstatus = _context.StaticDataDetail.OrderByDescending(x => x.ROWID).FirstOrDefault(x => x.DETAIL_TITLE.Trim() == "Active").ROWID.ToString();
            var list = new List<Employee>();
            if (empid > 0)
            {
                list = _context.Employee.Where(x => x.EMPLOYEE_ID == empid).ToList();
            }
            else
            {
                list = _context.Employee.ToList();
            }
            var selectList = new List<SelectListItem>();
            list.ForEach(x => selectList.Add(new SelectListItem { Text = x.FIRST_NAME + " " + x.MIDDLE_NAME + " " + x.LAST_NAME, Value = x.EMPLOYEE_ID.ToString() }));
            return selectList;
        }
        #endregion
        #region BranchWiseSupervisorListForFuelRecommend
        public List<SelectListItem> BranchWiseSupervisorList(int bid)
        {
            var empstatus = _context.StaticDataDetail.OrderByDescending(x => x.ROWID).FirstOrDefault(x => x.DETAIL_TITLE.Trim() == "Active").ROWID.ToString();

            var list = _context.Employee
                     .Join(_context.Admins, emp => emp.EMPLOYEE_ID, ad => ad.Name, (emp, ad) => new { emp, ad })
                     .Join(_context.user_role, ad => ad.ad.AdminID, ur => ur.user_id, (ad, ur) => new { ad, ur })
                     .Join(_context.StaticDataDetail, ur => ur.ur.role_id, std => std.ROWID, (ur, std) => new { ur, std })
                     .Where(x => x.ur.ad.emp.BRANCH_ID == bid && (x.std.DETAIL_TITLE == "Supervisor" || x.std.DETAIL_TITLE == "Admin_User") && x.ur.ad.emp.EMP_STATUS == empstatus)
                     .Select(x => new SelectListItem
                     {
                         Text = x.ur.ad.emp.FIRST_NAME + " " + x.ur.ad.emp.MIDDLE_NAME + " " + x.ur.ad.emp.LAST_NAME,
                         Value = x.ur.ad.emp.EMPLOYEE_ID.ToString()
                     }).ToList();

            return list;
        }
        #endregion
        #region EmployeeList
        public List<SelectListItem> EmployeeListDirectDispatchForBranch(int bid)
        {
            var Session = HttpContext.Current.Session;
            var adminid = Convert.ToInt32(Session["AuthId"]);
            var empid = CodeService.GetEmployeeIdFromAdmin(adminid);
            var empstatus = _context.StaticDataDetail.OrderByDescending(x => x.ROWID).FirstOrDefault(x => x.DETAIL_TITLE.Trim() == "Active").ROWID.ToString();
            if (empid == 1211 || empid == 1000)
            {
                var list = _context.Employee.Where(x => x.BRANCH_ID == bid && x.EMP_STATUS == empstatus).ToList();
                var selectList = new List<SelectListItem>();
                list.ForEach(x => selectList.Add(new SelectListItem { Text = x.FIRST_NAME + " " + x.MIDDLE_NAME + " " + x.LAST_NAME, Value = x.EMPLOYEE_ID.ToString() }));
                return selectList;
            }
            else
            {
                var list = _context.Employee.Where(x => x.BRANCH_ID == bid && x.EMPLOYEE_ID != empid && x.EMP_STATUS == empstatus).ToList();
                var selectList = new List<SelectListItem>();
                list.ForEach(x => selectList.Add(new SelectListItem { Text = x.FIRST_NAME + " " + x.MIDDLE_NAME + " " + x.LAST_NAME, Value = x.EMPLOYEE_ID.ToString() }));
                return selectList;
            }
        }
        #endregion
        #region SuperVisorAssignForEmployee
        public List<SelectListItem> SuperVisorAssignForEmployee(int bid, int empid)
        {
            var data = _context.SuperVisroAssignment
                      .Join(_context.Employee, sa => sa.SUPERVISOR, emp => emp.EMPLOYEE_ID, (sa, emp) => new { sa, emp })
                      .Where(x => x.emp.EMP_STATUS == "458" && x.sa.EMP == empid && x.sa.record_status == "y" && x.emp.BRANCH_ID == bid)
                      .Select(x => new
                      {
                          SupervisorId = x.sa.SUPERVISOR,
                          SupervisorFullName = x.emp
                      }).ToList();

            // Apply the method to the fetched data
            var list = data.Select(x => new SelectListItem
            {
                Text = CodeService.GetEmployeeFullName(Convert.ToInt32(x.SupervisorId)),
                Value = x.SupervisorId.ToString()
            }).ToList();

            return list;
            //var list = _context.SuperVisroAssignment.Where(x => x.EMP == empid && x.record_status=="y").ToList();
            //var selectList = new List<SelectListItem>();
            //list.ForEach(x => selectList.Add(new SelectListItem { Text =CodeService.GetEmployeeFullName((int)x.SUPERVISOR), Value = x.SUPERVISOR.ToString() }));
            //return selectList;
        }
        #endregion
        #region EmployeeListBranchWise
        public List<SelectListItem> EmployeeListBranchWise(int bid)
        {
            var empstatus = _context.StaticDataDetail.OrderByDescending(x => x.ROWID).FirstOrDefault(x => x.DETAIL_TITLE.Trim() == "Active").ROWID.ToString();
            var list = _context.Employee.Where(x => x.BRANCH_ID == bid && x.EMP_STATUS == empstatus).ToList();
            var selectList = new List<SelectListItem>();
            list.ForEach(x => selectList.Add(new SelectListItem { Text = x.FIRST_NAME + " " + x.MIDDLE_NAME + " " + x.LAST_NAME, Value = x.EMPLOYEE_ID.ToString() }));
            return selectList;
        }
        #endregion
        #region EmployeeListBranchWise
        public List<SelectListItem> EmployeeListBranchWiseSuperVisor(int bid, int userid)
        {
            var empstatus = _context.StaticDataDetail.OrderByDescending(x => x.ROWID).FirstOrDefault(x => x.DETAIL_TITLE.Trim() == "Active").ROWID.ToString();
            var list = _context.Employee.Where(x => x.BRANCH_ID == bid && x.EMPLOYEE_ID == userid && x.EMP_STATUS == empstatus).ToList();
            var selectList = new List<SelectListItem>();
            list.ForEach(x => selectList.Add(new SelectListItem { Text = x.FIRST_NAME + " " + x.MIDDLE_NAME + " " + x.LAST_NAME, Value = x.EMPLOYEE_ID.ToString() }));
            return selectList;
        }
        #endregion
        #region UserAccessList
        public List<SelectListItem> UserAccessList()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "All", Value = "All" });
            list.Add(new SelectListItem { Text = "All Branch Access", Value = "All Branch Access" });
            list.Add(new SelectListItem { Text = "HO & Self Branch Access", Value = "HO & Self Branch Access" });
            list.Add(new SelectListItem { Text = "Self Branch Only", Value = "Self Branch Only" });
            return list;
        }
        #endregion

        #region UserNameList
        public List<SelectListItem> UserNameList()
        {
            var list = new List<SelectListItem>();
            var selectList = _context.Employee.ToList();
            list.Add((new SelectListItem { Text = "All", Value = "All" }));
            selectList.ForEach(x => list.Add(new SelectListItem { Text = x.FIRST_NAME + " " + x.MIDDLE_NAME + " " + x.LAST_NAME, Value = x.FIRST_NAME + " " + x.MIDDLE_NAME + " " + x.LAST_NAME }));
            return list;
        }
        #endregion

        #region PaymentStatusList
        public List<SelectListItem> PaymentStatusList()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "All", Value = "All" });
            list.Add(new SelectListItem { Text = "Paid", Value = "Paid" });
            list.Add(new SelectListItem { Text = "Unpaid", Value = "Unpaid" });
            return list;
        }
        #endregion

        #region AquisitionTypeList
        public List<SelectListItem> AquisitionTypeList()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "All", Value = "All" });
            //list.Add(new SelectListItem { Text = "Requested", Value = "Requested" });
            //list.Add(new SelectListItem { Text = "Recommended", Value = "Recommended" });
            //list.Add(new SelectListItem { Text = "Approved", Value = "Approved" });
            list.Add(new SelectListItem { Text = "Full Dispatched", Value = "Full Dispatched" });
            //list.Add(new SelectListItem { Text = "Not Acknowledge", Value = "Not Acknowledge" });
            list.Add(new SelectListItem { Text = "Full Acknowledge", Value = "Full Acknowledge" });
            return list;
        }
        #endregion

        #region InItemList
        public List<SelectListItem> ItemList()
        {
            var list = _context.IN_ITEM.ToList();
            var selectList = new List<SelectListItem>();
            list.ForEach(x => selectList.Add(new SelectListItem { Text = x.item_name, Value = x.id.ToString() }));
            return selectList;
        }
        #endregion

        #region CountryList
        public List<SelectListItem> CountryList()
        {
            var list = _context.StaticDataDetail.Where(x => x.DETAIL_TITLE.Contains("Nepal") && x.IsActive == true).ToList();
            var selectList = new List<SelectListItem>();
            list.ForEach(x => selectList.Add(new SelectListItem { Text = x.DETAIL_TITLE, Value = x.DETAIL_TITLE }));
            return selectList;
        }
        #endregion

        #region ProvinceList
        public List<SelectListItem> ProvinceList()
        {
            var list = new List<SelectListItem>();
            var selectList = _context.StaticDataDetail.Where(x => x.TYPE_ID == 103 && x.IsActive == true).ToList();
            list.Add((new SelectListItem { Text = "All", Value = "0" }));
            selectList.ForEach(x => list.Add(new SelectListItem { Text = x.DETAIL_TITLE, Value = x.DETAIL_TITLE }));
            return list;
        }
        #endregion

        #region MunicipalityList
        public List<SelectListItem> MunicipalityList()
        {
            var list = new List<SelectListItem>();
            var selectList = _context.StaticDataDetail.Where(x => x.TYPE_ID == 110 && x.IsActive == true).ToList();
            list.Add((new SelectListItem { Text = "All", Value = "0" }));
            selectList.ForEach(x => list.Add(new SelectListItem { Text = x.DETAIL_TITLE, Value = x.DETAIL_TITLE }));
            return list;
        }
        #endregion

        #region DistrictList
        public List<SelectListItem> DistrictList()
        {
            var list = _context.StaticDataDetail.Where(x => x.isdistrict == true && x.IsActive == true).ToList();
            var selectList = new List<SelectListItem>();
            list.ForEach(x => selectList.Add(new SelectListItem { Text = x.DETAIL_TITLE, Value = x.DETAIL_TITLE }));
            return selectList;
        }
        #endregion

        #region DistrictWithZoneList
        public List<SelectListItem> DistricWithProvinceList(string provincename)
        {
            var text = _context.StaticDataDetail.Where(x => x.DETAIL_TITLE == provincename).Select(x => x.value).FirstOrDefault();
            var list = _context.StaticDataDetail.Where(x => (x.value == text.ToString() && x.isdistrict == true && x.IsActive == true)).ToList();
            var selectList = new List<SelectListItem>();
            list.ForEach(x => selectList.Add(new SelectListItem { Text = x.DETAIL_TITLE, Value = x.DETAIL_TITLE }));
            return selectList;
        }
        #endregion

        #region VendorList
        public List<SelectListItem> VendorList()
        {
            var list = _context.Customer.Where(x => x.IsActive == true).OrderBy(x => x.CustomerName.Trim()).ToList();
            var selectList = new List<SelectListItem>();
            list.ForEach(x => selectList.Add(new SelectListItem { Text = x.CustomerName, Value = x.ID.ToString() }));
            return selectList;
        }
        #endregion

        #region BranchList
        public List<SelectListItem> BranchList()
        {
            var list = new List<SelectListItem>();
            var selectList = _context.Branches.Where(x => x.Is_Active == true).ToList();
            list.Add((new SelectListItem { Text = "All", Value = "0" }));
            selectList.ForEach(x => list.Add(new SelectListItem { Text = x.BRANCH_NAME, Value = x.BRANCH_ID.ToString() }));
            return list;
        }

        public List<SelectListItem> BranchList(int branchId)
        {
            var list = new List<SelectListItem>();

            // Fetch only the branch that matches the branchId
            var userBranch = _context.Branches
                                     .Where(x => x.Is_Active && x.BRANCH_ID == branchId)
                                     .FirstOrDefault();

            if (userBranch != null)
            {
                list.Add(new SelectListItem { Text = userBranch.BRANCH_NAME, Value = userBranch.BRANCH_ID.ToString() });
            }

            return list;
        }
        #endregion
        #region BranchList
        public List<SelectListItem> BranchListForAuditor()
        {
            var list = new List<SelectListItem>();
            var selectList = _context.Branches.Where(x => x.Is_Active == true && x.BRANCH_ID != 12).ToList();
            list.Add((new SelectListItem { Text = "All", Value = "0" }));
            selectList.ForEach(x => list.Add(new SelectListItem { Text = x.BRANCH_NAME, Value = x.BRANCH_ID.ToString() }));
            return list;
        }
        #endregion
        #region BranchList
        public List<SelectListItem> BranchListForRequestWithBranch()
        {
            var list = new List<SelectListItem>();
            var selectList = _context.Branches.Where(x => x.Is_Active == true && x.BRANCH_ID == 12).ToList();
            selectList.ForEach(x => list.Add(new SelectListItem { Text = x.BRANCH_NAME, Value = x.BRANCH_ID.ToString() }));
            return list;
        }
        #endregion

        #region BranchListDirectDispatchForBranch
        public List<SelectListItem> BranchListForDirectDispatchForBranch(int branchid)
        {
            var list = new List<SelectListItem>();
            var selectList = _context.Branches.Where(x => x.Is_Active == true && x.Is_Active == true && x.BRANCH_ID == branchid).ToList();
            selectList.ForEach(x => list.Add(new SelectListItem { Text = x.BRANCH_NAME, Value = x.BRANCH_ID.ToString() }));
            return list;
        }
        #endregion

        #region ProductGroupList
        public List<SelectListItem> ProductGroupList()
        {
            var list = new List<SelectListItem>();
            var selectList = _context.IN_ITEM.Where(x => x.parent_id == 1 && x.Is_Active == true).ToList();
            list.Add((new SelectListItem { Text = "All", Value = "0" }));
            selectList.ForEach(x => list.Add(new SelectListItem { Text = x.item_name, Value = x.id.ToString() }));
            return list;
        }
        #endregion
        #region ProductGroupListForTransferProduct
        public List<SelectListItem> ProductGroupListForTransferProduct(int parentid)
        {
            var list = new List<SelectListItem>();
            var selectList = _context.IN_ITEM.Where(x => x.parent_id == 1 && x.Is_Active == true && x.id != parentid).ToList();
            selectList.ForEach(x => list.Add(new SelectListItem { Text = x.item_name, Value = x.id.ToString() }));
            return list;
        }
        #endregion
        #region ProductGroupListForPlaceRequisition
        public List<SelectListItem> ProductGroupListForplaceRequisition(int groupid)
        {
            var list = new List<SelectListItem>();
            var selectList = _context.IN_ITEM.Where(x => x.parent_id == 1 && x.id == groupid && x.Is_Active == true).ToList();
            list.Add((new SelectListItem { Text = "All", Value = "0" }));
            selectList.ForEach(x => list.Add(new SelectListItem { Text = x.item_name, Value = x.id.ToString() }));
            return list;
        }
        #endregion  

        #region ProductList
        public List<SelectListItem> ProductList()
        {
            var list = _context.IN_PRODUCT.Where(x => x.is_active == true).OrderBy(x => x.porduct_code.Trim()).ToList();
            var selectList = new List<SelectListItem>();
            list.ForEach(x => selectList.Add(new SelectListItem { Text = x.porduct_code + '|' + x.id, Value = x.id.ToString() }));
            return selectList;
        }
        #endregion

        #region SerialProductList
        public List<SelectListItem> SerialProductList()
        {
            var list = _context.IN_PRODUCT.Where(x => x.is_active == true && x.serial_no == true).OrderBy(x => x.porduct_code.Trim()).ToList();
            var selectList = new List<SelectListItem>();
            list.ForEach(x => selectList.Add(new SelectListItem { Text = x.porduct_code + '|' + x.id, Value = x.id.ToString() }));
            return selectList;
        }
        #endregion

        #region ProductWithProductGroupList
        public List<SelectListItem> ProductWithProductGroupList(int groupid)
        {
            var text = _context.IN_ITEM.Where(x => x.id == groupid).Select(x => x.id).FirstOrDefault();
            var list = _context.IN_ITEM.Where(x => (x.parent_id == text && x.is_product == true && x.Is_Active == true)).OrderBy(x => x.item_name.Trim()).ToList();
            var selectList = new List<SelectListItem>();
            selectList.Add((new SelectListItem { Text = "All", Value = "0" }));
            list.ForEach(x => selectList.Add(new SelectListItem { Text = x.item_name + '|' + x.IN_PRODUCT.FirstOrDefault().id, Value = x.IN_PRODUCT.FirstOrDefault().id.ToString() }));
            return selectList;
        }
        #endregion

        #region ProductWithProductGroupAndBranchAssignList
        public List<SelectListItem> ProductWithProductGroupAndBranchAssignList(int groupid, int branchid)
        {
            var text = _context.IN_ITEM.Where(x => x.id == groupid).Select(x => x.id).FirstOrDefault();
            var list = _context.IN_BRANCH.Where(x => (x.IN_PRODUCT.IN_ITEM.parent_id == text && x.IN_PRODUCT.IN_ITEM.is_product == true && x.IN_PRODUCT.is_active == true && x.IS_ACTIVE == true && x.BRANCH_ID == branchid)).OrderBy(x => x.IN_PRODUCT.IN_ITEM.item_name.Trim()).ToList();
            var selectList = new List<SelectListItem>();
            selectList.Add((new SelectListItem { Text = "All", Value = "0" }));
            list.ForEach(x => selectList.Add(new SelectListItem { Text = x.IN_PRODUCT.IN_ITEM.item_name + '|' + x.IN_PRODUCT.id, Value = x.IN_PRODUCT.id.ToString() }));
            return selectList;
        }
        #endregion
        #region ProductNameFromVendorList
        public List<SelectListItem> ProductNameFromVendorList()
        {
            var list = new List<SelectListItem>();
            var listp = _context.Vendor_Bid_Price.Take(2).ToList();
            listp.ForEach(x => list.Add(new SelectListItem { Text = x.IN_PRODUCT.porduct_code, Value = x.IN_PRODUCT.id.ToString() }));
            return list;
        }
        #endregion

        #region ProductNameFromVendorListwithparameter
        public List<SelectListItem> ProductNameFromVendorList(int vendorname)
        {
            var list = new List<SelectListItem>();
            var listp = _context.Vendor_Bid_Price.Where(x => x.vendor_id == vendorname && x.IN_PRODUCT.is_active == true).OrderBy(x => x.IN_PRODUCT.porduct_code.Trim()).ToList();
            listp.ForEach(x => list.Add(new SelectListItem { Text = x.IN_PRODUCT.porduct_code + '|' + x.IN_PRODUCT.id, Value = x.IN_PRODUCT.id.ToString() }));
            return list;
        }
        #endregion
        #region ProductNameFromInBranchAssign
        public List<SelectListItem> ProductNameFromInBranchAssignList(int branchid)
        {
            var list = new List<SelectListItem>();
            var listp = _context.IN_BRANCH.Where(x => x.BRANCH_ID == branchid && x.IN_PRODUCT.is_active == true && x.IS_ACTIVE == true).OrderBy(x => x.IN_PRODUCT.porduct_code.Trim()).ToList();
            listp.ForEach(x => list.Add(new SelectListItem { Text = x.IN_PRODUCT.porduct_code + '|' + x.IN_PRODUCT.id, Value = x.IN_PRODUCT.id.ToString() }));
            return list;
        }
        #endregion
        #region NonPrintingProductNameFromInBranchAssign
        public List<SelectListItem> NonPrintingProductNameFromInBranchAssignList(int branchid)
        {
            var list = new List<SelectListItem>();
            var listp = _context.IN_BRANCH.Where(x => x.BRANCH_ID == branchid && x.IS_ACTIVE == true && x.IN_PRODUCT.is_active == true && x.IN_PRODUCT.serial_no == false && x.IN_PRODUCT.IN_ITEM.parent_id == 2).OrderBy(x => x.IN_PRODUCT.porduct_code.Trim()).ToList();
            listp.ForEach(x => list.Add(new SelectListItem { Text = x.IN_PRODUCT.porduct_code + '|' + x.IN_PRODUCT.id, Value = x.IN_PRODUCT.id.ToString() }));
            return list;
        }
        #endregion

        #region PrintingProductNameFromInBranchAssign
        public List<SelectListItem> PrintingProductNameFromInBranchAssignList(int branchid)
        {
            var list = new List<SelectListItem>();
            var listp = _context.IN_BRANCH.Where(x => x.BRANCH_ID == branchid && x.IN_PRODUCT.is_active == true && x.IS_ACTIVE == true && x.IN_PRODUCT.serial_no == false && x.IN_PRODUCT.IN_ITEM.parent_id == 3).OrderBy(x => x.IN_PRODUCT.porduct_code.Trim()).ToList();
            listp.ForEach(x => list.Add(new SelectListItem { Text = x.IN_PRODUCT.porduct_code + '|' + x.IN_PRODUCT.id, Value = x.IN_PRODUCT.id.ToString() }));
            return list;
        }
        #endregion

        #region PriotityList
        public List<SelectListItem> PriorityList()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "Normal", Value = "1" });
            list.Add(new SelectListItem { Text = "Low", Value = "2" });
            list.Add(new SelectListItem { Text = "High", Value = "3" });
            return list;
        }
        #endregion

        #region DepartmentList
        public List<SelectListItem> DepartmentList()
        {
            var list = _context.StaticDataDetail.Where(x => x.isdepartment == true && x.IsActive == true).ToList();
            var selectList = new List<SelectListItem>
             {
                new SelectListItem { Text = "All", Value = "0" }
             };
            list.ForEach(x => selectList.Add(new SelectListItem { Text = x.DETAIL_TITLE, Value = x.ROWID.ToString() }));
            return selectList;
        }
        #endregion

        #region DepartmentShortNameList
        public List<SelectListItem> DepartmentShortNameList()
        {
            var list = _context.StaticDataDetail.Where(x => x.isdepartment == true).ToList();
            var selectList = new List<SelectListItem>();
            list.ForEach(x => selectList.Add(new SelectListItem { Text = x.DETAIL_DESC, Value = x.ROWID.ToString() }));
            return selectList;
        }
        #endregion

        #region SalutationList
        public List<SelectListItem> SalutationList()
        {
            var list = _context.StaticDataDetail.Where(x => x.TYPE_ID == 43).ToList();
            var selectList = new List<SelectListItem>();
            list.ForEach(x => selectList.Add(new SelectListItem { Text = x.DETAIL_TITLE, Value = x.ROWID.ToString() }));
            return selectList;
        }
        #endregion

        #region GenderList
        public List<SelectListItem> GenderList()
        {
            var list = _context.StaticDataDetail.Where(x => x.TYPE_ID == 5).ToList();
            var selectList = new List<SelectListItem>();
            list.ForEach(x => selectList.Add(new SelectListItem { Text = x.DETAIL_TITLE, Value = x.ROWID.ToString() }));
            return selectList;
        }
        #endregion

        #region MaritalStatusList
        public List<SelectListItem> MaritalStatusList()
        {
            var list = _context.StaticDataDetail.Where(x => x.TYPE_ID == 11).ToList();
            var selectList = new List<SelectListItem>();
            selectList.Add((new SelectListItem { Text = "--Select--", Value = " " }));
            list.ForEach(x => selectList.Add(new SelectListItem { Text = x.DETAIL_TITLE, Value = x.ROWID.ToString() }));
            return selectList;
        }
        #endregion

        #region PositionList
        public List<SelectListItem> PositionList()
        {
            var list = _context.StaticDataDetail.Where(x => x.TYPE_ID == 4).ToList();
            var selectList = new List<SelectListItem>();
            list.ForEach(x => selectList.Add(new SelectListItem { Text = x.DETAIL_TITLE, Value = x.ROWID.ToString() }));
            return selectList;
        }
        #endregion

        #region FunctionalTitleList
        public List<SelectListItem> FunctionalTitleList()
        {
            var list = _context.StaticDataDetail.Where(x => x.TYPE_ID == 59).ToList();
            var selectList = new List<SelectListItem>();
            selectList.Add((new SelectListItem { Text = "--Select--", Value = " " }));
            list.ForEach(x => selectList.Add(new SelectListItem { Text = x.DETAIL_TITLE, Value = x.ROWID.ToString() }));
            return selectList;
        }
        #endregion

        #region SalaryTitleList
        public List<SelectListItem> SalaryTitleList()
        {
            var list = _context.StaticDataDetail.Where(x => x.TYPE_ID == 98).ToList();
            var selectList = new List<SelectListItem>();
            selectList.Add((new SelectListItem { Text = "--Select--", Value = " " }));
            list.ForEach(x => selectList.Add(new SelectListItem { Text = x.DETAIL_TITLE, Value = x.ROWID.ToString() }));
            return selectList;
        }
        #endregion

        #region EmployeeStatusList
        public List<SelectListItem> EmployeeStatusList()
        {
            var list = _context.StaticDataDetail.Where(x => x.TYPE_ID == 49).ToList();
            var selectList = new List<SelectListItem>();
            list.ForEach(x => selectList.Add(new SelectListItem { Text = x.DETAIL_TITLE, Value = x.ROWID.ToString() }));
            return selectList;
        }
        #endregion

        #region BloodGroupList
        public List<SelectListItem> BloodGroupList()
        {
            var list = _context.StaticDataDetail.Where(x => x.TYPE_ID == 6).ToList();
            var selectList = new List<SelectListItem>();
            selectList.Add((new SelectListItem { Text = "--Select--", Value = " " }));
            list.ForEach(x => selectList.Add(new SelectListItem { Text = x.DETAIL_TITLE, Value = x.ROWID.ToString() }));
            return selectList;
        }
        #endregion

        #region EmployeeTypeList
        public List<SelectListItem> EmployeeTypeList()
        {
            var list = _context.StaticDataDetail.Where(x => x.TYPE_ID == 10).ToList();
            var selectList = new List<SelectListItem>();
            selectList.Add((new SelectListItem { Text = "--Select--", Value = " " }));
            list.ForEach(x => selectList.Add(new SelectListItem { Text = x.DETAIL_TITLE, Value = x.ROWID.ToString() }));
            return selectList;
        }
        #endregion

        #region RelationshipList
        public List<SelectListItem> RelationshipList()
        {
            var list = _context.StaticDataDetail.Where(x => x.TYPE_ID == 27).ToList();
            var selectList = new List<SelectListItem>();
            selectList.Add((new SelectListItem { Text = "--Select--", Value = " " }));
            list.ForEach(x => selectList.Add(new SelectListItem { Text = x.DETAIL_TITLE, Value = x.ROWID.ToString() }));
            return selectList;
        }
        #endregion

        #region DepartmentFromBranchList
        public List<SelectListItem> DepartmentFromBranchList(int branchid)
        {
            var list = _context.departments.Where(x => x.BRANCH_ID == branchid).ToList();
            //var list = _context.departments.Where(x => (x.value == text.ToString() && x.isdistrict == true)).ToList();
            var selectList = new List<SelectListItem>();
            list.ForEach(x => selectList.Add(new SelectListItem { Text = x.DEPARTMENT_NAME, Value = x.DEPARTMENT_ID.ToString() }));
            return selectList;
        }
        #endregion
        #region MyRegion
        public List<SelectListItem> GetEmployeeNameForBranchAndDepartmentList(int branchid, int deptid)
        {
            var list = _context.Employee.Where(x => x.BRANCH_ID == branchid && x.DEPARTMENT_ID == deptid && x.EMP_STATUS == "458").ToList();
            //var list = _context.departments.Where(x => (x.value == text.ToString() && x.isdistrict == true)).ToList();
            var selectList = new List<SelectListItem>();
            list.ForEach(x => selectList.Add(new SelectListItem { Text = x.FIRST_NAME + " " + x.MIDDLE_NAME + " " + x.LAST_NAME, Value = x.EMPLOYEE_ID.ToString() }));
            return selectList;
        }
        #endregion
        #region AssetGroupList
        public List<SelectListItem> AssetGroupList()
        {
            var list = new List<SelectListItem>();
            var selectList = _context.ASSET_ITEM.Where(x => x.parent_id == 1).ToList();
            list.Add((new SelectListItem { Text = "All", Value = "0" }));
            selectList.ForEach(x => list.Add(new SelectListItem { Text = x.item_name, Value = x.id.ToString() }));
            return list;
        }
        #endregion

        #region VehicleCategoryList
        public List<SelectListItem> VehicleCategoryList()
        {
            var list = _context.StaticDataDetail.Where(x => x.TYPE_ID == 112).ToList();
            var selectList = new List<SelectListItem>();

            list.ForEach(x => selectList.Add(new SelectListItem { Text = x.DETAIL_TITLE, Value = x.DETAIL_TITLE.ToString() }));
            return selectList;
        }
        #endregion

        #region FuelCategoryList
        public List<SelectListItem> FuelCategoryList()
        {
            var list = _context.StaticDataDetail.Where(x => x.TYPE_ID == 113).ToList();
            var selectList = new List<SelectListItem>();

            list.ForEach(x => selectList.Add(new SelectListItem { Text = x.DETAIL_TITLE, Value = x.DETAIL_TITLE.ToString() }));
            return selectList;

        }
        #endregion
        #region FuelVendorList
        public List<SelectListItem> FuelVendorList()
        {
            var list = _context.StaticDataDetail.Where(x => x.TYPE_ID == 114).ToList();
            var selectList = new List<SelectListItem>();
            list.ForEach(x => selectList.Add(new SelectListItem { Text = x.DETAIL_TITLE, Value = x.DETAIL_TITLE.ToString() }));
            return selectList;

        }
        #endregion

        #region FuelStatusList
        public List<SelectListItem> FuelStatusList()
        {
            var list = _context.StaticDataDetail.Where(x => x.TYPE_ID == 115).ToList();
            var selectList = new List<SelectListItem>();
            list.ForEach(x => selectList.Add(new SelectListItem { Text = x.DETAIL_TITLE, Value = x.DETAIL_TITLE.ToString() }));
            return selectList;
        }
        #endregion

        #region UserRoleList
        public List<SelectListItem> UserRoleList()
        {
            var list = _context.StaticDataDetail.Where(x => x.TYPE_ID == 25).ToList();
            var selectList = new List<SelectListItem>();
            selectList.Add((new SelectListItem { Text = "All", Value = "0" }));
            list.ForEach(x => selectList.Add(new SelectListItem { Text = x.DETAIL_TITLE, Value = x.ROWID.ToString() }));
            return selectList;
        }
        #endregion

        #region AssetProductList
        public List<SelectListItem> AssetProductList()
        {
            var list = _context.ASSET_PRODUCT.ToList();
            var selectList = new List<SelectListItem>();
            list.ForEach(x => selectList.Add(new SelectListItem { Text = x.porduct_code, Value = x.id.ToString() }));
            return selectList;
        }
        #endregion

        #region AssetStatusList
        public List<SelectListItem> AssetStatusList()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "Active", Value = "1" });
            list.Add(new SelectListItem { Text = "InActive", Value = "2" });
            list.Add(new SelectListItem { Text = "Sold", Value = "3" });
            list.Add(new SelectListItem { Text = "Disposed", Value = "4" });
            list.Add(new SelectListItem { Text = "Write-Off", Value = "5" });
            return list;
        }
        #endregion

        #region SeparatorList
        public List<SelectListItem> SeparatorList()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "-", Value = "1" });
            list.Add(new SelectListItem { Text = "/", Value = "2" });
            return list;
        }
        #endregion

        #region DateFormatList
        public List<SelectListItem> DateFormatList()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "YY", Value = "1" });
            list.Add(new SelectListItem { Text = "YYYY", Value = "2" });
            list.Add(new SelectListItem { Text = "YYMM", Value = "3" });
            list.Add(new SelectListItem { Text = "YYMMDD", Value = "4" });
            list.Add(new SelectListItem { Text = "YYYYMMDD", Value = "5" });
            return list;
        }
        #endregion

        #region ApproverList
        public List<SelectListItem> ApproverAssignForEmployee(int empid)
        {
            //var list = _context.SuperVisroAssignment.Where(x => x.EMP == empid && x.record_status == "y").ToList();
            var query = from emp in _context.Employee
                        join ad in _context.Admins on emp.EMPLOYEE_ID equals ad.Name
                        join ur in _context.user_role on ad.AdminID equals ur.user_id
                        join std in _context.StaticDataDetail on ur.role_id equals std.ROWID

                        where std.DETAIL_TITLE == "Admin_User" ||
                              std.DETAIL_TITLE == "Administrator" ||
                              std.DETAIL_TITLE == "StoreKeeper(HeadOffice)" ||
                              std.DETAIL_TITLE == "Fuel Manager"
                        select new
                        {
                            Value = emp.EMPLOYEE_ID,
                            Text = emp.FIRST_NAME + " " + emp.MIDDLE_NAME + " " + emp.LAST_NAME
                        };

            var distinctEmp = query.Distinct().ToList();

            var selectList = new List<SelectListItem>
    {
        new SelectListItem { Text = "--Select--", Value = "0" }
    };

            foreach (var item in distinctEmp)
            {
                selectList.Add(new SelectListItem
                {
                    Text = item.Text,
                    Value = item.Value.ToString()
                });
            }

            return selectList;
        }
        #endregion

        #region Supervisors and Admins Dropdown
        public List<SelectListItem> BranchWiseSupervisorandAdminsList(int bid)
        {
            var empstatus = _context.StaticDataDetail.OrderByDescending(x => x.ROWID).FirstOrDefault(x => x.DETAIL_TITLE.Trim() == "Active").ROWID.ToString();

            var list = _context.Employee
                     .Join(_context.Admins, emp => emp.EMPLOYEE_ID, ad => ad.Name, (emp, ad) => new { emp, ad })
                     .Join(_context.user_role, ad => ad.ad.AdminID, ur => ur.user_id, (ad, ur) => new { ad, ur })
                     .Join(_context.StaticDataDetail, ur => ur.ur.role_id, std => std.ROWID, (ur, std) => new { ur, std })
                     .Where(x => x.ur.ad.emp.BRANCH_ID == bid && (x.std.DETAIL_TITLE == "Supervisor" || x.std.DETAIL_TITLE == "Admin_User") && x.ur.ad.emp.EMP_STATUS == empstatus)
                     .Select(x => new SelectListItem
                     {
                         Text = x.ur.ad.emp.FIRST_NAME + " " + x.ur.ad.emp.MIDDLE_NAME + " " + x.ur.ad.emp.LAST_NAME,
                         Value = x.ur.ad.emp.EMPLOYEE_ID.ToString()
                     }).ToList();

            return list;


        }
        #endregion


        public void Dispose()
        {
            _context.Dispose();
        }


    }
}
