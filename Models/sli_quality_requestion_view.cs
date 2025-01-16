using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_quality_request_view
    {

        [Key]
        public int Id { get; set; }              // Id 自增
        public string Fnumber { get; set; }      // 请检单号
        public int Fdeptid { get; set; }             // 部门 Id
        public int Fempid { get; set; }                // 请检人 Id
        public DateTime Fdate { get; set; }       // 请检日期
        public DateTime Fendate { get; set; }     // 预计完成日期
        public int Fstatus { get; set; }      // 状态
        public int ? Fbillerid { get; set; }      // 制单人ID
        public string Fdept_name { get; set; }     // 部门名称
        public string FempName { get; set; }      // 员工姓名
        public virtual ICollection<sli_quality_requestentry_view> sli_quality_requestentry_view { get; set; }

    }

    public class sli_quality_requestentry_view
    {

        [Key]
        public int Fentryid { get; set; }              // 表体ID自增
        public int Fworkorderlistid { get; set; }      // 工件id
        public int Fsourceid { get; set; }             // 工艺路线分录ID
        public int Id { get; set; }                // 表头ID
        public decimal Fqty { get; set; }       // 数量
        public int Fobjectid { get; set; }     //工步iD
        public int Fstatus { get; set; }      // 状态
        public string Fobjectnumber { get; set; }     // 工步代码
        public string Fobjectname { get; set; }      // 工步名称
        public string Fmaterialnumber { get; set; }     // 物料代码
        public string Fmaterialname { get; set; }      // 物料名称
        public string Fdescription { get; set; }      // 规格型号  Fproductno
        public string Fproductno { get; set; }      // 工件名称  Fproductno


        public virtual sli_quality_request_view sli_quality_request_view { get; set; }
    }
}