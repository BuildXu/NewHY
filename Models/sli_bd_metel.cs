using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    //材质
    public class sli_bd_metel
    {
        public List<string> NeedUpDateFields { get; set; }
        public List<string> NeedReturnFields { get; set; }
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
        public string IsAutoSubmitAndAudit { get; set; }
        public DataModel Model { get; set; }
    }

    public class DataModel
    {
        public int FID { get; set; }
        public string FNumber { get; set; }
        public string FName { get; set; }
        public OrgIdmetel FCreateOrgId { get; set; }
        public OrgIdmetel FUseOrgId { get; set; }
    }

    public class OrgIdmetel
    {
        public string FNumber { get; set; }
    }
}