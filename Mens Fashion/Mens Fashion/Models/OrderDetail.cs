namespace Mens_Fashion.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderDetail")]
    public partial class OrderDetail
    {
        [Key]
        public int ORDERDETAIL_ID { get; set; }

        public int? ORDER_FID { get; set; }

        public int PRODUCT_FID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SALE_PRICE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PURCHASE_PRICE { get; set; }

        public int? QUANTITY { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
