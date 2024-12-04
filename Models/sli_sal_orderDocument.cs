using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_sal_orderDocument    //合并销售订单表体，以物料代码
    {
        [Key]
        public int id { get; set; }
        public string fbillNo { get; set; }
        public int fid { get; set; }
        public int fseq { get; set; }
        public int fentryID { get; set; }
        public int fmaterialID { get; set; }
        public string fnumber { get; set; }
        public string fname { get; set; }
        public string fdescription { get; set; }
        public string fsliMetal { get; set; }
        public string fsliDrawingNO { get; set; }
        public string fsliGrade { get; set; }
        public int ftechSaleDefault { get; set; }
        public int ftechSale { get; set; }
        public string fcreaterT { get; set; }
        public string fmodifiedT { get; set; }
        public int fqualityStandardDefault { get; set; }
        public int fqualityStandard { get; set; }
        public string fcreaterQ { get; set; }
        public string fmodifiedQ { get; set; }
    }
}