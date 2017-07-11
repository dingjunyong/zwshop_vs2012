using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZwShop.Data.Entity.CustomerManagement;

namespace ZwShop.Data.Repository.CustomerManagement
{
    public interface ICustomerSessionRepository
    {
        CustomerSession GetCustomerSessionByGuid(Guid customerSessionGuid);

        CustomerSession GetCustomerSessionByCustomerId(int customerId);

        int DeleteCustomerSession(Guid customerSessionGuid);

        List<CustomerSession> GetAllCustomerSessions();

        List<CustomerSession> GetAllCustomerSessionsWithNonEmptyShoppingCart();

        int DeleteExpiredCustomerSessions(DateTime olderThan);

        int UpdateCustomerSession();

        int InsertCustomerSession();
    }
}
