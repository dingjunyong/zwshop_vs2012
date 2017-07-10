namespace ZwShop.Data.Entity.CustomerManagement
{
    public partial class CustomerAttribute : BaseEntity
    {
        #region Properties

        public int CustomerId { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }

        public string Description { get; set; }
        #endregion 

        #region Navigation Properties

        public virtual Customer Customer { get; set; }

        #endregion
    }

}
