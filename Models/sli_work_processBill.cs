using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.RightsManagement;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_work_processBill
    {
        public int Fwoentryid { get; set; }   //sli_work_order ID
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

        //public string Forderno { get; set; }//销售单号
        //public string Fproductno { get; set; }//工件号
        //public string Fpname { get; set; }//产品名称
        //public string Fdescription { get; set; }//产品规格
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

        public int Fqualityoption { get; set; } //*******************新增 检验方案  存放 sli_document_quality_sorderBill /Id

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
        public int? Fdeptid { get; set; }
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
        // 所有值类型改为可空
        [Key]
        public int? id { get; set; }
        public int? Fseq { get; set; }
        public string Fslimetal { get; set; } // string 默认可空
        public int? Fworkorderlistid { get; set; }
        public string Fname { get; set; }
        public string Fwobillno { get; set; }
        public int? Fprocessoption { get; set; }
        public string foptionname { get; set; }
        public DateTime? Fstartdate { get; set; }
        public DateTime? Fenddate { get; set; }
        public decimal? Fqty { get; set; }
        public decimal? Fweight { get; set; }
        public decimal? Fweights { get; set; }
        public decimal? Fcommitqty { get; set; }
        public decimal? Fcommitweight { get; set; }
        public int? Fstatus { get; set; }
        public int? Fsourceid { get; set; }
        public string Forderno { get; set; }
        public string Fproductno { get; set; }
        public string Fpname { get; set; }
        public string Fdescription { get; set; }
        public string Fothers { get; set; }
        public virtual ICollection<sli_work_processBillEntry_view> sli_work_processBillEntry_view { get; set; }
    }

    public class sli_work_processBillEntry_view
    {
        public int? Fbillid { get; set; }
        [Key]
        public int? Fentryid { get; set; }
        public int? Fseq { get; set; }
        public int? Fwobillid { get; set; }
        public int? Fworkorderlistid { get; set; }
        public string Fproductno { get; set; }
        public string  Fmaterialnumber { get; set; } // 修正为可空 int
        public string Fmaterialname { get; set; }
        public string Fdescription { get; set; }
        public int? Fprocessobject { get; set; }
        public string Fprocessobjectnumber { get; set; }
        public string Fprocessobjectname { get; set; }
        public DateTime? Fstartdate { get; set; }
        public DateTime? Fenddate { get; set; }
        public decimal? Fqty { get; set; }
        public decimal? Fweight { get; set; }
        public decimal? Fcommitqty { get; set; }
        public decimal? Fcommitweight { get; set; }
        public int? Fqualityoption { get; set; }
        public int? Fstatus { get; set; }
        public virtual sli_work_processBill_view sli_work_processBill_view { get; set; }
    }
    public class sli_wo_view
    {
        public int? Id { get; set; }
        public string Fcustname { get; set; }          // 数据库字段: Fcustname (varchar50)
        public string Fbillno { get; set; }            // 数据库字段: Fbillno (varchar50)
        public string Forderno { get; set; }           // 数据库字段: Forderno (varchar50)
        public string Fnotes { get; set; }             // 数据库字段: Fnotes (varchar500)
        public DateTime? Fdate { get; set; }            // 数据库字段: Fdate (datetime)
        public decimal? Fqty { get; set; }              // 数据库字段: Fqty (numeric18,2)
        public decimal? Fweight { get; set; }            // 数据库字段: Fweight (numeric18,4)
        public DateTime? Fplanstart { get; set; }        // 数据库字段: Fplanstart (datetime)
        public DateTime? Fplanend { get; set; }          // 数据库字段: Fplanend (datetime)
        public string Fordertype { get; set; }         // 数据库字段: Fordertype (varchar50)
        public int? Fforgeqty { get; set; }              // 数据库字段: Fforgeqty (int)
        public decimal? Fforgeweight { get; set; }       // 数据库字段: Fforgeweight (decimal18,2)
        public string Fname { get; set; }               // 数据库字段: Fname (varchar500)
        public string Fslimetal { get; set; }           // 数据库字段: Fslimetal (varchar500)
        public string Fdescription { get; set; }        // 数据库字段: Fdescription (varchar500)
        public string Fslidrawingno { get; set; }       // 数据库字段: Fslidrawingno (varchar500)
        public string Fsliheattreatment { get; set; }   // 数据库字段: Fsliheattreatment (varchar500)
        public string Fsliexplanation { get; set; }     // 数据库字段: Fsliexplanation (varchar500)

        // P1-P8 参数组（严格匹配数据库字段名）
        public string Fp1name { get; set; }             // 数据库字段: Fp1name (varchar50)
        public string Fp1status { get; set; }           // 数据库字段: Fp1status (原为 int，现改为 string)
        public string Fp2name { get; set; }             // 数据库字段: Fp2name (varchar50)
        public string Fp2status { get; set; }           // 数据库字段: Fp2status
        public string Fp3name { get; set; }             // 数据库字段: Fp3name
        public string Fp3status { get; set; }           // 数据库字段: Fp3status
        public string Fp4name { get; set; }             // 数据库字段: Fp4name
        public string Fp4status { get; set; }           // 数据库字段: Fp4status
        public string Fp5name { get; set; }             // 数据库字段: Fp5name
        public string Fp5status { get; set; }           // 数据库字段: Fp5status
        public string Fp6name { get; set; }             // 数据库字段: Fp6name
        public string Fp6status { get; set; }           // 数据库字段: Fp6status
        public string Fp7name { get; set; }             // 数据库字段: Fp7name
        public string Fp7status { get; set; }           // 数据库字段: Fp7status
        public string Fp8name { get; set; }             // 数据库字段: Fp8name
        public string Fp8status { get; set; }           // 数据库字段: Fp8status
    }


}