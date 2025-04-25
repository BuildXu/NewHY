using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Xml.Linq;
using WebApi_SY.Entity;
using WebApi_SY.Models;
using static Azure.Core.HttpHeader;

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

        public async Task<IHttpActionResult>   InsertData()
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
                List<sli_document_tech_saleBillEntry> entryList = new List<sli_document_tech_saleBillEntry>();
                List<sli_document_tech_saleBill> billList = new List<sli_document_tech_saleBill>();
                List<sli_document_tech_saleAttachment> attachmentList = new List<sli_document_tech_saleAttachment>();
                var entry = new sli_document_tech_saleBillEntry();
                var attachment = new sli_document_tech_saleAttachment();
                using (var dbContext = new YourDbContext())
                {
                    var provider = new MultipartMemoryStreamProvider();
                    var task = Request.Content.ReadAsMultipartAsync(provider);
                    //task.Wait();
                    await task;
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

                                dynamic sale1 = JsonConvert.DeserializeObject(propertyValue);

                                sale.Fnumber = sale1.Fnumber;
                                sale.Fname = sale1.Fname;
                                sale.Fdate = sale1.Fdate;
                                if (Convert.ToString( sale1.FmaterialID )=="")
                                {
                                    sale.FmaterialID = 0;
                                }
                                else
                                {
                                    sale.FmaterialID = sale1.FmaterialID;
                                }

                                if (Convert.ToString(sale1.FcustomerID) == "")
                                {
                                    sale.FcustomerID = 0;
                                }
                                else
                                {
                                    sale.FcustomerID = sale1.FcustomerID;
                                    //sale.FmaterialID = sale1.FmaterialID;
                                }
                                
                                //sale.Fstatus = sale1.Fstatus;
                                //if (sale1.ForderNo == "")
                                //{
                                //    sale.ForderNo = 0;
                                //}
                                //else
                                //{
                                //    sale.ForderNo = int.Parse(sale1.ForderNo);
                                //}

                                //sale.FstandardNo = sale1.FstandardNo;

                                //sale.Ftaxtrue = sale1.Ftaxtrue;
                                dbContext.Sli_document_tech_sale.Add(sale);
                                dbContext.SaveChanges();
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

                                dynamic Bill1 = JsonConvert.DeserializeObject(propertyValue);

                                foreach (var billItem in Bill1)
                                {
                                    //var ftechOptionID1 = 0;

                                    
                                    
                                    billList.Add(new sli_document_tech_saleBill
                                    {
                                        fmainID = sale.Id,
                                        ftechOptionID = billItem.ftechOptionID,


                                        fnote = billItem.fnote
                                    });
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
                                dynamic Entry1 = JsonConvert.DeserializeObject(propertyValue);

                                foreach (var entryItem in Entry1)
                                {
                                    //var ftechObjectID1 = 0;

                                    //if (entryItem.ftechObjectID =="")
                                    //{
                                    //    ftechObjectID1 = 0;
                                    //}
                                    //else
                                    //{
                                    //    ftechObjectID1 = entryItem.ftechObjectID;
                                    //}
                                    entryList.Add(new sli_document_tech_saleBillEntry
                                    {
                                        fbillID = sale.Id,
                                        ftechObjectID = entryItem.ftechObjectID,
                                        fnote = entryItem.fnote,
                                        ftarget = entryItem.ftarget,
                                        fmin = entryItem.fmin,
                                        fmax = entryItem.fmax
                                    });
                                }

                            }
                        }
                        else if (contentDisposition.Name.Trim('\"').StartsWith("Files"))
                        {
                            if (contentDisposition.Name.Trim('\"').EndsWith("Files"))
                            {
                                var fileName = Path.GetFileName(content.Headers.ContentDisposition.FileName.Trim('\"'));
                                var fileData = content.ReadAsByteArrayAsync().Result;
                                attachmentList.Add(new sli_document_tech_saleAttachment
                                {
                                    fattachment = fileName,
                                    fileData = fileData,
                                    fmainID = sale.Id
                                });
                                
                            }
                            
                        }
                        //return contentDisposition.Name;
                    }

                    // 添加子表
                    dbContext.Sli_document_tech_saleBill.AddRange(billList);
                    dbContext.Sli_document_tech_saleBillEntry.AddRange(entryList);
                    dbContext.Sli_document_tech_saleAttachment.AddRange(attachmentList);
                   // dbContext.SaveChanges();

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
        [HttpPost]
        public async Task<IHttpActionResult> UpdateData(int id)
        {
            try
            {
                if (!Request.Content.IsMimeMultipartContent())
                {
                    return BadRequest("Expected multipart/form-data request.");
                }

                var sale = new sli_document_tech_sale();
                var bill = new sli_document_tech_saleBill();
                List<sli_document_tech_saleBillEntry> entryList = new List<sli_document_tech_saleBillEntry>();
                List<sli_document_tech_saleBill> billList = new List<sli_document_tech_saleBill>();
                List<sli_document_tech_saleAttachment> attachmentList = new List<sli_document_tech_saleAttachment>();
                var entry = new sli_document_tech_saleBillEntry();
                var attachment = new sli_document_tech_saleAttachment();


                using (var dbContext = new YourDbContext())
                {
                    var existingbill = dbContext.Sli_document_tech_saleBill.Where(b => b.fmainID == id);
                    dbContext.Sli_document_tech_saleBill.RemoveRange(existingbill);
                    var existingEntry = dbContext.Sli_document_tech_saleBillEntry.Where(b => b.fbillID == id);
                    dbContext.Sli_document_tech_saleBillEntry.RemoveRange(existingEntry);
                    var existingachment = dbContext.Sli_document_tech_saleAttachment.Where(b => b.fmainID == id);
                    dbContext.Sli_document_tech_saleAttachment.RemoveRange(existingachment);
                    dbContext.SaveChanges();

                    //var provider = new MultipartMemoryStreamProvider();
                    var provider = new MultipartMemoryStreamProvider();
                    var task = Request.Content.ReadAsMultipartAsync(provider);
                    //await Request.Content.ReadAsMultipartAsync(provider);
                    //task.Wait();
                    await task;
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

                                dynamic sale1 = JsonConvert.DeserializeObject(propertyValue);
                                var existing = dbContext.Sli_document_tech_sale.Find(id);
                                existing.Fnumber = sale1.Fnumber;
                                existing.Fname = sale1.Fname;
                                existing.FmaterialID = Convert.ToInt32(sale1.FmaterialID);
                                existing.FcustomerID = sale1.FcustomerID;


                                //sale.Fstatus = sale1.Fstatus;
                                //if (sale1.ForderNo == "")
                                //{
                                //    sale.ForderNo = 0;
                                //}
                                //else
                                //{
                                //    sale.ForderNo = int.Parse(sale1.ForderNo);
                                //}

                                //sale.FstandardNo = sale1.FstandardNo;

                                //sale.Ftaxtrue = sale1.Ftaxtrue;
                                //dbContext.Sli_document_tech_sale.Add(existing);
                                dbContext.SaveChanges();
                                //var id = sale1.id;
                                var temp = sale1.sli_document_tech_saleAttachment_view;
                                if (temp.Count > 0)
                                {
                                    foreach (var tempitem in temp)
                                    {
                                        attachmentList.Add(new sli_document_tech_saleAttachment
                                        {
                                            fattachment = tempitem.fattachment,
                                            fileData = tempitem.fileData,
                                            fmainID = id
                                        });
                                        dbContext.Sli_document_tech_saleAttachment.AddRange(attachmentList);

                                    }
                                }
                                //dbContext.SaveChanges();

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

                                dynamic Bill1 = JsonConvert.DeserializeObject(propertyValue);

                                foreach (var billItem in Bill1)
                                {
                                    var ftechOptionID1 = 0;
                                    if (billItem.ftechOptionID is null)
                                    {
                                        ftechOptionID1 = 0;
                                    }
                                    else
                                    {
                                        ftechOptionID1 = Convert.ToInt32(billItem.ftechOptionID);
                                    }

                                    billList.Add(new sli_document_tech_saleBill
                                    {
                                        fmainID = id,
                                        ftechOptionID = ftechOptionID1,
                                        fnote = billItem.fnote
                                    });
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
                                dynamic Entry1 = JsonConvert.DeserializeObject(propertyValue);

                                foreach (var entryItem in Entry1)
                                {
                                    var ftechObjectID1 = 0;
                                    if (entryItem.ftechObjectID is null)
                                    {
                                        ftechObjectID1 = 0;
                                    }
                                    else
                                    {
                                        ftechObjectID1 = Convert.ToInt32(entryItem.ftechObjectID);
                                    }
                                    entryList.Add(new sli_document_tech_saleBillEntry
                                    {
                                        fbillID = id,
                                        ftechObjectID = ftechObjectID1,
                                        fnote = entryItem.fnote,
                                        ftarget = entryItem.ftarget,
                                        fmin = entryItem.fmin,
                                        fmax = entryItem.fmax
                                    });
                                }

                            }
                        }
                        else if (contentDisposition.Name.Trim('\"').StartsWith("Files"))
                        {
                            if (contentDisposition.Name.Trim('\"').EndsWith("Files"))
                            {
                                var fileName = Path.GetFileName(content.Headers.ContentDisposition.FileName.Trim('\"'));
                                var fileData = content.ReadAsByteArrayAsync().Result;
                                attachmentList.Add(new sli_document_tech_saleAttachment
                                {
                                    fattachment = fileName,
                                    fileData = fileData,
                                    fmainID = id
                                });

                            }

                        }
                        //return contentDisposition.Name;
                    }

                    // 添加子表
                    dbContext.Sli_document_tech_saleBill.AddRange(billList);
                    dbContext.Sli_document_tech_saleBillEntry.AddRange(entryList);
                    dbContext.Sli_document_tech_saleAttachment.AddRange(attachmentList);
                    // dbContext.SaveChanges();

                    dbContext.SaveChanges();



                }
                var data = new
                {
                    code = 200,
                    msg = "ok",
                    Id = sale.Id,
                    date = sale.Id + "保存成功"

                };
                return Ok(data);
            }
            catch (Exception ex)
            {
                return Ok(ex.ToString());
            }

        }

        [System.Web.Http.HttpPost]
        public async Task<object> Delete(List<int> id)
        {
            try
            {
                foreach (var deleteid in id)
                {
                    var context = new YourDbContext();
                    var entity = await context.Sli_document_tech_sale.FindAsync(deleteid);
                    if (entity == null)
                    {
                        var dataNull = new
                        {
                            code = 200,
                            msg = "ok",
                            Id = id.ToString(),
                            date = id.ToString() + "不存在"
                        };
                        //string json = JsonConvert.SerializeObject(data);
                        return dataNull;
                    }

                    var existingbill = context.Sli_document_tech_saleBill.Where(b => b.fmainID == deleteid);
                    context.Sli_document_tech_saleBill.RemoveRange(existingbill);
                    var existingEntry = context.Sli_document_tech_saleBillEntry.Where(b => b.fbillID == deleteid);
                    context.Sli_document_tech_saleBillEntry.RemoveRange(existingEntry);
                    var existingachment = context.Sli_document_tech_saleAttachment.Where(b => b.fmainID == deleteid);
                    context.Sli_document_tech_saleAttachment.RemoveRange(existingachment);
                    context.Sli_document_tech_sale.RemoveRange(entity);
                    await context.SaveChangesAsync();

                }
                
                // var data = new { Status = "Success", Message = "Data retrieved successfully", Data = new { /* actual data here */ } };
                var data = new
                {
                    code = 200,
                    msg = "Success",
                    //orderId = id.ToString(),
                    date =  "删除成功"
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


        [HttpGet]
        public IHttpActionResult GetTableBydocument_tech_saleAttachment(int? id = null)
        {
            try
            {
                var context = new YourDbContext();

                var attachment = context.Sli_document_tech_saleAttachment.FirstOrDefault(a => a.id == id);
                if (attachment == null)
                {
                    return NotFound();
                }

                // 将字节数组转换为流
                var stream = new MemoryStream(attachment.fileData);
                // 返回文件流作为响应，并设置适当的内容类型和文件名
                var response = new HttpResponseMessage()
                {
                    Content = new StreamContent(stream),
                    StatusCode = HttpStatusCode.OK
                };
                response.Content.Headers.ContentType = new MediaTypeHeaderValue(GetContentType(attachment.fattachment));
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline")
                {
                    FileName = attachment.fattachment
                };

                return ResponseMessage(response);
                // 返回文件流作为响应



            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }


        }
        private string GetContentType(string fileName)
        {
            var extension = System.IO.Path.GetExtension(fileName);
            switch (extension)
            {
                case ".pdf":
                    return "application/pdf";
                case ".jpg":
                case ".jpeg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                case ".doc":
                    return "application/msword";
                case ".docx":
                    return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                // 添加更多文件类型的处理
                default:
                    return "application/octet-stream";
            }
        }


        [HttpGet]
        public IHttpActionResult GetTableBydocument_tech_sale(int page = 1, int pageSize = 10, string fnumber = null, string fname = null,int Ftype=0)
        {
            try
            {
                var context = new YourDbContext();
                IQueryable<sli_document_tech_sale_view> query = context.Sli_document_tech_sale_view;

                if (!string.IsNullOrEmpty(fnumber))
                {
                    query = query.Where(q => q.Fnumber.Contains(fnumber));
                }

                if (!string.IsNullOrEmpty(fname))
                {
                    query = query.Where(q => q.Fname.Contains(fname));
                }
               
                query = query.Where(q => q.Ftype== Ftype);

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
        [HttpGet]
        public IHttpActionResult GetTableBydocument_tech_saleall(int? id = null)
        {
            try
            {
                var context = new YourDbContext();
                //IQueryable<sli_document_tech_sale> query = context.Sli_document_tech_sale;

                //if (id.HasValue)
                //{
                //    query = query.Where(t => t.Id == id.Value);
                //}

                //var mainTables = query.ToList();

                var techSale = context.Sli_document_tech_sale_view.Find(id);

                techSale.sli_document_tech_saleBill_view = context.Sli_document_tech_saleBill_view.Where(st1 => st1.fmainID == id).ToList() ?? new List<sli_document_tech_saleBill_view>();
                techSale.sli_document_tech_saleBillEntry_view = context.Sli_document_tech_saleBillEntry_view.Where(st2 => st2.fbillID == id).ToList() ?? new List<sli_document_tech_saleBillEntry_view>();
                techSale.sli_document_tech_saleAttachment_view = context.Sli_document_tech_saleAttachment_view.Where(st3 => st3.fmainID == id).ToList() ?? new List<sli_document_tech_saleAttachment_view>();
                //var subTable1List = context.Sli_document_tech_saleBill.Where(st1 => st1.fmainID == id).ToList() ?? new List<sli_document_tech_saleBill>();
                //var subTable2List = context.Sli_document_tech_saleBillEntry.Where(st1 => st1.fbillID == id).ToList() ?? new List<sli_document_tech_saleBillEntry>();
                //var subTable3List = context.Sli_document_tech_saleAttachment.Where(st3 => st3.fmainID == id).ToList() ?? new List<sli_document_tech_saleAttachment>();
                //    var subTable2List = context.Sli_document_tech_saleBillEntry.Where(st2 => st2.fbillID == mainTable.Id).ToList();
                //    var subTable3List = context.Sli_document_tech_saleAttachment.Where(st3 => st3.fmainID == mainTable.Id).ToList();

                var result = new List<object>();

                //foreach (var mainTable in mainTables)
                //{
                //    var subTable1List = context.Sli_document_tech_saleBill.Where(st1 => st1.fmainID == mainTable.Id).ToList();
                //    var subTable2List = context.Sli_document_tech_saleBillEntry.Where(st2 => st2.fbillID == mainTable.Id).ToList();
                //    var subTable3List = context.Sli_document_tech_saleAttachment.Where(st3 => st3.fmainID == mainTable.Id).ToList();

                    

                    
                //}

                result.Add(new
                {
                    Sli_document_tech_sale = techSale,
                    //Sli_document_tech_saleBill = subTable1List,
                    //Sli_document_tech_saleBillEntry = subTable2List,
                    //Sli_document_tech_saleAttachment = subTable3List
                });
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
                return Ok(ex.ToString());
            }
        }
    }
}