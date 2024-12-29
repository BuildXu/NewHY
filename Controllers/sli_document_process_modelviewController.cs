using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebApi_SY.Entity;
using WebApi_SY.Models;

namespace WebApi_SY.Controllers
{
    public class sli_document_process_modelviewController : ApiController
    {
        private readonly YourDbContext _context;

        // 添加一个默认的无参构造函数
        public sli_document_process_modelviewController()
        {
            // 在此处进行必要的初始化操作，如果有需要的话
        }

        // 或者添加一个带有依赖注入的构造函数，确保依赖注入容器能正确提供所需的依赖
        public sli_document_process_modelviewController(YourDbContext context)
        {
            _context = context;
        }


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