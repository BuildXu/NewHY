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
    public class sli_witnessing_orderController : ApiController
    {
        public sli_witnessing_orderController()
        {
            // _context = context;
        }

        [System.Web.Http.HttpPost]
        public async Task<object> sli_witnessing_order_Insert([Microsoft.AspNetCore.Mvc.FromBody] sli_witnessing_orderbill_view[] options)
        {
            var context = new YourDbContext();
            try
            {
                foreach (var option in options)
                {
                    var header = new sli_witnessing_order
                    {
                        Fnumber = option.Fnumber,
                        Fdate = option.Fdate,
                        Fnote = option.Fnote,
                        Fstatus = option.Fstatus,
                        sli_witnessing_orderbill = new List<sli_witnessing_orderbill>()
                    };
                    header.sli_witnessing_orderbill.Add(new sli_witnessing_orderbill
                    {
                        Id = header.Id,
                        Fstartdate = option.Fstartdate,
                        Fenddate = option.Fenddate,
                        Fobject = option.Fobject,
                        Fnote = option.Fnote,
                        Fworkorderlistid = option.Fworkorderlistid,
                        Fstatus = option.Fstatus
                    });
                            //i++;

                    context.Sli_witnessing_order.Add(header);
                }
                await context.SaveChangesAsync();
                var dataNull = new
                {
                    code = 200,
                    msg = "Success",
                    //modelid = header.Id,
                    Date =  "保存成功"

                };
                return dataNull;
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex.ToString());
            }
        }

        [System.Web.Http.HttpPost]
        public async Task<object> sli_witnessing_order_Update(sli_witnessing_orderbill_view bill)
        {
            try
            {
                var context = new YourDbContext();
                var entity = await context.Sli_witnessing_order.FindAsync(bill.Id);
                if (entity == null)
                {
                    var dataNull = new
                    {
                        code = 200,
                        msg = "ok",
                        date = "修改记录不存在"
                    };
                    return dataNull;
                }
                else
                {

                    var Sli_witnessing_order = context.Sli_witnessing_order.FirstOrDefault(p => p.Id == bill.Id);
                    var Sli_witnessing_orderbill = context.Sli_witnessing_orderbill.Where(p => p.Id == bill.Id).ToList();

                    Sli_witnessing_order.Fnumber = bill.Fnumber;
                    Sli_witnessing_order.Fdate = bill.Fdate;
                    Sli_witnessing_order.Fnote = bill.Fnote;
                    Sli_witnessing_order.Fstatus = bill.Fstatus;
                    context.Sli_witnessing_orderbill.RemoveRange(Sli_witnessing_orderbill);
                    var entry = new sli_witnessing_orderbill
                    {
                        Id = bill.Id,
                        Fstartdate = bill.Fstartdate,
                        Fenddate = bill.Fenddate,
                        Fobject = bill.Fobject,   //待确认是否传ID
                        Fnote = bill.Fnote,
                        Fworkorderlistid = bill.Fworkorderlistid,
                        Fstatus = bill.Fstatus

                    };
                    context.Sli_witnessing_orderbill.Add(entry);
                        //i++;
                    

                    await context.SaveChangesAsync();

                    var datas = new
                    {
                        code = 200,
                        msg = "ok",
                        date = bill.Id + "更新成功！"
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

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public IHttpActionResult GetTableHeader(int page = 1, int pageSize = 10)
        {
            var context = new YourDbContext();
            IQueryable<sli_witnessing_orderbill_view> query = context.Sli_witnessing_orderbill_view;
            //if (id.HasValue)
            //{
            //    query = query.Where(q => q.Id == id.Value);
            //}

            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedQuery = query.OrderByDescending(b => b.Id).Skip((page - 1) * pageSize).Take(pageSize);
            //var datas = query.ToList();
            //var datas = query.ToList();
            var result = paginatedQuery.Select(a => new
            {
                Fnumber = a.Fnumber,
                Fdate = a.Fdate,
                Fproductno = a.Fproductno,
                Fcustname = a.Fcustname,
                Fcustomer = a.Fcustomer,
                Fmaterial = a.Fmaterial,
                Fdescription = a.Fdescription,
                Fentryid = a.Fentryid,
                Id = a.Id,
                Fstartdate = a.Fstartdate,
                Fenddate = a.Fenddate,
                Fobject = a.Fobject,
                Fnote = a.Fnote,
                Fworkorderlistid = a.Fworkorderlistid,
                Fstatus = a.Fstatus,
                Fobjectname = a.Fobjectname


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

            return Json(response);

        }

        [System.Web.Http.HttpPost]
        public async Task<object> sli_witnessing_order_Delete(List<int> id)
        {
            try
            {
                var context = new YourDbContext();
                var headersToDelete = context.Sli_witnessing_order.Where(h => id.Contains(h.Id)).ToList();
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
                context.Sli_witnessing_order.RemoveRange(headersToDelete);

                foreach (var DeleteID in id)
                {
                    var Sli_witnessing_orderbill = context.Sli_witnessing_orderbill.Where(b => b.Id == DeleteID);
                    context.Sli_witnessing_orderbill.RemoveRange(Sli_witnessing_orderbill);
                }

                await context.SaveChangesAsync();
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

    }
}