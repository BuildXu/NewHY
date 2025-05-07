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
    //工序派工
    public class sli_mes_orderoptionController : ApiController
    {
        public sli_mes_orderoptionController()
        {
            // _context = context;
        }

        [System.Web.Http.HttpPost]
        public async Task<object> sli_mes_orderoption_Insert([Microsoft.AspNetCore.Mvc.FromBody] sli_mes_orderoption[] options)
        {
            var context = new YourDbContext();
            try
            {
                foreach (var option in options)
                {
                    var header = new sli_mes_orderoption
                    {
                        Fnumber = option.Fnumber,
                        Fsourceid = option.Fsourceid ?? 0,
                        Fworkorderlistid = option.Fworkorderlistid,
                        Fprocessoption = option.Fprocessoption,
                        Fqty = option.Fqty,
                        Fweight = option.Fweight,
                        Fcommitqty = option.Fcommitqty,
                        Fpassqty = option.Fpassqty,
                        Fbiller = option.Fbiller,
                        Fstartdate = option.Fstartdate ?? Convert.ToDateTime("2025-01-01"),
                        Fenddate = option.Fenddate ?? Convert.ToDateTime("2025-01-01"),
                        Fdate = option.Fdate
                    };
                    context.Sli_mes_orderoption.Add(header);
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
        public async Task<object> sli_mes_orderoptionUpdate([Microsoft.AspNetCore.Mvc.FromBody] sli_mes_orderoption option)
        {
            try
            {
                var context = new YourDbContext();
                var entity = await context.Sli_mes_orderoption.FindAsync(option.Id);
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
                    var sli_mes_orderoption = context.Sli_mes_orderoption.FirstOrDefault(p => p.Id == option.Id);
                    //var Sli_plan_modelEntrys = _context.Sli_plan_modelEntry.Where(p => p.fmodelID == model.Id).ToList();


                    sli_mes_orderoption.Fnumber = option.Fnumber;
                    sli_mes_orderoption.Fsourceid = option.Fsourceid;
                    sli_mes_orderoption.Fworkorderlistid = option.Fworkorderlistid;
                    sli_mes_orderoption.Fprocessoption = option.Fprocessoption;
                    sli_mes_orderoption.Fqty = option.Fqty;
                    sli_mes_orderoption.Fweight = option.Fweight;
                    sli_mes_orderoption.Fcommitqty = option.Fcommitqty;
                    sli_mes_orderoption.Fpassqty = option.Fpassqty;
                    sli_mes_orderoption.Fbiller = option.Fbiller;
                    sli_mes_orderoption.Fstartdate = option.Fstartdate;
                    sli_mes_orderoption.Fenddate = option.Fenddate;
                    sli_mes_orderoption.Fdate = option.Fdate;


                    await context.SaveChangesAsync();

                    var datas = new
                    {
                        code = 200,
                        msg = "ok",
                        date = sli_mes_orderoption.Id + "修改成功！"
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
        public async Task<object> sli_mes_orderoption_Delete(List<int> id)
        {
            try
            {
                var context = new YourDbContext();
                foreach (var deleteid in id)
                {
                    var entity = await context.Sli_mes_orderoption.FindAsync(deleteid);
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
                    context.Sli_mes_orderoption.Remove(entity);
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
        public IHttpActionResult GetTableBySli_mes_option(
            int page = 1,
            int pageSize = 10,
            string Fwobillno = null,
            string Fcustno = null,      // 新增参数
            string Fcustname = null,    // 新增参数
            string Fname = null,        // 新增参数
            string Fnumber = null       // 新增参数
        )
        {
            var context = new YourDbContext();
            IQueryable<sli_mes_orderoption_view> query = context.Sli_mes_orderoption_view;

            // 过滤条件：Fwobillno
            if (!string.IsNullOrEmpty(Fwobillno))
            {
                query = query.Where(q => q.Fwobillno == Fwobillno);
            }

            // 新增过滤条件：Fcustno
            if (!string.IsNullOrEmpty(Fcustno))
            {
                query = query.Where(q => q.Fcustno == Fcustno);
            }

            // 新增过滤条件：Fcustname
            if (!string.IsNullOrEmpty(Fcustname))
            {
                query = query.Where(q => q.Fcustname == Fcustname);
            }

            // 新增过滤条件：Fname
            if (!string.IsNullOrEmpty(Fname))
            {
                query = query.Where(q => q.Fname == Fname);
            }

            // 新增过滤条件：Fnumber
            if (!string.IsNullOrEmpty(Fnumber))
            {
                query = query.Where(q => q.Fnumber == Fnumber);
            }

            // 分页逻辑
            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedQuery = query.OrderByDescending(b => b.Id)
                                     .Skip((page - 1) * pageSize)
                                     .Take(pageSize);

            // 返回字段（已包含新增字段）
            var result = paginatedQuery.Select(a => new
            {
                Fwobillno = a.Fwobillno,
                Fcustno = a.Fcustno,          // 确保映射正确
                Fcustname = a.Fcustname,       // 确保映射正确
                Fname = a.Fname,               // 确保映射正确
                Fnumber = a.Fnumber,           // 确保映射正确
                Id = a.Id,
                Fsourceid = a.Fsourceid ?? 0,
                Fworkorderlistid = a.Fworkorderlistid,
                Fprocessoption = a.Fprocessoption,
                Fqty = a.Fqty,
                Fweight = a.Fweight,
                Fcommitqty = a.Fcommitqty,
                Fpassqty = a.Fpassqty,
                Fbiller = a.Fbiller,
                Fstartdate = a.Fstartdate,
                Fenddate = a.Fenddate,
                Fempid = a.Fempid ?? 0,
                Fdeptid = a.Fdeptid ?? 0,
                Fdate = a.Fdate,
                Foptionno = a.Foptionno,
                Foptionname = a.Foptionname,
                Fdept_name = a.Fdept_name ?? string.Empty,
                Femp_name = a.Femp_name ?? string.Empty,
                Fslimetal = a.Fslimetal ?? string.Empty,
                Fsliweightmaterial = a.Fsliweightmaterial ?? 0,
                Fslipunching = a.Fslipunching ?? string.Empty,
                Fmaterialid = a.Fmaterialid ?? 0,
                Fsliallowancehf = a.Fsliallowancehf ?? 0,
                Fproducttype = a.Fproducttype ?? string.Empty
            });

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
                    data = result.ToList()  // 确保返回分页数据
                }
            };

            return Ok(response);
        }


        [System.Web.Http.HttpGet]
        public IHttpActionResult GetTableBySli_mes_option_view(
    string Fwobillno,  // 增加 工作令号 查询
    int? Id = null,
    int? Fsourceid = null,
    int? Fworkorderlistid = null,
    int? Fprocessoption = null,
    decimal? Fqty = null,
    decimal? Fweight = null,
    decimal? Fcommitqty = null,
    decimal? Fpassqty = null,
    int? Fbiller = null,
    DateTime? Fstartdate = null,
    DateTime? Fenddate = null,
    DateTime? Fdate = null,
    string Fcustno = null,
    string Fcustname = null,
    string Fname = null,
    string Fnumber = null,
    int? Fempid = null,
    int? Fdeptid = null,
    string Foptionno = null,
    string Foptionname = null)
        {
            var context = new YourDbContext();
            IQueryable<sli_mes_orderoption_view> query = context.Sli_mes_orderoption_view;

            if (Id.HasValue)
            {
                query = query.Where(q => q.Id == Id);
            }
            if (Fsourceid.HasValue)
            {
                query = query.Where(q => q.Fsourceid == Fsourceid);
            }
            if (Fworkorderlistid.HasValue)
            {
                query = query.Where(q => q.Fworkorderlistid == Fworkorderlistid);
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
            if (Fbiller.HasValue)
            {
                query = query.Where(q => q.Fbiller == Fbiller);
            }
            if (Fstartdate.HasValue)
            {
                query = query.Where(q => q.Fstartdate == Fstartdate);
            }
            if (Fenddate.HasValue)
            {
                query = query.Where(q => q.Fenddate == Fenddate);
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
            if (!string.IsNullOrEmpty(Fnumber))
            {
                query = query.Where(q => q.Fnumber == Fnumber);
            }
            if (Fempid.HasValue)
            {
                query = query.Where(q => q.Fempid == Fempid);
            }
            if (Fdeptid.HasValue)
            {
                query = query.Where(q => q.Fdeptid == Fdeptid);
            }
            if (!string.IsNullOrEmpty(Foptionno))
            {
                query = query.Where(q => q.Foptionno == Foptionno);
            }
            if (!string.IsNullOrEmpty(Foptionname))
            {
                query = query.Where(q => q.Foptionname == Foptionname);
            }
            if (!string.IsNullOrEmpty(Fwobillno))
            {
                query = query.Where(q => q.Fwobillno == Fwobillno);
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