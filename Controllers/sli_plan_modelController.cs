using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi_SY.Entity;
using WebApi_SY.Models;

namespace WebApi_SY.Controllers
{
    public class sli_plan_modelController : ApiController
    {
        public sli_plan_modelController()
        {
            // _context = context;
           

        }
        [System.Web.Http.HttpPost]
        public async Task<object> Insert([Microsoft.AspNetCore.Mvc.FromBody] sli_plan_model model)
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
                    fnote=model.fnote,
                    sli_plan_modelEntry = model.sli_plan_modelEntry.Select(d => new sli_plan_modelEntry
                    {
                        //fmodelID = model.Id,
                        fplanOptionId = d.fplanOptionId,
                        fdays = d.fdays,
                        fdepartID = d.fdepartID,
                        fempId = d.fempId

                    }).ToList()
                };



                context.Sli_plan_model.Add(header);
                await context.SaveChangesAsync();
                var dataNull = new
                {
                    code=200,
                    msg = "Success",
                    modelid = header.Id,
                    Date = header.Id.ToString() + "保存成功"

                };
                return dataNull;
                //return Json(new { Result = "Success", orderId = order.Id }, JsonRequestBehavior.);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex.ToString());
                //Console.WriteLine();
            }
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        //[Microsoft.AspNetCore.Mvc.Route("api/user/GetTableByUsername/{username}")]
        public IHttpActionResult GetTable(int page = 1, int pageSize = 10, string fmodelNumber = null, string fmodelName = null, int? fdays = null)
        {
            var context = new YourDbContext();
            //var query = from p in context.Sli_plan_model
            //            join c in context.Sli_plan_modelEntry on p.Id equals c.fmodelID
            //            select new
            //            {
            //                Sli_plan_model = p,
            //                Sli_plan_modelEntry = c
            //            };
            var query = context.Sli_plan_model.Include(a => a.sli_plan_modelEntry);
            if (!string.IsNullOrEmpty(fmodelNumber))
            {
                query = query.Where(q => q.fmodelNumber.Contains(fmodelNumber));
                
            }

            if (!string.IsNullOrEmpty(fmodelName))
            {
                query = query.Where(q => q.fmodelName.Contains(fmodelName));
            }

            if (fdays.HasValue)
            {
                query = query.Where(q => q.fdays == fdays.Value);
            }

            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedQuery = query.OrderByDescending(b => b.Id).Skip((page - 1) * pageSize).Take(pageSize);
            
            var result = paginatedQuery.Select(a => new
            {
                id = a.Id,
                fmodelNumber = a.fmodelNumber,
                fmodelName = a.fmodelName ,
                fplanBeginDate = a.fplanBeginDate,
                fplanEndDate = a.fplanEndDate,
                fdays = a.fdays ,
                fnote=a.fnote,
                Sli_plan_modelEntry = a.sli_plan_modelEntry.Select(b => new
                {
                    id = b.Id,
                    fmodelID=b.fmodelID,
                    fplanOptionId=b.fplanOptionId,
                    fdays=b.fdays ,
                    fdepartID=b.fdepartID,
                    fempId = b.fdepartID
                    
                })

            });

            var response = new
            {
                code = 200,
                msg = "ok",
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
            var query = context.Sli_plan_model.Include(a => a.sli_plan_modelEntry);
            if (id.HasValue)
            {
                query = query.Where(q => q.Id==id);

            }
            
            
            var result = query.Select(a => new
            {
                id = a.Id,
                fmodelNumber = a.fmodelNumber,
                fmodelName = a.fmodelName,
                fplanBeginDate = a.fplanBeginDate,
                fplanEndDate = a.fplanEndDate,
                fdays = a.fdays,
                fnote=  a.fnote,
                Sli_plan_modelEntry = a.sli_plan_modelEntry.Select(b => new
                {
                    id = b.Id,
                    fmodelID = b.fmodelID,
                    fplanOptionId = b.fplanOptionId,
                    fdays = b.fdays,
                    fdepartID = b.fdepartID,
                    fempId = b.fdepartID

                })

            });
           // return Ok(result);
            var response = new
            {
                code=200,
                msg = "ok",
                data = result,
               // count= query.Count
            };

            return Ok(response);
        }

        

        [System.Web.Http.HttpPost]
        public async Task<object> Delete(List<int> id)
        {
            try
            {
                var context = new YourDbContext();
                //var entity = await context.Sli_plan_model.FindAsync(id);
                //var headersToDelete = context.Sli_plan_model.Where(h => id.Contains(h.Id)).ToList();
                var headersToDelete = context.Sli_plan_model.Include(h => h.sli_plan_modelEntry).Where(h => id.Contains(h.Id)).ToList();
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
                context.Sli_plan_model.RemoveRange(headersToDelete);
                foreach (var DeleteID in id)
                {
                    var Sli_plan_modelEntrys = context.Sli_plan_modelEntry.Where(b => b.fmodelID == DeleteID);
                    context.Sli_plan_modelEntry.RemoveRange(Sli_plan_modelEntrys);
                }

                    //var Sli_plan_modelEntrys = context.Sli_plan_modelEntry.Where(b => b.fmodelID == id);
                
                //context.Sli_plan_modelEntry.RemoveRange(Sli_plan_modelEntrys);
                //context.Sli_plan_model.Remove(entity);
                await context.SaveChangesAsync();
                // var data = new { Status = "Success", Message = "Data retrieved successfully", Data = new { /* actual data here */ } };
                var data = new
                {
                    code = 200,
                    msg = "Success",
                    orderId = id,
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


        //public async Task<object> Update(sli_plan_model model)
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<object> Update([Microsoft.AspNetCore.Mvc.FromBody] sli_plan_model model)
        {
            try
            {
                var context = new YourDbContext();
                var entity = await context.Sli_plan_model.FindAsync(model.Id);
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


                    var Sli_plan_models = context.Sli_plan_model.FirstOrDefault(p => p.Id == model.Id);
                    var Sli_plan_modelEntrys = context.Sli_plan_modelEntry.Where(p => p.fmodelID == model.Id).ToList();


                    Sli_plan_models.fmodelName = model.fmodelName;
                    Sli_plan_models.fmodelNumber= model.fmodelNumber;
                    Sli_plan_models.fplanBeginDate = model.fplanBeginDate;
                    Sli_plan_models.fplanEndDate = model.fplanEndDate;
                    Sli_plan_models.fdays = model.fdays;
                    Sli_plan_models.fnote = model.fnote;
                    context.Sli_plan_modelEntry.RemoveRange(Sli_plan_modelEntrys);

                    foreach (var childTableData in model.sli_plan_modelEntry)
                    {
                        var entry = new sli_plan_modelEntry
                        {
                            fmodelID = model.Id,
                            fplanOptionId = childTableData.fplanOptionId,
                            fdays = childTableData.fdays,
                            fdepartID = childTableData.fdepartID,
                            fempId = childTableData.fempId

                        };
                        context.Sli_plan_modelEntry.Add(entry);
                    }
                    await context.SaveChangesAsync();

                    var datas = new
                    {
                        code = 200,
                        msg = "ok",
                        date = model
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
    }
}