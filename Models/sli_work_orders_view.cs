using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_work_orders_view
    {
        [Key]
        public int? Fwoid { get; set; }  // 主键改为可空（需谨慎，通常主键不应为null）

        public string Fwobillno { get; set; }          // string默认允许null
        public string Forderno { get; set; }           // 工作令号
        public string Fnotes { get; set; }             // 备注

        public DateTime? Fwodate { get; set; }         // DateTime改为可空
        public decimal? Fqty { get; set; }             // decimal改为可空
        public decimal? Fwoweight { get; set; }        // decimal改为可空
        public DateTime? Fwoplanstart { get; set; }    // DateTime改为可空
        public DateTime? Fwoplanend { get; set; }      // DateTime改为可空

        public string Fwoordertype { get; set; }       // string默认允许null
        public int? Fwoentryqty { get; set; }          // int改为可空
        public int? Fwoecommitqty { get; set; }        // int改为可空
        public int? Fwoestatus { get; set; }           // int改为可空
        public int? Fwoeclosed { get; set; }           // int改为可空

        public int? Fworkorderlistid { get; set; }       // int改为可空
        public string Fproductno { get; set; }         // string默认允许null
        public decimal? Fworkqty { get; set; }         // decimal改为可空
        public decimal? Fworkweight { get; set; }      // decimal改为可空

        public string Forderbillno { get; set; }       // string默认允许null
        public string Fplandeleliverydate { get; set; }// string默认允许null
        public string Fslitemperatureend { get; set; } // string默认允许null

        public int? Fcustid { get; set; }              // int改为可空
        public string Fcustno { get; set; }            // string默认允许null
        public string Fcustname { get; set; }          // string默认允许null
        public string Fcustomer { get; set; }          // string默认允许null

        public int? Fid { get; set; }                  // int改为可空
        public int? Fentryid { get; set; }             // int改为可空
        public int? Fseq { get; set; }                 // int改为可空
        public decimal? Forderqty { get; set; }        // decimal改为可空
        public string Fnote { get; set; }              // string默认允许null

        public DateTime? Fplandeliverydate { get; set; }// DateTime改为可空
        public int? Fmaterialid { get; set; }          // int改为可空
        public string Fnumber { get; set; }            // string默认允许null
        public string Fname { get; set; }              // string默认允许null
        public string Fdescription { get; set; }       // string默认允许null

        public decimal? Fsliouterdiameter { get; set; }// decimal改为可空
        public decimal? Fsliinnerdiameter { get; set; }// decimal改为可空
        public decimal? Fslihight { get; set; }        // decimal改为可空
        public decimal? Fsliallowanceod { get; set; }  // decimal改为可空
        public decimal? Fsliallowanceid { get; set; }  // decimal改为可空
        public decimal? Fsliallowanceh { get; set; }   // decimal改为可空

        public decimal? Fsliweightmaterial { get; set; } // decimal改为可空
        public decimal? Fsliweightforging { get; set; }  // decimal改为可空
        public decimal? Fsliweightgoods { get; set; }    // decimal改为可空

        public string Fslidrawingno { get; set; }      // string默认允许null
        public string Fslimetal { get; set; }          // string默认允许null
        public string Fsligoodsstatus { get; set; }    // string默认允许null
        public string Fsliprocessing { get; set; }     // string默认允许null
        public string Fslidelivery { get; set; }       // string默认允许null
        public string Fsliblankmodel { get; set; }     // string默认允许null
        public string Fslipunching { get; set; }       // string默认允许null

        public int? Fslitemperaturebegin { get; set; } // int改为可空
        public string Fslimould { get; set; }          // string默认允许null
        public string Fsliroller { get; set; }         // string默认允许null
        public int? Fsliheatingtimes { get; set; }     // int改为可空
        public string Fsligrade { get; set; }          // string默认允许null
        public string Fsumnumber { get; set; }         // string默认允许null

        public int? Fsoqty { get; set; }               // int改为可空
        public int? Fwoqty { get; set; }               // int改为可空
        public int? Fwpqty { get; set; }               // int改为可空
        public int? Ffinisthqty { get; set; }          // int改为可空
        public int? Fstockqty { get; set; }            // int改为可空
        public int? Foption { get; set; }              // int改为可空
        public int? Fobject { get; set; }              // int改为可空
        public int? Fmo { get; set; }                  // int改为可空
        public int? Fworkorderliststatus { get; set; } // int改为可空
        public int? Fpause { get; set; }               // int改为可空
        public int? Fcancel { get; set; }              // int改为可空

        public virtual sli_work_order sli_work_order { get; set; }


        public class sli_work_orderprocess_view
        {
            [Key]
            public long RowNum { get; set; }  // 主键

            public int Id { get; set; }  // 主键

            public int Fprocessoption { get; set; }  // 工序ID

            public string Foptionname { get; set; }  // 工序选项名称

            public int Fseq { get; set; }  // 工序顺序

            public decimal Fqty { get; set; }  // 数量

            public decimal Fweight { get; set; }  // 重量

            public decimal Fcommitqty { get; set; }  // 提交数量

            public decimal Fcommitweight { get; set; }  // 提交重量

            // 外键关联到主表 sli_work_order_view
            //public int WorkOrderId { get; set; }  // 外键字段

            // 导航属性，表示与主表的关系
            //public sli_work_order_view sli_work_order_view { get; set; }
        }
    }


    public class sli_work_orderprocess_view
    {
        [Key]
        public long RowNum { get; set; }  // 主键

        public int Id { get; set; }  // 主键

        public int Fprocessoption { get; set; }  // 工序ID

        public string Foptionname { get; set; }  // 工序选项名称

        public int Fseq { get; set; }  // 工序顺序

        public decimal Fqty { get; set; }  // 数量

        public decimal Fweight { get; set; }  // 重量

        public decimal Fcommitqty { get; set; }  // 提交数量

        public decimal Fcommitweight { get; set; }  // 提交重量

        // 外键关联到主表 sli_work_order_view
        //public int WorkOrderId { get; set; }  // 外键字段

        // 导航属性，表示与主表的关系
        //public sli_work_order_view sli_work_order_view { get; set; }
    }
}