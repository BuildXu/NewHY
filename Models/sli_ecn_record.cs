using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;

    public class sli_ecn_record
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("fbillno")]
        public string FBillNo { get; set; }

        [JsonPropertyName("forderentryid")]
        public int FOrderEntryId { get; set; }

        [JsonPropertyName("forderno")]
        public string FOrderNo { get; set; }

        [JsonPropertyName("fseq")]
        public string FSeq { get; set; }

        [JsonPropertyName("fmaterialid")]
        public int FMaterialId { get; set; }

        [JsonPropertyName("fnumber")]
        public string FNumber { get; set; }

        [JsonPropertyName("fnamea")]
        public string FNameA { get; set; }

        [JsonPropertyName("fdescriptiona")]
        public string FDescriptionA { get; set; }

        [JsonPropertyName("fnameb")]
        public string FNameB { get; set; }

        [JsonPropertyName("fdescriptionb")]
        public string FDescriptionB { get; set; }

        [JsonPropertyName("fnote")]
        public string FNote { get; set; }
        public int Fdocid { get; set; }
        public string Fdocno { get; set; }
        public string Fslitechno { get; set; }
    }
    }