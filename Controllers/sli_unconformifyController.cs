using Newtonsoft.Json;
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
    public class sli_unconformifyController : ApiController
    {
        public sli_unconformifyController()
        {
            // _context = context;
        }

        [System.Web.Http.HttpGet]
        public IHttpActionResult GetTableBysli_unconformify_view(int page = 1, int pageSize = 10)
        {
            var context = new YourDbContext();
            IQueryable<sli_unconformify_view> query = context.Sli_unconformify_view;

            
            var totalCount = query.Count(); //记录数
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize); // 页数
            var paginatedQuery = query.OrderByDescending(b => b.Id).Skip((page - 1) * pageSize).Take(pageSize); //  某页记录
            var result = paginatedQuery.Select(a => new
            {
                Id = a.Id,
                Fnumber = a.Fnumber,
                FnameSpec = a.FnameSpec,
                FmaterialNo = a.FmaterialNo,
                FMarcrialspec = a.FMarcrialspec,
                FtackingNo = a.FtackingNo,
                Ftotalnumber = a.Ftotalnumber,
                Fprocess = a.Fprocess,
                Fposition = a.Fposition,
                Funnumber = a.Funnumber,
                Fdescription = a.Fdescription,
                Fdepid = a.Fdepid,
                Fdepname = a.Fdepname,
                Fresponsibleid = a.Fresponsibleid,
                Fresponsiblename = a.Fresponsiblename,
                Finspector = a.Finspector,
                Finspectorname = a.Finspectorname,
                FInopinion = a.FInopinion,
                Fproductno = a.Fproductno ?? string.Empty,
                Fworkorderlistid = a.Fworkorderlistid

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
                    data = result
                }
            };

            return Json(response);
        }

        [System.Web.Http.HttpPost]
        public async Task<object> sli_unconformify_Insert([Microsoft.AspNetCore.Mvc.FromBody] sli_unconformify[] options)
        {
            var context = new YourDbContext();
            try
            {
                foreach (var option in options)
                {
                    var header = new sli_unconformify
                    {
                        Fnumber = option.Fnumber,
                        FnameSpec = option.FnameSpec,
                        FmaterialNo = option.FmaterialNo,
                        FMarcrialspec = option.FMarcrialspec,
                        FtackingNo = option.FtackingNo,
                        Ftotalnumber = option.Ftotalnumber,
                        Fprocess = option.Fprocess,
                        Fposition = option.Fposition,
                        Funnumber = option.Funnumber,
                        Fdescription = option.Fdescription,
                        Fdepid = option.Fdepid,
                        Fresponsibleid = option.Fresponsibleid,
                        Finspector = option.Finspector,
                        FInopinion = option.FInopinion,
                        Fworkorderlistid = option.Fworkorderlistid,

                    };
                    //i++;

                    context.Sli_unconformify.Add(header);
                }
                await context.SaveChangesAsync();
                var dataNull = new
                {
                    code = 200,
                    msg = "Success",
                    //modelid = header.Id,
                    Date = "保存成功"

                };
                return dataNull;
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex.ToString());
            }
        }

        [System.Web.Http.HttpPost]
        public async Task<object> sli_unconformify_Update(sli_unconformify bill)
        {
            try
            {
                var context = new YourDbContext();
                var entity = await context.Sli_unconformify.FindAsync(bill.Id);
                if (entity == null)
                {
                    var dataNull = new
                    {
                        code = 400,
                        msg = "ok",
                        date = "修改记录不存在"
                    };
                    return dataNull;
                }
                else
                {

                    var sli_unconformify = context.Sli_unconformify.FirstOrDefault(p => p.Id == bill.Id);

                    sli_unconformify.Fnumber = bill.Fnumber;
                    sli_unconformify.FnameSpec = bill.FnameSpec;
                    sli_unconformify.FmaterialNo = bill.FmaterialNo;
                    sli_unconformify.FMarcrialspec = bill.FMarcrialspec;
                    sli_unconformify.FtackingNo = bill.FtackingNo;
                    sli_unconformify.Ftotalnumber = bill.Ftotalnumber;
                    sli_unconformify.Fprocess = bill.Fprocess;
                    sli_unconformify.Fposition = bill.Fposition;
                    sli_unconformify.Funnumber = bill.Funnumber;
                    sli_unconformify.Fdescription = bill.Fdescription;
                    sli_unconformify.Fdepid = bill.Fdepid;
                    sli_unconformify.Fresponsibleid = bill.Fresponsibleid;
                    sli_unconformify.Finspector = bill.Finspector;
                    sli_unconformify.FInopinion = bill.FInopinion;
                    sli_unconformify.Fworkorderlistid = bill.Fworkorderlistid;
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
                return Ok(datas);
            }


        }

        [System.Web.Http.HttpPost]
        public async Task<object> sli_unconformify_Delete(List<int> id)
        {
            try
            {
                var context = new YourDbContext();
                var headersToDelete = context.Sli_unconformify.Where(h => id.Contains(h.Id)).ToList();
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
                context.Sli_unconformify.RemoveRange(headersToDelete);

                

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