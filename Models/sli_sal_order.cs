using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace WebApi_SY.Models
{
    public class sli_sal_orderimport    //解析导入星空销售订单json
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
        public string IsControlPrecision { get; set; }
        public string ValidateRepeatJson { get; set; }
        public Model Model { get; set; }
    }
    public class Model
    {
        public int FID { get; set; }
        public BillTypeID FBillTypeID { get; set; }
        public DateTime FDate { get; set; }
        public OrgId FSaleOrgId { get; set; }
        public OrgId FCustId { get; set; }
        public OrgId FReceiveId { get; set; }
        public OrgId FSaleDeptId { get; set; }
        public OrgId FSalerId { get; set; }
        public OrgId FSettleId { get; set; }
        public OrgId FChargeId { get; set; }
        public int FNetOrderBillId { get; set; }
        public int FOppID { get; set; }
        public bool FISINIT { get; set; }
        public bool FIsMobile { get; set; }
        public int FContractId { get; set; }
        public bool FIsUseOEMBomPush { get; set; }
        public int FXPKID_H { get; set; }
        public bool FIsUseDrpSalePOPush { get; set; }
        public bool FIsCreateStraightOutIN { get; set; }
        public SaleOrderFinance FSaleOrderFinance { get; set; }
        public object FSalOrderRec { get; set; }
        public List<SaleOrderEntry> FSaleOrderEntry { get; set; }
    }

    // 对应JSON中的 "FBillTypeID" 等类似结构的类
    public class BillTypeID
    {
        public string FNUMBER { get; set; }
    }

    // 对应JSON中的 "FSaleOrgId" 等类似结构的类
    public class OrgId
    {
        public string FNumber { get; set; }
    }

    // 对应JSON中的 "FSaleOrderFinance" 部分的类
    public class SaleOrderFinance
    {
        public OrgId FSettleCurrId { get; set; }
        public bool FIsIncludedTax { get; set; }
        public bool FIsPriceExcludeTax { get; set; }
        public OrgId FExchangeTypeId { get; set; }
        public double FMarginLevel { get; set; }
        public double FMargin { get; set; }
        public bool FOverOrgTransDirect { get; set; }
        public double FAllDisCount { get; set; }
        public int FXPKID_F { get; set; }
    }

    // 对应JSON中的 "FSaleOrderEntry" 部分的类
    public class SaleOrderEntry
    {
        public string FRowType { get; set; }
        public OrgId FMaterialId { get; set; }
        public OrgId FUnitID { get; set; }
        public double FInventoryQty { get; set; }
        public double FCurrentInventory { get; set; }
        public double FAwaitQty { get; set; }
        public double FAvailableQty { get; set; }
        public double FQty { get; set; }
        public OrgId FPriceUnitId { get; set; }
        public double FOldQty { get; set; }
        public double FPrice { get; set; }
        public double FTaxPrice { get; set; }
        public bool FIsFree { get; set; }
        public double FEntryTaxRate { get; set; }
        public string FDeliveryDate { get; set; }
        public OrgId FStockOrgId { get; set; }
        public OrgId FSettleOrgIds { get; set; }
        public OrgId FSupplyOrgId { get; set; }
        public string FOwnerTypeId { get; set; }
        public OrgId FOwnerId { get; set; }
        public string FSrcType { get; set; }
        public string FReserveType { get; set; }
        public double FPriceBaseQty { get; set; }
        public OrgId FStockUnitID { get; set; }
        public double FStockQty { get; set; }
        public double FStockBaseQty { get; set; }
        public string FOUTLMTUNIT { get; set; }
        public OrgId FOutLmtUnitID { get; set; }
        public bool FISMRP { get; set; }
        public bool FISMRPCAL { get; set; }
        public double FAllAmountExceptDisCount { get; set; }
        public string FsliHeatTreatment { get; set; }
        public int FsliTestBarQty { get; set; }
        public OrgId FsliMetel { get; set; }
        public string FsliExplanation { get; set; }
        public string FsliNotice { get; set; }
        public string FsliWorkOrder { get; set; }
        public string FsliSaleOrder { get; set; }
        public string FsliQuotationNo { get; set; }
        public string FsliStockNo { get; set; }
        public string FsliBlank { get; set; }
        public string FsliDrawingNo { get; set; }
    }
}