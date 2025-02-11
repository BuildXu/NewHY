using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi_SY.Entity;
using WebApi_SY.Models;

namespace WebApi_SY.Controllers
{
    public class sli_document_quality_standardController : ApiController
    {
        [HttpPost]
        public IHttpActionResult InsertData()
        {
            try
            {
                //var context = new YourDbContext();
                if (!Request.Content.IsMimeMultipartContent())
                {
                    return BadRequest("Expected multipart/form-data request.");
                }

                var standard = new sli_document_quality_standard();
                List<sli_document_quality_standardBillEntry> entryList = new List<sli_document_quality_standardBillEntry>();
                List<sli_document_quality_standardBill> billList = new List<sli_document_quality_standardBill>();
                List<sli_document_quality_standardAttachment> attachmentList = new List<sli_document_quality_standardAttachment>();
                //var entry = new sli_document_tech_saleBillEntry();
                //var attachment = new sli_document_tech_saleAttachment();
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

                                standard.Fnumber = sale1.Fnumber;
                                standard.Fname = sale1.Fname;
                                standard.Fdate = sale1.Fdate;
                                if (Convert.ToString( sale1.FmaterialID) =="")
                                {
                                    standard.FmaterialID = 0;
                                }
                                else
                                {
                                    standard.FmaterialID = sale1.FmaterialID;
                                }

                                standard.FcustomerID = sale1.FcustomerID;
                                
                                dbContext.Sli_document_quality_standard.Add(standard);
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
                                    var ftechOptionID1 = 0;

                                    if (Convert.ToString( billItem.fqualityOptionID) == "")
                                    {
                                        ftechOptionID1 = 0;
                                    }
                                    else
                                    {
                                        ftechOptionID1 = billItem.fqualityOptionID;
                                    }
                                    billList.Add(new sli_document_quality_standardBill
                                    {
                                        fmainID = standard.Id,
                                        fqualityOptionID = ftechOptionID1,
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

                                    if (Convert.ToString(entryItem.fqualityObjectID) == "")
                                    {
                                        ftechObjectID1 = 0;
                                    }
                                    else
                                    {
                                        ftechObjectID1 = entryItem.fqualityObjectID;
                                    }
                                    entryList.Add(new sli_document_quality_standardBillEntry
                                    {
                                        fbillID = standard.Id,
                                        fqualityObjectID = ftechObjectID1,
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
                                attachmentList.Add(new sli_document_quality_standardAttachment
                                {
                                    fattachment = fileName,
                                    fileData = fileData,
                                    fmainID = standard.Id
                                });

                            }

                        }
                        //return contentDisposition.Name;
                    }

                    // 添加子表
                    dbContext.Sli_document_quality_standardBill.AddRange(billList);
                    dbContext.Sli_document_quality_standardBillEntry.AddRange(entryList);
                    dbContext.Sli_document_quality_standardAttachment.AddRange(attachmentList);
                    // dbContext.SaveChanges();

                    dbContext.SaveChanges();



                }


                
                var data = new
                {
                    code = 200,
                    msg = "ok",
                    Id = standard.Id,
                    date = standard.Id + "保存成功"

                };
                return Ok(data);
            }
            catch (Exception ex)
            {
                return Ok(ex.ToString());
            }
        }


        [HttpPost]
        public IHttpActionResult UpdateData(int id)
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return BadRequest("Expected multipart/form-data request.");
            }

            var standard = new sli_document_quality_standard();
            List<sli_document_quality_standardBillEntry> entryList = new List<sli_document_quality_standardBillEntry>();
            List<sli_document_quality_standardBill> billList = new List<sli_document_quality_standardBill>();
            List<sli_document_quality_standardAttachment> attachmentList = new List<sli_document_quality_standardAttachment>();


            using (var dbContext = new YourDbContext())
            {
                var existingbill = dbContext.Sli_document_quality_standardBill.Where(b => b.fmainID == id);
                dbContext.Sli_document_quality_standardBill.RemoveRange(existingbill);
                var existingEntry = dbContext.Sli_document_quality_standardBillEntry.Where(b => b.fbillID == id);
                dbContext.Sli_document_quality_standardBillEntry.RemoveRange(existingEntry);
                var existingachment = dbContext.Sli_document_quality_standardAttachment.Where(b => b.fmainID == id);
                dbContext.Sli_document_quality_standardAttachment.RemoveRange(existingachment);
                dbContext.SaveChanges();

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

                            dynamic sale1 = JsonConvert.DeserializeObject(propertyValue);
                            var existing = dbContext.Sli_document_quality_standard.Find(id);
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
                            var temp = sale1.sli_document_quality_standardAttachment_view;
                            if (temp.Count > 0)
                            {
                                foreach (var tempitem in temp)
                                {
                                    attachmentList.Add(new sli_document_quality_standardAttachment
                                    {
                                        fattachment = tempitem.fattachment,
                                        fileData = tempitem.fileData,
                                        fmainID = id
                                    });
                                    dbContext.Sli_document_quality_standardAttachment.AddRange(attachmentList);

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
                                if (billItem.fqualityOptionID is null)
                                {
                                    ftechOptionID1 = 0;
                                }
                                else
                                {
                                    ftechOptionID1 = Convert.ToInt32(billItem.fqualityOptionID);
                                }

                                billList.Add(new sli_document_quality_standardBill
                                {
                                    fmainID = id,
                                    fqualityOptionID = ftechOptionID1,
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
                                if (entryItem.fqualityObjectID is null)
                                {
                                    ftechObjectID1 = 0;
                                }
                                else
                                {
                                    ftechObjectID1 = Convert.ToInt32(entryItem.fqualityObjectID);
                                }
                                entryList.Add(new sli_document_quality_standardBillEntry
                                {
                                    fbillID = id,
                                    fqualityObjectID = ftechObjectID1,
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
                            attachmentList.Add(new sli_document_quality_standardAttachment
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
                dbContext.Sli_document_quality_standardBill.AddRange(billList);
                dbContext.Sli_document_quality_standardBillEntry.AddRange(entryList);
                dbContext.Sli_document_quality_standardAttachment.AddRange(attachmentList);
                // dbContext.SaveChanges();

                dbContext.SaveChanges();



            }
            var data = new
            {
                code = 200,
                msg = "ok",
                Id = id,
                date = id + "修改成功"

            };
            return Ok(data);

        }


        [System.Web.Http.HttpPost]
        public async Task<object> Delete(List<int> id)
        {
            try
            {
                foreach (var deleteid in id)
                {
                    var context = new YourDbContext();
                    var entity = await context.Sli_document_quality_standard.FindAsync(deleteid);
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

                    var existingbill = context.Sli_document_quality_standardBill.Where(b => b.fmainID == deleteid);
                    context.Sli_document_quality_standardBill.RemoveRange(existingbill);
                    var existingEntry = context.Sli_document_quality_standardBillEntry.Where(b => b.fbillID == deleteid);
                    context.Sli_document_quality_standardBillEntry.RemoveRange(existingEntry);
                    var existingachment = context.Sli_document_quality_standardAttachment.Where(b => b.fmainID == deleteid);
                    context.Sli_document_quality_standardAttachment.RemoveRange(existingachment);
                    context.Sli_document_quality_standard.RemoveRange(entity);
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

        [HttpGet]
        public IHttpActionResult GetTableBydocument_tech_saleAttachment(int? id = null)
        {
            try
            {
                var context = new YourDbContext();

                var attachment = context.Sli_document_quality_standardAttachment.FirstOrDefault(a => a.id == id);
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
        public IHttpActionResult GetTableBydocument_quality_standard(int page = 1, int pageSize = 10, string fnumber = null, string fname = null)
        {
            try
           {
                var context = new YourDbContext();
                IQueryable<sli_document_quality_standard_view> query = context.Sli_document_quality_standard_view;

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

        [HttpGet]
        public IHttpActionResult GetTableBydocument_quality_standardall(int? id = null)
        {
            try
            {
                var context = new YourDbContext();

                var techSale = context.Sli_document_quality_standard_view.Find(id);

                techSale.sli_document_quality_standardBill_view = context.Sli_document_quality_standardBill_view.Where(st1 => st1.fmainID == id).ToList() ?? new List<sli_document_quality_standardBill_view>();
                techSale.sli_document_quality_standardBillEntry_view = context.Sli_document_quality_standardBillEntry_view.Where(st2 => st2.fbillID == id).ToList() ?? new List<sli_document_quality_standardBillEntry_view>();
                techSale.sli_document_quality_standardAttachment_view = context.Sli_document_quality_standardAttachment_view.Where(st3 => st3.fmainID == id).ToList() ?? new List<sli_document_quality_standardAttachment_view>();


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

    }
}