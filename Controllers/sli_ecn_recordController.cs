using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using WebApi_SY.Entity;
using WebApi_SY.Models;

public class sli_ecn_recordController : ApiController
{
    [System.Web.Http.HttpPost]
    public async Task<object> sli_ecn_record_Insert(
     [System.Web.Http.FromBody] sli_ecn_record[] options)
    {
        var context = new YourDbContext();
        try
        {
            if (options == null || options.Length == 0)
            {
                return Content(HttpStatusCode.BadRequest, new
                {
                    code = 400,
                    msg = "请求参数不能为空",
                    data = ""
                });
            }

            foreach (var option in options)
            {
                if (option == null)
                {
                    return Content(HttpStatusCode.BadRequest, new
                    {
                        code = 400,
                        msg = "参数中存在空对象",
                        data = ""
                    });
                }

                var record = new sli_ecn_record
                {
                    // 确保不设置 Id    //   Id
                    FBillNo = option.FBillNo,    // 变更单号
                    FOrderEntryId = option.FOrderEntryId,   //  订单行Id  ---不显示
                    FOrderNo = option.FOrderNo,  // 订单号
                    FSeq = option.FSeq,         // 行号
                    FMaterialId = option.FMaterialId,    // 产品id  ---不显示
                    FNumber = option.FNumber, // 产品代码
                    FNameA = option.FNameA,     // 原名称
                    FDescriptionA = option.FDescriptionA, // 原规格
                    FNameB = option.FNameB,   //变更名称
                    FDescriptionB = option.FDescriptionB,  // 变更规格
                     FNote = option.FNote,  //备注
                    Fdocid = option.Fdocid,  //  商务技术要求id  ---不显示 
                    Fdocno = option.Fdocno,  // 商务技术要求
                    Fslitechno = option.Fslitechno  // 技术转化
                };
                context.sli_ecn_record.Add(record);
            }

            await context.SaveChangesAsync();

            // 获取实际插入的 Id 列表
            var savedIds = options.Select(option => option.Id).ToList();

            var response = new
            {
                code = 200,
                msg = "Success",
                data = savedIds.Count > 0 ? $"{string.Join(",", savedIds)} 保存成功" : "无记录保存成功"
            };
            return Ok(response);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.ToString());
            return Content(HttpStatusCode.InternalServerError, new
            {
                code = 500,
                msg = "失败",
                data = ex.Message
            });
        }
    }

    [System.Web.Http.HttpPost]
    public async Task<object> sli_ecn_record_Update(
  [System.Web.Http.FromBody] sli_ecn_record option)  // 明确指定 FromBody 来源
    {
        var context = new YourDbContext();
        try
        {
            var existingRecord = await context.sli_ecn_record.FindAsync(option.Id);
            if (existingRecord == null)
            {
                return Ok(new
                {
                    code = 404,
                    msg = "记录不存在",
                    data = ""
                });
            }

            // 更新所有字段
            existingRecord.FBillNo = option.FBillNo;
            existingRecord.FOrderEntryId = option.FOrderEntryId;
            existingRecord.FOrderNo = option.FOrderNo;
            existingRecord.FSeq = option.FSeq;
            existingRecord.FMaterialId = option.FMaterialId;
            existingRecord.FNumber = option.FNumber;
            existingRecord.FNameA = option.FNameA;
            existingRecord.FDescriptionA = option.FDescriptionA;
            existingRecord.FNameB = option.FNameB;
            existingRecord.FDescriptionB = option.FDescriptionB;
            existingRecord.FNote = option.FNote;
            existingRecord.Fdocid = option.Fdocid;
            existingRecord.Fdocno = option.Fdocno;
            existingRecord.Fslitechno = option.Fslitechno;

            await context.SaveChangesAsync();

            return Ok(new
            {
                code = 200,
                msg = "修改成功",
                data = existingRecord.Id
            });
        }
        catch (Exception ex)
        {
            return Content(HttpStatusCode.InternalServerError, new
            {
                code = 500,
                msg = "失败",
                data = ex.Message
            });
        }
    }
    [System.Web.Http.HttpPost]
        public async Task<object> sli_ecn_record_Delete(List<int> ids)
        {
            var context = new YourDbContext();
            try
            {
                foreach (var id in ids)
                {
                    var record = await context.sli_ecn_record.FindAsync(id);
                    if (record != null)
                    {
                        context.sli_ecn_record.Remove(record);
                    }
                }
                await context.SaveChangesAsync();

                return Ok(new
                {
                    code = 200,
                    msg = "删除成功",
                    data = string.Join(",", ids)
                });
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new
                {
                    code = 500,
                    msg = "失败",
                    data = ex.Message
                });
            }
        }

        [System.Web.Http.HttpGet]
        public IHttpActionResult GetSliEcnRecords(
            int page = 1,
            int pageSize = 10,
            string FBillNo = null,
            string FOrderNo = null,
            string FNumber = null)
        {
            var context = new YourDbContext();
            try
            {
                var query = context.sli_ecn_record.AsQueryable();

                // 动态过滤条件
                if (!string.IsNullOrEmpty(FBillNo))
                    query = query.Where(q => q.FBillNo.Contains(FBillNo));
                if (!string.IsNullOrEmpty(FOrderNo))
                    query = query.Where(q => q.FOrderNo.Contains(FOrderNo));
                if (!string.IsNullOrEmpty(FNumber))
                    query = query.Where(q => q.FNumber.Contains(FNumber));

                // 分页
                var totalCount = query.Count();
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
                var records = query.OrderByDescending(r => r.Id)
                                   .Skip((page - 1) * pageSize)
                                   .Take(pageSize)
                                   .ToList();

                var response = new
                {
                    code = 200,
                    msg = "OK",
                    data = new
                    {
                        totalCount,
                        totalPages,
                        currentPage = page,
                        pageSize,
                        records
                    }
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new
                {
                    code = 500,
                    msg = "查询失败",
                    data = ex.Message
                });
            }
        }
    }
