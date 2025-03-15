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


        //public int Fid { get; set; }

        public string FBillNo { get; set; }
        public DateTime FDate { get; set; }
        public int FCustId { get; set; }  //FCUSTID
        public string FCustNo { get; set; }
        public string FCustName { get; set; }
        public string Fcustomer { get; set; }
        public string Fproductno { get; set; }
        public int FID { get; set; }
        [Key]
        public int Forderid { get; set; }
        public int FEntryId { get; set; }//FENTRYID
        public int FSeq { get; set; }
        public decimal FQty { get; set; }
        public string FNote { get; set; }
        public DateTime? Fplandeliverydate { get; set; }
        public decimal Fstockqty { get; set; }
        public string FslisaleTechNo { get; set; }
        public string FslitechNo { get; set; }
        public string Fmaterialid { get; set; }
        public int Fnumber { get; set; }
        public string Fname { get; set; }
        public string Fdescription { get; set; }
        public string Fsliouterdiameter { get; set; }
        public decimal Fsliinnerdiameter { get; set; }
        public decimal Fslihight { get; set; }
        public decimal Fsliallowanceod { get; set; }
        public decimal Fsliallowanceid { get; set; }
        public decimal Fsliallowanceh { get; set; }
        public decimal Fsliweightmaterial { get; set; }
        public decimal Fsliweightforging { get; set; }//Fsliweightforging
        public decimal Fsliweightgoods { get; set; }//Fsliweightgoods
        public decimal Fslidrawingno { get; set; }//Fslidrawingno
        public string Fslimetal { get; set; }  //FsliMetal
        public string Fsligoodsstatus { get; set; }//FsliGoodsStatus
        public string Fsliprocessing { get; set; } //FsliProcessing
        public string Fslidelivery { get; set; }//FsliDelivery
        public string Fsliblankmodel { get; set; }//FsliBlankModel
        public string Fslipunching { get; set; }//FsliPunching
        public string FsliTemperatureBegin { get; set; }//FsliTemperatureBegin
        public int FsliTempratureEnd { get; set; }
        public int Fslimould { get; set; }//FsliMould
        public string Fsliroller { get; set; } //FsliRoller
        public string Fsliheatingtimes { get; set; }//FsliHeatingTimes
        public int Fsligrade { get; set; }//FsliGrade
        public string Fsumnumber { get; set; }//FSumNumber
        public string Fworkorderlistqty { get; set; } //FworkOrderListQty
        public int Fworkorderlistremain { get; set; }//FworkOrderListRemain
        public int Fworkorderliststatus { get; set; }
        public int Fslimetel { get; set; }


    }
}