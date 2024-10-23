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
                    FworkBillNumber = model.FworkBillNumber,
                    Fdate = model.Fdate,
                    FbeginDatePlan = model.FbeginDatePlan,
                    FendDatePlan = model.FendDatePlan,
                    FqtyMain = model.FqtyMain,
                    FqtyFinishedMain = model.FqtyFinishedMain,
                    FqtyScrapedMain = model.FqtyScrapedMain,
                    FweightMain = model.FweightMain,
                    FweightFinishedMain = model.FweightFinishedMain,
                    FweightScrapedMain = model.FweightScrapedMain,
                    Fnotes = model.Fnotes,
                    FworkProcessId = model.FworkProcessId,
                    FworkRequisitionId = model.FworkRequisitionId,
                    FticketType = model.FticketType,
                    sli_workorderentry = model.sli_workorderentry.Select(d => new sli_workorderentry
                    {
                        //fmodelID = model.Id,
                        FworkListId = d.FworkListId,
                        ForderEntryid = d.ForderEntryid,
                        FrowNumber = d.FrowNumber,
                        ForderRowNumber = d.ForderRowNumber,
                        Fmaterialid = d.Fmaterialid,
                        Fqty = d.Fqty,
                        FqtyFinished = d.FqtyFinished,
                        FqtyScraped = d.FqtyScraped,
                        Fweight = d.Fweight,
                        FweightFinished = d.FweightFinished,
                        FweightScraped = d.FweightScraped,
                        Sli_workRequisitionId = d.Sli_workRequisitionId,
                        Sli_workProcessId = d.Sli_workProcessId
                    }).ToList()
                };



                context.Sli_workorder.Add(header);
                await context.SaveChangesAsync();
                var dataNull = new
                {
                    msg = "Success",
                    modelid = header.ID,
                    Date = header.ID.ToString() + "保存成功"

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