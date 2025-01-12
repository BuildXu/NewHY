using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
 
        public class sli_mes_orderoption_view
    {
        /// <summary>
        /// 客户编号，存储客户的唯一标识符，使用 nvarchar 类型
        /// </summary>
        public string Fcustno { get; set; }

        /// <summary>
        /// 客户名称，存储客户的名称信息，使用 nvarchar 类型
        /// </summary>
        public string Fcustname { get; set; }

        /// <summary>
        /// 名称，存储业务相关的名称，使用 nvarchar 类型
        /// </summary>
        public string Fname { get; set; }

        /// <summary>
        /// 编号，存储业务编号，使用 varchar 类型
        /// </summary>
        public string Fnumber { get; set; }

        /// <summary>
        /// 唯一标识符，存储数据的唯一标识，使用 int 类型，通常为自增 ID
        /// </summary>
        [Key]
        public int Id { get; set; }


        public int Fsourceid { get; set; }  // 源单id   (sli_work_processbilletnry  / id )
        /// <summary>
        /// 工作订单列表 ID，存储工作订单列表的唯一标识，使用 int 类型
        /// </summary>
        public int Fworkorderlistid { get; set; }

        /// <summary>
        /// 流程选项，存储业务流程中的选项信息，使用 int 类型
        /// </summary>
        public int Fprocessoption { get; set; }

        /// <summary>
        /// 数量，存储业务相关的数量信息，使用 float 类型
        /// </summary>
        public decimal Fqty { get; set; }

        /// <summary>
        /// 重量，存储业务相关的重量信息，使用 float 类型
        /// </summary>
        public decimal Fweight { get; set; }

        /// <summary>
        /// 提交数量，存储已提交的数量信息，使用 float 类型
        /// </summary>
        public decimal Fcommitqty { get; set; }

        /// <summary>
        /// 合格数量，存储合格产品或服务的数量，使用 float 类型
        /// </summary>
        public decimal Fpassqty { get; set; }

        /// <summary>
        /// 制单人 ID，存储制单人的唯一标识，使用 int 类型
        /// </summary>
        public int Fbiller { get; set; }

        /// <summary>
        /// 开始日期，存储业务开始的日期时间，使用 DateTime 类型
        /// </summary>
        public DateTime  ? Fstartdate { get; set; }

        /// <summary>
        /// 结束日期，存储业务结束的日期时间，使用 DateTime 类型
        /// </summary>
        public DateTime ? Fenddate { get; set; }

        /// <summary>
        /// 员工 ID，存储员工的唯一标识，使用 int 类型
        /// </summary>
        public int ? Fempid { get; set; }

        /// <summary>
        /// 部门 ID，存储部门的唯一标识，使用 int 类型
        /// </summary>
        public int ? Fdeptid { get; set; }

        /// <summary>
        /// 日期，存储业务发生的日期时间，使用 DateTime 类型
        /// </summary>
        public DateTime Fdate { get; set; }

        /// <summary>
        /// 选项编号，存储业务选项的编号，使用 varchar 类型
        /// </summary>
        public string Foptionno { get; set; }

        /// <summary>
        /// 选项名称，存储业务选项的名称，使用 varchar 类型
        /// </summary>
        public string Foptionname { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string Fdept_name { get; set; }
        /// <summary>
        /// 职员
        /// </summary>
        public string Femp_name { get; set; }

    }
}