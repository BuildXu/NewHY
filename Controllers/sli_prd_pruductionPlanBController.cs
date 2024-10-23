using Microsoft.AspNetCore.Mvc;
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
    public class sli_prd_pruductionPlanBController : ApiController
    {
        public sli_prd_pruductionPlanBController()
        {
            // _context = context;

        }
        [System.Web.Http.HttpPost]
        public async Task<object> Insert([Microsoft.AspNetCore.Mvc.FromBody] sli_prd_prudcutionPlanB plan)
        {
            try
            {
                var context = new YourDbContext();
                var header = new sli_prd_prudcutionPlanB
                {
                    fplanNumber = plan.fplanNumber,
                    fdate = plan.fdate,
                    fstartDate = plan.fstartDate,
                    fendDate = plan.fendDate,
                    fbillerId = plan.fbillerId,
                    fdeptId = plan.fdeptId,
                    fprocessId = plan.fprocessId,
                    fmechineId = plan.fmechineId,
                    sli_prd_pruductionPlanEntryB = plan.sli_prd_pruductionPlanEntryB.Select(d => new sli_prd_pruductionPlanEntryB
                    {
                        //fmodelID = plan.Id,
                        fpruductionPlanId = d.fpruductionPlanId,
                        fworkOrderId=d.fworkOrderId,
                        frouteingCardId=d.frouteingCardId,
                        frouteingCardEntryId=d.frouteingCardEntryId,
                        fqty = d.fqty,
                        fqtyTicket = d.fqtyTicket,
                        fStatus = d.fStatus,
                        fprocessId = d.fprocessId,
                        fplanNumber = d.fplanNumber,
                    }).ToList()
                };



                context.Sli_prd_prudcutionPlanB.Add(header);
                await context.SaveChangesAsync();
                var dataNull = new
                {
                    msg = "Success",
                    planid = header.id,
                    Date = header.id.ToString() + "保存成功"

                };
                return dataNull;
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex.ToString());
            }


        }


        [System.Web.Http.HttpPost]
        public async Task<object> Delete(int id)
        {
            try
            {
                var context = new YourDbContext();
                var entity = await context.Sli_prd_prudcutionPlanB.FindAsync(id);
                if (entity == null)
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
                var Sli_prd_pruductionPlanEntryBs = context.Sli_prd_pruductionPlanEntryB.Where(b => b.fpruductionPlanId == id);
                context.Sli_prd_pruductionPlanEntryB.RemoveRange(Sli_prd_pruductionPlanEntryBs);
                context.Sli_prd_prudcutionPlanB.Remove(entity);
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

        [System.Web.Http.HttpPost]
        public async Task<IActionResult> Update(sli_prd_prudcutionPlanB plan)
        {
            var context = new YourDbContext();
            if (context.Entry(plan).State == Microsoft.EntityFrameworkCore.EntityState.Detached)
            {
                context.Attach(plan);
            }
            context.Entry(plan).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
            return new NoContentResult();

        }


        [Microsoft.AspNetCore.Mvc.HttpGet]
        //[Microsoft.AspNetCore.Mvc.Route("api/user/GetTableByUsername/{username}")]
        public IHttpActionResult GetTable(int page = 1, int pageSize = 10, string fplanNumber = null)
        {
            var context = new YourDbContext();
            var query = from p in context.Sli_prd_prudcutionPlanB
                        join c in context.Sli_prd_pruductionPlanEntryB on p.id equals c.fprocessId
                        select new
                        {
                            sli_prd_pruductionPlanB = p,
                            sli_prd_pruductionPlanEntryB = c
                        };
            if (!string.IsNullOrEmpty(fplanNumber))
            {
                query = query.Where(q => q.sli_prd_pruductionPlanB.fplanNumber.Contains(fplanNumber));
            }

            //if (!string.IsNullOrEmpty(fmodelName))
            //{
            //    query = query.Where(q => q.Sli_plan_plan.fplanName.Contains(fmodelName));
            //}

            //if (fdays.HasValue)
            //{
            //    query = query.Where(q => q.Sli_plan_model.fdays == fdays.Value);
            //}

            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedQuery = query.Skip((page - 1) * pageSize).Take(pageSize);
            var result = paginatedQuery.Select(a => new
            {
                id = a.sli_prd_pruductionPlanEntryB != null ? a.sli_prd_pruductionPlanB.id : 0
                //,
                //FmodelNumber = a.Sli_plan_model != null ? a.Sli_plan_model.fmodelNumber : string.Empty,
                //FmodelName = a.Sli_plan_model != null ? a.Sli_plan_model.fmodelName : string.Empty,
                //FplanBeginDate = a.Sli_plan_model == null ? a.Sli_plan_model.fplanBeginDate : string.Empty,
                //FplanEndDate = a.Sli_plan_model == null ? a.Sli_plan_model.fplanEndDate : string.Empty,
                //Fdays = a.Sli_plan_model == null ? a.Sli_plan_model.fdays : 0,
                //fSli_plan_modelentryid = a.Sli_plan_modelEntry != null ? a.Sli_plan_modelEntry.Id : 0,
                //fSli_plan_modelentryfmodelID = a.Sli_plan_modelEntry != null ? a.Sli_plan_modelEntry.fmodelID : 0,
                //fSli_plan_modelentryfplanOptionId = a.Sli_plan_modelEntry != null ? a.Sli_plan_modelEntry.fplanOptionId : string.Empty,
                //fSli_plan_modelentryfdays = a.Sli_plan_modelEntry != null ? a.Sli_plan_modelEntry.fdays : 0,
                //fSli_plan_modelentryfdepartID = a.Sli_plan_modelEntry != null ? a.Sli_plan_modelEntry.fdepartID : 0,
                //fSli_plan_modelentryfempId = a.Sli_plan_modelEntry != null ? a.Sli_plan_modelEntry.fempId : string.Empty
            });

            var response = new
            {
                totalCounts = totalCount,
                totalPagess = totalPages,
                currentPages = page,
                pageSizes = pageSize,
                data = result
            };

            return Ok(response);
            //}
            // else
            // {
            // return NotFound();
            // }
        }
    }


}