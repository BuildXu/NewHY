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
        public DateTime FDate { get; set; }
        public int FCustId { get; set; }
        public string FCustNo { get; set; }
        public string FCustName { get; set; }
        public string Fcustomer { get; set; }
        public string Fproductno { get; set; }
        public int FID { get; set; }
        [Key]
        public int Forderid { get; set; }
        public int FEntryId { get; set; }
        public int FSeq { get; set; }
        public decimal FQty { get; set; }
        public string FNote { get; set; }
        public DateTime? Fplandeliverydate { get; set; }
        public decimal Fstockqty { get; set; }
        public string FslisaleTechNo { get; set; }
        public string FslitechNo { get; set; }
        public int Fmaterialid { get; set; }  // 修正点：数据库类型 int → 实体类型 int（原错误为 string）
        public string Fnumber { get; set; }   // 修正点：数据库类型 nvarchar → 实体类型 string（原错误为 int）
        public string Fname { get; set; }
        public string Fdescription { get; set; }
        public decimal Fsliouterdiameter { get; set; }  // 修正点：数据库类型 decimal → 实体类型 decimal（原错误为 string）
        public decimal Fsliinnerdiameter { get; set; }
        public decimal Fslihight { get; set; }
        public decimal Fsliallowanceod { get; set; }
        public decimal Fsliallowanceid { get; set; }
        public decimal Fsliallowanceh { get; set; }
        public decimal Fsliweightmaterial { get; set; }
        public decimal Fsliweightforging { get; set; }
        public decimal Fsliweightgoods { get; set; }
        public string Fslidrawingno { get; set; }
        public string Fslimetal { get; set; }
        public string Fsligoodsstatus { get; set; }
        public string Fsliprocessing { get; set; }
        public string Fslidelivery { get; set; }
        public string Fsliblankmodel { get; set; }
        public string Fslipunching { get; set; }
        public int FsliTemperatureBegin { get; set; }  // 修正点：数据库类型 int → 实体类型 int（原错误为 string）
        public int FsliTempratureEnd { get; set; }
        public string Fslimould { get; set; }
        public string Fsliroller { get; set; }
        public int Fsliheatingtimes { get; set; }      // 修正点：数据库类型 int → 实体类型 int（原错误为 string）
        public string Fsligrade { get; set; }
        public string Fsumnumber { get; set; }
        public int Fworkorderlistqty { get; set; }     // 修正点：数据库类型 int → 实体类型 int（原错误为 string）
        public int Fworkorderlistremain { get; set; }
        public int Fworkorderliststatus { get; set; }
        public string Fslimetel { get; set; }


    }
}