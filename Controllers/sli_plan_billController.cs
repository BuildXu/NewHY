using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi_SY.Entity;
using WebApi_SY.Models;

namespace WebApi_SY.Controllers
{
    public class sli_plan_billController : ApiController
    {
        public sli_plan_billController()
        {
            // _context = context;

        }

        [System.Web.Http.HttpPost]
        public async Task<object> Insert([Microsoft.AspNetCore.Mvc.FromBody] sli_plan_bill bill)
        {



            try
            {
                using (var context = new YourDbContext())
                {
                    // 创建并配置 sli_plan_bill 实例
                    var header = new sli_plan_bill
                    {
                        Fplanlnumber = bill.Fplanlnumber,
                        Fissueddate = bill.Fissueddate,
                        Fplancontractentry = bill.Fplancontractentry,
                        Fqty = bill.Fqty,
                        Fweight = bill.Fweight,
                        Fplanbegindate = bill.Fplanbegindate,
                        Fplanenddate = bill.Fplanenddate,
                        Factualbegindate = bill.Factualbegindate,
                        Factualenddate = bill.Factualenddate,
                        Fnote = bill.Fnote,
                        Fdays = bill.Fdays,
                        sli_plan_billEntry = bill.sli_plan_billEntry.Select(d => new sli_plan_billEntry
                        {
                            Fplanoptionidid = d.Fplanoptionidid,
                            Fqty = d.Fqty,
                            Fweight = d.Fweight,
                            Fplanstartdate = d.Fplanstartdate,
                            Fplanenddate = d.Fplanenddate,
                            Factualstartdate = d.Factualstartdate,
                            Factualenddate = d.Factualenddate,
                            Fplandays = d.Fplandays,
                            Fcapacity = d.Fcapacity,
                            Fdepartid = d.Fdepartid,
                            Fempid = d.Fempid
                        }).ToList(),
                        sli_plan_billorder = bill.sli_plan_billorder.Select(o => new sli_plan_billorder
                        {
                            Fplanbillid = o.Id, // 设置外键
                            Forderentryid = o.Forderentryid, // 根据实际属性设置
                                                             //ForderDate = o.ForderDate // 根据实际属性设置
                                                             // 设置其他属性...
                        }).ToList()
                    };

                    context.Sli_plan_bill.Add(header);
                    await context.SaveChangesAsync();

                    var dataNull = new
                    {
                        code = 200,
                        msg = "Success",
                        planid = header.Id,
                        Date = $"{header.Id} 保存成功"
                    };
                    return dataNull;
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex.ToString());
            }




        }


        [System.Web.Http.HttpPost]
        public async Task<object> Delete(int id)
        {
            try
            {
                var context = new YourDbContext();
                var entity = await context.Sli_plan_bill.FindAsync(id);
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
                var Sli_plan_billEntrys = context.Sli_plan_billEntry.Where(b => b.Fplanbillid == id);
                context.Sli_plan_billEntry.RemoveRange(Sli_plan_billEntrys);
                context.Sli_plan_bill.Remove(entity);
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

        [System.Web.Http.HttpPost]
        public async Task<IActionResult> Update(sli_plan_bill bill)
        {
            var context = new YourDbContext();
            if (context.Entry(bill).State == Microsoft.EntityFrameworkCore.EntityState.Detached)
            {
                context.Attach(bill);
            }
            context.Entry(bill).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
            return new NoContentResult();

        }


        [Microsoft.AspNetCore.Mvc.HttpGet]
        //[Microsoft.AspNetCore.Mvc.Route("api/user/GetTableByUsername/{username}")]
        public IHttpActionResult GetTable(int ? id  =null)
        {
            var context = new YourDbContext();

            var query = from p in context.Sli_plan_bill
                        join c in context.Sli_plan_billlEntry on p.Id equals c.Fplanbillid
                        select new
                        {
                            Header = p,
                            Entries = p.sli_plan_billEntry.DefaultIfEmpty(),
                            Orders = p.sli_plan_billorder.DefaultIfEmpty()
                        };
            if (id.HasValue)
            {
                query = query.Where(p =>id == id.Value);
            }

            //var planBills = query
            //  .Include("sli_plan_billEntry")
            //  .Include("sli_plan_billorder")
            //  .ToList();

            //var planBills = query.ToList();
            var result = new List<object>();
            foreach (var bill in query)
            {
                var billData = new
                {
                    Id = bill.Header.Id,
                    Fplanlnumber = bill.Header.Fplanlnumber ?? "",
                    Fissueddate = bill.Header.Fissueddate ?? "",
                    Fplancontractentry = bill.Header.Fplancontractentry,
                    Fqty = bill.Header.Fqty,
                    Fweight = bill.Header.Fweight,
                    Fplanbegindate = bill.Header.Fplanbegindate ?? "",
                    Fplanenddate = bill.Header.Fplanenddate ?? "",
                    Factualbegindate = bill.Header.Factualbegindate ?? "",
                    Factualenddate = bill.Header.Factualenddate ?? "",
                    Fnote = bill.Header.Fnote ?? "",
                    Fdays = bill.Header.Fdays,
                    sli_plan_billEntry = bill.Entries?.Select(entry => new
                    {
                        Id = entry.Id,
                        Fplanbillid = entry.Fplanbillid,
                        Fplanoptionidid = entry.Fplanoptionidid,
                        Fqty = entry.Fqty,
                        Fweight = entry.Fweight,
                        Fplanstartdate = entry.Fplanstartdate ?? "",
                        Fplanenddate = entry.Fplanenddate ?? "",
                        Factualstartdate = entry.Factualstartdate ?? "",
                        Factualenddate = entry.Factualenddate ?? "",
                        Fplandays = entry.Fplandays,
                        Fcapacity = entry.Fcapacity,
                        Fdepartid = entry.Fdepartid,
                        Fempid = entry.Fempid
                    }),
                    sli_plan_billorder = bill.Orders?.Select(order => new
                    {
                        Id = order.Id,
                        Fplanbillid = order.Fplanbillid,
                        
                        Forderentryid = order.Forderentryid,
                        Fstatus = order.Fstatus
                    })
                };
                result.Add(billData);
            }
            var response = new
            {
                code = 200,
                msg = "操作成功",
                data = new
                {
                   
                    data = result
                }
            };

            return Ok(response);
            //}
            // else
            // {
            // return NotFound();
            // }
        }


        [Microsoft.AspNetCore.Mvc.HttpGet]
        //[Microsoft.AspNetCore.Mvc.Route("api/user/GetTableByUsername/{username}")]
        public IHttpActionResult GetTableHeader(int page = 1, int pageSize = 10)
        {
            var context = new YourDbContext();
            IQueryable<sli_plan_bill>  query = context.Sli_plan_bill;
            //if (id.HasValue)
            //{
            //    query = query.Where(q => q.Id == id.Value);
            //}

            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedQuery = query.Skip((page - 1) * pageSize).Take(pageSize);
            //var datas = query.ToList();
            //var datas = query.ToList();
            var response = new    // 定义 前端返回数据  总记录，总页，当前页 ，size,返回记录
            {
                code = 200,
                msg = "OK",
                totalCounts = totalCount,
                totalPagess = totalPages,
                currentPages = page,
                pageSizes = pageSize,
                data = paginatedQuery
            };

            return Json(response);

        }

    }


}