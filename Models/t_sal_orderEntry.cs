using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class t_sal_orderEntry
    {
        
        public int FID { get; set; }
        [Key]
        public int FENTRYID { get; set; }
        public int FWORKORDERLISTQTY { get; set; }
        public int FWORKORDERLISTREMAIN { get; set; }
        public string FSLITECHNO { get; set; }
        public string FSLISALETECHNO { get; set; }
        public int FSLIMETEL { get; set; }
    }
}