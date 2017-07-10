using ZwShop.Data.Entity.CustomerManagement;
using ZwShop.Services.CustomerManagement;
using ZwShop.Services.Directory;

namespace ZwShop.Services.Payment
{
    /// <summary>
    /// Represents a payment info holder
    /// </summary>
    public partial class PaymentInfo
    {
        #region Properties
        /// <summary>
        /// Gets or sets a shipping address
        /// </summary>
        public Address ShippingAddress { get; set; }

        /// <summary>
        /// Gets or sets an order total
        /// </summary>
        public decimal OrderTotal { get; set; }

        /// <summary>
        /// /// <summary>
        /// Gets or sets a payment method identifier
        /// </summary>
        /// </summary>
        public int PaymentMethodId { get; set; }
        /// <summary>
        /// Gets or sets an initial (parent) order identifier if order is recurring
        /// </summary>
        public int InitialOrderId { get; set; }
        

        #endregion
    }
}
