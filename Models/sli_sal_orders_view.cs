using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_sal_orders_view
    {

        public string FBillNo { get; set; }
        public DateTime? FDate { get; set; }
        public int? FCustId { get; set; }
        public string FCustNo { get; set; }
        public string FCustName { get; set; }
        public string Fcustomer { get; set; }
        public string Fproductno { get; set; }
        public string FID { get; set; }               // 数据库类型 nvarchar → string (原错误为 int)
        [Key]
        public int? Forderid { get; set; }             // 主键允许空需确认业务逻辑
        public int? FEntryId { get; set; }
        public int? FSeq { get; set; }
        public int? FQty { get; set; }                 // 数据库类型 int → int? (原错误为 decimal)
        public decimal? FNote { get; set; }            // 数据库类型 decimal → decimal? (原错误为 string)
        public string Fplandeliverydate { get; set; }  // 数据库类型 nvarchar → string (原错误为 DateTime?)
        public DateTime? Fstockqty { get; set; }       // 数据库类型 datetime → DateTime?
        public decimal? FslisaleTechNo { get; set; }   // 数据库类型 decimal → decimal?
        public string FslitechNo { get; set; }
        public string Fmaterialid { get; set; }        // 数据库类型 nvarchar → string (原错误为 int)
        public int? Fnumber { get; set; }              // 数据库类型 int → int? (原错误为 string)
        public string Fname { get; set; }
        public string Fdescription { get; set; }
        public string Fsliouterdiameter { get; set; }  // 数据库类型 nvarchar → string (原错误为 decimal)
        public decimal? Fsliinnerdiameter { get; set; }
        public decimal? Fslihight { get; set; }
        public decimal? Fsliallowanceod { get; set; }
        public decimal? Fsliallowanceid { get; set; }
        public decimal? Fsliallowanceh { get; set; }
        public decimal? Fsliweightmaterial { get; set; }
        public decimal? Fsliweightforging { get; set; }
        public decimal? Fsliweightgoods { get; set; }
        public decimal? Fslidrawingno { get; set; }    // 数据库类型 decimal → decimal?
        public string Fslimetal { get; set; }
        public string Fsligoodsstatus { get; set; }
        public string Fsliprocessing { get; set; }
        public string Fslidelivery { get; set; }
        public string Fsliblankmodel { get; set; }
        public string Fslipunching { get; set; }
        public string FsliTemperatureBegin { get; set; } // 数据库类型 nvarchar → string (原错误为 int)
        public int? FsliTempratureEnd { get; set; }      // 注意数据库字段拼写 Temprature (非 Temperature)
        public int? Fslimould { get; set; }              // 数据库类型 int → int?
        public string Fsliroller { get; set; }
        public string Fsliheatingtimes { get; set; }     // 数据库类型 nvarchar → string (原错误为 int)
        public int? Fsligrade { get; set; }              // 数据库类型 int → int?
        public string Fsumnumber { get; set; }
        public string Fworkorderlistqty { get; set; }    // 数据库类型 nvarchar → string (原错误为 int)
        public int? Fworkorderlistremain { get; set; }
        public int? Fworkorderliststatus { get; set; }
        public int? Fslimetel { get; set; }              // 数据库类型 int → int?
        public string Funit { get; set; }                // 新增字段 (数据库类型 nvarchar)


    }
}