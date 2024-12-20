using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_workOrderList
    {
        public int id { get; set; }
        public string forderEntryid { get; set; }   // 订单行ID
        public string fproductNumber { get; set; }  // 物料名称
        public int fmaterialid { get; set; }     //  物料id
        public decimal fworkQty { get; set; }  // 工件数量
        public decimal fworkWeight { get; set; }  // 拆分重量
        public string fnote { get; set; }    // 备注
        public int fworkOrderListStatus { get; set; } //  工件状态
        public string splittype { get; set; }  // 工件类型
    }
}