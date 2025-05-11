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
    /// 采购订单
    /// </summary>
    public class sli_pur_orderController : ApiController
    {
        public sli_pur_orderController()
        {
            // _context = context;

        }
        /// <summary>
        /// 采购订单新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<object> Insert([Microsoft.AspNetCore.Mvc.FromBody] sli_pur_po model)
        {
            try
            {
                var context = new YourDbContext();

                var header = new sli_pur_po
                {
                    Fbillno = model.Fbillno,
                    Fdate = model.Fdate,
                    Fsppliername = model.Fsppliername,
                    Fdocumentstatus = model.Fdocumentstatus,
                    Fclosestatus = model.Fclosestatus,
                    sli_pur_poentry = model.sli_pur_poentry.Select(d => new sli_pur_poentry
                    {
                        Fid = model.Fid,
                        Fnumber = d.Fnumber,
                        Fname = d.Fname,
                        Unit = d.Unit,
                        Fqty = d.Fqty,
                        Fdeliverydate = d.Fdeliverydate,
                        Fmrpclosestatus = d.Fmrpclosestatus,
                        Fseq = d.Fseq,
                        Finventory = d.Finventory
                    }).ToList()
                };



                context.Sli_pur_po.Add(header);
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
        /// 采购订单删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<object> Delete(List<int> id)
        {
            try
            {
                var context = new YourDbContext();
                var headersToDelete = context.Sli_pur_po.Where(h => id.Contains(h.Fid)).ToList();
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
                context.Sli_pur_po.RemoveRange(headersToDelete);

                foreach (var DeleteID in id)
                {
                    var Sli_pur_poentry = context.Sli_pur_poentry.Where(b => b.Fid == DeleteID);
                    context.Sli_pur_poentry.RemoveRange(Sli_pur_poentry);
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
        /// 采购订单修改
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<object> Update(sli_pur_po bill)
        {
            try
            {
                var context = new YourDbContext();
                var entity = await context.Sli_pur_po.FindAsync(bill.Fid);
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
                    var Sli_pur_po = context.Sli_pur_po.FirstOrDefault(p => p.Fid == bill.Fid);
                    var Sli_pur_poentry = context.Sli_pur_poentry.Where(p => p.Fid ==bill.Fid).ToList();

                    Sli_pur_po.Fbillno = bill.Fbillno;
                    Sli_pur_po.Fdate = bill.Fdate;
                    Sli_pur_po.Fsppliername = bill.Fsppliername;
                    Sli_pur_po.Fdocumentstatus = bill.Fdocumentstatus;
                    Sli_pur_po.Fclosestatus = bill.Fclosestatus;


                    context.Sli_pur_poentry.RemoveRange(Sli_pur_poentry);

                    foreach (var d in bill.sli_pur_poentry)
                    {
                        var entry = new sli_pur_poentry
                        {
                            Fid = bill.Fid,
                            Fnumber = d.Fnumber,
                            Fname = d.Fname,
                            Unit = d.Unit,
                            Fqty = d.Fqty,
                            Fdeliverydate = d.Fdeliverydate,
                            Fmrpclosestatus = d.Fmrpclosestatus,
                            Fseq = d.Fseq,
                            Finventory = d.Finventory
                        };
                        context.Sli_pur_poentry.Add(entry);
                    }
                    await context.SaveChangesAsync();
                    var datas = new
                    {
                        code = 200,
                        msg = "ok",
                        date = bill.Fid + "更新成功！"
                    };
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
        /// 采购订单表头分页查询
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetHeaderPurorder(int page = 1, int pageSize = 10,string Fdocumentstatus=null, string Fbillno = null)
        {
            var context = new YourDbContext();
            IQueryable<sli_pur_po_view> query = context.Sli_pur_po_view;
            if (!string.IsNullOrEmpty(Fdocumentstatus))
            {
                query = query.Where(t => t.Fdocumentstatus == Fdocumentstatus);
            }
            if (!string.IsNullOrEmpty(Fbillno))
            {
                query = query.Where(t => t.Fbillno.Contains(Fbillno));
            }
            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedQuery = query.OrderByDescending(b => b.Fid).Skip((page - 1) * pageSize).Take(pageSize);
            var result = paginatedQuery.Select(a => new
            {
                id = a.Fid,
                Fbillno = a.Fbillno,
                Fdate = a.Fdate,
                Fsppliername = a.Fsppliername,
                Fdocumentstatus = a.Fdocumentstatus,
                Fclosestatus = a.Fclosestatus,
                Flag = a.Flag ?? 0,
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
        /// 采购订单表体分页查询
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="FID"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetEntryPurorder(int ? FID=null,string Fbillno=null)
        {
            var context = new YourDbContext();
            var query = context.Sli_pur_po_view.Include(a => a.sli_pur_poentry_view);
            if (FID.HasValue)
            {
                query = query.Where(t => t.Fid == FID.Value);
            }
            if (!string.IsNullOrEmpty(Fbillno))
            {
                query = query.Where(t => t.Fbillno.Contains(Fbillno));
            }

            var result = query.Select(a => new
            {
                Fid = a.Fid,
                Fbillno = a.Fbillno,
                Fdate = a.Fdate,
                Fsppliername = a.Fsppliername,
                Fdocumentstatus = a.Fdocumentstatus,
                Fclosestatus = a.Fclosestatus,
                Flag = a.Flag ?? 0,
                FParameter = a.FParameter ?? string.Empty,
                FplanFReasonend = a.FReason ?? string.Empty,
                sli_pur_poentry_view = a.sli_pur_poentry_view.Select(b => new
                {
                    Fentryid = b.Fentryid,
                    Fid = b.Fid,
                    Fnumber = b.Fnumber,
                    Fname = b.Fname,
                    Unit = b.Unit,
                    Fqty = b.Fqty,
                    Fdeliverydate = b.Fdeliverydate,
                    Fmrpclosestatus = b.Fmrpclosestatus,
                    Fseq = b.Fseq,
                    Finventory = b.Finventory
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
        /// 同步采购订单到金蝶星空
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

                    var heard = context.Sli_pur_po.FirstOrDefault(p => p.Fid == id);   //获取表头单行数据
                    var FcustomerNumer = context.Sli_bd_supplier.FirstOrDefault(p => p.FNAME == heard.Fsppliername); //根据供应商名称查询客户代码
                    var entity = context.Sli_pur_poentry.Where(p => p.Fid == id);   //获取表体多行数据
                    var entityList = entity.ToList();
                    //var index = 0;
                    string json = "{\"NeedUpDateFields\":[],\"NeedReturnFields\":[],\"IsDeleteEntry\":\"true\",\"SubSystemId\":\"\",\"IsVerifyBaseDataField\":\"false\",\"IsEntryBatchFill\":\"true\",\"ValidateFlag\":\"true\",\"NumberSearch\":\"true\",\"IsAutoAdjustField\":\"true\",\"InterationFlags\":\"\",\"IgnoreInterationFlag\":\"\",\"IsControlPrecision\":\"false\",\"ValidateRepeatJson\":\"false\",\"Model\":{\"FID\":0,\"FBillTypeID\":{\"FNUMBER\":\"CGDD01_SYS\"},\"FBusinessType\":\"CG\",\"FDate\":\"2025-05-07 00:00:00\",\"FSupplierId\":{\"FNumber\":\"VEN00007\"},\"FPurchaseOrgId\":{\"FNumber\":\"100\"},\"FProviderId\":{\"FNumber\":\"VEN00007\"},\"FSettleId\":{\"FNumber\":\"VEN00007\"},\"FChargeId\":{\"FNumber\":\"VEN00007\"},\"FSourceBillNo\":\"\",\"FIsModificationOperator\":false,\"FChangeStatus\":\"A\",\"FACCTYPE\":\"Q\",\"FIsMobBill\":false,\"FPOOrderFinance\":{\"FSettleCurrId\":{\"FNumber\":\"PRE001\"},\"FExchangeTypeId\":{\"FNumber\":\"HLTX01_SYS\"},\"FExchangeRate\":1.0,\"FPriceTimePoint\":\"1\",\"FFOCUSSETTLEORGID\":{\"FNumber\":\"100\"},\"FIsIncludedTax\":true,\"FISPRICEEXCLUDETAX\":true,\"FLocalCurrId\":{\"FNumber\":\"PRE001\"},\"FPAYADVANCEAMOUNT\":0.0,\"FSupToOderExchangeBusRate\":1.0,\"FSEPSETTLE\":false,\"FDepositRatio\":0.0,\"FAllDisCount\":0.0,\"FUPPERBELIEL\":0.0},\"FPOOrderPay\":{},\"FPOOrderEntry\":[{\"FProductType\":\"1\",\"FMaterialId\":{\"FNumber\":\"1234-60-10\"},\"FMaterialDesc\":\"浮头法兰\",\"FUnitId\":{\"FNumber\":\"Pcs\"},\"FQty\":100.0,\"FPriceUnitId\":{\"FNumber\":\"Pcs\"},\"FPriceUnitQty\":100.0,\"FPriceBaseQty\":100.0,\"FDeliveryDate\":\"2025-05-07 21:39:32\",\"FPrice\":88.495575,\"FTaxPrice\":100.0,\"FEntryDiscountRate\":0.0,\"FEntryTaxRate\":13.00,\"FRequireOrgId\":{\"FNumber\":\"100\"},\"FReceiveOrgId\":{\"FNumber\":\"100\"},\"FEntrySettleOrgId\":{\"FNumber\":\"100\"},\"FGiveAway\":false,\"FStockUnitID\":{\"FNumber\":\"Pcs\"},\"FStockQty\":100.0,\"FStockBaseQty\":100.0,\"FDeliveryControl\":false,\"FTimeControl\":false,\"FDeliveryMaxQty\":100.0,\"FDeliveryMinQty\":100.0,\"FDeliveryBeforeDays\":0,\"FDeliveryDelayDays\":0,\"FDeliveryEarlyDate\":\"2025-05-07 21:39:32\",\"FDeliveryLastDate\":\"2025-05-07 23:59:59\",\"FPriceCoefficient\":1.0,\"FConsumeSumQty\":0.0,\"FSrcBillTypeId\":\"\",\"FSrcBillNo\":\"\",\"FDEMANDBILLENTRYSEQ\":0,\"FDEMANDBILLENTRYID\":0,\"FPlanConfirm\":true,\"FSalUnitID\":{\"FNumber\":\"Pcs\"},\"FSalQty\":100.0,\"FSalJoinQty\":0.0,\"FBaseSalJoinQty\":0.0,\"FInventoryQty\":0.0,\"FCentSettleOrgId\":{\"FNumber\":\"100\"},\"FDispSettleOrgId\":{\"FNumber\":\"100\"},\"FGroup\":0,\"FDeliveryStockStatus\":{\"FNumber\":\"KCZT02_SYS\"},\"FMaxPrice\":0.0,\"FMinPrice\":0.0,\"FIsStock\":false,\"FBaseConsumeSumQty\":0.0,\"FSalBaseQty\":100.0,\"FSubOrgId\":{\"FNumber\":\"100\"},\"FEntryPayOrgId\":{\"FNumber\":\"100\"},\"FPriceDiscount\":0.0,\"FAllAmountExceptDisCount\":10000.0,\"FSUBREQBILLSEQ\":0,\"FSUBREQENTRYID\":0,\"FEntryDeliveryPlan\":[{\"FDeliveryDate_Plan\":\"2025-05-07 21:39:32\",\"FPlanQty\":100.0,\"FPREARRIVALDATE\":\"2025-05-07 21:39:32\",\"FTRLT\":0,\"FConfirmDeliQty\":0.0}]}]}}";
                    PurchaseOrderRequest rootObject = JsonConvert.DeserializeObject<PurchaseOrderRequest>(json);
                    rootObject.Model.FPOOrderEntry.Clear();
                    rootObject.Model.FDate = DateTime.Now.ToString();
                    rootObject.Model.FBillNo = heard.Fbillno;
                    //rootObject.Model.FPOOrderFinance.
                    foreach (var entitydata in entityList)
                    {
                        POOrderEntry newEntry = new POOrderEntry();

                       
                        newEntry.FProductType = "1";
                        newEntry.FMaterialId = new NumberField { FNumber = entitydata.Fnumber };
                        newEntry.FMaterialDesc = entitydata.Fname;//传名称
                        newEntry.FUnitId = new NumberField { FNumber = entitydata.Unit };
                        newEntry.FQty = entitydata.Fqty;//传订单数量
                        newEntry.FPriceUnitId = new NumberField { FNumber = entitydata.Unit };
                        newEntry.FPriceUnitQty = entitydata.Fqty;//传订单数量
                        newEntry.FPriceBaseQty = entitydata.Fqty;//传订单数量
                        newEntry.FDeliveryDate =Convert.ToString (entitydata.Fdeliverydate);//传订单交货日期
                        newEntry.FPrice = 0;//传订单物料单价
                        newEntry.FTaxPrice = 0;//传订单物料含税单价
                        newEntry.FEntryDiscountRate = 0;
                        newEntry.FStockQty = entitydata.Fqty;// 传订单数量
                        newEntry.FStockBaseQty = entitydata.Fqty;// 传订单数量
                        newEntry.FDeliveryMaxQty = entitydata.Fqty;// 传订单数量
                        newEntry.FDeliveryMinQty = entitydata.Fqty;// 传订单数量
                        newEntry.FDeliveryEarlyDate = Convert.ToString(entitydata.Fdeliverydate);//传订单交货日期
                        newEntry.FDeliveryLastDate = Convert.ToString(entitydata.Fdeliverydate);//传订单交货日期
                        newEntry.FSalQty = entitydata.Fqty;// 传订单数量
                        newEntry.FSalBaseQty = entitydata.Fqty;// 传订单数量

                        EntryDeliveryPlan newDelivery = new EntryDeliveryPlan();
                        newDelivery.FDeliveryDate_Plan = Convert.ToString(entitydata.Fdeliverydate);
                        newDelivery.FDeliveryDate_Plan = Convert.ToString(entitydata.Fdeliverydate);
                        newDelivery.FPlanQty = entitydata.Fqty;
                        newDelivery.FPREARRIVALDATE = Convert.ToString(entitydata.Fdeliverydate);
                        newDelivery.FTRLT = 0;
                        newDelivery.FConfirmDeliQty = 0.0;
                        newEntry.FEntryDeliveryPlan = new List<EntryDeliveryPlan>();
                        newEntry.FEntryDeliveryPlan.Add(newDelivery);
                        rootObject.Model.FPOOrderEntry.Add(newEntry);
                    }
                    string newJson = JsonConvert.SerializeObject(rootObject);
                    System.Diagnostics.Debug.WriteLine(newJson);
                    //sJson = "{\"CreateOrgId\":0,\"Numbers\":[\"" + FPrdModel + "\"]}";
                    //string jsonData = "{\"NeedUpDateFields\": [],\"NeedReturnFields\": [],\"IsDeleteEntry\": \"true\",\"SubSystemId\": \"\",\"IsVerifyBaseDataField\": \"False\",\"IsEntryBatchFill\": \"true\",\"ValidateFlag\": \"true\",\"NumberSearch\": \"true\",\"IsAutoAdjustField\": \"False\",\"InterationFlags\": \"\",\"IgnoreInterationFlag\": \"\",\"IsControlPrecision\": \"False\",\"Model\": {\"FBillTypeID\": {\"FNUMBER\": \"XSDD01_SYS\"},\"FDate\": \"2022-04-27 00:00:00\",\"FSaleOrgId\": {\"FNumber\": \"100\"},\"FCustId\": {\"FNumber\": \"SCMKH100001\"},\"FReceiveId\": {\"FNumber\": \"SCMKH100001\"},\"FSaleDeptId\": {\"FNumber\": \"SCMBM000001\"},\"FSalerId\": {\"FNumber\": \"SCMYG000001_SCMGW000001_1\"},\"FSettleId\": {\"FNumber\": \"SCMKH100001\"},\"FChargeId\": {\"FNumber\": \"SCMKH100001\"},\"FSaleOrderFinance\": {\"FSettleCurrId\": {\"FNumber\": \"PRE001\"},\"FIsPriceExcludeTax\": 'true',\"FIsIncludedTax\": 'true',\"FExchangeTypeId\": {\"FNumber\": \"HLTX01_SYS\"}},\"FSaleOrderEntry\": [{\"FRowType\": \"Standard\",\"FMaterialId\": {\"FNumber\": \"SCMWL100002\"},\"FUnitID\": {\"FNumber\": \"Pcs\"},\"FQty\": 10,\"FPriceUnitId\": {\"FNumber\": \"Pcs\"},\"FPrice\": 8.849558,\"FTaxPrice\": 10,\"FEntryTaxRate\": 13,\"FDeliveryDate\": \"2022-04-27 15:15:54\",\"FStockOrgId\": {\"FNumber\": \"100\"},\"FSettleOrgIds\": {\"FNumber\": \"100\"},\"FSupplyOrgId\": {\"FNumber\": \"100\"},\"FOwnerTypeId\": \"BD_OwnerOrg\",\"FOwnerId\": {\"FNumber\": \"100\"},\"FReserveType\": \"1\",\"FPriceBaseQty\": 10,\"FStockUnitID\": {\"FNumber\": \"Pcs\"},\"FStockQty\": 10,\"FStockBaseQty\": 10,\"FOUTLMTUNIT\": \"SAL\",\"FOutLmtUnitID\": {\"FNumber\": \"Pcs\"},\"FAllAmountExceptDisCount\": 100,\"FOrderEntryPlan\": [{\"FPlanDate\": \"2022-04-27 15:15:54\",\"FPlanQty\": 10}]}],\"FSaleOrderPlan\": [{\"FRecAdvanceRate\": 100,\"FRecAdvanceAmount\": 100}],\"FBillNo\":" + "\"" + Number + "\"" + ",}}";
                    var result = client.Execute<string>("Kingdee.BOS.WebApi.ServicesStub.DynamicFormService.Save",
                    new object[] { "PUR_PurchaseOrder", newJson });

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
                            date = "同步成功！采购单号：" + resultdata.Result.Number + ""
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
        /// 采购入库单提交
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IHttpActionResult Porder_Submit_Approve(string FbillNo)
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
                    var heard = context.Sli_pur_po.FirstOrDefault(p => p.Fbillno == FbillNo);   //获取表头单行数据
                    var SubmitsJson = "{\"CreateOrgId\":0,\"Numbers\":[\"" + FbillNo + "\"]}";
                    var Submitresult = client.Execute<string>("Kingdee.BOS.WebApi.ServicesStub.DynamicFormService.Submit",
                    new object[] { "PUR_PurchaseOrder", SubmitsJson });

                    //审核刚创建的销售订单
                    var sJson = "{\"CreateOrgId\":0,\"Numbers\":[\"" + FbillNo + "\"]}";
                    var Auditresult = client.Execute<string>("Kingdee.BOS.WebApi.ServicesStub.DynamicFormService.Audit",
                    new object[] { "PUR_PurchaseOrder", sJson });
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