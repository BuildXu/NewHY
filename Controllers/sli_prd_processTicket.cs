using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi_SY.Entity;
using WebApi_SY.Models;

namespace WebApi.Controllers
{
    public class sli_prd_processTicketController : ApiController
    {
        public sli_prd_processTicketController()
        {
            // _context = context;

        }
        [System.Web.Http.HttpPost]
        public async Task<object> Insert([Microsoft.AspNetCore.Mvc.FromBody] sli_plan_bill bill)
        {
            try
            {
                var context = new YourDbContext();
                var header = new sli_plan_bill
                {
                    fplanlNumber = bill.fplanlNumber,
                    fissuedDate = bill.fissuedDate,
                    fplanContractEntry = bill.fplanContractEntry,
                    fqty = bill.fqty,
                    fweight = bill.fweight,
                    fplanBeginDate = bill.fplanBeginDate,
                    fplanEndDate = bill.fplanEndDate,
                    factualBeginDate = bill.factualBeginDate,
                    factualEndDate = bill.factualEndDate,
                    fnote = bill.fnote,
                    fdays = bill.fdays,
                    sli_plan_billlEntry = bill.sli_plan_billlEntry.Select(d => new sli_plan_billlEntry
                    {
                        //fmodelID = bill.Id,
                        fplanOptionIdId = d.fplanOptionIdId,
                        fqty = d.fqty,
                        fweight = d.fweight,
                        fplanStartDate = d.fplanStartDate,
                        fplanEndDate = d.fplanEndDate,
                        factualStartDate = d.factualStartDate,
                        factualEndDate = d.factualEndDate,
                        fPlanDays = d.fPlanDays,
                        fcapacity = d.fcapacity,
                        fdepartID = d.fdepartID,
                        fempId = d.fempId
                    }).ToList()
                };



                context.Sli_plan_bill.Add(header);
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
                var entity = await context.Sli_plan_bill.FindAsync(id);
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
                var Sli_plan_billEntrys = context.Sli_plan_billlEntry.Where(b => b.fplanBillId == id);
                context.Sli_plan_billlEntry.RemoveRange(Sli_plan_billEntrys);
                context.Sli_plan_bill.Remove(entity);
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
        public async Task<IActionResult> Update(sli_plan_bill bill)
        {
            var context = new YourDbContext();
            if (context.Entry(bill).State == Microsoft.EntityFrameworkCore.EntityState.Detached)
            {
                context.Attach(bill);
            }
            context.Entry(bill).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
            return new NoContentResult();

        }


        [Microsoft.AspNetCore.Mvc.HttpGet]
        //[Microsoft.AspNetCore.Mvc.Route("api/user/GetTableByUsername/{username}")]
        public IHttpActionResult GetTable(int page = 1, int pageSize = 10, string fplanNumber = null)
        {
            var context = new YourDbContext();
            var query = from p in context.Sli_plan_bill
                        join c in context.Sli_plan_billlEntry on p.id equals c.fplanBillId
                        select new
                        {
                            Sli_plan_bill = p,
                            Sli_plan_billEntry = c
                        };
            if (!string.IsNullOrEmpty(fplanNumber))
            {
                query = query.Where(q => q.Sli_plan_bill.fplanlNumber.Contains(fplanNumber));
            }

            //if (!string.IsNullOrEmpty(fmodelName))
            //{
            //    query = query.Where(q => q.Sli_plan_bill.fplanName.Contains(fmodelName));
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
                id = a.Sli_plan_bill != null ? a.Sli_plan_bill.id : 0
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