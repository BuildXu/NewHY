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
using System.Windows.Media.Media3D;

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
                    var columnNamesToMatch = new List<string> { "序号", "图号/Φ标准", "名称", "规格", "材质", "数量", "单重", "交期备注", "令号", "件号", "项号", "位号", "机名", "行号", "品号", "产号", "物料", "备注","项目号","计划号", "WBS","追踪号","炉批号", "任务号", "零件号", "设备号", "合同号", "订单编号", "产品编号", "产品编号", "容器编号", "锻件编号", "管子编号", "制造编号", "物料编码", "零件编号", "出厂编号", "设备名称", "入库编号" };

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
                                var fname = row.GetCell(columnIndices["名称"]) != null ? row.GetCell(columnIndices["名称"]).ToString() : "";
                                var fdescription = row.GetCell(columnIndices["规格"]) != null ? row.GetCell(columnIndices["规格"]).ToString() : "";
                                var fsliMetal = row.GetCell(columnIndices["材质"]) != null ? row.GetCell(columnIndices["材质"]).ToString() : "";
                                var fsliDrawingNo = row.GetCell(columnIndices["图号/Φ标准"]) != null ? row.GetCell(columnIndices["图号/Φ标准"]).ToString() : "";
                                var result = dbContext.Sli_bd_materials_view
                                .Where(item => item.Fname == fname && item.Fdescription == fdescription && item.FsliMetal == fsliMetal && item.FsliDrawingNo == fsliDrawingNo)
                                .Select(item => item.Fnumber)
                                .FirstOrDefault();
                                var FSerialNo = row.GetCell(columnIndices["序号"]) != null ? Convert.ToString(row.GetCell(columnIndices["序号"])) : "";
                                var FProjectNo = row.GetCell(columnIndices["项目号"]) != null ? row.GetCell(columnIndices["项目号"]).ToString() : "";
                                var FdeliveryDate = ( row.GetCell(columnIndices["交期备注"])!=null) ? Convert.ToString(row.GetCell(columnIndices["交期备注"])) : "";
                                var Fqty = row.GetCell(columnIndices["数量"]) != null ? int.Parse(row.GetCell(columnIndices["数量"]).ToString()) : 0;
                                var Fwight = row.GetCell(columnIndices["单重"]) != null ? int.Parse(row.GetCell(columnIndices["单重"]).ToString()) : 0;
                                var FOrderNo = row.GetCell(columnIndices["令号"]) != null ? Convert.ToString(row.GetCell(columnIndices["令号"])) : "";
                                var FPartNo = row.GetCell(columnIndices["件号"]) != null ? Convert.ToString(row.GetCell(columnIndices["件号"])) : "";
                                var FItemNo = row.GetCell(columnIndices["项号"]) != null ? Convert.ToString(row.GetCell(columnIndices["项号"])) : "";
                                var FPositionNo = row.GetCell(columnIndices["位号"]) != null ? Convert.ToString(row.GetCell(columnIndices["位号"])) : "";
                                var FMachineName = row.GetCell(columnIndices["机名"]) != null ? Convert.ToString(row.GetCell(columnIndices["机名"])) : "";
                                var Fseq = row.GetCell(columnIndices["行号"])!= null ? Convert.ToInt32(row.GetCell(columnIndices["行号"]).ToString()) : 0;
                                var FarticleNo = row.GetCell(columnIndices["品号"]) != null ? Convert.ToString(row.GetCell(columnIndices["品号"])) : "";
                                var FProductNo = row.GetCell(columnIndices["产号"]) != null ? Convert.ToString(row.GetCell(columnIndices["产号"])) : "";
                                var Fmaterial = row.GetCell(columnIndices["物料"]) != null ? Convert.ToString(row.GetCell(columnIndices["物料"])) : "";
                                var FNote = row.GetCell(columnIndices["备注"]) != null ? Convert.ToString(row.GetCell(columnIndices["备注"])) : "";
                                var FPlanNo = row.GetCell(columnIndices["计划号"]) != null ? Convert.ToString(row.GetCell(columnIndices["计划号"])) : "";
                                var Fwbs = row.GetCell(columnIndices["WBS"]) != null ? Convert.ToString(row.GetCell(columnIndices["WBS"])) : "";
                                var FTrackingNo = row.GetCell(columnIndices["追踪号"]) != null ? Convert.ToString(row.GetCell(columnIndices["追踪号"])) : "";
                                var FUrnaceBatchNo = row.GetCell(columnIndices["炉批号"]) != null ? Convert.ToString(row.GetCell(columnIndices["炉批号"])) : "";
                                var FTaskNo = row.GetCell(columnIndices["任务号"]) != null ? Convert.ToString(row.GetCell(columnIndices["任务号"])) : "";
                                var FPartNoC = row.GetCell(columnIndices["零件号"]) != null ? Convert.ToString(row.GetCell(columnIndices["零件号"])) : "";
                                var FEquipmentNo = row.GetCell(columnIndices["设备号"]) != null ? Convert.ToString(row.GetCell(columnIndices["设备号"])) : "";
                                var FContractNo = row.GetCell(columnIndices["合同号"]) != null ? Convert.ToString(row.GetCell(columnIndices["合同号"])) : "";
                                var FBillNo = row.GetCell(columnIndices["订单编号"]) != null ? Convert.ToString(row.GetCell(columnIndices["订单编号"])) : "";
                                var FProductNoC = row.GetCell(columnIndices["产品编号"]) != null ? Convert.ToString(row.GetCell(columnIndices["产品编号"])) : "";
                                var FContainerNo = row.GetCell(columnIndices["容器编号"]) != null ? Convert.ToString(row.GetCell(columnIndices["容器编号"])) : "";
                                var FForgingNo = row.GetCell(columnIndices["锻件编号"]) != null ? Convert.ToString(row.GetCell(columnIndices["锻件编号"])) : "";
                                var FPipeNo = row.GetCell(columnIndices["管子编号"]) != null ? Convert.ToString(row.GetCell(columnIndices["管子编号"])) : "";
                                var FManufacturingNo = row.GetCell(columnIndices["制造编号"]) != null ? Convert.ToString(row.GetCell(columnIndices["制造编号"])) : "";
                                var FmaterialNo = row.GetCell(columnIndices["物料编码"]) != null ? Convert.ToString(row.GetCell(columnIndices["物料编码"])) : "";
                                var FPartNoC1 = row.GetCell(columnIndices["零件编号"]) != null ? Convert.ToString(row.GetCell(columnIndices["零件编号"])) : "";
                                var FFactoryC = row.GetCell(columnIndices["出厂编号"]) != null ? Convert.ToString(row.GetCell(columnIndices["出厂编号"])) : "";
                                var FEquipmentNoC = row.GetCell(columnIndices["设备名称"]) != null ? Convert.ToString(row.GetCell(columnIndices["设备名称"])) : "";
                                var FStockNoC = row.GetCell(columnIndices["入库编号"]) != null ? Convert.ToString(row.GetCell(columnIndices["入库编号"])) : "";

                                var dataItem = new sli_sale_orderImportentry
                                {
                                    // 根据 Excel 中的列和你的数据模型进行映射
                                    FSerialNo = FSerialNo,//row.GetCell(columnIndices["序号"]).CellType != CellType.Blank ? int.Parse(row.GetCell(columnIndices["序号"]).ToString()) : 0,
                                   
                                    FProjectNo = FProjectNo,// row.GetCell(columnIndices["项目号"]).CellType != CellType.Blank ? row.GetCell(columnIndices["项目号"]).ToString() : "",
                                    FdeliveryDate = FdeliveryDate,// row.GetCell(columnIndices["交期备注"]).CellType != CellType.Blank ? Convert.ToString(row.GetCell(columnIndices["交期备注"])) : "",
                                    Fname = fname,
                                    Fdescription = fdescription,
                                    FsliMetal = fsliMetal,
                                    Fqty = Fqty,// row.GetCell(columnIndices["数量"]).CellType != CellType.Blank ? int.Parse(row.GetCell(columnIndices["数量"]).ToString()) : 0,
                                    Fwight = Fwight,// row.GetCell(columnIndices["单重"]).CellType != CellType.Blank ? int.Parse(row.GetCell(columnIndices["单重"]).ToString()) : 0,
                                    FsliDrawingNo = fsliDrawingNo,
                                    FOrderNo = FOrderNo,// row.GetCell(columnIndices["令号"]).CellType != CellType.Blank ? Convert.ToString(row.GetCell(columnIndices["令号"]).ToString()) : "",
                                    FPartNo = FPartNo,// row.GetCell(columnIndices["件号"]).CellType != CellType.Blank ? Convert.ToString(row.GetCell(columnIndices["件号"]).ToString()) : "",
                                    FItemNo = FItemNo,// row.GetCell(columnIndices["项号"]).CellType != CellType.Blank ? Convert.ToString(row.GetCell(columnIndices["项号"]).ToString()) : "",
                                    FPositionNo = FPositionNo,// row.GetCell(columnIndices["位号"]).CellType != CellType.Blank ? Convert.ToString(row.GetCell(columnIndices["位号"]).ToString()) : "",
                                    FMachineName = FMachineName,// row.GetCell(columnIndices["机名"]).CellType != CellType.Blank ? Convert.ToString(row.GetCell(columnIndices["机名"]).ToString()) : "",
                                    Fseq = Fseq,// row.GetCell(columnIndices["行号"]).CellType != CellType.Blank ? Convert.ToString(row.GetCell(columnIndices["行号"]).ToString()) : "",
                                    FarticleNo = FarticleNo,// row.GetCell(columnIndices["品号"]).CellType != CellType.Blank ? Convert.ToString(row.GetCell(columnIndices["品号"]).ToString()) : "",
                                    FProductNo = FProductNo,// row.GetCell(columnIndices["产号"]).CellType != CellType.Blank ? Convert.ToString(row.GetCell(columnIndices["产号"]).ToString()) : "",
                                    Fmaterial = Fmaterial,// row.GetCell(columnIndices["物料"]).CellType != CellType.Blank ? Convert.ToString(row.GetCell(columnIndices["物料"]).ToString()) : "",
                                    FNote = FNote,//row.GetCell(columnIndices["备注"]).CellType != CellType.Blank ? Convert.ToString(row.GetCell(columnIndices["备注"]).ToString()) : "",
                                    FPlanNo = FPlanNo,// row.GetCell(columnIndices["计划号"]).CellType != CellType.Blank ? Convert.ToString(row.GetCell(columnIndices["计划号"]).ToString()) : "",
                                    Fwbs = Fwbs,// row.GetCell(columnIndices["WBS"]).CellType != CellType.Blank ? Convert.ToString(row.GetCell(columnIndices["WBS"]).ToString()) : "",
                                    FTrackingNo = FTrackingNo,// row.GetCell(columnIndices["追踪号"]).CellType != CellType.Blank ? Convert.ToString(row.GetCell(columnIndices["追踪号"]).ToString()) : "",
                                    FUrnaceBatchNo = FUrnaceBatchNo,// row.GetCell(columnIndices["炉批号"]).CellType != CellType.Blank ? Convert.ToString(row.GetCell(columnIndices["炉批号"]).ToString()) : "",
                                    FTaskNo = FTaskNo,//row.GetCell(columnIndices["任务号"]).CellType != CellType.Blank ? Convert.ToString(row.GetCell(columnIndices["任务号"]).ToString()) : "",
                                    FPartNoC = FPartNoC,// row.GetCell(columnIndices["零件号"]).CellType != CellType.Blank ? Convert.ToString(row.GetCell(columnIndices["零件号"]).ToString()) : "",
                                    FEquipmentNo = FEquipmentNo,// row.GetCell(columnIndices["设备号"]).CellType != CellType.Blank ? Convert.ToString(row.GetCell(columnIndices["设备号"]).ToString()) : "",
                                    FContractNo = FContractNo,// row.GetCell(columnIndices["合同号"]).CellType != CellType.Blank ? Convert.ToString(row.GetCell(columnIndices["合同号"]).ToString()) : "",
                                    FBillNo = FBillNo,// row.GetCell(columnIndices["订单编号"]).CellType != CellType.Blank ? Convert.ToString(row.GetCell(columnIndices["订单编号"]).ToString()) : "",
                                    FProductNoC = FProductNoC,// row.GetCell(columnIndices["产品编号"]).CellType != CellType.Blank ? Convert.ToString(row.GetCell(columnIndices["产品编号"]).ToString()) : "",
                                    FContainerNo = FContainerNo,// row.GetCell(columnIndices["容器编号"]).CellType != CellType.Blank ? Convert.ToString(row.GetCell(columnIndices["容器编号"]).ToString()) : "",
                                    FForgingNo = FForgingNo,// row.GetCell(columnIndices["锻件编号"]).CellType != CellType.Blank ? Convert.ToString(row.GetCell(columnIndices["锻件编号"]).ToString()) : "",
                                    FPipeNo = FPipeNo,// row.GetCell(columnIndices["管子编号"]).CellType != CellType.Blank ? Convert.ToString(row.GetCell(columnIndices["管子编号"]).ToString()) : "",
                                    FManufacturingNo = FManufacturingNo,// row.GetCell(columnIndices["制造编号"]).CellType != CellType.Blank ? Convert.ToString(row.GetCell(columnIndices["制造编号"]).ToString()) : "",
                                    FmaterialNo = FmaterialNo,// row.GetCell(columnIndices["物料编码"]).CellType != CellType.Blank ? Convert.ToString(row.GetCell(columnIndices["物料编码"]).ToString()) : "",
                                    FPartNoC1 = FPartNoC1,// row.GetCell(columnIndices["零件编号"]).CellType != CellType.Blank ? Convert.ToString(row.GetCell(columnIndices["零件编号"]).ToString()) : "",
                                    FFactoryC = FFactoryC,// row.GetCell(columnIndices["出厂编号"]).CellType != CellType.Blank ? Convert.ToString(row.GetCell(columnIndices["出厂编号"]).ToString()) : "",
                                    FEquipmentNoC = FEquipmentNoC,// row.GetCell(columnIndices["设备名称"]).CellType != CellType.Blank ? Convert.ToString(row.GetCell(columnIndices["设备名称"]).ToString()) : "",
                                    FStockNoC = FStockNoC,// row.GetCell(columnIndices["入库编号"]).CellType != CellType.Blank ? Convert.ToString(row.GetCell(columnIndices["入库编号"]).ToString()) : "",
                                    //fstatus = 0,
                                    Fid = header.FID,
                                    FmaterialNumber = result == null ? "" : result
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
                query = query.Where(t => t.Fid == id.Value);
            }

            var totalCount = query.Count(); //记录数
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize); // 页数
            var paginatedQuery = query.OrderByDescending(b => b.Id).Skip((page - 1) * pageSize).Take(pageSize); //  某页记录
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
                query = query.Where(t => t.Id == id.Value);
            }
            if (fid.HasValue)
            {
                query = query.Where(t => t.Fid == fid.Value);
            }
            //if (fid.HasValue)
            //{
            //    query = query.Where(t => t.fid == fid.Value);
            //}

            var totalCount = query.Count(); //记录数
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize); // 页数
            var paginatedQuery = query.OrderByDescending(b => b.FContractNo1).ThenBy(b=>b.Fseq).Skip((page - 1) * pageSize).Take(pageSize); //  某页记录
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
        public IHttpActionResult GetTableBysale_order_view2(int? id = null, int? fid = null )
        {
            var context = new YourDbContext();
            IQueryable<sli_sale_orderImport_view> query = context.Sli_sale_orderImport_view;

           
            if (id.HasValue)
            {
                query = query.Where(t => t.Id == id.Value);
            }
            if (fid.HasValue)
            {
                query = query.Where(t => t.Fid == fid.Value);
            }
            //if (fid.HasValue)
            //{
            //    query = query.Where(t => t.fid == fid.Value);
            //}

            //var totalCount = query.Count(); //记录数
            //var totalPages = (int)Math.Ceiling((double)totalCount / pageSize); // 页数
            //var paginatedQuery = query.OrderByDescending(b => b.Fseq).Skip((page - 1) * pageSize).Take(pageSize); //  某页记录
            //var datas = query.ToList();
            var response = new    // 定义 前端返回数据  总记录，总页，当前页 ，size,返回记录
            {
                code = 200,
                msg = "OK",
                data = new
                {
                    //totalCounts = totalCount,
                    //totalPagess = totalPages,
                    //currentPages = page,
                    //pageSizes = pageSize,
                    data = query
                }
            };

            return Json(response);
        }

        [System.Web.Http.HttpGet]
        public IHttpActionResult GetTableBysale_order_view1(int? id = null, int? fid = null, int page = 1, int pageSize = 10, string FCustomerName = null)
        {
            var context = new YourDbContext();
            IQueryable<sli_sale_orderImport_view1> query = context.Sli_sale_orderImport_view1;

            if (!string.IsNullOrEmpty(FCustomerName))
            {
                query = query.Where(q => q.FCustomerName.Contains(FCustomerName));
            }
            if (id.HasValue)
            {
                query = query.Where(t => t.FID == id.Value);
            }
            //if (fid.HasValue)
            //{
            //    query = query.Where(t => t.fid == fid.Value);
            //}

            var totalCount = query.Count(); //记录数
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize); // 页数
            var paginatedQuery = query.OrderByDescending(b => b.FID).Skip((page - 1) * pageSize).Take(pageSize); //  某页记录
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
        public IHttpActionResult GetTableBysale_order_view3(int? id = null)
        {
            var context = new YourDbContext();
            IQueryable<sli_sale_orderImport_view1> query = context.Sli_sale_orderImport_view1;

            if (id.HasValue)
            {
                query = query.Where(t => t.FID == id.Value);
            }
            //if (fid.HasValue)
            //{
            //    query = query.Where(t => t.fid == fid.Value);
            //}

            //var totalCount = query.Count(); //记录数
            //var totalPages = (int)Math.Ceiling((double)totalCount / pageSize); // 页数
            //var paginatedQuery = query.OrderByDescending(b => b.FID).Skip((page - 1) * pageSize).Take(pageSize); //  某页记录
            //var datas = query.ToList();
            var response = new    // 定义 前端返回数据  总记录，总页，当前页 ，size,返回记录
            {
                code = 200,
                msg = "OK",
                data = new
                {
                    //totalCounts = totalCount,
                    //totalPagess = totalPages,
                    //currentPages = page,
                    //pageSizes = pageSize,
                    data = query
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


                    

                    foreach (var childTableData in import.sli_sale_orderImportentry)
                    {
                        var orderImportentry = context.Sli_sale_orderImportentry.FirstOrDefault(p => p.Id == childTableData.Id);
                        //var entry = new sli_sale_orderImportentry
                        //{
                        //orderImportentry.fid = import.FID;
                        var FNumber = import.FContractNo1 +"-"+childTableData.FSerialNo;   //根据合同、序号生成唯一码，并判断是否已导入
                        System.Diagnostics.Debug.WriteLine(FNumber);
                        var results = context.Sli_sale_orderImportentry
                                .Where(e => e.FmaterialNumber == FNumber)
                                .ToList();
                        System.Diagnostics.Debug.WriteLine(results);
                        if (results.Count >0)
                        {
                            var data12 = new
                            {
                                code = 400,
                                msg = "err",
                                date = FNumber + "该序号已存在！不能保存"
                            };
                            return Ok(data12);
                        }
                        else
                        {
                            orderImportentry.FmaterialNumber = FNumber;
                            orderImportentry.FSerialNo = childTableData.FSerialNo;
                            orderImportentry.FsliDrawingNo = childTableData.FsliDrawingNo;
                            orderImportentry.Fname = childTableData.Fname;
                            orderImportentry.Fdescription = childTableData.Fdescription;
                            orderImportentry.FsliMetal = childTableData.FsliMetal;
                            orderImportentry.Fqty = childTableData.Fqty;
                            orderImportentry.Fwight = childTableData.Fwight;
                            orderImportentry.FdeliveryDate = childTableData.FdeliveryDate;
                            orderImportentry.FOrderNo = childTableData.FOrderNo;
                            orderImportentry.FPartNo = childTableData.FPartNo;
                            orderImportentry.FItemNo = childTableData.FItemNo;
                            orderImportentry.FPositionNo = childTableData.FPositionNo;
                            orderImportentry.FMachineName = childTableData.FMachineName;
                            orderImportentry.Fseq = childTableData.Fseq;
                            orderImportentry.FarticleNo = childTableData.FarticleNo;
                            orderImportentry.FProductNo = childTableData.FProductNo;
                            orderImportentry.Fmaterial = childTableData.Fmaterial;
                            orderImportentry.FNote = childTableData.FNote;
                            orderImportentry.FProjectNo = childTableData.FProjectNo;
                            orderImportentry.FPlanNo = childTableData.FPlanNo;
                            orderImportentry.Fwbs = childTableData.Fwbs;
                            orderImportentry.FTrackingNo = childTableData.FTrackingNo;
                            orderImportentry.FUrnaceBatchNo = childTableData.FUrnaceBatchNo;
                            orderImportentry.FTaskNo = childTableData.FTaskNo;
                            orderImportentry.FPartNoC = childTableData.FPartNoC;
                            orderImportentry.FEquipmentNo = childTableData.FEquipmentNo;
                            orderImportentry.FContractNo = childTableData.FContractNo;
                            orderImportentry.FBillNo = childTableData.FBillNo;
                            orderImportentry.FProductNoC = childTableData.FProductNoC;
                            orderImportentry.FContainerNo = childTableData.FContainerNo;
                            orderImportentry.FForgingNo = childTableData.FForgingNo;
                            orderImportentry.FPipeNo = childTableData.FPipeNo;
                            orderImportentry.FManufacturingNo = childTableData.FManufacturingNo;
                            orderImportentry.FmaterialNo = childTableData.FmaterialNo;
                            orderImportentry.FPartNoC1 = childTableData.FPartNoC1;
                            orderImportentry.FFactoryC = childTableData.FFactoryC;
                            orderImportentry.FEquipmentNoC = childTableData.FEquipmentNoC;
                            orderImportentry.FStockNoC = childTableData.FStockNoC;
                            await context.SaveChangesAsync();
                        }
                        //};
                        //context.Sli_sale_orderImportentry.Add(orderImportentry);
                    }

                    //await context.SaveChangesAsync();
                    orderImport.FCustomerName = import.FCustomerName;
                    orderImport.FCustomerID = import.FCustomerID;
                    orderImport.FBillNo = import.FBillNo;
                    orderImport.FSaleId = import.FSaleId;
                    orderImport.FContractNo1 = import.FContractNo1;
                    orderImport.FpurProperty = import.FpurProperty;

                    //context.Sli_sale_orderImportentry.RemoveRange(orderImportentry);
                    await context.SaveChangesAsync();
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
                ApiClient client = new ApiClient("http://19vs7gv47690.vicp.fun/K3cloud/"); //接口地址
                string dbId = "67b289c33bafdd"; //账套ID
                bool bLogin = client.Login(dbId, "Administrator", "kingdee123*", 2052);
                if (bLogin)
                {
                    
                    var heard = context.Sli_sale_orderImport.FirstOrDefault(p => p.FID == id);   //获取表头单行数据
                    var FcustomerNumer = context.Sli_bd_customer_view.FirstOrDefault(p => p.FNAME == heard.FCustomerName); //根据客户名称查询客户代码
                    var entity = context.Sli_sale_orderImportentry.Where(p => p.Fid == id);   //获取表体多行数据
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
                    rootObject.Model.FBillNo = heard.FContractNo1;

                    foreach (var entitydata in entityList)
                    {
                        SaleOrderEntry newEntry = new SaleOrderEntry();

                        
                        newEntry.FRowType = "Standard";
                        newEntry.FMaterialId = new OrgId { FNumber = entitydata.FmaterialNumber };
                        newEntry.FUnitID = new OrgId { FNumber = "Pcs" };
                        newEntry.FInventoryQty = 0.0;
                        newEntry.FCurrentInventory = 0.0;
                        newEntry.FAwaitQty = 0.0;
                        newEntry.FAvailableQty = 0.0;
                        newEntry.FQty = entitydata.Fqty;
                        newEntry.FPriceUnitId = new OrgId { FNumber = "Pcs" };
                        newEntry.FOldQty = 0.0;
                        newEntry.FPrice = 0.0;
                        newEntry.FTaxPrice = 0.0;
                        newEntry.FIsFree = false;
                        newEntry.FEntryTaxRate = 13.00;
                        newEntry.FDeliveryDate = entitydata.FdeliveryDate;
                        newEntry.FStockOrgId = new OrgId { FNumber = "100" };
                        newEntry.FSettleOrgIds = new OrgId { FNumber = "100" };
                        newEntry.FSupplyOrgId = new OrgId { FNumber = "100" };
                        newEntry.FOwnerTypeId = "BD_OwnerOrg";
                        newEntry.FOwnerId = new OrgId { FNumber = "100" };
                        newEntry.FSrcType = "";
                        newEntry.FReserveType = "1";
                        newEntry.FPriceBaseQty = entitydata.Fqty;
                        newEntry.FStockUnitID = new OrgId { FNumber = "Pcs" };
                        newEntry.FStockQty = entitydata.Fqty;
                        newEntry.FStockBaseQty = entitydata.Fqty;
                        newEntry.FOUTLMTUNIT = "SAL";
                        newEntry.FOutLmtUnitID = new OrgId { FNumber = "Pcs" };
                        newEntry.FISMRP = false;
                        newEntry.FISMRPCAL = false;
                        newEntry.FAllAmountExceptDisCount = 0.0;
                        //newEntry.FsliHeatTreatment = entitydata.fsliHeatTreatment;
                        //newEntry.FsliTestBarQty = entitydata.fsliTestBarQty;
                        //newEntry.FsliMetel = new OrgId { FNumber = entitydata.fsliMetal };
                        //newEntry.FsliExplanation = entitydata.fsliExplanation;
                        //newEntry.FsliNotice = entitydata.fsliNotice;
                        //newEntry.FsliWorkOrder = entitydata.fsliWorkOrder;
                        //newEntry.FsliSaleOrder = entitydata.fsliSaleOrder;
                        //newEntry.FsliQuotationNo = entitydata.fsliQuotationNo;
                        //newEntry.FsliStockNo = entitydata.fsliStockNo;
                        //newEntry.FsliBlank = entitydata.fsliBlank;
                        //newEntry.FsliDrawingNo = entitydata.fsliDrawingNo;

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