using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_sal_deliverynotice
    {
        [Key]
        public int FId { get; set; }
        public string FBillNo { get; set; }
        public string FNumber { get; set; }
        public string FShortName { get; set; }
        public string FAddress { get; set; }
        public string FOrderNo { get; set; }
    }
    public class sli_sal_deliverynoticeentry
    {
        
        public int FId { get; set; }
        public int FSeq { get; set; }
        public string FNumber { get; set; }
        public decimal FQty { get; set; }
        public bool FIsVmiBusiness { get; set; }
        [Key]
        public int FEntryId { get; set; }
        public DateTime FDeliveryDate { get; set; }
    }
}