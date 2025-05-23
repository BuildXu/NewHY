﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_bd_mech_break
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string Fnumber { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Fname { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Fnote { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Fstatus { get; set; }

        /// <summary>
        /// 是否使用
        /// </summary>
        public bool Fused { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime FcreateDate { get; set; }
    }
}