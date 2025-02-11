using Kingdee.BOS.WebApi.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi_SY.Entity;
using WebApi_SY.Models;

namespace WebApi_SY.Controllers
{
    public class sli_bd_materialController : ApiController
    {
        public sli_bd_materialController()
        {
            // _context = context;

        }
        /// <summary>
        /// 调用金蝶物料保存接口，新增物料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IHttpActionResult DateSync(int id)
        {
            try
            {
                var context = new YourDbContext();
                //Assert.IsTrue((bool)isSuccess, resultJson);
                ApiClient client = new ApiClient("http://19vs7gv47690.vicp.fun/K3cloud/");
                string dbId = "6708e954644c2a"; //账套ID
                bool bLogin = client.Login(dbId, "Administrator", "kingdee123*", 2052);
                if (bLogin)
                {

                    var heard = context.Sli_sale_orderImport.FirstOrDefault(p => p.FID == id);   //获取表头单行数据
                    //var FcustomerNumer = context.Sli_bd_customer_view.FirstOrDefault(p => p.FNAME == heard.FCustomerName); //根据客户名称查询客户代码
                    var entity = context.Sli_sale_orderImportentry.Where(p => p.fid == id && p.fmaterialNumber=="").GroupBy(o => new { o.fname,o.fdescription, o.fsliDrawingNo, o.fsliMetal })
                                                                  .Select(g => new sli_sale_orderImportentry
                                                                  {
                                                                      fdescription = g.Key.fdescription,
                                                                      fsliDrawingNo = g.Key.fsliDrawingNo,
                                                                      fsliMetal = g.Key.fsliMetal,
                                                                      fname = g.Key.fname,
                                                                      id= g.Max(x => x.id)
                                                                  });  //获取表体多行数据
                    var entityList = entity.ToList();
                    if (entityList.Count == 0)
                    {
                        var datass = new
                        {
                            code = 200,
                            msg = "系统中未有空行，已有物料不需要新增",
                            date = ""
                        };
                        return Ok(datass);
                    }
                    //var index = 0;
                    string json = "{\"NeedUpDateFields\":[],\"NeedReturnFields\":[],\"IsDeleteEntry\":\"true\",\"SubSystemId\":\"\",\"IsVerifyBaseDataField\":\"false\",\"IsEntryBatchFill\":\"true\",\"ValidateFlag\":\"true\",\"NumberSearch\":\"true\",\"IsAutoAdjustField\":\"true\",\"InterationFlags\":\"\",\"IgnoreInterationFlag\":\"\",\"IsControlPrecision\":\"false\",\"ValidateRepeatJson\":\"false\",\"Model\":{\"FMATERIALID\":0,\"FCreateOrgId\":{\"FNumber\":\"100\"},\"FUseOrgId\":{\"FNumber\":\"100\"},\"FNumber\":\"SY-CP-01-0020\",\"FName\":\"TESE\",\"FSpecification\":\"TESS\",\"FMaterialGroup\":{\"FNumber\":\"SY-CP-01\"},\"FDSMatchByLot\":false,\"FImgStorageType\":\"A\",\"FIsSalseByNet\":false,\"FIsHandleReserve\":true,\"FsliOuterDiameter\":0.0,\"FsliInnerDiameter\":0.0,\"FsliHight\":0.0,\"FsliAllowanceOD\":0.0,\"FsliAllowanceID\":0.0,\"FsliAllownceH\":0.0,\"FsliWeightMaterial\":0.0,\"FsliWeightForging\":0.0,\"FsliWeightGoods\":0.0,\"FsliDrawingNo\":\"1222\",\"FsliMetal\":{\"FNUMBER\":\"15CrMoA\"},\"FsliTemperatureBegin\":0,\"FsliTempratureEnd\":0,\"FsliHeatingTimes\":0,\"FSubHeadEntity\":{\"FIsControlSal\":false,\"FLowerPercent\":0.0,\"FUpPercent\":0.0,\"FCalculateBase\":\"0\",\"FMaxSalPrice_CMK\":0.0,\"FMinSalPrice_CMK\":0.0,\"FIsAutoRemove\":false,\"FIsMailVirtual\":false,\"FIsFreeSend\":\"0\",\"FTimeUnit\":\"H\",\"FRentFreeDura\":0.0,\"FPricingStep\":0.0,\"FMinRentDura\":0.0,\"FPriceType\":\"0\",\"FRentBeginPrice\":0.0,\"FRentStepPrice\":0.0,\"FDepositAmount\":0.0,\"FLogisticsCount\":0.0,\"FRequestMinPackQty\":0.0,\"FMinRequestQty\":0.0,\"FIsPrinttAg\":false,\"FIsAccessory\":false,\"FUploadSkuImage\":false},\"SubHeadEntity\":{\"FErpClsID\":\"1\",\"FFeatureItem\":\"1\",\"FCategoryID\":{\"FNumber\":\"CHLB05_SYS\"},\"FTaxType\":{\"FNumber\":\"WLDSFL01_SYS\"},\"FTaxRateId\":{\"FNUMBER\":\"SL02_SYS\"},\"FBaseUnitId\":{\"FNumber\":\"Pcs\"},\"FIsPurchase\":true,\"FIsInventory\":true,\"FIsSubContract\":false,\"FIsSale\":true,\"FIsProduce\":false,\"FIsAsset\":false,\"FGROSSWEIGHT\":0.0,\"FNETWEIGHT\":0.0,\"FWEIGHTUNITID\":{\"FNUMBER\":\"kg\"},\"FLENGTH\":0.0,\"FWIDTH\":0.0,\"FHEIGHT\":0.0,\"FVOLUME\":0.0,\"FVOLUMEUNITID\":{\"FNUMBER\":\"m\"},\"FSuite\":\"0\",\"FCostPriceRate\":0.0},\"SubHeadEntity1\":{\"FStoreUnitID\":{\"FNumber\":\"Pcs\"},\"FUnitConvertDir\":\"1\",\"FIsLockStock\":true,\"FIsCycleCounting\":false,\"FCountCycle\":\"1\",\"FCountDay\":1,\"FIsMustCounting\":false,\"FIsBatchManage\":false,\"FIsKFPeriod\":false,\"FIsExpParToFlot\":false,\"FExpPeriod\":0,\"FOnlineLife\":0,\"FRefCost\":0.0,\"FCurrencyId\":{\"FNumber\":\"PRE001\"},\"FIsEnableMinStock\":false,\"FIsEnableMaxStock\":false,\"FIsEnableSafeStock\":false,\"FIsEnableReOrder\":false,\"FMinStock\":0.0,\"FSafeStock\":0.0,\"FReOrderGood\":0.0,\"FEconReOrderQty\":0.0,\"FMaxStock\":0.0,\"FIsSNManage\":false,\"FIsSNPRDTracy\":false,\"FSNManageType\":\"1\",\"FSNGenerateTime\":\"1\",\"FBoxStandardQty\":0.0},\"SubHeadEntity2\":{\"FSaleUnitId\":{\"FNumber\":\"Pcs\"},\"FSalePriceUnitId\":{\"FNumber\":\"Pcs\"},\"FOrderQty\":0.0,\"FMinQty\":0.0,\"FMaxQty\":100000.0,\"FOutStockLmtH\":0.0,\"FOutStockLmtL\":0.0,\"FAgentSalReduceRate\":0.0,\"FIsATPCheck\":false,\"FIsReturnPart\":false,\"FIsInvoice\":false,\"FIsReturn\":true,\"FAllowPublish\":false,\"FISAFTERSALE\":true,\"FISPRODUCTFILES\":true,\"FISWARRANTED\":false,\"FWARRANTY\":0,\"FWARRANTYUNITID\":\"D\",\"FOutLmtUnit\":\"SAL\",\"FIsTaxEnjoy\":false,\"FTaxDiscountsType\":\"0\",\"FUnValidateExpQty\":false},\"SubHeadEntity3\":{\"FBaseMinSplitQty\":0.0,\"FPurchaseUnitId\":{\"FNumber\":\"Pcs\"},\"FPurchasePriceUnitId\":{\"FNumber\":\"Pcs\"},\"FPurchaseOrgId\":{\"FNumber\":\"100\"},\"FIsQuota\":false,\"FQuotaType\":\"1\",\"FMinSplitQty\":0.0,\"FIsVmiBusiness\":false,\"FEnableSL\":false,\"FIsPR\":false,\"FIsReturnMaterial\":true,\"FIsSourceControl\":false,\"FReceiveMaxScale\":0.0,\"FReceiveMinScale\":0.0,\"FReceiveAdvanceDays\":0,\"FReceiveDelayDays\":0,\"FPOBillTypeId\":{\"FNUMBER\":\"CGSQD01_SYS\"},\"FAgentPurPlusRate\":0.0,\"FPrintCount\":1,\"FMinPackCount\":1.0,\"FDailyOutQtySub\":0.0,\"FIsEnableScheduleSub\":false},\"SubHeadEntity4\":{\"FPlanMode\":\"0\",\"FBaseVarLeadTimeLotSize\":0.0,\"FPlanningStrategy\":\"1\",\"FMfgPolicyId\":{\"FNumber\":\"ZZCL001_SYS\"},\"FOrderPolicy\":\"0\",\"FFixLeadTime\":0,\"FFixLeadTimeType\":\"1\",\"FVarLeadTime\":0,\"FVarLeadTimeType\":\"1\",\"FCheckLeadTime\":0,\"FCheckLeadTimeType\":\"1\",\"FOrderIntervalTimeType\":\"3\",\"FOrderIntervalTime\":0,\"FMaxPOQty\":100000.0,\"FMinPOQty\":0.0,\"FIncreaseQty\":0.0,\"FEOQ\":1.0,\"FVarLeadTimeLotSize\":1.0,\"FPlanIntervalsDays\":0,\"FPlanBatchSplitQty\":0.0,\"FRequestTimeZone\":0,\"FPlanTimeZone\":0,\"FIsMrpComReq\":false,\"FCanLeadDays\":0,\"FIsMrpComBill\":true,\"FLeadExtendDay\":0,\"FReserveType\":\"1\",\"FAllowPartAhead\":false,\"FPlanSafeStockQty\":0.0,\"FCanDelayDays\":999,\"FDelayExtendDay\":0,\"FAllowPartDelay\":true,\"FPlanOffsetTimeType\":\"1\",\"FPlanOffsetTime\":0,\"FWriteOffQty\":1.0,\"FDailyOutQty\":0.0},\"SubHeadEntity5\":{\"FProduceUnitId\":{\"FNumber\":\"Pcs\"},\"FFinishReceiptOverRate\":0.0,\"FFinishReceiptShortRate\":0.0,\"FProduceBillType\":{\"FNUMBER\":\"SCDD03_SYS\"},\"FOrgTrustBillType\":{\"FNUMBER\":\"SCDD06_SYS\"},\"FIsProductLine\":false,\"FIsSNCarryToParent\":false,\"FBOMUnitId\":{\"FNumber\":\"Pcs\"},\"FConsumVolatility\":0.0,\"FLOSSPERCENT\":0.0,\"FIsMainPrd\":false,\"FIsCoby\":false,\"FIsECN\":false,\"FIssueType\":\"1\",\"FOverControlMode\":\"1\",\"FMinIssueQty\":1.0,\"FISMinIssueQty\":false,\"FIsKitting\":false,\"FIsCompleteSet\":false,\"FStdLaborPrePareTime\":0.0,\"FStdLaborProcessTime\":0.0,\"FStdMachinePrepareTime\":0.0,\"FStdMachineProcessTime\":0.0,\"FMinIssueUnitId\":{\"FNUMBER\":\"Pcs\"},\"FStandHourUnitId\":\"3600\",\"FBackFlushType\":\"1\",\"FFIXLOSS\":0.0,\"FIsEnableSchedule\":false},\"SubHeadEntity7\":{\"FSubconUnitId\":{\"FNumber\":\"Pcs\"},\"FSubconPriceUnitId\":{\"FNumber\":\"Pcs\"},\"FSubBillType\":{\"FNUMBER\":\"WWDD01_SYS\"}},\"SubHeadEntity6\":{\"FCheckIncoming\":false,\"FCheckProduct\":false,\"FCheckStock\":false,\"FCheckReturn\":false,\"FCheckDelivery\":false,\"FEnableCyclistQCSTK\":false,\"FStockCycle\":0,\"FEnableCyclistQCSTKEW\":false,\"FEWLeadDay\":0,\"FCheckEntrusted\":false,\"FCheckOther\":false,\"FIsFirstInspect\":false,\"FCheckReturnMtrl\":false,\"FCheckSubRtnMtrl\":false,\"FFirstQCControlType\":\"0\"},\"FEntityInvPty\":[{\"FInvPtyId\":{\"FNumber\":\"01\"},\"FIsEnable\":true,\"FIsAffectPrice\":false,\"FIsAffectPlan\":false,\"FIsAffectCost\":false},{\"FInvPtyId\":{\"FNumber\":\"02\"},\"FIsEnable\":true,\"FIsAffectPrice\":false,\"FIsAffectPlan\":false,\"FIsAffectCost\":false},{\"FInvPtyId\":{\"FNumber\":\"03\"},\"FIsEnable\":false,\"FIsAffectPrice\":false,\"FIsAffectPlan\":false,\"FIsAffectCost\":false},{\"FInvPtyId\":{\"FNumber\":\"04\"},\"FIsEnable\":false,\"FIsAffectPrice\":false,\"FIsAffectPlan\":false,\"FIsAffectCost\":false},{\"FInvPtyId\":{\"FNumber\":\"06\"},\"FIsEnable\":false,\"FIsAffectPrice\":false,\"FIsAffectPlan\":false,\"FIsAffectCost\":false}]}}";
                    sli_bd_material rootObject = JsonConvert.DeserializeObject<sli_bd_material>(json);


                    foreach (var entitydata in entityList)
                    {
                        //SaleOrderEntry newEntry = new SaleOrderEntry();
                        rootObject.Model.FName=entitydata.fname;
                        rootObject.Model.FNumber = "SY-CP-01-"+ entitydata.id;
                        rootObject.Model.FSpecification = entitydata.fdescription;
                        rootObject.Model.FsliDrawingNo = entitydata.fsliDrawingNo;
                        rootObject.Model.FsliMetal = new MaterialOrgId { FNumber = entitydata.fsliMetal };

                        string newJson = JsonConvert.SerializeObject(rootObject);
                        System.Diagnostics.Debug.WriteLine(newJson);
                        //sJson = "{\"CreateOrgId\":0,\"Numbers\":[\"" + FPrdModel + "\"]}";
                        //string jsonData = "{\"NeedUpDateFields\": [],\"NeedReturnFields\": [],\"IsDeleteEntry\": \"true\",\"SubSystemId\": \"\",\"IsVerifyBaseDataField\": \"False\",\"IsEntryBatchFill\": \"true\",\"ValidateFlag\": \"true\",\"NumberSearch\": \"true\",\"IsAutoAdjustField\": \"False\",\"InterationFlags\": \"\",\"IgnoreInterationFlag\": \"\",\"IsControlPrecision\": \"False\",\"Model\": {\"FBillTypeID\": {\"FNUMBER\": \"XSDD01_SYS\"},\"FDate\": \"2022-04-27 00:00:00\",\"FSaleOrgId\": {\"FNumber\": \"100\"},\"FCustId\": {\"FNumber\": \"SCMKH100001\"},\"FReceiveId\": {\"FNumber\": \"SCMKH100001\"},\"FSaleDeptId\": {\"FNumber\": \"SCMBM000001\"},\"FSalerId\": {\"FNumber\": \"SCMYG000001_SCMGW000001_1\"},\"FSettleId\": {\"FNumber\": \"SCMKH100001\"},\"FChargeId\": {\"FNumber\": \"SCMKH100001\"},\"FSaleOrderFinance\": {\"FSettleCurrId\": {\"FNumber\": \"PRE001\"},\"FIsPriceExcludeTax\": 'true',\"FIsIncludedTax\": 'true',\"FExchangeTypeId\": {\"FNumber\": \"HLTX01_SYS\"}},\"FSaleOrderEntry\": [{\"FRowType\": \"Standard\",\"FMaterialId\": {\"FNumber\": \"SCMWL100002\"},\"FUnitID\": {\"FNumber\": \"Pcs\"},\"FQty\": 10,\"FPriceUnitId\": {\"FNumber\": \"Pcs\"},\"FPrice\": 8.849558,\"FTaxPrice\": 10,\"FEntryTaxRate\": 13,\"FDeliveryDate\": \"2022-04-27 15:15:54\",\"FStockOrgId\": {\"FNumber\": \"100\"},\"FSettleOrgIds\": {\"FNumber\": \"100\"},\"FSupplyOrgId\": {\"FNumber\": \"100\"},\"FOwnerTypeId\": \"BD_OwnerOrg\",\"FOwnerId\": {\"FNumber\": \"100\"},\"FReserveType\": \"1\",\"FPriceBaseQty\": 10,\"FStockUnitID\": {\"FNumber\": \"Pcs\"},\"FStockQty\": 10,\"FStockBaseQty\": 10,\"FOUTLMTUNIT\": \"SAL\",\"FOutLmtUnitID\": {\"FNumber\": \"Pcs\"},\"FAllAmountExceptDisCount\": 100,\"FOrderEntryPlan\": [{\"FPlanDate\": \"2022-04-27 15:15:54\",\"FPlanQty\": 10}]}],\"FSaleOrderPlan\": [{\"FRecAdvanceRate\": 100,\"FRecAdvanceAmount\": 100}],\"FBillNo\":" + "\"" + Number + "\"" + ",}}";
                        var result = client.Execute<string>("Kingdee.BOS.WebApi.ServicesStub.DynamicFormService.Save",
                        new object[] { "BD_MATERIAL", newJson });

                        ResultData resultdata = JsonConvert.DeserializeObject<ResultData>(result);
                        if (resultdata.Result.ResponseStatus.IsSuccess)
                        {
                            var products = context.Sli_sale_orderImportentry.Where(  p =>p.fsliMetal== entitydata.fsliMetal && p.fsliDrawingNo == entitydata.fsliDrawingNo && p.fdescription == entitydata.fdescription && p.id == entitydata.id).ToList();

                            //var entity1 = context.Sli_sale_orderImportentry.Where(p => p.fid == id).ToList();

                            // 更新字段
                            foreach (var product in products)
                            {
                                product.fparameter = newJson;
                                product.fmaterialNumber = resultdata.Result.Number;
                            }
                            context.SaveChanges();
                            

                        }
                        else
                        {
                            //var ErrorList = "";
                            //foreach (var Error in resultdata.Result.ResponseStatus.Errors)
                            //{

                            //    ErrorList = ErrorList + Error.Message;
                            //}
                            //var datas = new
                            //{
                            //    code = 400,
                            //    msg = "err,同步异常！" + ErrorList + "",
                            //    date = ""
                            //};
                            //heard.Flag = 2;
                            //heard.FParameter = newJson;
                            //heard.FReason = ErrorList;
                            //context.SaveChanges();
                            //return Ok(datas);
                        }

                    }


                    var data = new
                    {
                        code = 200,
                        msg = "成功",
                        date = "新增成功"
                    };
                    return Ok(data);

                }
                else
                {
                    return Ok("登录失败！");
                }

            }
            catch (Exception ex)
            {
                var datas = new
                {
                    code = 400,
                    msg = "失败",
                    date = ex.ToString()
                };
                return Ok(datas);
            }

        }

    }
}