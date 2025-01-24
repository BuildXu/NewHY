using Kingdee.BOS.WebApi.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi_SY.Entity;
using WebApi_SY.Models;

namespace WebApi_SY.Controllers
{
    public class sli_sal_deliverynoticeController : ApiController
    {
        public sli_sal_deliverynoticeController()
        {
            
        }
        public IHttpActionResult GetTabledeliverynotice_view(string FbillNo=null)
        {
            var context = new YourDbContext();

            IQueryable<sli_sal_deliverynotice_view> query = context.Sli_sal_deliverynotice_view;
            if (!string.IsNullOrEmpty(FbillNo))
            {
                query = query.Where(q => q.Fbillno.Contains(FbillNo));
            }

            var result = query.Select(a => new
            {
                Fid = a.Fid,
                Fbillno = a.Fbillno,
                FcustNumber = a.FcustNumber,
                Fshortname = a.Fshortname,
                Faddress = a.Faddress ?? string.Empty,
                ForderNo = a.ForderNo ?? string.Empty,
                Fentryid = a.Fentryid,
                Fnumber = a.Fnumber,
                Fseq = a.Fseq,
                Fqty = a.Fqty,
                Fisvmibusiness = a.Fisvmibusiness,
                Fdeliverydate = a.Fdeliverydate


            });
            var response = new    // 定义 前端返回数据  总记录，总页，当前页 ，size,返回记录
            {
                code = 200,
                msg = "OK",
                data = new
                {
                    
                    data = result
                }


            };

            return Ok(response);
        }


        public IHttpActionResult GetTabledeliverynotice(int page = 1, int pageSize = 10)
        {
            var context = new YourDbContext();

            IQueryable<sli_sal_deliverynotice_view> query = context.Sli_sal_deliverynotice_view;

            var totalCount = query.Count(); //记录数
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize); // 页数
            var paginatedQuery = query.OrderByDescending(b => b.Fid).Skip((page - 1) * pageSize).Take(pageSize); //  某页记录
            var result = query.Select(a => new
            {
                Fid = a.Fid,
                Fbillno = a.Fbillno,
                FcustNumber = a.FcustNumber,
                Fshortname = a.Fshortname,
                Faddress = a.Faddress ?? string.Empty,
                ForderNo = a.ForderNo ?? string.Empty,
                Fentryid = a.Fentryid,
                Fnumber = a.Fnumber,
                Fseq = a.Fseq,
                Fqty = a.Fqty,
                Fisvmibusiness = a.Fisvmibusiness,
                Fdeliverydate = a.Fdeliverydate


            });
            var response = new    // 定义 前端返回数据  总记录，总页，当前页 ，size,返回记录
            {
                code = 200,
                msg = "OK",
                data = new
                {
                    totalCounts = totalCount,
                    totalPagess = totalPages,
                    currentPages = page,
                    pageSizes = pageSize,
                    data = result
                }


            };

            return Ok(response);
        }

        /// <summary>
        /// 调用金蝶销售订单保存接口，写入excel导入数据；根据导入的生成的表头ID读取数据
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
                    var FcustomerNumer = context.Sli_bd_customer_view.FirstOrDefault(p => p.FNAME == heard.FCustomerName); //根据客户名称查询客户代码
                    var entity = context.Sli_sale_orderImportentry.Where(p => p.fid == id);   //获取表体多行数据
                    var entityList = entity.ToList();
                    //var index = 0;
                    string json = "{\"NeedUpDateFields\":[],\"NeedReturnFields\":[\"FBillNo\"],\"IsDeleteEntry\":\"true\",\"SubSystemId\":\"\",\"IsVerifyBaseDataField\":\"false\",\"IsEntryBatchFill\":\"true\",\"ValidateFlag\":\"true\",\"NumberSearch\":\"true\",\"IsAutoAdjustField\":\"true\",\"InterationFlags\":\"\",\"IgnoreInterationFlag\":\"\",\"IsControlPrecision\":\"false\",\"ValidateRepeatJson\":\"false\",\"Model\":{\"FID\":0,\"FBillTypeID\":{\"FNUMBER\":\"XSCKD01_SYS\"},\"FDate\":\"2025-01-24 00:00:00\",\"FSaleOrgId\":{\"FNumber\":\"100\"},\"FCustomerID\":{\"FNumber\":\"CUST0001\"},\"FReceiverID\":{\"FNumber\":\"CUST0001\"},\"FStockOrgId\":{\"FNumber\":\"100\"},\"FSettleID\":{\"FNumber\":\"CUST0001\"},\"FPayerID\":{\"FNumber\":\"CUST0001\"},\"FOwnerTypeIdHead\":\"BD_OwnerOrg\",\"FCDateOffsetValue\":0,\"FIsTotalServiceOrCost\":false,\"SubHeadEntity\":{\"FSettleCurrID\":{\"FNumber\":\"PRE001\"},\"FSettleOrgID\":{\"FNumber\":\"100\"},\"FIsIncludedTax\":true,\"FLocalCurrID\":{\"FNumber\":\"PRE001\"},\"FExchangeTypeID\":{\"FNumber\":\"HLTX01_SYS\"},\"FExchangeRate\":1.0,\"FIsPriceExcludeTax\":true,\"FAllDisCount\":0.0},\"FEntity\":[{\"FRowType\":\"Standard\",\"FMaterialID\":{\"FNumber\":\"111\"},\"FUnitID\":{\"FNumber\":\"kg\"},\"FInventoryQty\":0.0,\"FRealQty\":11.0,\"FDisPriceQty\":0.0,\"FPrice\":0.0,\"FTaxPrice\":0.0,\"FIsFree\":false,\"FOwnerTypeID\":\"BD_OwnerOrg\",\"FOwnerID\":{\"FNumber\":\"100\"},\"FLot\":{\"FNumber\":\"2222\"},\"FEntryTaxRate\":13.00,\"FAuxUnitQty\":11.0,\"FExtAuxUnitId\":{\"FNumber\":\"Pcs\"},\"FExtAuxUnitQty\":11.0,\"FStockID\":{\"FNumber\":\"01\"},\"FStockStatusID\":{\"FNumber\":\"KCZT01_SYS\"},\"FSrcType\":\"\",\"FSrcBillNo\":\"\",\"FDiscountRate\":0.0,\"FPriceDiscount\":0.0,\"FActQty\":0.0,\"FSalUnitID\":{\"FNumber\":\"kg\"},\"FSALUNITQTY\":11.0,\"FSALBASEQTY\":11.0,\"FPRICEBASEQTY\":11.0,\"FOUTCONTROL\":false,\"FRepairQty\":0.0,\"FIsOverLegalOrg\":false,\"FARNOTJOINQTY\":11.0,\"FQmEntryID\":0,\"FConvertEntryID\":0,\"FSOEntryId\":0,\"FBeforeDisPriceQty\":0.0,\"FSignQty\":0.0,\"FCheckDelivery\":false,\"FAllAmountExceptDisCount\":0.0,\"FSettleBySon\":false,\"FBOMEntryId\":0,\"FMaterialID_Sal\":{\"FNUMBER\":\"111\"},\"FInStockEntryId\":0,\"FReceiveEntryId\":0,\"FIsReplaceOut\":false,\"FVmiBusinessStatus\":false}]}}";
                    RootObjectdelivery rootObject = JsonConvert.DeserializeObject<RootObjectdelivery>(json);
                    rootObject.Model.FEntity.Clear();
                    
                    rootObject.Model.FDate = DateTime.Now;
                    rootObject.Model.FCustomerID.FNumber = FcustomerNumer.FNUMBER;
                    rootObject.Model.FReceiverID.FNumber = FcustomerNumer.FNUMBER;
                    rootObject.Model.FSettleID.FNumber = FcustomerNumer.FNUMBER;
                    rootObject.Model.FPayerID.FNumber = FcustomerNumer.FNUMBER;

                    foreach (var entitydata in entityList)
                    {
                        FEntity newEntry = new FEntity();


                        newEntry.FRowType = "Standard";
                        newEntry.FMaterialID = new Numberdelivery { FNumber = entitydata.fmaterialNumber };
                        newEntry.FUnitID = new Numberdelivery { FNumber = "kg" };   //考虑是否写死，需要更新
                        newEntry.FInventoryQty = 0.0;
                        newEntry.FRealQty = entitydata.fqty;
                        newEntry.FDisPriceQty = 0.0;
                        newEntry.FPrice = 0.0;
                        newEntry.FTaxPrice = 0.0;
                        newEntry.FIsFree = false;
                        newEntry.FOwnerTypeID = "BD_OwnerOrg";
                        newEntry.FOwnerID = new Numberdelivery { FNumber = "100" };
                        newEntry.FLot = new Numberdelivery { FNumber = entitydata.fname };   //需要调整为批号
                        newEntry.FEntryTaxRate = 13.00;
                        newEntry.FAuxUnitQty = entitydata.fqty;   //需要更新取数逻辑
                        newEntry.FExtAuxUnitId = new Numberdelivery { FNumber = "kg" };//考虑是否写死，需要更新
                        newEntry.FExtAuxUnitQty = entitydata.fqty;   //需要更新取数逻辑
                        newEntry.FStockID = new Numberdelivery { FNumber = "01" };      //更新仓库id
                        newEntry.FStockStatusID = new Numberdelivery { FNumber = "KCZT01_SYS" }; 
                        newEntry.FSrcType = "";
                        newEntry.FSrcBillNo = "";
                        newEntry.FDiscountRate = 0.0;
                        newEntry.FPriceDiscount = 0.0;
                        newEntry.FActQty = 0.0;
                        newEntry.FSalUnitID = new Numberdelivery { FNumber = "kg" };//考虑是否写死，需要更新
                        newEntry.FSALUNITQTY = entitydata.fqty;
                        newEntry.FSALBASEQTY = entitydata.fqty;
                        newEntry.FPRICEBASEQTY = entitydata.fqty;
                        newEntry.FOUTCONTROL = false;
                        newEntry.FRepairQty = 0.0;
                        newEntry.FIsOverLegalOrg = false;
                        newEntry.FARNOTJOINQTY = entitydata.fqty;
                        newEntry.FQmEntryID = 0;
                        newEntry.FConvertEntryID = 0;
                        newEntry.FSOEntryId = 0;
                        newEntry.FBeforeDisPriceQty = 0.0;
                        newEntry.FSignQty = 0.0;
                        newEntry.FCheckDelivery = false;
                        newEntry.FAllAmountExceptDisCount = 0.0;
                        newEntry.FSettleBySon = false;
                        newEntry.FBOMEntryId = 0;
                        newEntry.FMaterialID_Sal = new Numberdelivery { FNumber = entitydata.fmaterialNumber };
                        newEntry.FInStockEntryId = 0;
                        newEntry.FReceiveEntryId = 0;
                        newEntry.FIsReplaceOut = false;
                        newEntry.FVmiBusinessStatus = false;
                        

                        FEntity_Link newEntry_Link = new FEntity_Link();
                        newEntry_Link.FEntity_Link_FRuleId = "DeliveryNotice-OutStock";//转换规则
                        newEntry_Link.FEntity_Link_FSTableName = "T_SAL_DELIVERYNOTICEENTRY";//源单表
                        newEntry_Link.FEntity_Link_FSBillId = 1;// 源单内码
                        newEntry_Link.FEntity_Link_FSId = 1;//源单分录内码
                        newEntry_Link.FEntity_Link_FBaseUnitQtyOld = 1;//原始携带量
                        newEntry_Link.FEntity_Link_FBaseUnitQty = 1;//	 修改携带量
                        newEntry_Link.FEntity_Link_FSALBASEQTYOld = 1;//	 原始携带量
                        newEntry_Link.FEntity_Link_FSALBASEQTY = 1;//	 修改携带量
                        newEntry.FEntity_Link.Add(newEntry_Link);

                        rootObject.Model.FEntity.Add(newEntry);
                        // 将新创建的实例添加到FSaleOrderEntry列表中

                    }
                    string newJson = JsonConvert.SerializeObject(rootObject);
                    System.Diagnostics.Debug.WriteLine(newJson);
                    //sJson = "{\"CreateOrgId\":0,\"Numbers\":[\"" + FPrdModel + "\"]}";
                    //string jsonData = "{\"NeedUpDateFields\": [],\"NeedReturnFields\": [],\"IsDeleteEntry\": \"true\",\"SubSystemId\": \"\",\"IsVerifyBaseDataField\": \"False\",\"IsEntryBatchFill\": \"true\",\"ValidateFlag\": \"true\",\"NumberSearch\": \"true\",\"IsAutoAdjustField\": \"False\",\"InterationFlags\": \"\",\"IgnoreInterationFlag\": \"\",\"IsControlPrecision\": \"False\",\"Model\": {\"FBillTypeID\": {\"FNUMBER\": \"XSDD01_SYS\"},\"FDate\": \"2022-04-27 00:00:00\",\"FSaleOrgId\": {\"FNumber\": \"100\"},\"FCustId\": {\"FNumber\": \"SCMKH100001\"},\"FReceiveId\": {\"FNumber\": \"SCMKH100001\"},\"FSaleDeptId\": {\"FNumber\": \"SCMBM000001\"},\"FSalerId\": {\"FNumber\": \"SCMYG000001_SCMGW000001_1\"},\"FSettleId\": {\"FNumber\": \"SCMKH100001\"},\"FChargeId\": {\"FNumber\": \"SCMKH100001\"},\"FSaleOrderFinance\": {\"FSettleCurrId\": {\"FNumber\": \"PRE001\"},\"FIsPriceExcludeTax\": 'true',\"FIsIncludedTax\": 'true',\"FExchangeTypeId\": {\"FNumber\": \"HLTX01_SYS\"}},\"FSaleOrderEntry\": [{\"FRowType\": \"Standard\",\"FMaterialId\": {\"FNumber\": \"SCMWL100002\"},\"FUnitID\": {\"FNumber\": \"Pcs\"},\"FQty\": 10,\"FPriceUnitId\": {\"FNumber\": \"Pcs\"},\"FPrice\": 8.849558,\"FTaxPrice\": 10,\"FEntryTaxRate\": 13,\"FDeliveryDate\": \"2022-04-27 15:15:54\",\"FStockOrgId\": {\"FNumber\": \"100\"},\"FSettleOrgIds\": {\"FNumber\": \"100\"},\"FSupplyOrgId\": {\"FNumber\": \"100\"},\"FOwnerTypeId\": \"BD_OwnerOrg\",\"FOwnerId\": {\"FNumber\": \"100\"},\"FReserveType\": \"1\",\"FPriceBaseQty\": 10,\"FStockUnitID\": {\"FNumber\": \"Pcs\"},\"FStockQty\": 10,\"FStockBaseQty\": 10,\"FOUTLMTUNIT\": \"SAL\",\"FOutLmtUnitID\": {\"FNumber\": \"Pcs\"},\"FAllAmountExceptDisCount\": 100,\"FOrderEntryPlan\": [{\"FPlanDate\": \"2022-04-27 15:15:54\",\"FPlanQty\": 10}]}],\"FSaleOrderPlan\": [{\"FRecAdvanceRate\": 100,\"FRecAdvanceAmount\": 100}],\"FBillNo\":" + "\"" + Number + "\"" + ",}}";
                    var result = client.Execute<string>("Kingdee.BOS.WebApi.ServicesStub.DynamicFormService.Save",
                    new object[] { "SAL_OUTSTOCK", newJson });

                    ResultData resultdata = JsonConvert.DeserializeObject<ResultData>(result);
                    if (resultdata.Result.ResponseStatus.IsSuccess)
                    {
                        var FbillNoList = "";
                        foreach (var FbillNo in resultdata.Result.NeedReturnData)
                        {

                            FbillNoList = FbillNoList + FbillNo.FBillNo;
                        }
                        var datas = new
                        {
                            code = 200,
                            msg = "ok",
                            date = "同步成功！销售单号：" + FbillNoList + ""
                        };
                        heard.Flag = 1;
                        heard.FParameter = newJson;
                        heard.FReason = FbillNoList;
                        context.SaveChanges();
                        return Ok(datas);

                    }
                    else
                    {
                        var ErrorList = "";
                        foreach (var Error in resultdata.Result.ResponseStatus.Errors)
                        {

                            ErrorList = ErrorList + Error.Message;
                        }
                        var datas = new
                        {
                            code = 400,
                            msg = "err,同步异常！" + ErrorList + "",
                            date = ""
                        };
                        heard.Flag = 2;
                        heard.FParameter = newJson;
                        heard.FReason = ErrorList;
                        context.SaveChanges();
                        return Ok(datas);
                    }

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