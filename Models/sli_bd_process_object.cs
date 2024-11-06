using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_bd_process_object  //工艺流程参数
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