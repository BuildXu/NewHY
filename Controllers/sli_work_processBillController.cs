using DocumentFormat.OpenXml.Presentation;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi_SY.Entity;
using WebApi_SY.Models;

namespace WebApi_SY.Controllers
{
    public class sli_work_processBillController : ApiController
    {
        public sli_work_processBillController()
        {
            // _context = context;

        }

        //----------------------
        [System.Web.Http.HttpPost]
        public IHttpActionResult Inserts()
        {
            try
            {
                var context = new YourDbContext();
                var json = Request.Content.ReadAsStringAsync().Result;
                var root = JsonConvert.DeserializeAnonymousType(json, new
                {
                    Fworkordlistid = new List<WorkOrderListIds>(),
                    sli_workorderlist_view = new List<SliWorkorderlistView>()
                });

                var outerEntities = new List<sli_work_processBill>();
                var innerEntities = new List<sli_work_processBillEntry>();

                if (root.Fworkordlistid != null && root.sli_workorderlist_view != null)
                {
                    // 收集所有外层实体
                    foreach (var id in root.Fworkordlistid)
                    {
                        int seq = 1;
                        foreach (var item in root.sli_workorderlist_view)
                        {
                            outerEntities.Add(new sli_work_processBill
                            {
                                Fworkorderlistid = id.id,
                                Fprocessoption = item.Foptionid,
                                Fstatus = item.Fstatus,
                                Fseq = seq,
                                Fqty = id.Fqty,
                                Fweight = id.Fweight,
                            });
                            seq++;
                        }
                    }

                    // 批量插入外层实体
                    context.Sli_work_processBill.AddRange(outerEntities);
                    context.SaveChanges(); // 此时生成所有外层实体的Id

                    // 收集所有内层实体
                    foreach (var id in root.Fworkordlistid)
                    {
                        int seq = 1;
                        foreach (var item in root.sli_workorderlist_view)
                        {
                            // 查找对应的外层实体（通过顺序索引）
                            var outerIndex = (seq - 1) + (root.sli_workorderlist_view.Count * root.Fworkordlistid.IndexOf(id));
                            var outerEntity = outerEntities[outerIndex];

                            if (item.sli_document_process_modelBillEntry_view != null)
                            {
                                int entrySeq = 1;
                                foreach (var innerItem in item.sli_document_process_modelBillEntry_view)
                                {
                                    innerEntities.Add(new sli_work_processBillEntry
                                    {
                                        Fbillid = outerEntity.Id,
                                        Fprocessobject = innerItem.Fobjectid,
                                        Fseq = entrySeq,
                                        Fqty = id.Fqty,
                                        Fweight = id.Fweight,
                                        Fworkorderlistid = id.id
                                    });
                                    entrySeq++;
                                }
                            }
                            seq++;
                        }
                    }

                    // 批量插入内层实体
                    context.Sli_work_processBillEntry.AddRange(innerEntities);
                    context.SaveChanges();
                }

                return Ok(new { code = 200, msg = "Success" });
            }
            catch (Exception ex)
            {
                // 建议返回错误信息时使用错误状态码
                return Content(HttpStatusCode.InternalServerError, new { code = 500, msg = ex.Message });
            }
        }


        //-----
        [System.Web.Http.HttpPost]
        public IHttpActionResult Insert()
        {
            try
            {
                var context = new YourDbContext();
                var json = Request.Content.ReadAsStringAsync().Result;
                var root = JsonConvert.DeserializeAnonymousType(json, new
                {
                    Fworkordlistid = new List<WorkOrderListIds>(),
                    sli_workorderlist_view = new List<SliWorkorderlistView>()
                });

                // 处理 Fworkordlistid 插入逻辑
                if (root.Fworkordlistid != null)
                {

                    //var entryseq = 1;

                    foreach (var id in root.Fworkordlistid)
                    {
                        var seq = 1;
                        if (root.sli_workorderlist_view != null)
                        {
                            foreach (var item in root.sli_workorderlist_view)
                            {
                                // 先插入外层对象
                                var outerEntity = new sli_work_processBill
                                {
                                    Fworkorderlistid = id.id,
                                    Fprocessoption = item.Foptionid,
                                    Fstatus = item.Fstatus,
                                    Fseq = seq,
                                    Fqty = id.Fqty,
                                    Fweight = id.Fweight,
                                    //Fstartdate=DateTime.Today,
                                    //Fs = DateTime.Today,
                                };
                                var outerSaved = context.Sli_work_processBill.Add(outerEntity);
                                context.SaveChanges();

                                // 再插入内层对象
                                if (item.sli_document_process_modelBillEntry_view != null)
                                {
                                    var entryseq = 1;
                                    foreach (var innerItem in item.sli_document_process_modelBillEntry_view)
                                    {
                                        var innerEntity = new sli_work_processBillEntry
                                        {
                                            Fbillid = outerEntity.Id,
                                            Fprocessobject = innerItem.Fobjectid,
                                            Fseq = entryseq,
                                            Fqty = id.Fqty,
                                            Fweight = id.Fweight,
                                            Fworkorderlistid = id.id
                                        };

                                        context.Sli_work_processBillEntry.Add(innerEntity);
                                        entryseq++;
                                    }
                                }
                                context.SaveChanges();

                                seq++;
                            }
                        }

                    }
                }

                var dataNull = new
                {
                    code = 200,
                    msg = "Success",

                };
                return Ok(dataNull);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }


        [System.Web.Http.HttpPost]
        public async Task<object> sli_work_processBill_Delete(List<int> id)
        {
            try
            {
                var context = new YourDbContext();
                foreach (var deleteid in id)
                {
                    var entity = await context.Sli_work_processBill.FindAsync(deleteid);
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
                    context.Sli_work_processBill.Remove(entity);
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
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<object> Update([Microsoft.AspNetCore.Mvc.FromBody] sli_work_processBill model)
        {
            try
            {
                var context = new YourDbContext();
                var entity = await context.Sli_work_processBill.FindAsync(model.Id);
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


                    var Sli_plan_models = context.Sli_work_processBill.FirstOrDefault(p => p.Id == model.Id);
                    var Sli_plan_modelEntrys = context.Sli_work_processBillEntry.Where(p => p.Fbillid == model.Id).ToList();


                    Sli_plan_models.Fseq = model.Fseq;
                    Sli_plan_models.Fworkorderlistid = model.Fworkorderlistid;
                    Sli_plan_models.Fprocessoption = model.Fprocessoption;
                    //Sli_plan_models.Fstartdate = model.Fstartdate;
                    //Sli_plan_models.Fenddate = model.Fenddate;
                    if (model.Fstartdate != null)
                    {
                        Sli_plan_models.Fstartdate = model.Fstartdate.Value;
                    }

                    if (model.Fenddate != null)
                    {
                        Sli_plan_models.Fenddate = model.Fenddate.Value;
                    }

                    Sli_plan_models.Fqty = model.Fqty;
                    Sli_plan_models.Fweight = model.Fweight;
                    Sli_plan_models.Fcommitqty = model.Fcommitqty;
                    Sli_plan_models.Fcommitweight = model.Fcommitweight;
                    Sli_plan_models.Fstatus = model.Fstatus;
                    context.Sli_work_processBillEntry.RemoveRange(Sli_plan_modelEntrys);

                    foreach (var childTableData in model.sli_work_processBillEntry)
                    {

                        var entry = new sli_work_processBillEntry
                        {
                            Fbillid = model.Id,
                            Fseq = childTableData.Fseq,
                            Fworkorderlistid = childTableData.Fworkorderlistid,
                            Fprocessobject = childTableData.Fprocessobject,
                            Fqualityoption = childTableData.Fqualityoption,
                            Fstartdate = childTableData.Fstartdate,
                            Fenddate = childTableData.Fenddate,

                            Fqty = childTableData.Fqty,
                            Fweight = childTableData.Fweight,
                            Fcommitqty = childTableData.Fcommitqty,
                            Fcommitweight = childTableData.Fcommitweight,
                            Fstatus = childTableData.Fstatus
                        };
                        context.Sli_work_processBillEntry.Add(entry);
                    }
                    await context.SaveChangesAsync();

                    var datas = new
                    {
                        code = 200,
                        msg = "ok",
                        date = model
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
        public IHttpActionResult GetTableWorkprocessBillall(int? id = null)
        {
            var context = new YourDbContext();

            var query = context.Sli_work_processBill.Include(a => a.sli_work_processBillEntry);
            //var query = from p in context.Sli_work_order
            //            join c in context.Sli_work_orderEntry on p.Id equals c.Id
            //            select new
            //            {
            //                Sli_work_order = p,
            //                Sli_work_orderEntry = c
            //            };
            if (id.HasValue)
            {
                query = query.Where(q => q.Id == id.Value);
            }


            var result = query.Select(a => new
            {
                Fwoentryid = a.Fwoentryid,
                Id = a.Id,
                Fseq = a.Fseq,
                Fworkorderlistid = a.Fworkorderlistid,
                Fprocessoption = a.Fprocessoption,
                Fstartdate = a.Fstartdate,
                Fenddate = a.Fenddate,
                Fqty = a.Fqty,
                Fweight = a.Fweight,
                Fcommitqty = a.Fcommitqty,
                Fcommitweight = a.Fcommitweight,
                Fstatus = a.Fstatus,
                //Forderno=a.forder ?? string.Empty,
                //Fproductno=a.Fproductno ?? string.Empty,
                //Fpname=a.Fpname ?? string.Empty,
                //Fdescription=a.Fdescription ?? string.Empty,

                sli_work_processBillEntry = a.sli_work_processBillEntry.Select(b => new
                {
                    Fbillid = b.Fbillid,
                    Fentryid = b.Fentryid,
                    Fworkorderlistid = b.Fworkorderlistid,// ********1.14 增加
                    Fseq = b.Fseq,
                    Fwobillid = b.Fwobillid,
                    Fprocessobject = b.Fprocessobject,
                    Fstartdate = b.Fstartdate,
                    Fenddate = b.Fenddate,
                    Fqty = b.Fqty,
                    Fweight = b.Fweight,
                    Fcommitqty = b.Fcommitqty,
                    Fcommitweight = b.Fcommitweight,
                    Fstatus = b.Fstatus
                })

            });
            var response = new    // 定义 前端返回数据  总记录，总页，当前页 ，size,返回记录
            {
                code = 200,
                msg = "OK",
                data = new
                {

                    data = result
                }


            };

            return Ok(response);
        }

        public IHttpActionResult GetTableWorkprocessBill(int page = 1, int pageSize = 10)
        {
            var context = new YourDbContext();

            var query = context.Sli_work_processBill;
            //var query = from p in context.Sli_work_order
            //            join c in context.Sli_work_orderEntry on p.Id equals c.Id
            //            select new
            //            {
            //                Sli_work_order = p,
            //                Sli_work_orderEntry = c
            //            };

            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedQuery = query.OrderByDescending(b => b.Id).Skip((page - 1) * pageSize).Take(pageSize);
            var result = paginatedQuery.Select(a => new
            {
                Fwoentryid = a.Fwoentryid,
                Id = a.Id,
                Fseq = a.Fseq,
                Fworkorderlistid = a.Fworkorderlistid,
                Fprocessoption = a.Fprocessoption,
                Fstartdate = a.Fstartdate,
                Fenddate = a.Fenddate,
                Fqty = a.Fqty,
                Fweight = a.Fweight,
                Fcommitqty = a.Fcommitqty,
                Fcommitweight = a.Fcommitweight,
                Fstatus = a.Fstatus,
                //Forderno = a.Forderno,
                //Fproductno = a.Fproductno,
                //Fpname = a.Fpname,
                //Fdescription = a.Fdescription


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
        //public IHttpActionResult GetTableWorkprocessBill_view(int? id = null, string foptionname = null)
        //{
        //    var context = new YourDbContext();

        //    IQueryable<sli_work_processBill_view> query = context.Sli_work_processBill_view
        //        .Include(a => a.sli_work_processBillEntry_view);

        //    // 1. 添加 id 过滤条件
        //    if (id.HasValue)
        //    {
        //        query = query.Where(t => t.id == id.Value);
        //    }

        //    // 2. 添加 foptionname 过滤条件（精确匹配）
        //    if (!string.IsNullOrEmpty(foptionname))
        //    {
        //        query = query.Where(t => t.foptionname == foptionname);
        //    }

        //    // 3. 构建返回结果
        //    var result = query.Select(a => new
        //    {
        //        id = a.id,
        //        Fseq = a.Fseq,
        //        Fworkorderlistid = a.Fworkorderlistid,
        //        Fprocessoption = a.Fprocessoption,
        //        Fname = a.Fname,
        //        foptionname = a.foptionname,
        //        Fstartdate = a.Fstartdate,
        //        Fenddate = a.Fenddate,
        //        Fqty = a.Fqty,
        //        Fweight = a.Fweight,
        //        Fcommitqty = a.Fcommitqty,
        //        Fcommitweight = a.Fcommitweight,
        //        Fstatus = a.Fstatus,
        //        Fsourceid = a.Fsourceid,
        //        sli_work_processBillEntry_view = a.sli_work_processBillEntry_view.Select(b => new
        //        {
        //            Fbillid = b.Fbillid,
        //            Fentryid = b.Fentryid,
        //            Fseq = b.Fseq ?? 0,
        //            Fwobillid = b.Fwobillid ?? 0,
        //            Fworkorderlistid = a.Fworkorderlistid,
        //            Fproductno = b.Fproductno ?? string.Empty,
        //            Fmaterialnumber = b.Fmaterialnumber ?? string.Empty,
        //            Fmaterialname = b.Fmaterialname ?? string.Empty,
        //            Fdescription = b.Fdescription ?? string.Empty,
        //            Fprocessobject = b.Fprocessobject,
        //            Fprocessobjectnumber = b.Fprocessobjectnumber,
        //            Fprocessobjectname = b.Fprocessobjectname,
        //            Fstartdate = b.Fstartdate,
        //            Fenddate = b.Fenddate,
        //            Fqty = b.Fqty,
        //            Fweight = b.Fweight,
        //            Fcommitqty = b.Fcommitqty,
        //            Fcommitweight = b.Fcommitweight,
        //            Fqualityoption = b.Fqualityoption,
        //            Fstatus = b.Fstatus
        //        })
        //    });

        //    // 4. 构建响应对象
        //    var response = new
        //    {
        //        code = 200,
        //        msg = "OK",
        //        data = new
        //        {
        //            data = result
        //        }
        //    };

        //    return Ok(response);
        //}

        public IHttpActionResult GetTableWorkprocessBill_view(
        int? id = null,
        int? Fstatus = null,
        string foptionname = null,
        string Fwobillno = null,
        int page = 1,          // 新增分页参数
        int pageSize = 10)     // 默认每页10条
        {
            // 参数合法性校验
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            var context = new YourDbContext();

            // 基础查询
            //IQueryable<sli_work_processBill_view> query = context.Sli_work_processBill_view
            //    .Include(a => a.sli_work_processBillEntry_view);
            IQueryable<sli_work_processBill_view> query = context.Sli_work_processBill_view
                  .Include(a => a.sli_work_processBillEntry_view)
                  .OrderBy(q => q.Fwobillno)    // 先按 Fwobillno 正序排序
                  .ThenBy(q => q.Fseq);         // 再按 Fseq 正序排序（当 Fwobillno 相同时）

            // 条件过滤
            if (id.HasValue)
            {
                query = query.Where(t => t.id == id.Value);
            }

            if (Fstatus.HasValue)
            {
                query = query.Where(t => t.Fstatus == Fstatus.Value);
            }

            if (!string.IsNullOrEmpty(foptionname))
            {
                query = query.Where(t => t.foptionname == foptionname);
            }
            if (!string.IsNullOrEmpty(Fwobillno))
            {
                query = query.Where(t => t.Fwobillno == Fwobillno);
            }

            // 计算总记录数（在分页前）
            int total = query.Count();

            // 分页处理
            int skip = (page - 1) * pageSize;
            var pagedQuery = query
                .OrderByDescending(t => t.id)  // 必须指定排序规则
                .Skip(skip)
                .Take(pageSize);

            // 结果映射
            var result = pagedQuery.Select(a => new
            {
                id = a.id,
                Fseq = a.Fseq,
                Fwobillno = a.Fwobillno,
                Fslimetal = a.Fslimetal,
                Fweights = a.Fweights,
                Fworkorderlistid = a.Fworkorderlistid,
                Fprocessoption = a.Fprocessoption,
                Fname = a.Fname,
                foptionname = a.foptionname,
                Fstartdate = a.Fstartdate,
                Fenddate = a.Fenddate,
                Fqty = a.Fqty,
                Fweight = a.Fweight,
                Fcommitqty = a.Fcommitqty,
                Fcommitweight = a.Fcommitweight,
                Fstatus = a.Fstatus,
                Fsourceid = a.Fsourceid,
                Forderno = a.Forderno,
                Fproductno = a.Fproductno,
                Fpname = a.Fpname,
                Fdescription = a.Fdescription,
                Fothers = a.Fothers,
                sli_work_processBillEntry_view = a.sli_work_processBillEntry_view.Select(b => new
                {
                    Fbillid = b.Fbillid,
                    Fentryid = b.Fentryid,
                    Fseq = b.Fseq ,
                    Fwobillid = b.Fwobillid ,
                    Fworkorderlistid = a.Fworkorderlistid,
                    Fproductno = b.Fproductno ?? string.Empty,
                    Fmaterialnumber = b.Fmaterialnumber ,
                    Fmaterialname = b.Fmaterialname ?? string.Empty,
                    Fdescription = b.Fdescription ?? string.Empty,
                    Fprocessobject = b.Fprocessobject,
                    Fprocessobjectnumber = b.Fprocessobjectnumber,
                    Fprocessobjectname = b.Fprocessobjectname,
                    Fstartdate = b.Fstartdate,
                    Fenddate = b.Fenddate,
                    Fqty = b.Fqty,
                    Fweight = b.Fweight,
                    Fcommitqty = b.Fcommitqty,
                    Fcommitweight = b.Fcommitweight,
                    Fqualityoption = b.Fqualityoption,
                    Fstatus = b.Fstatus
                    // ...其他字段保持不变
                })
            }).ToList();  // 立即执行查询

            // 构建响应
            var response = new
            {
                code = 200,
                msg = "OK",
                data = new
                {
                    total = total,          // 总记录数
                    currentPage = page,     // 当前页码
                    pageSize = pageSize,    // 每页数量
                    totalPages = (int)Math.Ceiling(total / (double)pageSize),  // 总页数
                    data = result           // 分页数据
                }
            };

            return Ok(response);
        }

        public IHttpActionResult Getsli_wo_all(
            int? Id = null,
            string Fcustname = null,
            string Fbillno = null,
            string Forderno = null,
            DateTime? FdateFrom = null,
            DateTime? FdateTo = null,
            string Fordertype = null,
            int? Fforgeqty = null,
            int page = 1,
            int pageSize = 10)
        {
            try
            {
                if (page < 1) page = 1;
                if (pageSize < 1 || pageSize > 100) pageSize = 10;

                using (var context = new YourDbContext())
                {
                    IQueryable<sli_wo_view> query = context.sli_wo_view
                        .OrderByDescending(q => q.Fdate);

                    // 动态过滤条件保持不变...
                    // 你的原始过滤代码

                    int totalCount = query.Count();
                    int skip = (page - 1) * pageSize;

                    // 先执行分页查询并将结果加载到内存
                    var pagedList = query
                        .Skip(skip)
                        .Take(pageSize)
                        .ToList(); // 关键点：先物化到内存

                    // 在内存中进行类型转换
                    var pagedData = pagedList.Select(a => new
                    {
                        a.Id,
                        Fcustname = a.Fcustname ?? string.Empty,
                        Fbillno = a.Fbillno ?? string.Empty,
                        Forderno = a.Forderno ?? string.Empty,
                        a.Fdate,
                        Fqty = a.Fqty,
                        a.Fweight,
                        a.Fplanstart,
                        a.Fplanend,
                        Fordertype = a.Fordertype ?? string.Empty,
                        Fforgeqty = a.Fforgeqty, // 保持数值类型
                        Fforgeweight = a.Fforgeweight,
                        Fname = a.Fname ?? string.Empty,
                        Fslimetal = a.Fslimetal ?? string.Empty,
                        Fdescription = a.Fdescription ?? string.Empty,
                        Fslidrawingno = a.Fslidrawingno ?? string.Empty,
                        Fsliheattreatment = a.Fsliheattreatment ?? string.Empty,
                        Fsliexplanation = a.Fsliexplanation ?? string.Empty,
                        // 处理所有可能为数值类型的字段
                        Fp1name = a.Fp1name ?? string.Empty,  //一,名称
                        Fp1status = a.Fp1status ?? string.Empty, //，状态
                        Fp2name = a.Fp2name ?? string.Empty,
                        Fp2status = a.Fp2status ?? string.Empty, //  二名称，状态
                        Fp3name = a.Fp3name ?? string.Empty,
                        Fp3status = a.Fp3status ?? string.Empty,  //
                        Fp4name = a.Fp4name ?? string.Empty,
                        Fp4status = a.Fp4status ?? string.Empty, //
                        Fp5name = a.Fp5name ?? string.Empty,
                        Fp5status = a.Fp5status ?? string.Empty,  //
                        Fp6name = a.Fp6name ?? string.Empty,  //
                        Fp6status = a.Fp6status ?? string.Empty,  //
                        Fp7name = a.Fp7name ?? string.Empty,  //
                        Fp7status = a.Fp7status ?? string.Empty,  //
                        Fp8name = a.Fp8name ?? string.Empty,  //
                        Fp8status = a.Fp8status ?? string.Empty,  //   //

                    }).ToList();

                    var response = new
                    {
                        code = 200,
                        msg = "OK",
                        data = new
                        {
                            total = totalCount,
                            currentPage = page,
                            pageSize = pageSize,
                            totalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                            items = pagedData
                        }
                    };

                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}


