using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Web;
namespace WebApi_SY.Models
{
    public class sli_work_order
    {
        [Key]
        public int Id { get; set; }
        public string Fbillno { get; set; }   //生产订单号
        public DateTime Fdate { get; set; }  //单据日期
        public decimal Fqty { get; set; }  //生产数量
        public decimal Fweight { get; set; }  //重量
        public DateTime Fplanstart { get; set; } //计划开始时间
        public DateTime Fplanend { get; set; }//计划结束时间
        public string Fordertype { get; set; }//生产订单类型
        
        public virtual ICollection<sli_work_orderEntry> sli_work_orderEntry { get; set; }
    }

    public class sli_work_orderEntry
    {
        //[Key]
        //public int Fentryid { get; set; } //生产订单分录号
        //public int Id { get; set; } //表头ID
        //public int Fseq { get; set; }//行号
        //public int Forderid { get; set; }//订单id
        //public int Forderentryid { get; set; }//订单分录ID
        //public int Fworkorderlistid { get; set; }  //工件列表ID

        [Key]
        public int Fentryid { get; set; }

        public int? Id { get; set; }
        public int? Fworkorderlistid { get; set; }
        public int? Fseq { get; set; }
        public int? Fqty { get; set; }
        public int? Fcommitqty { get; set; }
        public int? Forderid { get; set; }
        public int? Forderentryid { get; set; }
        public int? Fstatus { get; set; }
        public int? Fclosed { get; set; }
        [JsonIgnore]
        public virtual sli_work_order sli_work_order { get; set; }
    }


    public class sli_work_order_view
    {
        [Key]
        public int Id { get; set; }
        public string Fbillno { get; set; }   //生产订单号
        public DateTime Fdate { get; set; }  //单据日期
        public decimal Fqty { get; set; }  //生产数量
        public decimal Fweight { get; set; }  //重量
        public DateTime Fplanstart { get; set; } //计划开始时间
        public DateTime Fplanend { get; set; }//计划结束时间
        public string Fordertype { get; set; }//生产订单类型
        public int Fentryid { get; set; }
        //public virtual ICollection<sli_work_orderEntry> sli_work_orderEntry { get; set; }
    }
}