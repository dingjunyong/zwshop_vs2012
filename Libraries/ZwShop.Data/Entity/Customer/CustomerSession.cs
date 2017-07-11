

using System;
namespace ZwShop.Data.Entity.CustomerManagement
{
    public partial class CustomerSession:BaseEntity
    {
        public Guid CustomerSessionGuid { get; set; }

        public int CustomerId { get; set; }

        public DateTime LastAccessed { get; set; }

        public bool IsExpired { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
