namespace ZwShop.Services.CustomerManagement
{
    public enum CustomerRegistrationTypeEnum : int
    {
        /// <summary>
        /// ��ͨ�˻�ע��
        /// </summary>
        Standard = 1,
        /// <summary>
        /// ������֤ע��
        /// </summary>
        EmailValidation = 2,
        /// <summary>
        /// ����Աע��
        /// </summary>
        AdminApproval = 3,
        /// <summary>
        /// ������ע�� 
        /// </summary>
        Disabled = 4,
    }
}
