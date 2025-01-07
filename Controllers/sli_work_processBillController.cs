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
        public IHttpActionResult Insert()
        {
            try
            {
                var context = new YourDbContext();
                var json = Request.Content.ReadAsStringAsync().Result;
                var root = JsonConvert.DeserializeAnonymousType(json, new
                {
                    Fworkordlistid = new int[] { },
                    sli_workorderlist_view = new List<SliWorkorderlistView>()
                });

                // 处理 Fworkordlistid 插入逻辑
                if (root.Fworkordlistid != null)
                {
                    foreach (var id in root.Fworkordlistid)
                    {
                        if (root.sli_workorderlist_view != null)
                        {
                            foreach (var item in root.sli_workorderlist_view)
                            {
                                // 先插入外层对象
                                var outerEntity = new sli_work_processBill
                                {
                                    Fworkorderlistid =id,
                                    Fprocessoption = item.Foptionid,
                                    Fstatus = item.Fstatus,
                                    //Fstartdate=DateTime.Today,
                                    //Fs = DateTime.Today,
                                };
                                var outerSaved = context.Sli_work_processBill.Add(outerEntity);
                                context.SaveChanges();

                                // 再插入内层对象
                                if (item.sli_document_process_modelBillEntry_view != null)
                                {
                                    foreach (var innerItem in item.sli_document_process_modelBillEntry_view)
                                    {
                                        var innerEntity = new sli_work_processBillEntry
                                        {
                                            Fbillid= outerEntity.Id,
                                            Fprocessobject = innerItem.Fobjectid
                                        };
                                        
                                        context.Sli_work_processBillEntry.Add(innerEntity);
                                    }
                                }
                                context.SaveChanges();
                            }
                        }
                    }
                }

                // 处理 sli_workorderlist_view 插入逻辑
                



                //var entry = new sli_work_processBillEntry
                //{
                //    Fbillid = header.Id,
                //};
                //context.Sli_work_processBillEntry.Add(entry);
                //await context.SaveChangesAsync();

                var dataNull = new
                {
                    code = 200,
                    msg = "Success",
                    //modelid = model.Fworkorderlistid,
                    //Date = header.Id.ToString() + "保存成功"

                };
                return Ok(dataNull);
            }
            catch (Exception ex)
            {
                return Ok(ex);
                //return JsonConvert.SerializeObject(ex.ToString());
            }


        }

    }
}

