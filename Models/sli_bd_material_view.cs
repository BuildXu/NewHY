using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_bd_material_view
    {
        [Key]
        public int FMATERIALID { get; set; }
        public string FMaterialNumber { get; set; }
        public string FMaterialName { get; set; }
        public string FSumNumber { get; set; }
        public string FDESCRIPTION { get; set; }
        public string FSPECIFICATION { get; set; }
        //public string F_SLI_CUSTWORKORDER { get; set; }
        //public string F_SLI_PARTNO { get; set; }
        //public string F_SLI_CUSTMATERIALNO { get; set; }
        //public string F_SLI_PROJECTNO { get; set; }
        //public string F_SLI_MATERIALNOTICE { get; set; }
        //public string F_SLI_PRODUCTNOTICE { get; set; }
        //public string F_SLI_GOODSNOTICE { get; set; }
        //public string F_SLI_CUSTORDERNO { get; set; }
        //public string F_SLI_CUSTORDERLINE { get; set; }
        //public string F_SLI_PRODUCTNOTE { get; set; }
        //public string F_SLI_PRODUCTTYPE { get; set; }
        //public string F_SLI_AREA { get; set; }
        //public string F_SLI_PROJECTNAME { get; set; }
        //public string FDOCUMENTSTATUS { get; set; }

    }
}