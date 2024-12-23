using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace WebApi_SY.Models
{
    public class sli_workorder
    {
        public int Id { get; set; }
        public string Fworkbillnumber { get; set; }
        public string Fdate { get; set; }
        public string Fbegindateplan { get; set; }
        public string Fenddateplan { get; set; }
        public decimal Fqtymain { get; set; }
        public decimal Fqtyfinishedmain { get; set; }
        public decimal Fqtyscrapedmain { get; set; }
        public decimal Fweightmain { get; set; }
        public decimal Fweightfinishedmain { get; set; }
        public decimal Fweightscrapedmain { get; set; }
        public string Fnotes { get; set; }
        public int Fworkprocessid { get; set; }
        public int Fworkrequisitionid { get; set; }
        public string Ftickettype { get; set; }
        public virtual ICollection<sli_workorderentry> sli_workorderentry { get; set; }
    }

    public class sli_workorderentry
    {
        public int Id { get; set; }
        public int Fworklistid { get; set; }
        public string Forderid { get; set; }
        public int Forderentryid { get; set; }
        public int Frownumber { get; set; }
        public int Forderrownumber { get; set; }
        public int Fmaterialid { get; set; }
        public decimal Fqty { get; set; }
        public decimal Fqtyfinished { get; set; }
        public decimal Fqtyscraped { get; set; }
        public decimal Fweight { get; set; }
        public decimal Fweightfinished { get; set; }
        public decimal Fweightscraped { get; set; }
        public decimal sli_workrequisitionid { get; set; }
        public decimal sli_workprocessid { get; set; }
        public virtual sli_workorder sli_workorder { get; set; }
    }
}