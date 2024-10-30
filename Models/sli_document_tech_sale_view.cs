using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_document_tech_sale_view
    {
        [Key]
        public int Id { get; set; }
        public string Fnumber { get; set; }
        public string Fname { get; set; }
        public string Fdate { get; set; }
        public int FbillerID { get; set; }
        public string Fstatus { get; set; }
        public string FcustomerID { get; set; }
        public int FmaterialID { get; set; }
        public int ForderNo { get; set; }
        public int ForderEntryID { get; set; }
        public string FstandardNo { get; set; }
        public string Ftaxtrue { get; set; }
        public string FmaterialName { get; set; }
        public string FcustomerName { get; set; }
        public string FSumNumber { get; set; }
        public virtual ICollection<sli_document_tech_saleBill_view> sli_document_tech_saleBill_view { get; set; }
        public virtual ICollection<sli_document_tech_saleBillEntry_view> sli_document_tech_saleBillEntry_view { get; set; }
        public virtual ICollection<sli_document_tech_saleAttachment_view> sli_document_tech_saleAttachment_view { get; set; }

    }
    public class sli_document_tech_saleBill_view
    {
        public int id { get; set; }
        public int fmainID { get; set; }
        public int ftechOptionID { get; set; }
        public string fnote { get; set; }
        public virtual sli_document_tech_sale_view sli_document_tech_sale_view { get; set; }

    }

    public class sli_document_tech_saleBillEntry_view
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
        public virtual sli_document_tech_sale_view sli_document_tech_sale_view { get; set; }
    }
    public class sli_document_tech_saleAttachment_view
    {
        public int id { get; set; }
        public int fmainID { get; set; }
        public string fattachment { get; set; }
        public byte[] fileData { get; set; }
        public virtual sli_document_tech_sale_view sli_document_tech_sale_view { get; set; }
    }

}