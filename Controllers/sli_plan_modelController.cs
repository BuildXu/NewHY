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
        public async Task<object> Insert([Microsoft.AspNetCore.Mvc.FromBody] Sli_plan_model model)
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
                    Fnote = model.Fnote,
                    Sli_plan_modelEntry = model.Sli_plan_modelEntry.Select(d => new Sli_plan_modelEntry
                    {
                        //Fmodelid = model.Id,
                        Fplanoptionid = d.Fplanoptionid,
                        Fdays = d.Fdays,
                        Fdepartid = d.Fdepartid,
                        Fempid = d.Fempid,
                        Fseq=d.Fseq

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
            var query = context.Sli_plan_model.Include(a => a.Sli_plan_modelEntry);
            if (!string.IsNullOrEmpty(fmodelNumber))
            {
                query = query.Where(q => q.Fmodelnumber.Contains(fmodelNumber));
                
            }

            if (!string.IsNullOrEmpty(fmodelName))
            {
                query = query.Where(q => q.Fmodelnumber.Contains(fmodelName));
            }

            if (fdays.HasValue)
            {
                query = query.Where(q => q.Fdays == fdays.Value);
            }

            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedQuery = query.OrderByDescending(b => b.Id).Skip((page - 1) * pageSize).Take(pageSize);
            
            var result = paginatedQuery.Select(a => new
            {
                id = a.Id,
                Fmodelnumber = a.Fmodelnumber,
                Fmodelname = a.Fmodelname,
                Fplanbegindate = a.Fplanbegindate,
                Fplanenddate = a.Fplanenddate,
                Fdays = a.Fdays,
                Fnote = a.Fnote,
                Sli_plan_modelEntry = a.Sli_plan_modelEntry.Select(b => new
                {
                    id = b.Id,
                    Fmodelid = b.Fmodelid,
                    Fplanoptionid = b.Fplanoptionid,
                    Fdays = b.Fdays,
                    Fdepartid = b.Fdepartid,
                    Fempid = b.Fempid,
                    Fseq=b.Fseq
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
            var query = context.Sli_plan_model.Include(a => a.Sli_plan_modelEntry);
            if (id.HasValue)
            {
                query = query.Where(q => q.Id==id);

            }

            //var paginatedQuery = query.OrderByDescending(b => b.Id);


            var result = query.Select(a => new
            {
                Id = a.Id,
                Fmodelnumber = a.Fmodelnumber,
                Fmodelname = a.Fmodelname,
                Fplanbegindate = a.Fplanbegindate,
                Fplanenddate = a.Fplanenddate,
                Fdays = a.Fdays,
                Fnote = a.Fnote,
                Sli_plan_modelEntry = a.Sli_plan_modelEntry.OrderBy(b => b.Fseq).Select(b => new
                {
                    id = b.Id,
                    Fmodelid = b.Fmodelid,
                    Fplanoptionid = b.Fplanoptionid,
                    Fdays = b.Fdays,
                    Fdepartid = b.Fdepartid,
                    Fempid = b.Fempid,
                    Fseq = b.Fseq
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
                var headersToDelete = context.Sli_plan_model.Include(h => h.Sli_plan_modelEntry).Where(h => id.Contains(h.Id)).ToList();
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
                    var Sli_plan_modelEntrys = context.Sli_plan_modelEntry.Where(b => b.Fmodelid == DeleteID);
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
        public async Task<object> Update([Microsoft.AspNetCore.Mvc.FromBody] Sli_plan_model model)
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
                    var Sli_plan_modelEntrys = context.Sli_plan_modelEntry.Where(p => p.Fmodelid == model.Id).ToList();


                    Sli_plan_models.Fmodelname = model.Fmodelname;
                    Sli_plan_models.Fmodelnumber = model.Fmodelnumber;
                    Sli_plan_models.Fplanbegindate = model.Fplanbegindate;
                    Sli_plan_models.Fplanenddate = model.Fplanenddate;
                    Sli_plan_models.Fdays = model.Fdays;
                    Sli_plan_models.Fnote = model.Fnote;
                    context.Sli_plan_modelEntry.RemoveRange(Sli_plan_modelEntrys);

                    foreach (var childTableData in model.Sli_plan_modelEntry)
                    {
                        var entry = new Sli_plan_modelEntry
                        {
                            Fmodelid = model.Id,
                            Fplanoptionid = childTableData.Fplanoptionid,
                            Fdays = childTableData.Fdays,
                            Fdepartid = childTableData.Fdepartid,
                            Fempid = childTableData.Fempid,
                            Fseq = childTableData.Fseq
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