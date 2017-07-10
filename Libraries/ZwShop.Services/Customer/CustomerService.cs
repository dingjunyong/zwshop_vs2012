using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Security;
using ZwShop.Services.Caching;
using ZwShop.Services.Configuration.Settings;
using ZwShop.Data;
using ZwShop.Services.Infrastructure;
using ZwShop.Services.Messages;
using ZwShop.Services.Orders;
using ZwShop.Services.Payment;
using ZwShop.Services.Profile;
using ZwShop.Services.Shipping;
using ZwShop.Common;
using ZwShop.Common.Utils;
using Dapper;
using System.Data;
using ZwShop.Data.Repository.CustomerManagement;
using ZwShop.Data.Entity.CustomerManagement;

namespace ZwShop.Services.CustomerManagement
{
    /// <summary>
    /// Customer service
    /// </summary>
    public partial class CustomerService : ICustomerService
    {
        private ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository) 
        {
            this._customerRepository = customerRepository;
        }

        #region 用户操作

        public Customer SetEmail(int customerId, string newEmail)
        {
            throw new NotImplementedException();
        }

        public Customer ChangeCustomerUsername(int customerId, string newUsername)
        {
            throw new NotImplementedException();
        }

        public Customer SetCustomerSignature(int customerId, string signature)
        {
            throw new NotImplementedException();
        }

        public void CreateAnonymousUser()
        {
            throw new NotImplementedException();
        }

        public List<Customer> GetCustomersByCustomerRoleId(int customerRoleId)
        {
            throw new NotImplementedException();
        }

        public void MarkCustomerAsDeleted(int customerId)
        {
            throw new NotImplementedException();
        }

        public Customer GetCustomerByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Customer GetCustomerByUsernameOrEmailOrPhoneNumber(string email_username_phonenumber)
        {
            throw new NotImplementedException();
        }

        public Customer GetCustomerById(int customerId)
        {
            throw new NotImplementedException();
        }

        public Customer GetCustomerByGuid(Guid customerGuid)
        {
            throw new NotImplementedException();
        }

        public void ModifyPassword(string email, string oldPassword, string password)
        {
            throw new NotImplementedException();
        }

        public void ModifyPassword(string email, string newPassword)
        {
            throw new NotImplementedException();
        }

        public void ModifyPassword(int customerId, string newPassword)
        {
            throw new NotImplementedException();
        }

        public void Activate(Guid customerGuid)
        {
            throw new NotImplementedException();
        }

        public void Activate(int customerId)
        {
            throw new NotImplementedException();
        }

        public void Activate(int customerId, bool sendCustomerWelcomeMessage)
        {
            throw new NotImplementedException();
        }

        public void Deactivate(Guid customerGuid)
        {
            throw new NotImplementedException();
        }

        public void Deactivate(int customerId)
        {
            throw new NotImplementedException();
        }

        public bool Login(string email, string password)
        {
            throw new NotImplementedException();
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 用户Session操作
        public CustomerSession GetCustomerSessionByGuid(Guid customerSessionGuid)
        {
            throw new NotImplementedException();
        }

        public CustomerSession GetCustomerSessionByCustomerId(int customerId)
        {
            throw new NotImplementedException();
        }

        public void DeleteCustomerSession(Guid customerSessionGuid)
        {
            throw new NotImplementedException();
        }

        public List<CustomerSession> GetAllCustomerSessions()
        {
            throw new NotImplementedException();
        }

        public List<CustomerSession> GetAllCustomerSessionsWithNonEmptyShoppingCart()
        {
            throw new NotImplementedException();
        }

        public void DeleteExpiredCustomerSessions(DateTime olderThan)
        {
            throw new NotImplementedException();
        }

        public CustomerSession SaveCustomerSession(Guid customerSessionGuid, int customerId, DateTime lastAccessed, bool isExpired)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}