using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_manu_plan
    {

            // 唯一标识符
            public int Id { get; set; }
            // 单据编号
            public string Fbillno { get; set; }
            // 日期
            public DateTime Fdate { get; set; }
            // 计划开始日期
            public DateTime Fdatestart { get; set; }
            // 计划结束日期
            public DateTime Fdateend { get; set; }
            // 制单人
            public int Fbiller { get; set; }
            // 状态
            public int Fstatus { get; set; }

            // 与 sli_manu_planentry 的一对多关系集合
            public virtual ICollection<sli_manu_planentry> sli_manu_planentry { get; set; }
        }

        public class sli_manu_planentry
        {
            // 主键
            [Key]
            public int Fentryid { get; set; }
            // 标识符---  关联表头
            public int Id { get; set; }
            // 源单行id
            public int Fsourceentryid { get; set; }
            // 开始日期
            public DateTime Fdatestart { get; set; }
            // 结束日期
            public DateTime Fdateend { get; set; }
            // 状态
            public int Fstatus { get; set; }

            // 与 sli_manu_plan 的多对一关系
            public virtual sli_manu_plan sli_manu_plan { get; set; }
        }
    
}