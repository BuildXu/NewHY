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
        public int Fbillerid { get; set; }      // 制单人ID
        public string dept_name { get; set; }     // 部门名称
        public string empName { get; set; }      // 员工姓名

    }

    public class sli_quality_request_view_all
    {

        [Key]
        public int Id { get; set; }              // Id 自增
        public string Fnumber { get; set; }      // 请检单号
        public int Fdeptid { get; set; }             // 部门 Id
        public int Fempid { get; set; }                // 请检人 Id
        public DateTime Fdate { get; set; }       // 请检日期
        public DateTime Fendate { get; set; }     // 预计完成日期
        public int Fstatus { get; set; }      // 状态
        public string Fdept_name { get; set; }     // 部门名称
        public string FempName { get; set; }      // 员工姓名
        public decimal Fqty { get; set; }      // 数量
        public string Fmaterialnumber { get; set; }      // 物料代码
        public string Fname { get; set; }      // 物料名称
        public string Fdescription { get; set; }      // 规格型号
        public string Fworkorderlistid { get; set; }      // 工件ID


    }
}