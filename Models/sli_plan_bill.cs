using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_plan_bill
    {
        public int Id { get; set; }
        public string Fplanlnumber { get; set; }
        public string Fissueddate { get; set; }
        public int Fplancontractentry { get; set; }
        public decimal Fqty { get; set; }
        public decimal Fweight { get; set; }
        public string Fplanbegindate { get; set; }
        public string Fplanenddate { get; set; }
        public string Factualbegindate { get; set; }
        public string Factualenddate { get; set; }
        public string Fnote { get; set; }
        public int Fdays { get; set; }
        public virtual ICollection<sli_plan_billlEntry> sli_plan_billlEntry { get; set; }
    }
    public class sli_plan_billlEntry
    {
        public int Id { get; set; }
        public int Fplanbillid { get; set; }
        public string Fplanoptionidid { get; set; }
        public decimal Fqty { get; set; }
        public decimal Fweight { get; set; }
        public string Fplanstartdate { get; set; }
        public string Fplanenddate { get; set; }
        public string Factualstartdate { get; set; }
        public string Factualenddate { get; set; }
        public int Fplandays { get; set; }
        public decimal Fcapacity { get; set; }
        public int Fdepartid { get; set; }
        public int Fempid { get; set; }
        public virtual sli_plan_bill sli_plan_bill { get; set; }
    }
}