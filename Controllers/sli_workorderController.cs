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
        public async Task<object> Insert([Microsoft.AspNetCore.Mvc.FromBody] sli_workorder model)
        {
            try
            {
                var context = new YourDbContext();
                var header = new sli_workorder
                {
                    Fworkbillnumber = model.Fworkbillnumber,
                    Fdate = model.Fdate,
                    Fbegindateplan = model.Fbegindateplan,
                    Fenddateplan = model.Fenddateplan,
                    Fqtymain = model.Fqtymain,
                    Fqtyfinishedmain = model.Fqtyfinishedmain,
                    Fqtyscrapedmain = model.Fqtyscrapedmain,
                    Fweightmain = model.Fweightmain,
                    Fweightfinishedmain = model.Fweightfinishedmain,
                    Fweightscrapedmain = model.Fweightscrapedmain,
                    Fnotes = model.Fnotes,
                    Fworkprocessid = model.Fworkprocessid,
                    Fworkrequisitionid = model.Fworkrequisitionid,
                    Ftickettype = model.Ftickettype,
                    sli_workorderentry = model.sli_workorderentry.Select(d => new sli_workorderentry
                    {
                        //fmodelID = model.Id,
                        Fworklistid = d.Fworklistid,
                        Forderentryid = d.Forderentryid,
                        Frownumber = d.Frownumber,
                        Forderrownumber = d.Forderrownumber,
                        Fmaterialid = d.Fmaterialid,
                        Fqty = d.Fqty,
                        Fqtyfinished = d.Fqtyfinished,
                        Fqtyscraped = d.Fqtyscraped,
                        Fweight = d.Fweight,
                        Fweightfinished = d.Fweightfinished,
                        Fweightscraped = d.Fweightscraped,
                        sli_workrequisitionid = d.sli_workrequisitionid,
                        sli_workprocessid = d.sli_workprocessid
                    }).ToList()
                };



                context.Sli_workorder.Add(header);
                await context.SaveChangesAsync();
                var dataNull = new
                {
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