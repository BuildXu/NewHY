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
        public MATERIAL_Model Model { get; set; }
    }

    // 主模型中的子模型
    public class MATERIAL_Model
    {
        public int FMATERIALID { get; set; }
        public OrgId FCreateOrgId { get; set; }
        public OrgId FUseOrgId { get; set; }
        public string FName { get; set; }
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
        public int FsliTemperatureBegin { get; set; }
        public int FsliTempratureEnd { get; set; }
        public int FsliHeatingTimes { get; set; }
        public MATERIALSubHeadEntity FSubHeadEntity { get; set; }
        public SubHeadEntity1 FSubHeadEntity1 { get; set; }
        public SubHeadEntity2 FSubHeadEntity2 { get; set; }
        public SubHeadEntity3 FSubHeadEntity3 { get; set; }
        public SubHeadEntity4 FSubHeadEntity4 { get; set; }
        public SubHeadEntity5 FSubHeadEntity5 { get; set; }
        public SubHeadEntity6 FSubHeadEntity6 { get; set; }
        public SubHeadEntity7 FSubHeadEntity7 { get; set; }
        public List<EntityInvPty> FEntityInvPty { get; set; }
    }

    // 组织 ID 模型
    public class MATERIALOrgId
    {
        public string FNumber { get; set; }
    }

    // 子头实体模型 1
    public class MATERIALSubHeadEntity
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
        public string FErpClsID { get; set; }
        public string FFeatureItem { get; set; }
        public MATERIALOrgId FCategoryID { get; set; }
        public MATERIALOrgId FTaxType { get; set; }
        public MATERIALOrgId FTaxRateId { get; set; }
        public MATERIALOrgId FBaseUnitId { get; set; }
        public bool FIsPurchase { get; set; }
        public bool FIsInventory { get; set; }
        public bool FIsSubContract { get; set; }
        public bool FIsSale { get; set; }
        public bool FIsProduce { get; set; }
        public bool FIsAsset { get; set; }
        public double FGROSSWEIGHT { get; set; }
        public double FNETWEIGHT { get; set; }
        public MATERIALOrgId FWEIGHTUNITID { get; set; }
        public double FLENGTH { get; set; }
        public double FWIDTH { get; set; }
        public double FHEIGHT { get; set; }
        public double FVOLUME { get; set; }
        public MATERIALOrgId FVOLUMEUNITID { get; set; }
        public string FSuite { get; set; }
        public double FCostPriceRate { get; set; }
    }

    // 子头实体模型 2
    public class SubHeadEntity1
    {
        public MATERIALOrgId FStoreUnitID { get; set; }
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
        public MATERIALOrgId FCurrencyId { get; set; }
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

    // 子头实体模型 3
    public class SubHeadEntity2
    {
        public MATERIALOrgId FSaleUnitId { get; set; }
        public MATERIALOrgId FSalePriceUnitId { get; set; }
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

    // 子头实体模型 4
    public class SubHeadEntity3
    {
        public double FBaseMinSplitQty { get; set; }
        public MATERIALOrgId FPurchaseUnitId { get; set; }
        public MATERIALOrgId FPurchasePriceUnitId { get; set; }
        public MATERIALOrgId FPurchaseOrgId { get; set; }
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
        public MATERIALOrgId FPOBillTypeId { get; set; }
        public double FAgentPurPlusRate { get; set; }
        public int FPrintCount { get; set; }
        public double FMinPackCount { get; set; }
        public double FDailyOutQtySub { get; set; }
        public bool FIsEnableScheduleSub { get; set; }
    }

    // 子头实体模型 5
    public class SubHeadEntity4
    {
        public string FPlanMode { get; set; }
        public double FBaseVarLeadTimeLotSize { get; set; }
        public string FPlanningStrategy { get; set; }
        public MATERIALOrgId FMfgPolicyId { get; set; }
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

    // 子头实体模型 6
    public class SubHeadEntity5
    {
        public MATERIALOrgId FProduceUnitId { get; set; }
        public double FFinishReceiptOverRate { get; set; }
        public double FFinishReceiptShortRate { get; set; }
        public MATERIALOrgId FProduceBillType { get; set; }
        public MATERIALOrgId FOrgTrustBillType { get; set; }
        public bool FIsProductLine { get; set; }
        public bool FIsSNCarryToParent { get; set; }
        public MATERIALOrgId FBOMUnitId { get; set; }
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
        public MATERIALOrgId FMinIssueUnitId { get; set; }
        public string FStandHourUnitId { get; set; }
        public string FBackFlushType { get; set; }
        public double FFIXLOSS { get; set; }
        public bool FIsEnableSchedule { get; set; }
    }

    // 子头实体模型 7
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

    // 子头实体模型 8
    public class SubHeadEntity7
    {
        public MATERIALOrgId FSubconUnitId { get; set; }
        public MATERIALOrgId FSubconPriceUnitId { get; set; }
        public MATERIALOrgId FSubBillType { get; set; }
    }

    // 库存属性实体模型
    public class EntityInvPty
    {
        public MATERIALOrgId FInvPtyId { get; set; }
        public bool FIsEnable { get; set; }
        public bool FIsAffectPrice { get; set; }
        public bool FIsAffectPlan { get; set; }
        public bool FIsAffectCost { get; set; }
    }
}
