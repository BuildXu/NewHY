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
    public class sli_task_orderController : ApiController
    {
        public sli_task_orderController()
        {
            // _context = context;
        }

        [System.Web.Http.HttpPost]
        public async Task<object> sli_task_order_Insert([Microsoft.AspNetCore.Mvc.FromBody] sli_task_order[] options)
        {
            var context = new YourDbContext();
            try
            {
                foreach (var option in options)
                {

                    var header = new sli_task_order
                    {
                        // 项目 ID
                        Fproject = option.Fproject,
                        // 项目相关字符串信息
                        Fprojects = option.Fprojects,
                        // 编号
                        Fnumber = option.Fnumber,
                        // 备注
                        Fnote = option.Fnote,
                        // 来源
                        Fsource = option.Fsource,
                        // 日期
                        Fdate = option.Fdate,
                        // 开始日期
                        Fstartdate = option.Fstartdate,
                        // 结束日期
                        Fenddate = option.Fenddate,
                        // 步骤 ID
                        Fstep = option.Fstep,
                        // 步骤相关字符串信息
                        Fsteps = option.Fsteps,
                        // 状态
                        Fstatus = option.Fstatus,
                        // 类型 ID
                        Ftype = option.Ftype,
                        // 类型相关字符串信息
                        Ftypes = option.Ftypes
                    };
                    context.sli_task_order.Add(header);
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
        public async Task<object> sli_task_order_Update([Microsoft.AspNetCore.Mvc.FromBody] sli_task_order option)
        {
            try
            {
                var context = new YourDbContext();
                var entity = await context.sli_task_order.FindAsync(option.Id);
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
                    var sli_task_orders = context.sli_task_order.FirstOrDefault(p => p.Id == option.Id);
                    //var Sli_plan_modelEntrys = _context.Sli_plan_modelEntry.Where(p => p.fmodelID == model.Id).ToList();



                    sli_task_orders.Fproject = option.Fproject;
                    sli_task_orders.Fprojects = option.Fprojects;
                    sli_task_orders.Fnumber = option.Fnumber;
                    sli_task_orders.Fnote = option.Fnote;
                    sli_task_orders.Fsource = option.Fsource;
                    sli_task_orders.Fdate = option.Fdate;
                    sli_task_orders.Fstartdate = option.Fstartdate;
                    sli_task_orders.Fenddate = option.Fenddate;
                    sli_task_orders.Fstep = option.Fstep;
                    sli_task_orders.Fsteps = option.Fsteps;
                    sli_task_orders.Fstatus = option.Fstatus;
                    sli_task_orders.Ftype = option.Ftype;
                    sli_task_orders.Ftypes = option.Ftypes;

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
        public async Task<object> sli_task_orders_Delete(List<int> id)
        {
            try
            {
                var context = new YourDbContext();
                foreach (var deleteid in id)
                {
                    var entity = await context.sli_task_order.FindAsync(deleteid);
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
                    context.sli_task_order.RemoveRange(entity);
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
        public IHttpActionResult GetTableBysli_task_order(int page = 1, int pageSize = 10)
        {

            var context = new YourDbContext();
            IQueryable<sli_task_order> query = context.sli_task_order;

            
            var totalCount = query.Count(); //记录数
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize); // 页数
            var paginatedQuery = query.OrderByDescending(b => b.Id).Skip((page - 1) * pageSize).Take(pageSize); //  某页记录
            var result = paginatedQuery.Select(a => new
            {
                // 自增主键
                Id = a.Id,
                // 项目 ID
                Fproject = a.Fproject,
                // 项目相关字符串信息
                Fprojects = a.Fprojects ?? string.Empty,
                // 编号
                Fnumber = a.Fnumber ?? string.Empty,
                // 备注
                Fnote = a.Fnote ?? string.Empty,
                // 来源
                Fsource = a.Fsource ?? string.Empty,
                // 日期
                Fdate = a.Fdate,
                // 开始日期
                Fstartdate = a.Fstartdate,
                // 结束日期
                Fenddate = a.Fenddate,
                // 步骤 ID
                Fstep = a.Fstep,
                // 步骤相关字符串信息
                Fsteps = a.Fsteps ?? string.Empty,
                // 状态
                Fstatus = a.Fstatus,
                // 类型 ID
                Ftype = a.Ftype,
                // 类型相关字符串信息
                Ftypes = a.Ftypes ?? string.Empty


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