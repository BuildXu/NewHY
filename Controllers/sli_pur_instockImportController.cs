using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Http;
using WebApi_SY.Entity;
using WebApi_SY.Models;
using NPOI.XSSF.UserModel;
using System.Drawing.Printing;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Kingdee.BOS.WebApi.Client;
using System.Text;
using System.Net.Http;
using System.Net;
using System.Collections;
using Kingdee.BOS.WebApi.DataEntities;
using Newtonsoft.Json;
using NPOI.HSSF.Record;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using System.Data.Entity.Core.Metadata.Edm;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace WebApi_SY.Controllers
{
    public class sli_pur_instockImportImportController : ApiController
    {


        /// <summary>
        /// 调用金蝶销售订单保存接口，写入excel导入数据；根据导入的生成的表头ID读取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IHttpActionResult DateSync(int id)
        {
            try
            {
                var context = new YourDbContext();
                //Assert.IsTrue((bool)isSuccess, resultJson);
                ApiClient client = new ApiClient("http://19vs7gv47690.vicp.fun/K3cloud/"); //接口地址
                string dbId = "6708e954644c2a"; //账套ID
                bool bLogin = client.Login(dbId, "Administrator", "kingdee123*", 2052);
                if (bLogin)
                {
                    
                    var heard = context.Sli_sale_orderImport.FirstOrDefault(p => p.FID == id);   //获取表头单行数据
                    var FcustomerNumer = context.Sli_bd_customer_view.FirstOrDefault(p => p.FNAME == heard.FCustomerName); //根据客户名称查询客户代码
                    var entity = context.Sli_sale_orderImportentry.Where(p => p.fid == id);   //获取表体多行数据
                    var entityList = entity.ToList();
                    //var index = 0;
                    string json = "{\"NeedUpDateFields\":[],\"NeedReturnFields\":[\"FBillNo\"],\"IsDeleteEntry\":\"true\",\"SubSystemId\":\"\",\"IsVerifyBaseDataField\":\"false\",\"IsEntryBatchFill\":\"true\",\"ValidateFlag\":\"true\",\"NumberSearch\":\"true\",\"IsAutoAdjustField\":\"true\",\"InterationFlags\":\"\",\"IsControlPrecision\":\"false\",\"ValidateRepeatJson\":\"false\",\"Model\":{\"FID\":0,\"FBillTypeID\":{\"FNUMBER\":\"XSDD01_SYS\"},\"FDate\":\"2024-11-12 11:38:54\",\"FSaleOrgId\":{\"FNumber\":\"100\"},\"FCustId\":{\"FNumber\":\"CUST0001\"},\"FReceiveId\":{\"FNumber\":\"CUST0001\"},\"FSaleDeptId\":{\"FNumber\":\"BM000001\"},\"FSalerId\":{\"FNumber\":\"001_001_1\"},\"FSettleId\":{\"FNumber\":\"CUST0001\"},\"FChargeId\":{\"FNumber\":\"CUST0001\"},\"FNetOrderBillId\":0,\"FOppID\":0,\"FISINIT\":false,\"FIsMobile\":false,\"FContractId\":0,\"FIsUseOEMBomPush\":false,\"FXPKID_H\":0,\"FIsUseDrpSalePOPush\":false,\"FIsCreateStraightOutIN\":false,\"FSaleOrderFinance\":{\"FSettleCurrId\":{\"FNumber\":\"PRE001\"},\"FIsIncludedTax\":true,\"FIsPriceExcludeTax\":true,\"FExchangeTypeId\":{\"FNumber\":\"HLTX01_SYS\"},\"FMarginLevel\":0.0,\"FMargin\":0.0,\"FOverOrgTransDirect\":false,\"FAllDisCount\":0.0,\"FXPKID_F\":0},\"FSalOrderRec\":{},\"FSaleOrderEntry\":[{\"FRowType\":\"Standard\",\"FMaterialId\":{\"FNumber\":\"T000001\"},\"FUnitID\":{\"FNumber\":\"Pcs\"},\"FInventoryQty\":0.0,\"FCurrentInventory\":0.0,\"FAwaitQty\":0.0,\"FAvailableQty\":0.0,\"FQty\":1,\"FPriceUnitId\":{\"FNumber\":\"Pcs\"},\"FOldQty\":0.0,\"FPrice\":0.0,\"FTaxPrice\":0.0,\"FIsFree\":false,\"FEntryTaxRate\":13.00,\"FDeliveryDate\":\"2024-11-30 00:00:00\",\"FStockOrgId\":{\"FNumber\":\"100\"},\"FSettleOrgIds\":{\"FNumber\":\"100\"},\"FSupplyOrgId\":{\"FNumber\":\"100\"},\"FOwnerTypeId\":\"BD_OwnerOrg\",\"FOwnerId\":{\"FNumber\":\"100\"},\"FSrcType\":\"\",\"FReserveType\":\"1\",\"FPriceBaseQty\":1,\"FStockUnitID\":{\"FNumber\":\"Pcs\"},\"FStockQty\":1,\"FStockBaseQty\":1,\"FOUTLMTUNIT\":\"SAL\",\"FOutLmtUnitID\":{\"FNumber\":\"Pcs\"},\"FISMRP\":false,\"FISMRPCAL\":false,\"FAllAmountExceptDisCount\":0.0,\"FsliHeatTreatment\":\"毛坯热处理\",\"FsliTestBarQty\":\"1\",\"FsliMetel\":{\"FNUMBER\":\"35\"},\"FsliExplanation\":\"测试\",\"FsliNotice\":\"测试\",\"FsliWorkOrder\":\"生产号\",\"FsliSaleOrder\":\"S241015-230745\",\"FsliQuotationNo\":\"未报价\",\"FsliStockNo\":\"科技1A毛坯库\",\"FsliBlank\":\"毛坯图号\",\"FsliDrawingNo\":\"图纸号\"},{\"FRowType\":\"Standard\",\"FMaterialId\":{\"FNumber\":\"T000001\"},\"FUnitID\":{\"FNumber\":\"Pcs\"},\"FInventoryQty\":0.0,\"FCurrentInventory\":0.0,\"FAwaitQty\":0.0,\"FAvailableQty\":0.0,\"FQty\":1,\"FPriceUnitId\":{\"FNumber\":\"Pcs\"},\"FOldQty\":0.0,\"FPrice\":0.0,\"FTaxPrice\":0.0,\"FIsFree\":false,\"FEntryTaxRate\":13.00,\"FDeliveryDate\":\"2024-11-21 00:00:00\",\"FStockOrgId\":{\"FNumber\":\"100\"},\"FSettleOrgIds\":{\"FNumber\":\"100\"},\"FSupplyOrgId\":{\"FNumber\":\"100\"},\"FOwnerTypeId\":\"BD_OwnerOrg\",\"FOwnerId\":{\"FNumber\":\"100\"},\"FSrcType\":\"\",\"FReserveType\":\"1\",\"FPriceBaseQty\":1,\"FStockUnitID\":{\"FNumber\":\"Pcs\"},\"FStockQty\":1,\"FStockBaseQty\":1,\"FOUTLMTUNIT\":\"SAL\",\"FOutLmtUnitID\":{\"FNumber\":\"Pcs\"},\"FISMRP\":false,\"FISMRPCAL\":false,\"FAllAmountExceptDisCount\":0.0,\"FsliHeatTreatment\":\"\",\"FsliTestBarQty\":\"0\",\"FsliMetel\":{\"FNUMBER\":\"35\"},\"FsliExplanation\":\"\",\"FsliNotice\":\"\",\"FsliWorkOrder\":\"\",\"FsliSaleOrder\":\"S241015-230701\",\"FsliQuotationNo\":\"未报价\",\"FsliStockNo\":\"科技1A毛坯库\",\"FsliBlank\":\"\",\"FsliDrawingNo\":\"\"},{\"FRowType\":\"Standard\",\"FMaterialId\":{\"FNumber\":\"T000001\"},\"FUnitID\":{\"FNumber\":\"Pcs\"},\"FInventoryQty\":0.0,\"FCurrentInventory\":0.0,\"FAwaitQty\":0.0,\"FAvailableQty\":0.0,\"FQty\":1,\"FPriceUnitId\":{\"FNumber\":\"Pcs\"},\"FOldQty\":0.0,\"FPrice\":0.0,\"FTaxPrice\":0.0,\"FIsFree\":false,\"FEntryTaxRate\":13.00,\"FDeliveryDate\":\"2024-11-21 00:00:00\",\"FStockOrgId\":{\"FNumber\":\"100\"},\"FSettleOrgIds\":{\"FNumber\":\"100\"},\"FSupplyOrgId\":{\"FNumber\":\"100\"},\"FOwnerTypeId\":\"BD_OwnerOrg\",\"FOwnerId\":{\"FNumber\":\"100\"},\"FSrcType\":\"\",\"FReserveType\":\"1\",\"FPriceBaseQty\":1,\"FStockUnitID\":{\"FNumber\":\"Pcs\"},\"FStockQty\":1,\"FStockBaseQty\":1,\"FOUTLMTUNIT\":\"SAL\",\"FOutLmtUnitID\":{\"FNumber\":\"Pcs\"},\"FISMRP\":false,\"FISMRPCAL\":false,\"FAllAmountExceptDisCount\":0.0,\"FsliHeatTreatment\":\"\",\"FsliTestBarQty\":\"0\",\"FsliMetel\":{\"FNUMBER\":\"35\"},\"FsliExplanation\":\"\",\"FsliNotice\":\"\",\"FsliWorkOrder\":\"\",\"FsliSaleOrder\":\"S241015-230702\",\"FsliQuotationNo\":\"未报价\",\"FsliStockNo\":\"科技1A毛坯库\",\"FsliBlank\":\"\",\"FsliDrawingNo\":\"\"}]}}";
                    sli_sal_orderimport rootObject = JsonConvert.DeserializeObject<sli_sal_orderimport>(json);
                    rootObject.Model.FSaleOrderEntry.Clear();
                    rootObject.Model.FDate = DateTime.Now;
                    rootObject.Model.FCustId.FNumber = FcustomerNumer.FNUMBER;
                    rootObject.Model.FReceiveId.FNumber = FcustomerNumer.FNUMBER;
                    rootObject.Model.FSettleId.FNumber = FcustomerNumer.FNUMBER;
                    rootObject.Model.FChargeId.FNumber = FcustomerNumer.FNUMBER;

                    foreach (var entitydata in entityList)
                    {
                        SaleOrderEntry newEntry = new SaleOrderEntry();

                        
                        newEntry.FRowType = "Standard";
                        newEntry.FMaterialId = new OrgId { FNumber = entitydata.fmaterialNumber };
                        newEntry.FUnitID = new OrgId { FNumber = "Pcs" };
                        newEntry.FInventoryQty = 0.0;
                        newEntry.FCurrentInventory = 0.0;
                        newEntry.FAwaitQty = 0.0;
                        newEntry.FAvailableQty = 0.0;
                        newEntry.FQty = entitydata.fqty;
                        newEntry.FPriceUnitId = new OrgId { FNumber = "Pcs" };
                        newEntry.FOldQty = 0.0;
                        newEntry.FPrice = 0.0;
                        newEntry.FTaxPrice = 0.0;
                        newEntry.FIsFree = false;
                        newEntry.FEntryTaxRate = 13.00;
                        newEntry.FDeliveryDate = entitydata.fdeliveryDate;
                        newEntry.FStockOrgId = new OrgId { FNumber = "100" };
                        newEntry.FSettleOrgIds = new OrgId { FNumber = "100" };
                        newEntry.FSupplyOrgId = new OrgId { FNumber = "100" };
                        newEntry.FOwnerTypeId = "BD_OwnerOrg";
                        newEntry.FOwnerId = new OrgId { FNumber = "100" };
                        newEntry.FSrcType = "";
                        newEntry.FReserveType = "1";
                        newEntry.FPriceBaseQty = entitydata.fqty;
                        newEntry.FStockUnitID = new OrgId { FNumber = "Pcs" };
                        newEntry.FStockQty = entitydata.fqty;
                        newEntry.FStockBaseQty = entitydata.fqty;
                        newEntry.FOUTLMTUNIT = "SAL";
                        newEntry.FOutLmtUnitID = new OrgId { FNumber = "Pcs" };
                        newEntry.FISMRP = false;
                        newEntry.FISMRPCAL = false;
                        newEntry.FAllAmountExceptDisCount = 0.0;
                        newEntry.FsliHeatTreatment = entitydata.fsliHeatTreatment;
                        newEntry.FsliTestBarQty = entitydata.fsliTestBarQty;
                        newEntry.FsliMetel = new OrgId { FNumber = entitydata.fsliMetal };
                        newEntry.FsliExplanation = entitydata.fsliExplanation;
                        newEntry.FsliNotice = entitydata.fsliNotice;
                        newEntry.FsliWorkOrder = entitydata.fsliWorkOrder;
                        newEntry.FsliSaleOrder = entitydata.fsliSaleOrder;
                        newEntry.FsliQuotationNo = entitydata.fsliQuotationNo;
                        newEntry.FsliStockNo = entitydata.fsliStockNo;
                        newEntry.FsliBlank = entitydata.fsliBlank;
                        newEntry.FsliDrawingNo = entitydata.fsliDrawingNo;

                        // 将新创建的实例添加到FSaleOrderEntry列表中
                        rootObject.Model.FSaleOrderEntry.Add(newEntry);
                    }
                    string newJson = JsonConvert.SerializeObject(rootObject);
                    System.Diagnostics.Debug.WriteLine(newJson);
                    //sJson = "{\"CreateOrgId\":0,\"Numbers\":[\"" + FPrdModel + "\"]}";
                    //string jsonData = "{\"NeedUpDateFields\": [],\"NeedReturnFields\": [],\"IsDeleteEntry\": \"true\",\"SubSystemId\": \"\",\"IsVerifyBaseDataField\": \"False\",\"IsEntryBatchFill\": \"true\",\"ValidateFlag\": \"true\",\"NumberSearch\": \"true\",\"IsAutoAdjustField\": \"False\",\"InterationFlags\": \"\",\"IgnoreInterationFlag\": \"\",\"IsControlPrecision\": \"False\",\"Model\": {\"FBillTypeID\": {\"FNUMBER\": \"XSDD01_SYS\"},\"FDate\": \"2022-04-27 00:00:00\",\"FSaleOrgId\": {\"FNumber\": \"100\"},\"FCustId\": {\"FNumber\": \"SCMKH100001\"},\"FReceiveId\": {\"FNumber\": \"SCMKH100001\"},\"FSaleDeptId\": {\"FNumber\": \"SCMBM000001\"},\"FSalerId\": {\"FNumber\": \"SCMYG000001_SCMGW000001_1\"},\"FSettleId\": {\"FNumber\": \"SCMKH100001\"},\"FChargeId\": {\"FNumber\": \"SCMKH100001\"},\"FSaleOrderFinance\": {\"FSettleCurrId\": {\"FNumber\": \"PRE001\"},\"FIsPriceExcludeTax\": 'true',\"FIsIncludedTax\": 'true',\"FExchangeTypeId\": {\"FNumber\": \"HLTX01_SYS\"}},\"FSaleOrderEntry\": [{\"FRowType\": \"Standard\",\"FMaterialId\": {\"FNumber\": \"SCMWL100002\"},\"FUnitID\": {\"FNumber\": \"Pcs\"},\"FQty\": 10,\"FPriceUnitId\": {\"FNumber\": \"Pcs\"},\"FPrice\": 8.849558,\"FTaxPrice\": 10,\"FEntryTaxRate\": 13,\"FDeliveryDate\": \"2022-04-27 15:15:54\",\"FStockOrgId\": {\"FNumber\": \"100\"},\"FSettleOrgIds\": {\"FNumber\": \"100\"},\"FSupplyOrgId\": {\"FNumber\": \"100\"},\"FOwnerTypeId\": \"BD_OwnerOrg\",\"FOwnerId\": {\"FNumber\": \"100\"},\"FReserveType\": \"1\",\"FPriceBaseQty\": 10,\"FStockUnitID\": {\"FNumber\": \"Pcs\"},\"FStockQty\": 10,\"FStockBaseQty\": 10,\"FOUTLMTUNIT\": \"SAL\",\"FOutLmtUnitID\": {\"FNumber\": \"Pcs\"},\"FAllAmountExceptDisCount\": 100,\"FOrderEntryPlan\": [{\"FPlanDate\": \"2022-04-27 15:15:54\",\"FPlanQty\": 10}]}],\"FSaleOrderPlan\": [{\"FRecAdvanceRate\": 100,\"FRecAdvanceAmount\": 100}],\"FBillNo\":" + "\"" + Number + "\"" + ",}}";
                    var result = client.Execute<string>("Kingdee.BOS.WebApi.ServicesStub.DynamicFormService.Save",
                    new object[] { "SAL_SaleOrder", newJson });

                    ResultData resultdata = JsonConvert.DeserializeObject<ResultData>(result);
                    if (resultdata.Result.ResponseStatus.IsSuccess)
                    {
                        var FbillNoList = "";
                        foreach (var FbillNo in   resultdata.Result.NeedReturnData)
                        {
                            
                            FbillNoList = FbillNoList+FbillNo.FBillNo;
                        }
                        var datas = new
                        {
                            code = 200,
                            msg = "ok",
                            date = "同步成功！销售单号：" + FbillNoList + ""
                        };
                        heard.Flag = 1;
                        heard.FParameter = newJson;
                        heard.FReason = FbillNoList;
                        context.SaveChanges();
                        return Ok(datas);
                        
                    }
                    else
                    {
                        var ErrorList = "";
                        foreach (var Error in resultdata.Result.ResponseStatus.Errors)
                        {

                            ErrorList = ErrorList + Error.Message;
                        }
                        var datas = new
                        {
                            code = 400,
                            msg = "err,同步异常！" + ErrorList + "",
                            date = ""
                        };
                        heard.Flag = 2;
                        heard.FParameter = newJson;
                        heard.FReason =  ErrorList;
                        context.SaveChanges();
                        return Ok(datas);
                    }
                    
                }
                else
                {
                    return Ok("登录失败！");
                }
                
            }
            catch(Exception ex)
            {
                var datas = new
                {
                    code = 400,
                    msg = "失败",
                    date = ex.ToString()
                };
                return Ok(datas);
            }

        }
    }
}