using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_quality_report
    {
        public int Id { get; set; }  // 主表Id
        public string Fnumber { get; set; }  // 请检单号
        public int? Fsourceid { get; set; }  // 源单Id
        public string Fworkorderlistid { get; set; }  // 工件Id
        public string Fmateril { get; set; }  // 物料
        public int? Fprocessobjectid { get; set; }  //  工步Id
        public string Fdescription { get; set; }  // 描述
        public int? Fstatus { get; set; }  // 状态

        public virtual ICollection<sli_quality_reportentry> sli_quality_reportentry { get; set; }
    }

    public class sli_quality_reportentry
    {
        public int Fentryid { get; set; }  // 表体Id
        public int Id { get; set; }  // 主表Id
        public int? Fsourceid { get; set; }  // 源单Id (sli_work_processbillentry / Fentryid)

        public int? Fobjectid { get; set; }  //  检验项 Id

        public decimal Fqty { get; set; }  // 数量
        public int? Fstatus { get; set; }  // 状态


        public string Fseq { get; set; }       //  行号
        public string Ftype { get; set; }   // 检验类型
        public string Fstandard { get; set; }  // 标准值
        public string Fmax { get; set; }       // 上限
        public string Fmin { get; set; }     //下限
        public int? FqualityTools { get; set; }    //  检具
        public string FqualityToolsName { get; set; }
        public string Factual { get; set; }   //  实测值
        public string Fdiff { get; set; }        // 公差值
        public string FserialNumber { get; set; }  //  
        public string Finspector { get; set; }   //  检验人
        public DateTime Fdate { get; set; }      //检验日期
           
        public virtual sli_quality_report sli_quality_report { get; set; }
    }
}