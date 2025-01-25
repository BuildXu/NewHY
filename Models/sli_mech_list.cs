using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_mech_list    //设备档案
    {
        public int Id { get; set; }
        public string Fnumber { get; set; }   //设备代码
        public string Fname { get; set; }   //设备名称
        public int Fstatus { get; set; }  //设备状态
    }
}