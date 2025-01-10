using System;
using System.Collections.Generic;

namespace WebApi_SY.Models
{
    public class sli_plan_bill
    {
        public int Id { get; set; }  // 主表Id
        public string Fplanlnumber { get; set; }// 计划单代码
        public string Fissueddate { get; set; }// 发布日期
        public int Fplancontractentry { get; set; }// 订单行id
        public decimal Fqty { get; set; }//  订单数量
        public decimal Fweight { get; set; }// 订单重量
        public string Fplanbegindate { get; set; }// 计划开始
        public string Fplanenddate { get; set; }//计划完工
        public string Factualbegindate { get; set; }// 实际开始 （空着）
        public string Factualenddate { get; set; }//  实际完工 （空着）
        public string Fnote { get; set; }// 计划说明
        public int Fdays { get; set; }// 计划天数  从前台带过来

        public virtual ICollection<sli_plan_billEntry> sli_plan_billEntry { get; set; }
        public virtual ICollection<sli_plan_billorder> sli_plan_billorder { get; set; } // 新增导航属性
    }

    public class sli_plan_billEntry
    {
        public int Id { get; set; }// 计划表Id
        public int Fplanbillid { get; set; }// 主表Id
        public int Fplanoptionidid { get; set; }// 工序Id
        public decimal Fqty { get; set; }//数量
        public decimal Fweight { get; set; }//重量
        public string Fplanstartdate { get; set; }//计划开始 (计算表头总天数,)
        public string Fplanenddate { get; set; }//计划完成 
        public string Factualstartdate { get; set; }
        public string Factualenddate { get; set; }
        public int Fplandays { get; set; }
        public decimal Fcapacity { get; set; }
        public int Fdepartid { get; set; }
        public int Fempid { get; set; }

        public virtual sli_plan_bill sli_plan_bill { get; set; }
    }

    public class sli_plan_billorder
    {
        public int Id { get; set; }
        public int Fplanbillid { get; set; } // 外键，关联到 SliPlanBill
        //public string Fplanoptionid { get; set; }
        public int Forderentryid { get; set; }
        public int Fstatus { get; set; }

        public virtual sli_plan_bill sli_plan_bill { get; set; } // 导航属性
    }
}