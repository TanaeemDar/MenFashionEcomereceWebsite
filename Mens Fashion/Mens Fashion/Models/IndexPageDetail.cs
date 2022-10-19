


namespace Mens_Fashion.Models
{
    using System;
    using System.Web;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("IndexPageDetail")]
    public partial class IndexPageDetail
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string ALERT { get; set; }

        [StringLength(200)]
        public string SIDER1_PIC { get; set; }

        [NotMapped]
        public HttpPostedFileBase S1_PIC { get; set; }

        [StringLength(50)]
        public string SLIDER1_TITLE { get; set; }

        [StringLength(200)]
        public string SLIDER1_DESCRIPTION { get; set; }

        [StringLength(200)]
        public string SIDER2_PIC { get; set; }

        [NotMapped]
        public HttpPostedFileBase S2_PIC { get; set; }


        [StringLength(50)]
        public string SLIDER2_TITLE { get; set; }

        [StringLength(200)]
        public string SLIDER2_DESCRIPTION { get; set; }

        [StringLength(200)]
        public string BANNER1_PIC { get; set; }

        [NotMapped]
        public HttpPostedFileBase B1_PIC { get; set; }


        [StringLength(50)]
        public string BANNER1_TITLE { get; set; }

        [StringLength(200)]
        public string BANNER2_PIC { get; set; }

        [NotMapped]
        public HttpPostedFileBase B2_PIC { get; set; }


        [StringLength(50)]
        public string BANNER2_TITLE { get; set; }

        [StringLength(200)]
        public string BANNER3_PIC { get; set; }

        [NotMapped]
        public HttpPostedFileBase B3_PIC { get; set; }


        [StringLength(50)]
        public string BANNER3_TITLE { get; set; }

        [StringLength(200)]
        public string FOOTER_DESCRIPTION { get; set; }

        [StringLength(2000)]
        public string PAYMENT_METHODES { get; set; }

        [StringLength(2000)]
        public string DELIVERRY { get; set; }

        [StringLength(2000)]
        public string RETURNandEXCHANGE { get; set; }
    }
}
