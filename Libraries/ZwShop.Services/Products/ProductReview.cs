using System;
using System.Collections.Generic;
using ZwShop.Data.Entity.CustomerManagement;
using ZwShop.Services.CustomerManagement;
using ZwShop.Services.Infrastructure;

namespace ZwShop.Services.Products
{
    public partial class ProductReview : BaseEntity
    {
        #region Properties

        public int ProductId { get; set; }


        public int CustomerId { get; set; }


        public string IPAddress { get; set; }


        public string Title { get; set; }

        public string ReviewText { get; set; }

        /// <summary>
        /// ��Ľ
        /// </summary>
        public int HelpfulYesTotal { get; set; }

        /// <summary>
        /// �Ƿ����ͨ��
        /// </summary>
        public bool IsApproved { get; set; }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime CreatedOn { get; set; }

        #endregion

        #region Custom Properties
        /// <summary>
        /// Gets the product
        /// </summary>
        public Product Product
        {
            get
            {
                return IoC.Resolve<IProductService>().GetProductById(this.ProductId);
            }
        }

        /// <summary>
        /// Gets the customer
        /// </summary>
        public Customer Customer
        {
            get
            {
                return IoC.Resolve<ICustomerService>().GetCustomerById(this.CustomerId);
            }
        }
        #endregion
        
        #region Navigation Properties

        /// <summary>
        /// Gets the products
        /// </summary>
        public virtual Product NpProduct { get; set; }

        /// <summary>
        /// Gets the products
        /// </summary>
        public virtual ICollection<ProductReviewHelpfulness> NpProductReviewHelpfulness { get; set; }
        
        #endregion
        
    }
}