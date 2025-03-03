using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_sale_orderImport  //excle导入销售订单表头
    {
        [Key]
        public int FID { get; set; }
        public int FCustomerID { get; set; }
        public string FCustomerName { get; set; }
        public int Flag { get; set; }
        public string FParameter { get; set; }
        public string FReason { get; set; }
        public string FBillNo { get; set; }
        public int FSaleId { get; set; }
        public string FContractNo1 { get; set; }
        public string FpurProperty { get; set; }  //采购属性
        public virtual ICollection<sli_sale_orderImportentry> sli_sale_orderImportentry { get; set; }
    }
    public class sli_sale_orderImportentry  //excle导入销售订单表体
    {
        public int Id { get; set; }
        public int Fid { get; set; }  //表头ID
        public string FmaterialNumber { get; set; }   //物料内码
        public string FSerialNo { get; set; }   //序号
        public string FsliDrawingNo { get; set; }    //图号/Φ标准
        public string Fname { get; set; }   //名称
        public string Fdescription { get; set; }   //规格
        public string FsliMetal { get; set; }   //材质
        public int Fqty { get; set; }   //数量
        public int Fwight { get; set; }   //单重
        public string FdeliveryDate { get; set; }   //交期备注  有调整
        public string FOrderNo { get; set; }   //令号
        public string FPartNo { get; set; }   //件号
        public string FItemNo { get; set; }   //项号
        public string FPositionNo { get; set; }   //位号
        public string FMachineName { get; set; }   //机名
        public int Fseq { get; set; }   //行号
        public string FarticleNo { get; set; }   //品号
        public string FProductNo { get; set; }   //产号
        public string Fmaterial { get; set; }   //物料(客户提供，不是系统物料代码)
        public string FNote { get; set; }   //备注
        public string FProjectNo { get; set; }   //项目号
        public string FPlanNo { get; set; }   //计划号
        public string Fwbs { get; set; }   //WBS
        public string FTrackingNo { get; set; }   //追踪号
        public string FUrnaceBatchNo { get; set; }   //炉批号
        public string FTaskNo { get; set; }   //任务号
        public string FPartNoC { get; set; }   //零件号(客户提供)
        public string FEquipmentNo { get; set; }   //设备号
        public string FContractNo { get; set; }   //合同号
        public string FBillNo { get; set; }   //订单编号
        public string FProductNoC { get; set; }   //产品编号
        public string FContainerNo { get; set; }   //容器编号
        public string FForgingNo { get; set; }   //锻件编号
        public string FPipeNo { get; set; }   //管子编号
        public string FManufacturingNo { get; set; }   //制造编号
        public string FmaterialNo { get; set; }   //物料编码
        public string FPartNoC1 { get; set; }   //零件编号(客户提供)
        public string FFactoryC { get; set; }   //出厂编号
        public string FEquipmentNoC { get; set; }   //设备名称(客户提供)
        public string FStockNoC { get; set; }   //入库编号

        //public int fchoose { get; set; }   //选择
        //public string fsupplierSubmit { get; set; }   //提交供应商标记
        //public string finquiryNo { get; set; }   //询价单号
        //public string fsupplierName { get; set; }   //供应商名称
        //public string fprojectNo { get; set; }   //项目号
        
        
        //public string fsliHeatTreatment { get; set; }   //毛坯热处理fsliHeatTreatment
        //public int fsliTestBarQty { get; set; }   //试棒数量fsliTestBarQty
        //public string fsliExplanation { get; set; }   //附注fsliExplanation
        //public string fsliNotice { get; set; }   //采购备注fsliNotice
        //public string fsliDrawingNo { get; set; }   //图纸号fsliDrawingNo
        //public string fsliBlank { get; set; }   //毛坯图号fsliBlank
        //public string fsliWorkOrder { get; set; }   //生产号fsliWorkOrder
        //public string fsliSaleOrder { get; set; }   //订单号fsliSaleOrder
        //public string fsliQuotationNo { get; set; }   //报价状态fsliQuotationNo
        //public string fsliStockNo { get; set; }   //仓库fsliStockNo
        //public string fsliStockLocation { get; set; }   //仓库位置fsliStockLocation
        public int fstatus { get; set; }
        public string fparameter { get; set; }
        public string freason { get; set; }

        public virtual sli_sale_orderImport sli_sale_orderImport { get; set; }
    }
}