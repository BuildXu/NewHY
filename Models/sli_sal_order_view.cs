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
        public int FID { get; set; }
        public string FBILLNO { get; set; }
        public string FDATE { get; set; }
        public int FCUSTID { get; set; }
        public string FNUMBER { get; set; }
        public string FNAME { get; set; }
        public string FSumNUMBER { get; set; }
        public virtual ICollection<sli_sal_orderEntry_view> sli_sal_orderEntry_view { get; set; }
    }

    public class sli_sal_orderEntry_view
    {
        public int FID { get; set; }
        [Key]
        public int FENTRYID { get; set; }
        public int FSEQ { get; set; }
        public decimal FQTY { get; set; }
        public string FNOTE { get; set; }
        public string FPLANDELIVERYDATE { get; set; }
        public decimal fstockqty { get; set; }
        public int FmaterialID { get; set; }
        public string Fnumber { get; set; }
        public string Fname { get; set; }
        public string Fdescription { get; set; }
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
        public string FsliDelivery { get; set; }
        public string Fsliblankmodel { get; set; }
        public string FsliPunching { get; set; }
        public decimal FsliTemperatureBegin { get; set; }
        public decimal FsliTempratureEnd { get; set; }
        public string Fslimould { get; set; }
        public string Fsliroller { get; set; }
        public int FsliHeatingTimes { get; set; }
        public string FsliGrade { get; set; }
        public string FSumNumber { get; set; }

        public int FworkOrderListQty { get; set; }
        public int FworkOrderListRemain { get; set; }
        public virtual sli_sal_order_view sli_sal_order_view { get; set; }
    }
}