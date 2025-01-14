using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_quality_request
    {
        public int id { get; set; }
        public string fplanNumber { get; set; }
        public string fdate { get; set; }
        public string fstartDate { get; set; }
        public string fendDate { get; set; }
        public int fbillerId { get; set; }
        public int fdeptId { get; set; }
        public int Fempid { get; set; }
        public int Fstatus { get; set; }

        public virtual ICollection<sli_quality_requestEntry> sli_quality_requestEntry { get; set; }
    }

    public class sli_quality_requestEntry
    {
        public int Fentryid { get; set; }
        public int Id { get; set; }
        public int Fsourceid { get; set; }
        public int Fworkorderlistid { get; set; }
      
        public decimal fqty { get; set; }
        public int fStatus { get; set; }

        public virtual sli_quality_request sli_quality_request { get; set; }

    }
}