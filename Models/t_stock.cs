using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class t_stock
    {
        [Key]
        public int FitemID { get; set; }
        public string  FName { get; set; }
        public string FNumber { get; set; }
    }
}