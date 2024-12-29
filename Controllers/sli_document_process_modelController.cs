using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Transactions;
using System.Web.Http;
using WebApi_SY.Entity;
using WebApi_SY.Models;

namespace WebApi_SY.Controllers
{
    public class sli_document_process_modelController : ApiController
    {
        private readonly YourDbContext _context; // 声明上下文变量

        // 通过构造函数注入上下文
        public sli_document_process_modelController(YourDbContext context)
        {
            _context = context;
        }

        public IHttpActionResult InsertData()
        {
            using (var scope = new TransactionScope())  //保存时报错回滚保存操作
            {
                try
                {
                    //var context = new YourDbContext();
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
                        var provider = new MultipartMemoryStreamProvider();
                        var task = Request.Content.ReadAsMultipartAsync(provider);
                        task.Wait();
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

                                    dynamic sale1 = JsonConvert.DeserializeObject(propertyValue);

                                    sale.Fnumber = sale1.Fnumber;
                                    sale.Fname = sale1.Fname;
                                    sale.Fdate = Convert.ToDateTime(sale1.Fdate);
                                    sale.Ftaxtrue = sale1.Ftaxtrue;
                                    sale.Fbillerid = sale1.Fbillerid;
                                    sale.Fstatus = sale1.Fstatus;
                                    dbContext.Sli_document_process_model.Add(sale);
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
                                        
                                        billList.Add(new sli_document_process_modelBill
                                        {
                                            Id = sale.Id,
                                            Foptionid = billItem.Foptionid,
                                            Fdeptid = billItem.Fdeptid,
                                            Fnote = billItem.Fnote,
                                            Fstatus = billItem.Fstatus,
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
                                        
                                        entryList.Add(new sli_document_process_modelBillEntry
                                        {
                                            Fbillid = bill.Fbillid,
                                            Fobjectid = entryItem.Fobjectid,
                                            Fmax = entryItem.Fmax,
                                            Fmin = entryItem.Fmin,
                                            Ftarget = entryItem.Ftarget,
                                            Fnote = entryItem.Fnote,
                                            Fnoties = entryItem.Fnoties,
                                            Fexplanation = entryItem.Fexplanation
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
                                    attachmentList.Add(new sli_document_process_modelAttachment
                                    {
                                        fattachment = fileName,
                                        fileData = fileData,
                                        fmainid = sale.Id
                                    });

                                }

                            }

                        }

                        // 添加子表
                        dbContext.Sli_document_process_modelBill.AddRange(billList);
                        dbContext.Sli_document_process_modelBillEntry.AddRange(entryList);
                        dbContext.Sli_document_process_modelAttachment.AddRange(attachmentList);
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
        }

        //   查询----------
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public IHttpActionResult GetDocprocess(int page = 1, int pagesize = 40)
        {
            try
            {
                var query = _context.sli_work_processbill.AsQueryable(); // sli_document_process_view

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
                return InternalServerError(new Exception("查询失败，请稍后再试。"));
            }
        }


    }
}

//返回类似如下的 JSON 响应：


//{
//    "code": 200,
//    "msg": "操作成功",
//    "data": {
//    "data": [
//            {
//        "Id": 1,
//                "Fnumber": "档案001",
//                "Fdate": "2023-01-01T00:00:00",
//                "Ftaxtrue": true,
//                "Fprocessid": 101,
//                "Fprocessoption": 1,
//                "Fprocessname": "工序A",
//                "Fprocessnote": "说明A",
//                "Fdeptid": 201,
//                "Fbillid": 1001
//            },
//            // 更多数据...
//        ],
//        "page": 1,
//        "pagesize": 40,
//        "total": 100
//    }
//}