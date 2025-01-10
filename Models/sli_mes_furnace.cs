using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_mes_furnace
    {
        /// <summary>
        /// 装炉单号，写表体（每次派工，给一个统一单号）
        /// </summary>
        public string Fnumber { get; set; }

        /// <summary>
        /// 自增 ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 工件 id，选单
        /// </summary>
        public int Fworkorderlistid { get; set; }

        /// <summary>
        /// 源单号，选单
        /// </summary>
        public int Fsourceid { get; set; }

        /// <summary>
        /// 工步代码，选单
        /// </summary>
        public int Fobjectid { get; set; }

        /// <summary>
        /// 数量，选单（可修改）
        /// </summary>
        public float Fqty { get; set; }

        /// <summary>
        /// 重量，选单（可修改）
        /// </summary>
        public float Fweight { get; set; }

        /// <summary>
        /// 炉 Id
        /// </summary>
        public string Ffurnaceno { get; set; }

        /// <summary>
        /// 热处理批号
        /// </summary>
        public string Fheatingno { get; set; }

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
        /// 装炉日期，前端获取当前
        /// </summary>
        public DateTime Fdate { get; set; }
    }
}