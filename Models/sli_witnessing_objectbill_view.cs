using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{

    public class sli_witnessing_objectbill_view
    {
        /// <summary>
        /// 销售订单行id
        /// </summary>
        public int Fsourceid { get; set; } 
        /// <summary>
        /// 订单编号
        /// </summary>
        public string Forderno { get; set; }
        /// <summary>
        /// 客户
        /// </summary>
        public string Fcustomer { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        public string Fmaterialname { get; set; }
        /// <summary>
        /// 规格型号
        /// </summary>
        public string Fdescription { get; set; }
        /// <summary>
        /// 数据id  自增
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 销售订单分录ID
        /// </summary>
        public int Fentryid { get; set; }
        /// <summary>
        /// 行号
        /// </summary>
        public int Fseq { get; set; }   
        /// <summary>
        /// 工步ID
        /// </summary>
        public int Fobject { get; set; }   //  sli_bd_process_object  /  id
        /// <summary>
        /// 工步编码
        /// </summary>
        public string Fobjectno { get; set; }
        /// <summary>
        /// 工步名称
        /// </summary>
        public string Fobjectname { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Fnote { get; set; }  
        /// <summary>
        /// 状态
        /// </summary>
        public int Fstatus { get; set; }   

    }
}