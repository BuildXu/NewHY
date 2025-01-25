using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_sal_deliverynotice
    {
        [Key]
        public int Fid { get; set; }
        public string Fbillno { get; set; }//通知单号
        public string Fnumber { get; set; }// 客户代码
        public string Fshortname { get; set; }// 客户简称
        public string  Faddress { get; set; }//地址
        public string  ForderNo { get; set; }// 订单号
        public virtual ICollection<sli_sal_deliverynoticeentry> sli_sal_deliverynoticeentry { get; set; }

    }
    public class sli_sal_deliverynoticeentry
    {
        
        public int Fid { get; set; }   //关联表头ID
        public int Fseq { get; set; }  // 订单行号
        public string Fnumber { get; set; } //物料代码
        public decimal Fqty { get; set; }// 发货数量
        public string Fisvmibusiness { get; set; }// 不要显示
        [Key]
        public int Fentryid { get; set; }//发货通知行Fentryid
        public DateTime Fdeliverydate { get; set; }// 发货日期
        public virtual sli_sal_deliverynotice sli_sal_deliverynotice { get; set; }
    }


    public class sli_sal_deliverynotice_view
    {
        [Key]
        public int Fid { get; set; }
        public string Fbillno { get; set; }//通知单号
        public string FcustNumber { get; set; }// 客户代码
        public string Fshortname { get; set; }// 客户简称
        public string Faddress { get; set; }//地址
        public string ForderNo { get; set; }// 订单号
        public int Fseq { get; set; }  // 订单行号
        public string Fnumber { get; set; } //物料代码
        public decimal Fqty { get; set; }// 发货数量
        public string Fisvmibusiness { get; set; }// 不要显示
        
        public int Fentryid { get; set; }//发货通知行Fentryid
        public DateTime Fdeliverydate { get; set; }// 发货日期
        public string Fcustno { get; set; }// 客户代码
        public string Funitno { get; set; }// 计量单位
        public string Fproductno { get; set; }// 批号
        public virtual ICollection<sli_sal_deliverynoticentry_view> sli_sal_deliverynoticentry_view { get; set; }

    }


    public class sli_sal_deliverynoticentry_view
    {

        public int Fid { get; set; }
        public string Fbillno { get; set; }//通知单号
        public string FcustNumber { get; set; }// 客户代码
        public string Fshortname { get; set; }// 客户简称
        public string Faddress { get; set; }//地址
        public string ForderNo { get; set; }// 订单号
        public int Fseq { get; set; }  // 订单行号
        public string Fnumber { get; set; } //物料代码
        public decimal Fqty { get; set; }// 发货数量
        public string Fisvmibusiness { get; set; }// 不要显示
        [Key]
        public int Fentryid { get; set; }//发货通知行Fentryid
        public DateTime Fdeliverydate { get; set; }// 发货日期
        public string Fproductno { get; set; }// 批号
        public string Fcustno { get; set; }// 客户代码
        public string Funitno { get; set; }// 计量单位
        public virtual sli_sal_deliverynotice_view sli_sal_deliverynotice_view { get; set; }
    }



    public class T_SAL_DELIVERYNOTICE   //
    {
        [Key]
        public int Fid { get; set; }
        public string Flag { get; set; }//同步标志
        public string FParameter { get; set; }// 同步json
        public string FReason { get; set; }// 同步结果

        

    }
}