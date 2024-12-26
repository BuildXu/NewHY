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
    public class sli_workorderController : ApiController
    {
        public sli_workorderController()
        {
            // _context = context;

        }
        [System.Web.Http.HttpPost]
        public async Task<object> Insert([Microsoft.AspNetCore.Mvc.FromBody] sli_work_order model)
        {
            try
            {
                var context = new YourDbContext();
                var header = new sli_work_order
                {
                    Fbillno = model.Fbillno,
                    Fdate = model.Fdate,
                    Fqty = model.Fqty,
                    Fweight = model.Fweight,
                    Fplanstart = model.Fplanstart,
                    Fplanend = model.Fplanend,
                    Fordertype = model.Fordertype,
                    sli_work_orderEntry = model.sli_work_orderEntry.Select(d => new sli_work_orderEntry
                    {
                        //Id = model.Id,
                        Fseq = d.Fseq,
                        Forderentryid = d.Forderentryid,
                        Forderid = d.Forderid,
                        Fworkorderlistid = d.Id,

                    }).ToList()
                };



                context.Sli_work_order.Add(header);
                await context.SaveChangesAsync();
                var dataNull = new
                {
                    code=200,
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