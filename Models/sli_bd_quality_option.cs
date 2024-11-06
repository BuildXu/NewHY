using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_bd_quality_option   //质量标准选项
    {
        public int id { get; set; }
        public string fnumber { get; set; }
        public string fname { get; set; }
        public string fnote { get; set; }
        public int fstatus { get; set; }
        public int fused { get; set; }
        public string fcreateDate { get; set; }
    }
}