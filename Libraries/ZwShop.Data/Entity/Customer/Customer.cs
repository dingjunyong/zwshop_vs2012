using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace ZwShop.Data.Entity.CustomerManagement
{
    public partial class Customer : BaseEntity
    {
        public Guid CustomerGuid { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string PhoneNumber { get; set; }

        public string PasswordHash { get; set; }

        public string SaltKey { get; set; }

        public int ShippingAddressId { get; set; }
        
        public int LastPaymentMethodId { get; set; }

        public int CustomerRoleId { get; set; }

        public int CustomerLevelId { get; set; }

        public string Signature { get; set; }

        public bool Active { get; set; }

        public bool Deleted { get; set; }

        public DateTime RegistrationDate { get; set; }

        public int AvatarId { get; set; }
        
        public DateTime? DateOfBirth { get; set; }

        public CustomerRoleIdType CustomerRoleIdType 
        { 
            get{ return (CustomerRoleIdType)CustomerRoleId; }
        }
    }
}