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
    /// 采购入库单
    /// </summary>
    public class sli_stk_instockController : ApiController
    {
        public sli_stk_instockController()
        {
            // _context = context;

        }
        /// <summary>
        /// 采购入库单新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<object> Insert([Microsoft.AspNetCore.Mvc.FromBody] sli_stk_instock model)
        {
            try
            {
                var context = new YourDbContext();

                var header = new sli_stk_instock
                {
                    FBillNo = model.FBillNo,
                    FDate = model.FDate,
                    FSpplierName = model.FSpplierName,
                    FDocumentStatus = model.FDocumentStatus,
                    FCloseStatus = model.FCloseStatus,
                    FBiller = model.FBiller,
                    FDepId = model.FDepId,
                    sli_stk_instockentry = model.sli_stk_instockentry.Select(d => new sli_stk_instockentry
                    {
                        Fid = model.Fid,
                        FNumber = d.FNumber,
                        FName = d.FName,
                        Unit = d.Unit,
                        FQty = d.FQty,
                        FSecQty = d.FSecQty,
                        FStockId = d.FStockId
                    }).ToList()
                };



                context.Sli_stk_instock.Add(header);
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
                var headersToDelete = context.Sli_stk_instock.Where(h => id.Contains(h.Fid)).ToList();
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
                context.Sli_stk_instock.RemoveRange(headersToDelete);

                foreach (var DeleteID in id)
                {
                    var Sli_pur_poentry = context.Sli_stk_instockentry.Where(b => b.Fid == DeleteID);
                    context.Sli_stk_instockentry.RemoveRange(Sli_pur_poentry);
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
        public async Task<object> Update(sli_stk_instock bill)
        {
            try
            {
                var context = new YourDbContext();
                var entity = await context.Sli_stk_instock.FindAsync(bill.Fid);
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
                    var Sli_stk_instock = context.Sli_stk_instock.FirstOrDefault(p => p.Fid == bill.Fid);
                    var Sli_stk_instockentry = context.Sli_stk_instockentry.Where(p => p.Fid == bill.Fid).ToList();

                    Sli_stk_instock.FBillNo = bill.FBillNo;
                    Sli_stk_instock.FDate = bill.FDate;
                    Sli_stk_instock.FSpplierName = bill.FSpplierName;
                    Sli_stk_instock.FDocumentStatus = bill.FDocumentStatus;
                    Sli_stk_instock.FCloseStatus = bill.FCloseStatus;
                    Sli_stk_instock.FBiller = bill.FBiller;
                    Sli_stk_instock.FDepId = bill.FDepId;
                    Sli_stk_instock.FDepId = bill.FDepId;

                    context.Sli_stk_instockentry.RemoveRange(Sli_stk_instockentry);

                    foreach (var d in bill.sli_stk_instockentry)
                    {
                        var entry = new sli_stk_instockentry
                        {
                            Fid = bill.Fid,
                            FNumber = d.FNumber,
                            FName = d.FName,
                            Unit = d.Unit,
                            FQty = d.FQty,
                            FSecQty = d.FSecQty,
                            FStockId = d.FStockId
                        };
                        context.Sli_stk_instockentry.Add(entry);
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
        public IHttpActionResult GetHeaderInstock(int page = 1, int pageSize = 10)
        {
            var context = new YourDbContext();
            var query = context.Sli_stk_instock_view;
            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedQuery = query.OrderByDescending(b => b.Fid).Skip((page - 1) * pageSize).Take(pageSize);
            var result = paginatedQuery.Select(a => new
            {
                id = a.Fid,
                FBillNo = a.FBillNo,
                FDate = a.FDate,
                FSpplierName = a.FSpplierName,
                FDocumentStatus = a.FDocumentStatus,
                FCloseStatus = a.FCloseStatus,
                FBiller = a.FBiller,
                empname = a.empname,
                Flag = a.Flag,
                FParameter = a.FParameter,
                FReason = a.FReason
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
        public IHttpActionResult GetEntryPurorder(int? FID = null)
        {
            var context = new YourDbContext();
            var query = context.Sli_stk_instock_view.Include(a => a.sli_stk_instockentry_view);
            if (FID.HasValue)
            {
                query = query.Where(t => t.Fid == FID.Value);
            }
            var result = query.Select(a => new
            {
                Fid = a.Fid,
                FBillNo = a.FBillNo,
                FDate = a.FDate,
                FSpplierName = a.FSpplierName,
                FDocumentStatus = a.FDocumentStatus,
                FCloseStatus = a.FCloseStatus,
                FBiller = a.FBiller,
                empname = a.empname,
                Flag = a.Flag,
                FParameter = a.FParameter,
                FReason = a.FReason,
                sli_stk_instockentry_view = a.sli_stk_instockentry_view.Select(b => new
                {
                    FEntryId = b.FEntryId,
                    Fid = b.Fid,
                    FNumber = b.FNumber,
                    FName = b.FName,
                    Unit = b.Unit,
                    FQty = b.FQty,
                    FSecQty = b.FSecQty,
                    FStockId = b.FStockId
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
    }
}