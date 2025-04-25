using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_work_orders_view
    {
        //[Key]
        //public int Fwoid { get; set; } // 主键ID

        //public string Fwobillno { get; set; } // 单据编号

        //public DateTime Fwodate { get; set; } // 日期

        //public decimal Fqty { get; set; } // 数量

        //public decimal Fwoweight { get; set; } // 工件重量

        //// ... 其他字段 ...

        //public string Fproductno { get; set; } // 工件号

        //public decimal Fworkqty { get; set; } // 工件数量

        //public decimal Fworkweight { get; set; } // 工件重量

        //public DateTime Fplandeliverydate { get; set; } // 计划交货日期

        //public string Fslitemperatureend { get; set; } // 结束温度

        //public int Fcustid { get; set; } // 客户ID

        //public string Fcustno { get; set; } // 客户编号

        //public string Fcustname { get; set; } // 客户名称

        //public string Fcustomer { get; set; } // 客户信息

        //public int Fid { get; set; } // 关联ID

        //public int Fentryid { get; set; } // 分录ID

        //public int Fseq { get; set; } // 序号

        //public string Fnote { get; set; } // 备注（假设来自B表）

        //public int Fmaterialid { get; set; } // 物料ID（假设来自B表）

        //public string Fnumber { get; set; } // 物料编号（假设来自B表）

        //public string Fname { get; set; } // 物料名称（假设来自B表）

        //public string Fdescription { get; set; } // 描述（假设来自B表）

        //public decimal Fsliouterdiameter { get; set; } // 外径（假设来自B表）

        //public decimal Fsliinnerdiameter { get; set; } // 内径（假设来自B表）

        //public decimal Fslihight { get; set; } // 高度（假设来自B表）

        //public decimal Fsliallowanceod { get; set; } // 外径公差（假设来自B表）

        //public decimal Fsliallowanceid { get; set; } // 内径公差（假设来自B表）

        //public decimal Fsliallowanceh { get; set; } // 高度公差（假设来自B表）

        //public decimal Fsliweightmaterial { get; set; } // 材料重量（假设来自B表）

        //public decimal Fsliweightforging { get; set; } // 锻造重量（假设来自B表）

        //public decimal Fsliweightgoods { get; set; } // 成品重量（假设来自B表）

        //public string Fslidrawingno { get; set; } // 图号（假设来自B表）

        //public string Fslimetal { get; set; } // 材质（假设来自B表）

        //public string Fsligoodsstatus { get; set; } // 发货状态（假设来自B表）

        //public string Fsliprocessing { get; set; } // 加工要求（假设来自B表）

        //public string Fslidelivery { get; set; } // 交货状态（假设来自B表）

        //public string Fsliblankmodel { get; set; } // 毛坯型号（假设来自B表）

        //public string Fslipunching { get; set; } // 冲压信息（假设来自B表）

        //public int Fslitemperaturebegin { get; set; } // 开始温度（假设来自B表）

        //public string Fslimould { get; set; } // 模具（假设来自B表，ISNULL处理为空字符串）

        //public string Fsliroller { get; set; } // 轧辊规格（假设来自B表）

        //public int Fsliheatingtimes { get; set; } // 加热次数（假设来自B表）

        //public string Fsligrade { get; set; } // 锻件等级（假设来自B表）

        //public string Fsumnumber { get; set; } // 产品信息（假设来自B表）

        //public int Fsoqty { get; set; } // 订单数量

        //public int Fwoqty { get; set; } // 工单数量

        //public int Fwpqty { get; set; } // 工单计划数量

        //public int Ffinisthqty { get; set; } // 完工数量

        //public int Fstockqty { get; set; } // 库存数量

        //public int Foption { get; set; } // 当前工序

        //public int Fobject { get; set; } // 当前工步

        //public int Fmo { get; set; } // 已配料状态 0 未配 / 1 已配

        //public int Fworkorderliststatus { get; set; } // 状态 0 未审核 / 1 审核

        //public int Fpause { get; set; } // 暂停 0 / 1

        //public int Fcancel { get; set; } // 取消 0 / 1
        //public int Fworkordlistid { get; set; } // 取消 0 / 1


        [Key]
        public int Fwoid { get; set; }
        public string Fwobillno { get; set; }
        public DateTime? Fwodate { get; set; }
        public decimal? Fqty { get; set; }
        public decimal? Fwoweight { get; set; }
        public DateTime? Fwoplanstart { get; set; }
        public DateTime? Fwoplanend { get; set; }
        public string Fwoordertype { get; set; }
        public int? Fwoentryqty { get; set; }
        public int? Fwoecommitqty { get; set; }
        public int? Fwoestatus { get; set; }
        public int? Fwoeclosed { get; set; }
        public int Fworkordlistid { get; set; }
        public string Fproductno { get; set; }
        public decimal? Fworkqty { get; set; }
        public decimal? Fworkweight { get; set; }
        public string Forderbillno { get; set; }
        public string Fplandeleliverydate { get; set; }
        public string Fslitemperatureend { get; set; }
        public int Fcustid { get; set; }
        public string Fcustno { get; set; }
        public string Fcustname { get; set; }
        public string Fcustomer { get; set; }
        public int Fid { get; set; }
        public int Fentryid { get; set; }
        public int Fseq { get; set; }
        public decimal Forderqty { get; set; }
        public string Fnote { get; set; }
        public DateTime? Fplandeliverydate { get; set; }
        public string Fmaterialid { get; set; }
        public int Fnumber { get; set; }
        public string Fname { get; set; }
        public string Fdescription { get; set; }
        public string Fsliouterdiameter { get; set; }
        public decimal Fsliinnerdiameter { get; set; }
        public decimal Fslihight { get; set; }
        public decimal Fsliallowanceod { get; set; }
        public decimal Fsliallowanceid { get; set; }
        public decimal Fsliallowanceh { get; set; }
        public decimal Fsliweightmaterial { get; set; }
        public decimal Fsliweightforging { get; set; }
        public decimal Fsliweightgoods { get; set; }
        public decimal Fslidrawingno { get; set; }
        public string Fslimetal { get; set; }
        public string Fsligoodsstatus { get; set; }
        public string Fsliprocessing { get; set; }
        public string Fslidelivery { get; set; }
        public string Fsliblankmodel { get; set; }
        public string Fslipunching { get; set; }
        public string Fslitemperaturebegin { get; set; }
        public int Fslimould { get; set; }
        public string Fsliroller { get; set; }
        public string Fsliheatingtimes { get; set; }
        public int Fsligrade { get; set; }
        public string Fsumnumber { get; set; }
        public int Fsoqty { get; set; }
        public int Fwoqty { get; set; }
        public int Fwpqty { get; set; }
        public int Ffinisthqty { get; set; }
        public int Fstockqty { get; set; }
        public int Foption { get; set; }
        public int Fobject { get; set; }
        public int Fmo { get; set; }
        public int Fworkorderliststatus { get; set; }
        public int Fpause { get; set; }
        public int Fcancel { get; set; }
    }


    public class sli_work_orderprocess_view
    {
        [Key]
        public long RowNum { get; set; }  // 主键

        public int Id { get; set; }  // 主键

        public int Fprocessoption { get; set; }  // 工序ID

        public string Foptionname { get; set; }  // 工序选项名称

        public int Fseq { get; set; }  // 工序顺序

        public decimal Fqty { get; set; }  // 数量

        public decimal Fweight { get; set; }  // 重量

        public decimal Fcommitqty { get; set; }  // 提交数量

        public decimal Fcommitweight { get; set; }  // 提交重量

        // 外键关联到主表 sli_work_order_view
        //public int WorkOrderId { get; set; }  // 外键字段

        // 导航属性，表示与主表的关系
        //public sli_work_order_view sli_work_order_view { get; set; }
    }
}