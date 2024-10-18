using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_prd_prudcutionPlanB
    {
        public int id { get; set; }
        public string fplanNumber { get; set; }
        public string fdate { get; set; }
        public string fstartDate { get; set; }
        public string fendDate { get; set; }
        public int fbillerId { get; set; }
        public int fdeptId { get; set; }
        public int fprocessId { get; set; }
        public int fmechineId { get; set; }
        public virtual ICollection<sli_prd_pruductionPlanEntryB> sli_prd_pruductionPlanEntryB { get; set; }
    }

    public class sli_prd_pruductionPlanEntryB
    {
        public int id { get; set; }
        public int fpruductionPlanId { get; set; }
        public int fworkOrderId { get; set; }
        public int frouteingCardId { get; set; }
        public int frouteingCardEntryId { get; set; }
        public decimal fqty { get; set; }
        public decimal fqtyTicket { get; set; }
        public int fStatus { get; set; }
        public int fprocessId { get; set; }
        public string fplanNumber { get; set; }
        public virtual sli_prd_prudcutionPlanB sli_prd_prudcutionPlanB { get; set; }
    }
}