using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace InventoryManagementSystem.Infrastructure.Interface
{
    public interface IDropDownList : IDisposable
    {
        List<SelectListItem> FiscalYearList();
        List<SelectListItem> EmployeeListFuel(int empid);
        List<SelectListItem> BranchWiseSupervisorList(int bid);
        List<SelectListItem> EmployeeList();
        List<SelectListItem> EmployeeListBranchWise(int bid);
        List<SelectListItem> EmployeeListBranchWiseSuperVisor(int bid,int userid);
        List<SelectListItem> EmployeeListDirectDispatchForBranch(int bid);
        List<SelectListItem> SuperVisorAssignForEmployee(int bid,int empid);
        List<SelectListItem> UserAccessList();
        List<SelectListItem> UserNameList();
        List<SelectListItem> PaymentStatusList();
        List<SelectListItem> AquisitionTypeList();
        List<SelectListItem> ItemList();
        List<SelectListItem> CountryList();
        List<SelectListItem> ProvinceList();
        List<SelectListItem> MunicipalityList();
        List<SelectListItem> DistrictList();
        List<SelectListItem> DistricWithProvinceList(string provincename);
        List<SelectListItem> VendorList();
        List<SelectListItem> BranchList();
        List<SelectListItem> BranchList(int branchId);
        List<SelectListItem> BranchListForAuditor();
        List<SelectListItem> BranchListForRequestWithBranch();
        List<SelectListItem> BranchListForDirectDispatchForBranch(int branchid);
        List<SelectListItem> ProductGroupList();
        List<SelectListItem> ProductGroupListForTransferProduct(int parentId);
        List<SelectListItem> ProductGroupListForplaceRequisition(int groupid);
        List<SelectListItem> ProductList();
        List<SelectListItem> ProductNameFromInBranchAssignList(int branchid);
        List<SelectListItem> NonPrintingProductNameFromInBranchAssignList(int branchid);
        List<SelectListItem> PrintingProductNameFromInBranchAssignList(int branchid);
        List<SelectListItem> SerialProductList();
        List<SelectListItem> ProductWithProductGroupList(int groupid);
        List<SelectListItem> ProductWithProductGroupAndBranchAssignList(int groupid,int branchid);
        List<SelectListItem> ProductNameFromVendorList();
        List<SelectListItem> ProductNameFromVendorList(int vendorname);
        List<SelectListItem> PriorityList();
        List<SelectListItem> DepartmentList();
        List<SelectListItem> DepartmentShortNameList();
        List<SelectListItem> SalutationList();
        List<SelectListItem> GenderList();
        List<SelectListItem> MaritalStatusList();
        List<SelectListItem> PositionList();
        List<SelectListItem> FunctionalTitleList();
        List<SelectListItem> SalaryTitleList();
        List<SelectListItem> EmployeeStatusList();
        List<SelectListItem> BloodGroupList();
        List<SelectListItem> EmployeeTypeList();
        List<SelectListItem> RelationshipList();
        List<SelectListItem> DepartmentFromBranchList(int branchid);
        List<SelectListItem> GetEmployeeNameForBranchAndDepartmentList(int branchid,int deptid);
        List<SelectListItem> UserRoleList();
        List<SelectListItem> AssetGroupList();
        List<SelectListItem> VehicleCategoryList();
        List<SelectListItem> FuelCategoryList();
        List<SelectListItem> FuelVendorList();
        List<SelectListItem> AssetProductList();
        List<SelectListItem> AssetStatusList();
        List<SelectListItem> SeparatorList();
        List<SelectListItem> DateFormatList();
        List<SelectListItem> ApproverAssignForEmployee(int empid);
        List<SelectListItem> BranchWiseSupervisorandAdminsList(int bid);
        List<SelectListItem> FuelStatusList();
    }
}
