namespace ZwShop.Services.Audit
{
    public enum LogTypeEnum : int
    {
        /// <summary>
        /// �ͻ�����
        /// </summary>
        CustomerError = 1,
        /// <summary>
        /// �ʼ����ʹ���
        /// </summary>
        MailError = 2,
        /// <summary>
        /// ��������
        /// </summary>
        OrderError = 3,
        /// <summary>
        /// ����Ա����
        /// </summary>
        AdministrationArea = 4,
        /// <summary>
        /// ��������
        /// </summary>
        CommonError = 5,
        /// <summary>
        /// δ֪����
        /// </summary>
        Unknown = 20,
    }
}
