using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_witnessing_order    {
        // 见证任务单号
        public string Fnumber { get; set; }
        // 见证任务Id
        [Key]
        public int Id { get; set; }
        // 日期
        public DateTime Fdate { get; set; }
        // 备注
        public string Fnote { get; set; }
        // 状态
        public int Fstatus { get; set; }
        public virtual ICollection<sli_witnessing_orderbill> sli_witnessing_orderbill { get; set; }
    }

    public class sli_witnessing_orderbill
    {
        // 见证明细Id
        [Key]
        public int Fentryid { get; set; }
        // 见证任务Id ---关联表头
        public int Id { get; set; }
        // 开始日期
        public DateTime ? Fstartdate { get; set; }
        // 结束日期
        public DateTime ? Fenddate { get; set; }
        // 工步id
        public int Fobject { get; set; }
        // 备注
        public string Fnote { get; set; }
        // 工件id
        public int Fworkorderlistid { get; set; }
        // 状态
        public int Fstatus { get; set; }
        public virtual sli_witnessing_order sli_witnessing_order { get; set; }
    }
}