using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_sale_orderImport_view
    {
        public int  FCustomerID { get; set; }
        public string FCustomerName { get; set; }
        [Key]
        public int id { get; set; }
        public int fid { get; set; }  //表头ID
        public string fmaterialNumber { get; set; }   //物料内码
        public int fseq { get; set; }   //序号
        public int fchoose { get; set; }   //选择
        public string fsupplierSubmit { get; set; }   //提交供应商标记
        public string finquiryNo { get; set; }   //询价单号
        public string fsupplierName { get; set; }   //供应商名称
        public string fprojectNo { get; set; }   //项目号
        public string fdeliveryDate { get; set; }   //需求日期
        public string fname { get; set; }   //名称
        public string fdescription { get; set; }   //规格
        public string fsliMetal { get; set; }   //材质
        public int fqty { get; set; }   //需求数量
        public string fsliHeatTreatment { get; set; }   //毛坯热处理fsliHeatTreatment
        public int fsliTestBarQty { get; set; }   //试棒数量fsliTestBarQty
        public string fsliExplanation { get; set; }   //附注fsliExplanation
        public string fsliNotice { get; set; }   //采购备注fsliNotice
        public string fsliDrawingNo { get; set; }   //图纸号fsliDrawingNo
        public string fsliBlank { get; set; }   //毛坯图号fsliBlank
        public string fsliWorkOrder { get; set; }   //生产号fsliWorkOrder
        public string fsliSaleOrder { get; set; }   //订单号fsliSaleOrder
        public string fsliQuotationNo { get; set; }   //报价状态fsliQuotationNo
        public string fsliStockNo { get; set; }   //仓库fsliStockNo
        public string fsliStockLocation { get; set; }   //仓库位置fsliStockLocation
        public int fstatus { get; set; }
        public string fparameter { get; set; }
        public string freason { get; set; }
        public int flag { get; set; }
    }
}