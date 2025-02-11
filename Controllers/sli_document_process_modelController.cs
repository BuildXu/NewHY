using Newtonsoft.Json;
using NPOI.POIFS.Properties;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Http;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using WebApi_SY.Entity;
using WebApi_SY.Models;

namespace WebApi_SY.Controllers
{
    public class sli_document_process_modelController : ApiController
    {
       // private readonly YourDbContext _context; // 声明上下文变量

        // 通过构造函数注入上下文
        //public sli_document_process_modelController(YourDbContext context)
        //{
        //    //_context = context;
        //}
        public sli_document_process_modelController()
        {
            // _context = context;

        }
        public async Task<IHttpActionResult> InsertData()
        {

            try
            {
                #region
                ////var context = new YourDbContext();
                //if (!Request.Content.IsMimeMultipartContent())
                //{
                //    return BadRequest("Expected multipart/form-data request.");
                //}

                //var sale = new sli_document_process_model();
                //var bill = new sli_document_process_modelBill();
                //List<sli_document_process_modelBillEntry> entryList = new List<sli_document_process_modelBillEntry>();
                //List<sli_document_process_modelBill> billList = new List<sli_document_process_modelBill>();
                //List<sli_document_process_modelAttachment> attachmentList = new List<sli_document_process_modelAttachment>();
                //var entry = new sli_document_process_modelBillEntry();
                //var attachment = new sli_document_process_modelAttachment();
                //using (var dbContext = new YourDbContext())
                //{
                //    var provider = new MultipartMemoryStreamProvider();
                //    var task = Request.Content.ReadAsMultipartAsync(provider);
                //    //task.Wait();
                //    //await task;
                //    foreach (var content in provider.Contents)
                //    {
                //        var contentDisposition = content.Headers.ContentDisposition;

                //        if (contentDisposition.Name.Trim('\"').StartsWith("Sale"))
                //        {
                //            var keyValuePairs = contentDisposition.Name.Trim('\"').Substring("Sale".Length).Trim('[', ']').Split('=');
                //            if (keyValuePairs.Length == 1)
                //            {
                //                var propertyName = keyValuePairs[0];
                //                var propertyValue = content.ReadAsStringAsync().Result;

                //                dynamic sale1 = JsonConvert.DeserializeObject(propertyValue);

                //                sale.Fnumber = sale1.Fnumber;
                //                sale.Fname = sale1.Fname;
                //                sale.Fdate =sale1.Fdate;
                //                sale.Ftaxtrue = sale1.Ftaxtrue;
                //                sale.Fbillerid = sale1.Fbillerid;
                //                sale.Fstatus = sale1.Fstatus;
                //                dbContext.Sli_document_process_model.Add(sale);
                //                dbContext.SaveChanges();
                //            }
                //        }
                //        else if (contentDisposition.Name.Trim('\"').StartsWith("Bill"))
                //        {
                //            // 解析子表1数据
                //            var keyValuePairs = contentDisposition.Name.Trim('\"').Substring("Bill".Length).Trim('[', ']').Split('=');
                //            if (keyValuePairs.Length == 1)
                //            {
                //                var propertyName = keyValuePairs[0];
                //                var propertyValue = content.ReadAsStringAsync().Result;

                //                dynamic Bill1 = JsonConvert.DeserializeObject(propertyValue);

                //                foreach (var billItem in Bill1)
                //                {

                //                    billList.Add(new sli_document_process_modelBill
                //                    {
                //                        Id = sale.Id,
                //                        Foptionid = billItem.Foptionid,
                //                        Fdeptid = billItem.Fdeptid,
                //                        Fnote = billItem.Fnote,
                //                        Fstatus = billItem.Fstatus,
                //                    });
                //                }


                //                dbContext.Sli_document_process_modelBill.AddRange(billList);

                //            }
                //            dbContext.SaveChanges();

                //        }
                //        else if (contentDisposition.Name.Trim('\"').StartsWith("Entry"))
                //        {
                //            // 解析子表2数据
                //            var keyValuePairs = contentDisposition.Name.Trim('\"').Substring("Entry".Length).Trim('[', ']').Split('=');
                //            if (keyValuePairs.Length == 1)
                //            {
                //                var propertyName = keyValuePairs[0];
                //                var propertyValue = content.ReadAsStringAsync().Result;
                //                dynamic Entry1 = JsonConvert.DeserializeObject(propertyValue);

                //                foreach (var entryItem in Entry1)
                //                {

                //                    entryList.Add(new sli_document_process_modelBillEntry
                //                    {
                //                        Fbillid = bill.Fbillid,
                //                        Fobjectid = entryItem.Fobjectid,
                //                        Fmax = entryItem.Fmax,
                //                        Fmin = entryItem.Fmin,
                //                        Ftarget = entryItem.Ftarget,
                //                        Fnote = entryItem.Fnote,
                //                        Fnoties = entryItem.Fnoties,
                //                        Fexplanation = entryItem.Fexplanation
                //                    });
                //                }

                //            }
                //        }
                //        else if (contentDisposition.Name.Trim('\"').StartsWith("Files"))
                //        {
                //            if (contentDisposition.Name.Trim('\"').EndsWith("Files"))
                //            {
                //                var fileName = Path.GetFileName(content.Headers.ContentDisposition.FileName.Trim('\"'));
                //                var fileData = content.ReadAsByteArrayAsync().Result;
                //                attachmentList.Add(new sli_document_process_modelAttachment
                //                {
                //                    fattachment = fileName,
                //                    fileData = fileData,
                //                    fmainid = sale.Id
                //                });

                //            }

                //        }

                //    }

                //    // 添加子表

                //    dbContext.Sli_document_process_modelBillEntry.AddRange(entryList);
                //    dbContext.Sli_document_process_modelAttachment.AddRange(attachmentList);
                //    // dbContext.SaveChanges();

                //    dbContext.SaveChanges();



                //}



                //var data = new
                //{
                //    code = 200,
                //    msg = "ok",
                //    Id = sale.Id,
                //    date = sale.Id + "保存成功"

                //};
                //return Ok(data);
                #endregion
                if (!Request.Content.IsMimeMultipartContent())
                {
                    return BadRequest("Unsupported media type.");
                }

                MultipartMemoryStreamProvider provider = new MultipartMemoryStreamProvider();
                try
                {
                    await Request.Content.ReadAsMultipartAsync(provider);
                }
                catch (Exception ex)
                {
                    return BadRequest("Error reading multipart content: " + ex.Message);
                }

                var salePart = provider.Contents.FirstOrDefault(c => c.Headers.ContentDisposition.Name.Trim('"') == "Sale");
                var billPart = provider.Contents.FirstOrDefault(c => c.Headers.ContentDisposition.Name.Trim('"') == "Bill");
                var fileParts = provider.Contents.Where(c => c.Headers.ContentDisposition.Name.Trim('"') == "Files").ToList();

                // 处理 Sale 数据
                string saleJson;
                if (salePart != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        await salePart.CopyToAsync(ms);
                        ms.Seek(0, SeekOrigin.Begin);
                        saleJson = new StreamReader(ms).ReadToEnd();
                    }
                }
                else
                {
                    return BadRequest("Sale data is missing.");
                }
                sli_document_process_model saleModel = JsonConvert.DeserializeObject<sli_document_process_model>(saleJson);

                // 处理 Bill 数据
                string billJson;
                if (billPart != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        await billPart.CopyToAsync(ms);
                        ms.Seek(0, SeekOrigin.Begin);
                        billJson = new StreamReader(ms).ReadToEnd();
                    }
                }
                else
                {
                    return BadRequest("Bill data is missing.");
                }
                var billModels = JsonConvert.DeserializeObject<List<sli_document_process_modelBill>>(billJson);

                using (var context = new YourDbContext())
                {
                    // 添加主表数据
                    context.Sli_document_process_model.Add(saleModel);
                    context.SaveChanges();

                    // 关联并添加中间表和子表数据
                    foreach (var bill in billModels)
                    {
                        bill.sli_document_process_model = saleModel;
                        context.Sli_document_process_modelBill.Add(bill);
                    }
                    context.SaveChanges();

                    //foreach (var entry in billModels.SelectMany(b => b.sli_document_process_modelBillEntry))
                    //{
                    //    entry.sli_document_process_modelBill = billModels.FirstOrDefault(b => b.Fbillid == entry.Fbillid);
                    //    context.Sli_document_process_modelBillEntry.Add(entry);
                    //}
                    //context.SaveChanges();

                    // 处理文件数据

                    foreach (var filePart in fileParts)
                    {
                        string fileName = filePart.Headers.ContentDisposition.FileName.Trim('"');
                        using (MemoryStream fileMs = new MemoryStream())
                        {
                            await filePart.CopyToAsync(fileMs);
                            fileMs.Seek(0, SeekOrigin.Begin);
                            byte[] fileBytes = fileMs.ToArray();
                            var attachment = new sli_document_process_modelAttachment_view
                            {
                                fmainid = saleModel.Id,
                                fattachment = fileName,
                                fileData = fileBytes
                            };
                            context.Sli_document_process_modelAttachment_view.Add(attachment);
                            context.SaveChanges();
                        }
                    }
                }
                var data = new
                {
                    code = 200,
                    msg = "ok",
                    Id = saleModel.Id,
                    date = saleModel.Id + "保存成功"

                };

                return Ok(data);
            }
            catch (DbUpdateException ex)
            {
                // 捕获数据库更新异常，记录日志或返回错误信息
                // 这里可以记录详细的日志，例如 ex.InnerException.Message
                return BadRequest("Database update error: " + ex.Message);
            }
            catch (SqlException ex)
            {
                // 捕获SQL异常
                return BadRequest("SQL error: " + ex.Message);
            }
            catch (Exception ex)
            {
                // 捕获其他异常
                return BadRequest("General error: " + ex.Message);
            }
            
        }

        public async Task<IHttpActionResult> UpdateData(int id)
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return BadRequest("Expected multipart/form-data request.");
            }

            var sale = new sli_document_process_model();
            var bill = new sli_document_process_modelBill();
            List<sli_document_process_modelBillEntry> entryList = new List<sli_document_process_modelBillEntry>();
            List<sli_document_process_modelBill> billList = new List<sli_document_process_modelBill>();
            List<sli_document_process_modelAttachment> attachmentList = new List<sli_document_process_modelAttachment>();
            var entry = new sli_document_process_modelBillEntry();
            var attachment = new sli_document_process_modelAttachment();


            using (var dbContext = new YourDbContext())
            {
                var existingbill = dbContext.Sli_document_process_modelBill.Where(b => b.Id == id);
                dbContext.Sli_document_process_modelBill.RemoveRange(existingbill);
                var existingEntry = dbContext.Sli_document_process_modelBillEntry.Where(b => b.Fbillid == id);
                dbContext.Sli_document_process_modelBillEntry.RemoveRange(existingEntry);
                var existingachment = dbContext.Sli_document_process_modelAttachment.Where(b => b.fmainid == id);
                dbContext.Sli_document_process_modelAttachment.RemoveRange(existingachment);
                dbContext.SaveChanges();

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
                            var existing = dbContext.Sli_document_process_model.Find(id);
                            existing.Fnumber = sale1.Fnumber;
                            existing.Fname = sale1.Fname;
                            existing.Ftaxtrue = sale1.Ftaxtrue;
                            //existing.FcustomerID = sale1.FcustomerID;
                            existing.Fdate = sale1.Fdate;
                            existing.Fbillerid = sale1.Fbillerid;
                            dbContext.SaveChanges();

                            var temp = sale1.sli_document_process_modelAttachment_view;
                            if (temp.Count > 0)
                            {
                                foreach (var tempitem in temp)
                                {
                                    attachmentList.Add(new sli_document_process_modelAttachment
                                    {
                                        fattachment = tempitem.fattachment,
                                        fileData = tempitem.fileData,
                                        fmainid = id
                                    });
                                    dbContext.Sli_document_process_modelAttachment.AddRange(attachmentList);

                                }
                            }


                        }
                    }
                    else if (contentDisposition.Name.Trim('\"').StartsWith("Files"))
                    {
                        if (contentDisposition.Name.Trim('\"').EndsWith("Files"))
                        {
                            var fileName = Path.GetFileName(content.Headers.ContentDisposition.FileName.Trim('\"'));
                            var fileData = content.ReadAsByteArrayAsync().Result;
                            attachmentList.Add(new sli_document_process_modelAttachment
                            {
                                fattachment = fileName,
                                fileData = fileData,
                                fmainid = id
                            });
                            dbContext.Sli_document_process_modelAttachment.AddRange(attachmentList);
                            // dbContext.SaveChanges();

                            dbContext.SaveChanges();
                        }

                    }

                }

                var billPart = provider.Contents.FirstOrDefault(c => c.Headers.ContentDisposition.Name.Trim('"') == "Bill");
                //var fileParts = provider.Contents.Where(c => c.Headers.ContentDisposition.Name.Trim('"') == "Files").ToList();

                //var salePart = provider.Contents.FirstOrDefault(c => c.Headers.ContentDisposition.Name.Trim('"') == "Sale");


                // 处理 Bill 数据
                string billJson;
                if (billPart != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        await billPart.CopyToAsync(ms);
                        ms.Seek(0, SeekOrigin.Begin);
                        billJson = new StreamReader(ms).ReadToEnd();
                    }
                }
                else
                {
                    return BadRequest("Bill data is missing.");
                }
                var billModels = JsonConvert.DeserializeObject<List<sli_document_process_modelBill>>(billJson);

                // 添加主表数据

                // 关联并添加中间表和子表数据
                foreach (var bill1 in billModels)
                {
                    var header = new sli_document_process_modelBill
                    {
                        Id = id,
                        Foptionid = bill1.Foptionid,
                        Fdeptid = bill1.Fdeptid,
                        Fnote = bill1.Fnote,
                        Fstatus = bill1.Fstatus,
                        sli_document_process_modelBillEntry = bill1.sli_document_process_modelBillEntry.Select(d => new sli_document_process_modelBillEntry
                        {
                            Fbillid = d.Fbillid,
                            Fobjectid = d.Fobjectid,
                            Fmax = d.Fmax,
                            Fmin = d.Fmin,
                            Ftarget = d.Ftarget,
                            Fnote = d.Fnote,
                            Fnoties = d.Fnoties,
                            Fexplanation = d.Fexplanation
                        }).ToList()
                    };
                    dbContext.Sli_document_process_modelBill.Add(header);
                }
                dbContext.SaveChanges();

                //foreach (var entry in billModels.SelectMany(b => b.sli_document_process_modelBillEntry))
                //{
                //    entry.sli_document_process_modelBill = billModels.FirstOrDefault(b => b.Fbillid == entry.Fbillid);
                //    context.Sli_document_process_modelBillEntry.Add(entry);
                //}
                //context.SaveChanges();

                // 处理文件数据

                //foreach (var filePart in fileParts)
                //{
                //    string fileName = filePart.Headers.ContentDisposition.FileName.Trim('"');
                //    using (MemoryStream fileMs = new MemoryStream())
                //    {
                //        filePart.CopyToAsync(fileMs);
                //        fileMs.Seek(0, SeekOrigin.Begin);
                //        byte[] fileBytes = fileMs.ToArray();
                //        var attachment1 = new sli_document_process_modelAttachment_view
                //        {
                //            fmainid = id,
                //            fattachment = fileName,
                //            fileData = fileBytes
                //        };
                //        dbContext.Sli_document_process_modelAttachment_view.Add(attachment1);
                //        dbContext.SaveChanges();
                //    }
                //}
                
                var data = new
                {
                    code = 200,
                    msg = "ok",
                    Id = id,
                    date = id + "修改成功"

                };

                return Ok(data);



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
                    var entity = await context.Sli_document_process_model.FindAsync(deleteid);
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

                    var existingbill = context.Sli_document_process_modelBill.Where(b => b.Id == deleteid);
                    context.Sli_document_process_modelBill.RemoveRange(existingbill);
                    var existingEntry = context.Sli_document_process_modelBillEntry.Where(b => b.Fbillid == deleteid);
                    context.Sli_document_process_modelBillEntry.RemoveRange(existingEntry);
                    var existingachment = context.Sli_document_process_modelAttachment.Where(b => b.fmainid == deleteid);
                    context.Sli_document_process_modelAttachment.RemoveRange(existingachment);
                    context.Sli_document_process_model.RemoveRange(entity);
                    await context.SaveChangesAsync();

                }

                // var data = new { Status = "Success", Message = "Data retrieved successfully", Data = new { /* actual data here */ } };
                var data = new
                {
                    code = 200,
                    msg = "Success",
                    //orderId = id.ToString(),
                    date = "删除成功"
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
        public IHttpActionResult GetDocprocess(int page = 1, int pagesize = 40)
        {
            try
            {
                var context = new YourDbContext();
                //var query = _context.Sli_document_process_model.AsQueryable(); // sli_document_process_view
                IQueryable<sli_document_process_model> query = context.Sli_document_process_model;
                // 可以根据需要添加过滤条件，例如：
                // query = query.Where(d => d.Fdate >= startDate && d.Fdate <= endDate);

                int total = query.Count();
                var data = query
                            .OrderBy(d => d.Id) // 排序方式可根据需求调整
                            .Skip((page - 1) * pagesize)
                            .Take(pagesize)
                            .ToList();

                var response = new
                {
                    code = 200,
                    msg = "操作成功",
                    data = new
                    {
                        data = data,
                        page = page,
                        pagesize = pagesize,
                        total = total
                    }
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                // 记录异常日志（建议使用日志框架）
                return InternalServerError(new Exception("查询失败，请稍后再试。"+ex.ToString()+""));
            }
        }


        public IHttpActionResult GetDocprocessAll( int? id = null)
        {
            try
            {
                var context = new YourDbContext();




                var techSale = context.Sli_document_process_model_view.Find(id);
                //var Fbillid = techSale.sli_document_process_modelBill_view.Where();
               // var modelBill = context.Sli_document_process_modelBillEntry_view.Find(id);

                techSale.sli_document_process_modelBill_view = context.Sli_document_process_modelBill_view.Where(st1 => st1.Id == id).ToList() ?? new List<sli_document_process_modelBill_view>();
                if (techSale.sli_document_process_modelBill_view != null)
                {
                    foreach (var bill in techSale.sli_document_process_modelBill_view)
                    {
                        bill.sli_document_process_modelBillEntry_view = context.Sli_document_process_modelBillEntry_view.Where(st2 => st2.Fbillid == bill.Fbillid).ToList() ?? new List<sli_document_process_modelBillEntry_view>();
                    }
                }

                techSale.sli_document_process_modelAttachment_view = context.Sli_document_process_modelAttachment_view.Where(st3 => st3.fmainid == id).ToList() ?? new List<sli_document_process_modelAttachment_view>();



                var result = new List<object>();

                

                result.Add(new
                {
                    Sli_document_tech_sale = techSale,
                   
                });
                
                var response = new    // 定义 前端返回数据  总记录，总页，当前页 ，size,返回记录
                {
                    code = 200,
                    msg = "OK",
                    data = new
                    {

                        data = result
                    }
                };

                return Ok(response);




                
            }
            catch (Exception ex)
            {
                return Ok(ex.ToString());
            }
        }

        public IHttpActionResult GetTableBysli_document_process_modelAttachment(int? id = null)
        {
            try
            {
                var context = new YourDbContext();

                var attachment = context.Sli_document_process_modelAttachment.FirstOrDefault(a => a.id == id);
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
    }


    


}

