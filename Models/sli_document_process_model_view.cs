using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_document_process_model_view
    {
        [Key]
        public int Id { get; set; }
        public string Fnumber { get; set; }  // 工艺档案编号

        public string Fname { get; set; }     // 名称
        public string Fdate { get; set; }     // 日期
        public int? Fbillerid { get; set; }    // 创建人
        public string Fstatus { get; set; }   // 状态
        public string Ftaxtrue { get; set; }    // 材质
        public string Fdefind01 { get; set; }   // 以下为备用字段
        public string Fdefind02 { get; set; }
        public string Fdefind03 { get; set; }
        public string Fdefind04 { get; set; }
        public string Fdefind05 { get; set; }
        public virtual ICollection<sli_document_process_modelBill_view> sli_document_process_modelBill_view { get; set; }
        //public virtual ICollection<sli_document_process_modelBillEntry> sli_document_process_modelBillEntry { get; set; }
        public virtual ICollection<sli_document_process_modelAttachment_view> sli_document_process_modelAttachment_view { get; set; }
    }
    public class sli_document_process_modelBill_view
    {
        [Key]
        public int Fbillid { get; set; }   //  ID
        public int Id { get; set; }      //  主表id
        public int? Foptionid { get; set; }   //  工序id （ sli_bd_process_option /  id）
        public int? Fdeptid { get; set; }    // 部门 id  
        public string Fnote { get; set; }    // 
        public int? Fstatus { get; set; }  //  状态 0 / 1  完成 / 未完成
        public virtual sli_document_process_model_view sli_document_process_model_view { get; set; }
        public virtual ICollection<sli_document_process_modelBillEntry_view> sli_document_process_modelBillEntry_view { get; set; }
    }

    public class sli_document_process_modelBillEntry_view
    {
        [Key]
        public int Fentryid { get; set; }    //   ID
        public int Fbillid { get; set; }     //  li_document_process_modelBill / Fbillid
        public int? Fobjectid { get; set; }    //  工步id   （ sli_bd_process_object /  id）
        public string Fmax { get; set; }       //  --不用显示- 直接默认 空
        public string Fmin { get; set; }    //  --不用显示- 直接默认 空
        public string Ftarget { get; set; }    //  --不用显示- 直接默认 空
        public string Fnote { get; set; }   //  --不用显示- 直接默认 空
        public string Fnoties { get; set; }   //  --工步说明-
        public string Fexplanation { get; set; }   //  --作业要求
        public virtual sli_document_process_modelBill_view sli_document_process_modelBill_view { get; set; }

    }

    public class sli_document_process_modelAttachment_view
    {
        [Key]
        public int id { get; set; }
        public int? fmainid { get; set; }
        public string fattachment { get; set; }
        public byte[] fileData { get; set; }
        public virtual sli_document_process_model_view sli_document_process_model_view { get; set; }

    }
}