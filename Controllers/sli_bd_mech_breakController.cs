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
    public class sli_bd_mech_breakController : ApiController
    {
        public sli_bd_mech_breakController()
        {
            // _context = context;

        }
        [System.Web.Http.HttpPost]
        public async Task<object> mech_break_Insert([Microsoft.AspNetCore.Mvc.FromBody] sli_bd_mech_break option)
        {
            var context = new YourDbContext();
            try
            {
                var header = new sli_bd_mech_break
                {
                    Fname = option.Fname,
                    Fnumber = option.Fnumber,
                    Fnote = option.Fnote,
                    Fstatus = option.Fstatus,
                    Fused = option.Fused,
                    FcreateDate = option.FcreateDate
                };
                context.Sli_bd_mech_break.Add(header);
                await context.SaveChangesAsync();
                await context.SaveChangesAsync();
                var datas = new
                {
                    code = 200,
                    msg = "Success",
                    Date = header.Id.ToString() + "保存成功"

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
        public async Task<object> mech_break_Update([Microsoft.AspNetCore.Mvc.FromBody] sli_bd_mech_break option)
        {
            try
            {
                var context = new YourDbContext();
                var entity = await context.Sli_bd_mech_break.FindAsync(option.Id);
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

                    var sli_bd_mech_breaks = context.Sli_bd_mech_break.FirstOrDefault(p => p.Id == option.Id);
                    //var Sli_plan_modelEntrys = _context.Sli_plan_modelEntry.Where(p => p.fmodelID == model.Id).ToList();

                    sli_bd_mech_breaks.Fname = option.Fname;
                    sli_bd_mech_breaks.Fnumber = option.Fnumber;
                    sli_bd_mech_breaks.Fnote = option.Fnote;
                    sli_bd_mech_breaks.Fstatus = option.Fstatus;
                    sli_bd_mech_breaks.Fused = option.Fused;
                    sli_bd_mech_breaks.FcreateDate = option.FcreateDate;
                    //Sli_bd_tech_options.fnote = model.fnote;

                    await context.SaveChangesAsync();

                    var datas = new
                    {
                        code = 200,
                        msg = "ok",
                        date = sli_bd_mech_breaks.Id + "修改成功！"
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
        public async Task<object> mech_break_Delete(List<int> id)
        {
            try
            {
                var context = new YourDbContext();
                foreach (var deleteid in id)
                {

                    var entity = await context.Sli_bd_mech_break.FindAsync(deleteid);
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
                    context.Sli_bd_mech_break.RemoveRange(entity);


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
        public IHttpActionResult GetTableBymech_break(string Fnumber = null, string Fname = null)
        {
            var context = new YourDbContext();
            IQueryable<sli_bd_mech_break> query = context.Sli_bd_mech_break;

            if (!string.IsNullOrEmpty(Fnumber))
            {
                query = query.Where(q => q.Fnumber.Contains(Fnumber));
            }

            if (!string.IsNullOrEmpty(Fname))
            {
                query = query.Where(q => q.Fname.Contains(Fname));
            }
            var totalCount = query.Count(); //记录数
            //var totalPages = (int)Math.Ceiling((double)totalCount / pageSize); // 页数
            //var paginatedQuery = query.OrderByDescending(b => b.id).Skip((page - 1) * pageSize).Take(pageSize); //  某页记录
            //var datas = query.ToList();
            var response = new    // 定义 前端返回数据  总记录，总页，当前页 ，size,返回记录
            {
                code = 200,
                msg = "OK",
                totalCount= totalCount,
                data = new
                {
                    
                    data = query
                }
            };

            return Json(response);
        }
    }
}