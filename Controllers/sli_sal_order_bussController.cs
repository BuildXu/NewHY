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

        public class UpdateRequest
        {
            public int Id { get; set; }
            public int Fdocid { get; set; }
            public string Fnumber { get; set; }
            public int Fid { get; set; }
            public string Fslimetal { get; set; }
        }
        [System.Web.Http.HttpPost]
        public IHttpActionResult UpdateData([FromBody] UpdateRequest request)
        {
            try
            {
                var context = new YourDbContext();
                var sli_doc_sales = context.Sli_doc_sales.FirstOrDefault(p => p.Id == request.Id);
                sli_doc_sales.Fdocid = request.Fdocid;
                sli_doc_sales.Fdocno = request.Fnumber;
                context.SaveChanges();
                //var FSLIMETEL= context.T_BAS_PREBDONE.Where(p => p.FNUMBER == request.Fslimetal).Select(p => p.FID);
                var result = context.T_BAS_PREBDONE .Where(p => p.FNUMBER == request.Fslimetal).Select(p => p.FID).FirstOrDefault();
                System.Diagnostics.Debug.WriteLine(result);
                System.Diagnostics.Debug.WriteLine(request.Fid);
                var products = context.T_sal_orderEntry.Where(p => p.FSLIMETEL == result && p.FID == request.Fid).ToList();

                foreach (var product in products)
                {
                    product.FSLISALETECHNO =Convert.ToString( request.Fnumber);
                    //var result1 = context.T_sal_orderEntry.Where(p => p.FENTRYID == product.FENTRYID).FirstOrDefault();
                    //result1.FSLISALETECHNO = Convert.ToString(request.Fnumber);
                    // product.fmaterialNumber = resultdata.Result.Number;
                }
                //
                context.SaveChanges();
                //sli_doc_sales.Fdocno = "";

                var datas = new
                {
                    code = 200,
                    msg = "ok",
                    date = request.Id + "更新成功！"
                };
                return Ok(datas);

            }
            catch (Exception ex)
            {
                var datas = new
                {
                    code = 400,
                    msg = ex.ToString(),
                    date = request.Id + "更新失败！"
                };
                return Ok(datas);
            }
        }
        public class UpdateRequestEntry
        {
            public int FentryId { get; set; }
            public string Fnumber { get; set; }
        }
        public IHttpActionResult UpdateDataEntry([FromBody] UpdateRequestEntry request)
        {
            try
            {
                var context = new YourDbContext();
                
                //var FSLIMETEL= context.T_BAS_PREBDONE.Where(p => p.FNUMBER == request.Fslimetal).Select(p => p.FID);
               
                var products = context.T_sal_orderEntry.Where(p => p.FENTRYID == request.FentryId).ToList();

                foreach (var product in products)
                {
                    product.FSLITECHNO = Convert.ToString(request.Fnumber);
                    //var result1 = context.T_sal_orderEntry.Where(p => p.FENTRYID == product.FENTRYID).FirstOrDefault();
                    //result1.FSLISALETECHNO = Convert.ToString(request.Fnumber);
                    // product.fmaterialNumber = resultdata.Result.Number;
                }
                //
                context.SaveChanges();
                //sli_doc_sales.Fdocno = "";

                var datas = new
                {
                    code = 200,
                    msg = "ok",
                    date = request.FentryId + "更新成功！"
                };
                return Ok(datas);

            }
            catch (Exception ex)
            {
                var datas = new
                {
                    code = 400,
                    msg = ex.ToString(),
                    date = request.FentryId + "更新失败！"
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
                var paginatedQuery = query.OrderByDescending(b => b.FID).Skip((page - 1) * pageSize).Take(pageSize); //  某页记录
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
                IQueryable<sli_doc_sales> query = context.Sli_doc_sales;

                //var Sli_doc_sales = context.Sli_doc_sales.FirstOrDefault(p => p.Fid == fid);
                if (fid.HasValue)
                {
                    query = query.Where(t => t.Fid == fid.Value);
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