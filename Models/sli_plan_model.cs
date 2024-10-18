using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace WebApi_SY.Models
{
    public class sli_plan_model
    {
        public int Id { get; set; }
        public string fmodelNumber { get; set; }
        public string fmodelName { get; set; }
        public string fplanBeginDate { get; set; }
        public string fplanEndDate { get;set;}
        public int fdays { get; set;}
        public string fnote { get; set; }
        public virtual ICollection<sli_plan_modelEntry> sli_plan_modelEntry { get; set; }
    }
    public class sli_plan_modelEntry
    {
        public int Id { get; set; }
        public int fmodelID { get; set; }
        public int fplanOptionId { get; set; }
        public int fdays { get; set; }
        public int fdepartID { get; set; }
        public string fempId { get; set; }
        [JsonIgnore]
        public virtual sli_plan_model sli_plan_model { get; set; }

    }

}