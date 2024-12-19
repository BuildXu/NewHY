using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_sal_orders_view
    {
        [Key]
        public int Fid { get; set; }
        public string Fbillno { get; set; }
        public string Fdate { get; set; }
        public int Fcustid { get; set; }
        public string Fcustno { get; set; }
        public string Fcustname { get; set; }
        public string Fcustomer { get; set; }
        public int Orderid { get; set; }
        [Key]
        public int Fentryid { get; set; }
        public int Fseq { get; set; }
        public decimal Fqty { get; set; }
        public string Fnote { get; set; }
        public string Fplandeliverydate { get; set; }
        public decimal Fstockqty { get; set; }
        public int Fmaterialid { get; set; }
        public string Fnumber { get; set; }
        public string Fname { get; set; }
        public string Fdescription { get; set; }
        public decimal Fsliouterdiameter { get; set; }
        public decimal Fsliinnerdiameter { get; set; }
        public decimal Fslihight { get; set; }
        public decimal Fsliallowanceod { get; set; }
        public decimal Fsliallowanceid { get; set; }
        public decimal Fsliallowanceh { get; set; }
        public decimal Fsliweightmaterial { get; set; }
        public decimal Fsliweightforging { get; set; }
        public decimal Fsliweightgoods { get; set; }
        public string Fslirawingno { get; set; }
        public string Fslimetal { get; set; }
        public string Fsligoodsstatus { get; set; }
        public string Fsliprocessing { get; set; }
        public string Fsliedelivery { get; set; }
        public string Fsliblankmodel { get; set; }
        public string Fslipunching { get; set; }
        public decimal Fslimould { get; set; }
        public decimal Fsliroller { get; set; }
        public int Fsliheatingtimes { get; set; }
        public string Fsligrade { get; set; }
        public string Fsumnumber { get; set; }
    }
}