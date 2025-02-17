using Kingdee.BOS.WebApi.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_bd_material
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
        public string IsAutoSubmitAndAudit { get; set; }
        public MaterialModel Model { get; set; }
    }
    public class MaterialOrgId
    {
        public string FNumber { get; set; }
    }

    public class MaterialModel
    {
        public int FMATERIALID { get; set; }
        public MaterialOrgId FCreateOrgId { get; set; }
        public MaterialOrgId FUseOrgId { get; set; }
        public string FNumber { get; set; }
        public string FName { get; set; }
        public string FSpecification { get; set; }
        public MaterialOrgId FMaterialGroup { get; set; }
        public bool FDSMatchByLot { get; set; }
        public string FImgStorageType { get; set; }
        public bool FIsSalseByNet { get; set; }
        public bool FIsHandleReserve { get; set; }
        public double FsliOuterDiameter { get; set; }
        public double FsliInnerDiameter { get; set; }
        public double FsliHight { get; set; }
        public double FsliAllowanceOD { get; set; }
        public double FsliAllowanceID { get; set; }
        public double FsliAllownceH { get; set; }
        public double FsliWeightMaterial { get; set; }
        public double FsliWeightForging { get; set; }
        public double FsliWeightGoods { get; set; }
        public string FsliDrawingNo { get; set; }
        public MaterialOrgId FsliMetal { get; set; }
        public int FsliTemperatureBegin { get; set; }
        public int FsliTempratureEnd { get; set; }
        public int FsliHeatingTimes { get; set; }
        public FSubHeadEntity FSubHeadEntity { get; set; }
        public SubHeadEntityMaterial SubHeadEntity { get; set; }
        public SubHeadEntity1 SubHeadEntity1 { get; set; }
        public SubHeadEntity2 SubHeadEntity2 { get; set; }
        public SubHeadEntity3 SubHeadEntity3 { get; set; }
        public SubHeadEntity4 SubHeadEntity4 { get; set; }
        public SubHeadEntity5 SubHeadEntity5 { get; set; }
        public SubHeadEntity6 SubHeadEntity6 { get; set; }
        public SubHeadEntity7 SubHeadEntity7 { get; set; }
        public List<FEntityInvPty> FEntityInvPty { get; set; }
    }

    public class FSubHeadEntity
    {
        public bool FIsControlSal { get; set; }
        public double FLowerPercent { get; set; }
        public double FUpPercent { get; set; }
        public string FCalculateBase { get; set; }
        public double FMaxSalPrice_CMK { get; set; }
        public double FMinSalPrice_CMK { get; set; }
        public bool FIsAutoRemove { get; set; }
        public bool FIsMailVirtual { get; set; }
        public string FIsFreeSend { get; set; }
        public string FTimeUnit { get; set; }
        public double FRentFreeDura { get; set; }
        public double FPricingStep { get; set; }
        public double FMinRentDura { get; set; }
        public string FPriceType { get; set; }
        public double FRentBeginPrice { get; set; }
        public double FRentStepPrice { get; set; }
        public double FDepositAmount { get; set; }
        public double FLogisticsCount { get; set; }
        public double FRequestMinPackQty { get; set; }
        public double FMinRequestQty { get; set; }
        public bool FIsPrinttAg { get; set; }
        public bool FIsAccessory { get; set; }
        public bool FUploadSkuImage { get; set; }
    }
    public class SubHeadEntityMaterial
    {
        public string FErpClsID { get; set; }
        public string FFeatureItem { get; set; }
        public MaterialOrgId FCategoryID { get; set; }
        public MaterialOrgId FTaxType { get; set; }
        public MaterialOrgId FTaxRateId { get; set; }
        public MaterialOrgId FBaseUnitId { get; set; }
        public bool FIsPurchase { get; set; }
        public bool FIsInventory { get; set; }
        public bool FIsSubContract { get; set; }
        public bool FIsSale { get; set; }
        public bool FIsProduce { get; set; }
        public bool FIsAsset { get; set; }
        public double FGROSSWEIGHT { get; set; }
        public double FNETWEIGHT { get; set; }
        public MaterialOrgId FWEIGHTUNITID { get; set; }
        public double FLENGTH { get; set; }
        public double FWIDTH { get; set; }
        public double FHEIGHT { get; set; }
        public double FVOLUME { get; set; }
        public MaterialOrgId FVOLUMEUNITID { get; set; }
        public string FSuite { get; set; }
        public double FCostPriceRate { get; set; }
    }
    public class SubHeadEntity1
    {
        public MaterialOrgId FStoreUnitID { get; set; }
        public string FUnitConvertDir { get; set; }
        public bool FIsLockStock { get; set; }
        public bool FIsCycleCounting { get; set; }
        public string FCountCycle { get; set; }
        public int FCountDay { get; set; }
        public bool FIsMustCounting { get; set; }
        public bool FIsBatchManage { get; set; }
        public bool FIsKFPeriod { get; set; }
        public bool FIsExpParToFlot { get; set; }
        public int FExpPeriod { get; set; }
        public int FOnlineLife { get; set; }
        public double FRefCost { get; set; }
        public CurrencyId FCurrencyId { get; set; }
        public bool FIsEnableMinStock { get; set; }
        public bool FIsEnableMaxStock { get; set; }
        public bool FIsEnableSafeStock { get; set; }
        public bool FIsEnableReOrder { get; set; }
        public double FMinStock { get; set; }
        public double FSafeStock { get; set; }
        public double FReOrderGood { get; set; }
        public double FEconReOrderQty { get; set; }
        public double FMaxStock { get; set; }
        public bool FIsSNManage { get; set; }
        public bool FIsSNPRDTracy { get; set; }
        public string FSNManageType { get; set; }
        public string FSNGenerateTime { get; set; }
        public double FBoxStandardQty { get; set; }
    }

    public class CurrencyId
    {
        public string FNumber { get; set; }
    }

    public class SubHeadEntity2
    {
        public MaterialOrgId FSaleUnitId { get; set; }
        public MaterialOrgId FSalePriceUnitId { get; set; }
        public double FOrderQty { get; set; }
        public double FMinQty { get; set; }
        public double FMaxQty { get; set; }
        public double FOutStockLmtH { get; set; }
        public double FOutStockLmtL { get; set; }
        public double FAgentSalReduceRate { get; set; }
        public bool FIsATPCheck { get; set; }
        public bool FIsReturnPart { get; set; }
        public bool FIsInvoice { get; set; }
        public bool FIsReturn { get; set; }
        public bool FAllowPublish { get; set; }
        public bool FISAFTERSALE { get; set; }
        public bool FISPRODUCTFILES { get; set; }
        public bool FISWARRANTED { get; set; }
        public int FWARRANTY { get; set; }
        public string FWARRANTYUNITID { get; set; }
        public string FOutLmtUnit { get; set; }
        public bool FIsTaxEnjoy { get; set; }
        public string FTaxDiscountsType { get; set; }
        public bool FUnValidateExpQty { get; set; }
    }

    public class SubHeadEntity3
    {
        public double FBaseMinSplitQty { get; set; }
        public MaterialOrgId FPurchaseUnitId { get; set; }
        public MaterialOrgId FPurchasePriceUnitId { get; set; }
        public MaterialOrgId FPurchaseOrgId { get; set; }
        public bool FIsQuota { get; set; }
        public string FQuotaType { get; set; }
        public double FMinSplitQty { get; set; }
        public bool FIsVmiBusiness { get; set; }
        public bool FEnableSL { get; set; }
        public bool FIsPR { get; set; }
        public bool FIsReturnMaterial { get; set; }
        public bool FIsSourceControl { get; set; }
        public double FReceiveMaxScale { get; set; }
        public double FReceiveMinScale { get; set; }
        public int FReceiveAdvanceDays { get; set; }
        public int FReceiveDelayDays { get; set; }
        public MaterialOrgId FOBillTypeId { get; set; }
        public double FAgentPurPlusRate { get; set; }
        public int FPrintCount { get; set; }
        public double FMinPackCount { get; set; }
        public double FDailyOutQtySub { get; set; }
        public bool FIsEnableScheduleSub { get; set; }
    }

    public class SubHeadEntity4
    {
        public string FPlanMode { get; set; }
        public double FBaseVarLeadTimeLotSize { get; set; }
        public string FPlanningStrategy { get; set; }
        public MaterialOrgId FMfgPolicyId { get; set; }
        public string FOrderPolicy { get; set; }
        public int FFixLeadTime { get; set; }
        public string FFixLeadTimeType { get; set; }
        public int FVarLeadTime { get; set; }
        public string FVarLeadTimeType { get; set; }
        public int FCheckLeadTime { get; set; }
        public string FCheckLeadTimeType { get; set; }
        public string FOrderIntervalTimeType { get; set; }
        public int FOrderIntervalTime { get; set; }
        public double FMaxPOQty { get; set; }
        public double FMinPOQty { get; set; }
        public double FIncreaseQty { get; set; }
        public double FEOQ { get; set; }
        public double FVarLeadTimeLotSize { get; set; }
        public int FPlanIntervalsDays { get; set; }
        public double FPlanBatchSplitQty { get; set; }
        public int FRequestTimeZone { get; set; }
        public int FPlanTimeZone { get; set; }
        public bool FIsMrpComReq { get; set; }
        public int FCanLeadDays { get; set; }
        public bool FIsMrpComBill { get; set; }
        public int FLeadExtendDay { get; set; }
        public string FReserveType { get; set; }
        public bool FAllowPartAhead { get; set; }
        public double FPlanSafeStockQty { get; set; }
        public int FCanDelayDays { get; set; }
        public int FDelayExtendDay { get; set; }
        public bool FAllowPartDelay { get; set; }
        public string FPlanOffsetTimeType { get; set; }
        public int FPlanOffsetTime { get; set; }
        public double FWriteOffQty { get; set; }
        public double FDailyOutQty { get; set; }
    }

    public class SubHeadEntity5
    {
        public MaterialOrgId FProduceUnitId { get; set; }
        public double FFinishReceiptOverRate { get; set; }
        public double FFinishReceiptShortRate { get; set; }
        public MaterialOrgId FProduceBillType { get; set; }
        public MaterialOrgId FOrgTrustBillType { get; set; }
        public bool FIsProductLine { get; set; }
        public bool FIsSNCarryToParent { get; set; }
        public MaterialOrgId FBOMUnitId { get; set; }
        public double FConsumVolatility { get; set; }
        public double FLOSSPERCENT { get; set; }
        public bool FIsMainPrd { get; set; }
        public bool FIsCoby { get; set; }
        public bool FIsECN { get; set; }
        public string FIssueType { get; set; }
        public string FOverControlMode { get; set; }
        public double FMinIssueQty { get; set; }
        public bool FISMinIssueQty { get; set; }
        public bool FIsKitting { get; set; }
        public bool FIsCompleteSet { get; set; }
        public double FStdLaborPrePareTime { get; set; }
        public double FStdLaborProcessTime { get; set; }
        public double FStdMachinePrepareTime { get; set; }
        public double FStdMachineProcessTime { get; set; }
        public MaterialOrgId FMinIssueUnitId { get; set; }
        public string FStandHourUnitId { get; set; }
        public string FBackFlushType { get; set; }
        public double FFIXLOSS { get; set; }
        public bool FIsEnableSchedule { get; set; }
    }

    public class SubHeadEntity6
    {
        public bool FCheckIncoming { get; set; }
        public bool FCheckProduct { get; set; }
        public bool FCheckStock { get; set; }
        public bool FCheckReturn { get; set; }
        public bool FCheckDelivery { get; set; }
        public bool FEnableCyclistQCSTK { get; set; }
        public int FStockCycle { get; set; }
        public bool FEnableCyclistQCSTKEW { get; set; }
        public int FEWLeadDay { get; set; }
        public bool FCheckEntrusted { get; set; }
        public bool FCheckOther { get; set; }
        public bool FIsFirstInspect { get; set; }
        public bool FCheckReturnMtrl { get; set; }
        public bool FCheckSubRtnMtrl { get; set; }
        public string FFirstQCControlType { get; set; }
    }

    public class SubHeadEntity7
    {
        public MaterialOrgId FSubconUnitId { get; set; }
        public MaterialOrgId FSubconPriceUnitId { get; set; }
        public MaterialOrgId FSubBillType { get; set; }
    }

    public class FEntityInvPty
    {
        public InvPtyId FInvPtyId { get; set; }
        public bool FIsEnable { get; set; }
        public bool FIsAffectPrice { get; set; }
        public bool FIsAffectPlan { get; set; }
        public bool FIsAffectCost { get; set; }
    }

    public class InvPtyId
    {
        public string FNumber { get; set; }
    }
}
