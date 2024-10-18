using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_prd_processReport
    {
        public int id { get; set; }
        public string fplanlNumber { get; set; }
        public string fissuedDate { get; set; }
        public int fplanContractEntry { get; set; }
        public decimal fqty { get; set; }
        public decimal fweight { get; set; }
        public string fplanBeginDate { get; set; }
        public string fplanEndDate { get; set; }
        public string factualBeginDate { get; set; }
        public string factualEndDate { get; set; }
        public string fnote { get; set; }
        public int fdays { get; set; }
        public virtual ICollection<sli_plan_billlEntry> sli_plan_billlEntry { get; set; }
    }
    public class sli_prd_processReportEntry
    {
        public int id { get; set; }
        public int fplanBillId { get; set; }
        public string fplanOptionIdId { get; set; }
        public decimal fqty { get; set; }
        public decimal fweight { get; set; }
        public string fplanStartDate { get; set; }
        public string fplanEndDate { get; set; }
        public string factualStartDate { get; set; }
        public string factualEndDate { get; set; }
        public int fPlanDays { get; set; }
        public decimal fcapacity { get; set; }
        public int fdepartID { get; set; }
        public int fempId { get; set; }
        public virtual sli_plan_bill sli_plan_bill { get; set; }
    }
}