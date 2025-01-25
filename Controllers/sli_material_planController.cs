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
    public class sli_material_planController : ApiController
    {
        public sli_material_planController()
        {
            // _context = context;
        }

        [System.Web.Http.HttpPost]
        public async Task<object> sli_material_plan_Insert([Microsoft.AspNetCore.Mvc.FromBody] sli_material_plan[] options)
        {
            var context = new YourDbContext();
            try
            {
                foreach (var option in options)
                {

                    var header = new sli_material_plan
                    {
                        // 物料 ID
                        Fmaterialid = option.Fmaterialid,
                        // 批号 来自即时库存
                        Flotno = option.Flotno,
                        // 仓库 ID 来自即时库存
                        Fstockid = option.Fstockid,
                        // 库位 ID 来自即时库存
                        Fstocklocid = option.Fstocklocid,
                        // 库存数量 来自即时库存
                        Fqtystock = option.Fqtystock,
                        // 库存重量 来自即时库存
                        Fweightstock = option.Fweightstock,
                        // 已使用数量 来自即时库存
                        Fqtyused = option.Fqtyused,
                        // 已使用重量 来自即时库存
                        Fweightused = option.Fweightused,
                        // 工单列表 ID 来自工件视图
                        Fworkorderlistid = option.Fworkorderlistid,
                        // 产品编号 来自工件视图
                        Fproductno = option.Fproductno,
                        // 数量 引用 工件视图, 可手工修改
                        Fqty = option.Fqty,
                        // 重量 引用 工件视图, 可手工修改
                        Fweight = option.Fweight,
                        // 实际数量 后期 领料单返写 控制()
                        Fqtyactul = option.Fqtyactul,
                        // 实际下料重量 后期 领料单返写 控制()
                        Fweightactul = option.Fweightactul,
                        // 状态
                        Fstatus = option.Fstatus
                    };
                    context.sli_material_plan.Add(header);
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
        public async Task<object> sli_material_plan_Update([Microsoft.AspNetCore.Mvc.FromBody] sli_material_plan option)
        {
            try
            {
                var context = new YourDbContext();
                var entity = await context.sli_material_plan.FindAsync(option.Id);
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
                    var sli_material_plans = context.sli_material_plan.FirstOrDefault(p => p.Id == option.Id);
                    //var Sli_plan_modelEntrys = _context.Sli_plan_modelEntry.Where(p => p.fmodelID == model.Id).ToList();



                    sli_material_plans.Id = option.Id;
                    sli_material_plans.Fmaterialid = option.Fmaterialid;
                    sli_material_plans.Flotno = option.Flotno;
                    sli_material_plans.Fstockid = option.Fstockid;
                    sli_material_plans.Fstocklocid = option.Fstocklocid;
                    sli_material_plans.Fqtystock = option.Fqtystock;
                    sli_material_plans.Fweightstock = option.Fweightstock;
                    sli_material_plans.Fqtyused = option.Fqtyused;
                    sli_material_plans.Fweightused = option.Fweightused;
                    sli_material_plans.Fworkorderlistid = option.Fworkorderlistid;
                    sli_material_plans.Fproductno = option.Fproductno;
                    sli_material_plans.Fqty = option.Fqty;
                    sli_material_plans.Fweight = option.Fweight;
                    sli_material_plans.Fqtyactul = option.Fqtyactul;
                    sli_material_plans.Fweightactul = option.Fweightactul;
                    sli_material_plans.Fstatus = option.Fstatus;

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
        public async Task<object> sli_material_plans_Delete(List<int> id)
        {
            try
            {
                var context = new YourDbContext();
                foreach (var deleteid in id)
                {
                    var entity = await context.sli_material_plan.FindAsync(deleteid);
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
                    context.sli_material_plan.RemoveRange(entity);
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
        public IHttpActionResult GetTableBysli_material_plan(int page = 1, int pageSize = 10)
        {

            var context = new YourDbContext();
            IQueryable<sli_material_plan> query = context.sli_material_plan;

            
            var totalCount = query.Count(); //记录数
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize); // 页数
            var paginatedQuery = query.OrderByDescending(b => b.Id).Skip((page - 1) * pageSize).Take(pageSize); //  某页记录
            var result = paginatedQuery.Select(a => new
            {
                // 自增主键
                Id = a.Id,
                // 物料 ID
                Fmaterialid = a.Fmaterialid,
                // 批号 来自即时库存
                Flotno = a.Flotno ?? string.Empty,
                // 仓库 ID 来自即时库存
                Fstockid = a.Fstockid,
                // 库位 ID 来自即时库存
                Fstocklocid = a.Fstocklocid,
                // 库存数量 来自即时库存
                Fqtystock = a.Fqtystock,
                // 库存重量 来自即时库存
                Fweightstock = a.Fweightstock,
                // 已使用数量 来自即时库存
                Fqtyused = a.Fqtyused,
                // 已使用重量 来自即时库存
                Fweightused = a.Fweightused,
                // 工单列表 ID 来自工件视图
                Fworkorderlistid = a.Fworkorderlistid,
                // 产品编号 来自工件视图
                Fproductno = a.Fproductno ?? string.Empty,
                // 数量 引用 工件视图, 可手工修改
                Fqty = a.Fqty,
                // 重量 引用 工件视图, 可手工修改
                Fweight = a.Fweight,
                // 实际数量 后期 领料单返写 控制()
                Fqtyactul = a.Fqtyactul,
                // 实际下料重量 后期 领料单返写 控制()
                Fweightactul = a.Fweightactul,
                // 状态
                Fstatus = a.Fstatus


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