using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_document_quality_standard
    {
        public int Id { get; set; }
        public string Fnumber { get; set; }
        public string Fname { get; set; }
        public string Fdate { get; set; }
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
        public virtual ICollection<sli_document_quality_standardBill> sli_document_quality_standardBill { get; set; }
        public virtual ICollection<sli_document_quality_standardBillEntry> sli_document_quality_standardBillEntry { get; set; }
        public virtual ICollection<sli_document_quality_standardAttachment> sli_document_quality_standardAttachment { get; set; }
    }
    public class sli_document_quality_standardBill
    {
        public int id { get; set; }
        public int fmainID { get; set; }
        public int fqualityOptionID { get; set; }
        public string fnote { get; set; }
        public virtual sli_document_quality_standard sli_document_quality_standard { get; set; }

    }

    public class sli_document_quality_standardBillEntry
    {
        public int id { get; set; }
        public int fbillID { get; set; }
        public int fqualityObjectID { get; set; }
        public string fmax { get; set; }
        public string fmin { get; set; }
        public string ftarget { get; set; }
        public string fnote { get; set; }
        public string fnoties { get; set; }
        public string fexplanation { get; set; }
        public virtual sli_document_quality_standard sli_document_quality_standard { get; set; }
    }
    public class sli_document_quality_standardAttachment
    {
        public int id { get; set; }
        public int fmainID { get; set; }
        public string fattachment { get; set; }
        public byte[] fileData { get; set; }
        public virtual sli_document_quality_standard sli_document_quality_standard { get; set; }
    }
}