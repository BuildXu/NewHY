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
        public int Fprocessobject { get; set; } //工步ID
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
        public int id { get; set; }
        // 对应s1.Fseq
        public int Fseq { get; set; }
        // 对应s1.Fworkorderlistid
        public int Fworkorderlistid { get; set; }
        public string Fname { get; set; }
        // 对应s1.Fprocessoption
        public int Fprocessoption { get; set; }
        // 对应s3.fname，即foptionname
        public string foptionname { get; set; }
        // 对应s1.Fstartdate
        public DateTime? Fstartdate { get; set; }
        // 对应s1.Fenddate
        public DateTime? Fenddate { get; set; }
        // 对应s1.Fqty
        public decimal Fqty { get; set; }
        // 对应s1.Fweight
        public decimal Fweight { get; set; }
        // 对应s1.Fcommitqty
        public decimal Fcommitqty { get; set; }
        // 对应s1.Fcommitweight
        public decimal Fcommitweight { get; set; }
        // 对应s1.Fstatus
        public int Fstatus { get; set; }
        public int Fsourceid { get; set; }
        //public int Fprocessobject { get; set; }

        //// 对应s2.Fbillid
        //public int Fbillid { get; set; }
        //// 对应s2.Fentryid
        //public int Fentryid { get; set; }
        //// 对应s2.Fseq，即Fentryseq
        //public int Fentryseq { get; set; }
        //// 对应s2.Fwobillid
        //public int Fwobillid { get; set; }
        //// 对应s2.Fprocessobject，即fobjectname
        //public int Fprocessobject { get; set; }
        //// 对应s4.fname
        //public string fobjectname { get; set; }
        //// 对应s2.Fstartdate，即Fentrystartdate
        //public DateTime? Fentrystartdate { get; set; }
        //// 对应s2.Fenddate，即Fentryenddate
        //public DateTime? Fentryenddate { get; set; }
        //// 对应s2.Fqty，即Fentryqty
        //public decimal Fentryqty { get; set; }
        //// 对应s2.Fweight，即Fentryweight
        //public decimal Fentryweight { get; set; }
        //// 对应s2.Fcommitqty，即Fentrycommitqty
        //public decimal Fentrycommitqty { get; set; }
        //// 对应s2.Fcommitweight，即Fentrycommitweight
        //public decimal Fentrycommitweight { get; set; }
        //// 对应s2.Fstatus，即Fentrystatus
        //public int Fentrystatus { get; set; }
    }

}