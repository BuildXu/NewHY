using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class t_sal_orderEntry
    {
        [Key]
        public int FENTRYID { get; set; }
        public int FWORKORDERLISTQTY { get; set; }
        public int FWORKORDERLISTREMAIN { get; set; }
    }
}