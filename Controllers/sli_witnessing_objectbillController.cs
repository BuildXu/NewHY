using Microsoft.IdentityModel.Tokens;
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
    public class sli_witnessing_objectbillController : ApiController
    {
        public sli_witnessing_objectbillController()
        {
            // _context = context;
        }

        [System.Web.Http.HttpPost]
        public async Task<object> sli_mes_lauch_Insert([Microsoft.AspNetCore.Mvc.FromBody] sli_witnessing_objectbill[] options)
        {
            var context = new YourDbContext();
            try
            {
                foreach (var option in options)
                {
                    var header = new sli_witnessing_objectbill
                    {
                        Fentryid = option.Fentryid,
                        //Id = options.Id,
                        Fseq = option.Fseq,
                        Fobject = option.Fobject,
                        Fnote = option.Fnote,
                        Fstatus = option.Fstatus
                    };
                    context.sli_witnessing_objectbill.Add(header);

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
        public async Task<object> sli_witnessing_objectbill_Update([Microsoft.AspNetCore.Mvc.FromBody] sli_witnessing_objectbill option)
        {
            try
            {
                var context = new YourDbContext();
                var entity = await context.sli_witnessing_objectbill.FindAsync(option.Id);
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
                    var sli_witnessing_objectbill = context.sli_witnessing_objectbill.FirstOrDefault(p => p.Id == option.Id);
                    //var Sli_plan_modelEntrys = _context.Sli_plan_modelEntry.Where(p => p.fmodelID == model.Id).ToList();


                    sli_witnessing_objectbill.Fentryid = option.Fentryid;
                    sli_witnessing_objectbill.Id = option.Id;
                    sli_witnessing_objectbill.Fseq = option.Fseq;
                    sli_witnessing_objectbill.Fobject = option.Fobject;
                    sli_witnessing_objectbill.Fstatus = option.Fstatus;



                    await context.SaveChangesAsync();

                    var datas = new
                    {
                        code = 200,
                        msg = "ok",
                        date = sli_witnessing_objectbill.Id + "修改成功！"
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
        public async Task<object> sli_witnessing_objectbill_Delete(List<int> id)
        {
            try
            {
                var context = new YourDbContext();
                foreach (var deleteid in id)
                {
                    var entity = await context.sli_witnessing_objectbill.FindAsync(deleteid);
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
                    context.sli_witnessing_objectbill.RemoveRange(entity);
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
        public IHttpActionResult GetTableBysli_witnessing_objectbill_view(int page = 1, int pageSize = 10)
        {

      //      int? Id = null,
      //int? Fsourceid = null,
      //int? Fseq = null,
      //int? Fobject = null,
      //string Fnote = null,
      //int? Fstatus = null
            var context = new YourDbContext();
            IQueryable<sli_witnessing_objectbill_view> query = context.sli_witnessing_objectbill_view;

            //if (Id.HasValue)
            //{
            //    query = query.Where(q => q.Id == Id);
            //}
            //if (Fsourceid.HasValue)
            //{
            //    query = query.Where(q => q.Fsourceid == Fsourceid);
            //}
            //if (Fseq.HasValue)
            //{
            //    query = query.Where(q => q.Fseq == Fseq);
            //}
            //if (Fobject.HasValue)
            //{
            //    query = query.Where(q => q.Fobject == Fobject);
            //}
            //if (!string.IsNullOrEmpty(Fnote))
            //{
            //    query = query.Where(q => q.Fnote == Fnote);
            //}
            //if (Fstatus.HasValue)
            //{
            //    query = query.Where(q => q.Fstatus == Fstatus);
            //}
            var totalCount = query.Count(); //记录数
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize); // 页数
            var paginatedQuery = query.OrderByDescending(b => b.Id).Skip((page - 1) * pageSize).Take(pageSize); //  某页记录
            var result = paginatedQuery.Select(a => new
            {
                Fsourceid = a.Fsourceid,
                Forderno = a.Forderno,
                Fcustomer = a.Fcustomer,
                Fmaterialname = a.Fmaterialname,
                Fdescription = a.Fdescription,
                Id = a.Id,
                Fentryid = a.Fentryid,
                Fseq = a.Fseq,
                Fobject = a.Fobject,
                Fobjectno = a.Fobjectno,
                Fobjectname = a.Fobjectname,
                Fnote = a.Fnote ?? string.Empty,
                Fstatus = a.Fstatus,
                


            });

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
                    data = query.ToList()
                }
            };

            return Json(response);
        }
    }
}