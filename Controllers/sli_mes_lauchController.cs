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
        public async Task<object> Process_object_Insert([Microsoft.AspNetCore.Mvc.FromBody] sli_bd_process_object option)
        {
            var context = new YourDbContext();
            try
            {
                var header = new sli_bd_process_object
                {
                    fname = option.fname,
                    fnumber = option.fnumber,
                    fnote = option.fnote,
                    fstatus = option.fstatus,
                    fused = option.fused,
                    fcreateDate = option.fcreateDate
                };
                context.Sli_bd_process_object.Add(header);
                await context.SaveChangesAsync();
                var datas = new
                {
                    code = 200,
                    msg = "Success",
                    Date = header.id.ToString() + "保存成功"

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
        public async Task<object> Process_object_Update([Microsoft.AspNetCore.Mvc.FromBody] sli_bd_process_object option)
        {
            try
            {
                var context = new YourDbContext();
                var entity = await context.Sli_bd_process_object.FindAsync(option.id);
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

                    var Sli_bd_process_objects = context.Sli_bd_process_object.FirstOrDefault(p => p.id == option.id);
                    //var Sli_plan_modelEntrys = _context.Sli_plan_modelEntry.Where(p => p.fmodelID == model.Id).ToList();


                    Sli_bd_process_objects.fname = option.fname;
                    Sli_bd_process_objects.fnumber = option.fnumber;
                    Sli_bd_process_objects.fnote = option.fnote;
                    Sli_bd_process_objects.fstatus = option.fstatus;
                    Sli_bd_process_objects.fused = option.fused;
                    Sli_bd_process_objects.fcreateDate = option.fcreateDate;
                    //Sli_bd_tech_options.fnote = model.fnote;

                    await context.SaveChangesAsync();

                    var datas = new
                    {
                        code = 200,
                        msg = "ok",
                        date = Sli_bd_process_objects.id + "修改成功！"
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
                return Ok(datas); ;
            }
        }

        [System.Web.Http.HttpPost]
        public async Task<object> Process_object_Delete(List<int> id)
        {
            try
            {
                var context = new YourDbContext();
                foreach (var deleteid in id)
                {

                    var entity = await context.Sli_bd_process_object.FindAsync(deleteid);
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
                    context.Sli_bd_process_object.RemoveRange(entity);


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
        public IHttpActionResult GetTableByProcess_object(string FNumber = null, string FName = null)
        {
            var context = new YourDbContext();
            IQueryable<sli_bd_process_object> query = context.Sli_bd_process_object;

            if (!string.IsNullOrEmpty(FNumber))
            {
                query = query.Where(q => q.fnumber.Contains(FNumber));
            }

            if (!string.IsNullOrEmpty(FName))
            {
                query = query.Where(q => q.fname.Contains(FName));
            }
            //var totalCount = query.Count(); //记录数
            //var totalPages = (int)Math.Ceiling((double)totalCount / pageSize); // 页数
            //var paginatedQuery = query.OrderByDescending(b => b.id).Skip((page - 1) * pageSize).Take(pageSize); //  某页记录
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