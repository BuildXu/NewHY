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
        public IHttpActionResult GetTable(int page = 1, int pageSize = 10, string fbillNo = null,  string fcustName = null, string fcustNo = null)
        {
            var context = new YourDbContext();
            var query = from p in context.Sli_sal_order_view
                        join c in context.Sli_sal_orderEntry_view on p.FID equals c.sli_sal_order_view.FID
                        select new
                        {
                            Sli_sal_order_view = p,
                            Sli_sal_orderEntry_view = c
                        };

            if (!string.IsNullOrEmpty(fbillNo))
            {
                query = query.Where(q => q.Sli_sal_order_view.FBILLNO.Contains(fbillNo));
            }
       
          
            if (!string.IsNullOrEmpty(fcustName))
            {
                query = query.Where(q => q.Sli_sal_order_view.FNAME.Contains(fcustName));
            }
            if (!string.IsNullOrEmpty(fcustNo))
            {
                query = query.Where(q => q.Sli_sal_order_view.FNUMBER.Contains(fcustNo));
            }

            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedQuery = query.Skip((page - 1) * pageSize).Take(pageSize);
            var result = paginatedQuery.Select(a => new
            {
                id = a.Sli_sal_order_view != null ? a.Sli_sal_order_view.FID : 0,
                fbillNo = a.Sli_sal_order_view != null ? a.Sli_sal_order_view.FBILLNO : string.Empty,
               // fdate = a.Sli_sal_order_view != null ? a.Sli_sal_order_view.FDATE : string.Empty,
                fcustomer = a.Sli_sal_order_view != null ? a.Sli_sal_order_view.FCUSTID : 0,
                fcustName = a.Sli_sal_order_view != null ? a.Sli_sal_order_view.FNAME : string.Empty,
                fcustNo = a.Sli_sal_order_view != null ? a.Sli_sal_order_view.FNUMBER : string.Empty,
                fsumNumber = a.Sli_sal_order_view != null ? a.Sli_sal_order_view.FSumNUMBER : string.Empty,
                // 子表的信息
                entryId = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.FENTRYID : 0,
                fseq = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.FSEQ : 0,
                fqty = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.FQTY : 0,
                fnote = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.FNOTE : string.Empty,
               /// fplanDeliveryDate = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.FPLANDELIVERYDATE : string.Empty,
                fstockQty = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.FSTOCKQTY : 0,
                fmaterialId = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.FmaterialID : 0,
                fnumber = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.Fnumber : string.Empty,
                fname = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.Fname : string.Empty,
                fdescription = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.Fdescription : string.Empty,
                fsliOuterDiameter = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.FsliOuterDiameter : 0,
                fsliInnerDiameter = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.FsliInnerDiameter : 0,
                fsliHight = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.FsliHight : 0,
                fsliAllowanceOD = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.FsliAllowanceOD : 0,
                fsliAllowanceID = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.FsliAllowanceID : 0,
                fsliallowanceH = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.fsliallowanceH : 0,
                fsliWeightMaterial = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.FsliWeightMaterial : 0,
                fsliWeightForging = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.FsliWeightForging : 0,
                fsliWeightGoods = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.FsliWeightGoods : 0,
                fsliDrawingNo = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.FsliDrawingNo : string.Empty,
                fsliMetal = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.FsliMetal : string.Empty,
                fsliGoodsStatus = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.FsliGoodsStatus : string.Empty,
                fsliProcessing = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.FsliProcessing : string.Empty,
                fsliDelivery = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.FsliDelivery : string.Empty,
                fsliBlankModel = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.FsliBlankModel : string.Empty,
                fsliPunching = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.FsliPunching : string.Empty,
                fsliTemperatureBegin = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.FsliTemperatureBegin : 0,
                fsliTempratureEnd = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.FsliTempratureEnd : 0,
                fsliMould = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.FsliMould : string.Empty,
                fsliRoller = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.FsliRoller : string.Empty,
                fsliHeatingTimes = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.FsliHeatingTimes : 0,
                fsliGrade = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.FsliGrade : string.Empty,
                fsumNumberEntry = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.FSumNumber : string.Empty
            }).ToArray();

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