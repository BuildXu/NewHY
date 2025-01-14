using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_quality_request
    {
        public int id { get; set; }      //  主表Id
        public string fnumber { get; set; }  // 请检单号
        public DateTime Fdate { get; set; }    // 单据日期

        public DateTime FendDate { get; set; }   // 计划完成日期
        //public int fbillerId { get; set; }
        public int FdeptId { get; set; }   // 请检部门
        public int Fempid { get; set; }   // 请检人
        public int Fstatus { get; set; }  //  状态

        public virtual ICollection<sli_quality_requestEntry> sli_quality_requestEntry { get; set; }
    }

    public class sli_quality_requestEntry
    {
        public int Fentryid { get; set; }  //  表体Id
        public int Id { get; set; }        // 主表Id
        public int Fsourceid { get; set; }  // 源单Id  (sli_work_processbillentry / Fentryid)
        public int Fworkorderlistid { get; set; }   //  工件Id   (sli_work_processbillentry / Fworkorderlistid) *****  上源需添加

        public decimal fqty { get; set; }   // 数量
        public int fStatus { get; set; }   //  状态

        public virtual sli_quality_request sli_quality_request { get; set; }

    }
}