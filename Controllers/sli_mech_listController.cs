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
    public class sli_mech_listController : ApiController
    {
        public sli_mech_listController()
        {

        }

        [System.Web.Http.HttpPost]
        public async Task<object> sli_mech_list_Insert([Microsoft.AspNetCore.Mvc.FromBody] sli_mech_list options)
        {
            var context = new YourDbContext();
            try
            {
                var header = new sli_mech_list
                {
                    Fnumber = options.Fnumber,
                    //Id = option.Id,
                    Fname = options.Fname,
                    Fstatus = options.Fstatus
                };
                context.Sli_mech_list.Add(header);
                await context.SaveChangesAsync();
                var datas = new
                {
                    code = 200,
                    msg = "Success",
                    Date = options.Id.ToString() +"保存成功!"
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
        public async Task<object> sli_mech_listUpdate([Microsoft.AspNetCore.Mvc.FromBody] sli_mech_list option)
        {
            try
            {
                var context = new YourDbContext();
                var entity = await context.Sli_mech_list.FindAsync(option.Id);
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
                    var Sli_mech_list = context.Sli_mech_list.FirstOrDefault(p => p.Id == option.Id);


                    Sli_mech_list.Fnumber = option.Fnumber;
                    //Id = option.Id,
                    Sli_mech_list.Fname = option.Fname;
                    //Sli_mech_list.Fstatus = option.Fstatus;


                    await context.SaveChangesAsync();

                    var datas = new
                    {
                        code = 200,
                        msg = "ok",
                        date = Sli_mech_list.Id + "修改成功！"
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
        public async Task<object> sli_mech_listUpdate_status([Microsoft.AspNetCore.Mvc.FromBody] sli_mech_list option)
        {
            try
            {
                var context = new YourDbContext();
                var entity = await context.Sli_mech_list.FindAsync(option.Id);
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
                    var Sli_mech_list = context.Sli_mech_list.FirstOrDefault(p => p.Id == option.Id);


                    //Sli_mech_list.Fnumber = option.Fnumber;
                    ////Id = option.Id,
                    //Sli_mech_list.Fname = option.Fname;
                    Sli_mech_list.Fstatus = option.Fstatus;


                    await context.SaveChangesAsync();

                    var datas = new
                    {
                        code = 200,
                        msg = "ok",
                        date = Sli_mech_list.Id + "修改成功！"
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
        public async Task<object> sli_mech_list_Delete(List<int> id)
        {
            try
            {
                var context = new YourDbContext();
                foreach (var deleteid in id)
                {
                    var entity = await context.Sli_mech_list.FindAsync(deleteid);
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
                    context.Sli_mech_list.Remove(entity);
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
        public IHttpActionResult GetTableBySli_mech_list(int page = 1, int pageSize = 10)
        {
            var context = new YourDbContext();
            var query = context.Sli_mech_list;

            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedQuery = query.OrderByDescending(b => b.Id).Skip((page - 1) * pageSize).Take(pageSize);
            var result = paginatedQuery.Select(a => new
            {
                Id=a.Id,
                Fnumber = a.Fnumber,
                Fname = a.Fname,
                Fstatus = a.Fstatus
            });
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
                    data = result
                }


            };

            return Ok(response);
        }

        [System.Web.Http.HttpGet]
        public IHttpActionResult GetTableBySli_mech_list_ALL(string Fname = null)
        {
            var context = new YourDbContext();
            IQueryable<sli_mech_list> query = context.Sli_mech_list;
            if (!string.IsNullOrEmpty(Fname))
            {
                query = query.Where(q => q.Fname.Contains(Fname));
            }
            var result = query.Select(a => new
            {
                Id = a.Id,
                Fnumber = a.Fnumber,
                Fname = a.Fname,
                Fstatus = a.Fstatus
            });
            var response = new    // 定义 前端返回数据  总记录，总页，当前页 ，size,返回记录
            {
                code = 200,
                msg = "OK",
                data = new
                {

                    data = result
                }


            };

            return Ok(response);
        }


    }
}