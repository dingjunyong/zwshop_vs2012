namespace ZwShop.Services.Shipping
{
    public enum ShippingStatusEnum : int
    {
        /// <summary>
        /// ����Ҫ����
        /// </summary>
        ShippingNotRequired = 10,
        /// <summary>
        /// δ����
        /// </summary>
        NotYetShipped = 20,
        /// <summary>
        /// ������
        /// </summary>
        Shipped = 30,
        /// <summary>
        /// �ѽ���
        /// </summary>
        Delivered = 40,
    }
}
