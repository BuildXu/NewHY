using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi_SY.Entity;
using WebApi_SY.Models;

namespace WebApi_SY.Controllers
{
    public class sli_sal_order_viewController : ApiController
    {
        public sli_sal_order_viewController()
        {
            // 这里可以添加构造函数的具体实现，例如初始化数据库上下文等操作
        }

        //[System.Web.Http.HttpPost]
        //public async Task<object> Insert([Microsoft.AspNetCore.Mvc.FromBody] sli_sal_order_view order)
        //{
        //    try
        //    {
        //        var context = new YourDbContext();
        //        var header = new sli_sal_order_view
        //        {
        //            FBILLNO = order.FBILLNO,
        //            FDATE = order.FDATE,
        //            FCUSTID = order.FCUSTID,
        //            FNUMBER = order.FNUMBER,
        //            FNAME = order.FNAME,
        //            FSumNUMBER = order.FSumNUMBER,
        //            sli_sal_orderEntry_view = order.sli_sal_orderEntry_view.Select(d => new sli_sal_orderEntry_view
        //            {
        //                FENTRYID = d.FENTRYID,
        //                FSEQ = d.FSEQ,
        //                FQTY = d.FQTY,
        //                FNOTE = d.FNOTE,
        //                FPLANDELIVERYDATE = d.FPLANDELIVERYDATE,
        //                FSTOCKQTY = d.FSTOCKQTY,
        //                FmaterialID = d.FmaterialID,
        //                Fnumber = d.Fnumber,
        //                Fname = d.Fname,
        //                Fdescription = d.Fdescription,
        //                FsliOuterDiameter = d.FsliOuterDiameter,
        //                FsliInnerDiameter = d.FsliInnerDiameter,
        //                FsliHight = d.FsliHight,
        //                FsliAllowanceOD = d.FsliAllowanceOD,
        //                FsliAllowanceID = d.FsliAllowanceID,
        //                fsliallowanceH = d.fsliallowanceH,
        //                FsliWeightMaterial = d.FsliWeightMaterial,
        //                FsliWeightForging = d.FsliWeightForging,
        //                FsliWeightGoods = d.FsliWeightGoods,
        //                FsliDrawingNo = d.FsliDrawingNo,
        //                FsliMetal = d.FsliMetal,
        //                FsliGoodsStatus = d.FsliGoodsStatus,
        //                FsliProcessing = d.FsliProcessing,
        //                FsliDelivery = d.FsliDelivery,
        //                FsliBlankModel = d.FsliBlankModel,
        //                FsliPunching = d.FsliPunching,
        //                FsliTemperatureBegin = d.FsliTemperatureBegin,
        //                FsliTempratureEnd = d.FsliTempratureEnd,
        //                FsliMould = d.FsliMould,
        //                FsliRoller = d.FsliRoller,
        //                FsliHeatingTimes = d.FsliHeatingTimes,
        //                FsliGrade = d.FsliGrade,
        //                FSumNumber = d.FSumNumber
        //            }).ToList()
        //        };

        //        context.Sli_sal_order_view.Add(header);
        //        await context.SaveChangesAsync();
        //        var dataNull = new
        //        {
        //            msg = "Success",
        //            orderId = header.FID,
        //            Date = header.FID.ToString() + "保存成功"
        //        };
        //        return dataNull;
        //    }
        //    catch (Exception ex)
        //    {
        //        return JsonConvert.SerializeObject(ex.ToString());
        //    }
        //}

        //[System.Web.Http.HttpPost]
        //public async Task<object> Delete(int id)
        //{
        //    try
        //    {
        //        var context = new YourDbContext();
        //        var entity = await context.Sli_sal_order_view.FindAsync(id);
        //        if (entity == null)
        //        {
        //            var dataNull = new
        //            {
        //                code = 200,
        //                msg = "ok",
        //                orderId = id.ToString(),
        //                date = id.ToString() + "不存在"
        //            };
        //            return dataNull;
        //        }
        //        var Sli_sal_order_entries = context.Sli_sal_orderEntry_view.Where(b => b.sli_sal_order_view.FID == id);
        //        context.Sli_sal_orderEntry_view.RemoveRange(Sli_sal_order_entries);
        //        context.Sli_sal_order_view.Remove(entity);
        //        await context.SaveChangesAsync();
        //        var data = new
        //        {
        //            code = 200,
        //            msg = "Success",
        //            orderId = id.ToString(),
        //            date = id.ToString() + "删除成功"
        //        };
        //        return data;
        //    }
        //    catch (Exception ex)
        //    {
        //        var data = new
        //        {
        //            code = 400,
        //            msg = "失败",
        //            orderId = id.ToString(),
        //            date = ex.ToString()
        //        };
        //        return data;
        //    }
        //}

        [System.Web.Http.HttpPost]
        public async Task<IActionResult> Update(sli_sal_order_view order)
        {
            var context = new YourDbContext();
            if (context.Entry(order).State == Microsoft.EntityFrameworkCore.EntityState.Detached)
            {
                context.Attach(order);
            }
            context.Entry(order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
            return new NoContentResult();
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public IHttpActionResult GetTable(int page = 1, int pageSize = 10, string fbillNo = null,string fcustomer=null,string fcustName=null, string fcustNo = null)
        {
            var context = new YourDbContext();
            var query = from p in context.Sli_sal_order_view
                        join c in context.Sli_sal_orderEntry_view on p.FID equals c.sli_sal_order_view.FID
                        select new
                        {
                            Sli_sal_order = p,
                            Sli_sal_order_entry = c
                        };
            if (!string.IsNullOrEmpty(fbillNo))
            {
                query = query.Where(q => q.Sli_sal_order.FBILLNO.Contains(fbillNo));
            }

            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedQuery = query.Skip((page - 1) * pageSize).Take(pageSize);
            var result = paginatedQuery.Select(a => new
            {
                id = a.Sli_sal_order != null ? a.Sli_sal_order.FID : 0
            });

            var response = new
            {
                totalCounts = totalCount,
                totalPages = totalPages,
                currentPage = page,
                pageSize = pageSize,
                data = result
            };

            return Ok(response);
        }
    }
}