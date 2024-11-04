using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi_SY.Entity;
using WebApi_SY.Models;

namespace WebApi_SY.Controllers
{
    public class sli_testController : ApiController
    {
        [System.Web.Http.HttpPost]
        public async Task<object> Tech_option_Insert([Microsoft.AspNetCore.Mvc.FromBody] sli_test option)
        {
            var context = new YourDbContext();
            //string json = "{\"header\":{\"name\":\"Header Name\",\"details\":[{\"detailContent\":\"Detail 1\"},{\"detailContent\":\"Detail 2\"}]}}";

            //var root = JsonConvert.DeserializeObject<RootObject>(json);
            try
            {
                //var context = new YourDbContext();
                //_context = context;
                var header = new sli_test
                {
                    FBillNo = option.FBillNo,
                    FCompay = option.FCompay,
                    FStatus = option.FStatus
                   
                };
                context.Sli_test.Add(header);
                await context.SaveChangesAsync();
                var datas = new
                {
                    code = 200,
                    msg = "Success",
                    //modelid = header.id,
                    Date = header.FID.ToString() + "保存成功"

                };
                return datas;
                //return Json(new { Result = "Success", orderId = order.Id }, JsonRequestBehavior.);
            }
            catch (Exception ex)
            {
                var dataerr = new
                {
                    code = 500,
                    msg = "失败",
                    //modelid = header.id,
                    Date = ex.ToString()

                };
                return dataerr;
                //return JsonConvert.SerializeObject(ex.ToString());
                //Console.WriteLine();
            }
        }
    }
}