using System.Web.Http;
using WebApi_SY.Models;
using System.Threading.Tasks;
using WebApi_SY.Entity;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Razor.Tokenizer.Symbols;
using System.Drawing.Printing;

namespace WebApi_SY.Controllers
{


    public class BasedataController : ApiController
    {

        private readonly YourDbContext _context;
        //var _context;

        // 无参数公共构造函数
        public BasedataController()
        {
            var context = new YourDbContext();
            _context = context;
        }

        public BasedataController(YourDbContext context)
        {
           // _context = context;
            
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
        public IHttpActionResult GetTableByDepName(int ? id=null)
        {
            IQueryable<sli_dept_info> query = _context.Sli_dept_info;
            if (id.HasValue)
            {
                query = query.Where(t => t.id == id.Value);
            }

            //var datas = query.ToList();
            var response = new    // 定义 前端返回数据  总记录，总页，当前页 ，size,返回记录
            {
                code = 200,
                msg = "OK",
                data = query
            };

            return Json(response);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public IHttpActionResult GetTableByemploy(int? id = null)
        {
            IQueryable<sli_bd_employ> query = _context.Sli_bd_employ;
            if (id.HasValue)
            {
                query = query.Where(t => t.id == id.Value);
            }

            //var datas = query.ToList();
            var response = new    // 定义 前端返回数据  总记录，总页，当前页 ，size,返回记录
            {
                code = 200,
                msg = "OK",
                data = query
            };

            return Json(response);
        }
        [System.Web.Http.HttpPost]
        public async Task<object> Tech_option_Insert([Microsoft.AspNetCore.Mvc.FromBody] sli_bd_tech_option option)
          {
            //string json = "{\"header\":{\"name\":\"Header Name\",\"details\":[{\"detailContent\":\"Detail 1\"},{\"detailContent\":\"Detail 2\"}]}}";

            //var root = JsonConvert.DeserializeObject<RootObject>(json);
            try
            {
                //var context = new YourDbContext();
                //_context = context;
                var header = new sli_bd_tech_option
                {
                    fname = option.fname,
                    fnumber = option.fnumber,
                    fnote = option.fnote,
                    fstatus = option.fstatus,
                    fused = option.fused,
                    fcreateDate = option.fcreateDate
            };
                _context.Sli_bd_tech_option.Add(header);
                await _context.SaveChangesAsync();
                var datas = new
                {
                    code = 200,
                    msg = "Success",
                    //modelid = header.id,
                    Date = header.id.ToString() + "保存成功"

                };
                return datas;
                //return Json(new { Result = "Success", orderId = order.Id }, JsonRequestBehavior.);
            }
            catch (Exception ex)
            {
                var dataerr = new
                {
                    code = 500,
                    msg = "失败",
                    //modelid = header.id,
                    Date = ex.ToString()

                };
                return dataerr;
                //return JsonConvert.SerializeObject(ex.ToString());
                //Console.WriteLine();
            }
        }

        [System.Web.Http.HttpPost]
        public async Task<object> Tech_option_Update([Microsoft.AspNetCore.Mvc.FromBody] sli_bd_tech_option option)
        {
            try
            {
               // var context = new YourDbContext();
                var entity = await _context.Sli_bd_tech_option.FindAsync(option.id);
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

                    var Sli_bd_tech_options = _context.Sli_bd_tech_option.FirstOrDefault(p => p.id == option.id);
                    //var Sli_plan_modelEntrys = _context.Sli_plan_modelEntry.Where(p => p.fmodelID == model.Id).ToList();


                    Sli_bd_tech_options.fname = option.fname;
                    Sli_bd_tech_options.fnumber = option.fnumber;
                    Sli_bd_tech_options.fnote = option.fnote;
                    Sli_bd_tech_options.fstatus = option.fstatus;
                    Sli_bd_tech_options.fused = option.fused;
                    Sli_bd_tech_options.fcreateDate = option.fcreateDate;
                    //Sli_bd_tech_options.fnote = model.fnote;

                    await _context.SaveChangesAsync();

                    var datas = new
                    {
                        code = 200,
                        msg = "ok",
                        date = Sli_bd_tech_options.id+"修改成功！"
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
        public async Task<object> Tech_option_Delete(List<int> id)
        {
            try
            {
                var context = new YourDbContext();
                //var entity = await context.Sli_plan_model.FindAsync(id);
                //var headersToDelete = context.Sli_plan_model.Where(h => id.Contains(h.Id)).ToList();
                foreach (var deleteid in id)
                {
                   
                    var entity = await context.Sli_bd_tech_option.FindAsync(deleteid);
                    if (entity == null)
                    {
                        var dataNull = new
                        {
                            code = 200,
                            msg = "ok",
                            Id = id.ToString(),
                            date = id.ToString() + "不存在"
                        };
                        //string json = JsonConvert.SerializeObject(data);
                        return dataNull;
                    }

                    //var existingbill = context.Sli_bd_tech_option.Where(b => b.id == deleteid);
                    context.Sli_bd_tech_option.RemoveRange(entity);
                    

                }


                
               
                await context.SaveChangesAsync();
                // var data = new { Status = "Success", Message = "Data retrieved successfully", Data = new { /* actual data here */ } };
                var data = new
                {
                    code = 200,
                    msg = "Success",
                    //orderId = id,
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
                    //orderId = id.ToString(),
                    date = ex.ToString()
                };
                return data;
            }


        }
        public IHttpActionResult GetTableBytech_option(int page = 1, int pageSize = 10, string FNumber = null, string FName = null)
        {
            IQueryable<sli_bd_tech_option> query = _context.Sli_bd_tech_option;

            if (!string.IsNullOrEmpty(FNumber))
            {
                query = query.Where(q => q.fnumber.Contains(FNumber));
            }

            if (!string.IsNullOrEmpty(FName))
            {
                query = query.Where(q => q.fname.Contains(FName));
            }
            var totalCount = query.Count(); //记录数
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize); // 页数
            var paginatedQuery = query.OrderByDescending(b => b.id).Skip((page - 1) * pageSize).Take(pageSize); //  某页记录
            //var datas = query.ToList();
            var response = new    // 定义 前端返回数据  总记录，总页，当前页 ，size,返回记录
            {
                code = 200,
                msg = "OK",
                data = new {
                    totalCounts = totalCount,
                    totalPagess = totalPages,
                    currentPages = page,
                    pageSizes = pageSize,
                    data = paginatedQuery
                }

                
            };

            return Json(response);
        }

        //[System.Web.Http.HttpGet]
        //public IHttpActionResult GetTableBybd_material(string FSumNumber = null)
        //{
        //    var similarEntities = _context.Sli_bd_material_view
        //    .Where(entity => entity.FSumNumber.Contains(FSumNumber) )
        //    .ToList();

        //    if (similarEntities.Count == 0)
        //    {
        //        return NotFound();
        //    }

        //    var response = new    // 定义 前端返回数据  总记录，总页，当前页 ，size,返回记录
        //    {
        //        code = 200,
        //        msg = "OK",
        //        data= similarEntities


        //    };

        //    //return Json(response);
        //    return Ok(response);
        //}


        [System.Web.Http.HttpGet]
        public IHttpActionResult GetTableBybd_customer(string FSumNumber = null)
        {
            var similarEntities = _context.Sli_bd_customer_view
            .Where(entity => entity.FSumNUMBER.Contains(FSumNumber))
            .ToList();

            if (similarEntities.Count == 0)
            {
                var datanull = new    // 定义 前端返回数据  总记录，总页，当前页 ，size,返回记录
                {
                    code = 200,
                    msg = "OK",
                    data = "null"


                };
                return Ok(datanull);
            }

            var response = new    // 定义 前端返回数据  总记录，总页，当前页 ，size,返回记录
            {
                code = 200,
                msg = "OK",
                data = similarEntities
            };

            //return Json(response);
            return Ok(response);
        }

        [System.Web.Http.HttpGet]
        public IHttpActionResult GetTableBybd_materials_view(int? FmaterialID = null, int page = 1, int pageSize = 10,  string Fdescription = null, string FName = null,string FsliDrawingNo=null, string FsliMetal = null)
        {
            try
            {
                var context = new YourDbContext();
                IQueryable<sli_bd_materials_view> query = context.Sli_bd_materials_view;

                if (!string.IsNullOrEmpty(Fdescription))
                {
                    query = query.Where(q => q.Fdescription.Contains(Fdescription));
                }

                if (!string.IsNullOrEmpty(FName))
                {
                    query = query.Where(q => q.Fname.Contains(FName));
                }
                if (!string.IsNullOrEmpty(FsliDrawingNo))
                {
                    query = query.Where(q => q.FsliDrawingNo.Contains(FsliDrawingNo));
                }
                if (!string.IsNullOrEmpty(FsliMetal))
                {
                    query = query.Where(q => q.FsliMetal.Contains(FsliMetal));
                }
                if (FmaterialID.HasValue)
                {
                    query = query.Where(t => t.FmaterialId == FmaterialID.Value);
                }
                var totalCount = query.Count(); //记录数
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize); // 页数
                var paginatedQuery = query.Skip((page - 1) * pageSize).Take(pageSize); //  某页记录
                                                                                       //var datas = query.ToList();
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
                        data = paginatedQuery
                    }


                };

                return Json( response);
            }
            catch (Exception ex)
            {
                return Json(ex.ToString());
            }
        }

        [System.Web.Http.HttpGet]
        public IHttpActionResult GetTableBybd_materials_view_FSumNumber( string FSumNumber=null)
        {
            try
            {
                var context = new YourDbContext();
                IQueryable<sli_bd_materials_view> query = context.Sli_bd_materials_view;

                if (!string.IsNullOrEmpty(FSumNumber))
                {
                    query = query.Where(q => q.FSumNumber.Contains(FSumNumber));
                }

                ///var totalCount = query.Count(); //记录数
                //var totalPages = (int)Math.Ceiling((double)totalCount / pageSize); // 页数
                //var paginatedQuery = query.Skip((page - 1) * pageSize).Take(pageSize); //  某页记录
                                                                                       //var datas = query.ToList();
                var response = new    // 定义 前端返回数据  总记录，总页，当前页 ，size,返回记录
                {
                    code = 200,
                    msg = "OK",
                    data = query
                    //{
                    //    //totalCounts = totalCount,
                    //    //totalPagess = totalPages,
                    //   // currentPages = page,
                    //    //pageSizes = pageSize,
                    //    data = query
                    //}


                };

                return Json(response);
            }
            catch (Exception ex)
            {
                return Json(ex.ToString());
            }
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public IHttpActionResult GetTableByplanOption(int? id = null)
        {
            IQueryable<sli_bd_planOption> query = _context.Sli_bd_planOption;
            if (id.HasValue)
            {
                query = query.Where(t => t.id == id.Value);
            }

            //var datas = query.ToList();
            var response = new    // 定义 前端返回数据  总记录，总页，当前页 ，size,返回记录
            {
                code = 200,
                msg = "OK",

                data = query
            };

            return Json(response);
        }


        [Microsoft.AspNetCore.Mvc.HttpGet]
        //定义 get 入参
        public IHttpActionResult GetTableSli_plan_model(int page = 1, int pageSize = 10, string fmodelNumber = null, string fmodelName = null, int? fdays = null)
        {
            var query = from p in _context.Sli_plan_model
                        join c in _context.Sli_plan_modelEntry on p.Id equals c.Fmodelid
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
                id = a.Sli_plan_model != null ? a.Sli_plan_model.Id : 0,
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
