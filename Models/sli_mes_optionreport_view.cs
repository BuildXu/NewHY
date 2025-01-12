using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
 
        public class sli_mes_optionreport_view
    {
        /// <summary>
        /// 客户编号，使用 nvarchar 存储
        /// </summary>
        public string Fcustno { get; set; }

        /// <summary>
        /// 客户名称，使用 nvarchar 存储
        /// </summary>
        public string Fcustname { get; set; }

        /// <summary>
        /// 名称，使用 nvarchar 存储
        /// </summary>
        public string Fname { get; set; }

        /// <summary>
        /// 实体的唯一标识符，使用 int 存储
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 工作订单列表的 ID，使用 int 存储
        /// </summary>
        public int Fworkorderlistid { get; set; }

        /// <summary>
        /// 源单的 ID，使用 int 存储
        /// </summary>
        public int Fsourceid { get; set; }

        /// <summary>
        /// 工序id，使用 int 存储
        /// </summary>
        public int Fprocessoption { get; set; }

        /// <summary>
        /// 数量，使用 float 存储
        /// </summary>
        public float Fqty { get; set; }

        /// <summary>
        /// 重量，使用 float 存储
        /// </summary>
        public float Fweight { get; set; }

        /// <summary>
        /// 提交的数量，使用 float 存储
        /// </summary>
        public float Fcommitqty { get; set; }

        /// <summary>
        /// 通过检查的数量，使用 float 存储
        /// </summary>
        public float Fpassqty { get; set; }

        /// <summary>
        /// 员工的 ID，使用 int 存储
        /// </summary>
        public int Fempid { get; set; }

        /// <summary>
        /// 部门的 ID，使用 int 存储
        /// </summary>
        public int Fdeptid { get; set; }

        /// <summary>
        /// 制单人的 ID，使用 int 存储
        /// </summary>
        public int Fbiller { get; set; }

        /// <summary>
        /// 日期信息，使用 DateTime 存储
        /// </summary>
        public DateTime Fdate { get; set; }

        /// <summary>
        /// 选项编号，使用 varchar 存储
        /// </summary>
        public string Foptionno { get; set; }

        /// <summary>
        /// 选项名称，使用 varchar 存储
        /// </summary>
        public string Foptionname { get; set; }
    }

}