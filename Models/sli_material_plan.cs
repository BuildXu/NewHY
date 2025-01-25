using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_material_plan
    {
        public int Id { get; set; }

        // 物料 ID
        public int Fmaterialid { get; set; }

        // 批号                                     来自即时库存
        public string Flotno { get; set; }

        // 仓库 ID                                 来自即时库存
        public int Fstockid { get; set; }

        // 库位 ID                                  来自即时库存
        public int Fstocklocid { get; set; }

        // 库存数量                                 来自即时库存
        public decimal Fqtystock { get; set; }

        // 库存重量                                 来自即时库存
        public decimal Fweightstock { get; set; }


        // 已使用数量                                  来自即时库存           
        public decimal Fqtyused { get; set; }

        // 已使用重量                               来自即时库存
        public decimal Fweightused { get; set; }

        // 工单列表 ID                                  来自工件视图                
        public int Fworkorderlistid { get; set; }

        // 产品编号                                    来自工件视图 
        public string Fproductno { get; set; }

        // 数量                                      引用   工件视图,  可手工修改
        public decimal Fqty { get; set; }

        // 已使用重量                              引用   工件视图,  可手工修改
        public decimal Fweight { get; set; }


        // 实际数量                                   后期 领料单返写   控制()
        public decimal Fqtyactul { get; set; }

        // 实际下料重量                                 后期 领料单返写   控制()                             
        public decimal Fweightactul { get; set; }

        // 状态                                             
        public decimal Fstatus { get; set; }


    }
}