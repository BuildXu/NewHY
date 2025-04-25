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
    //配料计算
    public class sli_material_planController : ApiController
    {
        public sli_material_planController()
        {
            // _context = context;

        }

        /// <summary>
        /// 插入配料表数据0320
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<object> Insert(List<sli_material_plan> model)
        {
            try
            {
                var context = new YourDbContext();
                foreach (var WList in model)
                {
                    var header = new sli_material_plan
                    {
                        Fmaterialid = WList.Fmaterialid,
                        Fmaterialnum = WList.Fmaterialnum,
                        Flotno = WList.Flotno,
                        Fstockid = WList.Fstockid,
                        Fstocklocid = WList.Fstocklocid,
                        Fqtystock = WList.Fqtystock,
                        Fweightstock = WList.Fweightstock,
                        Fqtyused = WList.Fqtyused,
                        Fweightused = WList.Fweightused,
                        Fworkorderlistid = WList.Fworkorderlistid,
                        Fproductno = WList.Fproductno,
                        Fqty = WList.Fqty,
                        Fweight = WList.Fweight,
                        Fqtyactul = WList.Fqtyactul,
                        Fweightactul = WList.Fweightactul,
                        Fstatus = WList.Fstatus
                    };



                    context.Sli_material_plan.Add(header);

                    await context.SaveChangesAsync();
                }
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

        /// <summary>
        /// 获取即时库存信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetInventorys(int page = 1, int pageSize = 10, string Fnumber=null,string Flot=null)
        {
            var context = new YourDbContext();
            IQueryable<sli_stk_inventorys> query = context.Sli_stk_inventorys;

            if (!string.IsNullOrEmpty(Fnumber))
            {
                query = query.Where(q => q.Fmaterialnum.Contains(Fnumber));
            }
            if (!string.IsNullOrEmpty(Flot))
            {
                query = query.Where(q => q.Flot.Contains(Flot));
            }
            query = query.Where(q => q.Fstatus== "可用");
            //query = query.Where(q => q.Fstatus == "可用");
            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedQuery = query.OrderByDescending(b => b.Fmaterialid).Skip((page - 1) * pageSize).Take(pageSize);
            var result = paginatedQuery.Select(a => new
            {
                //id = a.Id,
                Fmaterialid = a.Fmaterialid,
                Fmaterialnum = a.Fmaterialnum,
                FstockNum = a.FstockNum,
                FstockName = a.FstockName,
                Fstocklocid = a.Fstocklocid,
                Fqty = a.Fqty,
                Fsecqty = a.Fsecqty,
                Fmetal = a.Fmetal,
                Flength = a.Flength,
                Flot = a.Flot


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

    }
}