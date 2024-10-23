using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi_SY.Entity;
using WebApi_SY.Models;

namespace WebApi_SY.Controllers
{
    public class sli_document_tech_saleController : ApiController
    {
        public sli_document_tech_saleController()
        {
            // _context = context;

        }
        public class InsertRequestDto_tech_sale
        {
            public sli_document_tech_sale Tech_sale { get; set; }
            public List<sli_document_tech_saleBill> Tech_saleBill { get; set; }
            public List<sli_document_tech_saleBillEntry> Tech_saleBillEntry { get; set; }
            public List<sli_document_tech_saleAttachment> Tech_saleAttachment { get; set; }
        }
        public async Task<object> Insert(InsertRequestDto_tech_sale requestDto)
        {
            try
            {
                var context = new YourDbContext();
                // 将 DTO 转换为实体对象
                var tableHeader = new sli_document_tech_sale
                {
                    // 根据 DTO 的属性设置实体对象的属性
                    Fnumber = requestDto.Tech_sale.Fnumber,
                    Fname = requestDto.Tech_sale.Fname,
                    Fdate = requestDto.Tech_sale.Fdate,
                    FbillerID = requestDto.Tech_sale.FbillerID,
                    Fstatus = requestDto.Tech_sale.Fstatus,
                    FcustomerID = requestDto.Tech_sale.FcustomerID,
                    FmaterialID = requestDto.Tech_sale.FmaterialID,
                    ForderNo = requestDto.Tech_sale.ForderNo,
                    ForderEntryID = requestDto.Tech_sale.ForderEntryID,
                    FstandardNo = requestDto.Tech_sale.FstandardNo,
                    Ftaxtrue = requestDto.Tech_sale.Ftaxtrue,
                    fdefind01 = requestDto.Tech_sale.fdefind01,
                    fdefind02 = requestDto.Tech_sale.fdefind02,
                    fdefind03 = requestDto.Tech_sale.fdefind03,
                    fdefind04 = requestDto.Tech_sale.fdefind04,
                    fdefind05 = requestDto.Tech_sale.fdefind05,

                };

                var tableBody1Entities = requestDto.Tech_saleBill.Select(dto => new sli_document_tech_saleBill
                {
                    //fmainID = dto.fmainID,
                    ftechOptionID = dto.ftechOptionID,
                    fnote = dto.fnote,
                    fmainID = tableHeader.Id

                }).ToList();

                var tableBody2Entities = requestDto.Tech_saleBillEntry.Select(dto => new sli_document_tech_saleBillEntry
                {
                    ftechObjectID = dto.ftechObjectID,
                    fmax = dto.fmax,
                    fmin = dto.fmin,
                    ftarget = dto.ftarget,
                    fnote = dto.fnote,
                    fnoties = dto.fnoties,
                    fexplanation = dto.fexplanation,
                    fbillID = tableHeader.Id
                }).ToList();

                var tableBodyAttachment = requestDto.Tech_saleAttachment.Select(dto => new sli_document_tech_saleAttachment
                {
                    //fmainID = dto.fmainID,
                    fattachment = dto.fattachment,
                    fmainID = tableHeader.Id

                }).ToList();
                InsertDataToTables insert = new InsertDataToTables();
                // 调用数据访问层方法插入数据
                await insert.InsertData_document_tech_sale(context, tableHeader, tableBody1Entities, tableBody2Entities, tableBodyAttachment);

                var data = new
                {
                    code = 200,
                    msg = "ok",
                    Id = tableHeader.Id,
                    date = tableHeader.Id + "保存成功"

                };
                return data;
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult GetTableBydocument_tech_sale(int page = 1, int pageSize = 10, string fnumber = null, string fname = null)
        {
            try
            {
                var context = new YourDbContext();
                IQueryable<sli_document_tech_sale> query = context.Sli_document_tech_sale;

                if (!string.IsNullOrEmpty(fnumber))
                {
                    query = query.Where(q => q.Fnumber.Contains(fnumber));
                }

                if (!string.IsNullOrEmpty(fname))
                {
                    query = query.Where(q => q.Fname.Contains(fname));
                }
                var totalCount = query.Count(); //记录数
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize); // 页数
                var paginatedQuery = query.OrderByDescending(b => b.Id).Skip((page - 1) * pageSize).Take(pageSize); //  某页记录
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

                return Json(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public IHttpActionResult GetTableBydocument_tech_saleall(int? id = null)
        {
            try
            {
                var context = new YourDbContext();
                IQueryable<sli_document_tech_sale> query = context.Sli_document_tech_sale;

                if (id.HasValue)
                {
                    query = query.Where(t => t.Id == id.Value);
                }

                var mainTables = query.ToList();

                var result = new List<object>();

                foreach (var mainTable in mainTables)
                {
                    var subTable1List = context.Sli_document_tech_saleBill.Where(st1 => st1.fmainID == mainTable.Id).ToList();
                    var subTable2List = context.Sli_document_tech_saleBillEntry.Where(st2 => st2.fbillID == mainTable.Id).ToList();
                    var subTable3List = context.Sli_document_tech_saleAttachment.Where(st3 => st3.fmainID == mainTable.Id).ToList();

                    result.Add(new
                    {
                        Sli_document_tech_sale = mainTable,
                        //SubTable1s = subTable1List,
                        //SubTable2s = subTable2List,
                        //SubTable3s = subTable3List
                    });
                }
                //var totalCount = result.Count(); //记录数
                //var totalPages = (int)Math.Ceiling((double)totalCount / pageSize); // 页数
                //var paginatedQuery = query; //  某页记录
                var response = new    // 定义 前端返回数据  总记录，总页，当前页 ，size,返回记录
                {
                    code = 200,
                    msg = "OK",
                    data = new
                    {
                        //totalCounts = totalCount,
                        //totalPagess = totalPages,
                        //currentPages = page,
                        //pageSizes = pageSize,
                        data = result
                    }
                };

                return Ok(response);

                


                //var mainTableList = query.ToList();

                //var result = new List<object>();
                //foreach (var mainTable in mainTableList)
                //{
                //    var subTable1List = context.Sli_document_tech_saleBill.Where(st1 => st1.fmainID == mainTable.Id).ToList();
                //    var subTable2List = context.Sli_document_tech_saleBillEntry.Where(st2 => st2.fbillID == mainTable.Id).ToList();
                //    var subTable3List = context.Sli_document_tech_saleAttachment.Where(st3 => st3.fmainID == mainTable.Id).ToList();

                //    result.Add(new
                //    {
                //        Sli_document_tech_sale = mainTable,
                //        Sli_document_tech_saleBill = subTable1List,
                //        Sli_document_tech_saleBillEntry = subTable2List,
                //        Sli_document_tech_saleAttachment = subTable3List
                //    });
                //}

                
                //                                                                                      //var datas = query.ToList();
                


                //};

                //return Json(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}