using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Web;
namespace WebApi_SY.Models
{
    public class sli_work_order
    {
        [Key]
        public int Id { get; set; }
        public int? Fmeterialid { get; set; } //物料ID
        public string Fbillno { get; set; }   //生产订单号
        public string Forderno { get; set; }   //工作令号

        public string Fnotes { get; set; }   //备注
        public DateTime ? Fdate { get; set; }  //单据日期
        public decimal Fqty { get; set; }  //生产数量
        public decimal Fweight { get; set; }  //重量
        public DateTime ? Fplanstart { get; set; } //计划开始时间
        public DateTime ? Fplanend { get; set; }//计划结束时间
        public string Fordertype { get; set; }//生产订单类型
        public string Fforgeqty { get; set; }//合件锻、产品数量合并到表头
        public string Fforgeweight { get; set; }//合件锻、产品重量合并到表头
        public int? Fstatus { get; set; } //状态

        public string Fname { get; set; }
        public string Fslimetal { get; set; }
        public string Fdescription { get; set; }
        public string Fslidrawingno { get; set; }
        public string Fsliheattreatment { get; set; }
        public string Fsliexplanation { get; set; }

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

    public class sli_quality_techmetal_view   //订单技术方案--化学成分
    {
        [Key]
        public int FWorkOrderListId { get; set; }    

        public int FEntryId { get; set; } 
        public string FNumber { get; set; }//化学成分代码
        public string FName { get; set; }//化学成分名称
        public string FMin { get; set; }//最小值
        public string FMax { get; set; }//最大值
        public string FTarget { get; set; }//目标值
    }

    public class sli_quality_pur_view   //原材料--化学成分
    {
        public int Id { get; set; }  // 注意：数据库允许 NULL，建议检查是否需要主键约束
        public string Fheatnumber { get; set; }   //炉号 
        public string Fitems { get; set; }  //化学成分名称
        public string Fvalue { get; set; }//化学值  FBatchNo
        public string FBatchNo { get; set; }//批号  FBatchNo
        public int Fsid { get; set; }
    }
}