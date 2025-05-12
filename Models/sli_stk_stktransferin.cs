using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    /// <summary>
    /// 直接调拨单
    /// </summary>
    public class sli_stk_stktransferin
    {
        /// <summary>
        /// 主键ID（注意：表中定义为NULL，建议添加标识列）
        /// </summary>
        [Key]
        public int Fid { get; set; }

        /// <summary>
        /// 单据编号
        /// </summary>
        public string Fbillno { get; set; }

        /// <summary>
        /// 单据日期
        /// </summary>
        public DateTime Fdate { get; set; }

        /// <summary>
        /// 单据状态
        /// </summary>
        public string Fdocumentstatus { get; set; }

        /// <summary>
        /// 关闭状态
        /// </summary>
        public string Fclosestatus { get; set; }

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
        public virtual ICollection<sli_stk_stktransferinentry> sli_stk_stktransferinentry { get; set; }
    }
    public class sli_stk_stktransferinentry
    {
        /// <summary>
        /// 主表ID（外键关联sli_stk_instock.Fid）
        /// </summary>
        public int Fid { get; set; }
        [Key]
        /// <summary>
        /// 分录ID（建议与Fid组合作为复合主键）
        /// </summary>
        public int? FentryId { get; set; }

        /// <summary>
        /// 物料编号
        /// </summary>
        public string Fnumber { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public string Fname { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }
        public string Fbatchno { get; set; }

        /// <summary>
        /// 数量（主单位）
        /// </summary>
        public decimal? Fqty { get; set; }

        /// <summary>
        /// 数量（辅单位）
        /// </summary>
        public decimal? FSecQty { get; set; }

        /// <summary>
        /// 调入仓库
        /// </summary>
        public string FReceiveStockNumber { get; set; }
        /// <summary>
        /// 调出仓库
        /// </summary>
        public string FStockNumber { get; set; }

        public virtual sli_stk_stktransferin sli_stk_stktransferin { get; set; }
    }

    public class sli_stk_stktransferin_view
    {
        /// <summary>
        /// 主键ID（注意：表中定义为NULL，建议添加标识列）
        /// </summary>
        [Key]
        public int Fid { get; set; }

        /// <summary>
        /// 单据编号
        /// </summary>
        public string Fbillno { get; set; }

        /// <summary>
        /// 单据日期
        /// </summary>
        public DateTime Fdate { get; set; }


        /// <summary>
        /// 单据状态
        /// </summary>
        public string Fdocumentstatus { get; set; }

        /// <summary>
        /// 关闭状态
        /// </summary>
        public string Fclosestatus { get; set; }

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
        public string dept_name { get; set; }   //部门名称 
        public virtual ICollection<sli_stk_stktransferinentry_view> sli_stk_stktransferinentry_view { get; set; }
    }
    public class sli_stk_stktransferinentry_view
    {
        /// <summary>
        /// 主表ID（外键关联sli_stk_instock.Fid）
        /// </summary>
        public int? Fid { get; set; }
        [Key]
        /// <summary>
        /// 分录ID（建议与Fid组合作为复合主键）
        /// </summary>
        public int? FentryId { get; set; }

        /// <summary>
        /// 物料编号
        /// </summary>
        public string Fnumber { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public string Fname { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }
        public string Fbatchno { get; set; }

        /// <summary>
        /// 数量（主单位）
        /// </summary>
        public decimal? Fqty { get; set; }

        /// <summary>
        /// 数量（辅单位）
        /// </summary>
        public decimal? FSecQty { get; set; }

        /// <summary>
        /// 仓库代码
        /// </summary>
        public string FReceiveStockNumber { get; set; }  //调入仓库
        public string FStockNumber { get; set; }//调出仓库

        public virtual sli_stk_stktransferin_view sli_stk_stktransferin_view { get; set; }
    }

    public class FNumberObject
    {
        public string FNumber { get; set; }
    }

    public class BillEntry
    {
        public string FRowType { get; set; }
        public FNumberObject FMaterialId { get; set; }
        public FNumberObject FUnitID { get; set; }
        public double FQty { get; set; }
        public FNumberObject FSrcStockId { get; set; }
        public FNumberObject FDestStockId { get; set; }
        public FNumberObject FSrcStockStatusId { get; set; }
        public FNumberObject FDestStockStatusId { get; set; }
        public string FBusinessDate { get; set; }
        public string FSrcBillTypeId { get; set; }
        public string FOwnerTypeOutId { get; set; }
        public FNumberObject FOwnerOutId { get; set; }
        public string FOwnerTypeId { get; set; }
        public FNumberObject FOwnerId { get; set; }
        public string FSrcBillNo { get; set; }
        public double FSecQty { get; set; }
        public double FExtAuxUnitQty { get; set; }
        public FNumberObject FBaseUnitId { get; set; }
        public double FBaseQty { get; set; }
        public bool FISFREE { get; set; }
        public string FKeeperTypeId { get; set; }
        public double FActQty { get; set; }
        public FNumberObject FKeeperId { get; set; }
        public string FKeeperTypeOutId { get; set; }
        public FNumberObject FKeeperOutId { get; set; }
        public double FDiscountRate { get; set; }
        public double FRepairQty { get; set; }
        public FNumberObject FDestMaterialId { get; set; }
        public FNumberObject FSaleUnitId { get; set; }
        public double FSaleQty { get; set; }
        public double FSalBaseQty { get; set; }
        public FNumberObject FPriceUnitID { get; set; }
        public double FPriceQty { get; set; }
        public double FPriceBaseQty { get; set; }
        public double FOutJoinQty { get; set; }
        public double FBASEOUTJOINQTY { get; set; }
        public int FSOEntryId { get; set; }
        public bool FTransReserveLink { get; set; }
        public int FQmEntryId { get; set; }
        public int FConvertEntryId { get; set; }
        public bool FCheckDelivery { get; set; }
        public int FBomEntryId { get; set; }
    }

    public class transferinModel
    {
        public int FID { get; set; }
        public string FBillNo { get; set; }
        public FNumberObject FBillTypeID { get; set; }
        public string FBizType { get; set; }
        public string FTransferDirect { get; set; }
        public string FTransferBizType { get; set; }
        public FNumberObject FSaleOrgId { get; set; }
        public FNumberObject FSettleOrgId { get; set; }
        public FNumberObject FStockOutOrgId { get; set; }
        public string FOwnerTypeOutIdHead { get; set; }
        public FNumberObject FOwnerOutIdHead { get; set; }
        public FNumberObject FStockOrgId { get; set; }
        public bool FIsPriceExcludeTax { get; set; }
        public FNumberObject FExchangeTypeId { get; set; }
        public bool FIsIncludedTax { get; set; }
        public FNumberObject FSettleCurrId { get; set; }
        public double FExchangeRate { get; set; }
        public string FOwnerTypeIdHead { get; set; }
        public FNumberObject FOwnerIdHead { get; set; }
        public string FDate { get; set; }
        public FNumberObject FBaseCurrId { get; set; }
        public bool FWriteOffConsign { get; set; }
        public List<BillEntry> FBillEntry { get; set; }
    }
    public class transferinRequest
    {
        public List<string> NeedUpDateFields { get; set; }
        public List<string> NeedReturnFields { get; set; }
        public string IsDeleteEntry { get; set; }
        public string SubSystemId { get; set; }
        public string IsVerifyBaseDataField { get; set; }
        public string IsEntryBatchFill { get; set; }
        public string ValidateFlag { get; set; }
        public string NumberSearch { get; set; }
        public string IsAutoAdjustField { get; set; }
        public string InterationFlags { get; set; }
        public string IgnoreInterationFlag { get; set; }
        public string IsControlPrecision { get; set; }
        public string ValidateRepeatJson { get; set; }
        public transferinModel Model { get; set; }
    }
}