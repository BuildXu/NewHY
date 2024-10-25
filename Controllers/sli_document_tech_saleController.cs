using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
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
        //public class InsertRequestDto_tech_sale
        //{
        //    public sli_document_tech_sale Tech_sale { get; set; }
        //    public sli_document_tech_saleBill Tech_saleBill { get; set; }
        //    public sli_document_tech_saleBillEntry Tech_saleBillEntry { get; set; }
        //    public sli_document_tech_saleAttachment Tech_saleAttachment { get; set; }
        //    public  HttpPostedFile Files { get; set; }
        //}
        [HttpPost]
         //public async Task<object> InsertData()

        public IHttpActionResult   InsertData()
        {
            try
            {
                //var context = new YourDbContext();
                if (!Request.Content.IsMimeMultipartContent())
                {
                    return BadRequest("Expected multipart/form-data request.");
                }

                var sale = new sli_document_tech_sale();
                var bill = new sli_document_tech_saleBill();
                var entry = new sli_document_tech_saleBillEntry();
                var attachment = new sli_document_tech_saleAttachment();

                var provider = new MultipartMemoryStreamProvider();
                var task = Request.Content.ReadAsMultipartAsync(provider);
                //task.Wait();
                //await task;
                foreach (var content in provider.Contents)
                {
                    var contentDisposition = content.Headers.ContentDisposition;
                    
                    if (contentDisposition.Name.Trim('\"').StartsWith("Sale"))
                    {
                        var keyValuePairs = contentDisposition.Name.Trim('\"').Substring("Sale".Length).Trim('[', ']').Split('=');
                        if (keyValuePairs.Length == 1)
                        {
                            var propertyName = keyValuePairs[0];
                            var propertyValue = content.ReadAsStringAsync().Result;
                            if (propertyName == "Fnumber")
                            {
                                sale.Fnumber = propertyValue;
                            }
                            else if (propertyName == "Fname")
                            {
                                sale.Fname = propertyValue;
                            }
                            else if (propertyName == "Fdate")
                            {
                                sale.Fdate = propertyValue;
                            }
                            else if (propertyName == "FbillerID")
                            {
                                sale.FbillerID = int.Parse(propertyValue);
                            }
                            else if (propertyName == "Fstatus")
                            {
                                sale.Fstatus = propertyValue;
                            }
                            else if (propertyName == "FcustomerID")
                            {
                                sale.FcustomerID = propertyValue;
                            }
                            else if (propertyName == "FmaterialID")
                            {
                                sale.FmaterialID = int.Parse(propertyValue);
                            }
                            else if (propertyName == "ForderNo")
                            {
                                sale.ForderNo = int.Parse(propertyValue);
                            }
                            else if (propertyName == "ForderEntryID")
                            {
                                sale.ForderEntryID = int.Parse(propertyValue);
                            }
                            else if (propertyName == "FstandardNo")
                            {
                                sale.FstandardNo = propertyValue;
                            }
                            else if (propertyName == "Ftaxtrue")
                            {
                                sale.Ftaxtrue = propertyValue;
                            }
                        }
                    }
                    else if (contentDisposition.Name.Trim('\"').StartsWith("Bill"))
                    {
                        // 解析子表1数据
                        var keyValuePairs = contentDisposition.Name.Trim('\"').Substring("Bill".Length).Trim('[', ']').Split('=');
                        if (keyValuePairs.Length == 1)
                        {
                            var propertyName = keyValuePairs[0];
                            var propertyValue = content.ReadAsStringAsync().Result;
                            if (propertyName == "ftechOptionID")
                            {
                                bill.ftechOptionID = int.Parse(propertyValue);
                            }
                            else if (propertyName == "fnote")
                            {
                                bill.fnote = propertyValue;
                            }
                        }
                    }
                    else if (contentDisposition.Name.Trim('\"').StartsWith("Entry"))
                    {
                        // 解析子表2数据
                        var keyValuePairs = contentDisposition.Name.Trim('\"').Substring("Entry".Length).Trim('[', ']').Split('=');
                        if (keyValuePairs.Length == 1)
                        {
                            var propertyName = keyValuePairs[0];
                            var propertyValue = content.ReadAsStringAsync().Result;
                            if (propertyName == "ftechObjectID")
                            {
                                entry.ftechObjectID = int.Parse(propertyValue);
                            }
                            else if (propertyName == "fmax")
                            {
                                entry.fmax = propertyValue;
                            }
                            else if (propertyName == "fmin")
                            {
                                entry.fmin = propertyValue;
                            }
                            else if (propertyName == "ftarget")
                            {
                                entry.ftarget = propertyValue;
                            }
                            else if (propertyName == "fnote")
                            {
                                entry.fnote = propertyValue;
                            }
                            else if (propertyName == "fnoties")
                            {
                                entry.fnoties = propertyValue;
                            }
                            else if (propertyName == "fexplanation")
                            {
                                entry.fexplanation = propertyValue;
                            }
                        }
                    }
                    else if (contentDisposition.Name.Trim('\"').StartsWith("File"))
                    {
                        if (contentDisposition.Name.Trim('\"').EndsWith("File"))
                        {
                            var fileName = Path.GetFileName(content.Headers.ContentDisposition.FileName.Trim('\"'));
                            var fileData = content.ReadAsByteArrayAsync().Result;
                            attachment.fattachment = fileName;
                            attachment.fileData = fileData;
                        }
                    }
                    //return contentDisposition.Name;
                }
                using (var dbContext = new YourDbContext())
                {

                    // 添加表头
                    dbContext.Sli_document_tech_sale.Add(sale);
                    dbContext.SaveChanges();

                    // 设置子表与表头的关联
                    bill.fmainID = sale.Id;
                    entry.fbillID = sale.Id;
                    attachment.fmainID = sale.Id;

                    // 添加子表
                    dbContext.Sli_document_tech_saleBill.Add(bill);
                    dbContext.Sli_document_tech_saleBillEntry.Add(entry);
                    dbContext.Sli_document_tech_saleAttachment.Add(attachment);

                    dbContext.SaveChanges();



                }


                #region
                /*
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
                var tech_saleAttachment3 = new sli_document_tech_saleAttachment();


                if (requestDto.Files != null && requestDto.Files.ContentLength > 0)
                {
                    tech_saleAttachment3.fattachment = Path.GetFileName(requestDto.Files.FileName);
                    using (var binaryReader = new BinaryReader(requestDto.Files.InputStream))
                    {
                        tech_saleAttachment3.fileData = binaryReader.ReadBytes(requestDto.Files.ContentLength);
                    }
                    tech_saleAttachment3.fmainID = tableHeader.Id;
                }
                
                var tableBodyAttachment = requestDto.Tech_saleAttachment.Select(dto => new sli_document_tech_saleAttachment
                {

                    //fmainID = dto.fmainID,
                    fattachment = dto.fattachment,
                    fmainID = tableHeader.Id

                }).ToList();
                InsertDataToTables insert = new InsertDataToTables();
                // 调用数据访问层方法插入数据
                await insert.InsertData_document_tech_sale(context, tableHeader, tableBody1Entities, tableBody2Entities, tech_saleAttachment3);
                */
                #endregion
                var data = new
                {
                    code = 200,
                    msg = "ok",
                    Id = sale.Id,
                    date = sale.Id + "保存成功"

                };
                return Ok( data);
            }
            catch (Exception ex)
            {
                return Ok(ex.ToString());
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