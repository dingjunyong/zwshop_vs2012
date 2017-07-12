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
    public class AddressRepository:IAddressRepository
    {

        private readonly ShopObjectContext _context;

        public AddressRepository(ShopObjectContext context)
        {
            this._context = context;
        }

        public int DeleteAddress(int id)
        {
            using (var conn = _context.OpenConnection())
            {
                var param = new DynamicParameters();
                param.Add("@Id", id);
                var result = conn.Execute(
                    "address_delete_by_id",
                    param: param,
                    commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public Address GetAddressById(int id)
        {
            using (var conn = _context.OpenConnection())
            {
                var param = new DynamicParameters();
                param.Add("@Id", id);
                var result = conn.Query<Address>(
                    "address_get_by_id",
                    param: param,
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
                return result;
            }
        }

        public IEnumerable<Address> GetAddressesByCustomerId(int customerId)
        {
            using (var conn = _context.OpenConnection())
            {
                var param = new DynamicParameters();
                param.Add("@CustomerId", customerId);
                var result = conn.Query<Address>(
                    "address_get_by_customerid",
                    param: param,
                    commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public int InsertAddress(Address address)
        {
            using (var conn = _context.OpenConnection())
            {
                var param = new DynamicParameters();
                param.Add("@CustomerId", address.CustomerId);
                param.Add("@Name", address.Name);
                param.Add("@PhoneNumber", address.PhoneNumber);
                param.Add("@Email", address.Email);
                param.Add("@Address1", address.Address1);
                param.Add("@Address2", address.Address2);
                param.Add("@City", address.City);
                param.Add("@StateProvinceId", address.StateProvinceId);
                param.Add("@ZipPostalCode", address.ZipPostalCode);
                param.Add("@CreatedOn", address.CreatedOn);
                param.Add("@UpdatedOn", address.UpdatedOn);
                var result = conn.Execute(
                    "address_add",
                    param: param,
                    commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public int UpdateAddress(Address address)
        {
            using (var conn = _context.OpenConnection())
            {
                var param = new DynamicParameters();
                param.Add("@Id", address.Id);
                param.Add("@CustomerId", address.CustomerId);
                param.Add("@Name", address.Name);
                param.Add("@PhoneNumber", address.PhoneNumber);
                param.Add("@Email", address.Email);
                param.Add("@Address1", address.Address1);
                param.Add("@Address2", address.Address2);
                param.Add("@City", address.City);
                param.Add("@StateProvinceId", address.StateProvinceId);
                param.Add("@ZipPostalCode", address.ZipPostalCode);
                param.Add("@CreatedOn", address.CreatedOn);
                param.Add("@UpdatedOn", address.UpdatedOn);
                var result = conn.Execute(
                    "address_update",
                    param: param,
                    commandType: CommandType.StoredProcedure);
                return result;
            }
        }
    }
}
