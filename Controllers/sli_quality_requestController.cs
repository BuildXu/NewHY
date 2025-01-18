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
    public class sli_quality_requestController : ApiController
    {
        // GET api/<controller>
        public sli_quality_requestController()
        {
            // _context = context;
        }
        [System.Web.Http.HttpPost]
        public async Task<object> sli_quality_request_Insert([Microsoft.AspNetCore.Mvc.FromBody] sli_quality_request options)
        {
            var context = new YourDbContext();
            try
            {
                var header = new sli_quality_request
                {
                    Fnumber = options.Fnumber,
                    Fdate = options.Fdate,
                    Fendate = options.Fendate,
                    Fdeptid = options.Fdeptid,
                    Fempid = options.Fempid,
                    Fstatus = options.Fstatus,
                    sli_quality_requestEntry = new List<sli_quality_requestEntry>()
                };

                if (options.sli_quality_requestEntry != null)
                {
                    //var i = 1;
                    foreach (var entry in options.sli_quality_requestEntry)
                    {
                        header.sli_quality_requestEntry.Add(new sli_quality_requestEntry
                        {
                            //Fentryid = i,
                            Fsourceid = entry.Fsourceid,
                            Fworkorderlistid = entry.Fworkorderlistid,
                            Fqty = entry.Fqty,
                            Fobjectid = entry.Fobjectid,
                            Fstatus = entry.Fstatus
                        });
                        //i++;
                    }
                }
                context.Sli_quality_request.Add(header);
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

        [System.Web.Http.HttpPost]
        public async Task<object> sli_quality_request_Delete(List<int> id)
        {
            try
            {
                var context = new YourDbContext();
                var headersToDelete = context.Sli_quality_request.Where(h => id.Contains(h.Id)).ToList();
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
                context.Sli_quality_request.RemoveRange(headersToDelete);

                foreach (var DeleteID in id)
                {
                    var sli_quality_requestEntry = context.Sli_quality_requestEntry.Where(b => b.Id == DeleteID);
                    context.Sli_quality_requestEntry.RemoveRange(sli_quality_requestEntry);
                }

                await context.SaveChangesAsync();
                // var data = new { Status = "Success", Message = "Data retrieved successfully", Data = new { /* actual data here */ } };
                var data = new
                {
                    code = 200,
                    msg = "Success",
                    //orderId = id.ToString(),
                    date =  "删除成功"
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

        [System.Web.Http.HttpPost]
        public async Task<object> Update(sli_quality_request bill)
        {
            try
            {
                var context = new YourDbContext();
                var entity = await context.Sli_quality_request.FindAsync(bill.Id);
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


                    var Sli_quality_request = context.Sli_quality_request.FirstOrDefault(p => p.Id == bill.Id);
                    var Sli_quality_requestEntry = context.Sli_quality_requestEntry.Where(p => p.Id == bill.Id).ToList();


                    Sli_quality_request.Fnumber = bill.Fnumber;
                    Sli_quality_request.Fdate = bill.Fdate;
                    Sli_quality_request.Fendate = bill.Fendate;
                    Sli_quality_request.Fdeptid = bill.Fdeptid;
                    Sli_quality_request.Fempid = bill.Fempid;
                    Sli_quality_request.Fstatus = bill.Fstatus;
                    


                    context.Sli_quality_requestEntry.RemoveRange(Sli_quality_requestEntry);

                    foreach (var d in bill.sli_quality_requestEntry)
                    {
                        var entry = new sli_quality_requestEntry
                        {
                            Id = bill.Id,
                            Fsourceid = d.Fsourceid,
                            Fworkorderlistid = d.Fworkorderlistid,
                            Fqty = d.Fqty,
                            Fstatus = d.Fstatus,
                            Fobjectid = d.Fobjectid
                        };
                        context.Sli_quality_requestEntry.Add(entry);
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

        [Microsoft.AspNetCore.Mvc.HttpGet]
        //[Microsoft.AspNetCore.Mvc.Route("api/user/GetTableByUsername/{username}")]
        public IHttpActionResult GetTableHeader(int page = 1, int pageSize = 10)
        {
            var context = new YourDbContext();
            IQueryable<sli_quality_request_view> query = context.Sli_quality_request_view;
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
                fnumber = a.Fnumber,
                Fdeptid = a.Fdeptid,
                Fempid = a.Fempid,
                Fdate = a.Fdate,
                Fendate = a.Fendate,
                Fstatus = a.Fstatus,
                Fbillerid = a.Fbillerid ?? 0,
                Fdept_name = a.Fdept_name,
                FempName = a.FempName,


            });
            var response = new    // 定义 前端返回数据  总记录，总页，当前页 ，size,返回记录
            {
                code = 200,
                msg = "OK",
                totalCounts = totalCount,
                totalPagess = totalPages,
                currentPages = page,
                pageSizes = pageSize,
                data = result
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
            var query = context.Sli_quality_request_view.Include(a => a.sli_quality_requestentry_view);
            if (id.HasValue)
            {
                query = query.Where(q => q.Id == id);

            }


            var result = query.Select(a => new
            {
                Id = a.Id,
                Fnumber = a.Fnumber,
                Fdate = a.Fdate,
                Fendate = a.Fendate,
                Fdeptid = a.Fdeptid,
                Fempid = a.Fempid,
                Fstatus = a.Fstatus,
                Fdept_name=a.Fdept_name,
                FempName=a.FempName,
                Sli_quality_requestEntry = a.sli_quality_requestentry_view.Select(b => new
                {
                    id = b.Id,
                    Fentryid = b.Fentryid,
                    Fsourceid = b.Fsourceid,
                    Fworkorderlistid = b.Fworkorderlistid,
                    Fqty = b.Fqty,
                    Fstatus = b.Fstatus,
                    Fobjectid=b.Fobjectid,
                    Fobjectnumber=b.Fobjectnumber,
                    Fobjectname=b.Fobjectname,
                    Fmaterialnumber = b.Fmaterialnumber,
                    Fmaterialname = b.Fmaterialname,
                    Fdescription = b.Fdescription,
                    Fproductno = b.Fproductno

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

    }
}