using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    //材质基础表
    public class T_BAS_PREBDONE
    {
        [Key]
        public int FID { get; set; }
        public string FNUMBER { get; set; }
        
    }
}