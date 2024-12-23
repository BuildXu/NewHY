using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_SY.Models;
using System.Threading.Tasks;
using WebApi_SY.Entity;
using System;
using Newtonsoft.Json;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;

namespace WebApi_SY.Controllers
{


    public class PlanController : ApiController
    {

        //private  YourDbContext _context;
        //var _context;

        // 无参数公共构造函数
        public PlanController()
        {
            //var context = new YourDbContext();
            //_context = context;
        }

        //public PlanController(YourDbContext context)
        //{
        //   // _context = context;
            
        //}
        [System.Web.Http.HttpPost]
        public async Task<object> AddOrder([Microsoft.AspNetCore.Mvc.FromBody] Sli_plan_model model)
        {
            //string json = "{\"header\":{\"name\":\"Header Name\",\"details\":[{\"detailContent\":\"Detail 1\"},{\"detailContent\":\"Detail 2\"}]}}";

            //var root = JsonConvert.DeserializeObject<RootObject>(json);
            try
            {
                var context = new YourDbContext();
                //_context = context;
                var header = new Sli_plan_model
                {
                    Fmodelnumber = model.Fmodelnumber,
                    Fmodelname = model.Fmodelname,
                    Fplanbegindate = model.Fplanbegindate,
                    Fplanenddate = model.Fplanenddate,
                    Fdays = model.Fdays,
                    Sli_plan_modelEntry = model.Sli_plan_modelEntry.Select(d => new Sli_plan_modelEntry
                    {
                        //fmodelID = model.Id,
                        Fplanoptionid = d.Fplanoptionid,
                        Fdays = d.Fdays,
                        Fdepartid = d.Fdepartid,
                        Fempid = d.Fempid

                    }).ToList()
                };



                context.Sli_plan_model.Add(header);
                await context.SaveChangesAsync();
                var dataNull = new
                {
                    msg = "Success",
                    modelid = header.Id,
                    Date = header.Id.ToString() + "保存成功"

                };
                return dataNull;
                //return Json(new { Result = "Success", orderId = order.Id }, JsonRequestBehavior.);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex.ToString() );
                //Console.WriteLine();
            }
        }


        [System.Web.Http.HttpPost]
        public async Task<object> Add_sli_workOrderList(List<sli_workOrderList> workOrderList)
        {
            try
            {
                
                var context = new YourDbContext();
                //_context = context;
                //var context1= GetTableByUsername( "test");
                //workOrderList.fproductNumber
                foreach (var WList in workOrderList)
                {
                    var insert = new sli_workOrderList
                    {
                        Fproductno = maxfproductNumber.IncrementAfterLastSpecialCharacter(WList.Fproductno),
                        Forderentryid = WList.Forderentryid,
                        Fmaterialid = WList.Fmaterialid,
                        Fworkqty = WList.Fworkqty,
                        Fworkweight = WList.Fworkweight,
                        Fnote = WList.Fnote,
                        Fworkorderliststatus = WList.Fworkorderliststatus,
                        Fsplittype = WList.Fsplittype
                    };

                    context.Sli_workOrderList.Add(insert);

                   


                    

                    var entityToUpdate = context.T_sal_orderEntry.FirstOrDefault(p => p.FENTRYID == Convert.ToInt32( WList.Forderentryid));
                    if (WList.Fsplittype != "样品")
                    {
                        if (entityToUpdate != null)
                        {
                            // 累加字段值
                            entityToUpdate.FWORKORDERLISTQTY += Convert.ToInt32(WList.Fworkqty);
                            entityToUpdate.FWORKORDERLISTREMAIN -= Convert.ToInt32(WList.Fworkqty);
                            // 保存更改
                            //_context.SaveChanges();
                        }
                    }
                    await context.SaveChangesAsync();
                }

                var dataSucc = new
                {
                    code = 200,
                    msg = "OK",
                    
                    date = " "

                };
                return dataSucc;
                //return Json(new { Result = "Success", orderId = order.Id }, JsonRequestBehavior.);
            }
            catch (Exception ex)
            {
                var dataNo = new
                {
                    code = 400,
                    msg = "失败",

                    Date = ex.ToString()

                };
                return JsonConvert.SerializeObject(dataNo);
                //Console.WriteLine();
            }


        }

        [System.Web.Http.HttpPost]
        public async Task<object> Add_sli_plan_bill([Microsoft.AspNetCore.Mvc.FromBody] sli_plan_bill model)
        {
            try
            {
                var context = new YourDbContext();
                var header = new sli_plan_bill
                {
                    Fplanlnumber = model.Fplanlnumber,
                    Fissueddate = model.Fissueddate,
                    Fplancontractentry = model.Fplancontractentry,
                    Fqty = model.Fqty,
                    Fweight = model.Fweight,
                    Fplanbegindate = model.Fplanbegindate,
                    Fplanenddate = model.Fplanenddate,
                    Factualbegindate = model.Factualbegindate,
                    Factualenddate = model.Factualenddate,
                    Fnote = model.Fnote,
                    Fdays = model.Fdays,
                    sli_plan_billlEntry = model.sli_plan_billlEntry.Select(d => new sli_plan_billlEntry
                    {
                        //fmodelID = model.Id,
                        Fplanoptionidid = d.Fplanoptionidid,
                        Fqty = d.Fqty,
                        Fweight = d.Fweight,
                        Fplanstartdate = d.Fplanstartdate,
                        Fplanenddate = d.Fplanenddate,
                        Factualstartdate = d.Factualstartdate,
                        Factualenddate = d.Factualenddate,
                        Fplandays = d.Fplandays,
                        Fcapacity = d.Fcapacity,
                        Fdepartid = d.Fdepartid,
                        Fempid = d.Fempid
                    }).ToList()
                };

                context.Sli_plan_bill.Add(header);
                await context.SaveChangesAsync();
                var dataNull = new
                {
                    code=200,
                    msg = "Success",
                    modelid = header.Id,
                    Date = header.Id.ToString() + "保存成功"

                };
                return dataNull;
            }
            catch(Exception ex)
            {
                return JsonConvert.SerializeObject(ex.ToString());
            }


        }

            public async Task<object> Delete_sli_workOrderList(int id)
        {
            try
            {
                var context = new YourDbContext();
                var entity = await context.Sli_workOrderList.FindAsync(id);
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
                
                context.Sli_workOrderList.Remove(entity);
                await context.SaveChangesAsync();

                var entityToUpdate = context.T_sal_orderEntry.FirstOrDefault(p => p.FENTRYID == Convert.ToInt32(entity.Forderentryid));
                if (entity.Fsplittype != "样品")
                {
                    if (entityToUpdate != null)
                    {
                        // 累加字段值
                        entityToUpdate.FWORKORDERLISTQTY -= Convert.ToInt32(entity.Fworkqty);
                        entityToUpdate.FWORKORDERLISTREMAIN += Convert.ToInt32(entity.Fworkqty);
                        // 保存更改
                        //_context.SaveChanges();
                    }
                }

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
        public async Task<IActionResult> UpdateOrder(Sli_plan_model model)
        {
            var context = new YourDbContext();
            if (context.Entry(model).State == Microsoft.EntityFrameworkCore.EntityState.Detached)
            {
                context.Attach(model);
            }
            context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await context.SaveChangesAsync();
             return new NoContentResult();

        }

        [System.Web.Http.HttpPost]
        public async Task<object> DeleteSli_plan_model(int id)
        {
            try
            {
                var context = new YourDbContext();
                var entity = await context.Sli_plan_model.FindAsync(id);
                if (entity == null)
                {
                    var dataNull = new
                    {
                        code=200,
                        msg = "ok",
                        orderId = id.ToString(),
                        date = id.ToString() + "不存在"
                    };
                    //string json = JsonConvert.SerializeObject(data);
                    return dataNull;
                }
                var Sli_plan_modelEntrys = context.Sli_plan_modelEntry.Where(b => b.Fmodelid == id);
                context.Sli_plan_modelEntry.RemoveRange(Sli_plan_modelEntrys);
                context.Sli_plan_model.Remove(entity);
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
        //private List<sli_user> Sli_user = new List<sli_user>();
        //[HttpGet("search/{searchTerm}")]
        [Microsoft.AspNetCore.Mvc.HttpGet]
        //[Microsoft.AspNetCore.Mvc.Route("api/user/GetTableByUsername/{username}")]
        public IHttpActionResult GetTableByUsername(string username)
        {
            var context = new YourDbContext();
            var user = context.Sli_user.FirstOrDefault(u => u.username == username);
            if (user != null)
            {
                // 这里假设你想要返回包含该用户的整个表，可以根据实际需求调整
                return Ok(user.account);
            }
            else
            {
                return NotFound();
            }
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        //定义 get 入参
        public IHttpActionResult GetTableSli_plan_model(int page = 1, int pageSize = 10, string fmodelNumber = null, string fmodelName = null, int? fdays = null)
        {
            var context = new YourDbContext();
            var query = from p in context.Sli_plan_model
                        join c in context.Sli_plan_modelEntry on p.Id equals c.Fmodelid
                        select new
                        {
                            Sli_plan_model = p,
                            Sli_plan_modelEntry = c
                        };  //  定义查询 SQL 

            if (!string.IsNullOrEmpty(fmodelNumber))
            {
                query = query.Where(q => q.Sli_plan_model.Fmodelnumber.Contains(fmodelNumber));
            }

            if (!string.IsNullOrEmpty(fmodelName))
            {
                query = query.Where(q => q.Sli_plan_model.Fmodelname.Contains(fmodelName));
            }

            if (fdays.HasValue)
            {
                query = query.Where(q => q.Sli_plan_model.Fdays == fdays.Value);
            }

            var totalCount = query.Count(); //记录数
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize); // 页数
            var paginatedQuery = query.Skip((page - 1) * pageSize).Take(pageSize); //  某页记录
            var result = paginatedQuery.Select(a => new     //  返回 查询记录   并加入去NULL值逻辑
            {
                Id = a.Sli_plan_model != null ? a.Sli_plan_model.Id : 0,
                Fmodelnumber = a.Sli_plan_model != null ? a.Sli_plan_model.Fmodelnumber : string.Empty,
                Fmodelname = a.Sli_plan_model != null ? a.Sli_plan_model.Fmodelname : string.Empty,
                Fplanbegindate = a.Sli_plan_model == null ? a.Sli_plan_model.Fplanbegindate : string.Empty,
                Fplanenddate = a.Sli_plan_model == null ? a.Sli_plan_model.Fplanenddate : string.Empty,
                Fdays = a.Sli_plan_model == null ? a.Sli_plan_model.Fdays : 0,
                FSli_plan_modelentryid = a.Sli_plan_modelEntry != null ? a.Sli_plan_modelEntry.Id : 0,
                FSli_plan_modelentryfmodelid = a.Sli_plan_modelEntry != null ? a.Sli_plan_modelEntry.Fmodelid : 0,
                FSli_plan_modelentryfplanoptionid = a.Sli_plan_modelEntry != null ? a.Sli_plan_modelEntry.Fplanoptionid : 0,
                FSli_plan_modelentryfdays = a.Sli_plan_modelEntry != null ? a.Sli_plan_modelEntry.Fdays : 0,
                FSli_plan_modelentryfdepartid = a.Sli_plan_modelEntry != null ? a.Sli_plan_modelEntry.Fdepartid : 0,
                FSli_plan_modelentryfempid = a.Sli_plan_modelEntry != null ? a.Sli_plan_modelEntry.Fempid : string.Empty
            }); 

            var response = new    // 定义 前端返回数据  总记录，总页，当前页 ，size,返回记录
            {
                totalCounts = totalCount,
                totalPagess = totalPages,
                currentPages = page,
                pageSizes = pageSize,
                data = result
            };

            return Ok(response);  // 返回查询结果--> 前端
            //}
            // else
            // {
            // return NotFound();
            // }
        }

    }
}
