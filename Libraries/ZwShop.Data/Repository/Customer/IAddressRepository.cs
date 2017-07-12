using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZwShop.Data.Entity.CustomerManagement;

namespace ZwShop.Data.Repository.CustomerManagement
{
    public interface IAddressRepository
    {
        int DeleteAddress(int id);

        Address GetAddressById(int id);

        IEnumerable<Address> GetAddressesByCustomerId(int customerId);

        int InsertAddress(Address address);

        int UpdateAddress(Address address);
    }
}
