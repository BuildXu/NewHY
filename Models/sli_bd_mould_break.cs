using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_bd_mould_break
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string FNumber { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string FName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string FNote { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int FStatus { get; set; }

        /// <summary>
        /// 是否使用
        /// </summary>
        public bool FUsed { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime FCreateDate { get; set; }
    }
}