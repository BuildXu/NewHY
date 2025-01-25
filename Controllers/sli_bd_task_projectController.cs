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
    public class sli_bd_task_projectController : ApiController
    {
        public sli_bd_task_projectController()
        {
            // _context = context;
        }

        [System.Web.Http.HttpPost]
        public async Task<object> sli_bd_task_project_Insert([Microsoft.AspNetCore.Mvc.FromBody] sli_bd_task_project[] options)
        {
            var context = new YourDbContext();
            try
            {
                foreach (var option in options)
                {

                    var header = new sli_bd_task_project
                    {
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
                    context.sli_bd_task_project.Add(header);
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
        public async Task<object> sli_bd_task_project_Update([Microsoft.AspNetCore.Mvc.FromBody] sli_bd_task_project option)
        {
            try
            {
                var context = new YourDbContext();
                var entity = await context.sli_bd_task_project.FindAsync(option.Id);
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
                    var sli_bd_task_projects = context.sli_bd_task_project.FirstOrDefault(p => p.Id == option.Id);
                    //var Sli_plan_modelEntrys = _context.Sli_plan_modelEntry.Where(p => p.fmodelID == model.Id).ToList();


                    sli_bd_task_projects.FNumber = option.FNumber;
                    sli_bd_task_projects.FName = option.FName;
                    sli_bd_task_projects.FStartDate = option.FStartDate;
                    sli_bd_task_projects.FEndDate = option.FEndDate;
                    sli_bd_task_projects.FNote = option.FNote;


                    await context.SaveChangesAsync();

                    var datas = new
                    {
                        code = 200,
                        msg = "ok",
                        date = sli_bd_task_projects.Id + "修改成功！"
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
        public async Task<object> sli_bd_task_project_Delete(List<int> id)
        {
            try
            {
                var context = new YourDbContext();
                foreach (var deleteid in id)
                {
                    var entity = await context.sli_bd_task_project.FindAsync(deleteid);
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
                    context.sli_bd_task_project.RemoveRange(entity);
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
        public IHttpActionResult GetTableBysli_bd_task_project(int page = 1, int pageSize = 10)
        {

            //int? Id = null,
            //int? Fsourceid = null,
            //int? Fworkorderlistid = null,
            //int? Fprocessoption = null,
            //DateTime? Fstartdate = null,
            //DateTime? Fenddate = null,
            //int? Fdeptid = null,
            //int? Fstatus = null,
            //string Foptionno = null,
            //string Foptionname = null,
            //string Fcustno = null,
            //string Fcustname = null,
            //string Fname = null
            var context = new YourDbContext();
            IQueryable<sli_bd_task_project> query = context.sli_bd_task_project;

            //if (Id.HasValue)
            //{
            //    query = query.Where(q => q.Id == Id);
            //}
            //if (Fsourceid.HasValue)
            //{
            //    query = query.Where(q => q.Fsourceid == Fsourceid);
            //}
            //if (Fworkorderlistid.HasValue)
            //{
            //    query = query.Where(q => q.Fworkorderlistid == Fworkorderlistid);
            //}
            //if (Fprocessoption.HasValue)
            //{
            //    query = query.Where(q => q.Fprocessoption == Fprocessoption);
            //}
            //if (Fstartdate.HasValue)
            //{
            //    query = query.Where(q => q.Fstartdate == Fstartdate);
            //}
            //if (Fenddate.HasValue)
            //{
            //    query = query.Where(q => q.Fenddate == Fenddate);
            //}
            //if (Fdeptid.HasValue)
            //{
            //    query = query.Where(q => q.Fdeptid == Fdeptid);
            //}
            //if (Fstatus.HasValue)
            //{
            //    query = query.Where(q => q.Fstatus == Fstatus);
            //}
            //if (!string.IsNullOrEmpty(Foptionno))
            //{
            //    query = query.Where(q => q.Foptionno == Foptionno);
            //}
            //if (!string.IsNullOrEmpty(Foptionname))
            //{
            //    query = query.Where(q => q.Foptionname == Foptionname);
            //}
            //if (!string.IsNullOrEmpty(Fcustno))
            //{
            //    query = query.Where(q => q.Fcustno == Fcustno);
            //}
            //if (!string.IsNullOrEmpty(Fcustname))
            //{
            //    query = query.Where(q => q.Fcustname == Fcustname);
            //}
            //if (!string.IsNullOrEmpty(Fname))
            //{
            //    query = query.Where(q => q.Fname == Fname);
            //}
            var totalCount = query.Count(); //记录数
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize); // 页数
            var paginatedQuery = query.OrderByDescending(b => b.Id).Skip((page - 1) * pageSize).Take(pageSize); //  某页记录
            var result = paginatedQuery.Select(a => new
            {
                FNumber = a.FNumber,
                // 名称
                FName = a.FName,
                // 开始日期
                FStartDate = a.FStartDate,
                // 结束日期
                FEndDate = a.FEndDate,
                // 备注
                FNote = a.FNote ?? string.Empty,
                // 状态
                FStatus = a.FStatus,
                // 使用状态
                FUsed = a.FUsed,
                // 创建日期
                FCreateDate = a.FCreateDate 


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