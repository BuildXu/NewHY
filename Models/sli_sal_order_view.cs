using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_sal_order_view
    {
        [Key]
        public int Fid { get; set; } //销售订单主表ID
        public string Fbillno { get; set; }//销售订单单据号
        public DateTime ? Fdate { get; set; }//销售订单日期
        public int Fcustid { get; set; }//客户ID
        public string Fnumber { get; set; }//客户代码
        public string Fname { get; set; }//客户名称
        public string Fsumnumber { get; set; }
        public virtual ICollection<sli_sal_orderEntry_view> sli_sal_orderEntry_view { get; set; }
    }

    public class sli_sal_orderEntry_view
    {
        public int Fid { get; set; } //主表ID
        [Key]
        public int FentryId { get; set; } //分录表ID
        public int Fseq { get; set; } //行号
        public decimal Fqty { get; set; }//数量
        public string Fnote { get; set; }//备注
        public DateTime ? Fplandeliverydate { get; set; }//交货日期
        public decimal fstockqty { get; set; } //库存数量
        public int FmaterialID { get; set; }//物料ID
        public string Fnumber { get; set; }//物料代码
        public string Fname { get; set; }//物料名称
        public string Fdescription { get; set; }//规格型号
        public decimal FsliOuterDiameter { get; set; }
        public decimal FsliInnerDiameter { get; set; }
        public decimal FsliHight { get; set; }
        public decimal FsliAllowanceOD { get; set; }
        public decimal FsliAllowanceid { get; set; }
        public decimal fsliallowanceH { get; set; }
        public decimal FsliWeightmaterial { get; set; }
        public decimal FsliWeightforging { get; set; }
        public decimal FsliWeightGoods { get; set; }
        public string Fslidrawingno { get; set; }
        public string FsliMetal { get; set; }
        public string FsliGoodsStatus { get; set; }
        public string FsliProcessing { get; set; }
        public string Fslidelivery { get; set; }
        public string Fsliblankmodel { get; set; }
        public string FsliPunching { get; set; }
        public int FsliTemperatureBegin { get; set; }
        public int FsliTempratureEnd { get; set; }
        public string Fslimould { get; set; }
        public string Fsliroller { get; set; }
        public int FsliHeatingTimes { get; set; }
        public string FsliGrade { get; set; }
        public string FSumNumber { get; set; }

        public int FworkOrderListQty { get; set; }
        public int FworkOrderListRemain { get; set; }
        public string FslisaleTechNo { get; set; }
        public string FslitechNo { get; set; }  //Fslimetel
        public string Fslimetel { get; set; }  //Fslimetel FSERIALNO
        public string FSerialNo { get; set; }  //Fslimetel FSERIALNO
        public virtual sli_sal_order_view sli_sal_order_view { get; set; }
    }
}