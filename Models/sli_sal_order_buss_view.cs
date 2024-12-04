using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_sal_order_buss_view    //销售订单商务视图，关联客户名称
    {
        [Key]
        public int FID { get; set; }
        public string FBillno { get; set; }
        public DateTime FDate { get; set; }
        public string FCustNmae { get; set; }
        public string FCustSum { get; set; }
        public DateTime FApproveDate { get; set; }
    }

    public class sli_sal_orderentry    //销售订单表体
    {
        [Key]
        public int FID { get; set; }
        public string FBillno { get; set; }
        public DateTime FDate { get; set; }
        public string FCustNmae { get; set; }
        public string FCustSum { get; set; }
        public DateTime FApproveDate { get; set; }
    }
}