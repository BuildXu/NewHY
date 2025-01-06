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
        public int Id { get; set; }
        public int Fseq { get; set; }   
        public int Fworkorderlistid { get; set; }   //工件ID
        public int Fprocessoption { get; set; }
        public DateTime Fstartdate { get; set; }
        public DateTime Fenddate { get; set; }
        public decimal Fqty { get; set; }
        public decimal Fweight { get; set; }
        public decimal Fcommitqty { get; set; }
        public decimal Fcommitweight { get; set; }
        public int Fstatus { get; set; }
        public virtual ICollection<sli_work_processBillEntry> sli_work_processBillEntry { get; set; }
    }


    public class sli_work_processBillEntry
    {
        public int Fbillid { get; set; }
        [Key]
        public int Fentryid { get; set; }
        public int Fseq { get; set; }
        public int Fwobillid { get; set; }
        public int Fprocessobject { get; set; }
        public DateTime Fstartdate { get; set; }
        public DateTime Fenddate { get; set; }
        public decimal Fqty { get; set; }
        public decimal Fweight { get; set; }
        public decimal Fcommitqty { get; set; }
        public decimal Fcommitweight { get; set; }
        public int Fstatus { get; set; }
        //[JsonIgnore]
        public virtual sli_work_processBill sli_work_processBill { get; set; }
    }
}