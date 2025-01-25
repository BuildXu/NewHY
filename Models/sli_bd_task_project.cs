using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_bd_task_project
    {
        // 自增主键
        public int Id { get; set; }

        // 编号
        public string FNumber { get; set; }

        // 名称
        public string FName { get; set; }

        // 开始日期
        public DateTime FStartDate { get; set; }

        // 结束日期
        public DateTime FEndDate { get; set; }

        // 备注
        public string FNote { get; set; }

        // 状态
        public int? FStatus { get; set; }

        // 使用状态
        public int? FUsed { get; set; }

        // 创建日期
        public string FCreateDate { get; set; }
    }

    public class sli_bd_task_step
    {
        // 自增主键
        public int Id { get; set; }

        // 编号
        public string FNumber { get; set; }

        // 名称
        public string FName { get; set; }

        // 备注
        public string FNote { get; set; }

        // 状态
        public int? FStatus { get; set; }

        // 使用状态
        public int? FUsed { get; set; }

        // 创建日期
        public string FCreateDate { get; set; }
    }
    public class sli_bd_task_type
    {
        // 自增主键
        public int Id { get; set; }

        // 编号
        public string FNumber { get; set; }

        // 名称
        public string FName { get; set; }

        // 备注
        public string FNote { get; set; }

        // 状态
        public int? FStatus { get; set; }

        // 使用状态
        public int? FUsed { get; set; }

        // 创建日期
        public string FCreateDate { get; set; }
    }

  
        public class sli_bd_task_status
        {
            // 自增主键
            public int Id { get; set; }

            // 编号
            public string FNumber { get; set; }

            // 名称
            public string FName { get; set; }

            // 备注
            public string FNote { get; set; }

            // 状态
            public int? FStatus { get; set; }

            // 使用状态
            public int? FUsed { get; set; }

            // 创建日期
            public string FCreateDate { get; set; }
        }

    public class sli_task_order
    {
        // 自增主键
        public int Id { get; set; }

        // 项目 ID
        public int? Fproject { get; set; }

        // 项目相关字符串信息
        public string Fprojects { get; set; }

        // 编号
        public string Fnumber { get; set; }

        // 备注
        public string Fnote { get; set; }

        // 来源
        public string Fsource { get; set; }

        // 日期
        public DateTime? Fdate { get; set; }

        // 开始日期
        public DateTime? Fstartdate { get; set; }

        // 结束日期
        public DateTime? Fenddate { get; set; }

        // 步骤 ID
        public int? Fstep { get; set; }

        // 步骤相关字符串信息
        public string Fsteps { get; set; }

        // 状态
        public int? Fstatus { get; set; }

        // 类型 ID
        public int? Ftype { get; set; }

        // 类型相关字符串信息
        public string Ftypes { get; set; }
    }

    public class sli_task_report
    {
        // 源 ID
        public int? Fsourceid { get; set; }

        // 自增主键
        public int Id { get; set; }

        // 编号
        public string Fnumber { get; set; }

        // 员工 ID
        public int? Fempid { get; set; }

        // 部门 ID
        public int? Fdeptid { get; set; }

        // 状态
        public int? Fstatus { get; set; }

        // 日期
        public DateTime? Fdate { get; set; }

        // 备注
        public string Fnote { get; set; }
    }
}