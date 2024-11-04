using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_test
    {
        [Key]
        public int FID { get; set; }
        public string FBillNo { get; set; }
        public string FCompay { get; set; }
        public string FStatus { get; set; }
    }
}