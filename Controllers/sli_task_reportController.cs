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
    public class sli_task_reportController : ApiController
    {
        public sli_task_reportController()
        {
            // _context = context;
        }

        [System.Web.Http.HttpPost]
        public async Task<object> sli_task_report_Insert([Microsoft.AspNetCore.Mvc.FromBody] sli_task_report[] options)
        {
            var context = new YourDbContext();
            try
            {
                foreach (var option in options)
                {

                    var header = new sli_task_report
                    {
                        Fsourceid = option.Fsourceid,
                        // 自增主键
                        //Id = option.Id,
                        // 编号
                        Fnumber = option.Fnumber,
                        // 员工 ID
                        Fempid = option.Fempid,
                        // 部门 ID
                        Fdeptid = option.Fdeptid,
                        // 状态
                        Fstatus = option.Fstatus,
                        // 日期
                        Fdate = option.Fdate,
                        // 备注
                        Fnote = option.Fnote
                    };
                    context.sli_task_report.Add(header);
                }
                

                await context.SaveChangesAsync();
                var datas = new
                {
                    code = 200,
                    msg = "Success",
                    Date =  "保存成功"
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
        public async Task<object> sli_task_report_Update([Microsoft.AspNetCore.Mvc.FromBody] sli_task_report option)
        {
            try
            {
                var context = new YourDbContext();
                var entity = await context.sli_task_report.FindAsync(option.Id);
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
                    var sli_task_reports = context.sli_task_report.FirstOrDefault(p => p.Id == option.Id);
                    //var Sli_plan_modelEntrys = _context.Sli_plan_modelEntry.Where(p => p.fmodelID == model.Id).ToList();



                    sli_task_reports.Fsourceid = option.Fsourceid;
                    sli_task_reports.Id = option.Id;
                    sli_task_reports.Fnumber = option.Fnumber;
                    sli_task_reports.Fempid = option.Fempid;
                    sli_task_reports.Fdeptid = option.Fdeptid;
                    sli_task_reports.Fstatus = option.Fstatus;
                    sli_task_reports.Fdate = option.Fdate;
                    sli_task_reports.Fnote = option.Fnote;

                    await context.SaveChangesAsync();

                    var datas = new
                    {
                        code = 200,
                        msg = "ok",
                        date = "修改成功！"
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
        public async Task<object> sli_task_reports_Delete(List<int> id)
        {
            try
            {
                var context = new YourDbContext();
                foreach (var deleteid in id)
                {
                    var entity = await context.sli_task_report.FindAsync(deleteid);
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
                    context.sli_task_report.RemoveRange(entity);
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
        public IHttpActionResult GetTableBysli_task_report(int page = 1, int pageSize = 10)
        {

            var context = new YourDbContext();
            IQueryable<sli_task_report> query = context.sli_task_report;

            
            var totalCount = query.Count(); //记录数
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize); // 页数
            var paginatedQuery = query.OrderByDescending(b => b.Id).Skip((page - 1) * pageSize).Take(pageSize); //  某页记录
            var result = paginatedQuery.Select(a => new
            {
                // 源 ID
                Fsourceid = a.Fsourceid,
                // 自增主键
                Id = a.Id,
                // 编号
                Fnumber = a.Fnumber ?? string.Empty,
                // 员工 ID
                Fempid = a.Fempid,
                // 部门 ID
                Fdeptid = a.Fdeptid,
                // 状态
                Fstatus = a.Fstatus,
                // 日期
                Fdate = a.Fdate,
                // 备注
                Fnote = a.Fnote ?? string.Empty


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
    }
}