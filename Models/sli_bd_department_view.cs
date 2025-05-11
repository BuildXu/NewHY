using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_bd_department_view
    {
        [Key]
        public int Fdeptid { get; set; }
        public string FNUMBER { get; set; }
        public string FNAME { get; set; }
        public string FSumNUMBER { get; set; }
    }
}