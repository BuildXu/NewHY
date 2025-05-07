using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_bd_materials_view
    {
        [Key]
        public int FmaterialId { get; set; }  //物料内码
        public string Fnumber { get; set; }   //物料代码
        public string Fname { get; set; }     //物料名称
        public string Fdescription { get; set; }     //描述
        public decimal FsliOuterDiameter { get; set; }    //外径
        public decimal FsliInnerDiameter { get; set; }    //内径
        public decimal FsliHight { get; set; }     //高度
        public decimal FsliAllowanceOD { get; set; }   //外径余量
        public decimal FsliAllowanceID { get; set; }   //内径余量
        public decimal FsliallowanceH { get; set; }    //高度余量
        public decimal FsliWeightMaterial { get; set; }     //下料重量
        public decimal FsliWeightForging { get; set; }    //锻件重量
        public decimal FsliWeightGoods { get; set; }    //成品重量
        public string FsliDrawingNo { get; set; }    //图号
        public string FsliMetal { get; set; }    //材质
        public string FsliGoodsStatus { get; set; }    //发货状态
        public string FsliProcessing { get; set; }    //加工要求
        public string FsliDelivery { get; set; }    //发货要求
        public string FsliBlankModel { get; set; }    //制坯规格
        public string FsliPunching { get; set; }    //冲子规格
        public int FsliTemperatureBegin { get; set; }    //始锻温度
        public int FsliTempratureEnd { get; set; }    //终锻温度
        public string FsliMould { get; set; }    //模具
        public string FsliRoller { get; set; }    //轧辊
        public int FsliHeatingTimes { get; set; }    //火次
        public string FsliGrade { get; set; }    //锻件级别
        public string FSumNumber { get; set; }    //物料名称+描述+图号+材质

        public string Funit { get; set; } //单位

    }
}