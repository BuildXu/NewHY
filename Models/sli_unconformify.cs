using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_unconformify  //不合格品处理单
    {
        
        public int Id { get; set; }    //自增ID
        public string Fnumber { get; set; }  // 锻件编号
        public string FnameSpec { get; set; } //名称规格
        public string FmaterialNo { get; set; }//材质
        public string FMarcrialspec { get; set; }//用料规格
        public string FtackingNo { get; set; }//材料跟踪号
        public int? Ftotalnumber { get; set; }//计划数
        public string Fprocess { get; set; }//所在工序
        public string Fposition { get; set; }//工件位置
        public int? Funnumber { get; set; }//不合格数
        public string Fdescription { get; set; }//产品不合格描述
        public int? Fdepid { get; set; }//责任部门ID
        public int? Fresponsibleid { get; set; }//责任人ID
        public int? Finspector { get; set; }//检验员ID
        public string FInopinion { get; set; }//检验员意见
        public int Fworkorderlistid { get; set; }//工件ID
        public int Fproductno { get; set; }//工件编号
    }
}