using Dapper;
using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Domain.RoleSetupVM;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InventoryManagementSystem.Infrastructure.DapperRepo
{
    public class DapperRepoServices : IDisposable
    {
        private SqlConnection _dapper;

        public DapperRepoServices()
        {
            _dapper = new SqlConnection(ConfigurationManager.ConnectionStrings["JUINOAPIEntities"].ConnectionString);
        }
        public List<EmployeeVM> GetAllEmployee(int branchid, string provinceid)
        {
            try
            {
                var records = _dapper.Query<EmployeeVM>(@"select  emp.*,CONCAT(emp.FIRST_NAME ,' ', emp.MIDDLE_NAME ,' ', emp.LAST_NAME)[FullName],
	                                                    dbo.GetBranchName(emp.BRANCH_ID)[BranchName],
		                                                dbo.GetDeptName(emp.DEPARTMENT_ID)[DepartmentName],
                                                        dbo.GetDesignationName(emp.POSITION_ID)[Designation],
														sdd.DETAIL_TITLE as 'EMP_STATUS'
		                                                from Employee emp 
														join StaticDataDetail sdd on emp.EMP_STATUS=sdd.ROWID
                                                        where 1=1
                                                        and (emp.TEMP_PROVINCE=@provinceid or @provinceid=0)
                                                        and (emp.BRANCH_ID=@branchid or @branchid=0)
                                                        order by LTRIM(emp.FIRST_NAME)", new { branchid = branchid, provinceid = provinceid }).ToList();
                return records;
            }
            catch (Exception Ex)
            {

                throw new Exception(Ex.InnerException.Message);
            }
        }
        public int GetRoleId(int UserId)
        {
            try
            {
                var roleid = _dapper.Query<int>(@"select role_id from user_role where user_id=@UserId", new { UserId = UserId }).FirstOrDefault();
                return roleid;
            }
            catch (Exception Ex)
            {

                throw new Exception(Ex.InnerException.Message);
            }
        }
        public List<UserRoleVM> GetAllUserRole(int branchid, int roleid, int? depid)
        {
            try
            {
                var records = _dapper.Query<UserRoleVM>(@"select ur.*, ad.AdminID,ad.UserName,dbo.GetEmployeeFullNameOfId(emp.EMPLOYEE_ID)[FullName],
		                                                    ad.status[UserStatus],b.BRANCH_NAME[BranchName],sdd.DETAIL_TITLE[AssignedRole],dep.Department_Name [DepartmentName] 
		                                                    from user_role ur
		                                                    join Admins ad on ur.user_id=ad.AdminID
		                                                    join Employee emp on ad.Name =emp.EMPLOYEE_ID
		                                                    join Branches b on emp.BRANCH_ID=b.BRANCH_ID
		                                                    join StaticDataDetail sdd on ur.role_id=sdd.ROWID
                                                            Left join Departments dep on emp.Department_id = dep.Department_id
                                                            where ad.status=1
                                                            AND (b.BRANCH_ID=@branchid or @branchid=0)
			                                                AND (ur.role_id=@roleid or @roleid=0)
                                                           AND ((dep.Department_id = @depid AND ur.Department_id IS NOT NULL) OR @depid = 0)
                                                            order by LTRIM(emp.FIRST_NAME)", new { branchid = branchid, roleid = roleid, depid = depid }).ToList();
                return records;
            }
            catch (Exception Ex)
            {

                throw new Exception(Ex.InnerException.Message);
            }
        }
        public List<BranchVM> GetAllBranch()
        {
            try
            {
                var records = _dapper.Query<BranchVM>(@"select * from Branches order by LTRIM(BRANCH_NAME) asc ").ToList();
                return records;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.InnerException.Message);

            }
        }

        public List<PurchaseOrderMessageVM> GetAllDirectPurchaseRequested(int branchid, int userid)
        {
            try
            {
                var data = _dapper.Query<PurchaseOrderMessageVM>(@"
                            SELECT * 
                            FROM Purchase_Order_Message
                            WHERE status = 'DirectPurchaseRequested'
                            AND branch_id = @branchid
                            AND forwarded_to = @userid
                            ORDER BY Id DESC", new { userid = userid, branchid = branchid }).ToList();

                return data;
            }
            catch (Exception ex)
            {

                throw new Exception($"An error occurred while fetching direct purchase requests." + ex.InnerException.Message);
            }
        }
        public List<PurchaseOrderVM> GetItemByBillId(int id)
        {
            try
            {
                // Define the SQL query to fetch the purchase orders by bill ID
                var query = @"
            SELECT id, amount, order_message_id, product_code, qty, rate, Received_Qty, bill_id
            FROM Purchase_Order
            WHERE bill_id = @id";

                // Execute the query using Dapper and map the results to PurchaseOrderVM
                var data = _dapper.Query<PurchaseOrderVM>(query, new { id }).ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching purchase orders by bill ID: {ex.Message}", ex);
            }
        }

        public int getVoucherNo()
        {
            try
            {
                var voucherNo = _dapper.Query<int>(@"select purchase_voucher from BillSetting").FirstOrDefault();
                return voucherNo;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.InnerException.Message);

            }
        }
        public void UpdateVoucherNo()
        {
            try
            {
                _dapper.Query<int>(@"Update BillSetting set purchase_voucher=cast(purchase_voucher as int) + 1").FirstOrDefault();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.InnerException.Message);

            }
        }
        public bool UpdatePurchaseOrderById(List<PurchaseOrderVM> poList, int orderId, int approvedBy, decimal vatAmount)
        {
            using (var connection = _dapper)
            {
                try
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        // Prepare a list of parameters for bulk update
                        var parameters = poList.Select(item => new
                        {
                            Id = item.id,
                            Qty = item.Received_Qty,
                            Amount = item.amount
                        }).ToList();

                        // Execute bulk update query
                        connection.Execute(
                            @"UPDATE Purchase_Order SET Received_Qty = @Qty, amount = @Amount WHERE id = @Id;",
                            parameters,
                            transaction);

                        // Update Purchase_Order_Message
                        connection.Execute(
                            @"UPDATE Purchase_Order_Message SET vat_amt=@vatAmount, status = 'Approved', approved_date = GETDATE(), approved_by = @ApprovedBy WHERE id = @OrderId;",
                            new { ApprovedBy = approvedBy, OrderId = orderId, vatAmount = vatAmount }, transaction);

                        transaction.Commit();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    Console.WriteLine($"Error: {ex.Message}");
                    return false;
                }
            }
        }


        public void UpdateSerialProductStockDispatchRequisition(int reqmesid, int productid, int snf, int snt)
        {
            try
            {
                _dapper.Query<int>(@"update SerialProductStock set reqId=@reqmesid,branchId=NULL  where productId=@productid and sequenceNum between @snf and @snt", new { reqmesid = reqmesid, productid = productid, snf = snf, snt = snt }, commandTimeout: 2400).FirstOrDefault();

            }
            catch (Exception Ex)
            {

                throw new Exception(Ex.InnerException.Message);

            }
        }
        public void InsertSerialProductStockPurchaseOrderMessage(int branchid, int productid, int purid, int snf, int snt)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@branchid", branchid);
                param.Add("@productid", productid);
                param.Add("@purmesid", purid);
                param.Add("@snf", snf);
                param.Add("@snt", snt);
                var data = _dapper.Query<int>("SP_Inv_New_SerialProductInsertForPurchaseOrderMessage", param: param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.InnerException.Message);

            }
        }
        public void InsertSerialProductStockPurchaseVoucher(int branchid, int productid, int purid, int snf, int snt)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@branchid", branchid);
                param.Add("@productid", productid);
                param.Add("@purid", purid);
                param.Add("@snf", snf);
                param.Add("@snt", snt);
                var data = _dapper.Query<int>("SP_Inv_New_SerialProductInsert", param: param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception Ex)
            {

                throw;
            }
        }
        public void UpdateSerialProductStockBranchId(int branchId, int pid, int reqmesid)
        {
            try
            {
                _dapper.Query<int>(@"update SerialProductStock set branchId=@branchId where reqId=@reqmesid and productId=@pid", new { branchId = branchId, pid = pid, reqmesid = reqmesid }, commandTimeout: 2400).FirstOrDefault();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.InnerException.Message);

            }
        }
        public void UpdateSerialProductStockReturnBranch(int branchId, int pId, int snf, int snt)
        {
            try
            {
                _dapper.Query<int>(@"update SerialProductStock set branchId=@branchId where productId=@pId and sequenceNum between @snf and @snt", new { branchId = branchId, pId = pId, snf = snf, snt = snt }).FirstOrDefault();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.InnerException.Message);

            }
        }
        public void DeleteSerialProductStockAcknowledgeRequisition(int branchId, int pid, int reqmesid)
        {
            try
            {
                _dapper.Query<int>(@"delete from SerialProductStock where branchId=@branchId and reqId=@reqmesid and productId=@pid", new { branchId = branchId, pid = pid, reqmesid = reqmesid }).FirstOrDefault();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.InnerException.Message);

            }
        }
        public void UpdateSerialProductStockDepartmentId(int branchId, int pid, int reqmesid, int departmentid)
        {
            try
            {
                _dapper.Query<int>(@"update SerialProductStock set departmentid=@departmentid where reqId=@reqmesid and productId=@pid", new { branchId = branchId, pid = pid, reqmesid = reqmesid, departmentid = departmentid }).FirstOrDefault();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.InnerException.Message);

            }
        }
        public void DeleteSerialProductStock(int branchId, int pid, int reqmesid)
        {
            try
            {
                _dapper.Query<int>(@"delete from SerialProductStock where branchId=@branchId and disposemesid=@reqmesid and productId=@pid", new { branchId = branchId, pid = pid, reqmesid = reqmesid }).FirstOrDefault();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.InnerException.Message);

            }
        }
        public void DeleteSerialProductStockForSameBranchDispatch(int snf, int snt, int branchId, int pid)
        {
            try
            {
                _dapper.Query<int>(@"delete from SerialProductStock where sequenceNum between @snf and @snt and branchId=@branchId and productId=@pid", new { snf = snf, snt = snt, branchId = branchId, pid = pid }).FirstOrDefault();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.InnerException.Message);

            }
        }
        public void DeleteByReqMesId(int reqmesid, int prodid)
        {
            try
            {
                _dapper.Query<int>(@"delete from IN_Requisition_Detail_Other where detail_id=@reqmesid and productid=@prodid", new { reqmesid = reqmesid, prodid = prodid }).FirstOrDefault();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.InnerException.Message);

            }
        }
        //public SerialProductStockVM GetByPIdAndSequenceNo(int productid,int seqno)
        //{
        //    try
        //    {
        //        var seqnum = _dapper.Query<SerialProductStockVM>(@"select * from SerialProductStock where productId=@productid and sequenceNum=@seqno", new { productid = productid, seqno = seqno }).FirstOrDefault();
        //        return seqnum;
        //    }
        //    catch (Exception Ex)
        //    {
        //        throw;
        //    }
        //}
        public void DeleteSupervisorAssignmentId(int sId)
        {
            try
            {
                _dapper.Query<int>(@"delete from SuperVisroAssignment where SV_ASSIGN_ID=@sId", new { sId = sId }).FirstOrDefault();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.InnerException.Message);

            }
        }
        public List<DepartmentsVM> GetAllDepartment()
        {
            try
            {
                var records = _dapper.Query<DepartmentsVM>(@"select b.BRANCH_NAME[BranchName], d.* from departments d
                                                           join Branches b on d.BRANCH_ID=b.BRANCH_ID
                                                           order by LTRIM(b.BRANCH_NAME)").ToList();
                return records;
            }
            catch (Exception Ex)
            {

                throw new Exception(Ex.InnerException.Message);

            }
        }
        public List<INBranchVM> GetAllBranchAssign(int branchid, int ProductGroupId)
        {
            try
            {
                var records = _dapper.Query<INBranchVM>(@"select p.porduct_code[ProductName],b.BRANCH_NAME, ib.* from IN_BRANCH ib
                                                        join IN_PRODUCT p on ib.PRODUCT_ID=p.id
                                                        join Branches b on ib.BRANCH_ID=b.BRANCH_ID
                                                        where p.is_active=1 
														and (ib.BRANCH_ID=@branchid or @branchid=0)
                                                        AND (@ProductGroupId=0 or p.item_id IN (SELECT id FROM IN_ITEM WHERE parent_id=@ProductGroupId))
                                                        order by LTRIM(p.porduct_code)", new { branchid = branchid, ProductGroupId = ProductGroupId }).ToList();
                return records;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.InnerException.Message);

            }
        }
        public List<INBranchVM> GetAllBranchAssignGroupWise(int ProductGroupId, int branchid)
        {
            try
            {
                var records = _dapper.Query<INBranchVM>(@"select i.item_name[ProductGroupName], p.porduct_code[ProductName],b.BRANCH_NAME ,ba.*
                                                        from IN_BRANCH ba
                                                        join IN_PRODUCT p on ba.PRODUCT_ID=p.id
                                                        join Branches b on ba.BRANCH_ID=b.BRANCH_ID
                                                        join IN_ITEM i on ba.ProductGroupId=i.id
                                                        where p.is_active=1 
	                                                          and (ba.BRANCH_ID=@branchid or @branchid=0)
	                                                          and (ba.ProductGroupId=@ProductGroupId or @ProductGroupId=0)
                                                        order by LTRIM(p.porduct_code)", new { ProductGroupId = ProductGroupId, branchid = branchid }).ToList();
                return records;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.InnerException.Message);

            }
        }
        public int GetTempPurchaseDataByTPO(int ProductId, int bill_id)
        {
            try
            {
                var tpid = _dapper.Query<int>(@"select Id from Temp_Purchase where product_code=@ProductId and bill_id = @bill_id", new { ProductId = ProductId, bill_id = bill_id }).FirstOrDefault();
                return tpid;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.InnerException.Message);

            }
        }
        public int GetTempDisposeDataByTPO(int ProductId)
        {
            try
            {
                var tdid = _dapper.Query<int>(@"select Id from Temp_Dispose where ProductId=@ProductId", new { ProductId = ProductId }).FirstOrDefault();
                return tdid;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.InnerException.Message);

            }
        }
        public TempPurchaseOtherVM GetTempPurchaseOtherDataByTPO(int tpid)
        {
            try
            {
                var records = _dapper.Query<TempPurchaseOtherVM>(@"select * from Temp_Purchase_Other where temp_purchase_id=@tpid", new { tpid = tpid }).FirstOrDefault();
                return records;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.InnerException.Message);
            }
        }
        public List<TempDisposeOtherVM> GetTempDisposeOtherDataByTDO(int tdid)
        {
            try
            {
                var records = _dapper.Query<TempDisposeOtherVM>(@"select * from Temp_Dispose_Other where TempDisposedId=@tdid", new { tdid = tdid }).ToList();
                return records;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.InnerException.Message);
            }
        }
        public int GetSNFFromSerialProductStock(int ProductId, int OrderMesId)
        {
            try
            {
                var tpid = _dapper.Query<int>(@"select top 1 sequenceNum from SerialProductStock where productId=@ProductId and reqId=@OrderMesId order by sequenceNum asc", new { ProductId = ProductId, OrderMesId = OrderMesId }).FirstOrDefault();
                return tpid;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.InnerException.Message);
            }
        }
        public int GetSNTFromSerialProductStock(int ProductId, int OrderMesId)
        {
            try
            {
                var tpid = _dapper.Query<int>(@"select top 1 sequenceNum from SerialProductStock where productId=@ProductId and reqId=@OrderMesId order by sequenceNum desc", new { ProductId = ProductId, OrderMesId = OrderMesId }).FirstOrDefault();
                return tpid;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.InnerException.Message);
            }
        }
        public void UpdateSerialProductStockByPOMId(int OrderMesId, int purid, int prodcode)
        {
            try
            {
                _dapper.Query<int>(@"update SerialProductStock set purId=@purid ,reqId=NULL where reqId=@OrderMesId and productId=@prodcode", new { OrderMesId = OrderMesId, purid = purid, prodcode = prodcode }).FirstOrDefault();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.InnerException.Message);
            }
        }
        public InPurchaseVM getInPurchaseDataByPIdAndBillId(int ProductId, int billid)
        {
            try
            {
                var records = _dapper.Query<InPurchaseVM>(@"select * from In_Purchase where prod_code=@ProductId and bill_id=@billid", new { ProductId = ProductId, billid = billid }).FirstOrDefault();
                return records;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.InnerException.Message);
            }
        }
        public List<PurchaseOrderMessageVM> GetAllPurchaseOrderMessageRequisition(int branchid, int userid)
        {
            try
            {
                var records = _dapper.Query<PurchaseOrderMessageVM>(@"select c.CustomerName[vendorname],dbo.GetEmployeeFullNameOfId(pom.created_by)[orderby],
                                                                    dbo.GetEmployeeFullNameOfId(pom.approved_by)[approvername],
                                                                    dbo.GetEmployeeFullNameOfId(pom.received_by)[receivername],
                                                                    pom.* from Purchase_Order_Message pom 
                                                                    join Customer c on pom.vendor_code=c.ID
                                                                    where (pom.branch_id=@branchid or @branchid=0)
                                                                    and (pom.created_by=@userid or @userid=0)
                                                                    order by pom.id desc", new { branchid = branchid, userid = userid }).ToList();
                return records;
            }
            catch (Exception Ex)
            {

                throw new Exception(Ex.InnerException.Message);
            }
        }

        public List<BillInfoVM> GetAllDirectPurchaseRequisition()
        {
            try
            {
                var query = @"
                            SELECT 
                                billno, 
                                bill_date,  
                                vendorname, 
                                status,
                                dbo.GetEmployeeFullNameOfId(bi.entered_by) AS Orderby,
                                dbo.GetEmployeeFullNameOfId(bi.forwarded_to) AS forwardedToName,
                                bi.* 
                            FROM 
                                Bill_Info bi 
                            WHERE 
                                bi.status IN ('Direct PO Request', 'Direct P.O Rejected', 'Direct P.O Approved')
                            ORDER BY 
                                bi.bill_id DESC";
                var records = _dapper.Query<BillInfoVM>(query).ToList();
                return records;
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while fetching direct purchase order messages.", ex);
            }
        }
        public List<BillInfoVM> GetAllPurchaseVoucherRequisition()
        {
            try
            {
                var query = @"
                            SELECT 
                                billno, 
                                bill_date,  
                                vendorname, 
                                status,
                                dbo.GetEmployeeFullNameOfId(bi.entered_by) AS Orderby,
                                dbo.GetEmployeeFullNameOfId(bi.forwarded_to) AS forwardedToName,
                                bi.* 
                            FROM 
                                Bill_Info bi 
                            WHERE 
                                bi.status IN ('PurchaseVoucher Request', 'PurchaseVoucher Rejected', 'PurchaseVoucher Approved')
                            ORDER BY 
                                bi.bill_id DESC";
                var records = _dapper.Query<BillInfoVM>(query).ToList();
                return records;
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while fetching direct purchase order messages.", ex);
            }
        }


        public List<PurchaseOrderMessageVM> GetAllReceiveOrderHistory()
        {
            try
            {
                var records = _dapper.Query<PurchaseOrderMessageVM>(@"select c.CustomerName[vendorname],dbo.GetEmployeeFullNameOfId(pom.created_by)[orderby],
                                                                    dbo.GetEmployeeFullNameOfId(pom.approved_by)[approvername],
                                                                    dbo.GetEmployeeFullNameOfId(pom.received_by)[receivername],
                                                                    pom.* from Purchase_Order_Message pom 
                                                                    join Customer c on pom.vendor_code=c.ID
                                                                    where pom.status='Received'
                                                                    order by pom.id desc").ToList();
                return records;
            }
            catch (Exception Ex)
            {

                throw new Exception(Ex.InnerException.Message);
            }
        }
        public List<InRequisitionMessageVM> GetAllPlaceRequisition(int branchid, int userid)
        {
            try
            {
                var records = _dapper.Query<InRequisitionMessageVM>(@"select top(200) dbo.GetEmployeeFullNameOfId(rm.Requ_by)[RequestedBy],
                                                                    dbo.GetEmployeeFullNameOfId(rm.recommed_by)[RecommendedBy],
		                                                            dbo.GetEmployeeFullNameOfId(rm.Delivered_By)[DispatchedBy],
		                                                            dbo.GetEmployeeContactNo(rm.Requ_by)[RequestingContactNo],
                                                                    dbo.GetBranchName(rm.branch_id)[RequestingBranch],
                                                                    dbo.GetBranchName(rm.Forwarded_To)[ForwardedBranch],
		                                                            dbo.GetDeptNameByEmployee(rm.Delivered_By)[ForwardedDepartment],
                                                                    dbo.GetDeptName(rm.dept_id)[RequestingDepartment],
                                                                    rm.* from IN_Requisition_Message rm
                                                                    where (rm.branch_id=@branchid or @branchid=0)
                                                                          and (rm.Requ_by=@userid or @userid=0)
                                                                    order by rm.id desc", new { branchid = branchid, userid = userid }).ToList();
                return records;
            }
            catch (Exception Ex)
            {

                throw new Exception(Ex.InnerException.Message);
            }
        }
        public List<FuelRequestsMessageVM> GetAllFuelRequisition(int branchid, int userid)
        {
            try
            {
                var records = _dapper.Query<FuelRequestsMessageVM>(@"select top(200) dbo.GetEmployeeFullNameOfId(rm.Requested_By)[RequestedBy],
                                                                    dbo.GetEmployeeFullNameOfId(rm.Recommended_By)[RecommendedBy],
		                                                            dbo.GetBranchName(rm.Branch_id)[BranchName],
		                                                            dbo.GetEmployeeContactNo(rm.Requested_By)[RequestingContactNo],
                                                                    dbo.GetBranchName(rm.Branch_Id)[RequestingBranch],
                                                                    rm.* from Fuel_Requests_Message rm
                                                                    where (rm.Branch_id=@branchid or @branchid=0)
                                                                          and (rm.Requested_By=@userid or @userid=0)
                                                                    order by rm.id desc", new { branchid = branchid, userid = userid }).ToList();
                return records;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.InnerException.Message);
            }
        }
        public string GetPreviousKmRUn(string vehicleNo)
        {
            try
            {
                var records = _dapper.Query<string>(@"select top 1 (KM_Run) from Fuel_Requests fr
                                                    join Fuel_Requests_Message frm on fr.Fuel_Requests_Message_Id=frm.Id
                                                    where fr.Vehicle_No=@vehicleNo and Status!='Rejected'	
                                                    order by fr.Id desc", new { vehicleNo = vehicleNo.Trim() }).FirstOrDefault();
                return records;
            }
            catch (Exception Ex)
            {

                throw new Exception(Ex.InnerException.Message);
            }
        }
        public string getFuelStatusByVehicleNo(string vehicleNo)
        {
            try
            {
                var res = _dapper.Query<string>(@"select top 1 Status from Fuel_requests fr 
                                                join Fuel_Requests_Message frm on fr.Fuel_Requests_Message_Id = frm.Id
                                                where Vehicle_No=@vehicleNo order by fr.id desc", new { vehicleNo = vehicleNo.Trim() }).FirstOrDefault();
                return res;
            }
            catch (Exception Ex)
            {

                throw new Exception(Ex.InnerException.Message);
            }
        }

        public bool IsDuplicateCoupon(string couponNo)
        {
            var existingCount = _dapper.QueryFirstOrDefault<int>(
                @"SELECT COUNT(*) 
                FROM Fuel_Requests fr
                inner join Fuel_Requests_Message frm
                on fr.Fuel_Requests_Message_Id= frm.Id
                WHERE frm.Status != 'Rejected' and
                Coupon_No = @couponNo",
                new { couponNo = couponNo.Trim() }
            );

            return existingCount > 0;
        }

        public bool IsDuplicateRequest(string vehicleNo, string couponNo, string currentKm)
        {
            var existingCount = _dapper.QueryFirstOrDefault<int>(@"
                                                             SELECT COUNT(*) FROM Fuel_Requests 
                                                             WHERE Vehicle_No = @vehicleNo 
                                                             AND Coupon_No = @couponNo 
                                                             AND KM_Run = @currentKm", new { vehicleNo = vehicleNo.Trim(), couponNo = couponNo.Trim(), currentKm = currentKm.Trim() });
            return existingCount > 0;
        }
        public List<FuelRequestsMessageVM> GetAllFuelRequestForRecommendation(int branchid, int userid)
        {
            try
            {
                var records = _dapper.Query<FuelRequestsMessageVM>(@"select top(200) dbo.GetEmployeeFullNameOfId(rm.Requested_By)[RequestedBy],
                                                                    dbo.GetEmployeeFullNameOfId(rm.Recommended_By)[RecommendedBy],
		                                                            dbo.GetBranchName(rm.Branch_id)[BranchName],
		                                                            dbo.GetEmployeeContactNo(rm.Requested_By)[RequestingContactNo],
                                                                    dbo.GetBranchName(rm.Branch_Id)[RequestingBranch],
                                                                    rm.* from Fuel_Requests_Message rm
                                                                    where (rm.Branch_id=@branchid or @branchid=0)
                                                                          and (rm.Requested_By=@userid or @userid=0)
                                                                           and rm.Status='Requested'
                                                                    order by rm.id desc", new { branchid = branchid, userid = userid }).ToList();
                return records;
            }
            catch (Exception Ex)
            {

                throw;
            }
        }
        public List<FuelRequestsMessageVM> GetAllFuelRecommendationForApproval(int branchid, int userid)
        {
            try
            {
                var records = _dapper.Query<FuelRequestsMessageVM>(@"select top(200) dbo.GetEmployeeFullNameOfId(rm.Requested_By)[RequestedBy],
                                                                    dbo.GetEmployeeFullNameOfId(rm.Recommended_By)[RecommendedBy],
		                                                            dbo.GetBranchName(rm.Branch_id)[BranchName],
		                                                            dbo.GetEmployeeContactNo(rm.Requested_By)[RequestingContactNo],
                                                                    dbo.GetBranchName(rm.Branch_Id)[RequestingBranch],
                                                                    rm.* from Fuel_Requests_Message rm
                                                                    where (rm.Branch_id=@branchid or @branchid=0)
                                                                          and (rm.Requested_By=@userid or @userid=0)
                                                                           and rm.Status='Recommended'
                                                                    order by rm.id desc", new { branchid = branchid, userid = userid }).ToList();
                return records;
            }
            catch (Exception Ex)
            {

                throw;
            }
        }
        public List<InRequisitionMessageVM> GetAllPlaceRequisitionApproved(int branchid, int userid)
        {
            try
            {
                var records = _dapper.Query<InRequisitionMessageVM>(@"select rm.Req_no,rm.Requ_date,
				                                                    dbo.GetEmployeeFullNameOfId(rm.Requ_by)[RequestedBy],
                                                                    dbo.GetEmployeeFullNameOfId(rm.recommed_by)[RecommendedBy],
				                                                    dbo.GetEmployeeFullNameOfId(rm.Approver_id)[ApprovedBy],
                                                                    dbo.GetBranchName(rm.branch_id)[RequestingBranch],
                                                                    dbo.GetDeptName(rm.dept_id)[RequestingDepartment],
			                                                        dbo.GetBranchName(rm.Forwarded_To)[ForwardedBranch],
		                                                            rm.IS_SCHEDULE,rm.priority,rm.id,
                                                                    rm.Req_no,rm.Requ_date,rm.Approved_Date from IN_Requisition_Message rm
                                                                    where rm.status='Approved' and 
				                                                    (rm.Forwarded_To=@branchid or @branchid=0)
                                                                     and (rm.recommed_by=@userid or @userid=0)
                                                                     order by rm.id desc", new { branchid = branchid, userid = userid }).ToList();
                return records;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.InnerException.Message);
            }
        }
        public List<InRequisitionMessageVM> GetAllPlaceRequisitionApprovedOfOwnBranch(int branchid, int userid)
        {
            try
            {
                var records = _dapper.Query<InRequisitionMessageVM>(@"select top(200) rm.Req_no,rm.Requ_date,
				                                                    dbo.GetEmployeeFullNameOfId(rm.Requ_by)[RequestedBy],
                                                                    dbo.GetEmployeeFullNameOfId(rm.recommed_by)[RecommendedBy],
				                                                    dbo.GetEmployeeFullNameOfId(rm.Approver_id)[ApprovedBy],
                                                                    dbo.GetBranchName(rm.branch_id)[RequestingBranch],
                                                                    dbo.GetDeptName(rm.dept_id)[RequestingDepartment],
			                                                        dbo.GetBranchName(rm.Forwarded_To)[ForwardedBranch],
		                                                            rm.IS_SCHEDULE,rm.priority,
                                                                    rm.* from IN_Requisition_Message rm
                                                                    where rm.status='Approved' and rm.branch_id!=12 and
				                                                    (rm.Forwarded_To=@branchid or @branchid=0)
                                                                            and (rm.recommed_by=@userid or @userid=0)
                                                                            order by rm.id desc", new { branchid = branchid, userid = userid }).ToList();
                return records;
            }
            catch (Exception Ex)
            {

                throw;
            }
        }
        public List<InRequisitionMessageVM> GetAllDispatchedRequisitionOfOwnBranch(int branchid, int userid)
        {
            try
            {
                var records = _dapper.Query<InRequisitionMessageVM>(@"select top(200) rm.Req_no,rm.Requ_date,
                                                            dbo.GetEmployeeFullNameOfId(rm.Requ_by)[RequestedBy],
                                                            dbo.GetEmployeeFullNameOfId(rm.recommed_by)[RecommendedBy],
                                                            dbo.GetEmployeeFullNameOfId(rm.Approver_id)[ApprovedBy],
                                                            dbo.GetEmployeeFullNameOfId(rm.Delivered_By)[DeliveredBy],
                                                            dbo.GetBranchName(rm.branch_id)[RequestingBranch],
                                                            dbo.GetDeptName(rm.dept_id)[RequestingDepartment],
                                                            dbo.GetBranchName(rm.Forwarded_To)[ForwardedBranch],
                                                            rm.IS_SCHEDULE,rm.priority,
                                                            rm.* from IN_Requisition_Message rm
                                                            where rm.status='Full Dispatched' 
                                                            and rm.branch_id!=12 
                                                            and rm.Delivered_Date is not null
                                                            and rm.Acknowledged_Date is null
                                                            and (rm.branch_id=@branchid or @branchid=0)
                                                            and (rm.Requ_by=@userid or @userid=0)
                                                            order by rm.id desc",
                                                                    new { branchid = branchid, userid = userid }).ToList();
                return records;
            }
            catch (Exception Ex)
            {
                throw;
            }
        }
        public List<InRequisitionMessageVM> GetAllDispatchPlaceRequisition(int branchid, int userid)
        {
            try
            {
                var records = _dapper.Query<InRequisitionMessageVM>(@"select  dbo.GetEmployeeFullNameOfId(rm.Requ_by)[RequestedBy],
                                                                    dbo.GetEmployeeFullNameOfId(rm.recommed_by)[RecommendedBy],
		                                                            dbo.GetEmployeeFullNameOfId(rm.Delivered_By)[DispatchedBy],
		                                                            dbo.GetEmployeeContactNo(rm.Requ_by)[RequestingContactNo],
                                                                    dbo.GetBranchName(rm.branch_id)[RequestingBranch],
                                                                    dbo.GetBranchName(rm.Forwarded_To)[ForwardedBranch],
		                                                            dbo.GetDeptNameByEmployee(rm.Delivered_By)[ForwardedDepartment],
                                                                    dbo.GetDeptName(rm.dept_id)[RequestingDepartment],
                                                                    rm.* from IN_Requisition_Message rm
                                                                    where rm.status='Full Dispatched' and
                                                                        (rm.branch_id=@branchid or @branchid =0 )
                                                                        and (rm.Requ_by=@userid or @userid =0)
                                                                        order by rm.id desc", new { branchid = branchid, userid = userid }).ToList();
                return records;
            }
            catch (Exception Ex)
            {

                throw;
            }
        }
        public List<InRequisitionMessageVM> GetAllBranchWiseHistory(int branchid)
        {
            try
            {
                var records = _dapper.Query<InRequisitionMessageVM>(@"select Top(400) dbo.GetEmployeeFullNameOfId(rm.Requ_by)[RequestedBy],
                                                                    dbo.GetEmployeeFullNameOfId(rm.recommed_by)[RecommendedBy],
		                                                            dbo.GetEmployeeFullNameOfId(rm.Delivered_By)[DispatchedBy],
                                                                    dbo.GetEmployeeFullNameOfId(rm.Approver_id) AS [ApprovedBy],
		                                                            dbo.GetEmployeeContactNo(rm.Requ_by)[RequestingContactNo],
                                                                    dbo.GetBranchName(rm.branch_id)[RequestingBranch],
                                                                    dbo.GetBranchName(rm.Forwarded_To)[ForwardedBranch],
		                                                            dbo.GetDeptNameByEmployee(rm.Delivered_By)[ForwardedDepartment],
                                                                    dbo.GetDeptName(rm.dept_id)[RequestingDepartment],
                                                                    rm.* from IN_Requisition_Message rm
                                                                    where (rm.branch_id=@branchid or @branchid=0)
                                                                    order by rm.id desc", new { branchid = branchid }).ToList();
                return records;
            }
            catch (Exception Ex)
            {

                throw;
            }
        }
        public List<InRequisitionMessageVM> GetAllBranchWiseRejectedHistory(int branchid)
        {
            try
            {
                var records = _dapper.Query<InRequisitionMessageVM>(@"select rm.id, rm.Req_no,dbo.GetEmployeeFullNameOfId(rm.Requ_by)RequestedBy,
                                                                      rm.Requ_date,rm.Requ_Message,
                                                                      dbo.GetBranchName(rm.branch_id)RequestingBranch,
	                                                                  dbo.GetEmployeeFullNameOfId(rm.rejected_by)rejected_by,
	                                                                  rm.rejected_date,rm.rejected_message,rm.status
                                                                      from IN_Requisition_Message rm 
                                                                      where status='Rejected'
                                                                      and (rm.branch_id=@branchid or @branchid=0)
                                                                      order by rm.id desc", new { branchid = branchid }).ToList();
                return records;
            }
            catch (Exception Ex)
            {

                throw;
            }
        }
        public int getSerialNoFromSerialProductStock(int snf, int snt, int pid)
        {
            try
            {
                var records = _dapper.Query<int>(@"select count(*) from SerialProductStock where sequenceNum between @snf and @snt and productId=@pid", new { pid = pid, snf = snf, snt = snt }).FirstOrDefault();
                return records;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.InnerException.Message);
            }
        }
        public int getSerialNoFromSerialProductStockWithBranch(int snf, int snt, int pid, int branchid)
        {
            try
            {
                var records = _dapper.Query<int>(@"select count(*) from SerialProductStock where sequenceNum between @snf and @snt and productId=@pid and branchId=@branchid", new { pid = pid, snf = snf, snt = snt, branchid = branchid }).FirstOrDefault();
                return records;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.InnerException.Message);
            }
        }
        public List<BillInfoVM> GetAllUnpaidBill()
        {
            try
            {
                var records = _dapper.Query<BillInfoVM>(@"select * from Bill_info
                                                                    where paid_date is null and party_code is not null
                                                                    order by bill_id desc").ToList();
                return records;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.InnerException.Message);
            }
        }
        public List<BillInfoVM> GetAllPaidBill()
        {
            try
            {
                var records = _dapper.Query<BillInfoVM>(@"select * from Bill_info
                                                        where paid_date is not null and party_code is not null
                                                        order by bill_id desc").ToList();
                return records;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.InnerException.Message);
            }
        }
        public void DeleteTempReturnProduct()
        {
            try
            {
                _dapper.Query<int>(@"delete from Temp_Return_Product").FirstOrDefault();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.InnerException.Message);
            }
        }
        public void UpdateInBranchAssignByPIdInActive(int pId)
        {
            try
            {
                _dapper.Query<int>(@"update IN_BRANCH set IS_ACTIVE=0 where PRODUCT_ID=@pId", new { pId = pId }).FirstOrDefault();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.InnerException.Message);
            }
        }
        public void UpdateInBranchAssignByPIdActive(int pId)
        {
            try
            {
                _dapper.Query<int>(@"update IN_BRANCH set IS_ACTIVE=1 where PRODUCT_ID=@pId", new { pId = pId }).FirstOrDefault();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.InnerException.Message);
            }
        }
        public void AssignBranchForProduct(int Bid, int ProductId, int ProdGrpId, int ReorderLevel, int ReorderQty, int MaxHoldingQty)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@BId", Bid);
                param.Add("@ProductId", ProductId);
                param.Add("@ProductGroupId", ProdGrpId);
                param.Add("@ReorderLevel", ReorderLevel);
                param.Add("@ReorderQty", ReorderQty);
                param.Add("@MaxHoldingQty", MaxHoldingQty);
                _dapper.Query("InsertProductAssignment", param: param, commandTimeout: 2400, commandType: CommandType.StoredProcedure);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.InnerException.Message);
            }
        }
        public void InsertProductAssignmentWithGroup(int Bid, int ProdGrpId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@BId", Bid);
                param.Add("@ProductGroupId", ProdGrpId);
                _dapper.Query("InsertProductAssignmentWithGroup", param: param, commandTimeout: 2400, commandType: CommandType.StoredProcedure);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.InnerException.Message);
            }
        }
        public void DeleteInStaticDispatchByProductId(int prodid)
        {
            try
            {
                _dapper.Query<int>(@"delete from In_Static_Temp_Dispatch where ProductId=@prodid", new { prodid = prodid }).FirstOrDefault();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.InnerException.Message);
            }
        }
        public void UpdateSerialProductStockForDispose(int dmesId, int pId, int snf, int snt)
        {
            try
            {
                _dapper.Query<int>(@"Update SerialProductStock set disposemesid=@dmesId where productId=@pId and sequenceNum between @snf and @snt", new { dmesId = dmesId, pId = pId, snf = snf, snt = snt }).FirstOrDefault();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.InnerException.Message);
            }
        }
        public int InterBranchTransfer(string model)
        {
            try
            {

                var param = new DynamicParameters();
                param.Add("@MvJson", model, DbType.String);


                var result = _dapper.ExecuteScalar<int>(
             "Sp_Inter_Branch_TransferRe",
             param: param,
             commandType: CommandType.StoredProcedure
         );

                return result;
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw new Exception($"Error occurred while executing stored procedure: {ex.Message}", ex);
            }
        }

        public List<ProductStockVM> GetAllProductStock(int branchid, int groupid, int productid)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@BranchId", branchid);
                param.Add("@GroupId", groupid);
                param.Add("@ProductId", productid);

                var queryResult = _dapper.Query<ProductStockVM>(
                    "SP_new_Inv_StockInHand_For_InterBranchTransfer",
                    param: param,
                    commandType: CommandType.StoredProcedure
                ).ToList();
                return queryResult;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred while executing stored procedure: {ex.Message}", ex);
            }
        }

        public string CheckSerialNumber(string model)
        {

            var param = new DynamicParameters();
            param.Add("@jsonData", model, DbType.String);

            // Execute the stored procedure and get the JSON string result
            var queryResult = _dapper.Query<string>(
                "Sp_Check_SerialNumber",
                param: param,
                commandType: CommandType.StoredProcedure
            ).FirstOrDefault();


            // Parse the JSON string to check the response
            var jsonResponse = JsonConvert.DeserializeObject<JObject>(queryResult);
            var responseArray = jsonResponse["Response"] as JArray;

            var responseData = responseArray.Select(item => new
            {
                Header = JsonConvert.DeserializeObject<JObject>(item["Header"]?.ToString()),
                Data = JsonConvert.DeserializeObject<JObject>(item["Data"]?.ToString())
            }).ToList();
            var res = JsonConvert.SerializeObject(responseData);
            return res;


        }

        public List<InterBranchTransferACKVM> GetAllInterBranchTransfer(int branchid, int userid)
        {
            try
            {
                var records = _dapper.Query<InterBranchTransferACKVM>(@"select ibt.transfertype as TransferType,
                                                             dbo.GetEmployeeFullNameOfId(ibt.TransferedBy)[TransferedBy],
	                                                            dbo.GetEmployeeFullNameOfId(ibt.TransferedTo)[TransferedTo],
	                                                            dbo.GetBranchName(ibt.FromBranch)[FromBranch],
                                                             dbo.GetBranchName(ibt.ToBranch)[ToBranch],
	                                                            dbo.GetDeptName(ibt.FromDepartment)[FromDepartment],
                                                             dbo.GetDeptName(ibt.ToDepartment)[ToDepartment],
                                                             ibt.TransferedDate as TransferedDate,
                                                             ibt.Id as Id,
                                                             ibt.Narration as Narration
                                                             from Inter_Branch_Transfer ibt
                                                             where ibt.[Status]='TransferInitiated' and
																	ibt.TransferType = 'InterBranchTransfer' and
																	(ibt.ToBranch=@branchid or @branchid=0)
																	and (ibt.TransferedTo=@userid or @userid=0)
																	order by ibt.id desc", new { branchid = branchid, userid = userid }).ToList();
                return records;
            }
            catch (Exception Ex)
            {

                throw;
            }
        }

        public List<InterBranchTransferACKVM> GetAllInterBranchTransferDetails(int Tid)
        {
            try
            {
                var records = _dapper.Query<InterBranchTransferACKVM>(@"select ibtd.*,ibt.Narration as Narration
                                                                 from Inter_Branch_Transfer_Details ibtd
                                                                 inner join Inter_Branch_Transfer ibt
                                                                 on ibtd.Inter_Branch_TransferId = ibt.Id
                                                                 where 
                                                                 ibt.[Status]='TransferInitiated' and
                                                                 ibtd.Inter_Branch_TransferId = @Tid
                                                                 order by ibtd.id desc", new { Tid = Tid }).ToList();
                return records;
            }
            catch (Exception Ex)
            {

                throw;
            }
        }

        public void Dispose()
        {
            _dapper.Dispose();

        }
    }
}
