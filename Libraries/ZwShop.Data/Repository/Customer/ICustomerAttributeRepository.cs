using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZwShop.Data.Entity.CustomerManagement;

namespace ZwShop.Data.Repository.CustomerManagement
{
    public interface ICustomerAttributeRepository
    {
        int DeleteCustomerAttribute(int id);

        CustomerAttribute GetCustomerAttributeById(int id);

        IEnumerable<CustomerAttribute> GetCustomerAttributesByCustomerId(int customerId);

        int InsertCustomerAttribute(CustomerAttribute customerAttribute);

        int UpdateCustomerAttribute(CustomerAttribute customerAttribute);
    }
}
