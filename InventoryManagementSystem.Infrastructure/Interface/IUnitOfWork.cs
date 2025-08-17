using System;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        ICompanyRepo ICompany { get; }
        IInItemRepo InItem { get; }
        IInProductRepo InProduct { get; }
        IVendorRepo InVendor { get; }
        IBranchRepo InBranch { get; }
        IVendorAssignRepo InVendorAssign { get; }
        IBranchAssignRepo InBranchAssign { get; }
        IPurchaseOrderMessageRepo PurchaseOrderMessage { get; }
        IPurchaseOrderMessageHistoryRepo PurchaseOrderMessageHistory { get; }
        IPurchaseOrderRepo PurchaseOrder { get; }
        IInPurchaseReturnRepo PurchaseReturn { get; }
        IInPurchaseReturnDetailsRepo PurchaseReturnDetails { get; }
        IInPurchaseRepo InPurchase { get; }
        ITempPurchaseRepo TempPurchase { get; }
        ITempPurchaseOtherRepo TempPurchaseOther { get; }
        IStaticTempDispatchRepo StaticTempDispatch { get; }
        ITempDisposeRepo TempDispose { get; }
        ITempDisposeOtherRepo TempDisposeOther { get; }
        IBillInfoRepo BillInfo { get; }
        IBillSettingRepo BillSetting { get; }
        IOtherBillsInfoRepo OtherBillsInfo { get; }
        IInRequisitionMessageRepo InRequisitionMessage { get; }
        IInRequisitionRepo InRequisition { get; }
        IInRequisitionDetailRepo InRequisitionDetail { get; }
        IInRequisitionDetailOtherRepo InRequisitionDetailOther { get; }
        IInReceivedMessageRepo InReceivedMessage { get; }
        IInReceivedRepo InReceived { get; }
        IInDispatchMessageRepo InDispatchMessage { get; }
        IInDispatchRepo InDispatch { get; }
        IDepartmentRepo InDepartment { get; }
        IEmployeeRepo Employee { get; }
        IEmployeeContractRepo EmployeeContract { get; }
        IAdminRepo Admin { get; }
       
        IEmployeeLogRepo EmployeeLog { get; }
        IInTempRequisitionRepo InTempRequisition { get; }
        ISerialProductStockRepo SerialProductStock { get; }
        ISerialProductStockHistoryRepo SerialProductStockHistory { get; }
        ITempReturnProductRepo TempReturnProduct { get; }
        ISerialProductTransferBranchRepo SerialProductTransferBranch { get; }
        IAssetItemRepo AssetItem { get; }
        IAssetProductRepo AssetProduct { get; }
        IAssetBranchRepo AssetBranch { get; }
        IUserRoleRepo UserRole { get; }
        IRoleDetailsRepo RoleDetails { get; }
        IUserFunctionRepo UserFunction { get; }
        IStaticDataDetailRepo StaticDataDetail { get; }
        IStaticDataTypeRepo StaticDataType { get; }
        IAssetRequisitionMessageRepo AssetRequisitionMessage { get; }
        IAssetRequisitionRepo AssetRequisition { get; }
        IAssetInventoryTempRepo AssetInventoryTemp { get; }
        IChangesApprovalQueueRepo ChangesApprovalQueue { get; }
        IAssetNumberSequenceRepo AssetNumberSequence { get; }
        INotificationTypeRepo NotificationType { get; }
        INotificationRepo Notification { get; }
        INotificationUserRepo NotificationUser { get; }
        IInNotificationsRepo InNotifications { get; }
        IInDisposeMessageRepo InDisposeMessage { get; }
        IInDisposeDetailsRepo InDisposeDetails { get; }
        ISuperVisorAssignmentRepo SuperVisorAssignment { get; }
        IFuelRequestRepo FuelRequest { get; }
        IFuelRequestMessageRepo FuelRequestMessage { get; }
      
        int Save();
        Task<int> SaveAsync();
    }
}
