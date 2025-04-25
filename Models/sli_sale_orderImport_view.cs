using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_sale_orderImport_view
    {
        // public int  FCustomerID { get; set; }
        // public string FCustomerName { get; set; }
        // [Key]
        // public int id { get; set; }
        // public int fid { get; set; }  //表头ID
        // public string fmaterialNumber { get; set; }   //物料内码
        // //public int fseq { get; set; }   //序号
        // //public int fchoose { get; set; }   //选择
        // //public string fsupplierSubmit { get; set; }   //提交供应商标记
        //// public string finquiryNo { get; set; }   //询价单号
        //// public string fsupplierName { get; set; }   //供应商名称
        // public string fprojectNo { get; set; }   //项目号
        // public string fdeliveryDate { get; set; }   //需求日期
        // public string fname { get; set; }   //名称
        // public string fdescription { get; set; }   //规格
        // public string fsliMetal { get; set; }   //材质
        // //public int fqty { get; set; }   //需求数量
        // //public string fsliHeatTreatment { get; set; }   //毛坯热处理fsliHeatTreatment
        // //public int fsliTestBarQty { get; set; }   //试棒数量fsliTestBarQty
        // //public string fsliExplanation { get; set; }   //附注fsliExplanation
        // //public string fsliNotice { get; set; }   //采购备注fsliNotice
        // //public string fsliDrawingNo { get; set; }   //图纸号fsliDrawingNo
        // //public string fsliBlank { get; set; }   //毛坯图号fsliBlank
        // //public string fsliWorkOrder { get; set; }   //生产号fsliWorkOrder
        // //public string fsliSaleOrder { get; set; }   //订单号fsliSaleOrder
        // //public string fsliQuotationNo { get; set; }   //报价状态fsliQuotationNo
        // //public string fsliStockNo { get; set; }   //仓库fsliStockNo
        // //public string fsliStockLocation { get; set; }   //仓库位置fsliStockLocation

        // public string FSerialNo { get; set; }   //序号
        // public string FsliDrawingNo { get; set; }    //图号/Φ标准
        // public string Fname { get; set; }   //名称
        // public string Fdescription { get; set; }   //规格
        // public string FsliMetal { get; set; }   //材质
        // public int Fqty { get; set; }   //数量
        // public int Fwight { get; set; }   //单重
        // public string FdeliveryDate { get; set; }   //交期备注  有调整
        // public string FOrderNo { get; set; }   //令号
        // public string FPartNo { get; set; }   //件号
        // public string FItemNo { get; set; }   //项号
        // public string FPositionNo { get; set; }   //位号
        // public string FMachineName { get; set; }   //机名
        // public int Fseq { get; set; }   //行号
        // public string FarticleNo { get; set; }   //品号
        // public string FProductNo { get; set; }   //产号
        // public string Fmaterial { get; set; }   //物料(客户提供，不是系统物料代码)
        // public string FNote { get; set; }   //备注
        // public string FProjectNo { get; set; }   //项目号
        // public string FPlanNo { get; set; }   //计划号
        // public string Fwbs { get; set; }   //WBS
        // public string FTrackingNo { get; set; }   //追踪号
        // public string FUrnaceBatchNo { get; set; }   //炉批号
        // public string FTaskNo { get; set; }   //任务号
        // public string FPartNoC { get; set; }   //零件号(客户提供)
        // public string FEquipmentNo { get; set; }   //设备号
        // public string FContractNo { get; set; }   //合同号
        // public string FBillNo { get; set; }   //订单编号
        // public string FProductNoC { get; set; }   //产品编号
        // public string FContainerNo { get; set; }   //容器编号
        // public string FForgingNo { get; set; }   //锻件编号
        // public string FPipeNo { get; set; }   //管子编号
        // public string FManufacturingNo { get; set; }   //制造编号
        // public string FmaterialNo { get; set; }   //物料编码
        // public string FPartNoC1 { get; set; }   //零件编号(客户提供)
        // public string FFactoryC { get; set; }   //出厂编号
        // public string FEquipmentNoC { get; set; }   //设备名称(客户提供)
        // public string FStockNoC { get; set; }   //入库编号






        // public int fstatus { get; set; }
        // public string fparameter { get; set; }
        // public string  freason { get; set; }
        // public int flag { get; set; }

        public int? Flag { get; set; }
        public int? FSaleId { get; set; }
        public int? FCustomerID { get; set; }
        public string FCustomerName { get; set; }
        public string FpurProperty { get; set; }
        public int Id { get; set; }
        public int? Fid { get; set; }
        public string FmaterialNumber { get; set; }
        public int? Fseq { get; set; }
        public int? Fchoose { get; set; }
        public string FsupplierSubmit { get; set; }
        public string FinquiryNo { get; set; }
        public string FsupplierName { get; set; }
        public string FprojectNo { get; set; }
        public string FdeliveryDate { get; set; }
        public string Fname { get; set; }
        public string Fdescription { get; set; }
        public string FsliMetal { get; set; }
        public int? Fqty { get; set; }
        public string FsliHeatTreatment { get; set; }
        public int? FsliTestBarQty { get; set; }
        public string FsliExplanation { get; set; }
        public string FsliNotice { get; set; }
        public string FsliDrawingNo { get; set; }
        public string FsliBlank { get; set; }
        public string FsliWorkOrder { get; set; }
        public string FsliSaleOrder { get; set; }
        public string FsliQuotationNo { get; set; }
        public string FsliStockNo { get; set; }
        public string FsliStockLocation { get; set; }
        public string FSerialNo { get; set; }
        public int? Fwight { get; set; }
        public string FOrderNo { get; set; }
        public string FPartNo { get; set; }
        public string FItemNo { get; set; }
        public string FPositionNo { get; set; }
        public string FMachineName { get; set; }
        public string FarticleNo { get; set; }
        public string FProductNo { get; set; }
        public string Fmaterial { get; set; }
        public string FNote { get; set; }
        public string FPlanNo { get; set; }
        public string Fwbs { get; set; }
        public string FTrackingNo { get; set; }
        public string FUrnaceBatchNo { get; set; }
        public string FTaskNo { get; set; }
        public string FPartNoC { get; set; }
        public string FEquipmentNo { get; set; }
        public string FContractNo { get; set; }
        public string FContractNo1 { get; set; }
        public string FBillNo { get; set; }
        public string FProductNoC { get; set; }
        public string FContainerNo { get; set; }
        public string FForgingNo { get; set; }
        public string FPipeNo { get; set; }
        public string FManufacturingNo { get; set; }
        public string FmaterialNo { get; set; }
        public string FPartNoC1 { get; set; }
        public string FFactoryC { get; set; }
        public string FEquipmentNoC { get; set; }
        public string FStockNoC { get; set; }
        public int? Fstatus { get; set; }
        public string Fparameter { get; set; }
        public string Freason { get; set; }



    }


    public class sli_sale_orderImport_view1
    {
        [Key]
        public int? FID { get; set; }  
        public int? FCustomerID { get; set; }  //客户ID  FNumber
        public string FNumber { get; set; } //客户编码
        public string FCustomerName { get; set; } //客户名称
        public string FBillNo { get; set; }//单据编号

        public int ? FSaleId { get; set; } //业务员ID
        public string empName { get; set; } //业务员名称
        public string FContractNo1 { get; set; }//合同号
        public string FpurProperty { get; set; }//合同号
    }
}