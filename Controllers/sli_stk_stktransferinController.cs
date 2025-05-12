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
{
    /// <summary>
    /// 直接调拨单
    /// </summary>
    public class sli_stk_stktransferinController : ApiController
    {
        public sli_stk_stktransferinController()
        {
            // _context = context;

        }
        /// <summary>
        /// 直接调拨单新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<object> Insert([Microsoft.AspNetCore.Mvc.FromBody] sli_stk_stktransferin model)
        {
            try
            {
                var context = new YourDbContext();

                var header = new sli_stk_stktransferin
                {
                    Fbillno = model.Fbillno,
                    Fdate = model.Fdate,
                    //Fsppliername = model.Fsppliername,
                    Fdocumentstatus = model.Fdocumentstatus,
                    Fclosestatus = model.Fclosestatus,
                    FBiller = model.FBiller,
                    FDepId = model.FDepId,
                    Flag = 0,
                    sli_stk_stktransferinentry = model.sli_stk_stktransferinentry.Select(d => new sli_stk_stktransferinentry
                    {
                        Fid = model.Fid,
                        Fnumber = d.Fnumber,
                        Fname = d.Fname,
                        Fbatchno = d.Fbatchno,
                        Unit = d.Unit,
                        Fqty = d.Fqty,
                        FSecQty = d.FSecQty,
                        FReceiveStockNumber = d.FReceiveStockNumber,
                        FStockNumber = d.FStockNumber
                    }).ToList()
                };



                context.Sli_stk_stktransferin.Add(header);
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
        /// 直接调拨单删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<object> Delete(List<int> id)
        {
            try
            {
                var context = new YourDbContext();
                var headersToDelete = context.Sli_stk_stktransferin.Where(h => id.Contains(h.Fid)).ToList();
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
                context.Sli_stk_stktransferin.RemoveRange(headersToDelete);

                foreach (var DeleteID in id)
                {
                    var Sli_stk_stktransferinentry = context.Sli_stk_stktransferinentry.Where(b => b.Fid == DeleteID);
                    context.Sli_stk_stktransferinentry.RemoveRange(Sli_stk_stktransferinentry);
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
        /// 直接调拨单修改
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<object> Update(sli_stk_stktransferin bill)
        {
            try
            {
                var context = new YourDbContext();
                var entity = await context.Sli_stk_stktransferin.FindAsync(bill.Fid);
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
                    var Sli_stk_stktransferin = context.Sli_stk_stktransferin.FirstOrDefault(p => p.Fid == bill.Fid);
                    var Sli_stk_stktransferinentry = context.Sli_stk_stktransferinentry.Where(p => p.Fid == bill.Fid).ToList();

                    Sli_stk_stktransferin.Fbillno = bill.Fbillno;
                    Sli_stk_stktransferin.Fdate = bill.Fdate;
                    //Sli_stk_instock.Fsppliername = bill.Fsppliername;
                    Sli_stk_stktransferin.Fdocumentstatus = bill.Fdocumentstatus;
                    Sli_stk_stktransferin.Fclosestatus = bill.Fclosestatus;
                    Sli_stk_stktransferin.FBiller = bill.FBiller;
                    Sli_stk_stktransferin.FDepId = bill.FDepId;


                    context.Sli_stk_stktransferinentry.RemoveRange(Sli_stk_stktransferinentry);

                    foreach (var d in bill.sli_stk_stktransferinentry)
                    {
                        var entry = new sli_stk_stktransferinentry
                        {
                            Fid = bill.Fid,
                            Fnumber = d.Fnumber,
                            Fname = d.Fname,
                            Unit = d.Unit,
                            Fbatchno = d.Fbatchno,
                            Fqty = d.Fqty,
                            FSecQty = d.FSecQty,
                            FReceiveStockNumber = d.FReceiveStockNumber,
                            FStockNumber = d.FStockNumber
                        };
                        context.Sli_stk_stktransferinentry.Add(entry);
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
                        new object[] { "STK_TransferDirect", UnAuditsJson });

                        var Deleteresult = client.Execute<string>("Kingdee.BOS.WebApi.ServicesStub.DynamicFormService.Delete",
                        new object[] { "STK_TransferDirect", UnAuditsJson });

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
        public IHttpActionResult GetHeaderStktransferin(int page = 1, int pageSize = 10)
        {
            var context = new YourDbContext();
            var query = context.Sli_stk_stktransferin_view;
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
                Flag = a.Flag,
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
        public IHttpActionResult GetEntryStktransferin(int? FID = null)
        {
            var context = new YourDbContext();
            var query = context.Sli_stk_stktransferin_view.Include(a => a.sli_stk_stktransferinentry_view);
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
                sli_stk_stktransferinentry_view = a.sli_stk_stktransferinentry_view.Select(b => new
                {
                    Fentryid = b.FentryId,
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

                    var heard = context.Sli_stk_stktransferin.FirstOrDefault(p => p.Fid == id);   //获取表头单行数据
                    //var FdeptNumber = context.Sli_bd_department_view.FirstOrDefault(p => p.Fdeptid == heard.FDepId); //根据供应商名称查询客户代码
                    var entity = context.Sli_stk_stktransferinentry.Where(p => p.Fid == id);   //获取表体多行数据
                    var entityList = entity.ToList();
                    //var index = 0;
                    string json = "{\"NeedUpDateFields\":[],\"NeedReturnFields\":[],\"IsDeleteEntry\":\"true\",\"SubSystemId\":\"\",\"IsVerifyBaseDataField\":\"false\",\"IsEntryBatchFill\":\"true\",\"ValidateFlag\":\"true\",\"NumberSearch\":\"true\",\"IsAutoAdjustField\":\"true\",\"InterationFlags\":\"\",\"IgnoreInterationFlag\":\"\",\"IsControlPrecision\":\"false\",\"ValidateRepeatJson\":\"false\",\"Model\":{\"FID\":0,\"FBillNo\":\"123\",\"FBillTypeID\":{\"FNUMBER\":\"ZJDB01_SYS\"},\"FBizType\":\"NORMAL\",\"FTransferDirect\":\"GENERAL\",\"FTransferBizType\":\"InnerOrgTransfer\",\"FSaleOrgId\":{\"FNumber\":\"100\"},\"FSettleOrgId\":{\"FNumber\":\"100\"},\"FStockOutOrgId\":{\"FNumber\":\"100\"},\"FOwnerTypeOutIdHead\":\"BD_OwnerOrg\",\"FOwnerOutIdHead\":{\"FNumber\":\"100\"},\"FStockOrgId\":{\"FNumber\":\"100\"},\"FIsPriceExcludeTax\":true,\"FExchangeTypeId\":{\"FNUMBER\":\"HLTX01_SYS\"},\"FIsIncludedTax\":true,\"FSETTLECURRID\":{\"FNUMBER\":\"PRE001\"},\"FExchangeRate\":1.0,\"FOwnerTypeIdHead\":\"BD_OwnerOrg\",\"FOwnerIdHead\":{\"FNumber\":\"100\"},\"FDate\":\"2025-05-11 00:00:00\",\"FBaseCurrId\":{\"FNumber\":\"PRE001\"},\"FWriteOffConsign\":false,\"FBillEntry\":[{\"FRowType\":\"Standard\",\"FMaterialId\":{\"FNumber\":\"1234-60-1\"},\"FUnitID\":{\"FNumber\":\"Pcs\"},\"FQty\":1.0,\"FSrcStockId\":{\"FNumber\":\"01\"},\"FDestStockId\":{\"FNumber\":\"02\"},\"FSrcStockStatusId\":{\"FNumber\":\"KCZT01_SYS\"},\"FDestStockStatusId\":{\"FNumber\":\"KCZT01_SYS\"},\"FBusinessDate\":\"2025-05-11 00:00:00\",\"FSrcBillTypeId\":\"\",\"FOwnerTypeOutId\":\"BD_OwnerOrg\",\"FOwnerOutId\":{\"FNumber\":\"100\"},\"FOwnerTypeId\":\"BD_OwnerOrg\",\"FOwnerId\":{\"FNumber\":\"100\"},\"FSrcBillNo\":\"\",\"FSecQty\":0.0,\"FExtAuxUnitQty\":0.0,\"FBaseUnitId\":{\"FNumber\":\"Pcs\"},\"FBaseQty\":1.0,\"FISFREE\":false,\"FKeeperTypeId\":\"BD_KeeperOrg\",\"FActQty\":0.0,\"FKeeperId\":{\"FNumber\":\"100\"},\"FKeeperTypeOutId\":\"BD_KeeperOrg\",\"FKeeperOutId\":{\"FNumber\":\"100\"},\"FDiscountRate\":0.0,\"FRepairQty\":0.0,\"FDestMaterialId\":{\"FNUMBER\":\"1234-60-1\"},\"FSaleUnitId\":{\"FNumber\":\"Pcs\"},\"FSaleQty\":1.0,\"FSalBaseQty\":1.0,\"FPriceUnitID\":{\"FNumber\":\"Pcs\"},\"FPriceQty\":1.0,\"FPriceBaseQty\":1.0,\"FOutJoinQty\":0.0,\"FBASEOUTJOINQTY\":0.0,\"FSOEntryId\":0,\"FTransReserveLink\":false,\"FQmEntryId\":0,\"FConvertEntryId\":0,\"FCheckDelivery\":false,\"FBomEntryId\":0}]}}";
                    transferinRequest rootObject = JsonConvert.DeserializeObject<transferinRequest>(json);
                    rootObject.Model.FBillEntry.Clear();
                    rootObject.Model.FDate = Convert.ToString( heard.Fdate);
                    rootObject.Model.FBillNo = heard.Fbillno;
              

                    //rootObject.Model.FPOOrderFinance.
                    foreach (var entitydata in entityList)
                    {
                        BillEntry newEntry = new BillEntry();


                        newEntry.FMaterialId = new FNumberObject { FNumber = entitydata.Fnumber };
                        newEntry.FUnitID = new FNumberObject { FNumber = entitydata.Unit };
                        newEntry.FQty = (double)entitydata.Fqty;//传订单数量
                        newEntry.FSrcStockId = new FNumberObject { FNumber = entitydata.FStockNumber };;//传订单数量
                        //var FstockNumer = context.Sli_bd_stock_view.FirstOrDefault(p => p.Fstockid == entitydata.FStockId); //根据供应商名称查询客户代码
                        newEntry.FDestStockId = new FNumberObject { FNumber = entitydata.FReceiveStockNumber };
                        newEntry.FBusinessDate = Convert.ToString(heard.Fdate);
                        newEntry.FBaseQty = (double)entitydata.Fqty;//传订单数量
                        newEntry.FDestMaterialId = new FNumberObject { FNumber = entitydata.Fnumber };
                        newEntry.FSaleUnitId = new FNumberObject { FNumber = entitydata.Unit };
                        newEntry.FSaleQty = (double)entitydata.Fqty;//传订单数量FSalBaseQty
                        newEntry.FSalBaseQty = (double)entitydata.Fqty;//传订单数量
                        newEntry.FPriceUnitID = new FNumberObject { FNumber = entitydata.Unit };
                        newEntry.FPriceQty = (double)entitydata.Fqty;//传订单数量FSalBaseQty
                        newEntry.FPriceBaseQty = (double)entitydata.Fqty;//传订单数量
                        rootObject.Model.FBillEntry.Add(newEntry);
                    }
                    string newJson = JsonConvert.SerializeObject(rootObject);
                    System.Diagnostics.Debug.WriteLine(newJson);
                    //sJson = "{\"CreateOrgId\":0,\"Numbers\":[\"" + FPrdModel + "\"]}";
                    //string jsonData = "{\"NeedUpDateFields\": [],\"NeedReturnFields\": [],\"IsDeleteEntry\": \"true\",\"SubSystemId\": \"\",\"IsVerifyBaseDataField\": \"False\",\"IsEntryBatchFill\": \"true\",\"ValidateFlag\": \"true\",\"NumberSearch\": \"true\",\"IsAutoAdjustField\": \"False\",\"InterationFlags\": \"\",\"IgnoreInterationFlag\": \"\",\"IsControlPrecision\": \"False\",\"Model\": {\"FBillTypeID\": {\"FNUMBER\": \"XSDD01_SYS\"},\"FDate\": \"2022-04-27 00:00:00\",\"FSaleOrgId\": {\"FNumber\": \"100\"},\"FCustId\": {\"FNumber\": \"SCMKH100001\"},\"FReceiveId\": {\"FNumber\": \"SCMKH100001\"},\"FSaleDeptId\": {\"FNumber\": \"SCMBM000001\"},\"FSalerId\": {\"FNumber\": \"SCMYG000001_SCMGW000001_1\"},\"FSettleId\": {\"FNumber\": \"SCMKH100001\"},\"FChargeId\": {\"FNumber\": \"SCMKH100001\"},\"FSaleOrderFinance\": {\"FSettleCurrId\": {\"FNumber\": \"PRE001\"},\"FIsPriceExcludeTax\": 'true',\"FIsIncludedTax\": 'true',\"FExchangeTypeId\": {\"FNumber\": \"HLTX01_SYS\"}},\"FSaleOrderEntry\": [{\"FRowType\": \"Standard\",\"FMaterialId\": {\"FNumber\": \"SCMWL100002\"},\"FUnitID\": {\"FNumber\": \"Pcs\"},\"FQty\": 10,\"FPriceUnitId\": {\"FNumber\": \"Pcs\"},\"FPrice\": 8.849558,\"FTaxPrice\": 10,\"FEntryTaxRate\": 13,\"FDeliveryDate\": \"2022-04-27 15:15:54\",\"FStockOrgId\": {\"FNumber\": \"100\"},\"FSettleOrgIds\": {\"FNumber\": \"100\"},\"FSupplyOrgId\": {\"FNumber\": \"100\"},\"FOwnerTypeId\": \"BD_OwnerOrg\",\"FOwnerId\": {\"FNumber\": \"100\"},\"FReserveType\": \"1\",\"FPriceBaseQty\": 10,\"FStockUnitID\": {\"FNumber\": \"Pcs\"},\"FStockQty\": 10,\"FStockBaseQty\": 10,\"FOUTLMTUNIT\": \"SAL\",\"FOutLmtUnitID\": {\"FNumber\": \"Pcs\"},\"FAllAmountExceptDisCount\": 100,\"FOrderEntryPlan\": [{\"FPlanDate\": \"2022-04-27 15:15:54\",\"FPlanQty\": 10}]}],\"FSaleOrderPlan\": [{\"FRecAdvanceRate\": 100,\"FRecAdvanceAmount\": 100}],\"FBillNo\":" + "\"" + Number + "\"" + ",}}";
                    var result = client.Execute<string>("Kingdee.BOS.WebApi.ServicesStub.DynamicFormService.Save",
                    new object[] { "STK_TransferDirect", newJson });

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
                            date = "同步成功！直接调拨单：" + resultdata.Result.Number + ""
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
        /// 直接调拨单提交
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IHttpActionResult Stktransferin_Submit_Approve(string FbillNo)
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
                    var heard = context.Sli_stk_stktransferin.FirstOrDefault(p => p.Fbillno == FbillNo);   //获取表头单行数据
                    var SubmitsJson = "{\"CreateOrgId\":0,\"Numbers\":[\"" + FbillNo + "\"]}";
                    var Submitresult = client.Execute<string>("Kingdee.BOS.WebApi.ServicesStub.DynamicFormService.Submit",
                    new object[] { "STK_TransferDirect", SubmitsJson });

                    //审核刚创建的销售订单
                    var sJson = "{\"CreateOrgId\":0,\"Numbers\":[\"" + FbillNo + "\"]}";
                    var Auditresult = client.Execute<string>("Kingdee.BOS.WebApi.ServicesStub.DynamicFormService.Audit",
                    new object[] { "STK_TransferDirect", sJson });
                    ResultData resultdata = JsonConvert.DeserializeObject<ResultData>(Auditresult);
                    if (resultdata.Result.ResponseStatus.IsSuccess)
                    {
                        var datas = new
                        {
                            code = 200,
                            msg = "ok",
                            date = "同步成功！直接调拨单：" + resultdata.Result.Number + ""
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
                            msg = "err,审核不成功！" + ErrorList + "",
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