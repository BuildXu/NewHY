using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_witnessing_object
    {
        // 表示唯一标识符，通常作为主键
        public int Id { get; set; }

        // 存储材料的标识符，可能是材料的唯一编码或名称，使用 varchar(50) 存储字符串
        public string Fmaterialid { get; set; }

        // 存储客户的标识符，可能是客户的唯一编号，用于关联客户信息
        public int Fcustomer { get; set; }

        // 存储注释信息，这里类型为 int，可根据实际需求修改为更合适的类型，如 string
        public string Fnote { get; set; }

        // 存储状态信息，通常用于表示对象的状态，如 0 表示未处理，1 表示已处理等
        public int Fstatus { get; set; }

        public virtual ICollection<sli_witnessing_objectbill> sli_witnessing_objectbill { get; set; }
    }

    public class sli_witnessing_objectbill
    {
        // 可能是条目唯一标识符
        public int Fentryid { get; set; }

        // 表示唯一标识符，通常作为主键或外键
        public int Id { get; set; }

        // 存储序列号，可能用于排序或标识顺序
        public int Fseq { get; set; }

        // 存储对象标识符，可能用于关联其他对象，具体含义取决于业务逻辑
        public int Fobject { get; set; }
        public virtual sli_witnessing_object sli_witnessing_object { get; set; }
    }
}