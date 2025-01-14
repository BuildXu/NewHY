using System;

public class sli_quality_request_view
{
    // 对应 Fnumber 列，存储 varchar 类型的数据
    public string Fnumber { get; set; }  //  请检单号
    // 对应 Id 列，存储 int 类型的数据   ////不用显示
    public int Id { get; set; } // 请检单ID   //不用显示
    // 对应 Fdeptid 列，存储 int 类型的数据
    public int Fdeptid { get; set; }  // 部门ID  //不用显示
    // 对应 Fempid 列，存储 int 类型的数据
    public int Fempid { get; set; } // 请检人 ID  //不用显示
    // 对应 Fdate 列，存储 datetime 类型的数据
    public DateTime Fdate { get; set; }  // 单据日期
    // 对应 Fendate 列，存储 datetime 类型的数据
    public DateTime Fendate { get; set; }  // 计划结束日期
    // 对应 Fstatus 列，存储 int 类型的数据
    public int Fstatus { get; set; }  // 单据状态
    // 对应 Fbillerid 列，存储 int 类型的数据
    public int Fbillerid { get; set; }  // 制单人
    // 对应 fqty 列，存储 numeric 类型的数据，在 C# 中使用 double 类型表示
    public double Fqty { get; set; }  //请检数量
    // 对应 Fproductno 列，存储 varchar 类型的数据
    public string Fproductno { get; set; }   //工件号
    // 对应 Fname 列，存储 nvarchar 类型的数据，在 C# 中使用 string 类型表示
    public string Fname { get; set; }  // 工件名称
    // 对应 Fdescription 列，存储 nvarchar 类型的数据，在 C# 中使用 string 类型表示
    public string Fdescription { get; set; } //工件规格
    // 对应 dept_name 列，存储 varchar 类型的数据
    public string dept_name { get; set; }  // 部门名称
    // 对应 empName 列，存储 varchar 类型的数据
    public string empName { get; set; } //  请检人名称

    // 无参构造函数
    public sli_quality_request_view()
    {
    }

    // 有参构造函数，用于初始化对象时直接赋值
    public sli_quality_request_view(string Fnumber, int Id, int Fdeptid, int Fempid, DateTime Fdate, DateTime Fendate, int Fstatus, int Fbillerid, double Fqty, string Fproductno, string Fname, string Fdescription, string dept_name, string empName)
    {
        this.Fnumber = Fnumber;
        this.Id = Id;
        this.Fdeptid = Fdeptid;
        this.Fempid = Fempid;
        this.Fdate = Fdate;
        this.Fendate = Fendate;
        this.Fstatus = Fstatus;
        this.Fbillerid = Fbillerid;
        this.Fqty = Fqty;
        this.Fproductno = Fproductno;
        this.Fname = Fname;
        this.Fdescription = Fdescription;
        this.dept_name = dept_name;
        this.empName = empName;
    }
}