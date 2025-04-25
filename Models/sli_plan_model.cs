using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace WebApi_SY.Models
{
    public class Sli_plan_model
    {
        public int Id { get; set; }
        public string Fmodelnumber { get; set; }   //
        public string Fmodelname { get; set; }
        public string Fplanbegindate { get; set; }
        public string Fplanenddate { get; set; }
        public int Fdays { get; set; }
        public string Fnote { get; set; }
        public virtual ICollection<Sli_plan_modelEntry> Sli_plan_modelEntry { get; set; }
    }
    public class Sli_plan_modelEntry
    {
        public int Id { get; set; }
        public int Fmodelid { get; set; }
        public int Fplanoptionid { get; set; }
        public int Fdays { get; set; }
        public int Fdepartid { get; set; }
        public int Fseq { get; set; }
        public string Fempid { get; set; }
        [JsonIgnore]
        public virtual Sli_plan_model Sli_plan_model { get; set; }
    }
}