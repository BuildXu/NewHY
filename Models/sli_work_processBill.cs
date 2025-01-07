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
        public DateTime ? Fstartdate { get; set; }//开始时间
        public DateTime ? Fenddate { get; set; }//结束时间
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
        public DateTime ? Fstartdate { get; set; }//开始时间
        public DateTime ? Fenddate { get; set; }//结束时间
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
        public int[] Fworkordlistid { get; set; }
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
}