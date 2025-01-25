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
}