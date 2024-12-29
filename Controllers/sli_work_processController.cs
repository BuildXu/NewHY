using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi_SY.Entity;
using WebApi_SY.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApi_SY.Controllers
{
    public class sli_work_processbillController : ApiController
    {

        private readonly YourDbContext _context; // 声明上下文变量

        // 通过构造函数注入上下文
        public sli_work_processbillController(YourDbContext context)
        {
            _context = context;
        }


        [System.Web.Http.HttpPost]
        public async Task<object> Insert([Microsoft.AspNetCore.Mvc.FromBody] IEnumerable<sli_work_processbill> bills)
        {

            if (bills == null || !bills.Any())
            {
                return BadRequest("没有提供任何工作流程账单数据。");
            }

            foreach (var bill in bills)
            {
                // 设置外键关系（如果需要）
                foreach (var entry in bill.sli_work_processbillentry)
                {
                    entry.Fbillid = bill.Id; // 假设 Fbillid 是外键
                }

                _context.sli_work_processbill.Add(bill);
            }

            try
            {
                await _context.SaveChangesAsync();
                return Ok("工序流程单及其条目已成功添加。");
            }
            catch (Exception ex)
            {
                // 日志记录异常（建议使用日志框架）
                return (500, new { message = $"服务器错误: {ex.Message}" });
            }

        }

        //   查询----------
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public IHttpActionResult GetDocuments(int page = 1, int pagesize = 40)
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


//请求示例：
//    [
//    {
//        "Id": 1,      // 自增，不用传
//        "Fwoentryid": 101,   //   从 sli_work_orderEntry  /  Fentryid  **********
//        "Fseq": 1,              //  前端  --》 行号                 *********
//        "Fworkorderlistid": 201,    //   sli_work_orderEntry  /  Fworkorderlistid       
//        "Fprocessoption": 1,         //  前端  sli_document_process_model  /  Foption    ****************
//        "Fstartdate": "2023-10-01T00:00:00",    //  前端  
//        "Fenddate": "2023-10-05T00:00:00",     //   前端 ------------  这个工序计划时间，  要考  从  sli_plan_bill  获取  （根据 ForderEntryid）
//        "Fqty": 100.0m,     //  sli_work_orderEntry  /  Fqty
//        "Fweight": 500.0m,
//        "Fcommitqty": 90.0m,
//        "Fcommitweight": 450.0m,
//        "Fstatus": 1,
//        "sli_work_processbillentry": [
//            {
//                "Id": 1001,
//                "Fbillid": "BILL001",
//                "Fentryid": 1,
//                "Fseq": 1,
//                "Fwobillid": 0, // 这里可以留空或忽略，因为会在服务器端设置
//                "Fprocessobject": 10,
//                "Fstartdate": "2023-10-01T00:00:00",
//                "Fenddate": "2023-10-05T00:00:00",
//                "Fqty": 50.0m,
//                "Fweight": 250.0m,
//                "Fcommitqty": 45.0m,
//                "Fcommitweight": 225.0m,
//                "Fstatus": 1
//            }
//            // 可以添加更多条目
//        ]
//    }
//    // 可以添加更多账单
//]

