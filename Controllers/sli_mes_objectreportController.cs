using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi_SY.Entity;
using WebApi_SY.Models;

namespace WebApi_SY.Controllers
{
    public class sli_mes_objectreportController : ApiController
    {
        public sli_mes_objectreportController()
        {
            // _context = context;
        }

        [System.Web.Http.HttpPost]
        public async Task<object> sli_mes_objectreport_Insert([Microsoft.AspNetCore.Mvc.FromBody] sli_mes_objectreport[] options)
        {
            var context = new YourDbContext();
            try
            {
                foreach (var option in options)
                {
                    var header = new sli_mes_objectreport
                    {
                        Fsourceid = option.Fsourceid,
                        Fobjectid = option.Fobjectid,
                        Fqty = option.Fqty,
                        Fweight = option.Fweight,
                        Fcommitqty = option.Fcommitqty,
                        Fpassqty = option.Fpassqty,
                        Fempid = option.Fempid,
                        Fdeptid = option.Fdeptid,
                        Fbiller = option.Fbiller,
                        Fdate = option.Fdate
                    };
                    context.sli_mes_objectreport.Add(header);
                }
                await context.SaveChangesAsync();
                var datas = new
                {
                    code = 200,
                    msg = "Success",
                    Date = options.Length > 0 ? options[0].Id.ToString() + "等保存成功" : "无记录保存成功"
                };
                return datas;
            }
            catch (Exception ex)
            {
                var dataerr = new
                {
                    code = 500,
                    msg = "失败",
                    Date = ex.ToString()
                };
                return dataerr;
            }
        }

        [System.Web.Http.HttpPost]
        public async Task<object> sli_mes_objectreportUpdate([Microsoft.AspNetCore.Mvc.FromBody] sli_mes_objectreport option)
        {
            try
            {
                var context = new YourDbContext();
                var entity = await context.sli_mes_objectreport.FindAsync(option.Id);
                if (entity == null)
                {
                    var dataNull = new
                    {
                        code = 200,
                        msg = "ok",
                        date = "修改记录不存在"
                    };
                    //string json = JsonConvert.SerializeObject(data);
                    return dataNull;
                }
                else
                {
                    var sli_mes_objectreport = context.sli_mes_objectreport.FirstOrDefault(p => p.Id == option.Id);


                    sli_mes_objectreport.Fsourceid = option.Fsourceid;
                    sli_mes_objectreport.Fobjectid = option.Fobjectid;
                    sli_mes_objectreport.Fqty = option.Fqty;
                    sli_mes_objectreport.Fweight = option.Fweight;
                    sli_mes_objectreport.Fcommitqty = option.Fcommitqty;
                    sli_mes_objectreport.Fpassqty = option.Fpassqty;
                    sli_mes_objectreport.Fempid = option.Fempid;
                    sli_mes_objectreport.Fdeptid = option.Fdeptid;
                    sli_mes_objectreport.Fbiller = option.Fbiller;
                    sli_mes_objectreport.Fdate = option.Fdate;


                    await context.SaveChangesAsync();

                    var datas = new
                    {
                        code = 200,
                        msg = "ok",
                        date = sli_mes_objectreport.Id + "修改成功！"
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

        [System.Web.Http.HttpPost]
        public async Task<object> sli_mes_objectreport_Delete(List<int> id)
        {
            try
            {
                var context = new YourDbContext();
                foreach (var deleteid in id)
                {
                    var entity = await context.sli_mes_objectreport.FindAsync(deleteid);
                    if (entity == null)
                    {
                        var dataNull = new
                        {
                            code = 200,
                            msg = "ok",
                            Id = id.ToString(),
                            date = id.ToString() + "不存在"
                        };
                        return dataNull;
                    }
                    context.sli_mes_objectreport.Remove(entity);
                }
                await context.SaveChangesAsync();
                var data = new
                {
                    code = 200,
                    msg = "Success",
                    date = "删除成功"
                };
                return data;
            }
            catch (Exception ex)
            {
                var data = new
                {
                    code = 400,
                    msg = "失败",
                    date = ex.ToString()
                };
                return data;
            }
        }

        [System.Web.Http.HttpGet]
        public IHttpActionResult GetTableBySli_mes_option(int? Id = null, int? Fsourceid = null, int? Fobjectid = null, float? Fqty = null, float? Fweight = null, float? Fcommitqty = null, float? Fpassqty = null, int? Fempid = null, int? Fdeptid = null, int? Fbiller = null, DateTime? Fdate = null)
        {
            var context = new YourDbContext();
            IQueryable<sli_mes_objectreport> query = context.sli_mes_objectreport;

            if (Id.HasValue)
            {
                query = query.Where(q => q.Id == Id);
            }
            if (Fsourceid.HasValue)
            {
                query = query.Where(q => q.Fsourceid == Fsourceid);
            }
            if (Fobjectid.HasValue)
            {
                query = query.Where(q => q.Fobjectid == Fobjectid);
            }
            if (Fqty.HasValue)
            {
                query = query.Where(q => q.Fqty == Fqty);
            }
            if (Fweight.HasValue)
            {
                query = query.Where(q => q.Fweight == Fweight);
            }
            if (Fcommitqty.HasValue)
            {
                query = query.Where(q => q.Fcommitqty == Fcommitqty);
            }
            if (Fpassqty.HasValue)
            {
                query = query.Where(q => q.Fpassqty == Fpassqty);
            }
            if (Fempid.HasValue)
            {
                query = query.Where(q => q.Fempid == Fempid);
            }
            if (Fdeptid.HasValue)
            {
                query = query.Where(q => q.Fdeptid == Fdeptid);
            }
            if (Fbiller.HasValue)
            {
                query = query.Where(q => q.Fbiller == Fbiller);
            }
            if (Fdate.HasValue)
            {
                query = query.Where(q => q.Fdate == Fdate);
            }
            //var totalCount = query.Count(); //记录数
            //var totalPages = (int)Math.Ceiling((double)totalCount / pageSize); // 页数
            //var paginatedQuery = query.OrderByDescending(b => b.Id).Skip((page - 1) * pageSize).Take(pageSize); //  某页记录
            //var datas = query.ToList();
            var response = new    // 定义 前端返回数据  总记录，总页，当前页 ，size,返回记录
            {
                code = 200,
                msg = "OK",
                data = new
                {
                    //totalCounts = totalCount,
                    //totalPagess = totalPages,
                    //currentPages = page,
                    //pageSizes = pageSize,
                    data = query
                }
            };

            return Json(response);
        }
    }
}