using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Entities.Common;

namespace Data.Entities.Product
{
    public class ProductCategory : BaseEntity
    {
        #region Properties

        public string Title { get; set; }

        public long? ParentId { get; set; }


        #endregion

        #region Relations

        [ForeignKey("ParentId")]
        public ProductCategory ParentCategory { get; set; }



        #endregion
    }
}
