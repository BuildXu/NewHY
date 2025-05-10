using Kingdee.BOS.WebApi.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi_SY.Entity;
using WebApi_SY.Models;

namespace WebApi_SY.Controllers
{/// <summary>
/// 生产领料单
/// </summary>
    public class sli_prd_pickmtrlController : ApiController
    {
        public sli_prd_pickmtrlController()
        {
            // _context = context;

        }
        /// <summary>
        /// 采购入库单新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<object> Insert([Microsoft.AspNetCore.Mvc.FromBody] sli_prd_pickmtrl model)
        {
            try
            {
                var context = new YourDbContext();

                var header = new sli_prd_pickmtrl
                {
                    Fbillno = model.Fbillno,
                    Fdate = model.Fdate,
                    //Fsppliername = model.Fsppliername,
                    Fdocumentstatus = model.Fdocumentstatus,
                    Fclosestatus = model.Fclosestatus,
                    FBiller = model.FBiller,
                    FDepId = model.FDepId,
                    Flag = 0,
                    sli_prd_pickmtrlentry = model.sli_prd_pickmtrlentry.Select(d => new sli_prd_pickmtrlentry
                    {
                        Fid = model.Fid,
                        Fnumber = d.Fnumber,
                        Fname = d.Fname,
                        Fbatchno = d.Fbatchno,
                        Unit = d.Unit,
                        Fqty = d.Fqty,
                        FSecQty = d.FSecQty,
                        FStockNumber = d.FStockNumber
                    }).ToList()
                };



                context.Sli_prd_pickmtrl.Add(header);
                await context.SaveChangesAsync();
                var dataNull = new
                {
                    code = 200,
                    msg = "Success",
                    modelid = header.Fid,
                    Date = header.Fid.ToString() + "保存成功"

                };
                return dataNull;
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex.ToString());
            }
        }

        /// <summary>
        /// 采购入库单删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<object> Delete(List<int> id)
        {
            try
            {
                var context = new YourDbContext();
                var headersToDelete = context.Sli_prd_pickmtrl.Where(h => id.Contains(h.Fid)).ToList();
                if (headersToDelete == null)
                {
                    var dataNull = new
                    {
                        code = 200,
                        msg = "ok",
                        orderId = id.ToString(),
                        date = id.ToString() + "不存在"
                    };
                    //string json = JsonConvert.SerializeObject(data);
                    return dataNull;
                }
                context.Sli_prd_pickmtrl.RemoveRange(headersToDelete);

                foreach (var DeleteID in id)
                {
                    var Sli_prd_pickmtrlentry = context.Sli_prd_pickmtrlentry.Where(b => b.Fid == DeleteID);
                    context.Sli_prd_pickmtrlentry.RemoveRange(Sli_prd_pickmtrlentry);
                }
                await context.SaveChangesAsync();
                var data = new
                {
                    code = 200,
                    msg = "Success",
                    orderId = id.ToString(),
                    date = id.ToString() + "删除成功"
                };
                return data;
            }
            catch (Exception ex)
            {
                var data = new
                {
                    code = 400,
                    msg = "失败",
                    orderId = id.ToString(),
                    date = ex.ToString()
                };
                return data;
            }
        }

        /// <summary>
        /// 采购入库单修改
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<object> Update(sli_prd_pickmtrl bill)
        {
            try
            {
                var context = new YourDbContext();
                var entity = await context.Sli_prd_pickmtrl.FindAsync(bill.Fid);
                if (entity == null)
                {
                    var dataNull = new
                    {
                        code = 400,
                        msg = "ok",
                        date = "修改记录不存在"
                    };
                    return dataNull;
                }
                else
                {
                    var Sli_prd_pickmtrl = context.Sli_prd_pickmtrl.FirstOrDefault(p => p.Fid == bill.Fid);
                    var Sli_prd_pickmtrlentry = context.Sli_prd_pickmtrlentry.Where(p => p.Fid == bill.Fid).ToList();

                    Sli_prd_pickmtrl.Fbillno = bill.Fbillno;
                    Sli_prd_pickmtrl.Fdate = bill.Fdate;
                    //Sli_stk_instock.Fsppliername = bill.Fsppliername;
                    Sli_prd_pickmtrl.Fdocumentstatus = bill.Fdocumentstatus;
                    Sli_prd_pickmtrl.Fclosestatus = bill.Fclosestatus;
                    Sli_prd_pickmtrl.FBiller = bill.FBiller;
                    Sli_prd_pickmtrl.FDepId = bill.FDepId;


                    context.Sli_prd_pickmtrlentry.RemoveRange(Sli_prd_pickmtrlentry);

                    foreach (var d in bill.sli_prd_pickmtrlentry)
                    {
                        var entry = new sli_prd_pickmtrlentry
                        {
                            Fid = bill.Fid,
                            Fnumber = d.Fnumber,
                            Fname = d.Fname,
                            Unit = d.Unit,
                            Fbatchno = d.Fbatchno,
                            Fqty = d.Fqty,
                            FSecQty = d.FSecQty,
                            FStockNumber = d.FStockNumber
                        };
                        context.Sli_prd_pickmtrlentry.Add(entry);
                    }
                    await context.SaveChangesAsync();
                    var datas = new
                    {
                        code = 200,
                        msg = "ok",
                        date = bill.Fid + "更新成功！"
                    };
                    ApiClient client = new ApiClient("http://36.151.103.130:9000/k3cloud/"); //接口地址

                    string dbId = "67b289c33bafdd"; //账套ID
                    bool bLogin = client.Login(dbId, "Administrator", "kingdee123*", 2052);
                    if (bLogin)
                    {
                        var UnAuditsJson = "{\"CreateOrgId\":0,\"Numbers\":[\"" + bill.Fbillno + "\"]}";
                        var UnAuditresult = client.Execute<string>("Kingdee.BOS.WebApi.ServicesStub.DynamicFormService.UnAudit",
                        new object[] { "SP_PickMtrl", UnAuditsJson });

                        var Deleteresult = client.Execute<string>("Kingdee.BOS.WebApi.ServicesStub.DynamicFormService.Delete",
                        new object[] { "SP_PickMtrl", UnAuditsJson });

                    }
                    return Ok(datas);

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

        /// <summary>
        /// 采购入库表头分页查询
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetHeaderPickmtrl(int page = 1, int pageSize = 10)
        {
            var context = new YourDbContext();
            var query = context.Sli_prd_pickmtrl_view;
            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedQuery = query.OrderByDescending(b => b.Fid).Skip((page - 1) * pageSize).Take(pageSize);
            var result = paginatedQuery.Select(a => new
            {
                id = a.Fid,
                Fbillno = a.Fbillno,
                Fdate = a.Fdate,
                //Fsppliername = a.Fsppliername,
                Fdocumentstatus = a.Fdocumentstatus,
                Fclosestatus = a.Fclosestatus,
                FBiller = a.FBiller,
                empname = a.empname,
                FDepId = a.FDepId,
                dept_name = a.dept_name,
                Flag = a.Flag  ,
                FParameter = a.FParameter ?? string.Empty,
                FReason = a.FReason ?? string.Empty
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
        /// 采购入库表体分页查询
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="FID"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetEntryPickmtrl(int? FID = null)
        {
            var context = new YourDbContext();
            var query = context.Sli_prd_pickmtrl_view.Include(a => a.sli_prd_pickmtrlentry_view);
            if (FID.HasValue)
            {
                query = query.Where(t => t.Fid == FID.Value);
            }
            var result = query.Select(a => new
            {
                Fid = a.Fid,
                Fbillno = a.Fbillno,
                Fdate = a.Fdate,
                //Fsppliername = a.Fsppliername,
                Fdocumentstatus = a.Fdocumentstatus,
                Fclosestatus = a.Fclosestatus,
                FBiller = a.FBiller,
                empname = a.empname ?? string.Empty,
                FDepId = a.FDepId,
                dept_name = a.dept_name ?? string.Empty,
                Flag = a.Flag,
                FParameter = a.FParameter,
                FReason = a.FReason,
                sli_prd_pickmtrlentry_view = a.sli_prd_pickmtrlentry_view.Select(b => new
                {
                    Fentryid = b.Fentryid,
                    Fid = b.Fid,
                    Fnumber = b.Fnumber,
                    Fname = b.Fname,
                    Fbatchno = b.Fbatchno,
                    Unit = b.Unit,
                    Fqty = b.Fqty,
                    FSecQty = b.FSecQty,
                    FStockNumber = b.FStockNumber
                })

            }).ToList();

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

        /// <summary>
        /// 同步采购入库单到金蝶星空简单生产领料单
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
                var client = new K3CloudApi("http://36.151.103.130:9000/k3cloud/"); //接口地址
                string dbId = "67b289c33bafdd"; //账套ID
                bool bLogin = client.Login(dbId, "Administrator", "kingdee123*", 2052);
                if (bLogin)
                {

                    var heard = context.Sli_prd_pickmtrl.FirstOrDefault(p => p.Fid == id);   //获取表头单行数据
                    var FdeptNumber = context.Sli_bd_department_view.FirstOrDefault(p => p.Fdeptid == heard.FDepId); //根据供应商名称查询客户代码
                    var entity = context.Sli_prd_pickmtrlentry.Where(p => p.Fid == id);   //获取表体多行数据
                    var entityList = entity.ToList();
                    //var index = 0;
                    string json = "{\"NeedUpDateFields\":[],\"NeedReturnFields\":[],\"IsDeleteEntry\":\"true\",\"SubSystemId\":\"\",\"IsVerifyBaseDataField\":\"false\",\"IsEntryBatchFill\":\"true\",\"ValidateFlag\":\"true\",\"NumberSearch\":\"true\",\"IsAutoAdjustField\":\"true\",\"InterationFlags\":\"\",\"IgnoreInterationFlag\":\"\",\"IsControlPrecision\":\"false\",\"ValidateRepeatJson\":\"false\",\"Model\":{\"FID\":0,\"FBillType\":{\"FNUMBER\":\"JDSCLL01_SYS\"},\"FDate\":\"2025-05-10 00:00:00\",\"FStockOrgId\":{\"FNumber\":\"100\"},\"FCurrId\":{\"FNumber\":\"PRE001\"},\"FOwnerTypeId0\":\"BD_OwnerOrg\",\"FOwnerId0\":{\"FNumber\":\"100\"},\"FPrdOrgId\":{\"FNumber\":\"100\"},\"FWorkShopId\":{\"FNumber\":\"BM000011\"},\"FTransferBizTypeId\":{\"FNumber\":\"OverOrgPick\"},\"FBizType\":\"NORMAL\",\"FEntity\":[{\"FMaterialId\":{\"FNumber\":\"1234-60-1\"},\"FUnitID\":{\"FNumber\":\"Pcs\"},\"FAppQty\":100.0,\"FActualQty\":100.0,\"FStockId\":{\"FNumber\":\"01\"},\"FIsAffectCost\":false,\"FStockUnitId\":{\"FNumber\":\"Pcs\"},\"FStockActualQty\":100.0,\"FOwnerTypeId\":\"BD_OwnerOrg\",\"FExtAuxUnitQty\":0.0,\"FPrice\":0.0,\"FAmount\":0.0,\"FParentOwnerTypeId\":\"BD_OwnerOrg\",\"FParentOwnerId\":{\"FNumber\":\"100\"},\"FBaseUnitId\":{\"FNumber\":\"Pcs\"},\"FBaseActualQty\":100.0,\"FStockStatusId\":{\"FNumber\":\"KCZT01_SYS\"},\"FOwnerId\":{\"FNumber\":\"100\"},\"FSecActualQty\":0.0,\"FBaseAppQty\":100.0,\"FKeeperTypeId\":\"BD_KeeperOrg\",\"FKeeperId\":{\"FNumber\":\"100\"},\"FBomEntryId\":0}]}}";
                    TransferRequest rootObject = JsonConvert.DeserializeObject<TransferRequest>(json);
                    rootObject.Model.FEntity.Clear();
                    rootObject.Model.FDate = DateTime.Now.ToString();
                    rootObject.Model.FBillNo = heard.Fbillno;
                    rootObject.Model.FWorkShopId = new FNumberWrapper { FNumber = FdeptNumber.FNUMBER };

                    //rootObject.Model.FPOOrderFinance.
                    foreach (var entitydata in entityList)
                    {
                        MaterialEntity newEntry = new MaterialEntity();


                        newEntry.FMaterialId = new FNumberWrapper { FNumber = entitydata.Fnumber };
                        newEntry.FUnitID = new FNumberWrapper { FNumber = entitydata.Unit };
                        newEntry.FAppQty = (decimal)entitydata.Fqty;//传订单数量
                        newEntry.FActualQty = (decimal)entitydata.Fqty;//传订单数量
                        //var FstockNumer = context.Sli_bd_stock_view.FirstOrDefault(p => p.Fstockid == entitydata.FStockId); //根据供应商名称查询客户代码
                        newEntry.FStockId = new FNumberWrapper { FNumber = entitydata.FStockNumber };
                        newEntry.FStockUnitId = new FNumberWrapper { FNumber = entitydata.Unit };
                        newEntry.FBaseUnitId = new FNumberWrapper { FNumber = entitydata.Unit };
                        newEntry.FBaseActualQty = (decimal)entitydata.Fqty;//传订单数量
                        newEntry.FBaseAppQty = (decimal)entitydata.Fqty;//传订单数量
                        rootObject.Model.FEntity.Add(newEntry);
                    }
                    string newJson = JsonConvert.SerializeObject(rootObject);
                    System.Diagnostics.Debug.WriteLine(newJson);
                    //sJson = "{\"CreateOrgId\":0,\"Numbers\":[\"" + FPrdModel + "\"]}";
                    //string jsonData = "{\"NeedUpDateFields\": [],\"NeedReturnFields\": [],\"IsDeleteEntry\": \"true\",\"SubSystemId\": \"\",\"IsVerifyBaseDataField\": \"False\",\"IsEntryBatchFill\": \"true\",\"ValidateFlag\": \"true\",\"NumberSearch\": \"true\",\"IsAutoAdjustField\": \"False\",\"InterationFlags\": \"\",\"IgnoreInterationFlag\": \"\",\"IsControlPrecision\": \"False\",\"Model\": {\"FBillTypeID\": {\"FNUMBER\": \"XSDD01_SYS\"},\"FDate\": \"2022-04-27 00:00:00\",\"FSaleOrgId\": {\"FNumber\": \"100\"},\"FCustId\": {\"FNumber\": \"SCMKH100001\"},\"FReceiveId\": {\"FNumber\": \"SCMKH100001\"},\"FSaleDeptId\": {\"FNumber\": \"SCMBM000001\"},\"FSalerId\": {\"FNumber\": \"SCMYG000001_SCMGW000001_1\"},\"FSettleId\": {\"FNumber\": \"SCMKH100001\"},\"FChargeId\": {\"FNumber\": \"SCMKH100001\"},\"FSaleOrderFinance\": {\"FSettleCurrId\": {\"FNumber\": \"PRE001\"},\"FIsPriceExcludeTax\": 'true',\"FIsIncludedTax\": 'true',\"FExchangeTypeId\": {\"FNumber\": \"HLTX01_SYS\"}},\"FSaleOrderEntry\": [{\"FRowType\": \"Standard\",\"FMaterialId\": {\"FNumber\": \"SCMWL100002\"},\"FUnitID\": {\"FNumber\": \"Pcs\"},\"FQty\": 10,\"FPriceUnitId\": {\"FNumber\": \"Pcs\"},\"FPrice\": 8.849558,\"FTaxPrice\": 10,\"FEntryTaxRate\": 13,\"FDeliveryDate\": \"2022-04-27 15:15:54\",\"FStockOrgId\": {\"FNumber\": \"100\"},\"FSettleOrgIds\": {\"FNumber\": \"100\"},\"FSupplyOrgId\": {\"FNumber\": \"100\"},\"FOwnerTypeId\": \"BD_OwnerOrg\",\"FOwnerId\": {\"FNumber\": \"100\"},\"FReserveType\": \"1\",\"FPriceBaseQty\": 10,\"FStockUnitID\": {\"FNumber\": \"Pcs\"},\"FStockQty\": 10,\"FStockBaseQty\": 10,\"FOUTLMTUNIT\": \"SAL\",\"FOutLmtUnitID\": {\"FNumber\": \"Pcs\"},\"FAllAmountExceptDisCount\": 100,\"FOrderEntryPlan\": [{\"FPlanDate\": \"2022-04-27 15:15:54\",\"FPlanQty\": 10}]}],\"FSaleOrderPlan\": [{\"FRecAdvanceRate\": 100,\"FRecAdvanceAmount\": 100}],\"FBillNo\":" + "\"" + Number + "\"" + ",}}";
                    var result = client.Execute<string>("Kingdee.BOS.WebApi.ServicesStub.DynamicFormService.Save",
                    new object[] { "SP_PickMtrl", newJson });

                    ResultData resultdata = JsonConvert.DeserializeObject<ResultData>(result);
                    if (resultdata.Result.ResponseStatus.IsSuccess)
                    {
                        //var FbillNoList = "";
                        //foreach (var FbillNo in resultdata.Result.NeedReturnData)
                        //{

                        //    FbillNoList = FbillNoList + FbillNo.FBillNo;
                        //}

                        var datas = new
                        {
                            code = 200,
                            msg = "ok",
                            date = "同步成功！生产领料单：" + resultdata.Result.Number + ""
                        };
                        heard.Flag = 1;
                        heard.FParameter = newJson;
                        heard.FReason = resultdata.Result.Number;
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

        /// <summary>
        /// 简单生产领料单提交
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IHttpActionResult Pickmtrl_Submit_Approve(string FbillNo)
        {
            try
            {
                var context = new YourDbContext();
                //Assert.IsTrue((bool)isSuccess, resultJson);
                var client = new K3CloudApi("http://36.151.103.130:9000/k3cloud/"); //接口地址
                string dbId = "67b289c33bafdd"; //账套ID
                bool bLogin = client.Login(dbId, "Administrator", "kingdee123*", 2052);
                if (bLogin)
                {
                    var heard = context.Sli_prd_pickmtrl.FirstOrDefault(p => p.Fbillno == FbillNo);   //获取表头单行数据
                    var SubmitsJson = "{\"CreateOrgId\":0,\"Numbers\":[\"" + FbillNo + "\"]}";
                    var Submitresult = client.Execute<string>("Kingdee.BOS.WebApi.ServicesStub.DynamicFormService.Submit",
                    new object[] { "SP_PickMtrl", SubmitsJson });

                    //审核刚创建的销售订单
                    var sJson = "{\"CreateOrgId\":0,\"Numbers\":[\"" + FbillNo + "\"]}";
                    var Auditresult = client.Execute<string>("Kingdee.BOS.WebApi.ServicesStub.DynamicFormService.Audit",
                    new object[] { "SP_PickMtrl", sJson });
                    ResultData resultdata = JsonConvert.DeserializeObject<ResultData>(Auditresult);
                    if (resultdata.Result.ResponseStatus.IsSuccess)
                    {
                        var datas = new
                        {
                            code = 200,
                            msg = "ok",
                            date = "同步成功！采购单号：" + resultdata.Result.Number + ""
                        };
                        heard.Flag = 3;
                        heard.Fdocumentstatus = "C";
                        //heard.FParameter = newJson;
                        heard.FReason = resultdata.Result.Number;
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
                        heard.Flag = 4;
                        //heard.FParameter = newJson;
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