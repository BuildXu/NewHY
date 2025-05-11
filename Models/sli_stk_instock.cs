using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

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
        public string Fbillno { get; set; }

        /// <summary>
        /// 单据日期
        /// </summary>
        public DateTime Fdate { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string Fsppliername { get; set; }

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
        public virtual ICollection<sli_stk_instockentry> sli_stk_instockentry { get; set; }
    }

    public class sli_stk_instockentry
    {
        /// <summary>
        /// 主表ID（外键关联sli_stk_instock.Fid）
        /// </summary>
        public int Fid { get; set; }
        [Key]
        /// <summary>
        /// 分录ID（建议与Fid组合作为复合主键）
        /// </summary>
        public int? FEntryId { get; set; }

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
        public string  FReceiveStockNumber { get; set; }

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
        public string Fbillno { get; set; }

        /// <summary>
        /// 单据日期
        /// </summary>
        public DateTime Fdate { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string Fsppliername { get; set; }

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
        public virtual ICollection<sli_stk_instockentry_view> sli_stk_instockentry_view { get; set; }
    }

    public class sli_stk_instockentry_view
    {
        /// <summary>
        /// 主表ID（外键关联sli_stk_instock.Fid）
        /// </summary>
        public int? Fid { get; set; }
        [Key]
        /// <summary>
        /// 分录ID（建议与Fid组合作为复合主键）
        /// </summary>
        public int? Fentryid { get; set; }

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
        public string FReceiveStockNumber { get; set; }

        public virtual sli_stk_instock_view sli_stk_instock_view { get; set; }
    }

    public class FNumberWrapper
    {
        public string FNumber { get; set; }
    }

    public class InStockFinance
    {
        public FNumberWrapper FSettleOrgId { get; set; }
        public FNumberWrapper FSettleCurrId { get; set; }
        public bool FIsIncludedTax { get; set; }
        public string FPriceTimePoint { get; set; }
        public FNumberWrapper FLocalCurrId { get; set; }
        public FNumberWrapper FExchangeTypeId { get; set; }
        public double FExchangeRate { get; set; }
        public bool FISPRICEEXCLUDETAX { get; set; }
        public double FAllDisCount { get; set; }
        public double FHSExchangeRate { get; set; }
    }

    public class InStockEntry
    {
        public string FRowType { get; set; }
        public FNumberWrapper FMaterialId { get; set; }
        public FNumberWrapper FUnitID { get; set; }
        public string FMaterialDesc { get; set; }
        public double FWWPickMtlQty { get; set; }
        public double FRealQty { get; set; }
        public FNumberWrapper FPriceUnitID { get; set; }
        public double FPrice { get; set; }
        public FNumberWrapper FStockId { get; set; }
        public double FDisPriceQty { get; set; }
        public FNumberWrapper FStockStatusId { get; set; }
        public bool FGiveAway { get; set; }
        public string FOWNERTYPEID { get; set; }
        public double FExtAuxUnitQty { get; set; }
        public bool FCheckInComing { get; set; }
        public bool FIsReceiveUpdateStock { get; set; }
        public double FInvoicedJoinQty { get; set; }
        public double FPriceBaseQty { get; set; }
        public FNumberWrapper FRemainInStockUnitId { get; set; }
        public bool FBILLINGCLOSE { get; set; }
        public double FRemainInStockQty { get; set; }
        public double FAPNotJoinQty { get; set; }
        public double FRemainInStockBaseQty { get; set; }
        public double FTaxPrice { get; set; }
        public double FEntryTaxRate { get; set; }
        public double FDiscountRate { get; set; }
        public double FCostPrice { get; set; }
        public double FAuxUnitQty { get; set; }
        public FNumberWrapper FOWNERID { get; set; }
        public string FSRCBILLTYPEID { get; set; }
        public string FSRCBillNo { get; set; }
        public double FAllAmountExceptDisCount { get; set; }
        public double FPriceDiscount { get; set; }
        public double FConsumeSumQty { get; set; }
        public double FBaseConsumeSumQty { get; set; }
        public double FRejectsDiscountAmount { get; set; }
        public int FSalOutStockEntryId { get; set; }
        public double FBeforeDisPriceQty { get; set; }
        public int FPayableEntryID { get; set; }
        public int FSUBREQBILLSEQ { get; set; }
        public int FSUBREQENTRYID { get; set; }
    }

    public class InStockModel
    {
        public int FID { get; set; }
        public FNumberWrapper FBillTypeID { get; set; }
        public string FBusinessType { get; set; }
        public string FBillNo { get; set; }   //
        public string FDate { get; set; }
        public FNumberWrapper FStockOrgId { get; set; }
        public FNumberWrapper FDemandOrgId { get; set; }
        public FNumberWrapper FPurchaseOrgId { get; set; }
        public FNumberWrapper FSupplierId { get; set; }
        public FNumberWrapper FSupplyId { get; set; }
        public FNumberWrapper FSettleId { get; set; }
        public FNumberWrapper FChargeId { get; set; }
        public string FOwnerTypeIdHead { get; set; }
        public FNumberWrapper FOwnerIdHead { get; set; }
        public int FCDateOffsetValue { get; set; }
        public string FSplitBillType { get; set; }
        public FNumberWrapper FSalOutStockOrgId { get; set; }
        public InStockFinance FInStockFin { get; set; }
        public List<InStockEntry> FInStockEntry { get; set; }
    }

    public class InStockRequest
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
        public InStockModel Model { get; set; }
    }
}