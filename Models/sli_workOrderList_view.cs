using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_workorderlist_view
    {
        //public int Id { get; set; } // 主键ID
        //public string Fproductno { get; set; } // 工件号
        //public decimal Fworkqty { get; set; } // 工件数量
        //public decimal Fworkweight { get; set; } // 工件重量
        //public string Fbillno { get; set; } // 单据编号
        //public DateTime Fdate { get; set; } // 日期
        //public int Fcustid { get; set; } // 客户ID
        //public string Fcustno { get; set; } // 客户编号
        //public string Fcustname { get; set; } // 客户名称
        //public string Fcustomer { get; set; } // 客户信息
        //public int Fid { get; set; } // 关联ID
        //public string Fentryid { get; set; } // 分录ID
        //public int Fseq { get; set; } // 序号
        //public decimal Fqty { get; set; } // 数量
        //public string Fnote { get; set; } // 备注（B表）
        //public DateTime Fplandeleliverydate { get; set; } // 计划交货日期
        //public decimal Fstockqty { get; set; } // 库存数量
        //public int Fmaterialid { get; set; } // 物料ID（B表）
        //public string Fnumber { get; set; } // 物料编号
        //public string Fname { get; set; } // 物料名称
        //public string Fdescription { get; set; } // 描述
        //public decimal Fsliouterdiameter { get; set; } // 外径
        //public decimal Fsliinnerdiameter { get; set; } // 内径
        //public decimal Fslihight { get; set; } // 高度
        //public decimal Fsliallowanceod { get; set; } // 外径公差
        //public decimal Fsliallowanceid { get; set; } // 内径公差
        //public decimal Fsliallowanceh { get; set; } // 高度公差
        //public decimal Fsliweightmaterial { get; set; } // 材料重量
        //public decimal Fsliweightforging { get; set; } // 锻造重量
        //public decimal Fsliweightgoods { get; set; } // 成品重量
        //public string Fslidrawingno { get; set; } // 图号
        //public string Fslimetal { get; set; } // 材质
        //public int Fsligoodsstatus { get; set; } // 发货状态
        //public string Fsliprocessing { get; set; } // 加工要求
        //public string Fslidelivery { get; set; } // 交货状态
        //public string Fsliblankmodel { get; set; } // 毛坯型号
        //public string Fslipunching { get; set; } // 冲压信息
        //public decimal Fslitemperaturebegin { get; set; } // 开始温度
        //public decimal Fslitemperatureend { get; set; } // 结束温度
        //public string Fslimould { get; set; } // 模具
        //public string Fsliroller { get; set; } // 轧辊规格
        //public int Fsliheatingtimes { get; set; } // 加热次数
        //public string Fsligrade { get; set; } // 锻件等级
        //public string Fsumnumber { get; set; } // 产品信息
        //[Key]
        //public int Id { get; set; }

        //public int Fworkorderlistid { get; set; }  //  视图里加一个   id   就是工件号  id
        //public string Fproductno { get; set; }
        //public decimal? Fworkqty { get; set; }
        //public decimal? Fworkweight { get; set; }
        //public string Fbillno { get; set; }
        //public DateTime Fdate { get; set; }
        //public string Fplandeleliverydate { get; set; }
        //public string Fslitemperatureend { get; set; }
        //public int Fcustid { get; set; }
        //public string Fcustno { get; set; }
        //public string Fcustname { get; set; }
        //public string Fcustomer { get; set; }
        //public int Fid { get; set; }
        //public int Fentryid { get; set; }
        //public int Fseq { get; set; }
        //public decimal Fqty { get; set; }
        //public string Fnote { get; set; }
        //public DateTime? Fplandeliverydate { get; set; }
        //public decimal Fstockqty { get; set; }
        //public int Fmaterialid { get; set; }
        //public string Fnumber { get; set; }
        //public string Fname { get; set; }
        //public string Fdescription { get; set; }
        //public decimal Fsliouterdiameter { get; set; }
        //public decimal Fsliinnerdiameter { get; set; }
        //public decimal Fslihight { get; set; }
        //public decimal Fsliallowanceod { get; set; }
        //public decimal Fsliallowanceid { get; set; }
        //public decimal Fsliallowanceh { get; set; }
        //public decimal Fsliweightmaterial { get; set; }
        //public decimal Fsliweightforging { get; set; }
        //public decimal Fsliweightgoods { get; set; }
        //public string Fslidrawingno { get; set; }
        //public string Fslimetal { get; set; }
        //public string Fsligoodsstatus { get; set; }
        //public string Fsliprocessing { get; set; }
        //public string Fslidelivery { get; set; }
        //public string Fsliblankmodel { get; set; }
        //public string Fslipunching { get; set; }
        //public int Fslitemperaturebegin { get; set; }
        //public int Fslitempratureend { get; set; }
        //public string Fslimould { get; set; }
        //public string Fsliroller { get; set; }
        //public int Fsliheatingtimes { get; set; }
        //public string Fsligrade { get; set; }
        //public string Fsumnumber { get; set; }
        //public string Fsplittype { get; set; }


        //// 额外字段----------------------------------------新增的

        //public int Fsoqty { get; set; } // 订单数量

        //public int Fwoqty { get; set; } // 工单数量

        //public int Fwpqty { get; set; } // 工单计划 数量

        //public int Ffinisthqty { get; set; } // 完工数量

        ////public int Fstockqty { get; set; } // 库存数量

        //public int Foption { get; set; } // 当前工序  ---对应 option

        //public int Fobject { get; set; } // 当前工步   ---对应 object

        //public int Fmo { get; set; }    // 已配料状态   0 未配 /  1  已配
        //public int Fstatus { get; set; } //  状态 0   未审核 / 1  审核

        //public int Fpause { get; set; } // 暂停   --- 0  /   1

        //public int Fcancel { get; set; } // 取消   --- 0  / 1


        public int Fworkorderlistid { get; set; }
        [Key]
        public int Id { get; set; }
        public string Fproductno { get; set; }
        public decimal? Fworkqty { get; set; }
        public decimal? Fworkweight { get; set; }
        public string Fbillno { get; set; }
        public DateTime Fdate { get; set; }
        public string Fplandeleliverydate { get; set; }
        public string Fslitemperatureend { get; set; }
        public int Fcustid { get; set; }
        public string Fcustno { get; set; }
        public string Fcustname { get; set; }
        public string Fcustomer { get; set; }
        public int Fid { get; set; }
        public int Fentryid { get; set; }
        public int Fseq { get; set; }
        public decimal Fqty { get; set; }
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
        public int Fslitempratureend { get; set; }
        public int Fslimould { get; set; }
        public string Fsliroller { get; set; }
        public string Fsliheatingtimes { get; set; }
        public int Fsligrade { get; set; }
        public string Fsumnumber { get; set; }
        public string Fsplittype { get; set; }
        public int? Fsoqty { get; set; }
        public int? Fwoqty { get; set; }
        public int? Fwpqty { get; set; }
        public int? Ffinisthqty { get; set; }
        public int? Fstockqty { get; set; }
        public int? Foption { get; set; }
        public int? Fobject { get; set; }
        public int? Fmo { get; set; }
        public int? Fstatus { get; set; }
        public int? Fpause { get; set; }
        public int? Fcancel { get; set; }
    }
}