using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_mech_maintain_order
    {
        public int Id { get; set; }
        public string Fnumber { get; set; } // 报修单号
        public int Fmechine { get; set; }// 模具Id
        public DateTime Fdate { get; set; }// 报修日期
        public string Fnote { get; set; } // 报修说明 
        public int Fstatus { get; set; }
    }

    public class sli_mech_maintain_order_view  //报修单
    {
        public int Id { get; set; }
        public string Fnumber { get; set; } // 报修单号
        public int Fmechine { get; set; }// 模具Id
        public DateTime Fdate { get; set; }// 报修日期
        public string Fnote { get; set; } // 报修说明 
        public int Fstatus { get; set; }
        public string Fmouldnumber { get; set; }//模具编号
        public string Fname { get; set; }  //模具名称

    }
}