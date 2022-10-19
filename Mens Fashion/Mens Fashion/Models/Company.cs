namespace Mens_Fashion.Models
{
    using System;
    using System.Web;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Company")]
    public partial class Company
    {
        [Key]
        public int COMPANY_ID { get; set; }

        [StringLength(50)]
        public string COMPANY_NAME { get; set; }

        [StringLength(50)]
        public string COMPANY_EMAIL { get; set; }

        [StringLength(50)]
        public string COMPANY_CONTACT { get; set; }

        [StringLength(200)]
        public string COMPANY_ADDRESS { get; set; }

        [StringLength(200)]
        public string COMPANY_LOGO { get; set; }
        [NotMapped]
        public HttpPostedFileBase LOGO { get; set; }
        [StringLength(4000)]
        public string COMPANY_ABOUT { get; set; }
    }
}
