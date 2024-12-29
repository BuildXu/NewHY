using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_manu_plan
    {
        public int Id { get; set; }
        public string Fbillno { get; set; }
        public DateTime Fdate { get; set; }
        public DateTime Fdatestart { get; set; }
        public DateTime Fdateend { get; set; }
        public int Fbiller { get; set; }
        public int Fstatus { get; set; }

        public virtual ICollection<sli_manu_planentry> sli_manu_planentry { get; set; }
    }

    public class sli_manu_planentry
    {
        [Key]
        public int Fentryid { get; set; }
        public int Id { get; set; }
        public int Fsourceentryid { get; set; }
        public DateTime Fdatestart { get; set; }
        public DateTime Fdateend { get; set; }
        public int Fstatus { get; set; }

        public virtual sli_manu_plan sli_manu_plan { get; set; }
    }
}