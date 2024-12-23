using Microsoft.Ajax.Utilities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi_SY.Entity;
using WebApi_SY.Models;

namespace WebApi_SY.Controllers
{
    public class sli_workOrderListController : ApiController
    {
        public sli_workOrderListController()
        {
            // _context = context;

        }
        [System.Web.Http.HttpPost]
        public async Task<object> Insert(List<sli_workOrderList> workOrderList)
        {
            try
            {

                var context = new YourDbContext();
                //_context = context;
                //var context1= GetTableByUsername( "test");
                //workOrderList.fproductNumber
                foreach (var WList in workOrderList)
                {
                    var insert = new sli_workOrderList
                    {
                        Fproductno = maxfproductNumber.IncrementAfterLastSpecialCharacter(WList.Fproductno),
                        //Frowno = maxfproductNumber.IncrementAfterLastSpecialCharacter(WList.Fproductno),
                        Forderentryid = WList.Forderentryid,
                        Fmaterialid = WList.Fmaterialid,
                        Fworkqty = WList.Fworkqty,
                        Fworkweight = WList.Fworkweight,
                        Fnote = WList.Fnote,
                        Fworkorderliststatus = WList.Fworkorderliststatus,
                        Fsplittype = WList.Fsplittype
                    };

                    context.Sli_workOrderList.Add(insert);






                    var entityToUpdate = context.T_sal_orderEntry.FirstOrDefault(p => p.FENTRYID == Convert.ToInt32(WList.Forderentryid));
                    if (WList.Fsplittype != "样品")
                    {
                        if (entityToUpdate != null)
                        {
                            // 累加字段值
                            entityToUpdate.FWORKORDERLISTQTY += Convert.ToInt32(WList.Fworkqty);
                            entityToUpdate.FWORKORDERLISTREMAIN -= Convert.ToInt32(WList.Fworkqty);
                            // 保存更改
                            //_context.SaveChanges();
                        }
                    }
                    await context.SaveChangesAsync();
                }

                var dataSucc = new
                {
                    code = 200,
                    msg = "OK",
                    date = " "

                };
                return dataSucc;
                //return Json(new { Result = "Success", orderId = order.Id }, JsonRequestBehavior.);
            }
            catch (Exception ex)
            {
                var dataNo = new
                {
                    code = 400,
                    msg = "失败",

                    Date = ex.ToString()

                };
                return JsonConvert.SerializeObject(dataNo);
                //Console.WriteLine();
            }


        }
        [System.Web.Http.HttpPost]
        public async Task<object> Delete(int id)
        {
            try
            {
                var context = new YourDbContext();
                var entity = await context.Sli_workOrderList.FindAsync(id);
                if (entity == null)
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

                context.Sli_workOrderList.Remove(entity);
                await context.SaveChangesAsync();

                var entityToUpdate = context.T_sal_orderEntry.FirstOrDefault(p => p.FENTRYID == Convert.ToInt32(entity.Forderentryid));
                if (entity.Fsplittype != "样品")
                {
                    if (entityToUpdate != null)
                    {
                        // 累加字段值
                        entityToUpdate.FWORKORDERLISTQTY -= Convert.ToInt32(entity.Fworkqty);
                        entityToUpdate.FWORKORDERLISTREMAIN += Convert.ToInt32(entity.Fworkqty);
                        // 保存更改
                        //_context.SaveChanges();
                    }
                }

                // var data = new { Status = "Success", Message = "Data retrieved successfully", Data = new { /* actual data here */ } };
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
        //******************

        [Microsoft.AspNetCore.Mvc.HttpGet]

            public IActionResult GetWorkOrderList(
                int Page = 1,
                int PageSize = 10,
                string Fproductno=null,
                string Fbillno = null,
                DateTime? Fstartdate = null,
                DateTime? Fenddate = null,
                string Fcustomer = null,
                string Fdescription = null,
                string Fsumnumber = null)
            {
            var context = new YourDbContext();
            // 构建基础查询
            var query = from p in context.sli_workorderlist_view
                            select p;

                // 根据Fbillno过滤
                if (!string.IsNullOrEmpty(Fbillno))
                {
                    query = query.Where(q => q.Fbillno.Contains(Fbillno));
                }
            // 根据Fproductno过滤
            if (!string.IsNullOrEmpty(Fproductno))
            {
                query = query.Where(q => q.Fproductno.Contains(Fproductno));
            }

            // 根据日期区间过滤
            if (Fstartdate.HasValue && Fenddate.HasValue)
                {
                    query = query.Where(q => q.Fdate >= Fstartdate.Value && q.Fdate <= Fenddate.Value);
                }
                else if (Fstartdate.HasValue)
                {
                    query = query.Where(q => q.Fdate >= Fstartdate.Value);
                }
                else if (Fenddate.HasValue)
                {
                    query = query.Where(q => q.Fdate <= Fenddate.Value);
                }

                // 根据Fcustomer过滤
                if (!string.IsNullOrEmpty(Fcustomer))
                {
                    query = query.Where(q => q.Fcustomer.Contains(Fcustomer));
                }

                // 根据Fdescription过滤
                if (!string.IsNullOrEmpty(Fdescription))
                {
                    query = query.Where(q => q.Fdescription.Contains(Fdescription));
                }

                // 根据Fsumnumber过滤
                if (!string.IsNullOrEmpty(Fsumnumber))
                {
                    query = query.Where(q => q.Fsumnumber.Contains(Fsumnumber));
                }

                // 获取总数和总页数
                var totalCount = query.Count();
                var totalPages = (int)Math.Ceiling((double)totalCount / PageSize);

                // 分页查询
                var paginatedQuery = query
                    .OrderBy(q => q.Id) // 根据需要排序，这里以Id为例
                    .Skip((Page - 1) * PageSize)
                    .Take(PageSize);

                // 选择需要的字段（根据需要调整）
                var result = paginatedQuery.Select(a => new
                {
                    a.Id,
                    a.Fproductno,
                    a.Fbillno,
                    a.Fdate,
                    a.Fcustomer,
                    a.Fdescription,
                    a.Fsumnumber,
                    a.Fworkqty,
                    a.Fworkweight,
                    a.Fcustid,
                    a.Fcustno,
                    a.Fcustname,
                    a.Fid,
                    a.Fentryid,
                    a.Fseq,
                    a.Fqty,
                    a.B_Fnote,
                    a.Fplandeleliverydate,
                    a.Fstockqty,
                    a.B_Fmaterialid,
                    a.Fnumber,
                    a.Fname,
                    a.Fsliouterdiameter,
                    a.Fsliinnerdiameter,
                    a.Fslihight,
                    a.Fsliallowanceod,
                    a.Fsliallowanceid,
                    a.Fsliallowanceh,
                    a.Fsliweightmaterial,
                    a.Fsliweightforging,
                    a.Fsliweightgoods,
                    a.Fslidrawingno,
                    a.Fslimetal,
                    a.Fsligoodsstatus,
                    a.Fsliprocessing,
                    a.Fslidelivery,
                    a.Fsliblankmodel,
                    a.Fslipunching,
                    a.Fslitemperaturebegin,
                    a.Fslitemperatureend,
                    a.Fslimould,
                    a.Fsliroller,
                    a.Fsliheatingtimes,
                    a.Fsligrade
                    // 添加其他需要的字段
                }).ToList();

                // 构建响应
                var response = new
                {
                    code = 200,
                    msg = "操作成功",
                    data = new
                    {
                        data = result,
                        current = Page,
                        pageSize = PageSize,
                        totalCount = totalCount,
                        totalPages = totalPages
                    }
                };

             return new OkObjectResult(response);
        }
        
        // ----------------
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public IHttpActionResult GetTableOrders(int Page = 1, int PageSize = 10, string Fbillno = null, string Fcustno = null,string Fcustname = null, DateTime? Fstartdate = null, DateTime? Fenddate = null, string Fproductname = null)
            /// 用于销售订单列表查询---》workorderlist
        {
            var context = new YourDbContext();
            var query = from p in context.Sli_sal_orders_view    
                        select p;

            if (!string.IsNullOrEmpty(Fbillno))
            {
                query = query.Where(q => q.Fbillno.Contains(Fbillno));
            }

            if (!string.IsNullOrEmpty(Fcustno))
            {
                query = query.Where(q => q.Fcustno.Contains(Fcustno));
            }

            if (!string.IsNullOrEmpty(Fcustname))
            {
                query = query.Where(q => q.Fcustname.Contains(Fcustname));
            }

            if (!string.IsNullOrEmpty(Fproductname))
            {
                query = query.Where(q => q.Fname.Contains(Fproductname));
            }

            //if (Fstartdate.HasValue && Fenddate.HasValue)
            //{
            //    query = query.Where(q => q.Fdate >= Fstartdate.Value && q.Fdate <= Fenddate.Value);
            //}
            else if (Fstartdate.HasValue)
            {
                query = query.Where(q => q.Fdate >= Fstartdate.Value);
            }
            else if (Fenddate.HasValue)
            {
                query = query.Where(q => q.Fdate <= Fenddate.Value);
            }

            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / PageSize);
            var paginatedQuery = query.Skip((Page - 1) * PageSize).Take(PageSize);
            var result = paginatedQuery.Select(a => new
            {
                Fid = a.Fid,
                Fbillno = a.Fbillno,
                Forderid = a.Forderid,
                Fdate = a.Fdate,
                Fcustid = a.Fcustid,
                Fcustname = a.Fcustname,
                Fcustno = a.Fcustno,
                Fcustomer = a.Fcustomer,
                Fentryid = a.Fentryid,
                Fseq = a.Fseq,
                Fqty = a.Fqty,
                Fnote = a.Fnote,
                Fplandeliverydate = a.Fplandeliverydate,
                Fstockqty = a.Fstockqty,
                Fmaterialid = a.Fmaterialid,
                Fnumber = a.Fnumber,
                Fname = a.Fname,
                Fdescription = a.Fdescription,
                Fsliouterdiameter = a.Fsliouterdiameter,
                Fsliinnerdiameter = a.Fsliinnerdiameter,
                Fslihight = a.Fslihight,
                Fsliallowanceod = a.Fsliallowanceod,
                Fsliallowanceid = a.Fsliallowanceid,
                Fsliallowanceh = a.Fsliallowanceh,
                Fsliweightmaterial = a.Fsliweightmaterial,
                Fsliweightforging = a.Fsliweightforging,
                Fsliweightgoods = a.Fsliweightgoods,
                Fslirawingno = a.Fslidrawingno,
                Fslimetal = a.Fslimetal,
                Fsligoodsstatus = a.Fsligoodsstatus,
                Fsliprocessing = a.Fsliprocessing,
                Fsliedelivery = a.Fslidelivery,
                Fsliblankmodel = a.Fsliblankmodel,
                Fslipunching = a.Fslipunching,
                Fslitemperaturebegin = a.Fslimould,
                Fslitempratureend = a.Fsliroller,
                Fslimould = a.Fslimould,
                Fsliroller = a.Fsliroller,
                Fsliheatingtimes = a.Fsliheatingtimes,
                Fsligrade = a.Fsligrade,
                Fsumnumber = a.Fsumnumber,
                Fworkorderlistqty = a.Fworkorderlistqty,
                Fworkorderlistremain = a.Fworkorderlistremain,
                Fworkorderliststatus = a.Fworkorderliststatus
            }).ToArray();

            var response = new
            {
                code = 200,
                msg = "操作成功",
                data = new
                {
                    data = result,
                    current = Page,
                    pageSize = PageSize,
                    totalCounts = totalCount
                }
            };

            return Ok(response);
        }
    }
}
