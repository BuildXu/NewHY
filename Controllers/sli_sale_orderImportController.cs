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
    public class sli_sale_orderImportController : ApiController
    {

        [System.Web.Mvc.HttpPost]
        public IHttpActionResult ImportExcel()
        {
            var httpRequest = System.Web.HttpContext.Current.Request;
            if (httpRequest.Files.Count == 0)
            {
                return BadRequest("请选择一个有效的 Excel 文件。");
            }

            var excelFile = httpRequest.Files[0];
            var headerid = 0;
            try
            {
                // 使用 NPOI 读取 Excel 文件
                using (var stream = excelFile.InputStream)
                {

                    IWorkbook workbook;

                    string extension = System.IO.Path.GetExtension(excelFile.FileName);

                    if (extension.Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                    {
                        workbook = new XSSFWorkbook(stream);
                        // 处理 xlsx 文件
                    }
                    else if (extension.Equals(".xls", StringComparison.OrdinalIgnoreCase))
                    {
                        workbook = new HSSFWorkbook(stream);
                        // 处理 xls 文件
                    }
                    else
                    {
                        throw new InvalidOperationException("不支持的文件格式。");
                    }
                    //stream.ReadTimeout = 111;
                    //var workbook = new XSSFWorkbook(stream);
                    var sheet = workbook.GetSheetAt(0);

                    var dataList = new List<sli_sale_orderImportentry>();

                    // 假设要匹配的列名列表
                    var columnNamesToMatch = new List<string> { "序号", "选择", "提交供应商标记", "询价单号", "供应商名称", "项目号", "需求日期", "名称", "规格", "材质", "需求数量", "毛坯热处理", "试棒数量", "附注", "采购备注", "图纸号", "毛坯图号", "生产号", "订单号", "报价状态", "仓库", "仓库位置" };

                    // 存储匹配到的列索引
                    var columnIndices = new Dictionary<string, int>();

                    // 获取列索引
                    var headerRow = sheet.GetRow(0);
                    for (int cellIndex = 0; cellIndex < headerRow.LastCellNum; cellIndex++)
                    {
                        var cellValue = headerRow.GetCell(cellIndex)?.ToString();
                        if (columnNamesToMatch.Contains(cellValue))
                        {
                            columnIndices[cellValue] = cellIndex;
                        }
                    }
                    // 检查是否所有要匹配的列都找到了
                    if (columnNamesToMatch.Any(name => !columnIndices.ContainsKey(name)))
                    {
                        return BadRequest("Excel 文件中未找到所有需要匹配的列。");
                    }
                    using (var dbContext = new YourDbContext())
                    {
                        // 空插表头,获取当前插入的表头ID
                        var header = new sli_sale_orderImport
                        {
                            Flag = 0,
                            FCustomerID = 1
                        };
                        dbContext.Sli_sale_orderImport.Add(header);
                        dbContext.SaveChanges();
                        headerid = header.FID;


                        //循环插入表体
                        for (int rowIndex = 1; rowIndex <= sheet.LastRowNum; rowIndex++)
                        {
                            var row = sheet.GetRow(rowIndex);
                            if (row != null)
                            {
                                var fname = row.GetCell(columnIndices["名称"]).CellType != CellType.Blank ? row.GetCell(columnIndices["名称"]).ToString() : "";
                                var fdescription = row.GetCell(columnIndices["规格"]).CellType != CellType.Blank ? row.GetCell(columnIndices["规格"]).ToString() : "";
                                var fsliMetal = row.GetCell(columnIndices["材质"]).CellType != CellType.Blank ? row.GetCell(columnIndices["材质"]).ToString() : "";
                                var fsliDrawingNo = row.GetCell(columnIndices["图纸号"]).CellType != CellType.Blank ? row.GetCell(columnIndices["图纸号"]).ToString() : "";
                                var result = dbContext.Sli_bd_materials_view
                                .Where(item => item.Fname == fname && item.Fdescription == fdescription && item.FsliMetal == fsliMetal && item.FsliDrawingNo == fsliDrawingNo)
                                .Select(item => item.Fnumber)
                                .FirstOrDefault();

                                var dataItem = new sli_sale_orderImportentry
                                {
                                    // 根据 Excel 中的列和你的数据模型进行映射
                                    fseq = row.GetCell(columnIndices["序号"]).CellType != CellType.Blank ? int.Parse(row.GetCell(columnIndices["序号"]).ToString()) : 0,
                                    fchoose = row.GetCell(columnIndices["选择"]).CellType != CellType.Blank ? int.Parse(row.GetCell(columnIndices["选择"]).ToString()) : 0,
                                    fsupplierSubmit = row.GetCell(columnIndices["提交供应商标记"]).CellType != CellType.Blank ? row.GetCell(columnIndices["提交供应商标记"]).ToString() : "",
                                    finquiryNo = row.GetCell(columnIndices["询价单号"]).CellType != CellType.Blank ? row.GetCell(columnIndices["询价单号"]).ToString() : "",
                                    fsupplierName = row.GetCell(columnIndices["供应商名称"]).CellType != CellType.Blank ? row.GetCell(columnIndices["供应商名称"]).ToString() : "",
                                    fprojectNo = row.GetCell(columnIndices["项目号"]).CellType != CellType.Blank ? row.GetCell(columnIndices["项目号"]).ToString() : "",
                                    fdeliveryDate = row.GetCell(columnIndices["需求日期"]).CellType != CellType.Blank ? row.GetCell(columnIndices["需求日期"]).ToString() : "",
                                    fname = fname,
                                    fdescription = fdescription,
                                    fsliMetal = fsliMetal,
                                    fqty = row.GetCell(columnIndices["需求数量"]).CellType != CellType.Blank ? int.Parse(row.GetCell(columnIndices["需求数量"]).ToString()) : 0,
                                    fsliHeatTreatment = row.GetCell(columnIndices["毛坯热处理"]).CellType != CellType.Blank ? row.GetCell(columnIndices["毛坯热处理"]).ToString() : "",
                                    fsliTestBarQty = row.GetCell(columnIndices["试棒数量"]).CellType != CellType.Blank ? int.Parse(row.GetCell(columnIndices["试棒数量"]).ToString()) : 0,
                                    fsliExplanation = row.GetCell(columnIndices["附注"]).CellType != CellType.Blank ? row.GetCell(columnIndices["附注"]).ToString() : "",
                                    fsliNotice = row.GetCell(columnIndices["采购备注"]).CellType != CellType.Blank ? row.GetCell(columnIndices["采购备注"]).ToString() : "",
                                    fsliDrawingNo = fsliDrawingNo,
                                    fsliBlank = row.GetCell(columnIndices["毛坯图号"]).CellType != CellType.Blank ? row.GetCell(columnIndices["毛坯图号"]).ToString() : "",
                                    fsliWorkOrder = row.GetCell(columnIndices["生产号"]).CellType != CellType.Blank ? row.GetCell(columnIndices["生产号"]).ToString() : "",
                                    fsliSaleOrder = row.GetCell(columnIndices["订单号"]).CellType != CellType.Blank ? row.GetCell(columnIndices["订单号"]).ToString() : "",
                                    fsliQuotationNo = row.GetCell(columnIndices["报价状态"]).CellType != CellType.Blank ? row.GetCell(columnIndices["报价状态"]).ToString() : "",
                                    fsliStockNo = row.GetCell(columnIndices["仓库"]).CellType != CellType.Blank ? row.GetCell(columnIndices["仓库"]).ToString() : "",
                                    fsliStockLocation = row.GetCell(columnIndices["仓库位置"]).CellType != CellType.Blank ? row.GetCell(columnIndices["仓库位置"]).ToString() : "",
                                    //fstatus = 0,
                                    fid = header.FID,
                                    fmaterialNumber = result == null ? "" : result
                                    //...
                                };
                                dataList.Add(dataItem);
                            }
                        }

                        // 将数据保存到数据库

                        dbContext.Sli_sale_orderImportentry.AddRange(dataList);
                        dbContext.SaveChanges();
                    }
                }

                var response = new    // 定义 前端返回数据  总记录，总页，当前页 ，size,返回记录
                {
                    code = 200,
                    msg = "OK",
                    data = headerid

                };

                return Json(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [System.Web.Http.HttpGet]
        public IHttpActionResult GetTableBysale_orderentry(int page = 1, int pageSize = 10, int? id = null)
        {
            var context = new YourDbContext();
            IQueryable<sli_sale_orderImportentry> query = context.Sli_sale_orderImportentry;

            //if (!string.IsNullOrEmpty(Fname))
            //{
            //    query = query.Where(q => q.fname.Contains(Fname));
            //}
            if (id.HasValue)
            {
                query = query.Where(t => t.fid == id.Value);
            }

            var totalCount = query.Count(); //记录数
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize); // 页数
            var paginatedQuery = query.OrderByDescending(b => b.id).Skip((page - 1) * pageSize).Take(pageSize); //  某页记录
            //var datas = query.ToList();
            var response = new    // 定义 前端返回数据  总记录，总页，当前页 ，size,返回记录
            {
                code = 200,
                msg = "OK",
                data = new
                {
                    totalCounts = totalCount,
                    totalPagess = totalPages,
                    currentPages = page,
                    pageSizes = pageSize,
                    data = paginatedQuery
                }
            };

            return Json(response);
        }

        [System.Web.Http.HttpGet]
        public IHttpActionResult GetTableBysale_order_view(int? id = null, int? fid = null, int page = 1, int pageSize = 10,  string FCustomerName = null)
        {
            var context = new YourDbContext();
            IQueryable<sli_sale_orderImport_view> query = context.Sli_sale_orderImport_view;

            if (!string.IsNullOrEmpty(FCustomerName))
            {
                query = query.Where(q => q.FCustomerName.Contains(FCustomerName));
            }
            if (id.HasValue)
            {
                query = query.Where(t => t.id == id.Value);
            }
            if (fid.HasValue)
            {
                query = query.Where(t => t.fid == fid.Value);
            }
            //if (fid.HasValue)
            //{
            //    query = query.Where(t => t.fid == fid.Value);
            //}

            var totalCount = query.Count(); //记录数
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize); // 页数
            var paginatedQuery = query.OrderByDescending(b => b.fseq).Skip((page - 1) * pageSize).Take(pageSize); //  某页记录
            //var datas = query.ToList();
            var response = new    // 定义 前端返回数据  总记录，总页，当前页 ，size,返回记录
            {
                code = 200,
                msg = "OK",
                data = new
                {
                    totalCounts = totalCount,
                    totalPagess = totalPages,
                    currentPages = page,
                    pageSizes = pageSize,
                    data = paginatedQuery
                }
            };

            return Json(response);
        }


        /// <summary>
        /// 更新接口，传入表体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<object> Update([Microsoft.AspNetCore.Mvc.FromBody] sli_sale_orderImport import)
        {
            try
            {
                var context = new YourDbContext();
                var entity = await context.Sli_sale_orderImport.FindAsync(import.FID);
                if (entity == null)
                {
                    var dataNull = new
                    {
                        code = 200,
                        msg = "ok",
                        date = "修改记录不存在"
                    };
                    return dataNull;
                }
                else
                {

                    var orderImport = context.Sli_sale_orderImport.FirstOrDefault(p => p.FID == import.FID);
                    //var orderImportentry = context.Sli_sale_orderImportentry.Where(p => p.fid == import.FID).ToList();


                    orderImport.FCustomerName = import.FCustomerName;
                    orderImport.FCustomerID = import.FCustomerID;

                    //context.Sli_sale_orderImportentry.RemoveRange(orderImportentry);
                    await context.SaveChangesAsync();

                    foreach (var childTableData in import.sli_sale_orderImportentry)
                    {
                        var orderImportentry = context.Sli_sale_orderImportentry.FirstOrDefault(p => p.id == childTableData.id);
                        //var entry = new sli_sale_orderImportentry
                        //{
                        //orderImportentry.fid = import.FID;
                        orderImportentry.fmaterialNumber = childTableData.fmaterialNumber;
                        orderImportentry.fseq = childTableData.fseq;
                        orderImportentry.fchoose = childTableData.fchoose;
                        orderImportentry.fsupplierSubmit = childTableData.fsupplierSubmit;
                        orderImportentry.finquiryNo = childTableData.finquiryNo;
                        orderImportentry.fsupplierName = childTableData.fsupplierName;
                        orderImportentry.fprojectNo = childTableData.fprojectNo;
                        orderImportentry.fdeliveryDate = childTableData.fdeliveryDate;
                        orderImportentry.fname = childTableData.fname;
                        orderImportentry.fdescription = childTableData.fdescription;
                        orderImportentry.fsliMetal = childTableData.fsliMetal;
                        orderImportentry.fqty = childTableData.fqty;
                        orderImportentry.fsliHeatTreatment = childTableData.fsliHeatTreatment;
                        orderImportentry.fsliTestBarQty = childTableData.fsliTestBarQty;
                        orderImportentry.fsliExplanation = childTableData.fsliExplanation;
                        orderImportentry.fsliNotice = childTableData.fsliNotice;
                        orderImportentry.fsliDrawingNo = childTableData.fsliDrawingNo;
                        orderImportentry.fsliBlank = childTableData.fsliBlank;
                        orderImportentry.fsliWorkOrder = childTableData.fsliWorkOrder;
                        orderImportentry.fsliSaleOrder = childTableData.fsliSaleOrder;
                        orderImportentry.fsliQuotationNo = childTableData.fsliQuotationNo;
                        orderImportentry.fsliStockNo = childTableData.fsliStockNo;
                        orderImportentry.fsliStockLocation = childTableData.fsliStockLocation;
                        await context.SaveChangesAsync();
                        //};
                        //context.Sli_sale_orderImportentry.Add(orderImportentry);
                    }
                    //await context.SaveChangesAsync();

                    var datas = new
                    {
                        code = 200,
                        msg = "ok",
                        date = "修改成功！"
                    };
                    return Ok(datas);
                }
            }
            catch (Exception ex)
            {
                var datas = new
                {
                    code = 400,
                    msg = "失败",
                    date = ex.ToString()
                };
                return Ok(datas); ;
            }

        }
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
                ApiClient client = new ApiClient("http://19vs7gv47690.vicp.fun/K3cloud/");
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