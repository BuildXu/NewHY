using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_document_processbill_view
    {

        [Key]
        public int Fbillid { get; set; } // 主键ID
        public int Id { get; set; } // 主表ID

        public string Fnumber { get; set; } // 编号

        public string Fdate { get; set; } // 日期

        public string Ftaxtrue { get; set; } // 税务真实标记
     
        public int Fprocessid { get; set; } // 流程ID

        public string Fprocessname { get; set; } // 流程名称

        public string Fprocessnote { get; set; } // 流程备注

        public int Fdeptid { get; set; } // 部门ID

    }
}