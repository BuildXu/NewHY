using Kingdee.BOS.WebApi.DataEntities;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebApi_SY.Controllers
{
    public class sli_workorderController : ApiController
    {
        public sli_workorderController()
        {
            // _context = context;

        }
        [System.Web.Http.HttpPost]
        public async Task<object> Insert([Microsoft.AspNetCore.Mvc.FromBody] sli_work_order model)
        {
            try
            {
                var context = new YourDbContext();
                var header = new sli_work_order
                {
                    Fbillno = model.Fbillno,
                    Fdate = model.Fdate,
                    Fqty = model.Fqty,
                    Fweight = model.Fweight,
                    Fplanstart = model.Fplanstart,
                    Fplanend = model.Fplanend,
                    Fordertype = model.Fordertype,
                    sli_work_orderEntry = model.sli_work_orderEntry.Select(d => new sli_work_orderEntry
                    {
                        Id = model.Id,
                        Fseq = d.Fseq,
                        Forderentryid = d.Forderentryid,
                        Forderid = d.Forderid,
                        Fworkorderlistid = d.Fworkorderlistid,

                    }).ToList()
                };



                context.Sli_work_order.Add(header);
                await context.SaveChangesAsync();
                var dataNull = new
                {
                    code=200,
                    msg = "Success",
                    modelid = header.Id,
                    Date = header.Id.ToString() + "保存成功"

                };
                return dataNull;
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex.ToString());
            }


        }

        public IHttpActionResult GetTableWorkorder(int page = 1, int pageSize = 10)
        {
            var context = new YourDbContext();

            var query = context.Sli_work_order.Include(a => a.sli_work_orderEntry);
            //var query = from p in context.Sli_work_order
            //            join c in context.Sli_work_orderEntry on p.Id equals c.Id
            //            select new
            //            {
            //                Sli_work_order = p,
            //                Sli_work_orderEntry = c
            //            };

            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedQuery = query.OrderByDescending(b => b.Id).Skip((page - 1) * pageSize).Take(pageSize);
            var result = paginatedQuery.Select(a => new
            {
                id = a.Id,
                Fbillno = a.Fbillno,
                Fdate = a.Fdate,
                Fqty = a.Fqty,
                Fweight = a.Fweight,
                Fplanstart = a.Fplanstart,
                Fplanend = a.Fplanend,
                Fordertype = a.Fordertype,
                Sli_plan_modelEntry = a.sli_work_orderEntry.Select(b => new
                {
                    Fentryid = b.Fentryid,
                    Id = b.Id,
                    Fworkorderlistid = b.Fworkorderlistid,
                    Fseq = b.Fseq,
                    Fqty = b.Fqty,
                    Fcommitqty = b.Fcommitqty,
                    Forderid = b.Forderid,
                    Forderentryid = b.Forderentryid,
                    Fstatus = b.Fstatus,
                    Fclosed = b.Fclosed,
                })

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
    }
}