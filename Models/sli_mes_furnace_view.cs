using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
 
        public class sli_mes_furnace_view
    {
            /// <summary>
            /// 客户编号，使用 nvarchar 类型存储
            /// </summary>
            public string Fcustno { get; set; }

            /// <summary>
            /// 客户名称，使用 nvarchar 类型存储
            /// </summary>
            public string Fcustname { get; set; }

            /// <summary>
            /// 名称，使用 nvarchar 类型存储
            /// </summary>
            public string Fname { get; set; }

            /// <summary>
            /// 编号，使用 varchar 类型存储
            /// </summary>
            public string Fnumber { get; set; }

        /// <summary>
        /// 自增 ID
        /// </summary>
        [Key]
            public int Id { get; set; }

            /// <summary>
            /// 工作订单列表 ID，使用 int 类型存储
            /// </summary>
            public int Fworkorderlistid { get; set; }

            /// <summary>
            /// 源单 ID，使用 int 类型存储
            /// </summary>
            public int Fsourceid { get; set; }

            /// <summary>
            /// 工步对象 ID，使用 int 类型存储
            /// </summary>
            public int Fobjectid { get; set; }

            /// <summary>
            /// 数量，使用 float 类型存储
            /// </summary>
            public float Fqty { get; set; }

            /// <summary>
            /// 重量，使用 float 类型存储
            /// </summary>
            public float Fweight { get; set; }

            /// <summary>
            /// 炉号，使用 varchar 类型存储
            /// </summary>
            public string Ffurnaceno { get; set; }

            /// <summary>
            /// 加热编号，使用 varchar 类型存储
            /// </summary>
            public string Fheatingno { get; set; }

            /// <summary>
            /// 员工 ID，使用 int 类型存储
            /// </summary>
            public int Fempid { get; set; }

            /// <summary>
            /// 部门 ID，使用 int 类型存储
            /// </summary>
            public int Fdeptid { get; set; }

            /// <summary>
            /// 制单人 ID，使用 int 类型存储
            /// </summary>
            public int Fbiller { get; set; }

            /// <summary>
            /// 日期，使用 DateTime 类型存储
            /// </summary>
            public DateTime Fdate { get; set; }

            /// <summary>
            /// 工步对象编号，使用 varchar 类型存储
            /// </summary>
            public string Fobjectno { get; set; }

            /// <summary>
            /// 工步对象名称，使用 varchar 类型存储
            /// </summary>
            public string Fobjectname { get; set; }
        }
}