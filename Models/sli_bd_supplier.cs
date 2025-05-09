using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{ //供应商信息查询
    public class sli_bd_supplier
    {
        [Key]
        public int FSUPPLIERID { get; set; }
        public string FNUMBER { get; set; }
        public string FNAME { get; set; }
        public string FSumNumber { get; set; }
    }
}