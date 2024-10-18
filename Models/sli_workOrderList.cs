using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_workOrderList
    {
        public int id { get; set; }
        public string forderEntryid { get; set; }
        public string fproductNumber { get; set; }
        public int fmaterialid { get; set; }
        public decimal fworkQty { get; set; }
        public decimal fworkWeight { get; set; }
        public string fnote { get; set; }
        public int fworkOrderListStatus { get; set; }
        public string splittype { get; set; }
    }
}