using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace WebApi_SY.Models
{
    public class sli_pur_instock    // 解析导入星空销售订单json
    {
        //public List<string> NeedUpDateFields { get; set; }
        //public List<string> NeedReturnFields { get; set; }
        public string IsDeleteEntry { get; set; }
        public string SubSystemId { get; set; }
        public string IsVerifyBaseDataField { get; set; }
        public string IsEntryBatchFill { get; set; }
        public string ValidateFlag { get; set; }
        public string NumberSearch { get; set; }
        public string IsAutoAdjustField { get; set; }
        public string InterationFlags { get; set; }
        public string IsControlPrecision { get; set; }
        public string ValidateRepeatJson { get; set; }
        public int Flag { get; set; }
        public string FParameter { get; set; }
        public string FReason { get; set; }
        public int FID { get; set; }
        public string FNumber { get; set; }
        public Modelpur Modelpur { get; set; }
    }

    public class Modelpur
    {
        public int FID { get; set; }
        public Child FSupplierId { get; set; }
        public BillTypeIDpur FBillTypeID { get; set; }
        public DateTime? FDate { get; set; }
        public Child FSaleOrgId { get; set; }
        public Child FCustId { get; set; }
        public Child FReceiveId { get; set; }
        public Child FSaleDeptId { get; set; }
        public Child FSalerId { get; set; }
        public Child FSettleId { get; set; }
        public Child FChargeId { get; set; }
        public int? FNetOrderBillId { get; set; }
        public int? FOppID { get; set; }
        public bool? FISINIT { get; set; }
        public bool? FIsMobile { get; set; }
        public int? FContractId { get; set; }
        public bool? FIsUseOEMBomPush { get; set; }
        public int? FXPKID_H { get; set; }
        public bool? FIsUseDrpSalePOPush { get; set; }
        public bool? FIsCreateStraightOutIN { get; set; }
        public purFinance FSaleOrderFinance { get; set; }
        //public object FSalOrderRec { get; set; }
        public List<sli_pur_instockentry> sli_pur_instockentry { get; set; }
    }

    // 对应JSON中的 "FBillTypeID" 等类似结构的类
    public class BillTypeIDpur
    {
        public string FNUMBER { get; set; }
    }

    // 对应JSON中的 "FSaleOrgId" 等类似结构的类
    public class Child
    {
        public string FNumber { get; set; }
    }

    // 对应JSON中的 "FSaleOrderFinance" 部分的类
    public class purFinance
    {
        public Child FSettleCurrId { get; set; }
        public bool? FIsIncludedTax { get; set; }
        public bool? FIsPriceExcludeTax { get; set; }
        public Child FExchangeTypeId { get; set; }
        public double? FMarginLevel { get; set; }
        public double? FMargin { get; set; }
        public bool? FOverOrgTransDirect { get; set; }
        public double? FAllDisCount { get; set; }
        public int? FXPKID_F { get; set; }
    }

    // 对应JSON中的 "FSaleOrderEntry" 部分的类
    public class sli_pur_instockentry
    {
        public int? FID { get; set; }
        public string FRowType { get; set; }
        public string fmaterialNumber { get; set; }
        
        public Child FMaterialId { get; set; }
        public Child FUnitID { get; set; }
        public double? FInventoryQty { get; set; }

        //public double? fqty { get; set; }
        public double? FCurrentInventory { get; set; }
        public double? FAwaitQty { get; set; }
        public double? FAvailableQty { get; set; }
        public double? FQty { get; set; }
        public Child FPriceUnitId { get; set; }
        public double? FOldQty { get; set; }
        public double? FPrice { get; set; }
        public double? FTaxPrice { get; set; }
        public bool? FIsFree { get; set; }
        public double? FEntryTaxRate { get; set; }
        public string FDeliveryDate { get; set; }
        public Child FStockOrgId { get; set; }
        public Child FSettleOrgId { get; set; }
        public Child FSupplyOrgId { get; set; }
        public string FOwnerTypeId { get; set; }
        public Child FOwnerId { get; set; }
        public string FSrcType { get; set; }
        public string FReserveType { get; set; }
        public double? FPriceBaseQty { get; set; }
        public Child FStockUnitID { get; set; }
        public double? FStockQty { get; set; }
        public double? FStockBaseQty { get; set; }
        public string FOUTLMTUNIT { get; set; }
        public Child FOutLmtUnitID { get; set; }
        public bool? FISMRP { get; set; }
        public bool? FISMRPCAL { get; set; }
        public double? FAllAmountExceptDisCount { get; set; }
        //public string FsliHeatTreatment { get; set; }
        //public int? FsliTestBarQty { get; set; }
        //public Child FsliMetel { get; set; }
        //public string FsliExplanation { get; set; }
        //public string FsliNotice { get; set; }
        //public string FsliWorkOrder { get; set; }
        //public string FsliSaleOrder { get; set; }
        //public string FsliQuotationNo { get; set; }
        //public string FsliStockNo { get; set; }
        //public string FsliBlank { get; set; }
        //public string FsliDrawingNo { get; set; }
    }
}