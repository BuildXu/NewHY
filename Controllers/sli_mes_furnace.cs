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
    public class sli_mes_furnaceController : ApiController
    {
        public sli_mes_furnaceController()
        {
            // _context = context;
        }

        [System.Web.Http.HttpPost]
        public async Task<object> sli_mes_furnace_Insert([Microsoft.AspNetCore.Mvc.FromBody] sli_mes_furnace[] options)
        {
            var context = new YourDbContext();
            try
            {
                foreach (var option in options)
                {
                    var header = new sli_mes_furnace
                    {
                        Fnumber = option.Fnumber,
                        //Id = option.Id,
                        Fworkorderlistid = option.Fworkorderlistid,
                        Fsourceid = option.Fsourceid,
                        Foptionid = option.Foptionid,
                        Fqty = option.Fqty,
                        Fweight = option.Fweight,
                        Ffurnaceno = option.Ffurnaceno,
                        Fheatingno = option.Fheatingno,
                        Fempid = option.Fempid,
                        Fdeptid = option.Fdeptid,
                        Fbiller = option.Fbiller,
                        Fdate = option.Fdate
                    };
                    context.Sli_mes_furnace.Add(header);
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
        public async Task<object> sli_mes_furnaceUpdate([Microsoft.AspNetCore.Mvc.FromBody] sli_mes_furnace option)
        {
            try
            {
                var context = new YourDbContext();
                var entity = await context.Sli_mes_furnace.FindAsync(option.Id);
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
                    var sli_mes_furnace = context.Sli_mes_furnace.FirstOrDefault(p => p.Id == option.Id);


                    sli_mes_furnace.Fnumber = option.Fnumber;
                    //sli_mes_furnace.Id = option.Id;
                    sli_mes_furnace.Fworkorderlistid = option.Fworkorderlistid;
                    sli_mes_furnace.Fsourceid = option.Fsourceid;
                    sli_mes_furnace.Foptionid = option.Foptionid;
                    sli_mes_furnace.Fqty = option.Fqty;
                    sli_mes_furnace.Fweight = option.Fweight;
                    sli_mes_furnace.Ffurnaceno = option.Ffurnaceno;
                    sli_mes_furnace.Fheatingno = option.Fheatingno;
                    sli_mes_furnace.Fempid = option.Fempid;
                    sli_mes_furnace.Fdeptid = option.Fdeptid;
                    sli_mes_furnace.Fbiller = option.Fbiller;
                    sli_mes_furnace.Fdate = option.Fdate;


                    await context.SaveChangesAsync();

                    var datas = new
                    {
                        code = 200,
                        msg = "ok",
                        date = sli_mes_furnace.Id + "修改成功！"
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
        public async Task<object> sli_mes_furnace_Delete(List<int> id)
        {
            try
            {
                var context = new YourDbContext();
                foreach (var deleteid in id)
                {
                    var entity = await context.Sli_mes_furnace.FindAsync(deleteid);
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
                    context.Sli_mes_furnace.Remove(entity);
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
        public IHttpActionResult GetTableBySli_mes_furnace(int page = 1, int pageSize = 10 )
            //string Fnumber = null, int? Fworkorderlistid = null, int? Fsourceid = null, int? Foptionid = null, float? Fqty = null, float? Fweight = null, string Ffurnaceno = null, string Fheatingno = null, int? Fempid = null, int? Fdeptid = null, int? Fbiller = null, DateTime? Fdate = null
        {
            var context = new YourDbContext();
            var query = context.Sli_mes_furnace_view;
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
                Fnumber = a.Fnumber,
                Id = a.Id,
                Fworkorderlistid = a.Fworkorderlistid,
                Fsourceid = a.Fsourceid,
                Fcustno = a.Fcustno,
                Fcustname = a.Fcustname,
                Fname = a.Fname,
                Fqty = a.Fqty,
                Fweight = a.Fweight,
                Foptionid = a.Foptionid,
                Foptionno = a.Foptionno,
                Foptionname = a.Foptionname,
                Ffurnaceno = a.Ffurnaceno,
                Fheatingno = a.Fheatingno,
                Fempid = a.Fempid,
                Fdeptid = a.Fdeptid,
                Fbiller = a.Fbiller,
                Fdate = a.Fdate,
                Fdept_name = a.Fdept_name,
                Femp_name = a.Femp_name

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



        [System.Web.Http.HttpGet]
        public IHttpActionResult GetTableBySli_mes_furnace_view(
           string Fcustno = null,
           string Fcustname = null,
           string Fname = null,
           string Fnumber = null,
           int? Id = null,
           int? Fworkorderlistid = null,
           int? Fsourceid = null,
           int? Fobjectid = null,
           float? Fqty = null,
           float? Fweight = null,
           string Ffurnaceno = null,
           string Fheatingno = null,
           int? Fempid = null,
           int? Fdeptid = null,
           int? Fbiller = null,
           DateTime? Fdate = null,
           string Fobjectno = null,
           string Fobjectname = null)
        {
            var context = new YourDbContext();
            IQueryable<sli_mes_furnace_view> query = context.sli_mes_furnace_view;

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
            if (!string.IsNullOrEmpty(Fnumber))
            {
                query = query.Where(q => q.Fnumber == Fnumber);
            }
            if (Id.HasValue)
            {
                query = query.Where(q => q.Id == Id);
            }
            if (Fworkorderlistid.HasValue)
            {
                query = query.Where(q => q.Fworkorderlistid == Fworkorderlistid);
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
            if (!string.IsNullOrEmpty(Ffurnaceno))
            {
                query = query.Where(q => q.Ffurnaceno == Ffurnaceno);
            }
            if (!string.IsNullOrEmpty(Fheatingno))
            {
                query = query.Where(q => q.Fheatingno == Fheatingno);
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
            if (!string.IsNullOrEmpty(Fobjectno))
            {
                query = query.Where(q => q.Fobjectno == Fobjectno);
            }
            if (!string.IsNullOrEmpty(Fobjectname))
            {
                query = query.Where(q => q.Fobjectname == Fobjectname);
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