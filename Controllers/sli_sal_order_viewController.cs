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
        public IHttpActionResult GetTableSeOrder(int Page = 1, int Pagezize = 10, string Fbillno = null,  string Fcustname = null, string Fcustno = null)
        {
            var context = new YourDbContext();
            var query = from p in context.Sli_sal_order_view
                        join c in context.Sli_sal_orderEntry_view on p.Fid equals c.sli_sal_order_view.Fid
                        select new
                        {
                            Sli_sal_order_view = p,
                            Sli_sal_orderEntry_view = c
                        };

            if (!string.IsNullOrEmpty(Fbillno))
            {
                query = query.Where(q => q.Sli_sal_order_view.Fbillno.Contains(Fbillno));
            }
       
          
            if (!string.IsNullOrEmpty(Fcustname))
            {
                query = query.Where(q => q.Sli_sal_order_view.Fname.Contains(Fcustname));
            }
            if (!string.IsNullOrEmpty(Fcustno))
            {
                query = query.Where(q => q.Sli_sal_order_view.Fnumber.Contains(Fcustno));
            }

            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / Pagezize);
            var paginatedQuery = query.Skip((Page - 1) * Pagezize).Take(Pagezize);
            var result = paginatedQuery.Select(a => new
            {
                Id = a.Sli_sal_order_view != null ? a.Sli_sal_order_view.Fid : 0,
                Fbillno = a.Sli_sal_order_view != null ? a.Sli_sal_order_view.Fbillno : string.Empty,
                //Fdate= a.Sli_sal_order_view.Fdate,
                Fdate = a.Sli_sal_order_view!= null? a.Sli_sal_order_view.Fdate : null,
                Fcustomer = a.Sli_sal_order_view != null ? a.Sli_sal_order_view.Fcustid : 0,
                Fcustname = a.Sli_sal_order_view != null ? a.Sli_sal_order_view.Fname : string.Empty,
                Fcustno = a.Sli_sal_order_view != null ? a.Sli_sal_order_view.Fnumber : string.Empty,
                Fsumnumber = a.Sli_sal_order_view != null ? a.Sli_sal_order_view.Fsumnumber : string.Empty,
                
                // 子表的信息
                Entryid = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.FentryId : 0,
                Fseq = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.Fseq : 0,
                Fqty = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.Fqty : 0,
                Fnote = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.Fnote : string.Empty,
                // FplanDeliveryDate = a.Sli_sal_orderEntry_view!= null? a.Sli_sal_orderEntry_view.FPLANDELIVERYDATE : string.Empty,
                Fstockqty = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.fstockqty : 0,
                Fmaterialid = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.FmaterialID : 0,
                Fnumber = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.Fnumber : string.Empty,
                Fname = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.Fname : string.Empty,
                Fdescription = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.Fdescription : string.Empty,
                Fsliouterdiameter = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.FsliOuterDiameter : 0,
                Fsliinnerdiameter = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.FsliInnerDiameter : 0,
                Fslihight = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.FsliHight : 0,
                Fsliallowanceod = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.FsliAllowanceOD : 0,
                Fsliallowanceid = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.FsliAllowanceid : 0,
                Fsliallowanceh = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.fsliallowanceH : 0,
                Fsliweightmaterial = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.FsliWeightmaterial : 0,
                Fsliweightforging = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.FsliWeightforging : 0,
                Fsliweightgoods = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.FsliWeightGoods : 0,
                Fslirawingno = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.Fslidrawingno : string.Empty,
                Fslimetal = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.FsliMetal : string.Empty,
                Fsligoodsstatus = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.FsliGoodsStatus : string.Empty,
                Fsliprocessing = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.FsliProcessing : string.Empty,
                Fsliedelivery = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.Fslidelivery : string.Empty,
                Fsliblankmodel = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.Fsliblankmodel : string.Empty,
                FsliPunching = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.FsliPunching : string.Empty,
                FsliTemperatureBegin = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.FsliTemperatureBegin : 0,
                FsliTempratureEnd = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.FsliTempratureEnd : 0,
                Fslimould = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.Fslimould : string.Empty,
                Fsliroller = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.Fsliroller : string.Empty,
                Fsliheatingtimes = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.FsliHeatingTimes : 0,
                Fsligrade = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.FsliGrade : string.Empty,
                FsumnumberEntry = a.Sli_sal_orderEntry_view != null ? a.Sli_sal_orderEntry_view.FSumNumber : string.Empty
            }).ToArray();

            var response = new
            {
                code = 200,
                msg = "OK",
                data = new { 
                totalCounts = totalCount,
                totalPages = totalPages,
                currentPage = Page,
                pageSize = Pagezize,
                data = result
                }
            };

            return Ok(response);
        }
    }
}