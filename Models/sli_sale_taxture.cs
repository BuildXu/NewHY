using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_sale_taxture
    {
        public int ID { get; set; }
        public string FNumber { get; set; }
        public string FDate { get; set; }
        public string FCustomer { get; set; }
        public string FDeliveryDate { get; set; }
        public virtual ICollection<sli_sale_taxturebill> sli_sale_taxturebill { get; set; }
        public virtual ICollection<sli_sale_taxturebillEntry> sli_sale_taxturebillEntry { get; set; }
    }

    public class sli_sale_taxturebill
    {
        public int ID { get; set; }
        public int FMainId { get; set; }
        public string FTexture { get; set; }
        public int FSaleTechId { get; set; }
        public int FTechId { get; set; }
        public string FTechName { get; set; }
        public string FNum { get; set; }
        public string FWeight { get; set;}
        public string FTechNo { get; set;}
        public string FSaleTechNo { get; set; }
        public string Updatetime { get; set; }
        public virtual sli_sale_taxture sli_sale_taxture { get; set; }
        
    }

    public class sli_sale_taxturebillEntry
    {
        public int ID { get; set; }
        public int FBillId { get; set; }
        public int FOrderEntryId { get; set; }
        public string FTexture { get; set; }
        public int FSaleTechId { get; set; }
        public int FTechId { get; set; }
        public string FMaterialNo { get; set; }
        public string FBillNo { get; set; }
        public int FRowNo { get; set; }
        public string FTechNo { get; set; }
        public string FModel { get; set; }
        public string FSaleTechNo { get; set; }
        public virtual sli_sale_taxture sli_sale_taxture { get; set; }
    }


}