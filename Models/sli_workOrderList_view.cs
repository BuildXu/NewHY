using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_workorderlist_view
    {
        public int Id { get; set; } // 主键ID
        public decimal Fworkqty { get; set; } // 工件数量
        public decimal Fworkweight { get; set; } // 拆分重量
        public string Fbillno { get; set; } // 单据编号
        public DateTime Fdate { get; set; } // 日期
        public int Fcustid { get; set; } // 客户ID
        public string Fcustno { get; set; } // 客户编号
        public string Fcustname { get; set; } // 客户名称
        public string Fcustomer { get; set; } // 客户信息
        public int Fid { get; set; } // 关联ID
        public string Fentryid { get; set; } // 分录ID
        public int Fseq { get; set; } // 序号
        public decimal Fqty { get; set; } // 数量
        public string B_Fnote { get; set; } // 备注（B表）
        public DateTime Fplandeleliverydate { get; set; } // 计划交货日期
        public decimal Fstockqty { get; set; } // 库存数量
        public int B_Fmaterialid { get; set; } // 物料ID（B表）
        public string Fnumber { get; set; } // 编号
        public string Fname { get; set; } // 名称
        public string Fdescription { get; set; } // 描述
        public decimal Fsliouterdiameter { get; set; } // 外径
        public decimal Fsliinnerdiameter { get; set; } // 内径
        public decimal Fslihight { get; set; } // 高度
        public decimal Fsliallowanceod { get; set; } // 外径公差
        public decimal Fsliallowanceid { get; set; } // 内径公差
        public decimal Fsliallowanceh { get; set; } // 高度公差
        public decimal Fsliweightmaterial { get; set; } // 材料重量
        public decimal Fsliweightforging { get; set; } // 锻造重量
        public decimal Fsliweightgoods { get; set; } // 成品重量
        public string Fslidrawingno { get; set; } // 图纸编号
        public string Fslimetal { get; set; } // 金属类型
        public int Fsligoodsstatus { get; set; } // 货物状态
        public string Fsliprocessing { get; set; } // 加工方式
        public string Fslidelivery { get; set; } // 交货状态
        public string Fsliblankmodel { get; set; } // 毛坯型号
        public string Fslipunching { get; set; } // 冲压信息
        public decimal Fslitemperaturebegin { get; set; } // 开始温度
        public decimal Fslitemperatureend { get; set; } // 结束温度
        public string Fslimould { get; set; } // 模具
        public string Fsliroller { get; set; } // 轧辊规格
        public int Fsliheatingtimes { get; set; } // 加热次数
        public string Fsligrade { get; set; } // 等级
        public string Fsumnumber { get; set; } // 总编号（字符串类型）
    }
}