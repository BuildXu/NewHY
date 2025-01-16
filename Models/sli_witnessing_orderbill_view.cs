using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_witnessing_orderbill_view
    {
        // 见证任务单号
        public string Fnumber { get; set; }
        // 日期
        public DateTime Fdate { get; set; }
        // 工件号
        public string Fproductno { get; set; }
        // 客户名称
        public string Fcustname { get; set; }
        // 客户信息
        public string Fcustomer { get; set; }
        // 物料名称
        public string Fmaterial { get; set; }
        // 物料规格
        public string Fdescription { get; set; }
        // 见证任务行Id
        public int Fentryid { get; set; }
        // 见证任务单Id
        public int Id { get; set; }
        // 开始日期
        public DateTime Fstartdate { get; set; }
        // 结束日期
        public DateTime Fenddate { get; set; }
        //工步id
        public int Fobject { get; set; }
        // 备注
        public string Fnote { get; set; }
        // 工件id
        public int Fworkorderlistid { get; set; }
        // 状态
        public int Fstatus { get; set; }
        //工步名
        public string Fobjectname { get; set; }
    }
}