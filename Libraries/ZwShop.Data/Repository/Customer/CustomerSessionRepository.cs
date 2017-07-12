using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZwShop.Data.Entity.CustomerManagement;
using Dapper;
using System.Data;
namespace ZwShop.Data.Repository.CustomerManagement
{
    public class CustomerSessionRepository : ICustomerSessionRepository
    {
        private readonly ShopObjectContext _context;
        public CustomerSessionRepository(ShopObjectContext context)
        {
            this._context = context;
        }
        public CustomerSession GetCustomerSessionByGuid(Guid customerSessionGuid)
        {
            using (var conn = _context.OpenConnection())
            {
                var param = new DynamicParameters();
                param.Add("@CustomerSessionGuid", customerSessionGuid);
                var result = conn.Query<CustomerSession>(
                    "customersession_get_by_guid",
                    param: param,
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
                return result;
            }
        }

        public CustomerSession GetCustomerSessionByCustomerId(int customerId)
        {
            using (var conn = _context.OpenConnection())
            {
                var param = new DynamicParameters();
                param.Add("@CustomerId", customerId);
                var result = conn.Query<CustomerSession>(
                    "customersession_get_last_by_customerId",
                    param: param,
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
                return result;
            }
        }

        public int DeleteCustomerSession(Guid customerSessionGuid)
        {
            using (var conn = _context.OpenConnection())
            {
                var param = new DynamicParameters();
                param.Add("@CustomerSessionGuid", customerSessionGuid);
                var result = conn.Execute(
                    "customersession_delete_by_guid",
                    param: param,
                    commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public List<CustomerSession> GetAllCustomerSessions()
        {
            using (var conn = _context.OpenConnection())
            {
                var result = conn.Query<CustomerSession>(
                    "customersession_get_all",
                    commandType: CommandType.StoredProcedure).ToList();
                return result;
            }
        }

        public List<CustomerSession> GetAllCustomerSessionsWithNonEmptyShoppingCart()
        {
            using (var conn = _context.OpenConnection())
            {
                var result = conn.Query<CustomerSession>(
                    "customersession_get_with_nonempty_shoppingcart",
                    commandType: CommandType.StoredProcedure).ToList();
                return result;
            }
        }

        public int DeleteExpiredCustomerSessions(DateTime olderThan)
        {
            using (var conn = _context.OpenConnection())
            {
                var result = conn.Execute(
                    "customersession_delete_expire",
                    commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public int SaveCustomerSession(Guid customerSessionGuid,
            int customerId, DateTime lastAccessed, bool isExpired)
        {
            throw new NotImplementedException();
        }
    }
}
