using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_stk_inventorys
    {
        [Key]
        //public int Id { get; set; }  // 物料Id
        public int Fmaterialid { get; set; }  // 物料Id
        public string Fmaterialnum { get; set; }  // 物料代码
        //public string Fname { get; set; }  // 物料名称
        public string FstockNum { get; set; }  // 仓库代码
        public string FstockName { get; set; }  // 仓库名称
        public int Fstocklocid { get; set; }  // 仓位ID
        public string Flot { get; set; }  // 批号
        public decimal Fqty { get; set; }  // 数量
        public decimal Fsecqty { get; set; }  // 辅助数量
        public string Fmetal { get; set; }  // 材质
        public decimal Flength { get; set; }  // 长度 (前台可维护)
        public string Fstatus { get; set; }  // 状态 (可用 / 不可用)
    }
}