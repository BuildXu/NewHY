using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_document_process_view
    {
        [Key]
        public int Fentryid { get; set; } // 主键ID
        public int Id { get; set; } //主表ID

        public string Fnumber { get; set; } // 工艺档案编号

        public string Fdate { get; set; } // 日期

        public string Ftaxtrue { get; set; } // 材质

        public int Fprocessid { get; set; } // 工序ID
        public int Fprocessoption { get; set; }  // 工序ID (给下游传参)


        public string Fprocessname { get; set; } // 工序名称

        public string Fprocessnote { get; set; } // 工序备注

        public int Fdeptid { get; set; } // 部门ID

        public int Fstepid { get; set; } // 工步ID

        public string Fstepname { get; set; } // 工步名称

        public string Fstepnote { get; set; } // 工步说明
    }
}