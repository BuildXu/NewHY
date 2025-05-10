using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_mes_lauchbill
    {

        /// <summary>
        /// 投产单号，每次新增给一个统一单号
        /// </summary>
        public string Fnumber { get; set; }


        /// <summary>
        /// 自增的唯一标识符
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 来源标识符，取自工序流转卡（sli_work_processbill_view / Id）查询视图，转成 FsourceId
        /// </summary>
        public int Fsourceid { get; set; }

        /// <summary>
        /// 工作订单列表标识符，取自工序流转卡（sli_work_processbill_view / Id）查询视图的 Fworkorderlistid
        /// </summary>
        public int Fworkorderlistid { get; set; }

        /// <summary>
        /// 工序选项，取自工序流转卡（sli_work_processbill_view / Fprocessoption）查询视图
        /// </summary>
        public int Fprocessoption { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime ? Fstartdate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime ? Fenddate { get; set; }

        /// <summary>
        /// 部门标识符
        /// </summary>
        public int Fdeptid { get; set; }

        /// <summary>
        /// 状态，默认值为 1
        /// </summary>
        public int Fstatus { get; set; } = 1;
        public string Ftype { get; set; }    // ---------------------  业务类型   自制  、  委外     

        public string Fsupplier { get; set; } //    ----------------- 供应商  
    }
}