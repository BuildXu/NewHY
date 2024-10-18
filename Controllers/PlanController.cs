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

        private readonly YourDbContext _context;
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
        public async Task<object> AddOrder([Microsoft.AspNetCore.Mvc.FromBody] sli_plan_model model)
        {
            //string json = "{\"header\":{\"name\":\"Header Name\",\"details\":[{\"detailContent\":\"Detail 1\"},{\"detailContent\":\"Detail 2\"}]}}";

            //var root = JsonConvert.DeserializeObject<RootObject>(json);
            try
            {
                var context = new YourDbContext();
                //_context = context;
                var header = new sli_plan_model
                {
                    fmodelNumber = model.fmodelNumber,
                    fmodelName = model.fmodelName,
                    fplanBeginDate = model.fplanBeginDate,
                    fplanEndDate = model.fplanEndDate,
                    fdays = model.fdays,
                    sli_plan_modelEntry = model.sli_plan_modelEntry.Select(d => new sli_plan_modelEntry
                    {
                        //fmodelID = model.Id,
                        fplanOptionId = d.fplanOptionId,
                        fdays = d.fdays,
                        fdepartID = d.fdepartID,
                        fempId = d.fempId

                    }).ToList()
                };



                _context.Sli_plan_model.Add(header);
                await _context.SaveChangesAsync();
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
                        fproductNumber = maxfproductNumber.IncrementAfterLastSpecialCharacter(WList.fproductNumber),
                        forderEntryid = WList.forderEntryid,
                        fmaterialid = WList.fmaterialid,
                        fworkQty = WList.fworkQty,
                        fworkWeight = WList.fworkWeight,
                        fnote = WList.fnote,
                        fworkOrderListStatus = WList.fworkOrderListStatus,
                        splittype = WList.splittype
                    };

                    _context.Sli_workOrderList.Add(insert);

                   


                    

                    var entityToUpdate = _context.T_sal_orderEntry.FirstOrDefault(p => p.FENTRYID == Convert.ToInt32( WList.forderEntryid));
                    if (WList.splittype != "样品")
                    {
                        if (entityToUpdate != null)
                        {
                            // 累加字段值
                            entityToUpdate.FWORKORDERLISTQTY += Convert.ToInt32(WList.fworkQty);
                            entityToUpdate.FWORKORDERLISTREMAIN -= Convert.ToInt32(WList.fworkQty);
                            // 保存更改
                            //_context.SaveChanges();
                        }
                    }
                    await _context.SaveChangesAsync();
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
                    fplanlNumber = model.fplanlNumber,
                    fissuedDate = model.fissuedDate,
                    fplanContractEntry=model.fplanContractEntry,
                    fqty = model.fqty,
                    fweight = model.fweight,
                    fplanBeginDate = model.fplanBeginDate,
                    fplanEndDate = model.fplanEndDate,
                    factualBeginDate = model.factualBeginDate,
                    factualEndDate = model.factualEndDate,
                    fnote=model.fnote,
                    fdays = model.fdays,
                    sli_plan_billlEntry = model.sli_plan_billlEntry.Select(d => new sli_plan_billlEntry
                    {
                        //fmodelID = model.Id,
                        fplanOptionIdId = d.fplanOptionIdId,
                        fqty = d.fqty,
                        fweight = d.fweight,
                        fplanStartDate = d.fplanStartDate,
                        fplanEndDate = d.fplanEndDate,
                        factualStartDate= d.factualStartDate,
                        factualEndDate= d.factualEndDate,
                        fPlanDays = d.fPlanDays,
                        fcapacity = d.fcapacity,
                        fdepartID = d.fdepartID,
                        fempId = d.fempId
                    }).ToList()
                };

                _context.Sli_plan_bill.Add(header);
                await _context.SaveChangesAsync();
                var dataNull = new
                {
                    msg = "Success",
                    modelid = header.id,
                    Date = header.id.ToString() + "保存成功"

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

                var entity = await _context.Sli_workOrderList.FindAsync(id);
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
                
                _context.Sli_workOrderList.Remove(entity);
                await _context.SaveChangesAsync();

                var entityToUpdate = _context.T_sal_orderEntry.FirstOrDefault(p => p.FENTRYID == Convert.ToInt32(entity.forderEntryid));
                if (entity.splittype != "样品")
                {
                    if (entityToUpdate != null)
                    {
                        // 累加字段值
                        entityToUpdate.FWORKORDERLISTQTY -= Convert.ToInt32(entity.fworkQty);
                        entityToUpdate.FWORKORDERLISTREMAIN += Convert.ToInt32(entity.fworkQty);
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
        public async Task<IActionResult> UpdateOrder(sli_plan_model model)
        {

                if (_context.Entry(model).State == Microsoft.EntityFrameworkCore.EntityState.Detached)
                {
                    _context.Attach(model);
                }
                _context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _context.SaveChangesAsync();
             return new NoContentResult();

        }

        [System.Web.Http.HttpPost]
        public async Task<object> DeleteSli_plan_model(int id)
        {
            try
            {

                var entity = await _context.Sli_plan_model.FindAsync(id);
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
                var Sli_plan_modelEntrys = _context.Sli_plan_modelEntry.Where(b => b.fmodelID == id);
                _context.Sli_plan_modelEntry.RemoveRange(Sli_plan_modelEntrys);
                _context.Sli_plan_model.Remove(entity);
                await _context.SaveChangesAsync();
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
            var user = _context.Sli_user.FirstOrDefault(u => u.username == username);
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
            var query = from p in _context.Sli_plan_model
                        join c in _context.Sli_plan_modelEntry on p.Id equals c.fmodelID
                        select new
                        {
                            Sli_plan_model = p,
                            Sli_plan_modelEntry = c
                        };  //  定义查询 SQL 

            if (!string.IsNullOrEmpty(fmodelNumber))
            {
                query = query.Where(q => q.Sli_plan_model.fmodelNumber.Contains(fmodelNumber));
            }

            if (!string.IsNullOrEmpty(fmodelName))
            {
                query = query.Where(q => q.Sli_plan_model.fmodelName.Contains(fmodelName));
            }

            if (fdays.HasValue)
            {
                query = query.Where(q => q.Sli_plan_model.fdays == fdays.Value);
            }

            var totalCount = query.Count(); //记录数
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize); // 页数
            var paginatedQuery = query.Skip((page - 1) * pageSize).Take(pageSize); //  某页记录
            var result = paginatedQuery.Select(a => new     //  返回 查询记录   并加入去NULL值逻辑
            {
                id = a.Sli_plan_model != null ? a.Sli_plan_model.Id : 0,
                FmodelNumber = a.Sli_plan_model != null ? a.Sli_plan_model.fmodelNumber : string.Empty,
                FmodelName = a.Sli_plan_model != null ? a.Sli_plan_model.fmodelName : string.Empty,
                FplanBeginDate = a.Sli_plan_model == null ? a.Sli_plan_model.fplanBeginDate: string.Empty ,
                FplanEndDate = a.Sli_plan_model == null ? a.Sli_plan_model.fplanEndDate : string.Empty,
                Fdays = a.Sli_plan_model == null ? a.Sli_plan_model.fdays : 0,
                fSli_plan_modelentryid = a.Sli_plan_modelEntry != null ? a.Sli_plan_modelEntry.Id : 0,
                fSli_plan_modelentryfmodelID = a.Sli_plan_modelEntry != null ? a.Sli_plan_modelEntry.fmodelID : 0,
                fSli_plan_modelentryfplanOptionId = a.Sli_plan_modelEntry != null ? a.Sli_plan_modelEntry.fplanOptionId : 0,
                fSli_plan_modelentryfdays = a.Sli_plan_modelEntry != null ? a.Sli_plan_modelEntry.fdays : 0,
                fSli_plan_modelentryfdepartID = a.Sli_plan_modelEntry != null ? a.Sli_plan_modelEntry.fdepartID : 0,
                fSli_plan_modelentryfempId = a.Sli_plan_modelEntry != null ? a.Sli_plan_modelEntry.fempId  : string.Empty
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
