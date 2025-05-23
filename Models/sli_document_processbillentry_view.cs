﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_document_processbillentry_view
    {
        [Key]
        public int Fentryid { get; set; } // 主键ID

        public int Fprocessid { get; set; } // 流程ID

        public string Fprocessname { get; set; } // 流程名称

        public string Fprocessnote { get; set; } // 流程备注

        public int Fdeptid { get; set; } // 部门ID

        public int Fstepid { get; set; } // 步骤ID

        public string Fstepname { get; set; } // 步骤名称

        public string Fstepnote { get; set; } // 步骤备注
    }
}