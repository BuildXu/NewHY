using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_quality_report
    {
        [Key]
        public int Id { get; set; }  // 主表Id
        public string Fnumber { get; set; }  // 请检单号
        public int? Fsourceid { get; set; }  // 源单Id
        public string Fworkorderlistid { get; set; }  // 工件Id
        public string Fmateril { get; set; }  // 物料
        public int? Fprocessobjectid { get; set; }  //  工步Id
        public string Fdescription { get; set; }  // 描述
        public int? Fstatus { get; set; }  // 状态Fprintstatus
        public int? Fprintstatus { get; set; }  // 状态


        public virtual ICollection<sli_quality_reportentry> sli_quality_reportentry { get; set; }
    }

    public class sli_quality_reportentry
    {
        //public int Fentryid { get; set; }  // 表体Id
        //public int Id { get; set; }  // 主表Id
        //public int? Fsourceid { get; set; }  // 源单Id (sli_work_processbillentry / Fentryid)

        //public int? Fobjectid { get; set; }  //  检验项 Id

        //public decimal Fqty { get; set; }  // 数量
        ////public int? Fstatus { get; set; }  // 状态


        //public string Fseq { get; set; }       //  行号
        //public string Ftype { get; set; }   // 检验类型
        //public string Fstandard { get; set; }  // 标准值
        //public string Fmax { get; set; }       // 上限
        //public string Fmin { get; set; }     //下限
        //public int? FqualityTools { get; set; }    //  检具
        //public string FqualityToolsName { get; set; }
        //public string Factual { get; set; }   //  实测值
        //public string Fdiff { get; set; }        // 公差值
        //public string FserialNumber { get; set; }  //  
        //public string Finspector { get; set; }   //  检验人
        //public DateTime Fdate { get; set; }      //检验日期




        [Key]
        public int Fentryid { get; set; }//表体Id
        // 对应表中的 Id 字段
        public int? Id { get; set; }// 主表Id
        // 对应表中的 Fobject 字段
        public int? Fobject { get; set; }//检验项 Id
        // 对应表中的 Fseq 字段
        public int Fseq { get; set; }//行号
        // 对应表中的 Ftype 字段
        public string Ftype { get; set; }//检验类型
        // 对应表中的 Fstandard 字段
        public string Fstandard { get; set; }// 标准值
        // 对应表中的 Fmax 字段
        public string Fmax { get; set; } // 上限
        // 对应表中的 Fmin 字段
        public string Fmin { get; set; }//下限
        // 对应表中的 Fqualitytools 字段
        public int? Fqualitytools { get; set; }//  检具编码
        // 对应表中的 Fqualitytoolsname 字段
        public string Fqualitytoolsname { get; set; }//  检具名称
        // 对应表中的 Factual 字段
        public string Factual { get; set; }//  实测值
        // 对应表中的 Fdiff 字段
        public string Fdiff { get; set; }// 公差值
        // 对应表中的 Fserialnumber 字段
        public string Fserialnumber { get; set; }
        // 对应表中的 Finspector 字段
        public string Finspector { get; set; }//  检验人
        // 对应表中的 Fdate 字段
        public DateTime Fdate { get; set; }//检验日期


        [JsonIgnore]
        public virtual sli_quality_report sli_quality_report { get; set; }
    }


    public class sli_quality_report_view
    {
        [Key]
        public int Id { get; set; }  // 主表Id
        public string Fnumber { get; set; }  // 请检单号
        public int? Fsourceid { get; set; }  // 源单Id
        public string Fworkorderlistid { get; set; }  // 工件Id
        public string Fmateril { get; set; }  // 物料  不要显示
        public int? Fprocessobjectid { get; set; }  //  工步Id
        public string Fdescription { get; set; }  // 描述
        public int? Fstatus { get; set; }  // 状态
        public int? Fprintstatus { get; set; }  // 打印状态
        public string Fprocessobjectnumber { get; set; }  // 工步编码
        public string Fprocessobjectname { get; set; }  // 工步名称
        public string Fproductno { get; set; }  // 工件编码
        public virtual ICollection<sli_quality_reportentry_view> sli_quality_reportentry_view { get; set; }
    }

    public class sli_quality_reportentry_view
    {
        [Key]
        public int Fentryid { get; set; }//表体Id
        // 对应表中的 Id 字段
        public int? Id { get; set; }// 主表Id
        // 对应表中的 Fobject 字段
        public int? Fobject { get; set; }//检验项 Id
        // 对应表中的 Fseq 字段
        public int ? Fseq { get; set; }//行号
        // 对应表中的 Ftype 字段
        public string Ftype { get; set; }//检验类型
        // 对应表中的 Fstandard 字段
        public string Fstandard { get; set; }// 标准值
        // 对应表中的 Fmax 字段
        public string Fmax { get; set; } // 上限
        // 对应表中的 Fmin 字段
        public string Fmin { get; set; }//下限
        // 对应表中的 Fqualitytools 字段
        public int? Fqualitytools { get; set; }//  检具
        // 对应表中的 Fqualitytoolsname 字段
        public string Fqualitytoolsname { get; set; }
        // 对应表中的 Factual 字段
        public string Factual { get; set; }//  实测值
        // 对应表中的 Fdiff 字段
        public string Fdiff { get; set; }// 公差值
        // 对应表中的 Fserialnumber 字段
        public string Fserialnumber { get; set; }
        // 对应表中的 Finspector 字段
        public string Finspector { get; set; }//  检验人
        // 对应表中的 Fdate 字段
        public DateTime ? Fdate { get; set; }//检验日期
        public string Fobjectnumber { get; set; }//检验项Number
        public string Fobjectname { get; set; }//检验项Name
        public virtual sli_quality_report_view sli_quality_report_view { get; set; }
    }
}