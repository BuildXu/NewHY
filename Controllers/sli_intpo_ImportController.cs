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
using Microsoft.Ajax.Utilities;
using NPOI.SS.Formula.PTG;
using System.Diagnostics.Eventing.Reader;

namespace WebApi_SY.Controllers
{
    public class sli_intpo_ImportController : ApiController
    {
        public sli_intpo_ImportController()
        {
            // _context = context;
        }
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

                    var dataList = new List<sli_intpo_Import>();

                    // 假设要匹配的列名列表
                    //var columnNamesToMatch = new List<string> { "序号", "选择", "提交供应商标记", "询价单号", "供应商名称", "项目号", "需求日期", "名称", "规格", "材质", "需求数量", "毛坯热处理", "试棒数量", "附注", "采购备注", "图纸号", "毛坯图号", "生产号", "订单号", "报价状态", "仓库", "仓库位置" };

                    var columnNamesToMatch = new List<string>
{
    "我方文号", "案件名称", "案件状态", "案件类型", "客户文号", "客户名称", "客户代码", "产品类别",
    "案件处理人", "同日申请", "同日递交", "是否同时提实审", "是否提前公布", "是否请求费用减缓",
    "是否优先审查", "是否同时请求DAS码", "是否预审案件", "是否是快维案件", "业务人员", "业务助理",
    "业务部门", "开卷日期", "委案日期", "国家(地区)", "收据号", "合同号", "案件客户联系人",
    "案件客户联系人电话", "案件客户联系人邮箱", "协办所案号", "注册号", "商标类别", "案件承办部门",
    "所属分部", "业务类型", "案件流向", "案件处理人部门", "申请类型", "申请号", "申请日",
    "注册号", "公告日", "新申请递交日", "案件备注", "是否提前公开", "费减比例", "缴费单号",
    "PCT申请号", "PCT申请日", "优先权日", "客户类别", "处理事项", "处理事项(含阶段)",
    "处理事项处理人", "处理事项处理人部门", "事项备注", "处理事项完成日", "初稿日", "内部定稿日",
    "返稿日", "定稿日", "送合作所日", "处理事项状态", "官方期限", "内部定稿日", "客户期限",
    "申请人", "代理机构", "发明人", "费用类型", "费用名称", "费用描述", "费用描述英文", "金额",
    "币别", "换算后币别", "换算后金额", "应收日期", "请款日期", "实收日期", "实收/实付金额",
    "实收币别", "缴费期限", "缴费抬头", "实付日期", "请款单号", "缴年费阶段", "缴费类型",
    "汇率", "费用备注", "修改记录", "收款公司账号", "外方账单号", "外方账单日期", "费用创建日期",
    "发票号", "开票日期", "费用合作所代理机构", "收款状态", "缴费状态", "销账日期", "权利要求项数",
    "说明书页数", "技术领域", "外部案源人", "内部案源人", "专利标签", "提案编号", "代理人",
    "自定义栏位3", "自定义栏位4", "自定义栏位5", "相关发票号一", "相关关发票号二", "付款人名称",
    "文本1", "文本2", "文本3", "文本4", "文本5", "文本6", "文本7", "文本8", "文本9", "文本10",
    "文本11", "文本12", "文本13", "文本14", "文本15", "文本16", "文本17", "文本18", "文本19",
    "文本20", "文本21", "文本22", "文本23", "文本24", "文本25", "文本26", "文本27", "文本28",
    "文本29", "文本30"
};
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

                        var Sli_intpo_Import = new List<sli_intpo_Import>();
                        //循环插入表体
                        for (int rowIndex = 1; rowIndex <= sheet.LastRowNum; rowIndex++)
                        {
                            var row = sheet.GetRow(rowIndex);
                            if (row != null)
                            {
                                //var fname = row.GetCell(columnIndices["名称"]).CellType != CellType.Blank ? row.GetCell(columnIndices["名称"]).ToString() : "";
                                //var fdescription = row.GetCell(columnIndices["规格"]).CellType != CellType.Blank ? row.GetCell(columnIndices["规格"]).ToString() : "";
                                //var fsliMetal = row.GetCell(columnIndices["材质"]).CellType != CellType.Blank ? row.GetCell(columnIndices["材质"]).ToString() : "";
                                //var fsliDrawingNo = row.GetCell(columnIndices["图纸号"]).CellType != CellType.Blank ? row.GetCell(columnIndices["图纸号"]).ToString() : "";
                                //var result = dbContext.Sli_bd_materials_view
                                //.Where(item => item.Fname == fname && item.Fdescription == fdescription && item.FsliMetal == fsliMetal && item.FsliDrawingNo == fsliDrawingNo)
                                //.Select(item => item.Fnumber)
                                //.FirstOrDefault();
                                //var my_document_no = row.GetCell(columnIndices["我方文号"]).CellType != CellType.Blank ? row.GetCell(columnIndices["我方文号"]).ToString() : "";
                                //var case_name = row.GetCell(columnIndices["案件名称"]).CellType != CellType.Blank ? row.GetCell(columnIndices["案件名称"]).ToString() : "";
                                //var case_status = row.GetCell(columnIndices["案件状态"]).CellType != CellType.Blank ? row.GetCell(columnIndices["案件状态"]).ToString() : "";
                                //var case_type = row.GetCell(columnIndices["案件类型"]).CellType != CellType.Blank ? row.GetCell(columnIndices["案件类型"]).ToString() : "";
                                //var customer_document_no = row.GetCell(columnIndices["客户文号"]).CellType != CellType.Blank ? row.GetCell(columnIndices["客户文号"]).ToString() : "";
                                //var customer_name = row.GetCell(columnIndices["客户名称"]).CellType != CellType.Blank ? row.GetCell(columnIndices["客户名称"]).ToString() : "";
                                //var customer_no = row.GetCell(columnIndices["客户代码"]).CellType != CellType.Blank ? row.GetCell(columnIndices["客户代码"]).ToString() : "";

                                var my_document_no = Convert.ToString(row.GetCell(columnIndices["我方文号"])) ?? "";
                                var case_name = Convert.ToString(row.GetCell(columnIndices["案件名称"])) ?? "";
                                var case_status = Convert.ToString(row.GetCell(columnIndices["案件状态"])) ?? "";
                                var case_type = Convert.ToString(row.GetCell(columnIndices["案件类型"])) ?? "";
                                var customer_document_no = Convert.ToString(row.GetCell(columnIndices["客户文号"])) ?? "";
                                var customer_name = Convert.ToString(row.GetCell(columnIndices["客户名称"])) ?? "";
                                var customer_no = Convert.ToString(row.GetCell(columnIndices["客户代码"])) ?? "";
                                var product_type =Convert.ToString( row.GetCell(columnIndices["产品类别"]));
                                var case_user = Convert.ToString(row.GetCell(columnIndices["案件处理人"]));// row.GetCell(columnIndices["案件处理人"]).CellType != CellType.Blank ? row.GetCell(columnIndices["案件处理人"]).ToString() : "";
                                var date_apply = Convert.ToString(row.GetCell(columnIndices["同日申请"]));// row.GetCell(columnIndices["同日申请"]).CellType != CellType.Blank ? row.GetCell(columnIndices["同日申请"]).ToString() : "";
                                var date_submit = Convert.ToString(row.GetCell(columnIndices["同日递交"])); //row.GetCell(columnIndices["同日递交"]).CellType != CellType.Blank ? row.GetCell(columnIndices["同日递交"]).ToString() : "";
                                var is_date_submit = Convert.ToString(row.GetCell(columnIndices["是否同时提实审"]));// row.GetCell(columnIndices["是否同时提实审"]).CellType != CellType.Blank ? row.GetCell(columnIndices["是否同时提实审"]).ToString() : "";
                                var is_publish = Convert.ToString(row.GetCell(columnIndices["是否提前公布"]));// row.GetCell(columnIndices["是否提前公布"]).CellType != CellType.Blank ? row.GetCell(columnIndices["是否提前公布"]).ToString() : "";
                                var is_reduce = Convert.ToString(row.GetCell(columnIndices["是否请求费用减缓"]));// row.GetCell(columnIndices["是否请求费用减缓"]).CellType != CellType.Blank ? row.GetCell(columnIndices["是否请求费用减缓"]).ToString() : "";
                                var is_first = Convert.ToString(row.GetCell(columnIndices["是否优先审查"]));// row.GetCell(columnIndices["是否优先审查"]).CellType != CellType.Blank ? row.GetCell(columnIndices["是否优先审查"]).ToString() : "";
                                var is_das = Convert.ToString(row.GetCell(columnIndices["是否同时请求DAS码"]));//row.GetCell(columnIndices["是否同时请求DAS码"]).CellType != CellType.Blank ? row.GetCell(columnIndices["是否同时请求DAS码"]).ToString() : "";
                                var is_preview = Convert.ToString(row.GetCell(columnIndices["是否预审案件"]));//row.GetCell(columnIndices["是否预审案件"]).CellType != CellType.Blank ? row.GetCell(columnIndices["是否预审案件"]).ToString() : "";
                                var is_fast_case = Convert.ToString(row.GetCell(columnIndices["是否是快维案件"]));//row.GetCell(columnIndices["是否是快维案件"]).CellType != CellType.Blank ? row.GetCell(columnIndices["是否是快维案件"]).ToString() : "";
                                var business_user = Convert.ToString(row.GetCell(columnIndices["业务人员"]));// row.GetCell(columnIndices["业务人员"]).CellType != CellType.Blank ? row.GetCell(columnIndices["业务人员"]).ToString() : "";
                                var business_deputy = Convert.ToString(row.GetCell(columnIndices["业务助理"]));//  row.GetCell(columnIndices["业务助理"]).CellType != CellType.Blank ? row.GetCell(columnIndices["业务助理"]).ToString() : "";
                                var business_deptment = Convert.ToString(row.GetCell(columnIndices["业务部门"]));//row.GetCell(columnIndices["业务部门"]).CellType != CellType.Blank ? row.GetCell(columnIndices["业务部门"]).ToString() : "";
                                                                                                             // open_date
                                var openDateCell = row.GetCell(columnIndices["开卷日期"]);
                                DateTime? openDate = null;

                                if (openDateCell != null && openDateCell.CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(openDateCell))
                                {
                                    openDate = openDateCell.DateCellValue;
                                }
                                else if (openDateCell != null && DateTime.TryParse(Convert.ToString(openDateCell), out var parsedOpenDate))
                                {
                                    openDate = parsedOpenDate;
                                }
                                var open_date = openDate?.ToString("yyyy-MM-dd") ?? "";

                                // commission_case
                                var commissionCaseCell = row.GetCell(columnIndices["委案日期"]);
                                DateTime? commissionCaseDate = null;

                                if (commissionCaseCell != null && commissionCaseCell.CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(commissionCaseCell))
                                {
                                    commissionCaseDate = commissionCaseCell.DateCellValue;
                                }
                                else if (commissionCaseCell != null && DateTime.TryParse(Convert.ToString(commissionCaseCell), out var parsedCommissionCaseDate))
                                {
                                    commissionCaseDate = parsedCommissionCaseDate;
                                }
                                var commission_case = commissionCaseDate?.ToString("yyyy-MM-dd") ?? "";

                                // handle_event_date
                                var handleEventDateCell = row.GetCell(columnIndices["处理事项完成日"]);
                                DateTime? handleEventDate = null;

                                if (handleEventDateCell != null && handleEventDateCell.CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(handleEventDateCell))
                                {
                                    handleEventDate = handleEventDateCell.DateCellValue;
                                }
                                else if (handleEventDateCell != null && DateTime.TryParse(Convert.ToString(handleEventDateCell), out var parsedHandleEventDate))
                                {
                                    handleEventDate = parsedHandleEventDate;
                                }
                                var handle_event_date = handleEventDate?.ToString("yyyy-MM-dd") ?? "";

                                // official_date
                                var officialDateCell = row.GetCell(columnIndices["官方期限"]);
                                DateTime? officialDate = null;

                                if (officialDateCell != null && officialDateCell.CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(officialDateCell))
                                {
                                    officialDate = officialDateCell.DateCellValue;
                                }
                                else if (officialDateCell != null && DateTime.TryParse(Convert.ToString(officialDateCell), out var parsedOfficialDate))
                                {
                                    officialDate = parsedOfficialDate;
                                }
                                var official_date = officialDate?.ToString("yyyy-MM-dd") ?? "";

                                // customer_date
                                var customerDateCell = row.GetCell(columnIndices["客户期限"]);
                                DateTime? customerDate = null;

                                if (customerDateCell != null && customerDateCell.CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(customerDateCell))
                                {
                                    customerDate = customerDateCell.DateCellValue;
                                }
                                else if (customerDateCell != null && DateTime.TryParse(Convert.ToString(customerDateCell), out var parsedCustomerDate))
                                {
                                    customerDate = parsedCustomerDate;
                                }
                                var customer_date = customerDate?.ToString("yyyy-MM-dd") ?? "";

                                // request_date
                                var requestDateCell = row.GetCell(columnIndices["请款日期"]);
                                DateTime? requestDate = null;

                                if (requestDateCell != null && requestDateCell.CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(requestDateCell))
                                {
                                    requestDate = requestDateCell.DateCellValue;
                                }
                                else if (requestDateCell != null && DateTime.TryParse(Convert.ToString(requestDateCell), out var parsedRequestDate))
                                {
                                    requestDate = parsedRequestDate;
                                }
                                var request_date = requestDate?.ToString("yyyy-MM-dd") ?? "";

                                // actual_date
                                var actualDateCell = row.GetCell(columnIndices["实收日期"]);
                                DateTime? actualDate = null;

                                if (actualDateCell != null && actualDateCell.CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(actualDateCell))
                                {
                                    actualDate = actualDateCell.DateCellValue;
                                }
                                else if (actualDateCell != null && DateTime.TryParse(Convert.ToString(actualDateCell), out var parsedActualDate))
                                {
                                    actualDate = parsedActualDate;
                                }
                                var actual_date = actualDate?.ToString("yyyy-MM-dd") ?? "";

                                // money_create_date
                                var moneyCreateDateCell = row.GetCell(columnIndices["费用创建日期"]);
                                DateTime? moneyCreateDate = null;

                                if (moneyCreateDateCell != null && moneyCreateDateCell.CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(moneyCreateDateCell))
                                {
                                    moneyCreateDate = moneyCreateDateCell.DateCellValue;
                                }
                                else if (moneyCreateDateCell != null && DateTime.TryParse(Convert.ToString(moneyCreateDateCell), out var parsedMoneyCreateDate))
                                {
                                    moneyCreateDate = parsedMoneyCreateDate;
                                }
                                var money_create_date = moneyCreateDate?.ToString("yyyy-MM-dd") ?? "";

                                // create_invoice_date
                                var createInvoiceDateCell = row.GetCell(columnIndices["开票日期"]);
                                DateTime? createInvoiceDate = null;

                                if (createInvoiceDateCell != null && createInvoiceDateCell.CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(createInvoiceDateCell))
                                {
                                    createInvoiceDate = createInvoiceDateCell.DateCellValue;
                                }
                                else if (createInvoiceDateCell != null && DateTime.TryParse(Convert.ToString(createInvoiceDateCell), out var parsedCreateInvoiceDate))
                                {
                                    createInvoiceDate = parsedCreateInvoiceDate;
                                }
                                var create_invoice_date = createInvoiceDate?.ToString("yyyy-MM-dd") ?? "";

                                // out_account_date
                                var outAccountDateCell = row.GetCell(columnIndices["销账日期"]);
                                DateTime? outAccountDate = null;

                                if (outAccountDateCell != null && outAccountDateCell.CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(outAccountDateCell))
                                {
                                    outAccountDate = outAccountDateCell.DateCellValue;
                                }
                                else if (outAccountDateCell != null && DateTime.TryParse(Convert.ToString(outAccountDateCell), out var parsedOutAccountDate))
                                {
                                    outAccountDate = parsedOutAccountDate;
                                }
                                var out_account_date = outAccountDate?.ToString("yyyy-MM-dd") ?? "";

                                // apply_date
                                var applyDateCell = row.GetCell(columnIndices["申请日"]);
                                DateTime? applyDate = null;

                                if (applyDateCell != null && applyDateCell.CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(applyDateCell))
                                {
                                    applyDate = applyDateCell.DateCellValue;
                                }
                                else if (applyDateCell != null && DateTime.TryParse(Convert.ToString(applyDateCell), out var parsedApplyDate))
                                {
                                    applyDate = parsedApplyDate;
                                }
                                var apply_date = applyDate?.ToString("yyyy-MM-dd") ?? "";

                                // first_draft_date
                                var firstDraftDateCell = row.GetCell(columnIndices["初稿日"]);
                                DateTime? firstDraftDate = null;

                                if (firstDraftDateCell != null && firstDraftDateCell.CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(firstDraftDateCell))
                                {
                                    firstDraftDate = firstDraftDateCell.DateCellValue;
                                }
                                else if (firstDraftDateCell != null && DateTime.TryParse(Convert.ToString(firstDraftDateCell), out var parsedFirstDraftDate))
                                {
                                    firstDraftDate = parsedFirstDraftDate;
                                }
                                var first_draft_date = firstDraftDate?.ToString("yyyy-MM-dd") ?? "";

                                // inside_draft_date
                                var insideDraftDateCell = row.GetCell(columnIndices["内部定稿日"]);
                                DateTime? insideDraftDate = null;

                                if (insideDraftDateCell != null && insideDraftDateCell.CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(insideDraftDateCell))
                                {
                                    insideDraftDate = insideDraftDateCell.DateCellValue;
                                }
                                else if (insideDraftDateCell != null && DateTime.TryParse(Convert.ToString(insideDraftDateCell), out var parsedInsideDraftDate))
                                {
                                    insideDraftDate = parsedInsideDraftDate;
                                }
                                var inside_draft_date = insideDraftDate?.ToString("yyyy-MM-dd") ?? "";

                                // return_draft_date
                                var returnDraftDateCell = row.GetCell(columnIndices["返稿日"]);
                                DateTime? returnDraftDate = null;

                                if (returnDraftDateCell != null && returnDraftDateCell.CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(returnDraftDateCell))
                                {
                                    returnDraftDate = returnDraftDateCell.DateCellValue;
                                }
                                else if (returnDraftDateCell != null && DateTime.TryParse(Convert.ToString(returnDraftDateCell), out var parsedReturnDraftDate))
                                {
                                    returnDraftDate = parsedReturnDraftDate;
                                }
                                var return_draft_date = returnDraftDate?.ToString("yyyy-MM-dd") ?? "";

                                // draft_date
                                var draftDateCell = row.GetCell(columnIndices["定稿日"]);
                                DateTime? draftDate = null;

                                if (draftDateCell != null && draftDateCell.CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(draftDateCell))
                                {
                                    draftDate = draftDateCell.DateCellValue;
                                }
                                else if (draftDateCell != null && DateTime.TryParse(Convert.ToString(draftDateCell), out var parsedDraftDate))
                                {
                                    draftDate = parsedDraftDate;
                                }
                                var draft_date = draftDate?.ToString("yyyy-MM-dd") ?? "";

                                // give_cooperate_date
                                var giveCooperateDateCell = row.GetCell(columnIndices["送合作所日"]);
                                DateTime? giveCooperateDate = null;

                                if (giveCooperateDateCell != null && giveCooperateDateCell.CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(giveCooperateDateCell))
                                {
                                    giveCooperateDate = giveCooperateDateCell.DateCellValue;
                                }
                                else if (giveCooperateDateCell != null && DateTime.TryParse(Convert.ToString(giveCooperateDateCell), out var parsedGiveCooperateDate))
                                {
                                    giveCooperateDate = parsedGiveCooperateDate;
                                }
                                var give_cooperate_date = giveCooperateDate?.ToString("yyyy-MM-dd") ?? "";


                                // submit_date
                                var submit_date = ""; // 根据需求处理






                                // var open_date = Convert.ToString(row.GetCell(columnIndices["开卷日期"]));//row.GetCell(columnIndices["开卷日期"]).CellType != CellType.Blank ? row.GetCell(columnIndices["开卷日期"]).ToString() : "";
                                //var commission_case = Convert.ToString(row.GetCell(columnIndices["委案日期"])); //row.GetCell(columnIndices["委案日期"]).CellType != CellType.Blank ? row.GetCell(columnIndices["委案日期"]).ToString() : "";
                                var area = Convert.ToString(row.GetCell(columnIndices["国家(地区)"])); // row.GetCell(columnIndices["国家(地区)"]).CellType != CellType.Blank ? row.GetCell(columnIndices["国家(地区)"]).ToString() : "";
                                var receipt = Convert.ToString(row.GetCell(columnIndices["收据号"])); //row.GetCell(columnIndices["收据号"]).CellType != CellType.Blank ? row.GetCell(columnIndices["收据号"]).ToString() : "";
                                var contract_no = Convert.ToString(row.GetCell(columnIndices["合同号"])); // row.GetCell(columnIndices["合同号"]).CellType != CellType.Blank ? row.GetCell(columnIndices["合同号"]).ToString() : "";
                                var case_customer_user = Convert.ToString(row.GetCell(columnIndices["案件客户联系人"])); // row.GetCell(columnIndices["案件客户联系人"]).CellType != CellType.Blank ? row.GetCell(columnIndices["案件客户联系人"]).ToString() : "";
                                var case_user_phone = Convert.ToString(row.GetCell(columnIndices["案件客户联系人电话"])); //  row.GetCell(columnIndices["案件客户联系人"]).CellType != CellType.Blank ? row.GetCell(columnIndices["案件客户联系人电话"]).ToString() : "";
                                var case_user_email = Convert.ToString(row.GetCell(columnIndices["案件客户联系人邮箱"])); // row.GetCell(columnIndices["案件客户联系人邮箱"]).CellType != CellType.Blank ? row.GetCell(columnIndices["案件客户联系人邮箱"]).ToString() : "";
                                var assist_house_no = Convert.ToString(row.GetCell(columnIndices["协办所案号"])); //  row.GetCell(columnIndices["协办所案号"]).CellType != CellType.Blank ? row.GetCell(columnIndices["协办所案号"]).ToString() : "";
                                var register_no = Convert.ToString(row.GetCell(columnIndices["注册号"])); // row.GetCell(columnIndices["注册号"]).CellType != CellType.Blank ? row.GetCell(columnIndices["注册号"]).ToString() : "";
                                var brand_category = Convert.ToString(row.GetCell(columnIndices["商标类别"]));// row.GetCell(columnIndices["商标类别"]).CellType != CellType.Blank ? row.GetCell(columnIndices["商标类别"]).ToString() : "";
                                var case_handle_dept = Convert.ToString(row.GetCell(columnIndices["案件承办部门"]));// row.GetCell(columnIndices["案件承办部门"]).CellType != CellType.Blank ? row.GetCell(columnIndices["案件承办部门"]).ToString() : "";
                                var belong_branch = Convert.ToString(row.GetCell(columnIndices["所属分部"]));//row.GetCell(columnIndices["所属分部"]).CellType != CellType.Blank ? row.GetCell(columnIndices["所属分部"]).ToString() : "";
                                var work_type = Convert.ToString(row.GetCell(columnIndices["业务类型"]));// row.GetCell(columnIndices["业务类型"]).CellType != CellType.Blank ? row.GetCell(columnIndices["业务类型"]).ToString() : "";
                                var case_direction = Convert.ToString(row.GetCell(columnIndices["案件流向"])); //row.GetCell(columnIndices["案件流向"]).CellType != CellType.Blank ? row.GetCell(columnIndices["案件流向"]).ToString() : "";
                                var case_user_deptment = Convert.ToString(row.GetCell(columnIndices["案件处理人部门"])); // row.GetCell(columnIndices["案件处理人部门"]).CellType != CellType.Blank ? row.GetCell(columnIndices["案件处理人部门"]).ToString() : "";
                                var apply_tpye = Convert.ToString(row.GetCell(columnIndices["申请类型"])); //  row.GetCell(columnIndices["申请类型"]).CellType != CellType.Blank ? row.GetCell(columnIndices["申请类型"]).ToString() : "";
                                var apply_no = Convert.ToString(row.GetCell(columnIndices["申请号"])); // row.GetCell(columnIndices["申请号"]).CellType != CellType.Blank ? row.GetCell(columnIndices["申请号"]).ToString() : "";
                                //var apply_date = Convert.ToString(row.GetCell(columnIndices["申请日"])); // row.GetCell(columnIndices["申请日"]).CellType != CellType.Blank ? row.GetCell(columnIndices["申请日"]).ToString() : "";
                                var registration_no = Convert.ToString(row.GetCell(columnIndices["注册号"])); // row.GetCell(columnIndices["注册号"]).CellType != CellType.Blank ? row.GetCell(columnIndices["注册号"]).ToString() : "";
                                var announcement_date = Convert.ToString(row.GetCell(columnIndices["公告日"])); // row.GetCell(columnIndices["公告日"]).CellType != CellType.Blank ? row.GetCell(columnIndices["公告日"]).ToString() : "";
                                var new_submit_date = Convert.ToString(row.GetCell(columnIndices["新申请递交日"])); // row.GetCell(columnIndices["新申请递交日"]).CellType != CellType.Blank ? row.GetCell(columnIndices["新申请递交日"]).ToString() : "";
                                var case_remark = Convert.ToString(row.GetCell(columnIndices["案件备注"])); // row.GetCell(columnIndices["案件备注"]).CellType != CellType.Blank ? row.GetCell(columnIndices["案件备注"]).ToString() : "";
                                var is_advance = Convert.ToString(row.GetCell(columnIndices["是否提前公开"])); //row.GetCell(columnIndices["是否提前公开"]).CellType != CellType.Blank ? row.GetCell(columnIndices["是否提前公开"]).ToString() : "";
                                var reduce_proportion = Convert.ToString(row.GetCell(columnIndices["费减比例"])); // row.GetCell(columnIndices["费减比例"]).CellType != CellType.Blank ? row.GetCell(columnIndices["费减比例"]).ToString() : "";
                                var pay_no = Convert.ToString(row.GetCell(columnIndices["缴费单号"])); //row.GetCell(columnIndices["缴费单号"]).CellType != CellType.Blank ? row.GetCell(columnIndices["缴费单号"]).ToString() : "";
                                var pct_apply_no = Convert.ToString(row.GetCell(columnIndices["PCT申请号"])); //row.GetCell(columnIndices["PCT申请号"]).CellType != CellType.Blank ? row.GetCell(columnIndices["PCT申请号"]).ToString() : "";
                                var pct_apply_date = Convert.ToString(row.GetCell(columnIndices["PCT申请日"])); //row.GetCell(columnIndices["PCT申请日"]).CellType != CellType.Blank ? row.GetCell(columnIndices["PCT申请日"]).ToString() : "";
                                var priority_date = Convert.ToString(row.GetCell(columnIndices["优先权日"])); //row.GetCell(columnIndices["优先权日"]).CellType != CellType.Blank ? row.GetCell(columnIndices["优先权日"]).ToString() : "";
                                var customer_category = Convert.ToString(row.GetCell(columnIndices["客户类别"])); //row.GetCell(columnIndices["客户类别"]).CellType != CellType.Blank ? row.GetCell(columnIndices["客户类别"]).ToString() : "";
                                var handle_event = Convert.ToString(row.GetCell(columnIndices["处理事项"])); //row.GetCell(columnIndices["处理事项"]).CellType != CellType.Blank ? row.GetCell(columnIndices["处理事项"]).ToString() : "";
                                var handle_event_stage = Convert.ToString(row.GetCell(columnIndices["处理事项(含阶段)"])); // row.GetCell(columnIndices["处理事项(含阶段)"]).CellType != CellType.Blank ? row.GetCell(columnIndices["处理事项(含阶段)"]).ToString() : "";
                                var handle_event_user = Convert.ToString(row.GetCell(columnIndices["处理事项处理人"])); // row.GetCell(columnIndices["处理事项处理人"]).CellType != CellType.Blank ? row.GetCell(columnIndices["处理事项处理人"]).ToString() : "";
                                var handle_event_deptment = Convert.ToString(row.GetCell(columnIndices["处理事项处理人部门"])); //row.GetCell(columnIndices["处理事项处理人部门"]).CellType != CellType.Blank ? row.GetCell(columnIndices["处理事项处理人部门"]).ToString() : "";
                                var event_remark = Convert.ToString(row.GetCell(columnIndices["事项备注"])); //row.GetCell(columnIndices["事项备注"]).CellType != CellType.Blank ? row.GetCell(columnIndices["事项备注"]).ToString() : "";
                                //var handle_event_date = Convert.ToString(row.GetCell(columnIndices["处理事项完成日"])); //row.GetCell(columnIndices["处理事项完成日"]).CellType != CellType.Blank ? row.GetCell(columnIndices["处理事项完成日"]).ToString() : "";
                                //var first_draft_date = Convert.ToString(row.GetCell(columnIndices["初稿日"])); //row.GetCell(columnIndices["初稿日"]).CellType != CellType.Blank ? row.GetCell(columnIndices["初稿日"]).ToString() : "";
                                //var inside_draft_date = Convert.ToString(row.GetCell(columnIndices["内部定稿日"])); // row.GetCell(columnIndices["内部定稿日"]).CellType != CellType.Blank ? row.GetCell(columnIndices["内部定稿日"]).ToString() : "";
                                //var return_draft_date = Convert.ToString(row.GetCell(columnIndices["返稿日"])); //row.GetCell(columnIndices["返稿日"]).CellType != CellType.Blank ? row.GetCell(columnIndices["返稿日"]).ToString() : "";
                                //var draft_date = Convert.ToString(row.GetCell(columnIndices["定稿日"])); // row.GetCell(columnIndices["定稿日"]).CellType != CellType.Blank ? row.GetCell(columnIndices["定稿日"]).ToString() : "";
                                //var give_cooperate_date = Convert.ToString(row.GetCell(columnIndices["送合作所日"])); // row.GetCell(columnIndices["送合作所日"]).CellType != CellType.Blank ? row.GetCell(columnIndices["送合作所日"]).ToString() : "";
                                //var submit_date = "";
                                var handle_event_status = Convert.ToString(row.GetCell(columnIndices["处理事项状态"])); // row.GetCell(columnIndices["处理事项状态"]).CellType != CellType.Blank ? row.GetCell(columnIndices["处理事项状态"]).ToString() : "";
                                //var official_date = Convert.ToString(row.GetCell(columnIndices["官方期限"])); //row.GetCell(columnIndices["官方期限"]).CellType != CellType.Blank ? row.GetCell(columnIndices["官方期限"]).ToString() : "";
                                var inside_draft_date_two = Convert.ToString(row.GetCell(columnIndices["内部定稿日"])); //row.GetCell(columnIndices["内部定稿日"]).CellType != CellType.Blank ? row.GetCell(columnIndices["内部定稿日"]).ToString() : "";
                                //var customer_date = Convert.ToString(row.GetCell(columnIndices["客户期限"])); //row.GetCell(columnIndices["客户期限"]).CellType != CellType.Blank ? row.GetCell(columnIndices["客户期限"]).ToString() : "";
                                var apply_user = Convert.ToString(row.GetCell(columnIndices["申请人"])); //row.GetCell(columnIndices["申请人"]).CellType != CellType.Blank ? row.GetCell(columnIndices["申请人"]).ToString() : "";
                                var agent_mechanism = Convert.ToString(row.GetCell(columnIndices["代理机构"])); // row.GetCell(columnIndices["代理机构"]).CellType != CellType.Blank ? row.GetCell(columnIndices["代理机构"]).ToString() : "";
                                var invention_user = Convert.ToString(row.GetCell(columnIndices["发明人"])); //row.GetCell(columnIndices["发明人"]).CellType != CellType.Blank ? row.GetCell(columnIndices["发明人"]).ToString() : "";
                                var money_type = Convert.ToString(row.GetCell(columnIndices["费用类型"])); // row.GetCell(columnIndices["费用类型"]).CellType != CellType.Blank ? row.GetCell(columnIndices["费用类型"]).ToString() : "";
                                var money_name = Convert.ToString(row.GetCell(columnIndices["费用名称"])); //row.GetCell(columnIndices["费用名称"]).CellType != CellType.Blank ? row.GetCell(columnIndices["费用名称"]).ToString() : "";
                                var money_description = Convert.ToString(row.GetCell(columnIndices["费用描述"]));// row.GetCell(columnIndices["费用描述"]).CellType != CellType.Blank ? row.GetCell(columnIndices["费用描述"]).ToString() : "";
                                var money_english = Convert.ToString(row.GetCell(columnIndices["费用描述英文"]));//row.GetCell(columnIndices["费用描述英文"]).CellType != CellType.Blank ? row.GetCell(columnIndices["费用描述英文"]).ToString() : "";
                                var price = Convert.ToString(row.GetCell(columnIndices["金额"]));// row.GetCell(columnIndices["金额"]).CellType != CellType.Blank ? row.GetCell(columnIndices["金额"]).ToString() : "";
                                var currency = Convert.ToString(row.GetCell(columnIndices["币别"]));//row.GetCell(columnIndices["币别"]).CellType != CellType.Blank ? row.GetCell(columnIndices["币别"]).ToString() : "";
                                var change_currency = Convert.ToString(row.GetCell(columnIndices["换算后币别"])); //row.GetCell(columnIndices["换算后币别"]).CellType != CellType.Blank ? row.GetCell(columnIndices["换算后币别"]).ToString() : "";
                                var change_money = Convert.ToString(row.GetCell(columnIndices["换算后金额"])); //row.GetCell(columnIndices["换算后金额"]).CellType != CellType.Blank ? row.GetCell(columnIndices["换算后金额"]).ToString() : "";
                                var receivable_date = Convert.ToString(row.GetCell(columnIndices["应收日期"])); // row.GetCell(columnIndices["应收日期"]).CellType != CellType.Blank ? row.GetCell(columnIndices["应收日期"]).ToString() : "";
                                //var request_date = Convert.ToString(row.GetCell(columnIndices["请款日期"])); //row.GetCell(columnIndices["请款日期"]).CellType != CellType.Blank ? row.GetCell(columnIndices["请款日期"]).ToString() : "";
                                //var actual_date = Convert.ToString(row.GetCell(columnIndices["实收日期"])); //row.GetCell(columnIndices["实收日期"]).CellType != CellType.Blank ? row.GetCell(columnIndices["实收日期"]).ToString() : "";
                                var actual_get_money = Convert.ToString(row.GetCell(columnIndices["实收/实付金额"])); //row.GetCell(columnIndices["实收/实付金额"]).CellType != CellType.Blank ? row.GetCell(columnIndices["实收/实付金额"]).ToString() : "";
                                var actual_currency = Convert.ToString(row.GetCell(columnIndices["实收币别"])); // row.GetCell(columnIndices["实收币别"]).CellType != CellType.Blank ? row.GetCell(columnIndices["实收币别"]).ToString() : "";
                                var pay_date = Convert.ToString(row.GetCell(columnIndices["缴费期限"])); //row.GetCell(columnIndices["缴费期限"]).CellType != CellType.Blank ? row.GetCell(columnIndices["缴费期限"]).ToString() : "";
                                var pay_head = Convert.ToString(row.GetCell(columnIndices["缴费抬头"])); // row.GetCell(columnIndices["缴费抬头"]).CellType != CellType.Blank ? row.GetCell(columnIndices["缴费抬头"]).ToString() : "";
                                var actual_pay_date = Convert.ToString(row.GetCell(columnIndices["实付日期"])); //row.GetCell(columnIndices["实付日期"]).CellType != CellType.Blank ? row.GetCell(columnIndices["实付日期"]).ToString() : "";
                                var request_money_no = Convert.ToString(row.GetCell(columnIndices["请款单号"]));// row.GetCell(columnIndices["请款单号"]).CellType != CellType.Blank ? row.GetCell(columnIndices["请款单号"]).ToString() : "";
                                var pay_year_stage = Convert.ToString(row.GetCell(columnIndices["缴年费阶段"]));//row.GetCell(columnIndices["缴年费阶段"]).CellType != CellType.Blank ? row.GetCell(columnIndices["缴年费阶段"]).ToString() : "";
                                var pay_type = Convert.ToString(row.GetCell(columnIndices["缴费类型"]));// row.GetCell(columnIndices["缴费类型"]).CellType != CellType.Blank ? row.GetCell(columnIndices["缴费类型"]).ToString() : "";
                                var exchange_rate = Convert.ToString(row.GetCell(columnIndices["汇率"]));// row.GetCell(columnIndices["汇率"]).CellType != CellType.Blank ? row.GetCell(columnIndices["汇率"]).ToString() : "";
                                var money_remark = Convert.ToString(row.GetCell(columnIndices["费用备注"]));//row.GetCell(columnIndices["费用备注"]).CellType != CellType.Blank ? row.GetCell(columnIndices["费用备注"]).ToString() : "";
                                var update_record = Convert.ToString(row.GetCell(columnIndices["修改记录"]));// row.GetCell(columnIndices["修改记录"]).CellType != CellType.Blank ? row.GetCell(columnIndices["修改记录"]).ToString() : "";
                                var collection_account = Convert.ToString(row.GetCell(columnIndices["收款公司账号"]));// row.GetCell(columnIndices["收款公司账号"]).CellType != CellType.Blank ? row.GetCell(columnIndices["收款公司账号"]).ToString() : "";
                                var bill_no = Convert.ToString(row.GetCell(columnIndices["外方账单号"]));//row.GetCell(columnIndices["外方账单号"]).CellType != CellType.Blank ? row.GetCell(columnIndices["外方账单号"]).ToString() : "";
                                //var money_create_date = Convert.ToString(row.GetCell(columnIndices["费用创建日期"]));//row.GetCell(columnIndices["费用创建日期"]).CellType != CellType.Blank ? row.GetCell(columnIndices["费用创建日期"]).ToString() : "";
                                var invoice_no = Convert.ToString(row.GetCell(columnIndices["发票号"]));//row.GetCell(columnIndices["发票号"]).CellType != CellType.Blank ? row.GetCell(columnIndices["发票号"]).ToString() : "";
                                //var create_invoice_date = Convert.ToString(row.GetCell(columnIndices["开票日期"]));// row.GetCell(columnIndices["开票日期"]).CellType != CellType.Blank ? row.GetCell(columnIndices["开票日期"]).ToString() : "";
                                var cost_agent = Convert.ToString(row.GetCell(columnIndices["费用合作所代理机构"]));// row.GetCell(columnIndices["费用合作所代理机构"]).CellType != CellType.Blank ? row.GetCell(columnIndices["费用合作所代理机构"]).ToString() : "";
                                var collection_status = Convert.ToString(row.GetCell(columnIndices["收款状态"]));//row.GetCell(columnIndices["收款状态"]).CellType != CellType.Blank ? row.GetCell(columnIndices["收款状态"]).ToString() : "";
                                var pay_status = Convert.ToString(row.GetCell(columnIndices["缴费状态"]));//row.GetCell(columnIndices["缴费状态"]).CellType != CellType.Blank ? row.GetCell(columnIndices["缴费状态"]).ToString() : "";
                                //var out_account_date = Convert.ToString(row.GetCell(columnIndices["销账日期"]));// row.GetCell(columnIndices["销账日期"]).CellType != CellType.Blank ? row.GetCell(columnIndices["销账日期"]).ToString() : "";
                                var power_ask_num = Convert.ToString(row.GetCell(columnIndices["权利要求项数"]));//row.GetCell(columnIndices["权利要求项数"]).CellType != CellType.Blank ? row.GetCell(columnIndices["权利要求项数"]).ToString() : "";
                                var instructions_num = Convert.ToString(row.GetCell(columnIndices["说明书页数"]));//row.GetCell(columnIndices["说明书页数"]).CellType != CellType.Blank ? row.GetCell(columnIndices["说明书页数"]).ToString() : "";
                                var technical_field = Convert.ToString(row.GetCell(columnIndices["技术领域"]));//row.GetCell(columnIndices["技术领域"]).CellType != CellType.Blank ? row.GetCell(columnIndices["技术领域"]).ToString() : "";
                                // 根据 Excel 中的列和你的数据模型进行映射
                                var outside_user = Convert.ToString(row.GetCell(columnIndices["外部案源人"]));// row.GetCell(columnIndices["外部案源人"]).CellType != CellType.Blank ? row.GetCell(columnIndices["外部案源人"]).ToString() : "";
                                var inside_user = Convert.ToString(row.GetCell(columnIndices["内部案源人"]));//row.GetCell(columnIndices["内部案源人"]).CellType != CellType.Blank ? row.GetCell(columnIndices["内部案源人"]).ToString() : "";
                                var patent_label = Convert.ToString(row.GetCell(columnIndices["专利标签"]));//row.GetCell(columnIndices["专利标签"]).CellType != CellType.Blank ? row.GetCell(columnIndices["专利标签"]).ToString() : "";
                                var proposal_no = Convert.ToString(row.GetCell(columnIndices["提案编号"]));// row.GetCell(columnIndices["提案编号"]).CellType != CellType.Blank ? row.GetCell(columnIndices["提案编号"]).ToString() : "";
                                var proxy_user = Convert.ToString(row.GetCell(columnIndices["代理人"]));//row.GetCell(columnIndices["代理人"]).CellType != CellType.Blank ? row.GetCell(columnIndices["代理人"]).ToString() : "";
                                var custom_one = Convert.ToString(row.GetCell(columnIndices["自定义栏位3"]));//row.GetCell(columnIndices["自定义栏位3"]).CellType != CellType.Blank ? row.GetCell(columnIndices["自定义栏位3"]).ToString() : "";
                                var custom_two = Convert.ToString(row.GetCell(columnIndices["自定义栏位4"]));//row.GetCell(columnIndices["自定义栏位4"]).CellType != CellType.Blank ? row.GetCell(columnIndices["自定义栏位4"]).ToString() : "";
                                var custom_three = Convert.ToString(row.GetCell(columnIndices["自定义栏位5"]));//row.GetCell(columnIndices["自定义栏位5"]).CellType != CellType.Blank ? row.GetCell(columnIndices["自定义栏位5"]).ToString() : "";
                                var text1 = Convert.ToString(row.GetCell(columnIndices["文本1"]));//row.GetCell(columnIndices["文本1"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本1"]).ToString() : "";
                                var text2 = Convert.ToString(row.GetCell(columnIndices["文本2"]));//row.GetCell(columnIndices["文本2"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本2"]).ToString() : "";
                                var text3 = Convert.ToString(row.GetCell(columnIndices["文本3"]));//row.GetCell(columnIndices["文本3"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本3"]).ToString() : "";
                                var text4 = Convert.ToString(row.GetCell(columnIndices["文本4"]));//row.GetCell(columnIndices["文本4"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本4"]).ToString() : "";
                                var text5 = Convert.ToString(row.GetCell(columnIndices["文本5"]));//row.GetCell(columnIndices["文本5"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本5"]).ToString() : "";
                                var text6 = Convert.ToString(row.GetCell(columnIndices["文本6"]));//row.GetCell(columnIndices["文本6"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本6"]).ToString() : "";
                                var text7 = Convert.ToString(row.GetCell(columnIndices["文本7"]));// Convert.ToString(row.GetCell(columnIndices["请款单号"]));//row.GetCell(columnIndices["文本7"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本7"]).ToString() : "";
                                var text8 = Convert.ToString(row.GetCell(columnIndices["文本8"]));//row.GetCell(columnIndices["文本8"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本8"]).ToString() : "";
                                var text9 = Convert.ToString(row.GetCell(columnIndices["文本9"]));//row.GetCell(columnIndices["文本9"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本9"]).ToString() : "";
                                var text10 = Convert.ToString(row.GetCell(columnIndices["文本10"]));//row.GetCell(columnIndices["文本10"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本10"]).ToString() : "";
                                var text11 = Convert.ToString(row.GetCell(columnIndices["文本11"]));//row.GetCell(columnIndices["文本11"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本11"]).ToString() : "";
                                var text12 = Convert.ToString(row.GetCell(columnIndices["文本12"]));//row.GetCell(columnIndices["文本12"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本12"]).ToString() : "";
                                var text13 = Convert.ToString(row.GetCell(columnIndices["文本13"]));//row.GetCell(columnIndices["文本13"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本13"]).ToString() : "";
                                var text14 = Convert.ToString(row.GetCell(columnIndices["文本14"]));//row.GetCell(columnIndices["文本14"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本14"]).ToString() : "";
                                var text15 = Convert.ToString(row.GetCell(columnIndices["文本15"]));// Convert.ToString(row.GetCell(columnIndices["请款单号"]));//row.GetCell(columnIndices["文本15"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本15"]).ToString() : "";
                                var text16 = Convert.ToString(row.GetCell(columnIndices["文本16"]));//row.GetCell(columnIndices["文本16"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本16"]).ToString() : "";
                                var text17 = Convert.ToString(row.GetCell(columnIndices["文本17"]));//row.GetCell(columnIndices["文本17"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本17"]).ToString() : "";
                                var text18 = Convert.ToString(row.GetCell(columnIndices["文本18"]));//row.GetCell(columnIndices["文本18"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本18"]).ToString() : "";
                                var text19 = Convert.ToString(row.GetCell(columnIndices["文本19"]));//row.GetCell(columnIndices["文本19"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本19"]).ToString() : "";
                                var text20 = Convert.ToString(row.GetCell(columnIndices["文本20"]));//row.GetCell(columnIndices["文本20"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本20"]).ToString() : "";
                                var text21 = Convert.ToString(row.GetCell(columnIndices["文本21"]));//row.GetCell(columnIndices["文本21"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本21"]).ToString() : "";
                                var text22 = Convert.ToString(row.GetCell(columnIndices["文本22"]));//row.GetCell(columnIndices["文本22"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本22"]).ToString() : "";
                                var text23 = Convert.ToString(row.GetCell(columnIndices["文本23"]));// row.GetCell(columnIndices["文本23"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本23"]).ToString() : "";
                                var text24 = Convert.ToString(row.GetCell(columnIndices["文本24"]));//row.GetCell(columnIndices["文本24"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本24"]).ToString() : "";
                                var text25 = Convert.ToString(row.GetCell(columnIndices["文本25"]));//row.GetCell(columnIndices["文本25"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本25"]).ToString() : "";
                                var text26 = Convert.ToString(row.GetCell(columnIndices["文本26"]));//row.GetCell(columnIndices["文本26"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本26"]).ToString() : "";
                                var text27 = Convert.ToString(row.GetCell(columnIndices["文本27"]));//row.GetCell(columnIndices["文本27"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本27"]).ToString() : "";
                                var text28 = Convert.ToString(row.GetCell(columnIndices["文本28"]));//row.GetCell(columnIndices["文本28"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本28"]).ToString() : "";
                                var text29 = Convert.ToString(row.GetCell(columnIndices["文本29"]));//row.GetCell(columnIndices["文本29"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本29"]).ToString() : "";
                                var text30 = Convert.ToString(row.GetCell(columnIndices["文本30"]));//row.GetCell(columnIndices["文本30"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本30"]).ToString() : "";
                                Sli_intpo_Import.Add ( new sli_intpo_Import
                                {
                                    // 根据 Excel 中的列和你的数据模型进行映射
                                    my_document_no = my_document_no,// row.GetCell(columnIndices["我方文号"]).CellType != CellType.Blank ? row.GetCell(columnIndices["我方文号"]).ToString() : "",
                                    case_name = case_name,// row.GetCell(columnIndices["案件名称"]).CellType != CellType.Blank ? row.GetCell(columnIndices["案件名称"]).ToString() : "",
                                    case_status = case_status,// row.GetCell(columnIndices["案件状态"]).CellType != CellType.Blank ? row.GetCell(columnIndices["案件状态"]).ToString() : "",
                                    case_type = case_type,// row.GetCell(columnIndices["案件类型"]).CellType != CellType.Blank ? row.GetCell(columnIndices["案件类型"]).ToString() : "",
                                    customer_document_no = customer_document_no,//row.GetCell(columnIndices["客户文号"]).CellType != CellType.Blank ? row.GetCell(columnIndices["客户文号"]).ToString() : "",
                                    customer_name = customer_name,// row.GetCell(columnIndices["客户名称"]).CellType != CellType.Blank ? row.GetCell(columnIndices["客户名称"]).ToString() : "",
                                    customer_no = customer_no,// row.GetCell(columnIndices["客户代码"]).CellType != CellType.Blank ? row.GetCell(columnIndices["客户代码"]).ToString() : "",
                                    product_type = product_type,// row.GetCell(columnIndices["产品类别"]).CellType != CellType.Blank ? row.GetCell(columnIndices["产品类别"]).ToString() : "",
                                    case_user = case_user,// row.GetCell(columnIndices["案件处理人"]).CellType != CellType.Blank ? row.GetCell(columnIndices["案件处理人"]).ToString() : "",
                                    date_apply = date_apply,// row.GetCell(columnIndices["同日申请"]).CellType != CellType.Blank ? row.GetCell(columnIndices["同日申请"]).ToString() : "",
                                    date_submit = date_submit,// row.GetCell(columnIndices["同日递交"]).CellType != CellType.Blank ? row.GetCell(columnIndices["同日递交"]).ToString() : "",
                                    is_date_submit = is_date_submit,// row.GetCell(columnIndices["是否同时提实审"]).CellType != CellType.Blank ? row.GetCell(columnIndices["是否同时提实审"]).ToString() : "",
                                    is_publish = is_publish,// row.GetCell(columnIndices["是否提前公布"]).CellType != CellType.Blank ? row.GetCell(columnIndices["是否提前公布"]).ToString() : "",
                                    is_reduce = is_reduce,// row.GetCell(columnIndices["是否请求费用减缓"]).CellType != CellType.Blank ? row.GetCell(columnIndices["是否请求费用减缓"]).ToString() : "",
                                    is_first = is_first,// row.GetCell(columnIndices["是否优先审查"]).CellType != CellType.Blank ? row.GetCell(columnIndices["是否优先审查"]).ToString() : "",
                                    is_das = is_das,// row.GetCell(columnIndices["是否同时请求DAS码"]).CellType != CellType.Blank ? row.GetCell(columnIndices["是否同时请求DAS码"]).ToString() : "",
                                    is_preview = is_preview,// row.GetCell(columnIndices["是否预审案件"]).CellType != CellType.Blank ? row.GetCell(columnIndices["是否预审案件"]).ToString() : "",
                                    is_fast_case = is_fast_case,// row.GetCell(columnIndices["是否是快维案件"]).CellType != CellType.Blank ? row.GetCell(columnIndices["是否是快维案件"]).ToString() : "",
                                    business_user = business_user,//row.GetCell(columnIndices["业务人员"]).CellType != CellType.Blank ? row.GetCell(columnIndices["业务人员"]).ToString() : "",
                                    business_deputy = business_deputy,// row.GetCell(columnIndices["业务助理"]).CellType != CellType.Blank ? row.GetCell(columnIndices["业务助理"]).ToString() : "",
                                    business_deptment = business_deptment,// row.GetCell(columnIndices["业务部门"]).CellType != CellType.Blank ? row.GetCell(columnIndices["业务部门"]).ToString() : "",
                                    open_date = open_date,// row.GetCell(columnIndices["开卷日期"]).CellType != CellType.Blank ? row.GetCell(columnIndices["开卷日期"]).ToString() : "",
                                    commission_case = commission_case,//row.GetCell(columnIndices["委案日期"]).CellType != CellType.Blank ? row.GetCell(columnIndices["委案日期"]).ToString() : "",
                                    area = area,// row.GetCell(columnIndices["国家(地区)"]).CellType != CellType.Blank ? row.GetCell(columnIndices["国家(地区)"]).ToString() : "",
                                    receipt = receipt,//row.GetCell(columnIndices["收据号"]).CellType != CellType.Blank ? row.GetCell(columnIndices["收据号"]).ToString() : "",
                                    contract_no = contract_no,// row.GetCell(columnIndices["合同号"]).CellType != CellType.Blank ? row.GetCell(columnIndices["合同号"]).ToString() : "",
                                    case_customer_user = case_customer_user,// row.GetCell(columnIndices["案件客户联系人"]).CellType != CellType.Blank ? row.GetCell(columnIndices["案件客户联系人"]).ToString() : "",
                                    case_user_phone = case_user_phone,// row.GetCell(columnIndices["案件客户联系人电话"]).CellType != CellType.Blank ? row.GetCell(columnIndices["案件客户联系人电话"]).ToString() : "",
                                    case_user_email = case_user_email,// row.GetCell(columnIndices["案件客户联系人邮箱"]).CellType != CellType.Blank ? row.GetCell(columnIndices["案件客户联系人邮箱"]).ToString() : "",
                                    assist_house_no = assist_house_no,// row.GetCell(columnIndices["协办所案号"]).CellType != CellType.Blank ? row.GetCell(columnIndices["协办所案号"]).ToString() : "",
                                    register_no = register_no,// row.GetCell(columnIndices["注册号"]).CellType != CellType.Blank ? row.GetCell(columnIndices["注册号"]).ToString() : "",
                                    brand_category = brand_category,// row.GetCell(columnIndices["商标类别"]).CellType != CellType.Blank ? row.GetCell(columnIndices["商标类别"]).ToString() : "",
                                    case_handle_dept = case_handle_dept,// row.GetCell(columnIndices["案件承办部门"]).CellType != CellType.Blank ? row.GetCell(columnIndices["案件承办部门"]).ToString() : "",
                                    belong_branch = belong_branch,// row.GetCell(columnIndices["所属分部"]).CellType != CellType.Blank ? row.GetCell(columnIndices["所属分部"]).ToString() : "",
                                    work_type = work_type,// row.GetCell(columnIndices["业务类型"]).CellType != CellType.Blank ? row.GetCell(columnIndices["业务类型"]).ToString() : "",
                                    case_direction = case_direction,// row.GetCell(columnIndices["案件流向"]).CellType != CellType.Blank ? row.GetCell(columnIndices["案件流向"]).ToString() : "",
                                    case_user_deptment = case_user_deptment,// row.GetCell(columnIndices["案件处理人部门"]).CellType != CellType.Blank ? row.GetCell(columnIndices["案件处理人部门"]).ToString() : "",
                                    apply_tpye = apply_tpye,//row.GetCell(columnIndices["申请类型"]).CellType != CellType.Blank ? row.GetCell(columnIndices["申请类型"]).ToString() : "",
                                    apply_no = apply_no,// row.GetCell(columnIndices["申请号"]).CellType != CellType.Blank ? row.GetCell(columnIndices["申请号"]).ToString() : "",
                                    apply_date = apply_date,// row.GetCell(columnIndices["申请日"]).CellType != CellType.Blank ? row.GetCell(columnIndices["申请日"]).ToString() : "",
                                    registration_no = registration_no,// row.GetCell(columnIndices["注册号"]).CellType != CellType.Blank ? row.GetCell(columnIndices["注册号"]).ToString() : "",
                                    announcement_date = announcement_date,// row.GetCell(columnIndices["公告日"]).CellType != CellType.Blank ? row.GetCell(columnIndices["公告日"]).ToString() : "",
                                    new_submit_date = new_submit_date,// row.GetCell(columnIndices["新申请递交日"]).CellType != CellType.Blank ? row.GetCell(columnIndices["新申请递交日"]).ToString() : "",
                                    case_remark = case_remark,// row.GetCell(columnIndices["案件备注"]).CellType != CellType.Blank ? row.GetCell(columnIndices["案件备注"]).ToString() : "",
                                    is_advance = is_advance,// row.GetCell(columnIndices["是否提前公开"]).CellType != CellType.Blank ? row.GetCell(columnIndices["是否提前公开"]).ToString() : "",
                                    reduce_proportion = reduce_proportion,// row.GetCell(columnIndices["费减比例"]).CellType != CellType.Blank ? row.GetCell(columnIndices["费减比例"]).ToString() : "",
                                    pay_no = pay_no,// row.GetCell(columnIndices["缴费单号"]).CellType != CellType.Blank ? row.GetCell(columnIndices["缴费单号"]).ToString() : "",
                                    pct_apply_no = pct_apply_no,// row.GetCell(columnIndices["PCT申请号"]).CellType != CellType.Blank ? row.GetCell(columnIndices["PCT申请号"]).ToString() : "",
                                    pct_apply_date = pct_apply_date,// row.GetCell(columnIndices["PCT申请日"]).CellType != CellType.Blank ? row.GetCell(columnIndices["PCT申请日"]).ToString() : "",
                                    priority_date = priority_date,// row.GetCell(columnIndices["优先权日"]).CellType != CellType.Blank ? row.GetCell(columnIndices["优先权日"]).ToString() : "",
                                    customer_category = customer_category,// row.GetCell(columnIndices["客户类别"]).CellType != CellType.Blank ? row.GetCell(columnIndices["客户类别"]).ToString() : "",
                                    handle_event = handle_event,// row.GetCell(columnIndices["处理事项"]).CellType != CellType.Blank ? row.GetCell(columnIndices["处理事项"]).ToString() : "",
                                    handle_event_stage = handle_event_stage,// row.GetCell(columnIndices["处理事项(含阶段)"]).CellType != CellType.Blank ? row.GetCell(columnIndices["处理事项(含阶段)"]).ToString() : "",
                                    handle_event_user = handle_event_user,// row.GetCell(columnIndices["处理事项处理人"]).CellType != CellType.Blank ? row.GetCell(columnIndices["处理事项处理人"]).ToString() : "",
                                    handle_event_deptment = handle_event_deptment,// row.GetCell(columnIndices["处理事项处理人部门"]).CellType != CellType.Blank ? row.GetCell(columnIndices["处理事项处理人部门"]).ToString() : "",
                                    event_remark = event_remark,// row.GetCell(columnIndices["事项备注"]).CellType != CellType.Blank ? row.GetCell(columnIndices["事项备注"]).ToString() : "",
                                    handle_event_date = handle_event_date,// row.GetCell(columnIndices["处理事项完成日"]).CellType != CellType.Blank ? row.GetCell(columnIndices["处理事项完成日"]).ToString() : "",
                                    first_draft_date = first_draft_date,// row.GetCell(columnIndices["初稿日"]).CellType != CellType.Blank ? row.GetCell(columnIndices["初稿日"]).ToString() : "",
                                    inside_draft_date = inside_draft_date,// row.GetCell(columnIndices["内部定稿日"]).CellType != CellType.Blank ? row.GetCell(columnIndices["内部定稿日"]).ToString() : "",
                                    return_draft_date = return_draft_date,//row.GetCell(columnIndices["返稿日"]).CellType != CellType.Blank ? row.GetCell(columnIndices["返稿日"]).ToString() : "",
                                    draft_date = draft_date,// row.GetCell(columnIndices["定稿日"]).CellType != CellType.Blank ? row.GetCell(columnIndices["定稿日"]).ToString() : "",
                                    give_cooperate_date = give_cooperate_date,// row.GetCell(columnIndices["送合作所日"]).CellType != CellType.Blank ? row.GetCell(columnIndices["送合作所日"]).ToString() : "",
                                    submit_date = submit_date,// row.GetCell(columnIndices[""]).CellType != CellType.Blank ? row.GetCell(columnIndices[""]).ToString() : "",
                                    handle_event_status = handle_event_status,// row.GetCell(columnIndices["处理事项状态"]).CellType != CellType.Blank ? row.GetCell(columnIndices["处理事项状态"]).ToString() : "",
                                    official_date = official_date,// row.GetCell(columnIndices["官方期限"]).CellType != CellType.Blank ? row.GetCell(columnIndices["官方期限"]).ToString() : "",
                                    inside_draft_date_two = inside_draft_date_two,// row.GetCell(columnIndices["内部定稿日"]).CellType != CellType.Blank ? row.GetCell(columnIndices["内部定稿日"]).ToString() : "",
                                    customer_date = customer_date,// row.GetCell(columnIndices["客户期限"]).CellType != CellType.Blank ? row.GetCell(columnIndices["客户期限"]).ToString() : "",
                                    apply_user = apply_user,//row.GetCell(columnIndices["申请人"]).CellType != CellType.Blank ? row.GetCell(columnIndices["申请人"]).ToString() : "",
                                    agent_mechanism = agent_mechanism,// row.GetCell(columnIndices["代理机构"]).CellType != CellType.Blank ? row.GetCell(columnIndices["代理机构"]).ToString() : "",
                                    invention_user = invention_user,// row.GetCell(columnIndices["发明人"]).CellType != CellType.Blank ? row.GetCell(columnIndices["发明人"]).ToString() : "",
                                    money_type = money_type,// row.GetCell(columnIndices["费用类型"]).CellType != CellType.Blank ? row.GetCell(columnIndices["费用类型"]).ToString() : "",
                                    money_name = money_name,// row.GetCell(columnIndices["费用名称"]).CellType != CellType.Blank ? row.GetCell(columnIndices["费用名称"]).ToString() : "",
                                    money_description = money_description,//row.GetCell(columnIndices["费用描述"]).CellType != CellType.Blank ? row.GetCell(columnIndices["费用描述"]).ToString() : "",
                                    money_english = money_english,// row.GetCell(columnIndices["费用描述英文"]).CellType != CellType.Blank ? row.GetCell(columnIndices["费用描述英文"]).ToString() : "",
                                    price = price,// row.GetCell(columnIndices["金额"]).CellType != CellType.Blank ? row.GetCell(columnIndices["金额"]).ToString() : "",
                                    currency = currency,//row.GetCell(columnIndices["币别"]).CellType != CellType.Blank ? row.GetCell(columnIndices["币别"]).ToString() : "",
                                    change_currency = change_currency,// row.GetCell(columnIndices["换算后币别"]).CellType != CellType.Blank ? row.GetCell(columnIndices["换算后币别"]).ToString() : "",
                                    change_money = change_money,//row.GetCell(columnIndices["换算后金额"]).CellType != CellType.Blank ? row.GetCell(columnIndices["换算后金额"]).ToString() : "",
                                    receivable_date = receivable_date,// row.GetCell(columnIndices["应收日期"]).CellType != CellType.Blank ? row.GetCell(columnIndices["应收日期"]).ToString() : "",
                                    request_date = request_date,// row.GetCell(columnIndices["请款日期"]).CellType != CellType.Blank ? row.GetCell(columnIndices["请款日期"]).ToString() : "",
                                    actual_date = actual_date,//row.GetCell(columnIndices["实收日期"]).CellType != CellType.Blank ? row.GetCell(columnIndices["实收日期"]).ToString() : "",
                                    actual_get_money = actual_get_money,// row.GetCell(columnIndices["实收/实付金额"]).CellType != CellType.Blank ? row.GetCell(columnIndices["实收/实付金额"]).ToString() : "",
                                    actual_currency = actual_currency,// row.GetCell(columnIndices["实收币别"]).CellType != CellType.Blank ? row.GetCell(columnIndices["实收币别"]).ToString() : "",
                                    pay_date = pay_date,// row.GetCell(columnIndices["缴费期限"]).CellType != CellType.Blank ? row.GetCell(columnIndices["缴费期限"]).ToString() : "",
                                    pay_head = pay_head,// row.GetCell(columnIndices["缴费抬头"]).CellType != CellType.Blank ? row.GetCell(columnIndices["缴费抬头"]).ToString() : "",
                                    actual_pay_date = actual_pay_date,// row.GetCell(columnIndices["实付日期"]).CellType != CellType.Blank ? row.GetCell(columnIndices["实付日期"]).ToString() : "",
                                    request_money_no = request_money_no,// row.GetCell(columnIndices["请款单号"]).CellType != CellType.Blank ? row.GetCell(columnIndices["请款单号"]).ToString() : "",
                                    pay_year_stage = pay_year_stage,// row.GetCell(columnIndices["缴年费阶段"]).CellType != CellType.Blank ? row.GetCell(columnIndices["缴年费阶段"]).ToString() : "",
                                    pay_type = pay_type,// row.GetCell(columnIndices["缴费类型"]).CellType != CellType.Blank ? row.GetCell(columnIndices["缴费类型"]).ToString() : "",
                                    exchange_rate = exchange_rate,// row.GetCell(columnIndices["汇率"]).CellType != CellType.Blank ? row.GetCell(columnIndices["汇率"]).ToString() : "",
                                    money_remark = money_remark,// row.GetCell(columnIndices["费用备注"]).CellType != CellType.Blank ? row.GetCell(columnIndices["费用备注"]).ToString() : "",
                                    update_record = update_record,// row.GetCell(columnIndices["修改记录"]).CellType != CellType.Blank ? row.GetCell(columnIndices["修改记录"]).ToString() : "",
                                    collection_account = collection_account,// row.GetCell(columnIndices["收款公司账号"]).CellType != CellType.Blank ? row.GetCell(columnIndices["收款公司账号"]).ToString() : "",
                                    bill_no = bill_no,// row.GetCell(columnIndices["外方账单号"]).CellType != CellType.Blank ? row.GetCell(columnIndices["外方账单号"]).ToString() : "",
                                    money_create_date = money_create_date,// row.GetCell(columnIndices["费用创建日期"]).CellType != CellType.Blank ? row.GetCell(columnIndices["费用创建日期"]).ToString() : "",
                                    invoice_no = invoice_no,// row.GetCell(columnIndices["发票号"]).CellType != CellType.Blank ? row.GetCell(columnIndices["发票号"]).ToString() : "",
                                    create_invoice_date = create_invoice_date,// row.GetCell(columnIndices["开票日期"]).CellType != CellType.Blank ? row.GetCell(columnIndices["开票日期"]).ToString() : "",
                                    cost_agent = cost_agent,// row.GetCell(columnIndices["费用合作所代理机构"]).CellType != CellType.Blank ? row.GetCell(columnIndices["费用合作所代理机构"]).ToString() : "",
                                    collection_status = collection_status,// row.GetCell(columnIndices["收款状态"]).CellType != CellType.Blank ? row.GetCell(columnIndices["收款状态"]).ToString() : "",
                                    pay_status = pay_status,// row.GetCell(columnIndices["缴费状态"]).CellType != CellType.Blank ? row.GetCell(columnIndices["缴费状态"]).ToString() : "",
                                    out_account_date = out_account_date,// row.GetCell(columnIndices["销账日期"]).CellType != CellType.Blank ? row.GetCell(columnIndices["销账日期"]).ToString() : "",
                                    power_ask_num = power_ask_num,// row.GetCell(columnIndices["权利要求项数"]).CellType != CellType.Blank ? row.GetCell(columnIndices["权利要求项数"]).ToString() : "",
                                    instructions_num = instructions_num,// row.GetCell(columnIndices["说明书页数"]).CellType != CellType.Blank ? row.GetCell(columnIndices["说明书页数"]).ToString() : "",
                                    technical_field = technical_field,// row.GetCell(columnIndices["技术领域"]).CellType != CellType.Blank ? row.GetCell(columnIndices["技术领域"]).ToString() : "",
                                    // 根据 Excel 中的列和你的数据模型进行映射
                                    outside_user = outside_user,// row.GetCell(columnIndices["外部案源人"]).CellType != CellType.Blank ? row.GetCell(columnIndices["外部案源人"]).ToString() : "",
                                    inside_user = inside_user,// row.GetCell(columnIndices["内部案源人"]).CellType != CellType.Blank ? row.GetCell(columnIndices["内部案源人"]).ToString() : "",
                                    patent_label = patent_label,// row.GetCell(columnIndices["专利标签"]).CellType != CellType.Blank ? row.GetCell(columnIndices["专利标签"]).ToString() : "",
                                    proposal_no = proposal_no,// row.GetCell(columnIndices["提案编号"]).CellType != CellType.Blank ? row.GetCell(columnIndices["提案编号"]).ToString() : "",
                                    proxy_user = proxy_user,//row.GetCell(columnIndices["代理人"]).CellType != CellType.Blank ? row.GetCell(columnIndices["代理人"]).ToString() : "",
                                    custom_one = custom_one,// row.GetCell(columnIndices["自定义栏位3"]).CellType != CellType.Blank ? row.GetCell(columnIndices["自定义栏位3"]).ToString() : "",
                                    custom_two = custom_two,// row.GetCell(columnIndices["自定义栏位4"]).CellType != CellType.Blank ? row.GetCell(columnIndices["自定义栏位4"]).ToString() : "",
                                    custom_three = custom_three,// row.GetCell(columnIndices["自定义栏位5"]).CellType != CellType.Blank ? row.GetCell(columnIndices["自定义栏位5"]).ToString() : "",
                                    text1 = text1,// row.GetCell(columnIndices["文本1"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本1"]).ToString() : "",
                                    text2 = text2,//row.GetCell(columnIndices["文本2"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本2"]).ToString() : "",
                                    text3 = text3,// row.GetCell(columnIndices["文本3"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本3"]).ToString() : "",
                                    text4 = text4,// row.GetCell(columnIndices["文本4"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本4"]).ToString() : "",
                                    text5 = text5,// row.GetCell(columnIndices["文本5"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本5"]).ToString() : "",
                                    text6 = text6,// row.GetCell(columnIndices["文本6"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本6"]).ToString() : "",
                                    text7 = text7,//row.GetCell(columnIndices["文本7"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本7"]).ToString() : "",
                                    text8 = text8,// row.GetCell(columnIndices["文本8"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本8"]).ToString() : "",
                                    text9 = text9,// row.GetCell(columnIndices["文本9"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本9"]).ToString() : "",
                                    text10 = text10,// row.GetCell(columnIndices["文本10"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本10"]).ToString() : "",
                                    text11 = text11,//row.GetCell(columnIndices["文本11"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本11"]).ToString() : "",
                                    text12 = text12,// row.GetCell(columnIndices["文本12"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本12"]).ToString() : "",
                                    text13 = text13,// row.GetCell(columnIndices["文本13"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本13"]).ToString() : "",
                                    text14 = text14,//row.GetCell(columnIndices["文本14"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本14"]).ToString() : "",
                                    text15 = text15,// row.GetCell(columnIndices["文本15"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本15"]).ToString() : "",
                                    text16 = text16,// row.GetCell(columnIndices["文本16"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本16"]).ToString() : "",
                                    text17 = text17,//row.GetCell(columnIndices["文本17"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本17"]).ToString() : "",
                                    text18 = text18,// row.GetCell(columnIndices["文本18"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本18"]).ToString() : "",
                                    text19 = text19,// row.GetCell(columnIndices["文本19"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本19"]).ToString() : "",
                                    text20 = text20,// row.GetCell(columnIndices["文本20"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本20"]).ToString() : "",
                                    text21 = text21,// row.GetCell(columnIndices["文本21"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本21"]).ToString() : "",
                                    text22 = text22,// row.GetCell(columnIndices["文本22"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本22"]).ToString() : "",
                                    text23 = text23,//row.GetCell(columnIndices["文本23"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本23"]).ToString() : "",
                                    text24 = text24,//row.GetCell(columnIndices["文本24"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本24"]).ToString() : "",
                                    text25 = text25,//row.GetCell(columnIndices["文本25"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本25"]).ToString() : "",
                                    text26 = text26,//row.GetCell(columnIndices["文本26"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本26"]).ToString() : "",
                                    text27 = text27,// row.GetCell(columnIndices["文本27"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本27"]).ToString() : "",
                                    text28 = text28,//row.GetCell(columnIndices["文本28"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本28"]).ToString() : "",
                                    text29 = text29,//row.GetCell(columnIndices["文本29"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本29"]).ToString() : "",
                                    text30 = text30//row.GetCell(columnIndices["文本30"]).CellType != CellType.Blank ? row.GetCell(columnIndices["文本30"]).ToString() : ""

                                    //fmaterialNumber = result == null ? "" : result
                                    //...
                                });;
                                //dataList.Add(dataItem);
                            }
                        }

                        // 将数据保存到数据库

                        dbContext.sli_intpo_Import.AddRange(Sli_intpo_Import);
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

        //[System.Web.Http.HttpGet]
        //public IHttpActionResult GetTableBysale_orderentry(int page = 1, int pageSize = 10, int? id = null)
        //{
        //    var context = new YourDbContext();
        //    IQueryable<sli_sale_orderImportentry> query = context.Sli_sale_orderImportentry;

        //    //if (!string.IsNullOrEmpty(Fname))
        //    //{
        //    //    query = query.Where(q => q.fname.Contains(Fname));
        //    //}
        //    if (id.HasValue)
        //    {
        //        query = query.Where(t => t.fid == id.Value);
        //    }

        //    var totalCount = query.Count(); //记录数
        //    var totalPages = (int)Math.Ceiling((double)totalCount / pageSize); // 页数
        //    var paginatedQuery = query.OrderByDescending(b => b.id).Skip((page - 1) * pageSize).Take(pageSize); //  某页记录
        //    //var datas = query.ToList();
        //    var response = new    // 定义 前端返回数据  总记录，总页，当前页 ，size,返回记录
        //    {
        //        code = 200,
        //        msg = "OK",
        //        data = new
        //        {
        //            totalCounts = totalCount,
        //            totalPagess = totalPages,
        //            currentPages = page,
        //            pageSizes = pageSize,
        //            data = paginatedQuery
        //        }
        //    };

        //    return Json(response);
        //}

        //[System.Web.Http.HttpGet]
        //public IHttpActionResult GetTableBysale_order_view(int? id = null, int? fid = null, int page = 1, int pageSize = 10,  string FCustomerName = null)
        //{
        //    var context = new YourDbContext();
        //    IQueryable<sli_sale_orderImport_view> query = context.Sli_sale_orderImport_view;

        //    if (!string.IsNullOrEmpty(FCustomerName))
        //    {
        //        query = query.Where(q => q.FCustomerName.Contains(FCustomerName));
        //    }
        //    if (id.HasValue)
        //    {
        //        query = query.Where(t => t.id == id.Value);
        //    }
        //    if (fid.HasValue)
        //    {
        //        query = query.Where(t => t.fid == fid.Value);
        //    }
        //    //if (fid.HasValue)
        //    //{
        //    //    query = query.Where(t => t.fid == fid.Value);
        //    //}

        //    var totalCount = query.Count(); //记录数
        //    var totalPages = (int)Math.Ceiling((double)totalCount / pageSize); // 页数
        //    var paginatedQuery = query.OrderByDescending(b => b.id).Skip((page - 1) * pageSize).Take(pageSize); //  某页记录
        //    //var datas = query.ToList();
        //    var response = new    // 定义 前端返回数据  总记录，总页，当前页 ，size,返回记录
        //    {
        //        code = 200,
        //        msg = "OK",
        //        data = new
        //        {
        //            totalCounts = totalCount,
        //            totalPagess = totalPages,
        //            currentPages = page,
        //            pageSizes = pageSize,
        //            data = paginatedQuery
        //        }
        //    };

        //    return Json(response);
        //}


        /// <summary>
        /// 更新接口，传入表体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Microsoft.AspNetCore.Mvc.HttpPost]
        //public async Task<object> Update([Microsoft.AspNetCore.Mvc.FromBody] sli_sale_orderImport import)
        //{
        //    try
        //    {
        //        var context = new YourDbContext();
        //        var entity = await context.Sli_sale_orderImport.FindAsync(import.FID);
        //        if (entity == null)
        //        {
        //            var dataNull = new
        //            {
        //                code = 200,
        //                msg = "ok",
        //                date = "修改记录不存在"
        //            };
        //            return dataNull;
        //        }
        //        else
        //        {

        //            var orderImport = context.Sli_sale_orderImport.FirstOrDefault(p => p.FID == import.FID);
        //            //var orderImportentry = context.Sli_sale_orderImportentry.Where(p => p.fid == import.FID).ToList();


        //            orderImport.FCustomerName = import.FCustomerName;
        //            orderImport.FCustomerID = import.FCustomerID;

        //            //context.Sli_sale_orderImportentry.RemoveRange(orderImportentry);
        //            await context.SaveChangesAsync();

        //            foreach (var childTableData in import.sli_sale_orderImportentry)
        //            {
        //                var orderImportentry = context.Sli_sale_orderImportentry.FirstOrDefault(p => p.id == childTableData.id);
        //                //var entry = new sli_sale_orderImportentry
        //                //{
        //                //orderImportentry.fid = import.FID;
        //                orderImportentry.fmaterialNumber = childTableData.fmaterialNumber;
        //                orderImportentry.fseq = childTableData.fseq;
        //                orderImportentry.fchoose = childTableData.fchoose;
        //                orderImportentry.fsupplierSubmit = childTableData.fsupplierSubmit;
        //                orderImportentry.finquiryNo = childTableData.finquiryNo;
        //                orderImportentry.fsupplierName = childTableData.fsupplierName;
        //                orderImportentry.fprojectNo = childTableData.fprojectNo;
        //                orderImportentry.fdeliveryDate = childTableData.fdeliveryDate;
        //                orderImportentry.fname = childTableData.fname;
        //                orderImportentry.fdescription = childTableData.fdescription;
        //                orderImportentry.fsliMetal = childTableData.fsliMetal;
        //                orderImportentry.fqty = childTableData.fqty;
        //                orderImportentry.fsliHeatTreatment = childTableData.fsliHeatTreatment;
        //                orderImportentry.fsliTestBarQty = childTableData.fsliTestBarQty;
        //                orderImportentry.fsliExplanation = childTableData.fsliExplanation;
        //                orderImportentry.fsliNotice = childTableData.fsliNotice;
        //                orderImportentry.fsliDrawingNo = childTableData.fsliDrawingNo;
        //                orderImportentry.fsliBlank = childTableData.fsliBlank;
        //                orderImportentry.fsliWorkOrder = childTableData.fsliWorkOrder;
        //                orderImportentry.fsliSaleOrder = childTableData.fsliSaleOrder;
        //                orderImportentry.fsliQuotationNo = childTableData.fsliQuotationNo;
        //                orderImportentry.fsliStockNo = childTableData.fsliStockNo;
        //                orderImportentry.fsliStockLocation = childTableData.fsliStockLocation;
        //                await context.SaveChangesAsync();
        //                //};
        //                //context.Sli_sale_orderImportentry.Add(orderImportentry);
        //            }
        //            //await context.SaveChangesAsync();

        //            var datas = new
        //            {
        //                code = 200,
        //                msg = "ok",
        //                date = "修改成功！"
        //            };
        //            return Ok(datas);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var datas = new
        //        {
        //            code = 400,
        //            msg = "失败",
        //            date = ex.ToString()
        //        };
        //        return Ok(datas); ;
        //    }

        //}
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
                    //string jsonData = "";
                    //string sJson = {\"NeedUpDateFields\":[],\"NeedReturnFields\":[],\"IsDeleteEntry\":\"true\",\"SubSystemId\":\"\",\"IsVerifyBaseDataField\":\"false\",
                    //\"IsEntryBatchFill\":\"true\",\"ValidateFlag\":\"true\",\"NumberSearch\":\"true\",\"IsAutoAdjustField\":\"true\",\"InterationFlags\":\"\",
                    //\"IgnoreInterationFlag\":\"\",\"IsControlPrecision\":\"false\",\"ValidateRepeatJson\":\"false\",
                    //\"Model\":{
                    //\"FID\":0,\"FBillTypeID\":{\"FNUMBER\":\"XSDD01_SYS\"},\"FDate\":\"2024-11-12 00:00:00\",\"FSaleOrgId\":{\"FNumber\":\"100\"},
                    //\"FCustId\":{\"FNumber\":\"CUST0001\"},\"FReceiveId\":{\"FNumber\":\"CUST0001\"},\"FSaleDeptId\":{\"FNumber\":\"BM000001\"},
                    //\"FSalerId\":{\"FNumber\":\"001_001_1\"},\"FSettleId\":{\"FNumber\":\"CUST0001\"},\"FChargeId\":{\"FNumber\":\"CUST0001\"},
                    //\"FNetOrderBillId\":0,\"FOppID\":0,\"FISINIT\":false,\"FIsMobile\":false,\"FContractId\":0,\"FIsUseOEMBomPush\":false,
                    //\"FXPKID_H\":0,\"FIsUseDrpSalePOPush\":false,\"FIsCreateStraightOutIN\":false,
                    //\"FSaleOrderFinance\":{\"FSettleCurrId\":{\"FNumber\":\"PRE001\"},\"FIsIncludedTax\":true,\"FIsPriceExcludeTax\":true,
                    //\"FExchangeTypeId\":{\"FNumber\":\"HLTX01_SYS\"},\"FOverOrgTransDirect\":false},\"FSalOrderRec\":{},
                    //\"FSaleOrderEntry\":[{\"FRowType\":\"Standard\",\"FMaterialId\":{\"FNumber\":\"T000001\"},\"FUnitID\":{\"FNumber\":\"Pcs\"},
                    //\"FInventoryQty\":0.0,\"FCurrentInventory\":0.0,\"FAwaitQty\":0.0,\"FAvailableQty\":0.0,\"FQty\":100.0,
                    //\"FPriceUnitId\":{\"FNumber\":\"Pcs\"},\"FIsFree\":false,\"FEntryTaxRate\":13.00,\"FDeliveryDate\":\"2024-11-12 11:01:22\",
                    //\"FStockOrgId\":{\"FNumber\":\"100\"},\"FSettleOrgIds\":{\"FNumber\":\"100\"},\"FSupplyOrgId\":{\"FNumber\":\"100\"},
                    //\"FOwnerTypeId\":\"BD_OwnerOrg\",\"FOwnerId\":{\"FNumber\":\"100\"},\"FEntryNote\":\"备注\",\"FSrcType\":\"\",
                    //\"FSrcBillNo\":\"\",\"FReserveType\":\"1\",\"FPriority\":0,\"FNetOrderEntryId\":0,\"FPriceBaseQty\":100.0,
                    //\"FStockUnitID\":{\"FNumber\":\"Pcs\"},\"FStockQty\":100.0,\"FStockBaseQty\":100.0,\"FPurQty\":0.0,\"FPurBaseQty\":0.0,
                    //\"FOUTLMTUNIT\":\"SAL\",\"FOutLmtUnitID\":{\"FNumber\":\"Pcs\"},\"FISMRPCAL\":false,\"FBOMEntryId\":0,\"FAllAmountExceptDisCount\":1000.0,
                    //\"FXPKID\":0,\"FThirdPartyEntrySeq\":0,\"FsliHeatTreatment\":\"毛坯热处理\",\"FsliTestBarQty\":10,\"FsliMetel\":{\"FNUMBER\":\"35#\"},
                    //\"FsliExplanation\":\"附注\",\"FsliNotice\":\"备注\",\"FsliWorkOrder\":\"生产号\",\"FsliSaleOrder\":\"客户订单号\",
                    //\"FsliQuotationNo\":\"未\",\"FsliStockNo\":\"测试\",\"FsliStockLocation\":\"仓库位置\",\"FsliBlank\":\"毛坯图号\",\"FsliDrawingNo\":\"图号\"}]}}



                    #region
                    //StringBuilder Str = new StringBuilder();
                    //Str.Append("{");
                    //Str.Append("\"NeedUpDateFields\":[],");
                    //Str.Append("\"NeedReturnFields\":[FBillNo],");
                    //Str.Append("\"IsDeleteEntry\":\"true\",");
                    //Str.Append("\"SubSystemId\":\"\",");
                    //Str.Append("\"IsVerifyBaseDataField\":\"false\",");
                    //Str.Append("\"IsEntryBatchFill\":\"true\",");
                    //Str.Append("\"ValidateFlag\":\"true\",");
                    //Str.Append("\"NumberSearch\":\"true\",");
                    //Str.Append("\"IsAutoAdjustField\":\"true\",");
                    //Str.Append("\"InterationFlags\":\"\",");
                    //Str.Append("\"IsControlPrecision\":\"false\",");
                    //Str.Append("\"ValidateRepeatJson\":\"false\",");
                    //Str.Append("\"Model\":{");
                    //Str.Append("\"FID\":0,");
                    //Str.Append("\"FBillTypeID\":{\"FNUMBER\":\"XSDD01_SYS\"},");
                    //Str.Append("\"FDate\":\"" + DateTime.Now + "\",");
                    //Str.Append("\"FSaleOrgId\":{\"FNumber\":\"100\"},");
                    //Str.Append("\"FCustId\":{\"FNumber\":\"" + FcustomerNumer.FNUMBER + "\"},");
                    //Str.Append("\"FReceiveId\":{\"FNumber\":\"" + FcustomerNumer.FNUMBER + "\"},");
                    //Str.Append("\"FSaleDeptId\":{\"FNumber\":\"BM000001\"},");    //部门
                    //Str.Append("\"FSalerId\":{\"FNumber\":\"001_001_1\"},");     //销售员
                    //Str.Append("\"FSettleId\":{\"FNumber\":\"" + FcustomerNumer.FNUMBER + "\"},");
                    //Str.Append("\"FChargeId\":{\"FNumber\":\"" + FcustomerNumer.FNUMBER + "\"},");
                    //Str.Append("\"FNetOrderBillId\":0,");
                    //Str.Append("\"FOppID\":0,");
                    //Str.Append("\"FISINIT\":false,");
                    //Str.Append("\"FIsMobile\":false,");
                    //Str.Append("\"FContractId\":0,");
                    //Str.Append("\"FIsUseOEMBomPush\":false,");
                    //Str.Append("\"FXPKID_H\":0,");
                    //Str.Append("\"FIsUseDrpSalePOPush\":false,");
                    //Str.Append("\"FIsCreateStraightOutIN\":false,");
                    //Str.Append("\"FSaleOrderFinance\":{");
                    //Str.Append("\"FSettleCurrId\":{\"FNumber\":\"PRE001\"},");
                    //Str.Append("\"FIsIncludedTax\":true,");
                    //Str.Append("\"FIsPriceExcludeTax\":true,");
                    //Str.Append("\"FExchangeTypeId\":{\"FNumber\":\"HLTX01_SYS\"},");
                    //Str.Append("\"FMarginLevel\":0.0,");
                    //Str.Append("\"FMargin\":0.0,");
                    //Str.Append("\"FOverOrgTransDirect\":false,");
                    //Str.Append("\"FAllDisCount\":0.0,");
                    //Str.Append("\"FXPKID_F\":0},");
                    //Str.Append("\"FSalOrderRec\":{},");
                    //Str.Append("\"FSaleOrderEntry\":[");

                    //foreach (var entitydata in entityList)
                    //{
                    //    bool isLastItem = index == entity.Count() - 1;
                    //    Str.Append("{\"FRowType\":\"Standard\",");
                    //    //Str.Append("\"FMaterialId\":{\"FNumber\":\""+ entitydata.fmaterialNumber + "\"},");
                    //    Str.Append("\"FMaterialId\":{\"FNumber\":\"T000001\"},");
                    //    Str.Append("\"FUnitID\":{\"FNumber\":\"Pcs\"},");
                    //    Str.Append("\"FInventoryQty\":0.0,");
                    //    Str.Append("\"FCurrentInventory\":0.0,");
                    //    Str.Append("\"FAwaitQty\":0.0,");
                    //    Str.Append("\"FAvailableQty\":0.0,");
                    //    Str.Append("\"FQty\":"+ entitydata.fqty+ ",");
                    //    Str.Append("\"FPriceUnitId\":{\"FNumber\":\"Pcs\"},");
                    //    Str.Append("\"FOldQty\":0.0,");
                    //    Str.Append("\"FPrice\":0.0,");
                    //    Str.Append("\"FTaxPrice\":0.0,");
                    //    Str.Append("\"FIsFree\":false,");
                    //    Str.Append("\"FEntryTaxRate\":13.00,");
                    //    //Str.Append("\"FExpPeriod\":0,");
                    //    //Str.Append("\"FDiscountRate\":0.0,");
                    //    //Str.Append("\"FPriceDiscount\":0.0,");
                    //    Str.Append("\"FDeliveryDate\":\""+ entitydata.fdeliveryDate+ "\",");
                    //    Str.Append("\"FStockOrgId\":{\"FNumber\":\"100\"},");
                    //    Str.Append("\"FSettleOrgIds\":{\"FNumber\":\"100\"},");
                    //    Str.Append("\"FSupplyOrgId\":{\"FNumber\":\"100\"},");
                    //    Str.Append("\"FOwnerTypeId\":\"BD_OwnerOrg\",");
                    //    Str.Append("\"FOwnerId\":{\"FNumber\":\"100\"},");
                    //    Str.Append("\"FSrcType\":\"\",");
                    //    Str.Append("\"FReserveType\":\"1\",");
                    //    Str.Append("\"FPriceBaseQty\":"+ entitydata.fqty+ ",");
                    //    Str.Append("\"FStockUnitID\":{\"FNumber\":\"Pcs\"},");
                    //    Str.Append("\"FStockQty\":"+ entitydata.fqty+ ",");
                    //    Str.Append("\"FStockBaseQty\":" + entitydata.fqty + ",");
                    //    Str.Append("\"FOUTLMTUNIT\":\"SAL\",");
                    //    Str.Append("\"FOutLmtUnitID\":{\"FNumber\":\"Pcs\"},");
                    //    Str.Append("\"FISMRP\":false,");
                    //    Str.Append("\"FISMRPCAL\":false,");
                    //    Str.Append("\"FAllAmountExceptDisCount\":0.0,");
                    //    Str.Append("\"FsliHeatTreatment\":\""+ entitydata .fsliHeatTreatment+ "\",");
                    //    Str.Append("\"FsliTestBarQty\":\"" + entitydata.fsliTestBarQty + "\",");
                    //    Str.Append("\"FsliMetel\":{\"FNUMBER\":\""+ entitydata.fsliMetal + "\"},");
                    //    Str.Append("\"FsliExplanation\":\"" + entitydata.fsliExplanation + "\",");
                    //    Str.Append("\"FsliNotice\":\"" + entitydata.fsliNotice + "\",");
                    //    Str.Append("\"FsliWorkOrder\":\"" + entitydata.fsliWorkOrder + "\",");
                    //    Str.Append("\"FsliSaleOrder\":\"" + entitydata.fsliSaleOrder + "\",");
                    //    Str.Append("\"FsliQuotationNo\":\"" + entitydata.fsliQuotationNo + "\",");
                    //    Str.Append("\"FsliStockNo\":\"" + entitydata.fsliStockNo + "\",");
                    //    Str.Append("\"FsliBlank\":\"" + entitydata.fsliBlank + "\",");
                    //    Str.Append("\"FsliDrawingNo\":\"" + entitydata.fsliDrawingNo + "\"");
                    //    if (index == entity.Count() - 1)
                    //    {
                    //        Str.Append("}");
                    //    }
                    //    else
                    //    {
                    //        Str.Append("},");
                    //    }
                    //    index++;
                    //}
                    //Str.Append("]}}");
                    #endregion
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