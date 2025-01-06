using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi_SY.Entity;
using WebApi_SY.Models;

namespace WebApi_SY.Controllers
{
    public class Sli_document_mp_rollingController : ApiController
    {
        [System.Web.Http.HttpPost]
        //新增接口，无必要，这一步用存触发器或存储过程实现，自动新增 （销售订单行新增时根据产品类型 & 对应档案信息）
        // 如果物料没有维护产品类型？ 
        //*****************   

        public async Task<object> mp_rolling_Insert([Microsoft.AspNetCore.Mvc.FromBody] sli_document_mp_rolling rolling)
        {
            var context = new YourDbContext();
            try
            {
                // 检查 rolling 对象是否为 null
                if (rolling == null)
                {
                    throw new ArgumentNullException(nameof(rolling), "传入的 rolling 对象不能为 null。");
                }

                var mp_Rolling = new sli_document_mp_rolling
                {
                    // 假设这里的 Id 属性已经在 sli_document_mp_rolling 类中正确定义
                    //Id = rolling.Id,
                    Fnumber = rolling.Fnumber ?? "默认编号",
                    Fversion = rolling.Fversion ?? "默认版本",
                    Fproducttype = rolling.Fproducttype ?? "默认产品类型",
                    Fstatus = rolling.Fstatus,
                    Fbiller = rolling.Fbiller,
                    Fsliouterdiameter = rolling.Fsliouterdiameter+3+ rolling.Fsliinnerdiameter,
                    Fsliinnerdiameter = rolling.Fsliinnerdiameter,
                    Fslihight = rolling.Fslihight,
                    Fsliallowanceod = rolling.Fsliallowanceod,
                    Fsliallowanceid = rolling.Fsliallowanceid,
                    fsliallowanceh = rolling.fsliallowanceh,
                    Fsliweightmaterial = rolling.Fsliweightmaterial,
                    Fsliweightforging = rolling.Fsliweightforging,
                    Fsliweightgoods = rolling.Fsliweightgoods,
                    Fsliweightfurnace = rolling.Fsliweightfurnace,
                    Fslidrawingno = rolling.Fslidrawingno,
                    Fslimetal = rolling.Fslimetal,
                    Fheattreatment = rolling.Fheattreatment,
                    Fcooldown = rolling.Fcooldown
                };
                //context.Sli_bd_process_object.Add(header);
                await context.SaveChangesAsync();
                var datas = new
                {
                    code = 200,
                    msg = "Success",
                    Date = rolling.ToString() + "保存成功"
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
        //  调用查询接口（sli_document_mp_rolling_view），修改后，提交Update 接口，修改相应信息
        public async Task<object> mp_rolling_Update([Microsoft.AspNetCore.Mvc.FromBody] sli_document_mp_rolling objct)
        {
            try
            {
                var context = new YourDbContext();
                var entity = await context.sli_document_mp_rolling.FindAsync(objct);
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

                    var sli_document_mp_rolling = context.sli_document_mp_rolling.FirstOrDefault(p => p.Id == objct.Id);
                    //var Sli_plan_modelEntrys = _context.Sli_plan_modelEntry.Where(p => p.fmodelID == model.Id).ToList();


                    sli_document_mp_rolling.Fsliallowanceid = objct.Fsliallowanceid;
                    //sli_document_mp_rolling.Fsliallowanceod = object.Fsliallowanceod;
                    //sli_document_mp_rolling.fsliallowanceh = object.fsliallowanceh;
                    //sli_document_mp_rolling.fstatus = option.fstatus;
                    //sli_document_mp_rolling.fused = option.fused;
                    //sli_document_mp_rolling.fcreateDate = option.fcreateDate;
                    ////Sli_bd_tech_options.fnote = model.fnote;

                    await context.SaveChangesAsync();

                    var datas = new
                    {
                        code = 200,
                        msg = "ok",
                        date = sli_document_mp_rolling.Id + "修改成功！"
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

        //  工艺文件删除接口，根据Id 删除  ---前台删除控制，后台操作逻辑  
        public async Task<object> mp_rolling_Delete(List<int> id)
        {
            try
            {
                var context = new YourDbContext();
                foreach (var deleteid in id)
                {

                    var entity = await context.sli_document_mp_rolling.FindAsync(deleteid);
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
                    context.sli_document_mp_rolling.RemoveRange();


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

        //   查询页面   查询接口

        [System.Web.Http.HttpGet]
        public IHttpActionResult GetTableBymp_rolling(int page = 1, int pageSize = 10, string Fnumber = null)
        {
            var context = new YourDbContext();
            IQueryable<sli_document_mp_rolling> query = context.sli_document_mp_rolling;

            try
            {
                // 更严格的 FNumber 检查，确保不为 null 或空
                if (!string.IsNullOrWhiteSpace(Fnumber))
                {
                    query = query.Where(q => q.Fnumber.Contains(Fnumber));
                }

                // 检查查询结果是否为空
                if (!query.Any())
                {
                    return Json(new
                    {
                        code = 404,
                        msg = "No records found for the given criteria",
                        data = new
                        {
                            totalCounts = 0,
                            totalPagess = 0,
                            currentPages = page,
                            pageSizes = pageSize,
                            data = new List<sli_document_mp_rolling>()
                        }
                    });
                }

                int totalCount = query.Count();

                int totalPages = 0;
                if (totalCount > 0)
                {
                    // 只有当总记录数大于 0 时才计算页数
                    totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
                }

                var paginatedQuery = Enumerable.Empty<sli_document_mp_rolling>();
                if (totalCount > 0)
                {
                    // 只有当总记录数大于 0 时才进行分页查询
                    paginatedQuery = query.OrderByDescending(b => b.Id).Skip((page - 1) * pageSize).Take(pageSize);
                }

                // 检查分页查询结果是否为空
                if (!paginatedQuery.Any())
                {
                    return Json(new
                    {
                        code = 404,
                        msg = "No records found for the given page and page size",
                        data = new
                        {
                            totalCounts = totalCount,
                            totalPagess = totalPages,
                            currentPages = page,
                            pageSizes = pageSize,
                            data = new List<sli_document_mp_rolling>()
                        }
                    });
                }

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
                        data = paginatedQuery.ToList()
                    }
                };

                return Json(response);
            }
            catch (Exception ex)
            {
                // 捕获异常并返回错误信息
                return Json(new
                {
                    code = 500,
                    msg = "An error occurred while processing the request: " + ex.Message,
                    data = new
                    {
                        totalCounts = 0,
                        totalPagess = 0,
                        currentPages = page,
                        pageSizes = pageSize,
                        data = new List<sli_document_mp_rolling>()
                    }
                });
            }
        }
    }
}