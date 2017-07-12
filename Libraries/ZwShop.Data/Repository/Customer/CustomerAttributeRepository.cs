using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ZwShop.Data.Entity.CustomerManagement;

namespace ZwShop.Data.Repository.CustomerManagement
{
    public class CustomerAttributeRepository:ICustomerAttributeRepository
    {
         private readonly ShopObjectContext _context;
         public CustomerAttributeRepository(ShopObjectContext context)
        {
            this._context = context;
        }
      
        public int DeleteCustomerAttribute(int id)
        {
            using (var conn = _context.OpenConnection())
            {
                var param = new DynamicParameters();
                param.Add("@Id", id);
                var result = conn.Execute(
                    "customerattribute_delete_by_id",
                    param: param,
                    commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public CustomerAttribute GetCustomerAttributeById(int id)
        {
            using (var conn = _context.OpenConnection())
            {
                var param = new DynamicParameters();
                param.Add("@Id", id);
                var result = conn.Query<CustomerAttribute>(
                    "customerattribute_get_by_id",
                    param: param,
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
                return result;
            }
        }

        public IEnumerable<CustomerAttribute> GetCustomerAttributesByCustomerId(int customerId)
        {
            using (var conn = _context.OpenConnection())
            {
                var param = new DynamicParameters();
                param.Add("@CustomerId", customerId);
                var result = conn.Query<CustomerAttribute>(
                    "customerattribute_get_by_customerId",
                    param: param,
                    commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public int InsertCustomerAttribute(CustomerAttribute customerAttribute)
        {
            using (var conn = _context.OpenConnection())
            {
                var param = new DynamicParameters();
                param.Add("@CustomerId", customerAttribute.CustomerId);
                param.Add("@Key",customerAttribute.Key);
                param.Add("@Value", customerAttribute.Value);
                var result = conn.Execute(
                    "customerattribute_add",
                    param: param,
                    commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public int UpdateCustomerAttribute(CustomerAttribute customerAttribute)
        {
            using (var conn = _context.OpenConnection())
            {
                var param = new DynamicParameters();
                param.Add("@CustomerId", customerAttribute.CustomerId);
                param.Add("@Key", customerAttribute.Key);
                param.Add("@Value", customerAttribute.Value);
                var result = conn.Execute(
                    "customerattribute_update",
                    param: param,
                    commandType: CommandType.StoredProcedure);
                return result;
            }
        }
    }
}
