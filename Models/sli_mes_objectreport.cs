﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
   
        public class sli_mes_objectreport
        {


        /// <summary>
        /// 工步报工单号，每次新增给一个统一单号
        /// </summary>
        public string Fnumber { get; set; }
        /// <summary>
        /// 源单号，选单
        /// </summary>
        public int Fsourceid { get; set; }

        [Key]
        public int Id { get; set; }

        // 工件id
        public int Fworkorderlistid { get; set; }
        /// <summary>
        /// 工步代码，选单
        /// </summary>
        public int Fobjectid { get; set; }

            /// <summary>
            /// 数量，选单（可修改）
            /// </summary>
            public decimal Fqty { get; set; }

            /// <summary>
            /// 重量，选单（可修改）
            /// </summary>
            public decimal Fweight { get; set; }

            /// <summary>
            /// 关联数量，默认 0
            /// </summary>
            public decimal Fcommitqty { get; set; } = 0;

            /// <summary>
            /// 合格数量，默认 0
            /// </summary>
            public decimal Fpassqty { get; set; } = 0;

            /// <summary>
            /// 作业员 id，前端选
            /// </summary>
            public int Fempid { get; set; }

            /// <summary>
            /// 部门 id，前端选
            /// </summary>
            public int Fdeptid { get; set; }

            /// <summary>
            /// 制单人，前端获取登录人员
            /// </summary>
            public int Fbiller { get; set; }

            /// <summary>
            /// 报工日期，前端获取当前
            /// </summary>
            public DateTime Fdate { get; set; }
        }
    
}