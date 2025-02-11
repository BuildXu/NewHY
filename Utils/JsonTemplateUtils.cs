using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace JsonTemplateUtils
{
    public static class JsonTemplateUtils
    {
        /// <summary>
        /// 获取JSON模板
        /// </summary>
        public static string GetJson(string filePath)
        {
            string jsonStr = File.ReadAllText(filePath, Encoding.UTF8);
            return Regex.Replace(jsonStr, @"\s*|\r|\n|\t", "");
        }

        /// <summary>
        /// 通过字符串替换拼装模板
        /// </summary>
        public static string AssemblingByReplace(string jsonTemplate, object param)
        {
            string paramStr = JObject.FromObject(param).ToString();
            JObject paramsObj = JObject.Parse(paramStr);

            foreach (var kvp in paramsObj)
            {
                string key = kvp.Key;
                JToken value = kvp.Value;

                string replaceSymbol1 = $"\"${{{key}}}\"";  // 处理带引号的占位符
                string replaceSymbol2 = $"${{{key}}}";      // 处理不带引号的占位符

                if (value.Type == JTokenType.Object || value.Type == JTokenType.Array)
                {
                    jsonTemplate = jsonTemplate.Replace(replaceSymbol1, value.ToString(Newtonsoft.Json.Formatting.None));
                }
                else if (value.Type == JTokenType.String)
                {
                    jsonTemplate = jsonTemplate.Replace(replaceSymbol2, value.ToString());
                }
                else
                {
                    jsonTemplate = jsonTemplate.Replace(replaceSymbol1, value.ToString());
                }
            }
            return jsonTemplate;
        }

        /// <summary>
        /// 通过数组分割拼装模板
        /// </summary>
        public static string AssemblingByArray(string jsonTemplate, object param)
        {
            string paramStr = JObject.FromObject(param).ToString();
            JObject paramsObj = JObject.Parse(paramStr);

            StringBuilder ret = new StringBuilder();
            string[] split = Regex.Split(jsonTemplate, "\"\\$\\{");

            for (int i = 0; i < split.Length; i++)
            {
                if (i == 0)
                {
                    ret.Append(split[i]);
                }
                else
                {
                    string segment = split[i];
                    int endIndex = segment.IndexOf("}\"");
                    if (endIndex == -1)
                    {
                        ret.Append("\"${").Append(segment);
                        continue;
                    }

                    string key = segment.Substring(0, endIndex);
                    string remaining = segment.Substring(endIndex + 2);

                    if (!paramsObj.TryGetValue(key, out JToken value))
                    {
                        ret.Append("\"\"");
                    }
                    else
                    {
                        if (value.Type == JTokenType.Object || value.Type == JTokenType.Array)
                        {
                            ret.Append(value.ToString(Newtonsoft.Json.Formatting.None));
                        }
                        else if (value.Type == JTokenType.String)
                        {
                            ret.Append('"').Append(value).Append('"');
                        }
                        else
                        {
                            ret.Append(value);
                        }
                    }
                    ret.Append(remaining);
                }
            }
            return ret.ToString();
        }
    }
}



// 需要安装Newtonsoft.Json NuGet包
//// 使用示例：
//var template = JsonTemplateUtils.GetJson("template.json");
//var parameters = new
//{
//    name = "John",
//    age = 30,
//    address = new
//    {
//        city = "New York"
//    }
//};

//string result1 = JsonTemplateUtils.AssemblingByReplace(template, parameters);
//string result2 = JsonTemplateUtils.AssemblingByArray(template, parameters);