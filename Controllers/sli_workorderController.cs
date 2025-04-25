using Kingdee.BOS.WebApi.DataEntities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebApi_SY.Controllers
{
    public class sli_workorderController : ApiController
    {
        public sli_workorderController()
        {
            // _context = context;

        }
        [System.Web.Http.HttpPost]
        public async Task<object> Insert([Microsoft.AspNetCore.Mvc.FromBody] sli_work_order model)
        {
            try
            {
                var context = new YourDbContext();

                var header = new sli_work_order
                {
                    Fbillno = model.Fbillno,
                    Fdate = model.Fdate,
                    Fqty = model.Fqty,
                    Fweight = model.Fweight,
                    Fplanstart = model.Fplanstart,
                    Fplanend = model.Fplanend,
                    Fordertype = model.Fordertype,
                    
                    sli_work_orderEntry = model.sli_work_orderEntry.Select(d => new sli_work_orderEntry
                    {
                        Id = model.Id,
                        Fseq = d.Fseq,
                        Forderentryid = d.Forderentryid,
                        Forderid = d.Forderid,
                        Fworkorderlistid = d.Fworkorderlistid,

                    }).ToList()
                };



                context.Sli_work_order.Add(header);
                await context.SaveChangesAsync();
                var dataNull = new
                {
                    code = 200,
                    msg = "Success",
                    modelid = header.Id,
                    Date = header.Id.ToString() + "保存成功"

                };
                return dataNull;
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex.ToString());
            }


        }
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetHeaderWorkorder(int page = 1, int pageSize = 10)
        {
            var context = new YourDbContext();
            var query = context.Sli_work_order;
            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedQuery = query.OrderByDescending(b => b.Id).Skip((page - 1) * pageSize).Take(pageSize);
            var result = paginatedQuery.Select(a => new
            {
                id = a.Id,
                Fbillno = a.Fbillno,
                Fdate = a.Fdate,
                Fqty = a.Fqty,
                Fweight = a.Fweight,
                Fplanstart = a.Fplanstart,
                Fplanend = a.Fplanend,
                Fordertype = a.Fordertype,
                Fforgeqty = a.Fforgeqty,
                Fforgeweight = a.Fforgeweight,


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


        public IHttpActionResult GetTableWorkorder(int page = 1, int pageSize = 10)
        {
            var context = new YourDbContext();

            var query = context.Sli_work_order.Include(a => a.sli_work_orderEntry);
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
                id = a.Id,
                Fbillno = a.Fbillno,
                Fdate = a.Fdate,
                Fqty = a.Fqty,
                Fweight = a.Fweight,
                Fplanstart = a.Fplanstart,
                Fplanend = a.Fplanend,
                Fordertype = a.Fordertype,
                Sli_plan_modelEntry = a.sli_work_orderEntry.Select(b => new
                {
                    Fentryid = b.Fentryid,
                    Id = b.Id,
                    Fworkorderlistid = b.Fworkorderlistid,
                    Fseq = b.Fseq,
                    Fqty = b.Fqty,
                    Fcommitqty = b.Fcommitqty,
                    Forderid = b.Forderid,
                    Forderentryid = b.Forderentryid,
                    Fstatus = b.Fstatus,
                    Fclosed = b.Fclosed,
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

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public IHttpActionResult GetTableprocess(int page = 1, int pageSize = 10, int? id = null)
        {
            var context = new YourDbContext();

            IQueryable<sli_work_orderprocess_view> query = context.Sli_work_orderprocess_view;
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
            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedQuery = query.OrderByDescending(b => b.Id).Skip((page - 1) * pageSize).Take(pageSize);

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

            return Ok(response);
        }


        [Microsoft.AspNetCore.Mvc.HttpGet]
        public IHttpActionResult GetTablequality_pur(int page = 1, int pageSize = 10, string FBatchNo = null)
        {
            var context = new YourDbContext();

            IQueryable<sli_quality_pur_view> query = context.Sli_quality_pur_view;
            //var query = from p in context.Sli_work_order
            //            join c in context.Sli_work_orderEntry on p.Id equals c.Id
            //            select new
            //            {
            //                Sli_work_order = p,
            //                Sli_work_orderEntry = c
            //            };
            if (!string.IsNullOrEmpty(FBatchNo))
            {
                query = query.Where(q => q.FBatchNo.Contains(FBatchNo));
            }
            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedQuery = query.OrderByDescending(b => b.Id).Skip((page - 1) * pageSize).Take(pageSize);
            var result = paginatedQuery.Select(a => new
            {
                a.Id,
                a.Fheatnumber,
                a.Fitems,
                a.Fvalue,
                a.FBatchNo,
                a.Fsid
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

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public IHttpActionResult GetTablequality_techmetal(int page = 1, int pageSize = 10, int? id = null)
        {
            var context = new YourDbContext();

            IQueryable<sli_quality_techmetal_view> query = context.Sli_quality_techmetal_view;
            //var query = from p in context.Sli_work_order
            //            join c in context.Sli_work_orderEntry on p.Id equals c.Id
            //            select new
            //            {
            //                Sli_work_order = p,
            //                Sli_work_orderEntry = c
            //            };
            if (id.HasValue)
            {
                query = query.Where(t => t.FWorkOrderListId == id.Value);
            }
            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedQuery = query.OrderByDescending(b => b.FEntryId).Skip((page - 1) * pageSize).Take(pageSize);
            var result = paginatedQuery.Select(a => new
            {
                a.FWorkOrderListId,
                a.FEntryId,
                a.FNumber,
                a.FName,
                a.FMin,
                 a.FMax,
                 a.FTarget
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


        public IHttpActionResult GetTableWorkorder_view(int page = 1, int pageSize = 10,string FBillno=null)
        {
            var context = new YourDbContext();

            IQueryable<sli_work_orders_view> query = context.Sli_work_orders_view;

            if (!string.IsNullOrEmpty(FBillno))
            {
                query = query.Where(q => q.Fwobillno.Contains(FBillno));
            }

            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedQuery = query.OrderByDescending(d => d.Fwoid).Skip((page - 1) * pageSize).Take(pageSize);
            //var result = paginatedQuery.Select(a => new
            //{
            //    id = a.Id,
            //    Fbillno = a.Fbillno,
            //    Fdate = a.Fdate,
            //    Fqty = a.Fqty,
            //    Fweight = a.Fweight,
            //    Fplanstart = a.Fplanstart,
            //    Fplanend = a.Fplanend,
            //    Fordertype = a.Fordertype,
            //    Sli_plan_modelEntry = a.sli_work_orderEntry.Select(b => new
            //    {
            //        Fentryid = b.Fentryid,
            //        Id = b.Id,
            //        Fworkorderlistid = b.Fworkorderlistid,
            //        Fseq = b.Fseq,
            //        Fqty = b.Fqty,
            //        Fcommitqty = b.Fcommitqty,
            //        Forderid = b.Forderid,
            //        Forderentryid = b.Forderentryid,
            //        Fstatus = b.Fstatus,
            //        Fclosed = b.Fclosed,
            //    })

            //});

            var result = paginatedQuery.Select(a => new
            {
                Fwoid = a.Fwoid,
                Fwobillno = a.Fwobillno,
                Fproductno = a.Fproductno,
                Fwodate = a.Fwodate,
                Fqty = a.Fqty,
                Fwoweight = a.Fwoweight,
                Fwoplanstart = a.Fwoplanstart,
                Fwoplanend = a.Fwoplanend,
                Fwoordertype = a.Fwoordertype,
                Fwoentryqty = a.Fwoentryqty ?? 0,
                Fwoecommitqty = a.Fwoecommitqty ?? 0,
                Fwoestatus = a.Fwoestatus ?? 0,
                Fwoeclosed = a.Fwoeclosed ?? 0,
                Fworkordlistid = a.Fworkordlistid,
                Fworkqty = a.Fworkqty,
                Fworkweight = a.Fworkweight,
                Forderbillno = a.Forderbillno,
                Fplandeleliverydate = a.Fplandeleliverydate,
                Fslitemperatureend = a.Fslitemperatureend,
                Fcustid = a.Fcustid,
                Fcustno = a.Fcustno,
                Fcustname = a.Fcustname,
                Fcustomer = a.Fcustomer,
                Fid = a.Fid,
                Fentryid = a.Fentryid,
                Fseq = a.Fseq,
                // --注意这里可能是 Fsliheight 的拼写错误 
                Forderqty = a.Forderqty,
                Fnote = a.Fnote,
                Fplandeliverydate = a.Fplandeliverydate,
                Fmaterialid = a.Fmaterialid,
                Fnumber = a.Fnumber,
                Fname = a.Fname,
                Fdescription = a.Fdescription,
                Fsliouterdiameter = a.Fsliouterdiameter,
                Fslihight = a.Fslihight,
                Fsliallowanceod = a.Fsliallowanceod,
                Fsliallowanceid = a.Fsliallowanceid,
                Fsliallowanceh = a.Fsliallowanceh,
                //--注意这里可能是 Fsliblankmodel 的拼写错误
                Fsliweightmaterial = a.Fsliweightmaterial,
                Fsliweightforging = a.Fsliweightforging,
                Fsliweightgoods = a.Fsliweightgoods,
                Fslidrawingno = a.Fslidrawingno,
                Fslimetal = a.Fslimetal,
                Fsligoodsstatus = a.Fsligoodsstatus,
                Fsliprocessing = a.Fsliprocessing,
                Fslidelivery = a.Fslidelivery,
                Fsliblankmodel = a.Fsliblankmodel,
                Fslipunching = a.Fslipunching,
                Fslitemperaturebegin = a.Fslitemperaturebegin,
                Fslimould = a.Fslimould,
                Fsliroller = a.Fsliroller,
                Fsliheatingtimes = a.Fsliheatingtimes,
                Fsligrade = a.Fsligrade,
                Fsumnumber = a.Fsumnumber,
                Fsoqty = a.Fsoqty,
                Fwoqty = a.Fwoqty,
                Fwpqty = a.Fwpqty,
                Ffinisthqty = a.Ffinisthqty,
                Fstockqty = a.Fstockqty,
                Foption = a.Foption,
                Fobject = a.Fobject,
                Fmo = a.Fmo,
                Fworkorderliststatus = a.Fworkorderliststatus,
                Fpause = a.Fpause,
                Fcancel = a.Fcancel,
                // 添加其他需要的字段
            }).ToList();
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