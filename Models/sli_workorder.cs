using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_workorder
    {
        public int ID { get; set; }
        public string FworkBillNumber { get; set; }
        public string Fdate { get; set; }
        public string FbeginDatePlan { get; set; }
        public string FendDatePlan { get; set; }
        public decimal FqtyMain { get; set; }
        public decimal FqtyFinishedMain { get; set; }
        public decimal FqtyScrapedMain { get; set; }
        public decimal FweightMain { get; set; }
        public decimal FweightFinishedMain { get; set; }
        public decimal FweightScrapedMain { get; set; }
        public string Fnotes { get; set; }
        public int FworkProcessId { get; set; }
        public int FworkRequisitionId { get; set; }
        public string FticketType { get; set; }
        public virtual ICollection<sli_workorderentry> sli_workorderentry { get; set; }
    }
    public class sli_workorderentry
    {
        public int ID { get; set; }
        public int FworkListId { get; set; }
        public string ForderId { get; set; }
        public int ForderEntryid { get; set; }
        public int FrowNumber { get; set; }
        public int ForderRowNumber { get; set; }
        public int Fmaterialid { get; set; }
        public decimal Fqty { get; set; }
        public decimal FqtyFinished { get; set; }
        public decimal FqtyScraped { get; set; }
        public decimal Fweight { get; set; }
        public decimal FweightFinished { get; set; }
        public decimal FweightScraped { get; set; }
        public decimal Sli_workRequisitionId { get; set; }
        public decimal Sli_workProcessId { get; set; }
        public virtual sli_workorder sli_workorder { get; set; }
    }
}