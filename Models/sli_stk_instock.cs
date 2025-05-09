using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_stk_instock
    {
        /// <summary>
        /// 主键ID（注意：表中定义为NULL，建议添加标识列）
        /// </summary>
        [Key]
        public int Fid { get; set; }

        /// <summary>
        /// 单据编号
        /// </summary>
        public string FBillNo { get; set; }

        /// <summary>
        /// 单据日期
        /// </summary>
        public DateTime  FDate { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string FSpplierName { get; set; }

        /// <summary>
        /// 单据状态
        /// </summary>
        public string FDocumentStatus { get; set; }

        /// <summary>
        /// 关闭状态
        /// </summary>
        public string FCloseStatus { get; set; }

        /// <summary>
        /// 制单人ID
        /// </summary>
        public int? FBiller { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public int? FDepId { get; set; }

        /// <summary>
        /// 标志位
        /// </summary>
        public int Flag { get; set; }

        /// <summary>
        /// 参数（JSON格式）
        /// </summary>
        public string FParameter { get; set; }

        /// <summary>
        /// 原因说明
        /// </summary>
        public string FReason { get; set; }
        public virtual ICollection<sli_stk_instockentry> sli_stk_instockentry { get; set; }
    }

    public class sli_stk_instockentry
    {
        /// <summary>
        /// 主表ID（外键关联sli_stk_instock.Fid）
        /// </summary>
        public int? Fid { get; set; }

        /// <summary>
        /// 分录ID（建议与Fid组合作为复合主键）
        /// </summary>
        [Key]
        public int? FEntryId { get; set; }

        /// <summary>
        /// 物料编号
        /// </summary>
        public string FNumber { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public string FName { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 数量（主单位）
        /// </summary>
        public decimal? FQty { get; set; }

        /// <summary>
        /// 数量（辅单位）
        /// </summary>
        public decimal? FSecQty { get; set; }

        /// <summary>
        /// 仓库ID
        /// </summary>
        public int? FStockId { get; set; }

        public virtual sli_stk_instock sli_stk_instock { get; set; }
    }



    public class sli_stk_instock_view
    {
        /// <summary>
        /// 主键ID（注意：表中定义为NULL，建议添加标识列）
        /// </summary>
        [Key]
        public int Fid { get; set; }

        /// <summary>
        /// 单据编号
        /// </summary>
        public string FBillNo { get; set; }

        /// <summary>
        /// 单据日期
        /// </summary>
        public DateTime FDate { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string FSpplierName { get; set; }

        /// <summary>
        /// 单据状态
        /// </summary>
        public string FDocumentStatus { get; set; }

        /// <summary>
        /// 关闭状态
        /// </summary>
        public string FCloseStatus { get; set; }

        /// <summary>
        /// 制单人ID
        /// </summary>
        public int? FBiller { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public int? FDepId { get; set; }

        /// <summary>
        /// 标志位
        /// </summary>
        public int Flag { get; set; }

        /// <summary>
        /// 参数（JSON格式）
        /// </summary>
        public string FParameter { get; set; }

        /// <summary>
        /// 原因说明
        /// </summary>
        public string FReason { get; set; }

        public string empname { get; set; }   //制单人名称
        public virtual ICollection<sli_stk_instockentry_view> sli_stk_instockentry_view { get; set; }
    }

    public class sli_stk_instockentry_view
    {
        /// <summary>
        /// 主表ID（外键关联sli_stk_instock.Fid）
        /// </summary>
        public int? Fid { get; set; }

        /// <summary>
        /// 分录ID（建议与Fid组合作为复合主键）
        /// </summary>
        [Key]
        public int? FEntryId { get; set; }

        /// <summary>
        /// 物料编号
        /// </summary>
        public string FNumber { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public string FName { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 数量（主单位）
        /// </summary>
        public decimal? FQty { get; set; }

        /// <summary>
        /// 数量（辅单位）
        /// </summary>
        public decimal? FSecQty { get; set; }

        /// <summary>
        /// 仓库ID
        /// </summary>
        public int? FStockId { get; set; }

        public virtual sli_stk_instock_view sli_stk_instock_view { get; set; }
    }
}