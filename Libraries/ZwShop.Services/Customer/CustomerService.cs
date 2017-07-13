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
        private ICustomerSessionRepository _customerSessionRepository;

        public CustomerService(ICustomerRepository customerRepository,ICustomerSessionRepository customerSessionRepository) 
        {
            this._customerRepository = customerRepository;
            this._customerSessionRepository = customerSessionRepository;
        }

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

        public List<Customer> GetCustomersByCustomerRoleId(int customerRoleId)
        {
            throw new NotImplementedException();
        }

        public void DeleteCustomer(int customerId)
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

        public bool Login(Guid customerGuid, string password)
        {
            var customer = _customerRepository.GetCustomerByGuid(customerGuid);
            if (customer == null)
                return false;

            if (!customer.Active)
                return false;

            if (customer.Deleted)
                return false;

            if (customer.CustomerRoleIdType == CustomerRoleIdType.Guest)
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
                ShopContext.Current.Session = SaveCustomerSession(ShopContext.Current.Session.CustomerSessionGuid,
                    ShopContext.Current.Session.CustomerId, ShopContext.Current.Session.LastAccessed,
                    ShopContext.Current.Session.IsExpired);
            }
            return result;
        }
        public void Logout()
        {
            if (ShopContext.Current != null)
            {
                ShopContext.Current.ResetSession();
            }
            if (ShopContext.Current != null &&
                //ShopContext.Current.IsCurrentCustomerImpersonated &&
                ShopContext.Current.OriginalUser != null)
            {
                //ShopContext.Current.OriginalUser.ImpersonatedCustomerGuid = Guid.Empty;
            }
            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                HttpContext.Current.Session.Abandon();
            }
            FormsAuthentication.SignOut();
        }

        #region CustomerSession
        public CustomerSession GetCustomerSessionByGuid(Guid customerSessionGuid)
        {
            return _customerSessionRepository.GetCustomerSessionByGuid(customerSessionGuid);
        }

        public CustomerSession GetCustomerSessionByCustomerId(int customerId)
        {
            return _customerSessionRepository.GetCustomerSessionByCustomerId(customerId);
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
            _customerSessionRepository.DeleteExpiredCustomerSessions(olderThan);
        }

        public CustomerSession SaveCustomerSession(Guid customerSessionGuid, int customerId, DateTime lastAccessed, bool isExpired)
        {
            var customerSession = _customerSessionRepository.GetCustomerSessionByGuid(customerSessionGuid);
            if (customerSession == null)
            {
                customerSession = new CustomerSession()
                {
                    CustomerSessionGuid = customerSessionGuid,
                    CustomerId = customerId,
                    LastAccessed = lastAccessed,
                    IsExpired = isExpired
                };
                _customerSessionRepository.InsertCustomerSession(customerSession);
            }
            else
            {
                customerSession.CustomerSessionGuid = customerSessionGuid;
                customerSession.CustomerId = customerId;
                customerSession.LastAccessed = lastAccessed;
                customerSession.IsExpired = isExpired;
                _customerSessionRepository.UpdateCustomerSession(customerSession);
            }
            return customerSession;

        }
        #endregion 

        #region ÀΩ”–¿‡
        private string CreatePasswordHash(string password, string salt)
        {
            //MD5, SHA1
            string passwordFormat = "SHA1";
            return FormsAuthentication.HashPasswordForStoringInConfigFile(password + salt, passwordFormat);
        }
        #endregion
    }
}