using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mens_Fashion.Models
{
    public class FilterModel
    {

        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int? Category { get; set; }
        public int? Product{ get; set; }

    }
}