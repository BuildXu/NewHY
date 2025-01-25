using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_mould_list
    {
        public int Id { get; set; }
        public string Fnumber { get; set; }  //模具代码
        public string Fname { get; set; }  //模具名称
        public int Fstatus { get; set; }//模具状态
    }
}