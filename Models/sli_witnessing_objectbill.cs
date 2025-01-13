using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_witnessing_objectbill
    {
       
        public int Fentryid { get; set; } //  销售订单行id
  
        public int Id { get; set; }      // 数据id  自增

        public int Fseq { get; set; }  //  行号  

        public int Fobject { get; set; }   //  sli_bd_process_object  /  id

        public string Fnote { get; set; }   //  说明  

        public int Fstatus { get; set; }   //  状态
    }
}