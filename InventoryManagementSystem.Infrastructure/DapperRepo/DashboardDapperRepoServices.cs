using Dapper;
using InventoryManagementSystem.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.DapperRepo
{
    public class DashboardDapperRepoServices
    {
        private readonly string _connectionString;

        public DashboardDapperRepoServices()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["JUINOAPIEntities"].ConnectionString;
        }

        private SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<List<InRequisitionMessageVM>> GetRecentApprovedRequisition()
        {
            using (var connection = CreateConnection())
            {
                return (await connection.QueryAsync<InRequisitionMessageVM>("SP_Inv_New_GetRecentApprovedRequisition", commandType: CommandType.StoredProcedure)).ToList();
            }
        }

        public async Task<List<InRequisitionMessageVM>> GetRecentDispatchRequisition()
        {
            using (var connection = CreateConnection())
            {
                return (await connection.QueryAsync<InRequisitionMessageVM>("SP_Inv_New_GetRecentDispatchRequisition", commandType: CommandType.StoredProcedure)).ToList();
            }
        }

        public async Task<List<InRequisitionMessageVM>> GetRecentAcknowledgeRequisition()
        {
            using (var connection = CreateConnection())
            {
                return (await connection.QueryAsync<InRequisitionMessageVM>("SP_Inv_New_GetRecentAcknowledgeRequisition", commandType: CommandType.StoredProcedure)).ToList();
            }
        }

        public async Task<List<InPurchaseVM>> GetRecentPurchases()
        {
            using (var connection = CreateConnection())
            {
                return (await connection.QueryAsync<InPurchaseVM>("SP_Inv_New_GetRecentPurchases", commandType: CommandType.StoredProcedure)).ToList();
            }
        }

        public async Task<List<INBranchVM>> GetLowStockProducts()
        {
            using (var connection = CreateConnection())
            {
                return (await connection.QueryAsync<INBranchVM>("SP_Inv_New_GetLowStockProducts", commandType: CommandType.StoredProcedure)).ToList();
            }
        }
    }
}
