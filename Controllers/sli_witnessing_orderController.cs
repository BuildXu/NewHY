using Newtonsoft.Json;
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
    public class sli_witnessing_orderController : ApiController
    {
        public sli_witnessing_orderController()
        {
            // _context = context;
        }

        [System.Web.Http.HttpPost]
        public async Task<object> sli_witnessing_order_Insert([Microsoft.AspNetCore.Mvc.FromBody] sli_witnessing_order options)
        {
            var context = new YourDbContext();
            try
            {
                var header = new sli_witnessing_order
                {
                    Fnumber = options.Fnumber,
                    Fdate = options.Fdate,
                    Fnote = options.Fnote,
                    Fstatus = options.Fstatus,
                    sli_witnessing_orderbill = new List<sli_witnessing_orderbill>()
                };

                if (options.sli_witnessing_orderbill != null)
                {
                    //var i = 1;
                    foreach (var entry in options.sli_witnessing_orderbill)
                    {
                        header.sli_witnessing_orderbill.Add(new sli_witnessing_orderbill
                        {
                            Id = header.Id,
                            Fstartdate = entry.Fstartdate,
                            Fenddate = entry.Fenddate,
                            Fobject = entry.Fobject,
                            Fnote = entry.Fnote,
                            Fworkorderlistid = entry.Fworkorderlistid,
                            Fstatus = entry.Fstatus
                        });
                        //i++;
                    }
                }
                context.Sli_witnessing_order.Add(header);
                await context.SaveChangesAsync();
                var dataNull = new
                {
                    code = 200,
                    msg = "Success",
                    modelid = header.Id,
                    Date = header.Id.ToString() + "保存成功"

                };
                return dataNull;
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex.ToString());
            }
        }


    }
}