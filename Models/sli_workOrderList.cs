using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_workOrderList
    {
        public int id { get; set; } // ID

        public string Forderentryid { get; set; } // 订单行ID

        public string Fproductno { get; set; } // 物料名称

        public int Fmaterialid { get; set; } // 物料ID

        public decimal Fworkqty { get; set; } // 工件数量

        public decimal Fworkweight { get; set; } // 拆分重量

        public string Fnote { get; set; } // 备注

        public int Fworkorderliststatus { get; set; } // 工件状态

        public string Fsplittype { get; set; } // 工件类型（产品 / 加工件 / 试样 / 破坏件）

        // 额外字段----------------------------------------新增的

        public int Fsoqty { get; set; } // 订单数量

        public int Fwoqty { get; set; } // 工单数量

        public int Fwpqty { get; set; } // 工单计划 数量

        public int Ffinisthqty { get; set; } // 完工数量

        public int Fstockqty { get; set; } // 库存数量

        public int Foption { get; set; } // 当前工序  ---对应 option

        public int Fobject { get; set; } // 当前工步   ---对应 object

        public int Fmo { get; set; } // 配料状态
        public int Fstatus { get; set; } //  状态 0 / 1

        public int Fpause { get; set; } // 暂停   --- 0  /   1

        public int Fcancel { get; set; } // 取消   --- 0  / 1
    }
}