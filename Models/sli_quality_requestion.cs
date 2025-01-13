using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_quality_requestion
    {
         
        [Key]
        public int Id { get; set; }              // Id自增
        public int Fworkorderlistid { get; set; }  // 工件Id
        public int Fsourceid { get; set; }    //  工序流转卡  工步 Id   sli_work_processbillentry / Id
        public string Fnumber { get; set; }    //  请检单号
        public decimal Fqty { get; set; }         //  数量
        public int Fdeptid { get; set; }             // 部门 Id
        public int Fempid { get; set; }                //  请检人 Id
        public DateTime Fdate { get; set; }       // 请检日期
        public DateTime Fendate { get; set; }     //  预计完成日期
        public int Fstatus { get; set; }      // 状态
    
}
}