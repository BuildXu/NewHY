using MathNet.Numerics.Statistics.Mcmc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class Numberdelivery
    {
        public string FNumber { get; set; }
    }

    // 根对象类，对应整个 JSON 的结构
    public class RootObjectdelivery
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
        public Modeldelivery Model { get; set; }
    }

    // Model 类，包含多个子对象和列表
    public class Modeldelivery
    {
        public int FID { get; set; }
        public Numberdelivery FBillTypeID { get; set; }
        public DateTime FDate { get; set; }
        public Numberdelivery FSaleOrgId { get; set; }
        public Numberdelivery FCustomerID { get; set; }
        public Numberdelivery FReceiverID { get; set; }
        public Numberdelivery FStockOrgId { get; set; }
        public Numberdelivery FSettleID { get; set; }
        public Numberdelivery FPayerID { get; set; }
        public string FOwnerTypeIdHead { get; set; }
        public int FCDateOffsetValue { get; set; }
        public bool FIsTotalServiceOrCost { get; set; }
        public SubHeadEntity SubHeadEntity { get; set; }
        public List<FEntity> FEntity { get; set; }
    }

    // SubHeadEntity 类，包含结算相关信息
    public class SubHeadEntity
    {
        public Numberdelivery FSettleCurrID { get; set; }
        public Numberdelivery FSettleOrgID { get; set; }
        public bool FIsIncludedTax { get; set; }
        public Numberdelivery FLocalCurrID { get; set; }
        public Numberdelivery FExchangeTypeID { get; set; }
        public double FExchangeRate { get; set; }
        public bool FIsPriceExcludeTax { get; set; }
        public double FAllDisCount { get; set; }
    }

    public class FEntity
    {
        public string FRowType { get; set; }
        public Numberdelivery FMaterialID { get; set; }
        public Numberdelivery FUnitID { get; set; }
        public double FInventoryQty { get; set; }
        public double FRealQty { get; set; }
        public double FDisPriceQty { get; set; }
        public double FPrice { get; set; }
        public double FTaxPrice { get; set; }
        public bool FIsFree { get; set; }
        public string FOwnerTypeID { get; set; }
        public Numberdelivery FOwnerID { get; set; }
        public Numberdelivery FLot { get; set; }
        public double FEntryTaxRate { get; set; }
        public double FAuxUnitQty { get; set; }
        public Numberdelivery FExtAuxUnitId { get; set; }
        public double FExtAuxUnitQty { get; set; }
        public Numberdelivery FStockID { get; set; }
        public Numberdelivery FStockStatusID { get; set; }
        public string FSrcType { get; set; }
        public string FSrcBillNo { get; set; }
        public double FDiscountRate { get; set; }
        public double FPriceDiscount { get; set; }
        public double FActQty { get; set; }
        public Numberdelivery FSalUnitID { get; set; }
        public double FSALUNITQTY { get; set; }
        public double FSALBASEQTY { get; set; }
        public double FPRICEBASEQTY { get; set; }
        public bool FOUTCONTROL { get; set; }
        public double FRepairQty { get; set; }
        public bool FIsOverLegalOrg { get; set; }
        public double FARNOTJOINQTY { get; set; }
        public int FQmEntryID { get; set; }
        public int FConvertEntryID { get; set; }
        public int FSOEntryId { get; set; }
        public double FBeforeDisPriceQty { get; set; }
        public double FSignQty { get; set; }
        public bool FCheckDelivery { get; set; }
        public double FAllAmountExceptDisCount { get; set; }
        public bool FSettleBySon { get; set; }
        public int FBOMEntryId { get; set; }
        public Numberdelivery FMaterialID_Sal { get; set; }
        public int FInStockEntryId { get; set; }
        public int FReceiveEntryId { get; set; }
        public bool FIsReplaceOut { get; set; }
        public bool FVmiBusinessStatus { get; set; }
        public List<FEntity_Link> FEntity_Link { get; set; }
    }
    public class FEntity_Link
    {
        public string FEntity_Link_FRuleId { get; set; }
        public string FEntity_Link_FSTableName { get; set; }
        public int FEntity_Link_FSBillId { get; set; }
        public int FEntity_Link_FSId { get; set; }
        public decimal FEntity_Link_FBaseUnitQtyOld { get; set; }
        public decimal FEntity_Link_FBaseUnitQty { get; set; }
        public decimal FEntity_Link_FSALBASEQTYOld { get; set; }
        public decimal FEntity_Link_FSALBASEQTY { get; set; }
    }
}