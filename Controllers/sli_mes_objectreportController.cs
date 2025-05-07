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
{  //工步报工
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
                        Fnumber = option.Fnumber,
                        Fsourceid = option.Fsourceid,
                        Fworkorderlistid = option.Fworkorderlistid,
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
                    context.Sli_mes_objectreport.Add(header);
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
                var entity = await context.Sli_mes_objectreport.FindAsync(option.Id);
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
                    var sli_mes_objectreport = context.Sli_mes_objectreport.FirstOrDefault(p => p.Id == option.Id);


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
                    var entity = await context.Sli_mes_objectreport.FindAsync(deleteid);
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
                    context.Sli_mes_objectreport.Remove(entity);
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
        public IHttpActionResult GetTableBySli_mes_option_view(
      int page = 1,
      int pageSize = 10,
      string Fwobillno = null,
      string Fcustno = null,      // 新增参数1
      string Fcustname = null,    // 新增参数2
      string Fname = null       // 新增参数3
                                //string Fnumber = null       // 新增参数4
  )
        {
            var context = new YourDbContext();
            IQueryable<sli_mes_objectreport_view> query = context.Sli_mes_objectreport_view;

            // 1. 过滤条件：Fwobillno
            if (!string.IsNullOrEmpty(Fwobillno))
            {
                query = query.Where(q => q.Fwobillno == Fwobillno);
            }

            // 2. 新增过滤条件：Fcustno
            if (!string.IsNullOrEmpty(Fcustno))
            {
                query = query.Where(q => q.Fcustno == Fcustno);
            }

            // 3. 新增过滤条件：Fcustname
            if (!string.IsNullOrEmpty(Fcustname))
            {
                query = query.Where(q => q.Fcustname == Fcustname);
            }

            // 4. 新增过滤条件：Fname
            if (!string.IsNullOrEmpty(Fname))
            {
                query = query.Where(q => q.Fname == Fname);
            }

            //// 5. 新增过滤条件：Fnumber
            //if (!string.IsNullOrEmpty(Fnumber))
            //{
            //    query = query.Where(q => q.Fnumber == Fnumber);
            //}

            // 分页逻辑
            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedQuery = query.OrderByDescending(b => b.Id)
                                     .Skip((page - 1) * pageSize)
                                     .Take(pageSize);

            // 返回字段（包含所有新增字段）
            var result = paginatedQuery.Select(a => new
            {
                Fwobillno = a.Fwobillno,
                Fcustno = a.Fcustno,
                Fcustname = a.Fcustname,
                Fname = a.Fname,
                //Fnumber = a.Fnumber,  // 确保视图中有此字段
                Fsourceid = a.Fsourceid,
                Fworkorderlistid = a.Fworkorderlistid,
                Id = a.Id,
                Fobjectid = a.Fobjectid,
                Fqty = a.Fqty,
                Fweight = a.Fweight,
                Fcommitqty = a.Fcommitqty,
                Fpassqty = a.Fpassqty,
                Fempid = a.Fempid,
                Fdeptid = a.Fdeptid,
                Fbiller = a.Fbiller,
                Fdate = a.Fdate,
                Fobjectno = a.Fobjectno,
                Fobjectname = a.Fobjectname,
                Fdept_name = a.Fdept_name ?? string.Empty,
                Femp_name = a.Femp_name ?? string.Empty
            }).ToList();

            // 构造响应数据（包含分页信息）
            var response = new
            {
                code = 200,
                msg = "OK",
                data = new
                {
                    totalCounts = totalCount,
                    totalPagess = totalPages,
                    currentPages = page,
                    pageSizes = pageSize,
                    data = result  // 分页后的数据
                }
            };

            return Json(response);
        }
    }
}