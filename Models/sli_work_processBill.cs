using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_work_processBill
    {
        public int Fwoentryid { get; set; }   //sli_work_order ID
        [Key]
        public int Id { get; set; }//主键ID
        public int Fseq { get; set; }   //序号
        public int Fworkorderlistid { get; set; }   //工件ID
        public int Fprocessoption { get; set; } //工序ID
        public DateTime? Fstartdate { get; set; }//开始时间
        public DateTime? Fenddate { get; set; }//结束时间
        public decimal Fqty { get; set; }//数量
        public decimal Fweight { get; set; }//重量
        public decimal Fcommitqty { get; set; }//下游关联数量
        public decimal Fcommitweight { get; set; }//下游关联重量
        public int Fstatus { get; set; }//工序状态
        public virtual ICollection<sli_work_processBillEntry> sli_work_processBillEntry { get; set; }
    }


    public class sli_work_processBillEntry
    {

        public int Fbillid { get; set; }//sli_work_processBill ID
        [Key]
        public int Fentryid { get; set; }//主键ID
        public int Fseq { get; set; } //序号
        public int Fwobillid { get; set; }
        public int Fworkorderlistid { get; set; }  //*******************1.14 新增加上去的
        public int Fprocessobject { get; set; } //工步ID

        public int ? Fqualityoption { get; set; } //*******************新增 检验方案  存放 sli_document_quality_sorderBill /Id

        public DateTime? Fstartdate { get; set; }//开始时间
        public DateTime? Fenddate { get; set; }//结束时间
        public decimal Fqty { get; set; } //数量
        public decimal Fweight { get; set; }//重量
        public decimal Fcommitqty { get; set; }//下游关联数量
        public decimal Fcommitweight { get; set; }//下游关联数量
        public int Fstatus { get; set; }//工步状态
        //[JsonIgnore]
        public virtual sli_work_processBill sli_work_processBill { get; set; }
    }

    public class WorkOrderListIds
    {
        public int id { get; set; }
        public int Fqty { get; set; }
        public int Fweight { get; set; }
    }

    public class SliDocumentProcessModelBillEntryView
    {
        public int Fentryid { get; set; }
        public int Fbillid { get; set; }
        public int Fobjectid { get; set; }
        public string Fmax { get; set; }
        public string Fmin { get; set; }
        public string Ftarget { get; set; }
        public string Fnote { get; set; }
        public string Fnoties { get; set; }
        public string Fexplanation { get; set; }
    }

    public class SliWorkorderlistView
    {
        public int Fbillid { get; set; }
        public int Id { get; set; }
        public int Foptionid { get; set; }
        public int Fdeptid { get; set; }
        public string Fnote { get; set; }
        public int Fstatus { get; set; }
        public List<SliDocumentProcessModelBillEntryView> sli_document_process_modelBillEntry_view { get; set; }
    }

    //public class RootObject
    //{
    //    public List<WorkOrderListIds> Fworkordlistid { get; set; }
    //    public List<SliWorkorderlistView> sli_workorderlist_view { get; set; }
    //}
    public class sli_work_processBill_view
    {
        // 对应s1.id
        //public int id { get; set; }
        //// 对应s1.Fseq
        //public int Fseq { get; set; }
        //// 对应s1.Fworkorderlistid
        //public int Fworkorderlistid { get; set; }
        //public string Fname { get; set; }
        //// 对应s1.Fprocessoption
        //public int Fprocessoption { get; set; }
        //// 对应s3.fname，即foptionname
        //public string foptionname { get; set; }
        //// 对应s1.Fstartdate
        //public DateTime? Fstartdate { get; set; }
        //// 对应s1.Fenddate
        //public DateTime? Fenddate { get; set; }
        //// 对应s1.Fqty
        //public decimal Fqty { get; set; }
        //// 对应s1.Fweight
        //public decimal Fweight { get; set; }
        //// 对应s1.Fcommitqty
        //public decimal Fcommitqty { get; set; }
        //// 对应s1.Fcommitweight
        //public decimal Fcommitweight { get; set; }
        //// 对应s1.Fstatus
        //public int Fstatus { get; set; }
        //public int Fsourceid { get; set; }
        // 主键
        public int Id { get; set; }

        // 可为空的整数
        public int? Fseq { get; set; }

        // 非空整数
        public int Fworkorderlistid { get; set; }

        // 可为空的字符串
        public string Fname { get; set; }

        // 非空整数
        public int Fprocessoption { get; set; }

        // 可为空的字符串
        public string Foptionname { get; set; }

        // 可为空的日期时间
        public DateTime? Fstartdate { get; set; }

        // 可为空的日期时间
        public DateTime? Fenddate { get; set; }

        // 可为空的数值类型（18位精度，2位小数）
        public decimal? Fqty { get; set; }

        // 可为空的数值类型（18位精度，2位小数）
        public decimal? Fweight { get; set; }

        // 可为空的数值类型（18位精度，2位小数）
        public decimal? Fcommitqty { get; set; }

        // 可为空的数值类型（18位精度，2位小数）
        public decimal? Fcommitweight { get; set; }

        // 可为空的整数
        public int? Fstatus { get; set; }

        // 非空整数
        public int Fsourceid { get; set; }


        public virtual ICollection<sli_work_processBillEntry_view> sli_work_processBillEntry_view { get; set; }
    }

    public class sli_work_processBillEntry_view
    {
        //// 对应 s1.Fbillid
        //public int Fbillid { get; set; }   // 关联主表视图ID
        //// 对应 s1.Fentryid
        //[Key]
        //public int Fentryid { get; set; }// 分录ID
        //// 对应 s1.Fseq
        //public int ? Fseq { get; set; } //行号
        //// 对应 s1.Fwobillid
        //public int ? Fwobillid { get; set; } //
        //// 对应 s1.Fworkorderlistid
        //public int ? Fworkorderlistid { get; set; } //工件ID
        //// 对应 s4.Fproductno
        //public string Fproductno { get; set; } //物料代码  Fmaterialname
        //public string Fmaterialnumber { get; set; } //物料代码  Fmaterialname

        //public string Fmaterialname { get; set; } //物料名称  Fdescription

        //public string Fdescription { get; set; } //物料名称  Fdescription
        //public int Fprocessobject { get; set; } //工步ID
        //// 对应 s1.Fprocessobject
        //public string Fprocessobjectnumber { get; set; } //工步代码
        //// 对应 s3.fname
        //public string Fprocessobjectname { get; set; }//工步名称
        //// 对应 s1.Fstartdate
        //public DateTime ? Fstartdate { get; set; }//开始时间
        //// 对应 s1.Fenddate
        //public DateTime ? Fenddate { get; set; }//结束时间
        //// 对应 s1.Fqty
        //public decimal Fqty { get; set; }//数量
        //// 对应 s1.Fweight
        //public decimal Fweight { get; set; }//重量
        //// 对应 s1.Fcommitqty
        //public decimal Fcommitqty { get; set; }
        //// 对应 s1.Fcommitweight
        //public decimal Fcommitweight { get; set; }
        //// 对应 s1.Fqualityoption
        //public int Fqualityoption { get; set; }
        //// 对应 s1.Fstatus
        //public int Fstatus { get; set; }//状态

        public int? Fbillid { get; set; }
        [Key]
        // 非空整数
        public int Fentryid { get; set; }

        // 可为空的整数
        public int? Fseq { get; set; }

        // 可为空的整数
        public int? Fwobillid { get; set; }

        // 可为空的整数
        public int? Fworkorderlistid { get; set; }

        // 可为空的字符串
        public string Fproductno { get; set; }

        // 可为空的整数
        public int? Fmaterialnumber { get; set; }

        // 可为空的字符串
        public string Fmaterialname { get; set; }

        // 可为空的字符串
        public string Fdescription { get; set; }

        // 可为空的整数
        public int? Fprocessobject { get; set; }

        // 可为空的字符串
        public string Fprocessobjectnumber { get; set; }

        // 可为空的字符串
        public string Fprocessobjectname { get; set; }

        // 可为空的日期时间
        public DateTime? Fstartdate { get; set; }

        // 可为空的日期时间
        public DateTime? Fenddate { get; set; }

        // 可为空的数值类型（18位精度，2位小数）
        public decimal? Fqty { get; set; }

        // 可为空的数值类型（18位精度，2位小数）
        public decimal? Fweight { get; set; }

        // 可为空的数值类型（18位精度，2位小数）
        public decimal? Fcommitqty { get; set; }

        // 可为空的数值类型（18位精度，2位小数）
        public decimal? Fcommitweight { get; set; }

        // 可为空的整数
        public int? Fqualityoption { get; set; }

        // 可为空的整数
        public int? Fstatus { get; set; }
        public virtual sli_work_processBill_view sli_work_processBill_view { get; set; }
    }

}