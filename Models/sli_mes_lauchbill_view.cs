using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    /// <summary>
    /// 投产计划
    /// </summary>

    public class sli_mes_lauchbill_view
    {
        public string Fwobillno { get; set; }
        /// <summary>
        /// 客户编号，使用 nvarchar 存储
        /// </summary>
        public string Fslimetal { get; set; }
        public string Fothers { get; set; }

        public Decimal? Fqty { get; set; }
        public Decimal? Fweight { get; set; }
        public Decimal? Fweights { get; set; }

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
        /// 自增的唯一标识符，使用 int 存储
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 源单号，使用 int 存储
        /// </summary>
        public int Fsourceid { get; set; }

        /// <summary>
        /// 工作订单列表编号，使用 int 存储
        /// </summary>
        public int Fworkorderlistid { get; set; }

        /// <summary>
        /// 流程选项，使用 int 存储
        /// </summary>
        public int Fprocessoption { get; set; }

        /// <summary>
        /// 开始日期，使用 DateTime 存储
        /// </summary>
        public DateTime? Fstartdate { get; set; }

        /// <summary>
        /// 结束日期，使用 DateTime 存储
        /// </summary>
        public DateTime? Fenddate { get; set; }

        /// <summary>
        /// 部门编号，使用 int 存储
        /// </summary>
        public int Fdeptid { get; set; }

        /// <summary>
        /// 状态，使用 int 存储
        /// </summary>
        public int Fstatus { get; set; }

        /// <summary>
        /// 选项编号，使用 varchar 存储
        /// </summary>
        public string Foptionno { get; set; }

        /// <summary>
        /// 选项名称，使用 varchar 存储
        /// </summary>
        public string Foptionname { get; set; }
        /// <summary>
        /// 部门编号，使用 int 存储
        /// </summary>
        public string Fdept_name { get; set; }
        public string Fnumber { get; set; }

        public string Forderno { get; set; }//销售单号
        public string Fproductno { get; set; }//工件号
        public string Fpname { get; set; }//产品名称
        public string Fdescription { get; set; }//产品规格

        public string Ftype { get; set; }    // ---------------------  业务类型   自制  、  委外     

        public string Fsupplier { get; set; } //    ----------------- 供应商  

    }
}