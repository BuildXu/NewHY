using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_mes_orderoption
    {
        /// <summary>
        /// 派工单号，每次派工给一个统一单号
        /// </summary>
        public string Fnumber { get; set; }

        /// <summary>
        /// 自增行 id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 工件 Id，选工序计划时带入
        /// </summary>
        public int Fworkorderlistid { get; set; }

        /// <summary>
        /// 工序 Id，选工序计划时带入
        /// </summary>
        public int Fprocessoption { get; set; }

        /// <summary>
        /// 工件数量，选工序计划时带入
        /// </summary>
        public decimal Fqty { get; set; }

        /// <summary>
        /// 工件重量，选工序计划时带入
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
        /// 制单人，登录用户 id
        /// </summary>
        public int Fbiller { get; set; }

        /// <summary>
        /// 计划开始日期，选工序计划时带入（可修改）
        /// </summary>
        public DateTime ?  Fstartdate { get; set; }

        /// <summary>
        /// 计划结束日期，选工序计划时带入（可修改）
        /// </summary>
        public DateTime ? Fenddate { get; set; }

        /// <summary>
        /// 派工日期
        /// </summary>
        public DateTime Fdate { get; set; }
    }
}