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
                    Fworkordlistid = new int[] { },
                    sli_workorderlist_view = new List<SliWorkorderlistView>()
                });

                // 处理 Fworkordlistid 插入逻辑
                if (root.Fworkordlistid != null)
                {
                    var seq = 1;
                    var entryseq = 1;

                    foreach (var id in root.Fworkordlistid)
                    {
                        if (root.sli_workorderlist_view != null)
                        {
                            foreach (var item in root.sli_workorderlist_view)
                            {
                                // 先插入外层对象
                                var outerEntity = new sli_work_processBill
                                {
                                    Fworkorderlistid =id,
                                    Fprocessoption = item.Foptionid,
                                    Fstatus = item.Fstatus,
                                    Fseq= seq
                                    //Fstartdate=DateTime.Today,
                                    //Fs = DateTime.Today,
                                };
                                var outerSaved = context.Sli_work_processBill.Add(outerEntity);
                                context.SaveChanges();

                                // 再插入内层对象
                                if (item.sli_document_process_modelBillEntry_view != null)
                                {
                                    foreach (var innerItem in item.sli_document_process_modelBillEntry_view)
                                    {
                                        var innerEntity = new sli_work_processBillEntry
                                        {
                                            Fbillid= outerEntity.Id,
                                            Fprocessobject = innerItem.Fobjectid,
                                            Fseq= entryseq
                                        };
                                        
                                        context.Sli_work_processBillEntry.Add(innerEntity);
                                    }
                                }
                                context.SaveChanges();
                                entryseq++;
                            }
                        }
                        seq++;
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

        public IHttpActionResult GetTableWorkprocessBill(int page = 1, int pageSize = 10)
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
                sli_work_processBillEntry = a.sli_work_processBillEntry.Select(b => new
                {
                    Fbillid = b.Fbillid,
                    Fentryid = b.Fentryid,
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
                    totalCounts = totalCount,
                    totalPagess = totalPages,
                    currentPages = page,
                    pageSizes = pageSize,
                    data = result
                }


            };

            return Ok(response);
        }



    }
}

