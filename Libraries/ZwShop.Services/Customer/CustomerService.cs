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
            if (string.IsNullOrEmpty(email_username_phonenumber))
                return null;

            var customer = _customerRepository.
                GetCustomerByUserNameOrEmailOrPhone(email_username_phonenumber);
            return customer;
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

        public bool Login(Guid customerGuid, string password)
        {
            var customer = _customerRepository.GetCustomerByGuid(customerGuid);
            if (customer == null)
                return false;

            if (!customer.Active)
                return false;

            if (customer.Deleted)
                return false;

            if (customer.CustomerRoleIdType==CustomerRoleIdType.Guest)
                return false;

            string passwordHash = CreatePasswordHash(password, customer.SaltKey);
            bool result = customer.PasswordHash.Equals(passwordHash);

            if (result)
            {
                var registeredCustomerSession = GetCustomerSessionByCustomerId(customer.Id);
                if (registeredCustomerSession != null)
                {
                    registeredCustomerSession.IsExpired = false;
                    var anonCustomerSession = ShopContext.Current.Session;
                    var cart1 = IoC.Resolve<IShoppingCartService>().GetCurrentShoppingCart(ShoppingCartTypeEnum.ShoppingCart);
                    var cart2 = IoC.Resolve<IShoppingCartService>().GetCurrentShoppingCart(ShoppingCartTypeEnum.Wishlist);
                    ShopContext.Current.Session = registeredCustomerSession;

                    if ((anonCustomerSession != null) && (anonCustomerSession.CustomerSessionGuid != registeredCustomerSession.CustomerSessionGuid))
                    {
                        foreach (ShoppingCartItem item in cart1)
                        {
                            IoC.Resolve<IShoppingCartService>().AddToCart(
                                item.ShoppingCartType,
                                item.ProductId,
                                item.Quantity);
                            IoC.Resolve<IShoppingCartService>().DeleteShoppingCartItem(item.Id, true);
                        }
                        foreach (ShoppingCartItem item in cart2)
                        {
                            IoC.Resolve<IShoppingCartService>().AddToCart(
                                item.ShoppingCartType,
                                item.ProductId,
                                item.Quantity);
                            IoC.Resolve<IShoppingCartService>().DeleteShoppingCartItem(item.Id, true);
                        }
                    }
                }
                if (ShopContext.Current.Session == null)
                    ShopContext.Current.Session = ShopContext.Current.GetSession(true);
                ShopContext.Current.Session.IsExpired = false;
                ShopContext.Current.Session.LastAccessed = DateTime.UtcNow;
                ShopContext.Current.Session.CustomerId = customer.Id;
                ShopContext.Current.Session = SaveCustomerSession(ShopContext.Current.Session.CustomerSessionGuid, ShopContext.Current.Session.CustomerId, NopContext.Current.Session.LastAccessed, NopContext.Current.Session.IsExpired);
            }
            return result;


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


        private string CreatePasswordHash(string password, string salt)
        {
            //MD5, SHA1
            string passwordFormat = "SHA1";
            return FormsAuthentication.HashPasswordForStoringInConfigFile(password + salt, passwordFormat);
        }
    }
}