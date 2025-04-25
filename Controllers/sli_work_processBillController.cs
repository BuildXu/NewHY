using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
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
                   
                    

                    foreach (var id in root.Fworkordlistid)
                    {
                        if (root.sli_workorderlist_view != null)
                        {
                            var seq = 1;
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

                // 处理 sli_workorderlist_view 插入逻辑




                //var entry = new sli_work_processBillEntry
                //{
                //    Fbillid = header.Id,
                //};
                //context.Sli_work_processBillEntry.Add(entry);
                //await context.SaveChangesAsync();

                var dataNull = new
                {
                    code = 200,
                    msg = "Success",
                    //modelid = model.Fworkorderlistid,
                    //Date = header.Id.ToString() + "保存成功"

                };
                return Ok(dataNull);
            }
            catch (Exception ex)
            {
                return Ok(ex);
                //return JsonConvert.SerializeObject(ex.ToString());
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

            var query = context.Sli_work_processBill.Include(a => a.sli_work_processBillEntry) ;
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
                sli_work_processBillEntry = a.sli_work_processBillEntry.Select(b => new
                {
                    Fbillid = b.Fbillid,
                    Fentryid = b.Fentryid,
                    Fworkorderlistid=b.Fworkorderlistid,// ********1.14 增加
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
                Fstatus = a.Fstatus


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

        public IHttpActionResult GetTableWorkprocessBill_view( int ? id=null)
        {
            var context = new YourDbContext();

            IQueryable<sli_work_processBill_view> query = context.Sli_work_processBill_view.Include(a => a.sli_work_processBillEntry_view);
            //var query = from p in context.Sli_work_order
            //            join c in context.Sli_work_orderEntry on p.Id equals c.Id
            //            select new
            //            {
            //                Sli_work_order = p,
            //                Sli_work_orderEntry = c
            //            };
            if (id.HasValue)
            {
                query = query.Where(t => t.Id == id.Value);
            }

            var result = query.Select(a => new
            {
                id = a.Id,
                Fseq = a.Fseq,
                Fworkorderlistid = a.Fworkorderlistid,
                Fprocessoption = a.Fprocessoption,
                Fname = a.Fname,
                foptionname = a.Foptionname,
                Fstartdate = a.Fstartdate,
                Fenddate = a.Fenddate,
                Fqty = a.Fqty,
                Fweight = a.Fweight,
                Fcommitqty = a.Fcommitqty,
                Fcommitweight = a.Fcommitweight,
                Fstatus = a.Fstatus,
                Fsourceid = a.Fsourceid,
                sli_work_processBillEntry_view = a.sli_work_processBillEntry_view.Select(b => new
                {
                    Fbillid = b.Fbillid,
                    Fentryid = b.Fentryid,    //Fseq
                    Fseq = b.Fseq ?? 0,    //
                    Fwobillid = b.Fwobillid ?? 0,// ********1.14 增加
                    Fworkorderlistid = a.Fworkorderlistid ,
                    Fproductno = b.Fproductno ?? string.Empty,  //Fmaterialname
                    Fmaterialnumber = b.Fmaterialnumber ,  //Fmaterialname
                    Fmaterialname = b.Fmaterialname ?? string.Empty,  //Fmaterialname
                    Fdescription = b.Fdescription ?? string.Empty,  //Fmaterialname
                    Fprocessobject = b.Fprocessobject,  //
                    Fprocessobjectnumber = b.Fprocessobjectnumber,  //
                    Fprocessobjectname = b.Fprocessobjectname,  //
                    Fstartdate = b.Fstartdate,
                    Fenddate = b.Fenddate,
                    Fqty = b.Fqty,
                    Fweight = b.Fweight,
                    Fcommitqty = b.Fcommitqty,
                    Fcommitweight = b.Fcommitweight,
                    Fqualityoption = b.Fqualityoption,
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

    }
}

