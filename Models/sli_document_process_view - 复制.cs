using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_document_processbill_view
    {
        // 主键 
        public int Id { get; set; }

        // 工艺档案编号
        public string Fnumber { get; set; }

        // 日期
        public DateTime Fdate { get; set; }

        // 材质
        public bool Ftaxtrue { get; set; }

        // 工序ID
        public int Fprocessid { get; set; }

        // 工序ID
        public int Fprocessoption { get; set; }

        // 工序名称
        public string Fprocessname { get; set; }

        // 工序说明
        public string Fprocessnote { get; set; }

        // 部门ID
        public int Fdeptid { get; set; }

        // 表体ID
        [Key]
        public int Fbillid { get; set; }

        // 可选：重写ToString方法以便于调试
        public override string ToString()
        {
            return $"Id: {Id}, Fnumber: {Fnumber}, Fdate: {Fdate.ToShortDateString()}, " +
                   $"Ftaxtrue: {Ftaxtrue}, Fprocessid: {Fprocessid}, Fprocessoption: {Fprocessoption}, " +
                   $"Fprocessname: {Fprocessname}, Fprocessnote: {Fprocessnote}, " +
                   $"Fdeptid: {Fdeptid}, Fbillid: {Fbillid}";
        }
    }
}