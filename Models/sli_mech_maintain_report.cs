﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_mech_maintain_report
    {
        public int Id { get; set; }
        public int Fsourceid { get; set; }  //报修单id	
        public int Fmechine { get; set; }//设备 Id
        public string Fnumber { get; set; } // 维修单号
        public DateTime Fdate { get; set; }// 维修日期
        public int Fempid { get; set; }// 维修员
        public string Fnote { get; set; } //  维护说明 
        public int Fbreak { get; set; } // 故障类型
        public int Fstatus { get; set; }
    }
    public class sli_mech_maintain_report_view
    {
        public int Id { get; set; }
        public int Fsourceid { get; set; }  //报修单id	
        public int Fmechine { get; set; }//设备 Id
        public string Fnumber { get; set; } // 维修单号
        public DateTime Fdate { get; set; }// 维修日期
        public int Fempid { get; set; }// 维修员
        public string Fnote { get; set; } //  维护说明 
        public int Fbreak { get; set; } // 故障类型
        public int Fstatus { get; set; }
        public string Fmechnumber { get; set; }//设备编号
        public string Fname { get; set; }  //模具名称
        public string FempName { get; set; }  //维修员  
        public string Fbreakname { get; set; }  //故障名称
    }
}