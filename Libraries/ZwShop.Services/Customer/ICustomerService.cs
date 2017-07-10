using System;
using System.Collections.Generic;
using System.Web.Security;
using ZwShop.Services.Profile;
using ZwShop.Services.Shipping;
using ZwShop.Common;
using ZwShop.Data.Entity.CustomerManagement;
namespace ZwShop.Services.CustomerManagement
{
    public partial interface ICustomerService
    {
        #region ¿Í»§
        Customer SetEmail(int customerId, string newEmail);

        Customer ChangeCustomerUsername(int customerId, string newUsername);

        Customer SetCustomerSignature(int customerId, string signature);

        void CreateAnonymousUser();

        List<Customer> GetCustomersByCustomerRoleId(int customerRoleId);

        void MarkCustomerAsDeleted(int customerId);

        Customer GetCustomerByEmail(string email);

        Customer GetCustomerByUsernameOrEmailOrPhoneNumber(string email_username_phonenumber);

        Customer GetCustomerById(int customerId);

        Customer GetCustomerByGuid(Guid customerGuid);

        void ModifyPassword(string email, string oldPassword, string password);

        void ModifyPassword(string email, string newPassword);

        void ModifyPassword(int customerId, string newPassword);

        void Activate(Guid customerGuid);

        void Activate(int customerId);

        void Activate(int customerId, bool sendCustomerWelcomeMessage);

        void Deactivate(Guid customerGuid);

        void Deactivate(int customerId);

        bool Login(string email, string password);

        void Logout();

        #region Customer sessions
        #endregion

        CustomerSession GetCustomerSessionByGuid(Guid customerSessionGuid);

        CustomerSession GetCustomerSessionByCustomerId(int customerId);

        void DeleteCustomerSession(Guid customerSessionGuid);

        List<CustomerSession> GetAllCustomerSessions();

        List<CustomerSession> GetAllCustomerSessionsWithNonEmptyShoppingCart();

        void DeleteExpiredCustomerSessions(DateTime olderThan);

        CustomerSession SaveCustomerSession(Guid customerSessionGuid,
            int customerId, DateTime lastAccessed, bool isExpired);

        #endregion

    }
}