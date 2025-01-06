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
                    //Date = header.id.ToString() + "保存成功"
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
        public IHttpActionResult GetTableBymp_rolling(int page = 1, int pageSize = 10, string FNumber = null, string FName = null)
        {
            var context = new YourDbContext();
            IQueryable<WebApi_SY.Models.sli_document_mp_rolling> query = context.sli_document_mp_rolling;

            if (!string.IsNullOrEmpty(FNumber))
            {
                query = query.Where(q => q.Fnumber.Contains(FNumber));
            }

          
            var totalCount = query.Count(); //记录数
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize); // 页数
            var paginatedQuery = query.OrderByDescending(b => b.Id).Skip((page - 1) * pageSize).Take(pageSize); //  某页记录
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
                    data = paginatedQuery
                }
            };

            return Json(response);
        }
    }
}