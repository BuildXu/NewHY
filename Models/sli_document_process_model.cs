using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_document_process_model
    {
        [Key]
        public int Id { get; set; }
        public string Fnumber { get; set; }

        public string Fname { get; set; }
        public string Fdate { get; set; }
        public int? Fbillerid { get; set; }
        public string Fstatus { get; set; }
        public string Ftaxtrue { get; set; }
        public string Fdefind01 { get; set; }
        public string Fdefind02 { get; set; }
        public string Fdefind03 { get; set; }
        public string Fdefind04 { get; set; }
        public string Fdefind05 { get; set; }
        public virtual ICollection<sli_document_process_modelBill> sli_document_process_modelBill { get; set; }
        //public virtual ICollection<sli_document_process_modelBillEntry> sli_document_process_modelBillEntry { get; set; }
        public virtual ICollection<sli_document_process_modelAttachment> sli_document_process_modelAttachment { get; set; }
    }

    public class sli_document_process_modelBill
    {
        [Key]
        public int Fbillid { get; set; }
        public int Id { get; set; }
        public int? Foptionid { get; set; }
        public int? Fdeptid { get; set; }
        public string Fnote { get; set; }
        public int? Fstatus { get; set; }
        public virtual sli_document_process_model sli_document_process_model { get; set; }
        public virtual ICollection<sli_document_process_modelBillEntry> sli_document_process_modelBillEntry { get; set; }
    }

    public class sli_document_process_modelBillEntry
    {
        [Key]
        public int Fentryid { get; set; }
        public int Fbillid { get; set; }
        public int? Fobjectid { get; set; }
        public string Fmax { get; set; }
        public string Fmin { get; set; }
        public string Ftarget { get; set; }
        public string Fnote { get; set; }
        public string Fnoties { get; set; }
        public string Fexplanation { get; set; }
        public virtual sli_document_process_modelBill sli_document_process_modelBill { get; set; }

    }

    public class sli_document_process_modelAttachment
    {
        [Key]
        public int id { get; set; }
        public int? fmainid { get; set; }
        public string fattachment { get; set; }
        public byte[] fileData { get; set; }
        public virtual sli_document_process_model sli_document_process_model { get; set; }

    }

}