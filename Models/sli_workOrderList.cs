using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_workOrderList
    {
        public int id { get; set; }
        //public string Fproductno { get; set; }
        public string Forderentryid { get; set; }   // 订单行ID
        public string Fproductno { get; set; }  // 物料名称
        public int Fmaterialid { get; set; }     //  物料id
        public decimal Fworkqty { get; set; }  // 工件数量
        public decimal Fworkweight { get; set; }  // 拆分重量
        public string Fnote { get; set; }    // 备注
        public int Fworkorderliststatus { get; set; } //  工件状态
        public string Fsplittype { get; set; }  // 工件类型  产品 / 加工件  /  试样  /  破坏件;
    }
}