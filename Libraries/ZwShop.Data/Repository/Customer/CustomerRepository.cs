using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZwShop.Common;
using Dapper;
using System.Data;
using ZwShop.Data.Entity.CustomerManagement;

namespace ZwShop.Data.Repository.CustomerManagement
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ShopObjectContext _context;

        public CustomerRepository(ShopObjectContext context)
        {
            this._context = context;
        }

        public Customer GetCustomerById(int id)
        {
            using (var conn = _context.OpenConnection())
            {
                var param = new DynamicParameters();
                param.Add("@Id", id);
                var customer = conn.Query<Customer>(
                    "get_customer_by_id",
                    param: param,
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
                return customer;
            }
        }

        public Customer GetCustomerByGuid(string guid)
        {
            using (var conn = _context.OpenConnection())
            {
                var param = new DynamicParameters();
                param.Add("@Guid", guid);
                var customer = conn.Query<Customer>(
                    "get_customer_by_guid",
                    param: param,
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
                return customer;
            }
        }

        public Customer GetCustomerByUserNameOrEmailOrPhone(string email_username_phonenumber)
        {
            using (var conn = _context.OpenConnection())
            {
                var param = new DynamicParameters();
                param.Add("@UserNameOrEmailOrPhoneNumber", email_username_phonenumber);
                var customer = conn.Query<Customer>(
                    "get_customer_by_email_or_username_or_phonenumber",
                    param: param,
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
                return customer;
            }
        }

        public int UpdateCustomer(int id, Dictionary<string, object> newValueDic)
        {
            using (var conn = _context.OpenConnection())
            {
                var param = new DynamicParameters();
                param.Add("@Id", id);
                foreach (var item in newValueDic)
                    param.Add("@" + item.Key, item.Key);

                int result = conn.Execute(
                    "update_customer_by_id",
                    param: param,
                    commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public int DeleteCustomer(int id)
        {
            using (var conn = _context.OpenConnection())
            {
                var param = new DynamicParameters();
                param.Add("@Id", id);
            
                int result = conn.Execute(
                    "delete_customer_by_id",
                    param: param,
                    commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public PagedList<Customer> GetAllCustomers(DateTime? registrationFrom = null, 
            DateTime? registrationTo = null,
            string email = "", string username = "", 
            bool? dontLoadGuestCustomers = null, int dateOfBirthMonth = 0, 
            int dateOfBirthDay = 0, int pageSize = int.MaxValue, int pageIndex = 1)
        {
            throw new NotImplementedException();
        }



    }
}
