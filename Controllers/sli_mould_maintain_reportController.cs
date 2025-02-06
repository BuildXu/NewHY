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
    public class sli_mould_maintain_reportController : ApiController
    {
        public sli_mould_maintain_reportController()   //模具档案
        {

        }
        [System.Web.Http.HttpPost]
        public async Task<object> sli_mould_maintain_report_Insert([Microsoft.AspNetCore.Mvc.FromBody] sli_mould_maintain_report options)
        {
            var context = new YourDbContext();
            try
            {
                var header = new sli_mould_maintain_report
                {
                    Fnumber = options.Fnumber,
                    Fsourceid = options.Fsourceid,
                    mould = options.mould,
                    Fdate = options.Fdate,
                    Fempid = options.Fempid,
                    Fnote = options.Fnote,
                    Fbreak = options.Fbreak,
                    Fstatus = options.Fstatus
                };
                context.Sli_mould_maintain_report.Add(header);
                await context.SaveChangesAsync();
                var datas = new
                {
                    code = 200,
                    msg = "Success",
                    Date = options.Id.ToString() + "保存成功!"
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
        public async Task<object> sli_mould_maintain_reportUpdate([Microsoft.AspNetCore.Mvc.FromBody] sli_mould_maintain_report option)
        {
            try
            {
                var context = new YourDbContext();
                var entity = await context.Sli_mould_maintain_report.FindAsync(option.Id);
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
                    var Sli_mould_maintain_report = context.Sli_mould_maintain_report.FirstOrDefault(p => p.Id == option.Id);


                    Sli_mould_maintain_report.Fnumber = option.Fnumber;
                    //Id = option.Id,
                    Sli_mould_maintain_report.Fdate = option.Fdate;
                    Sli_mould_maintain_report.Fempid = option.Fempid;
                    Sli_mould_maintain_report.Fnote = option.Fnote;
                    Sli_mould_maintain_report.Fbreak = option.Fbreak;
                    Sli_mould_maintain_report.Fstatus = option.Fstatus;


                    await context.SaveChangesAsync();

                    var datas = new
                    {
                        code = 200,
                        msg = "ok",
                        date = Sli_mould_maintain_report.Id + "修改成功！"
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
        public async Task<object> sli_mould_maintain_report_Delete(List<int> id)
        {
            try
            {
                var context = new YourDbContext();
                foreach (var deleteid in id)
                {
                    var entity = await context.Sli_mould_maintain_report.FindAsync(deleteid);
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
                    context.Sli_mould_maintain_report.Remove(entity);
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
        public IHttpActionResult GetTableBysli_mould_maintain_report(int page = 1, int pageSize = 10)
        {
            var context = new YourDbContext();
            var query = context.Sli_mould_maintain_report_view;

            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedQuery = query.OrderByDescending(b => b.Id).Skip((page - 1) * pageSize).Take(pageSize);
            var result = paginatedQuery.Select(a => new
            {
                Id=a.Id,
                Fnumber = a.Fnumber,
                Fsourceid = a.Fsourceid,
                mould = a.mould,
                Fdate = a.Fdate,
                Fempid = a.Fempid,
                Fnote = a.Fnote,
                Fstatus = a.Fstatus,
                Fmouldnumber = a.Fmouldnumber,
                Fname = a.Fname,
                Fbreak = a.Fbreak,
                Fbreakname = a.Fbreakname ?? string.Empty,
                FempName = a.FempName
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