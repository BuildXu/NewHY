using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi_SY.Entity;
using WebApi_SY.Models;

namespace WebApi_SY.Controllers
{
    public class sli_sal_order_bussController : ApiController
    {
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetTableBysale_order_buss_view(int page = 1, int pageSize = 10, string FCustSum = null, DateTime? FBeginTime = null, DateTime? FEndTime = null)
        {
            try
            {
                var context = new YourDbContext();
                IQueryable<sli_sal_order_buss_view> query = context.Sli_sal_order_buss_view;

                if (!string.IsNullOrEmpty(FCustSum))
                {
                    query = query.Where(q => q.FCustSum.Contains(FCustSum));
                }

                if (FBeginTime.HasValue)
                {
                    query = query.Where(e => e.FDate >= FBeginTime.Value);
                }
                if (FEndTime.HasValue)
                {
                    query = query.Where(e => e.FDate <= FEndTime.Value);
                }
                
                //if (FmaterialID.HasValue)
                //{
                //    query = query.Where(t => t.FmaterialId == FmaterialID.Value);
                //}
                var totalCount = query.Count(); //记录数
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize); // 页数
                var paginatedQuery = query.Skip((page - 1) * pageSize).Take(pageSize); //  某页记录
                //var result = paginatedQuery.Select(a => new
                //{
                //    FID = a.FID,
                //    FBillno = a.FBillno,
                //    FDate = a.FDate,
                //    FCustNmae = a.FCustNmae,
                //    FCustSum = a.FCustSum,
                //    FApproveDate = a.FApproveDate ?? string.Empty
                //});//var datas = query.ToList();
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

                return Json(response);
            }
            catch (Exception ex)
            {
                return Json(ex.ToString());
            }
        }


        public IHttpActionResult GetTableBysal_orderDocument(int ? fid = null)
        {
            try
            {
                var context = new YourDbContext();
                IQueryable<sli_sal_orderDocument> query = context.Sli_sal_orderDocument;



                if (fid.HasValue)
                {
                    query = query.Where(t => t.fid == fid.Value);
                }
                
                                                                                       //var datas = query.ToList();
                var response = new    // 定义 前端返回数据  总记录，总页，当前页 ，size,返回记录
                {
                    code = 200,
                    msg = "OK",
                    data = new
                    {
                        
                        data = query
                    }


                };

                return Json(response);
            }
            catch (Exception ex)
            {
                return Json(ex.ToString());
            }
        }
    }
}