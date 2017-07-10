using System;
using ZwShop.Data.Entity.CustomerManagement;
using ZwShop.Services.CustomerManagement;
using ZwShop.Services.Infrastructure;
using ZwShop.Services.Products;

namespace ZwShop.Services.Orders
{
    public partial class ShoppingCartItem : BaseEntity
    {
        #region Fields
        private Product _cachedProduct;
        #endregion

        #region Properties
        public int ShoppingCartTypeId { get; set; }

        public Guid CustomerSessionGuid { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }
        #endregion 

        #region Custom Properties

        public ShoppingCartTypeEnum ShoppingCartType
        {
            get
            {
                return (ShoppingCartTypeEnum)this.ShoppingCartTypeId;
            }
        }
        public Product Product
        {
            get
            {
                if (_cachedProduct == null)
                {
                    _cachedProduct = IoC.Resolve<IProductService>().GetProductById(this.ProductId);
                }
                return _cachedProduct;
            }
        }
        public CustomerSession CustomerSession
        {
            get
            {
                return IoC.Resolve<ICustomerService>().GetCustomerSessionByGuid(this.CustomerSessionGuid);
            }
        }
        #endregion
    }
}
