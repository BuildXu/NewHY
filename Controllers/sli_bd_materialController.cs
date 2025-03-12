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
                string dbId = "67b289c33bafdd"; //账套ID   67b289c33bafdd
                bool bLogin = client.Login(dbId, "Administrator", "kingdee123*", 2052);
                if (bLogin)
                {

                    //var heard = context.Sli_sale_orderImport.FirstOrDefault(p => p.FID == id);   //获取表头单行数据
                    //var FcustomerNumer = context.Sli_bd_customer_view.FirstOrDefault(p => p.FNAME == heard.FCustomerName); //根据客户名称查询客户代码
                    //var entity = context.Sli_sale_orderImportentry.Where(p => p.Fid == id && p.FmaterialNumber=="").GroupBy(o => new { o.Fname,o.Fdescription, o.FsliDrawingNo, o.FsliMetal })
                    //                                              .Select(g => new sli_sale_orderImportentry
                    //                                              {
                    //                                                  Fdescription = g.Key.Fdescription,
                    //                                                  FsliDrawingNo = g.Key.FsliDrawingNo,
                    //                                                  FsliMetal = g.Key.FsliMetal,
                    //                                                  Fname = g.Key.Fname,
                    //                                                  Id= g.Max(x => x.Id)
                    //                                              });  //获取表体多行数据
                    var entity = context.Sli_sale_orderImportentry.Where(p => p.Fid == id);
                    var entityList = entity.ToList();
                    //if (entityList.Count == 0)
                    //{
                    //    var datass = new
                    //    {
                    //        code = 200,
                    //        msg = "系统中未有空行，已有物料不需要新增",
                    //        date = ""
                    //    };
                    //    return Ok(datass);
                    //}
                    //var index = 0;
                    string json = "{\"NeedUpDateFields\":[],\"NeedReturnFields\":[],\"IsDeleteEntry\":\"true\",\"SubSystemId\":\"\",\"IsVerifyBaseDataField\":\"false\",\"IsEntryBatchFill\":\"true\",\"ValidateFlag\":\"true\",\"NumberSearch\":\"true\",\"IsAutoAdjustField\":\"true\",\"InterationFlags\":\"\",\"IgnoreInterationFlag\":\"\",\"IsControlPrecision\":\"false\",\"ValidateRepeatJson\":\"false\",\"Model\":{\"FID\":0,\"FNumber\":\"12345\",\"FName\":\"1234\",\"FCreateOrgId\":{\"FNumber\":\"100\"},\"FUseOrgId\":{\"FNumber\":\"100\"}}}";
                    sli_bd_material rootObject = JsonConvert.DeserializeObject<sli_bd_material>(json);


                    foreach (var entitydata in entityList)
                    {
                        //SaleOrderEntry newEntry = new SaleOrderEntry();
                        rootObject.IsAutoSubmitAndAudit = "True";
                        rootObject.Model.FName=entitydata.Fname;
                        rootObject.Model.FNumber =  entitydata.FmaterialNumber;
                        rootObject.Model.FSpecification = entitydata.Fdescription;
                        rootObject.Model.FsliDrawingNo = entitydata.FsliDrawingNo;
                        rootObject.Model.FsliMetal = new MaterialOrgId { FNumber = entitydata.FsliMetal };

                        string newJson = JsonConvert.SerializeObject(rootObject);
                        System.Diagnostics.Debug.WriteLine(newJson);
                        //sJson = "{\"CreateOrgId\":0,\"Numbers\":[\"" + FPrdModel + "\"]}";
                        //string jsonData = "{\"NeedUpDateFields\": [],\"NeedReturnFields\": [],\"IsDeleteEntry\": \"true\",\"SubSystemId\": \"\",\"IsVerifyBaseDataField\": \"False\",\"IsEntryBatchFill\": \"true\",\"ValidateFlag\": \"true\",\"NumberSearch\": \"true\",\"IsAutoAdjustField\": \"False\",\"InterationFlags\": \"\",\"IgnoreInterationFlag\": \"\",\"IsControlPrecision\": \"False\",\"Model\": {\"FBillTypeID\": {\"FNUMBER\": \"XSDD01_SYS\"},\"FDate\": \"2022-04-27 00:00:00\",\"FSaleOrgId\": {\"FNumber\": \"100\"},\"FCustId\": {\"FNumber\": \"SCMKH100001\"},\"FReceiveId\": {\"FNumber\": \"SCMKH100001\"},\"FSaleDeptId\": {\"FNumber\": \"SCMBM000001\"},\"FSalerId\": {\"FNumber\": \"SCMYG000001_SCMGW000001_1\"},\"FSettleId\": {\"FNumber\": \"SCMKH100001\"},\"FChargeId\": {\"FNumber\": \"SCMKH100001\"},\"FSaleOrderFinance\": {\"FSettleCurrId\": {\"FNumber\": \"PRE001\"},\"FIsPriceExcludeTax\": 'true',\"FIsIncludedTax\": 'true',\"FExchangeTypeId\": {\"FNumber\": \"HLTX01_SYS\"}},\"FSaleOrderEntry\": [{\"FRowType\": \"Standard\",\"FMaterialId\": {\"FNumber\": \"SCMWL100002\"},\"FUnitID\": {\"FNumber\": \"Pcs\"},\"FQty\": 10,\"FPriceUnitId\": {\"FNumber\": \"Pcs\"},\"FPrice\": 8.849558,\"FTaxPrice\": 10,\"FEntryTaxRate\": 13,\"FDeliveryDate\": \"2022-04-27 15:15:54\",\"FStockOrgId\": {\"FNumber\": \"100\"},\"FSettleOrgIds\": {\"FNumber\": \"100\"},\"FSupplyOrgId\": {\"FNumber\": \"100\"},\"FOwnerTypeId\": \"BD_OwnerOrg\",\"FOwnerId\": {\"FNumber\": \"100\"},\"FReserveType\": \"1\",\"FPriceBaseQty\": 10,\"FStockUnitID\": {\"FNumber\": \"Pcs\"},\"FStockQty\": 10,\"FStockBaseQty\": 10,\"FOUTLMTUNIT\": \"SAL\",\"FOutLmtUnitID\": {\"FNumber\": \"Pcs\"},\"FAllAmountExceptDisCount\": 100,\"FOrderEntryPlan\": [{\"FPlanDate\": \"2022-04-27 15:15:54\",\"FPlanQty\": 10}]}],\"FSaleOrderPlan\": [{\"FRecAdvanceRate\": 100,\"FRecAdvanceAmount\": 100}],\"FBillNo\":" + "\"" + Number + "\"" + ",}}";
                        var result = client.Execute<string>("Kingdee.BOS.WebApi.ServicesStub.DynamicFormService.Save",
                        new object[] { "BD_MATERIAL", newJson });

                        ResultData resultdata = JsonConvert.DeserializeObject<ResultData>(result);
                        if (resultdata.Result.ResponseStatus.IsSuccess)
                        {
                            var products = context.Sli_sale_orderImportentry.Where(  p =>p.FsliMetal== entitydata.FsliMetal && p.FsliDrawingNo == entitydata.FsliDrawingNo && p.Fdescription == entitydata.Fdescription && p.Fid == id).ToList();

                            //var entity1 = context.Sli_sale_orderImportentry.Where(p => p.fid == id).ToList();

                            // 更新字段
                            foreach (var product in products)
                            {
                                product.fparameter = newJson;
                                product.FmaterialNumber = resultdata.Result.Number;
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