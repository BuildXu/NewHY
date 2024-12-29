using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    using System;
    using System.Collections.Generic;

    public class sli_work_processbill
    {
        public int Id { get; set; }
        public int Fwoentryid { get; set; }
        public int Fseq { get; set; }
        public int Fworkorderlistid { get; set; }
        public int Fprocessoption { get; set; }
        public DateTime Fstartdate { get; set; }
        public DateTime Fenddate { get; set; }
        public decimal Fqty { get; set; }
        public decimal Fweight { get; set; }
        public decimal Fcommitqty { get; set; }
        public decimal Fcommitweight { get; set; }
        public int Fstatus { get; set; }

        // 导航属性，关联到sli_work_processBillentry集合
        public virtual ICollection<sli_work_processbillentry> sli_work_processbillentry { get; set; }
    }

    public class sli_work_processbillentry
    {

        public int Fbillid { get; set; }
        public int Fentryid { get; set; }
        public int Fseq { get; set; }
        public int Fwobillid { get; set; }
        public int Fprocessobject { get; set; }
        public DateTime Fstartdate { get; set; }
        public DateTime Fenddate { get; set; }
        public decimal Fqty { get; set; }
        public decimal Fweight { get; set; }
        public decimal Fcommitqty { get; set; }
        public decimal Fcommitweight { get; set; }
        public int Fstatus { get; set; }

        // 导航属性，关联到sli_work_processbill
        public virtual sli_work_processbill sli_work_processbill { get; set; }
    }
}