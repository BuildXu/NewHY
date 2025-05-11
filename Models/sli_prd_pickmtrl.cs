using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace WebApi_SY.Models
{/// <summary>
/// 生产领料单
/// </summary>
    public class sli_prd_pickmtrl
    {
        [Key]
        public int Fid { get; set; }
        /// <summary>
        /// 单据编号
        /// </summary>
        public string Fbillno { get; set; }
        /// <summary>
        /// 单据日期
        /// </summary>
        public DateTime Fdate { get; set; }
        /// <summary>
        /// 制单人ID
        /// </summary>
        public int? FBiller { get; set; }
        /// <summary>
        /// 部门ID
        /// </summary>
        public int? FDepId { get; set; }
        /// <summary>
        /// 单据状态
        /// </summary>
        public string Fdocumentstatus { get; set; }
        /// <summary>
        /// 关闭状态
        /// </summary>
        public string Fclosestatus { get; set; }
        /// <summary>
        /// 标志位
        /// </summary>
        public int? Flag { get; set; }
        /// <summary>
        /// 参数(JSON格式)
        /// </summary>
        public string FParameter { get; set; }
        /// <summary>
        /// 原因说明
        /// </summary>
        public string FReason { get; set; }
        public virtual ICollection<sli_prd_pickmtrlentry> sli_prd_pickmtrlentry { get; set; }
    }
    public class sli_prd_pickmtrlentry
    {
        /// <summary>
        /// 主表关联ID（外键）
        /// </summary>
        public int Fid { get; set; }
        /// <summary>
        /// 分录ID（自增主键）
        /// </summary>
        [Key]
        public int Fentryid { get; set; }
        /// <summary>
        /// 物料编号
        /// </summary>
        public string Fnumber { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        public string Fname { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }
        public string Fbatchno { get; set; }
        /// <summary>
        /// 数量（主单位）
        /// </summary>
        public decimal? Fqty { get; set; }

        /// <summary>
        /// 数量（辅单位）
        /// </summary>
        public decimal? FSecQty { get; set; }
        /// <summary>
        /// 仓库编号
        /// </summary>
        public string FStockNumber { get; set; }
        public virtual sli_prd_pickmtrl sli_prd_pickmtrl { get; set; }
    }

    public class sli_prd_pickmtrl_view
    {
        [Key]
        /// <summary>
        /// 主键ID（注意：表中定义为NULL，建议添加标识列）
        /// </summary>
        public int Fid { get; set; }

        /// <summary>
        /// 单据编号
        /// </summary>
        public string Fbillno { get; set; }

        /// <summary>
        /// 单据日期
        /// </summary>
        public DateTime Fdate { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        //public string Fsppliername { get; set; }

        /// <summary>
        /// 单据状态
        /// </summary>
        public string Fdocumentstatus { get; set; }

        /// <summary>
        /// 关闭状态
        /// </summary>
        public string Fclosestatus { get; set; }

        /// <summary>
        /// 制单人ID
        /// </summary>
        public int? FBiller { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public int? FDepId { get; set; }

        /// <summary>
        /// 标志位
        /// </summary>
        public int Flag { get; set; }

        /// <summary>
        /// 参数（JSON格式）
        /// </summary>
        public string FParameter { get; set; }

        /// <summary>
        /// 原因说明
        /// </summary>
        public string FReason { get; set; }

        public string empname { get; set; }   //制单人名称 
        public string dept_name { get; set; }   //部门名称 
        public virtual ICollection<sli_prd_pickmtrlentry_view> sli_prd_pickmtrlentry_view { get; set; }
    }

    public class sli_prd_pickmtrlentry_view
    {
        /// <summary>
        /// 主表ID（外键关联sli_stk_instock.Fid）
        /// </summary>
        public int? Fid { get; set; }
        [Key]
        /// <summary>
        /// 分录ID（建议与Fid组合作为复合主键）
        /// </summary>
        public int? Fentryid { get; set; }

        /// <summary>
        /// 物料编号
        /// </summary>
        public string Fnumber { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public string Fname { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }
        public string Fbatchno { get; set; }

        /// <summary>
        /// 数量（主单位）
        /// </summary>
        public decimal? Fqty { get; set; }

        /// <summary>
        /// 数量（辅单位）
        /// </summary>
        public decimal? FSecQty { get; set; }

        /// <summary>
        /// 仓库代码
        /// </summary>
        public string FStockNumber { get; set; }

        public virtual sli_prd_pickmtrl_view sli_prd_pickmtrl_view { get; set; }
    }



    public class MaterialEntity
    {
        public FNumberWrapper FMaterialId { get; set; }
        public FNumberWrapper FUnitID { get; set; }
        public decimal FAppQty { get; set; }
        public decimal FActualQty { get; set; }
        public FNumberWrapper FStockId { get; set; }
        public bool FIsAffectCost { get; set; }
        public FNumberWrapper FStockUnitId { get; set; }
        public decimal FStockActualQty { get; set; }
        public string FOwnerTypeId { get; set; }
        public decimal FExtAuxUnitQty { get; set; }
        public decimal FPrice { get; set; }
        public decimal FAmount { get; set; }
        public string FParentOwnerTypeId { get; set; }
        public FNumberWrapper FParentOwnerId { get; set; }
        public FNumberWrapper FBaseUnitId { get; set; }
        public decimal FBaseActualQty { get; set; }
        public FNumberWrapper FStockStatusId { get; set; }
        public FNumberWrapper FOwnerId { get; set; }
        public decimal FSecActualQty { get; set; }
        public decimal FBaseAppQty { get; set; }
        public string FKeeperTypeId { get; set; }
        public FNumberWrapper FKeeperId { get; set; }
        public int FBomEntryId { get; set; }
    }

    public class TransferModel
    {
        public int FID { get; set; }
        public FNumberWrapper FBillType { get; set; }
        public string FDate { get; set; }
        public string FBillNo { get; set; }

        public FNumberWrapper FStockOrgId { get; set; }
        public FNumberWrapper FCurrId { get; set; }
        public string FOwnerTypeId0 { get; set; }
        public FNumberWrapper FOwnerId0 { get; set; }
        public FNumberWrapper FPrdOrgId { get; set; }
        public FNumberWrapper FWorkShopId { get; set; }
        public FNumberWrapper FTransferBizTypeId { get; set; }
        public string FBizType { get; set; }
        public List<MaterialEntity> FEntity { get; set; }
    }

    public class TransferRequest
    {
        public List<string> NeedUpDateFields { get; set; } = new List<string>();
        public List<string> NeedReturnFields { get; set; } = new List<string>();
        public string IsDeleteEntry { get; set; }
        public string SubSystemId { get; set; }
        public string IsVerifyBaseDataField { get; set; }
        public string IsEntryBatchFill { get; set; }
        public string ValidateFlag { get; set; }
        public string NumberSearch { get; set; }
        public string IsAutoAdjustField { get; set; }
        public string InterationFlags { get; set; }
        public string IgnoreInterationFlag { get; set; }
        public string IsControlPrecision { get; set; }
        public string ValidateRepeatJson { get; set; }
        public TransferModel Model { get; set; }
    }
}