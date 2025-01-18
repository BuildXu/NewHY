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
    public class sli_quality_reportController : ApiController
    {
        public sli_quality_reportController()
        {
            // _context = context;
        }
        [System.Web.Http.HttpPost]
        public async Task<object> sli_quality_report_Insert([Microsoft.AspNetCore.Mvc.FromBody] sli_quality_report options)
        {
            var context = new YourDbContext();
            try
            {
                var header = new sli_quality_report
                {
                    Fnumber = options.Fnumber,
                    Fsourceid = options.Fsourceid,
                    Fworkorderlistid = options.Fworkorderlistid,
                    //Fmateril = options.Fmateril,
                    Fprocessobjectid = options.Fprocessobjectid,
                    Fdescription = options.Fdescription,
                    Fstatus = options.Fstatus,
                    sli_quality_reportentry = new List<sli_quality_reportentry>()
                };

                if (options.sli_quality_reportentry != null)
                {
                    var i = 1;
                    foreach (var entry in options.sli_quality_reportentry)
                    {
                        header.sli_quality_reportentry.Add(new sli_quality_reportentry
                        {
                            Id = options.Id,
                            Fobject = entry.Fobject,
                            Fseq = i,
                            Ftype = entry.Ftype,
                            Fstandard = entry.Fstandard,
                            Fmax = entry.Fmax,
                            Fmin = entry.Fmin,
                            Fqualitytools = entry.Fqualitytools,
                            Fqualitytoolsname = entry.Fqualitytoolsname,
                            Factual = entry.Factual,
                            Fdiff = entry.Fdiff,
                            Fserialnumber = entry.Fserialnumber,
                            Finspector = entry.Finspector,
                            Fdate = entry.Fdate

                            //Fstatus = entry.Fstatus
                        });
                        i++;
                    }
                }
                context.Sli_quality_report.Add(header);
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

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public IHttpActionResult GetTableHeader(int page = 1, int pageSize = 10)
        {
            var context = new YourDbContext();
            IQueryable<sli_quality_report_view> query = context.Sli_quality_report_view;
            //if (id.HasValue)
            //{
            //    query = query.Where(q => q.Id == id.Value);
            //}

            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedQuery = query.OrderByDescending(b => b.Id).Skip((page - 1) * pageSize).Take(pageSize);
            //var datas = query.ToList();
            //var datas = query.ToList();
            var result = paginatedQuery.Select(a => new
            {
                Id = a.Id,
                Fnumber = a.Fnumber,
                Fsourceid = a.Fsourceid,
                Fworkorderlistid = a.Fworkorderlistid,
                Fprocessobjectid = a.Fprocessobjectid,
                Fdescription = a.Fdescription,
                Fstatus = a.Fstatus,
                Fprintstatus = a.Fprintstatus ,
                Fprocessobjectnumber = a.Fprocessobjectnumber,
                Fprocessobjectname = a.Fprocessobjectname,
                Fproductno = a.Fproductno


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

            return Json(response);

        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public IHttpActionResult GetTableID(int? id = null)
        {
            var context = new YourDbContext();

            //var query = from p in context.Sli_plan_model
            //            join c in context.Sli_plan_modelEntry on p.Id equals c.fmodelID
            //            select new
            //            {
            //                Sli_plan_model = p,
            //                Sli_plan_modelEntry = c
            //            };
            var query = context.Sli_quality_report_view.Include(a => a.sli_quality_reportentry_view);
            if (id.HasValue)
            {
                query = query.Where(q => q.Id == id);

            }


            var result = query.Select(a => new
            {
                Id = a.Id,
                Fnumber = a.Fnumber,
                Fsourceid = a.Fsourceid,
                Fworkorderlistid = a.Fworkorderlistid,
                Fprocessobjectid = a.Fprocessobjectid,
                Fdescription = a.Fdescription,
                Fstatus = a.Fstatus,
                Fprintstatus = a.Fprintstatus,
                Fprocessobjectnumber = a.Fprocessobjectnumber,
                Fprocessobjectname = a.Fprocessobjectname,
                Fproductno = a.Fproductno,
                sli_quality_reportentry = a.sli_quality_reportentry_view.Select(b => new
                {
                    Id = b.Id,
                    Fentryid = b.Fentryid,
                    Fobject = b.Fobject,
                    Fobjectnumber = b.Fobjectnumber,
                    Fobjectname = b.Fobjectname,
                    Fseq = b.Fseq ?? 0,
                    Ftype = b.Ftype ?? string.Empty,
                    Fstandard = b.Fstandard,
                    Fmax = b.Fmax,
                    Fmin = b.Fmin,
                    Fqualitytools = b.Fqualitytools ?? 0,
                    Fqualitytoolsname = b.Fqualitytoolsname ?? string.Empty,
                    Factual = b.Factual,
                    Fdiff = b.Fdiff ?? string.Empty,
                    Fserialnumber = b.Fserialnumber ?? string.Empty,
                    Finspector = b.Finspector ?? string.Empty,
                    Fdate = b.Fdate
                })

            });
            // return Ok(result);
            var response = new
            {
                code = 200,
                msg = "ok",
                data = result,
                // count= query.Count
            };

            return Ok(response);
        }

        [System.Web.Http.HttpPost]
        public async Task<object> Update(sli_quality_report bill)
        {
            try
            {
                var context = new YourDbContext();
                var entity = await context.Sli_quality_report.FindAsync(bill.Id);
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
                    //var headerEntity = context.Sli_plan_model.Include(h => h.sli_plan_modelEntry).FirstOrDefault(h => h.Id == model.Idi);
                    //await Delete(model.Id);//

                    //var context1 = new YourDbContext();
                    //await Insert(model);


                    var Sli_quality_report = context.Sli_quality_report.FirstOrDefault(p => p.Id == bill.Id);
                    var Sli_quality_reportentry = context.Sli_quality_reportentry.Where(p => p.Id == bill.Id).ToList();


                    Sli_quality_report.Fnumber = bill.Fnumber;
                    Sli_quality_report.Fsourceid = bill.Fsourceid;
                    Sli_quality_report.Fworkorderlistid = bill.Fworkorderlistid;
                    Sli_quality_report.Fprocessobjectid = bill.Fprocessobjectid;
                    Sli_quality_report.Fdescription = bill.Fdescription ?? string.Empty;
                    Sli_quality_report.Fstatus = bill.Fstatus ?? 0;
                    Sli_quality_report.Fprintstatus = bill.Fprintstatus ?? 0;


                    var i = 1;
                    context.Sli_quality_reportentry.RemoveRange(Sli_quality_reportentry);

                    foreach (var d in bill.sli_quality_reportentry)
                    {
                        var entry = new sli_quality_reportentry
                        {
                            Id = bill.Id,
                            Fobject = d.Fobject,
                            Fseq = i,
                            Ftype = d.Ftype?? string.Empty,   //待确认是否传ID
                            Fstandard = d.Fstandard ?? string.Empty,
                            Fmax = d.Fmax ?? string.Empty,
                            Fmin = d.Fmin ?? string.Empty,
                            Fqualitytools = d.Fqualitytools ?? 0,  //数据怎么来
                            Fqualitytoolsname = d.Fqualitytoolsname ?? string.Empty,  //数据怎么来
                            Factual = d.Factual,  //前端填写
                            Fdiff = d.Fdiff ?? string.Empty,
                            Fserialnumber = d.Fserialnumber ?? string.Empty,
                            Finspector = d.Finspector ?? string.Empty,
                            Fdate = d.Fdate
                        };
                        context.Sli_quality_reportentry.Add(entry);
                        i++;
                    }

                    await context.SaveChangesAsync();

                    var datas = new
                    {
                        code = 200,
                        msg = "ok",
                        date = bill.Id + "更新成功！"
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
        public async Task<object> sli_quality_report_Delete(List<int> id)
        {
            try
            {
                var context = new YourDbContext();
                var headersToDelete = context.Sli_quality_report.Where(h => id.Contains(h.Id)).ToList();
                if (headersToDelete == null)
                {
                    var dataNull = new
                    {
                        code = 200,
                        msg = "ok",
                        orderId = id.ToString(),
                        date = id.ToString() + "不存在"
                    };
                    //string json = JsonConvert.SerializeObject(data);
                    return dataNull;
                }
                context.Sli_quality_report.RemoveRange(headersToDelete);

                foreach (var DeleteID in id)
                {
                    var Sli_quality_reportentry = context.Sli_quality_reportentry.Where(b => b.Id == DeleteID);
                    context.Sli_quality_reportentry.RemoveRange(Sli_quality_reportentry);
                }

                await context.SaveChangesAsync();
                // var data = new { Status = "Success", Message = "Data retrieved successfully", Data = new { /* actual data here */ } };
                var data = new
                {
                    code = 200,
                    msg = "Success",
                    orderId = id.ToString(),
                    date = id.ToString() + "删除成功"
                };
                return data;
            }
            catch (Exception ex)
            {
                var data = new
                {
                    code = 400,
                    msg = "失败",
                    orderId = id.ToString(),
                    date = ex.ToString()
                };
                return data;
            }


        }
    }
}