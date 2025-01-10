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
                        Fstartdate = option.Fstartdate,
                        Fenddate = option.Fenddate,
                        Fdeptid = option.Fdeptid,
                        Fstatus = option.Fstatus
                    };
                    context.Sli_mes_lauchbill.Add(header);
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
        public async Task<object> sli_mes_lauch_Update([Microsoft.AspNetCore.Mvc.FromBody] sli_mes_lauchbill option)
        {
            try
            {
                var context = new YourDbContext();
                var entity = await context.Sli_mes_lauchbill.FindAsync(option.Id);
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
                    var sli_mes_lauchbills = context.Sli_mes_lauchbill.FirstOrDefault(p => p.Id == option.Id);
                    //var Sli_plan_modelEntrys = _context.Sli_plan_modelEntry.Where(p => p.fmodelID == model.Id).ToList();


                    sli_mes_lauchbills.Fsourceid = option.Fsourceid;
                    sli_mes_lauchbills.Fworkorderlistid = option.Fworkorderlistid;
                    sli_mes_lauchbills.Fprocessoption = option.Fprocessoption;
                    sli_mes_lauchbills.Fstartdate = option.Fstartdate;
                    sli_mes_lauchbills.Fenddate = option.Fenddate;
                    sli_mes_lauchbills.Fdeptid = option.Fdeptid;
                    sli_mes_lauchbills.Fstatus = option.Fstatus;


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
                    var entity = await context.Sli_mes_lauchbill.FindAsync(deleteid);
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
                    context.Sli_mes_lauchbill.RemoveRange(entity);
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
        public IHttpActionResult GetTableBySli_mes_lauch(int? Id = null, int? Fsourceid = null, int? Fworkorderlistid = null, int? Fprocessoption = null, DateTime? Fstartdate = null, DateTime? Fenddate = null, int? Fdeptid = null)
        {
            var context = new YourDbContext();
            IQueryable<sli_mes_lauchbill> query = context.Sli_mes_lauchbill;

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
                query = query.Where(q => q.Fprocessoption == Fprocessoption);
            if (Fstartdate.HasValue)
            {
                query = query.Where(q => q.Fstartdate == Fstartdate);
            }
            if (Fenddate.HasValue)
            {
                query = query.Where(q => q.Fenddate == Fenddate);
            }
            if (Fdeptid.HasValue)
            {
                query = query.Where(q => q.Fdeptid == Fdeptid);
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