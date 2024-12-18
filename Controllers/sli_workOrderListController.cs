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
    public class sli_workOrderListController : ApiController
    {
        public sli_workOrderListController()
        {
            // _context = context;

        }
        [System.Web.Http.HttpPost]
        public async Task<object> Insert(List<sli_workOrderList> workOrderList)
        {
            try
            {

                var context = new YourDbContext();
                //_context = context;
                //var context1= GetTableByUsername( "test");
                //workOrderList.fproductNumber
                foreach (var WList in workOrderList)
                {
                    var insert = new sli_workOrderList
                    {
                        fproductNumber = maxfproductNumber.IncrementAfterLastSpecialCharacter(WList.fproductNumber),
                        forderEntryid = WList.forderEntryid,
                        fmaterialid = WList.fmaterialid,
                        fworkQty = WList.fworkQty,
                        fworkWeight = WList.fworkWeight,
                        fnote = WList.fnote,
                        fworkOrderListStatus = WList.fworkOrderListStatus,
                        splittype = WList.splittype
                    };

                    context.Sli_workOrderList.Add(insert);






                    var entityToUpdate = context.T_sal_orderEntry.FirstOrDefault(p => p.FENTRYID == Convert.ToInt32(WList.forderEntryid));
                    if (WList.splittype != "样品")
                    {
                        if (entityToUpdate != null)
                        {
                            // 累加字段值
                            entityToUpdate.FWORKORDERLISTQTY += Convert.ToInt32(WList.fworkQty);
                            entityToUpdate.FWORKORDERLISTREMAIN -= Convert.ToInt32(WList.fworkQty);
                            // 保存更改
                            //_context.SaveChanges();
                        }
                    }
                    await context.SaveChangesAsync();
                }

                var dataSucc = new
                {
                    code = 200,
                    msg = "OK",

                    date = " "

                };
                return dataSucc;
                //return Json(new { Result = "Success", orderId = order.Id }, JsonRequestBehavior.);
            }
            catch (Exception ex)
            {
                var dataNo = new
                {
                    code = 400,
                    msg = "失败",

                    Date = ex.ToString()

                };
                return JsonConvert.SerializeObject(dataNo);
                //Console.WriteLine();
            }


        }
        [System.Web.Http.HttpPost]
        public async Task<object> Delete(int id)
        {
            try
            {
                var context = new YourDbContext();
                var entity = await context.Sli_workOrderList.FindAsync(id);
                if (entity == null)
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

                context.Sli_workOrderList.Remove(entity);
                await context.SaveChangesAsync();

                var entityToUpdate = context.T_sal_orderEntry.FirstOrDefault(p => p.FENTRYID == Convert.ToInt32(entity.forderEntryid));
                if (entity.splittype != "样品")
                {
                    if (entityToUpdate != null)
                    {
                        // 累加字段值
                        entityToUpdate.FWORKORDERLISTQTY -= Convert.ToInt32(entity.fworkQty);
                        entityToUpdate.FWORKORDERLISTREMAIN += Convert.ToInt32(entity.fworkQty);
                        // 保存更改
                        //_context.SaveChanges();
                    }
                }

                // var data = new { Status = "Success", Message = "Data retrieved successfully", Data = new { /* actual data here */ } };
                var data = new
                {
                    code = 200,
                    msg = "Success",
                    orderId = id.ToString(),
                    date = id.ToString() + "删除成功"
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


        [Microsoft.AspNetCore.Mvc.HttpGet]
        public IHttpActionResult GetTable(int page = 1, int pageSize = 10, string billNo = null, int? customerId = null, string productName = null)
        {
            var context = new YourDbContext();
            var query = from p in context.Sli_sal_orders_view
                        select p;

            if (!string.IsNullOrEmpty(billNo))
            {
                query = query.Where(q => q.BillNo.Contains(billNo));
            }
            if (customerId.HasValue)
            {
                query = query.Where(q => q.CustomerId == customerId.Value);
            }
            if (!string.IsNullOrEmpty(productName))
            {
                query = query.Where(q => q.ProductName.Contains(productName));
            }

            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedQuery = query.Skip((page - 1) * pageSize).Take(pageSize);
            var result = paginatedQuery.Select(a => new
            {
                id = a.Id,
                billNo = a.BillNo,
                orderId = a.OrderId,
                orderDate = a.OrderDate,
                customerId = a.CustomerId,
                customerName = a.CustomerName,
                customerNumber = a.CustomerNumber,
                entryId1 = a.EntryId1,
                entryId2 = a.EntryId2,
                sequence = a.Sequence,
                quantity = a.Quantity,
                stockQuantity = a.StockQuantity,
                deliveryDate = a.DeliveryDate,
                weightMaterial = a.WeightMaterial,
                materialId = a.MaterialId,
                productNumber = a.ProductNumber,
                productName = a.ProductName,
                productDescription = a.ProductDescription,
                outerDiameter = a.OuterDiameter,
                innerDiameter = a.InnerDiameter,
                height = a.Height,
                allowanceOD = a.AllowanceOD,
                allowanceID = a.AllowanceID,
                allowanceH = a.AllowanceH,
                weightForging = a.WeightForging,
                weightGoods = a.WeightGoods,
                drawingNo = a.DrawingNo,
                metal = a.Metal,
                goodsStatus = a.GoodsStatus,
                processingMethod = a.ProcessingMethod,
                deliveryMethod = a.DeliveryMethod,
                blankModel = a.BlankModel,
                punchingModel = a.PunchingModel,
                temperatureBegin = a.TemperatureBegin,
                temperatureEnd = a.TemperatureEnd,
                mould = a.Mould,
                roller = a.Roller,
                heatingTimes = a.HeatingTimes,
                grade = a.Grade,
                orderNote = a.OrderNote
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
