using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{

    public class sli_mes_orderoption_view
    {
        // 字符串类型（默认可空）
        public string Fwobillno { get; set; }            // varchar
        public string Fcustno { get; set; }              // nvarchar
        public string Fcustname { get; set; }            // nvarchar
        public string Fname { get; set; }                // nvarchar
        public string Fnumber { get; set; }              // varchar
        public string Forderno { get; set; }             // nvarchar
        public string Fpname { get; set; }               // nvarchar
        public string Fdescription { get; set; }         // nvarchar
        public string Foptionno { get; set; }            // varchar
        public string Foptionname { get; set; }          // varchar
        public string Fdept_name { get; set; }           // varchar
        public string Femp_name { get; set; }            // varchar
        public string Fslimetal { get; set; }            // nvarchar
        public string Fslipunching { get; set; }         // nvarchar
        public string Fproducttype { get; set; }         // varchar
        public string Ftype { get; set; }                // varchar
        public string Fsupplier { get; set; }             // varchar

        // 值类型改为可空
        [Key]
        public int? Id { get; set; }                     // int（主键建议保持非空）
        public int? Fsourceid { get; set; }              // int
        public int? Fworkorderlistid { get; set; }       // int
        public int? Fprocessoption { get; set; }         // int
        public decimal? Fqty { get; set; }               // decimal
        public decimal? Fweight { get; set; }            // decimal
        public decimal? Fcommitqty { get; set; }         // decimal
        public decimal? Fpassqty { get; set; }           // decimal
        public int? Fbiller { get; set; }                // int
        public int? Fempid { get; set; }                 // int
        public int? Fdeptid { get; set; }                // int

        // 日期类型改为可空
        public DateTime? Fstartdate { get; set; }        // datetime
        public DateTime? Fenddate { get; set; }          // datetime
        public DateTime? Fdate { get; set; }             // datetime

        // 修正错误类型字段（原错误定义为int?）
        public string Fmaterialid { get; set; }          // nvarchar（原数据库是nvarchar）
        public string Fsliallowancehf { get; set; }      // varchar（原数据库是varchar）

        // 保留原decimal字段
        public decimal? Fsliweightmaterial { get; set; } // decimal



    }
}