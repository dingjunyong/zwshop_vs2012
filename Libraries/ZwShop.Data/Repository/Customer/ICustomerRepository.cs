using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZwShop.Common;
using ZwShop.Data.Entity.CustomerManagement;

namespace ZwShop.Data.Repository.CustomerManagement
{
    public interface ICustomerRepository 
    {
        Customer GetCustomerById(int id);

        Customer GetCustomerByGuid(string guid);

        Customer GetCustomerByUserNameOrEmailOrPhone(string email_username_phonenumber);

        int UpdateCustomer(int id, Dictionary<string, object> newValueDic);

        int DeleteCustomer(int id);

        PagedList<Customer> GetAllCustomers(DateTime? registrationFrom = null,
            DateTime? registrationTo=null, string email="", string username="",
            bool? dontLoadGuestCustomers=null, int dateOfBirthMonth=0, int dateOfBirthDay=0,
            int pageSize=int.MaxValue, int pageIndex=1);        
    }
}
