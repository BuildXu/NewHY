using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace WebApi_SY.Models
{
    public class sli_work_order
    {
        public int Id { get; set; }
        public string Fbillno { get; set; }
        public DateTime Fdate { get; set; }
        public decimal Fqty { get; set; }
        public decimal Fweight { get; set; }
        public DateTime Fplanstart { get; set; }
        public DateTime Fplanend { get; set; }
        public string Fordertype { get; set; }
        
        public virtual ICollection<sli_work_orderEntry> sli_work_orderEntry { get; set; }
    }

    public class sli_work_orderEntry
    {
        public int Fentryid { get; set; }
        public int Id { get; set; }
        public int Fseq { get; set; }
        public int Forderid { get; set; }
        public int Forderentryid { get; set; }
        public int Fworkorderlistid { get; set; }
        
        public virtual sli_work_order sli_work_order { get; set; }
    }
}