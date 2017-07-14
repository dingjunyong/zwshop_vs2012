
using System;
using System.Collections.Generic;
using System.Linq;
using ZwShop.Data;

namespace ZwShop.Data.Repository.Categories
{
    public partial class Category : BaseEntity
    {
        #region Properties
        public string Name { get; set; }

        public string Description { get; set; }

        public int ParentCategoryId { get; set; }

        public int PictureId { get; set; }

        public bool Published { get; set; }

        public bool Deleted { get; set; }

        public int DisplayOrder { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }
        #endregion

        public virtual Category ParentCategory { get; set; }

    }
}
