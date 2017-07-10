using System;

namespace ZwShop.Data.Entity.CustomerManagement
{
    /// <summary>
    /// µØÖ·
    /// </summary>
    public partial class Address : BaseEntity
    {

        public int CustomerId { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public int StateProvinceId { get; set; }

        public string ZipPostalCode { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }


        public virtual Customer Customer { get; set; }
    }

}
