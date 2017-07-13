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
        #region 客户
        /// <summary>
        ///  设置邮箱
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="newEmail"></param>
        /// <returns></returns>
        Customer SetEmail(int customerId, string newEmail);

        /// <summary>
        /// 修改用户名
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="newUsername"></param>
        /// <returns></returns>
        Customer ChangeCustomerUsername(int customerId, string newUsername);


        /// <summary>
        /// 设置个性签名
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
        Customer SetCustomerSignature(int customerId, string signature);

        /// <summary>
        /// 根据角色名获取用户
        /// </summary>
        /// <param name="customerRoleId"></param>
        /// <returns></returns>
        List<Customer> GetCustomersByCustomerRoleId(int customerRoleId);

        /// <summary>
        ///  删除用户
        /// </summary>
        /// <param name="customerId"></param>
        void DeleteCustomer(int customerId);

        /// <summary>
        /// 根据用户名邮箱电话号码获取用户
        /// </summary>
        /// <param name="email_username_phonenumber"></param>
        /// <returns></returns>
        Customer GetCustomerByUsernameOrEmailOrPhoneNumber(string email_username_phonenumber);

        /// <summary>
        /// 根据用户ID获取用户
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        Customer GetCustomerById(int customerId);

        /// <summary>
        /// 根据GUID获取用户
        /// </summary>
        /// <param name="customerGuid"></param>
        /// <returns></returns>
        Customer GetCustomerByGuid(Guid customerGuid);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="email"></param>
        /// <param name="oldPassword"></param>
        /// <param name="password"></param>
        void ModifyPassword(string email, string oldPassword, string password);

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool Login(Guid customerGuid, string password);

        /// <summary>
        ///  登出
        /// </summary>
        void Logout();

        #endregion

        #region Customer sessions

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