using System.Collections.Generic;

namespace ZwShop.Services.Media
{
    public partial class Picture : BaseEntity
    {
        #region Properties

        public int PictureId { get; set; }

        public byte[] PictureBinary { get; set; }

        public string MimeType { get; set; }

        public bool IsNew { get; set; }

        #endregion

      
    }
}
