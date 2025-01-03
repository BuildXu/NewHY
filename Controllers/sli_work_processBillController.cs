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
    public class sli_work_processBillController : ApiController
    {
        public sli_work_processBillController()
        {
            // _context = context;

        }
        [System.Web.Http.HttpPost]
        public async Task<object> Insert([Microsoft.AspNetCore.Mvc.FromBody] sli_work_processBill model)
        {
            try
            {
                var context = new YourDbContext();
                var header = new sli_work_processBill
                {
                    Fwoentryid = model.Fwoentryid,
                    Fseq = model.Fseq,
                    Fqty = model.Fqty,
                    Fweight = model.Fweight,
                    Fworkorderlistid = model.Fworkorderlistid,
                    Fprocessoption = model.Fprocessoption,
                    Fstartdate = model.Fstartdate,
                    Fenddate = model.Fenddate,
                    Fcommitqty = model.Fcommitqty,
                    Fcommitweight = model.Fcommitweight,
                    Fstatus = model.Fstatus,
                    sli_work_processBillEntry = model.sli_work_processBillEntry.Select(d => new sli_work_processBillEntry
                    {
                        Fbillid = model.Id,
                        Fseq = d.Fseq,
                        Forderentryid = d.Forderentryid,
                        Forderid = d.Forderid,
                        Fworkorderlistid = d.Fworkorderlistid,

                    }).ToList()
                };



                context.Sli_work_processBill.Add(header);
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