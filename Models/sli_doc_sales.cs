using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_doc_sales
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        public int? Fid { get; set; }
        public string Fbillno { get; set; }
        public string Fdate { get; set; }
        public string Fslimetal { get; set; }
        public string Fqty { get; set; }
        public decimal? Fweight { get; set; }
        public string Fnote { get; set; }
        public string Fnotice { get; set; }
        public int? Fstatus { get; set; }
        public int? Fdocid { get; set; }
        public string Fdocno { get; set; }
        public string Fstandard { get; set; }
    }
}