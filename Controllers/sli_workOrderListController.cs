using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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
                        fproductNumber = maxfproductNumber.IncrementAfterLastSpecialCharacter(WList.fproductNumber),
                        forderEntryid = WList.forderEntryid,
                        fmaterialid = WList.fmaterialid,
                        fworkQty = WList.fworkQty,
                        fworkWeight = WList.fworkWeight,
                        fnote = WList.fnote,
                        fworkOrderListStatus = WList.fworkOrderListStatus,
                        splittype = WList.splittype
                    };

                    context.Sli_workOrderList.Add(insert);






                    var entityToUpdate = context.T_sal_orderEntry.FirstOrDefault(p => p.FENTRYID == Convert.ToInt32(WList.forderEntryid));
                    if (WList.splittype != "样品")
                    {
                        if (entityToUpdate != null)
                        {
                            // 累加字段值
                            entityToUpdate.FWORKORDERLISTQTY += Convert.ToInt32(WList.fworkQty);
                            entityToUpdate.FWORKORDERLISTREMAIN -= Convert.ToInt32(WList.fworkQty);
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

                var entityToUpdate = context.T_sal_orderEntry.FirstOrDefault(p => p.FENTRYID == Convert.ToInt32(entity.forderEntryid));
                if (entity.splittype != "样品")
                {
                    if (entityToUpdate != null)
                    {
                        // 累加字段值
                        entityToUpdate.FWORKORDERLISTQTY -= Convert.ToInt32(entity.fworkQty);
                        entityToUpdate.FWORKORDERLISTREMAIN += Convert.ToInt32(entity.fworkQty);
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
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public IHttpActionResult GetTableOrders(int Page = 1, int PageSize = 10, string Fbillno = null, string Fcustno = null,
    string Fcustname = null, DateTime? Fstartdate = null, DateTime? Fenddate = null, string fProductName = null)
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

            if (!string.IsNullOrEmpty(fProductName))
            {
                query = query.Where(q => q.Fname.Contains(fProductName));
            }

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
                totalCounts = totalCount,
                totalPages = totalPages,
                currentPage = Page,
                pageSize = PageSize,
                data = result
            };

            return Ok(response);
        }
    }
}
