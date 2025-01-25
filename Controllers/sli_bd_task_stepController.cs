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
    public class sli_bd_task_stepController : ApiController
    {
        public sli_bd_task_stepController()
        {
            // _context = context;
        }

        [System.Web.Http.HttpPost]
        public async Task<object> sli_bd_task_step_Insert([Microsoft.AspNetCore.Mvc.FromBody] sli_bd_task_step[] options)
        {
            var context = new YourDbContext();
            try
            {
                foreach (var option in options)
                {

                    var header = new sli_bd_task_step
                    {
                        // 编号
                        FNumber = option.FNumber,
                        // 名称
                        FName = option.FName,
                        // 备注
                        FNote = option.FNote,
                        // 状态
                        FStatus = option.FStatus,
                        // 使用状态
                        FUsed = option.FUsed,
                        // 创建日期
                        FCreateDate = option.FCreateDate
                    };
                    context.sli_bd_task_step.Add(header);
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
        public async Task<object> sli_bd_task_step_Update([Microsoft.AspNetCore.Mvc.FromBody] sli_bd_task_step option)
        {
            try
            {
                var context = new YourDbContext();
                var entity = await context.sli_bd_task_step.FindAsync(option.Id);
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
                    var sli_bd_task_steps = context.sli_bd_task_step.FirstOrDefault(p => p.Id == option.Id);
                    //var Sli_plan_modelEntrys = _context.Sli_plan_modelEntry.Where(p => p.fmodelID == model.Id).ToList();


                    sli_bd_task_steps.FNumber = option.FNumber;
                    sli_bd_task_steps.FName = option.FName;
                    sli_bd_task_steps.FStatus = option.FStatus;
                    sli_bd_task_steps.FNote = option.FNote;
                    sli_bd_task_steps.FUsed = option.FUsed;

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
        public async Task<object> sli_bd_task_steps_Delete(List<int> id)
        {
            try
            {
                var context = new YourDbContext();
                foreach (var deleteid in id)
                {
                    var entity = await context.sli_bd_task_step.FindAsync(deleteid);
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
                    context.sli_bd_task_step.RemoveRange(entity);
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
        public IHttpActionResult GetTableBysli_bd_task_step(int page = 1, int pageSize = 10)
        {

            var context = new YourDbContext();
            IQueryable<sli_bd_task_step> query = context.sli_bd_task_step;

            
            var totalCount = query.Count(); //记录数
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize); // 页数
            var paginatedQuery = query.OrderByDescending(b => b.Id).Skip((page - 1) * pageSize).Take(pageSize); //  某页记录
            var result = paginatedQuery.Select(a => new
            {
                FNumber = a.FNumber,
                // 名称
                FName = a.FName,
  
                // 备注
                FNote = a.FNote ?? string.Empty,
                // 状态
                FStatus = a.FStatus,
                // 使用状态
                FUsed = a.FUsed,
                // 创建日期
                FCreateDate = a.FCreateDate ?? string.Empty


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