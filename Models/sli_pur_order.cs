using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    /// <summary>
    /// 采购订单实体类    查询试图
    /// </summary>
    public class sli_pur_po_view
    {
        [Key]
        public int Fid { get; set; } // 订单Id
        public string Fbillno { get; set; } // 订单号
        public DateTime Fdate { get; set; } // 日期
        public string Fsppliername { get; set; } // 供应商
        public string Fdocumentstatus { get; set; } // 单据状态
        public string Fclosestatus { get; set; } // 关闭状态
        public int ? Flag { get; set; } // 同步状态
        public string FParameter { get; set; } // 同步参数
        public string FReason { get; set; } // 同步结果
        public virtual ICollection<sli_pur_poentry_view> sli_pur_poentry_view { get; set; }
    }
    public class sli_pur_poentry_view
    {
        [Key]
        public int Fentryid { get; set; } // 行Id
        public int Fid { get; set; } // 订单Id (外键)
        public string Fnumber { get; set; } // 物料代码
        public string Fname { get; set; } // 物料名称
        public string Unit { get; set; } // 单位
        public int Fqty { get; set; } // 数量
        public DateTime? Fdeliverydate { get; set; } // 交货期
        public string Fmrpclosestatus { get; set; } // MRP关闭状态
        public int Fseq { get; set; } // 行序号
        public decimal? Finventory { get; set; } // 即时库存
        public virtual sli_pur_po_view sli_pur_po_view { get; set; }
    }

    /// <summary>
    /// 采购订单实体类  
    /// </summary>
    public class sli_pur_po
    {
        [Key]
        public int Fid { get; set; } // 订单Id
        public string Fbillno { get; set; } // 订单号
        public DateTime Fdate { get; set; } // 日期
        public string Fsppliername { get; set; } // 供应商名称
        public string Fdocumentstatus { get; set; } // 单据状态  
        public string Fclosestatus { get; set; } // 关闭状态
        public int ? Flag { get; set; } // 同步状态
        public string FParameter { get; set; } // 同步参数
        public string FReason { get; set; } // 同步结果
        public virtual ICollection<sli_pur_poentry> sli_pur_poentry { get; set; }
    }
    public class sli_pur_poentry
    {
        [Key]
        public int Fentryid { get; set; } // 行Id
        public int Fid { get; set; } // 订单Id (外键)
        public string Fnumber { get; set; } // 物料代码
        public string Fname { get; set; } // 物料名称
        public string Unit { get; set; } // 单位
        public int Fqty { get; set; } // 数量
        public DateTime? Fdeliverydate { get; set; } // 交货期
        public string Fmrpclosestatus { get; set; } // MRP关闭状态
        public int Fseq { get; set; } // 行序号
        public decimal? Finventory { get; set; } // 即时库存
        public virtual sli_pur_po sli_pur_po { get; set; }
    }

    ///拼接json
    public class PurchaseOrderRequest
    {
        public List<string> NeedUpDateFields { get; set; } = new List<string>();
        public List<string> NeedReturnFields { get; set; } = new List<string>();
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
        public PurchaseOrderModel Model { get; set; }
    }

    public class PurchaseOrderModel
    {
        public int FID { get; set; }
        public NumberField FBillTypeID { get; set; }
        public string FBusinessType { get; set; }
        public string FDate { get; set; }   //
        public string FBillNo { get; set; }   //
        public NumberField FSupplierId { get; set; }
        public NumberField FPurchaseOrgId { get; set; }
        public NumberField FProviderId { get; set; }
        public NumberField FSettleId { get; set; }
        public NumberField FChargeId { get; set; }
        public string FSourceBillNo { get; set; }
        public bool FIsModificationOperator { get; set; }
        public string FChangeStatus { get; set; }
        public string FACCTYPE { get; set; }
        public bool FIsMobBill { get; set; }
        public POOrderFinance FPOOrderFinance { get; set; }
        public object FPOOrderPay { get; set; }
        public List<POOrderEntry> FPOOrderEntry { get; set; }
    }

    public class NumberField
    {
        public string FNumber { get; set; }
    }

    public class POOrderFinance
    {
        public NumberField FSettleCurrId { get; set; }
        public NumberField FExchangeTypeId { get; set; }
        public double FExchangeRate { get; set; }
        public string FPriceTimePoint { get; set; }
        public NumberField FFOCUSSETTLEORGID { get; set; }
        public bool FIsIncludedTax { get; set; }
        public bool FISPRICEEXCLUDETAX { get; set; }
        public NumberField FLocalCurrId { get; set; }
        public double FPAYADVANCEAMOUNT { get; set; }
        public double FSupToOderExchangeBusRate { get; set; }
        public bool FSEPSETTLE { get; set; }
        public double FDepositRatio { get; set; }
        public double FAllDisCount { get; set; }
        public double FUPPERBELIEL { get; set; }
    }

    public class POOrderEntry
    {
        public string FProductType { get; set; }
        public NumberField FMaterialId { get; set; }
        public string FMaterialDesc { get; set; }
        public NumberField FUnitId { get; set; }
        public double FQty { get; set; }
        public NumberField FPriceUnitId { get; set; }
        public double FPriceUnitQty { get; set; }
        public double FPriceBaseQty { get; set; }
        public string FDeliveryDate { get; set; }
        public double FPrice { get; set; }
        public double FTaxPrice { get; set; }
        public double FEntryDiscountRate { get; set; }
        public double FEntryTaxRate { get; set; }
        public NumberField FRequireOrgId { get; set; }
        public NumberField FReceiveOrgId { get; set; }
        public NumberField FEntrySettleOrgId { get; set; }
        public bool FGiveAway { get; set; }
        public NumberField FStockUnitID { get; set; }
        public double FStockQty { get; set; }
        public double FStockBaseQty { get; set; }
        public bool FDeliveryControl { get; set; }
        public bool FTimeControl { get; set; }
        public double FDeliveryMaxQty { get; set; }
        public double FDeliveryMinQty { get; set; }
        public int FDeliveryBeforeDays { get; set; }
        public int FDeliveryDelayDays { get; set; }
        public string FDeliveryEarlyDate { get; set; }
        public string FDeliveryLastDate { get; set; }
        public double FPriceCoefficient { get; set; }
        public double FConsumeSumQty { get; set; }
        public string FSrcBillTypeId { get; set; }
        public string FSrcBillNo { get; set; }
        public int FDEMANDBILLENTRYSEQ { get; set; }
        public int FDEMANDBILLENTRYID { get; set; }
        public bool FPlanConfirm { get; set; }
        public NumberField FSalUnitID { get; set; }
        public double FSalQty { get; set; }
        public double FSalJoinQty { get; set; }
        public double FBaseSalJoinQty { get; set; }
        public double FInventoryQty { get; set; }
        public NumberField FCentSettleOrgId { get; set; }
        public NumberField FDispSettleOrgId { get; set; }
        public int FGroup { get; set; }
        public NumberField FDeliveryStockStatus { get; set; }
        public double FMaxPrice { get; set; }
        public double FMinPrice { get; set; }
        public bool FIsStock { get; set; }
        public double FBaseConsumeSumQty { get; set; }
        public double FSalBaseQty { get; set; }
        public NumberField FSubOrgId { get; set; }
        public NumberField FEntryPayOrgId { get; set; }
        public double FPriceDiscount { get; set; }
        public double FAllAmountExceptDisCount { get; set; }
        public int FSUBREQBILLSEQ { get; set; }
        public int FSUBREQENTRYID { get; set; }
        public List<EntryDeliveryPlan> FEntryDeliveryPlan { get; set; }
    }

    public class EntryDeliveryPlan
    {
        public string FDeliveryDate_Plan { get; set; }
        public double FPlanQty { get; set; }
        public string FPREARRIVALDATE { get; set; }
        public int FTRLT { get; set; }
        public double FConfirmDeliQty { get; set; }
    }



}