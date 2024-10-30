using System;
using System.Data;
using Newtonsoft.Json.Linq;

namespace WebApi_SY.Models
{
    public class JsonToDataTable
    {
        public DataTable JsonToDataTable1(string jsonString)
        {

            DataTable dataTable = new DataTable();  //实例化
            DataTable result;
            //try
            //{

                string json = @"[
    { 'Id': 1, 'Name': 'John', 'Age': 30 },
    { 'Id': 2, 'Name': 'Jane', 'Age': 25 },
    { 'Id': 3, 'Name': 'Bob', 'Age': 40 }
]";

                // 将 JSON 字符串解析为 JArray 对象
                JArray jsonArray = JArray.Parse(json);

                // 创建 DataTable
                 dataTable = new DataTable();

                // 添加列
                foreach (JProperty property in jsonArray[0])
                {
                    dataTable.Columns.Add(property.Name);
                }

                // 添加行
                foreach (JObject jsonObject in jsonArray)
                {
                    DataRow row = dataTable.NewRow();
                    foreach (JProperty property in jsonObject.Properties())
                    {
                        row[property.Name] = property.Value.ToString();
                    }
                    dataTable.Rows.Add(row);
                }

                // 输出 DataTable
                foreach (DataRow row in dataTable.Rows)
                {
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        Console.WriteLine($"{column.ColumnName}: {row[column]}");
                    }
                    Console.WriteLine();
                }

           // }
            //catch(Exception ex)
            //{
            //    //MessageBox.Show(ex.ToString());
            //    //result = ex.ToString();

            //}
            result = dataTable;
            return result;
        }
    }

}