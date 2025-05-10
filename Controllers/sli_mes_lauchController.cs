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
    public class sli_mes_lauchController : ApiController
    {
        //投产计划

        public sli_mes_lauchController()
        {
            // _context = context;
        }

        [System.Web.Http.HttpPost]
        public async Task<object> sli_mes_lauch_Insert([Microsoft.AspNetCore.Mvc.FromBody] sli_mes_lauchbill[] options)
        {
            var context = new YourDbContext();
            try
            {
                foreach (var option in options)
                {

                    var header = new sli_mes_lauchbill
                    {
                        Fsourceid = option.Fsourceid,
                        Fworkorderlistid = option.Fworkorderlistid,
                        Fprocessoption = option.Fprocessoption,
                        Fstartdate = option.Fstartdate ?? Convert.ToDateTime("2025-01-01"),
                        Fenddate = option.Fenddate ?? Convert.ToDateTime("2025-01-01"),
                        Fdeptid = option.Fdeptid,
                        Fstatus = option.Fstatus,
                        Ftype = option.Ftype,
                        Fsupplier = option.Fsupplier,
                        Fnumber = option.Fnumber
                    };
                    context.sli_mes_lauchbill.Add(header);
                }


                await context.SaveChangesAsync();
                var datas = new
                {
                    code = 200,
                    msg = "Success",
                    Date = "保存成功"
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
        public async Task<object> sli_mes_lauch_Update([Microsoft.AspNetCore.Mvc.FromBody] sli_mes_lauchbill option)
        {
            try
            {
                var context = new YourDbContext();
                var entity = await context.sli_mes_lauchbill.FindAsync(option.Id);
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
                    var sli_mes_lauchbills = context.sli_mes_lauchbill.FirstOrDefault(p => p.Id == option.Id);
                    //var Sli_plan_modelEntrys = _context.Sli_plan_modelEntry.Where(p => p.fmodelID == model.Id).ToList();


                    sli_mes_lauchbills.Fsourceid = option.Fsourceid;
                    sli_mes_lauchbills.Fworkorderlistid = option.Fworkorderlistid;
                    sli_mes_lauchbills.Fprocessoption = option.Fprocessoption;
                    sli_mes_lauchbills.Fstartdate = option.Fstartdate;
                    sli_mes_lauchbills.Fenddate = option.Fenddate;
                    sli_mes_lauchbills.Fdeptid = option.Fdeptid;
                    sli_mes_lauchbills.Fstatus = option.Fstatus;
                    sli_mes_lauchbills.Ftype = option.Ftype;
                    sli_mes_lauchbills.Fsupplier = option.Fsupplier;


                    await context.SaveChangesAsync();

                    var datas = new
                    {
                        code = 200,
                        msg = "ok",
                        date = sli_mes_lauchbills.Id + "修改成功！"
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
        public async Task<object> sli_mes_lauch_Delete(List<int> id)
        {
            try
            {
                var context = new YourDbContext();
                foreach (var deleteid in id)
                {
                    var entity = await context.sli_mes_lauchbill.FindAsync(deleteid);
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
                    context.sli_mes_lauchbill.RemoveRange(entity);
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
        public IHttpActionResult GetTableBySli_mes_lauch_view(
        int page = 1,
        int pageSize = 10,
        string Fwobillno = null,
        // 启用所有注释参数
        int? Id = null,
        int? Fsourceid = null,
        int? Fworkorderlistid = null,
        int? Fprocessoption = null,
        DateTime? Fstartdate = null,
        DateTime? Fenddate = null,
        DateTime? FenddateStart = null, // 新增起始日期参数
        DateTime? FenddateEnd = null,    // 新增结束日期参数
        int? Fdeptid = null,
        int? Fstatus = null,
        string Foptionno = null,
        string Foptionname = null,
        string Fcustno = null,
        string Fcustname = null,
        string Fname = null,
        string Ftype = null,
        string Fsupplier =null
    )
        {
            var context = new YourDbContext();
            IQueryable<sli_mes_lauchbill_view> query = context.sli_mes_lauchbill_view;

            // 过滤条件（启用所有注释的逻辑）
            // 修改Fenddate为日期区间查询
            if (FenddateStart.HasValue)
            {
                query = query.Where(q => q.Fenddate >= FenddateStart.Value);
            }
            if (FenddateEnd.HasValue)
            {
                query = query.Where(q => q.Fenddate <= FenddateEnd.Value);
            }
            if (!string.IsNullOrEmpty(Fwobillno)) query = query.Where(q => q.Fwobillno == Fwobillno);
            if (Id.HasValue) query = query.Where(q => q.Id == Id);
            if (Fsourceid.HasValue) query = query.Where(q => q.Fsourceid == Fsourceid);
            if (Fworkorderlistid.HasValue) query = query.Where(q => q.Fworkorderlistid == Fworkorderlistid);
            if (Fprocessoption.HasValue) query = query.Where(q => q.Fprocessoption == Fprocessoption);
            if (Fstartdate.HasValue) query = query.Where(q => q.Fstartdate == Fstartdate);
            if (Fenddate.HasValue) query = query.Where(q => q.Fenddate == Fenddate);
            if (Fdeptid.HasValue) query = query.Where(q => q.Fdeptid == Fdeptid);
            if (Fstatus.HasValue) query = query.Where(q => q.Fstatus == Fstatus);
            if (!string.IsNullOrEmpty(Foptionno)) query = query.Where(q => q.Foptionno == Foptionno);
            if (!string.IsNullOrEmpty(Foptionname)) query = query.Where(q => q.Foptionname == Foptionname);
            if (!string.IsNullOrEmpty(Fcustno)) query = query.Where(q => q.Fcustno == Fcustno);
            if (!string.IsNullOrEmpty(Fcustname)) query = query.Where(q => q.Fcustname == Fcustname);
            if (!string.IsNullOrEmpty(Fname)) query = query.Where(q => q.Fname == Fname);
            if (!string.IsNullOrEmpty(Ftype)) query = query.Where(q => q.Ftype == Ftype);
            if (!string.IsNullOrEmpty(Fsupplier)) query = query.Where(q => q.Fsupplier == Fsupplier);

            // 分页逻辑
            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedQuery = query.OrderByDescending(b => b.Id)
                                     .Skip((page - 1) * pageSize)
                                     .Take(pageSize);

            // 返回字段（已包含所有必要字段）
            var result = paginatedQuery.Select(a => new
            {
                Fwobillno = a.Fwobillno,
                Fslimetal = a.Fslimetal,
                Fothers = a.Fothers,
                Fqty = a.Fqty,
                Fweight = a.Fweight,
                Fweights = a.Fweights,
                Fcustno = a.Fcustno,
                Fcustname = a.Fcustname,
                Fname = a.Fname,
                Id = a.Id,
                Fsourceid = a.Fsourceid,
                Fworkorderlistid = a.Fworkorderlistid,
                Fprocessoption = a.Fprocessoption,
                Fstartdate = a.Fstartdate,
                Fenddate = a.Fenddate,
                Fdeptid = a.Fdeptid,
                Fstatus = a.Fstatus,
                Foptionno = a.Foptionno,
                Foptionname = a.Foptionname,
                Fdept_name = a.Fdept_name ?? string.Empty,
                Fnumber = a.Fnumber,
                Forderno = a.Forderno ?? string.Empty,
                Fproductno = a.Fproductno ?? string.Empty,
                Fpname = a.Fpname ?? string.Empty,
                Fdescription = a.Fdescription ?? string.Empty
                Ftype = a.Ftype ?? string.Empty,
                Fsupplier = a.Fsupplier ?? string.Empty

            }).ToList();  // 执行查询

            // 响应数据（修正分页参数）
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
                    data = result  // 返回分页后的数据
                }
            };

            return Json(response);
        }
    }
}