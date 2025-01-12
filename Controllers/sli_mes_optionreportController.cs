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
    public class sli_mes_optionreportController : ApiController
    {
        public sli_mes_optionreportController()
        {
            // _context = context;
        }

        [System.Web.Http.HttpPost]
        public async Task<object> sli_mes_optionreport_Insert([Microsoft.AspNetCore.Mvc.FromBody] sli_mes_optionreport[] options)
        {
            var context = new YourDbContext();
            try
            {
                foreach (var option in options)
                {
                    var header = new sli_mes_optionreport
                    {
                        Fnumber = option.Fnumber,
                        Fsourceid = option.Fsourceid,
                        Fworkorderlistid = option.Fworkorderlistid,
                        Fprocessoption = option.Fprocessoption,
                        Fqty = option.Fqty,
                        Fweight = option.Fweight,
                        Fcommitqty = option.Fcommitqty,
                        Fpassqty = option.Fpassqty,
                        Fempid = option.Fempid,
                        Fdeptid = option.Fdeptid,
                        Fbiller = option.Fbiller,
                        Fdate = option.Fdate
                    };
                    context.sli_mes_optionreport.Add(header);
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
        public async Task<object> sli_mes_optionreportUpdate([Microsoft.AspNetCore.Mvc.FromBody] sli_mes_optionreport option)
        {
            try
            {
                var context = new YourDbContext();
                var entity = await context.sli_mes_optionreport.FindAsync(option.Id);
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
                    var sli_mes_optionreport = context.sli_mes_optionreport.FirstOrDefault(p => p.Id == option.Id);


                    sli_mes_optionreport.Fsourceid = option.Fsourceid;
                    sli_mes_optionreport.Fprocessoption = option.Fprocessoption;
                    sli_mes_optionreport.Fqty = option.Fqty;
                    sli_mes_optionreport.Fweight = option.Fweight;
                    sli_mes_optionreport.Fcommitqty = option.Fcommitqty;
                    sli_mes_optionreport.Fpassqty = option.Fpassqty;
                    sli_mes_optionreport.Fempid = option.Fempid;
                    sli_mes_optionreport.Fdeptid = option.Fdeptid;
                    sli_mes_optionreport.Fbiller = option.Fbiller;
                    sli_mes_optionreport.Fdate = option.Fdate;


                    await context.SaveChangesAsync();

                    var datas = new
                    {
                        code = 200,
                        msg = "ok",
                        date = sli_mes_optionreport.Id + "修改成功！"
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
        public async Task<object> sli_mes_optionreport_Delete(List<int> id)
        {
            try
            {
                var context = new YourDbContext();
                foreach (var deleteid in id)
                {
                    var entity = await context.sli_mes_optionreport.FindAsync(deleteid);
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
                    context.sli_mes_optionreport.Remove(entity);
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
        public IHttpActionResult GetTableBySli_mes_option(int page = 1, int pageSize = 10)
        {
            //int? Id = null, int? Fsourceid = null, int? Fprocessoption = null, decimal? Fqty = null, decimal? Fweight = null, decimal? Fcommitqty = null, decimal? Fpassqty = null, int? Fempid = null, int? Fdeptid = null, int? Fbiller = null, DateTime? Fdate = null
            var context = new YourDbContext();
            IQueryable<sli_mes_optionreport_view> query = context.sli_mes_optionreport_view;

            //if (Id.HasValue)
            //{
            //    query = query.Where(q => q.Id == Id);
            //}
            //if (Fsourceid.HasValue)
            //{
            //    query = query.Where(q => q.Fsourceid == Fsourceid);
            //}
            //if (Fprocessoption.HasValue)
            //{
            //    query = query.Where(q => q.Fprocessoption == Fprocessoption);
            //}
            //if (Fqty.HasValue)
            //{
            //    query = query.Where(q => q.Fqty == Fqty);
            //}
            //if (Fweight.HasValue)
            //{
            //    query = query.Where(q => q.Fweight == Fweight);
            //}
            //if (Fcommitqty.HasValue)
            //{
            //    query = query.Where(q => q.Fcommitqty == Fcommitqty);
            //}
            //if (Fpassqty.HasValue)
            //{
            //    query = query.Where(q => q.Fpassqty == Fpassqty);
            //}
            //if (Fempid.HasValue)
            //{
            //    query = query.Where(q => q.Fempid == Fempid);
            //}
            //if (Fdeptid.HasValue)
            //{
            //    query = query.Where(q => q.Fdeptid == Fdeptid);
            //}
            //if (Fbiller.HasValue)
            //{
            //    query = query.Where(q => q.Fbiller == Fbiller);
            //}
            //if (Fdate.HasValue)
            //{
            //    query = query.Where(q => q.Fdate == Fdate);
            //}
            var totalCount = query.Count(); //记录数
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize); // 页数
            var paginatedQuery = query.OrderByDescending(b => b.Id).Skip((page - 1) * pageSize).Take(pageSize); //  某页记录
            
            var result = paginatedQuery.Select(a => new
            {
                Fcustno = a.Fcustno,
                Fcustname = a.Fcustname,
                Fname = a.Fname,
                Id = a.Id,
                Fworkorderlistid = a.Fworkorderlistid,
                Fsourceid = a.Fsourceid,
                Fprocessoption = a.Fprocessoption,
                Fqty = a.Fqty,
                Fweight = a.Fweight,
                Fcommitqty = a.Fcommitqty,
                Fpassqty = a.Fpassqty,
                Fempid = a.Fempid,
                Fdeptid = a.Fdeptid,
                Fbiller = a.Fbiller,
                Fdate = a.Fdate,
                Foptionno = a.Foptionno,
                Foptionname = a.Foptionname,
                Fdept_name = a.Fdept_name ?? string.Empty,
                Femp_name = a.Femp_name ?? string.Empty

            });
            //var datas = query.ToList();
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
                    data = query
                }
            };

            return Json(response);
        }
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetTableBySli_mes_option_view(
    int? Id = null,
    int? Fsourceid = null,
    int? Fprocessoption = null,
    decimal? Fqty = null,
    decimal? Fweight = null,
    decimal? Fcommitqty = null,
    decimal? Fpassqty = null,
    int? Fempid = null,
    int? Fdeptid = null,
    int? Fbiller = null,
    DateTime? Fdate = null,
    string Fcustno = null,
    string Fcustname = null,
    string Fname = null,
    int? Fworkorderlistid = null,
    string Foptionno = null,
    string Foptionname = null)
        {
            var context = new YourDbContext();
            IQueryable<sli_mes_optionreport_view> query = context.sli_mes_optionreport_view;

            if (Id.HasValue)
            {
                query = query.Where(q => q.Id == Id);
            }
            if (Fsourceid.HasValue)
            {
                query = query.Where(q => q.Fsourceid == Fsourceid);
            }
            if (Fprocessoption.HasValue)
            {
                query = query.Where(q => q.Fprocessoption == Fprocessoption);
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
            if (!string.IsNullOrEmpty(Fcustno))
            {
                query = query.Where(q => q.Fcustno == Fcustno);
            }
            if (!string.IsNullOrEmpty(Fcustname))
            {
                query = query.Where(q => q.Fcustname == Fcustname);
            }
            if (!string.IsNullOrEmpty(Fname))
            {
                query = query.Where(q => q.Fname == Fname);
            }
            if (Fworkorderlistid.HasValue)
            {
                query = query.Where(q => q.Fworkorderlistid == Fworkorderlistid);
            }
            if (!string.IsNullOrEmpty(Foptionno))
            {
                query = query.Where(q => q.Foptionno == Foptionno);
            }
            if (!string.IsNullOrEmpty(Foptionname))
            {
                query = query.Where(q => q.Foptionname == Foptionname);
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