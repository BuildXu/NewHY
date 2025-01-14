using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{

    public class sli_witnessing_objectbill_view
    {

        public int Fsourceid { get; set; } //  销售订单行id
        public string Forderno { get; set; } //  销售编号
        public string Fcustomer { get; set; } //  客户名称
        public string Fmaterialname { get; set; } //  物料名称
        public string Fdescription { get; set; } //  规格型号
        public int Fentryid { get; set; } //  销售订单行id
        public int Id { get; set; }      // 数据id  自增
        public int Fseq { get; set; }  //  行号  
        public int Fobject { get; set; }   //  sli_bd_process_object  /  id
        public string Fnote { get; set; }   //  说明  
        public string Fobjectno { get; set; }   //  说明  
        public string Fobjectname { get; set; }   //  说明  
        public int Fstatus { get; set; }   //  状态

    }
}