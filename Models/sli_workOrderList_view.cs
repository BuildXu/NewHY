using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_workorderlist_view
    {
        [Key]
        public int Id { get; set; }
        public string Fproductno { get; set; }          // 数据库类型 varchar → string ✔️
        public decimal? Fworkqty { get; set; }          // 数据库类型 numeric → decimal? ✔️
        public decimal? Fworkweight { get; set; }       // 数据库类型 numeric → decimal? ✔️
        public string Fbillno { get; set; }             // 数据库类型 nvarchar → string ✔️
        public DateTime Fdate { get; set; }             // 数据库类型 datetime → DateTime ✔️
        public string Fplandeleliverydate { get; set; } // 数据库类型 varchar → string ✔️
        public string Fslitemperatureend { get; set; }  // 数据库类型 varchar → string ✔️
        public int Fcustid { get; set; }                // 数据库类型 int → int ✔️
        public string Fcustno { get; set; }             // 数据库类型 nvarchar → string ✔️
        public string Fcustname { get; set; }           // 数据库类型 nvarchar → string ✔️
        public string Fcustomer { get; set; }           // 数据库类型 nvarchar → string ✔️
        public int Fid { get; set; }                    // 数据库类型 int → int ✔️
        public int Fentryid { get; set; }               // 数据库类型 int → int ✔️
        public int Fseq { get; set; }                   // 数据库类型 int → int ✔️
        public decimal Fqty { get; set; }               // 数据库类型 decimal → decimal ✔️
        public string Fnote { get; set; }               // 数据库类型 nvarchar → string ✔️
        public DateTime? Fplandeliverydate { get; set; }// 数据库类型 datetime → DateTime? ✔️
        public int Fmaterialid { get; set; }            // 修正点：数据库 int → int（原错误为 string）
        public string Fnumber { get; set; }             // 修正点：数据库 nvarchar → string（原错误为 int）
        public string Fname { get; set; }               // 数据库类型 nvarchar → string ✔️
        public string Fdescription { get; set; }        // 数据库类型 nvarchar → string ✔️
        public decimal Fsliouterdiameter { get; set; }  // 修正点：数据库 decimal → decimal（原错误为 string）
        public decimal Fsliinnerdiameter { get; set; }  // 数据库类型 decimal → decimal ✔️
        public decimal Fslihight { get; set; }          // 数据库类型 decimal → decimal ✔️
        public decimal Fsliallowanceod { get; set; }    // 数据库类型 decimal → decimal ✔️
        public decimal Fsliallowanceid { get; set; }    // 数据库类型 decimal → decimal ✔️
        public decimal Fsliallowanceh { get; set; }     // 数据库类型 decimal → decimal ✔️
        public decimal Fsliweightmaterial { get; set; } // 数据库类型 decimal → decimal ✔️
        public decimal Fsliweightforging { get; set; }  // 数据库类型 decimal → decimal ✔️
        public decimal Fsliweightgoods { get; set; }    // 数据库类型 decimal → decimal ✔️
        public string Fslidrawingno { get; set; }       // 修正点：数据库 nvarchar → string（原错误为 decimal）
        public string Fslimetal { get; set; }           // 数据库类型 nvarchar → string ✔️
        public string Fsligoodsstatus { get; set; }     // 数据库类型 nvarchar → string ✔️
        public string Fsliprocessing { get; set; }      // 数据库类型 nvarchar → string ✔️
        public string Fslidelivery { get; set; }        // 数据库类型 nvarchar → string ✔️
        public string Fsliblankmodel { get; set; }      // 数据库类型 nvarchar → string ✔️
        public string Fslipunching { get; set; }        // 数据库类型 nvarchar → string ✔️
        public int Fslitemperaturebegin { get; set; }   // 修正点：数据库 int → int（原错误为 string）
        public int Fslitempratureend { get; set; }      // 数据库类型 int → int ✔️
        public string Fslimould { get; set; }           // 修正点：数据库 nvarchar → string（原错误为 int）
        public string Fsliroller { get; set; }          // 数据库类型 nvarchar → string ✔️
        public int Fsliheatingtimes { get; set; }       // 修正点：数据库 int → int（原错误为 string）
        public string Fsligrade { get; set; }           // 修正点：数据库 nvarchar → string（原错误为 int）
        public string Fsumnumber { get; set; }          // 数据库类型 nvarchar → string ✔️
        public string Fsplittype { get; set; }          // 数据库类型 varchar → string ✔️
        public int? Fsoqty { get; set; }                // 数据库类型 int → int? ✔️
        public int? Fwoqty { get; set; }                // 数据库类型 int → int? ✔️
        public int? Fwpqty { get; set; }                // 数据库类型 int → int? ✔️
        public int? Ffinisthqty { get; set; }           // 数据库类型 int → int? ✔️
        public int? Fstockqty { get; set; }             // 数据库类型 int → int? ✔️
        public int? Foption { get; set; }               // 数据库类型 int → int? ✔️
        public int? Fobject { get; set; }               // 数据库类型 int → int? ✔️
        public int? Fmo { get; set; }                   // 数据库类型 int → int? ✔️
        public int? Fstatus { get; set; }               // 数据库类型 int → int? ✔️
        public int? Fpause { get; set; }                // 数据库类型 int → int? ✔️
        public int? Fcancel { get; set; }               // 数据库类型 int → int? ✔️
    }
}