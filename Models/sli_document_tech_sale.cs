using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Web;

namespace WebApi_SY.Models
{
    //产品工艺档案
    public class sli_document_tech_sale
    {
        public int Id { get; set; }
        public string Fnumber { get; set; }
        public string Fname { get; set; }
        public DateTime Fdate { get; set; }
        public int FbillerID { get; set; }
        public string Fstatus { get; set; }
        public int FcustomerID { get; set; }
        public int FmaterialID { get; set; }
        public int ForderNo { get; set; }
        public int ForderEntryID { get; set; }
        public string FstandardNo { get; set; }
        public string Ftaxtrue { get; set; }
   
        public string fdefind01 { get; set; }
        public string fdefind02 { get; set; }
        public string fdefind03 { get; set; }
        public string fdefind04 { get; set; }
        public string fdefind05 { get; set; }
        public virtual ICollection<sli_document_tech_saleBill> sli_document_tech_saleBill { get; set; }
        public virtual ICollection<sli_document_tech_saleBillEntry> sli_document_tech_saleBillEntry { get; set; }
        public virtual ICollection<sli_document_tech_saleAttachment> sli_document_tech_saleAttachment { get; set; }

    }

    public class sli_document_tech_saleBill
    {
        public int id { get; set; }
        public int fmainID { get; set; }
        public int ftechOptionID { get; set; }
        public string fnote { get; set; }
        public virtual sli_document_tech_sale sli_document_tech_sale { get; set; }

    }

    public class sli_document_tech_saleBillEntry
    {
        public int id { get; set; }
        public int fbillID { get; set; }
        public int ftechObjectID { get; set; }
        public string fmax { get; set; }
        public string fmin { get; set; }
        public string ftarget { get; set; }
        public string fnote { get; set; }
        public string fnoties { get; set; }
        public string fexplanation { get; set; }
        public virtual sli_document_tech_sale sli_document_tech_sale { get; set; }
    }
    public class sli_document_tech_saleAttachment
    {
        public int id { get; set; }
        public int fmainID { get; set; }
        public string fattachment { get; set; }
        public byte[] fileData { get; set; }
        public virtual sli_document_tech_sale sli_document_tech_sale { get; set; }
    }
}