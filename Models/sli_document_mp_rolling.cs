using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_document_mp_rolling
    { // 产品或对象的唯一标识符
        [Key]
        public int Id { get; set; }
        // 编号
        public string Fnumber { get; set; }
        // 版本
        public string Fversion { get; set; }
        // 产品类型
        public string Fproducttype { get; set; }
        // 状态
        public int Fstatus { get; set; }
        // 制单人
        public int Fbiller { get; set; }
        // 外径
        public decimal Fsliouterdiameter { get; set; }
        // 内径
        public decimal Fsliinnerdiameter { get; set; }
        // 高度
        public decimal Fslihight { get; set; }
        // 外径余量
        public decimal Fsliallowanceod { get; set; }
        // 内径余量
        public decimal Fsliallowanceid { get; set; }
        // 高度余量
        public decimal fsliallowanceh { get; set; }
        // 下料重量
        public decimal Fsliweightmaterial { get; set; }
        // 锻件重量
        public decimal Fsliweightforging { get; set; }
        // 货物重量
        public decimal Fsliweightgoods { get; set; }
        // 火耗重量
        public string Fsliweightfurnace { get; set; }
        // 图纸编号
        public string Fslidrawingno { get; set; }
        // 材质
        public string Fslimetal { get; set; }
        // 热处理
        public int Fheattreatment { get; set; }
        // 冷却
        public int Fcooldown { get; set; }

    }
}