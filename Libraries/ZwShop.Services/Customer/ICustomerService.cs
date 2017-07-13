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
        #region �ͻ�
        /// <summary>
        ///  ��������
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="newEmail"></param>
        /// <returns></returns>
        Customer SetEmail(int customerId, string newEmail);

        /// <summary>
        /// �޸��û���
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="newUsername"></param>
        /// <returns></returns>
        Customer ChangeCustomerUsername(int customerId, string newUsername);


        /// <summary>
        /// ���ø���ǩ��
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
        Customer SetCustomerSignature(int customerId, string signature);

        /// <summary>
        /// ���ݽ�ɫ����ȡ�û�
        /// </summary>
        /// <param name="customerRoleId"></param>
        /// <returns></returns>
        List<Customer> GetCustomersByCustomerRoleId(int customerRoleId);

        /// <summary>
        ///  ɾ���û�
        /// </summary>
        /// <param name="customerId"></param>
        void DeleteCustomer(int customerId);

        /// <summary>
        /// �����û�������绰�����ȡ�û�
        /// </summary>
        /// <param name="email_username_phonenumber"></param>
        /// <returns></returns>
        Customer GetCustomerByUsernameOrEmailOrPhoneNumber(string email_username_phonenumber);

        /// <summary>
        /// �����û�ID��ȡ�û�
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        Customer GetCustomerById(int customerId);

        /// <summary>
        /// ����GUID��ȡ�û�
        /// </summary>
        /// <param name="customerGuid"></param>
        /// <returns></returns>
        Customer GetCustomerByGuid(Guid customerGuid);

        /// <summary>
        /// �޸�����
        /// </summary>
        /// <param name="email"></param>
        /// <param name="oldPassword"></param>
        /// <param name="password"></param>
        void ModifyPassword(string email, string oldPassword, string password);

        /// <summary>
        /// ��¼
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool Login(Guid customerGuid, string password);

        /// <summary>
        ///  �ǳ�
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