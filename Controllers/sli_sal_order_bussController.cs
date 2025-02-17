using Microsoft.IdentityModel.Tokens;
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
    public class sli_sal_order_bussController : ApiController
    {
        public sli_sal_order_bussController()
        {
            // _context = context;

        }

        public async Task<IHttpActionResult> UpdateData(int id=0,int Fdocid=0)
        {
            try
            {
                var context = new YourDbContext();
                var sli_doc_sales = context.Sli_doc_sales.FirstOrDefault(p => p.Id == id);
                sli_doc_sales.Fdocid = Fdocid;

                //sli_doc_sales.Fdocno = "";
                await context.SaveChangesAsync();
                var datas = new
                {
                    code = 200,
                    msg = "ok",
                    date = id + "更新成功！"
                };
                return Ok(datas);

            }
            catch (Exception ex)
            {
                var datas = new
                {
                    code = 200,
                    msg = ex.ToString(),
                    date = id + "更新成功！"
                };
                return Ok(datas);
            }
        }
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
                //IQueryable<sli_doc_sales> query = context.Sli_doc_sales;

                var Sli_doc_sales = context.Sli_doc_sales.FirstOrDefault(p => p.Fid == fid);

                
                
                 //var datas = query.ToList();
                var response = new    // 定义 前端返回数据  总记录，总页，当前页 ，size,返回记录
                {
                    code = 200,
                    msg = "OK",
                    data = new
                    {
                        data = 111
                    }


                };

                return Json(response);
            }
            catch (Exception ex)
            {
                return Json(ex.ToString());
            }
        }


        public IHttpActionResult GetTableBysal_orderentry(int? fid = null)
        {
            try
            {
                var context = new YourDbContext();
                IQueryable<sli_sal_orderEntry_view> query = context.Sli_sal_orderEntry_view;



                if (fid.HasValue)
                {
                    query = query.Where(t => t.Fid == fid.Value);
                }
                //if (!string.IsNullOrEmpty(FsliMetal))
                //{
                //    query = query.Where(q => q.FsliMetal.Contains(FsliMetal));
                //}

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