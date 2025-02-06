using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public static class JsonParameterReplacer
{
    /// <summary>
    /// 替换JSON字符串中的占位符为实际值
    /// </summary>
    /// <param name="jsonString">原始的JSON字符串</param>
    /// <param name="parameters">参数键值对，用于替换占位符</param>
    /// <returns>替换后的JSON字符串</returns>
    public static string ReplaceParameters(string jsonString, Dictionary<string, object> parameters)
    {
        // 将JSON字符串解析为JObject
        var jsonObject = JObject.Parse(jsonString);

        // 遍历JSON对象，查找并替换占位符
        ReplaceTokens(jsonObject, parameters);

        // 将JObject转换回JSON字符串
        return jsonObject.ToString(Formatting.None);
    }

    /// <summary>
    /// 递归遍历JToken，替换占位符
    /// </summary>
    /// <param name="token">当前处理的JToken</param>
    /// <param name="parameters">参数键值对</param>
    private static void ReplaceTokens(JToken token, Dictionary<string, object> parameters)
    {
        if (token.Type == JTokenType.Object)
        {
            // 如果是对象，遍历其属性
            foreach (var property in ((JObject)token).Properties())
            {
                ReplaceTokens(property.Value, parameters);
            }
        }
        else if (token.Type == JTokenType.Array)
        {
            // 如果是数组，遍历其元素
            foreach (var item in (JArray)token)
            {
                ReplaceTokens(item, parameters);
            }
        }
        else if (token.Type == JTokenType.String)
        {
            // 如果是字符串，检查是否需要替换
            var value = token.ToString();
            if (value.StartsWith("$"))
            {
                var key = value.Substring(1); // 去掉$符号
                if (parameters.ContainsKey(key))
                {
                    // 替换为参数值
                    token.Replace(JToken.FromObject(parameters[key]));
                }
            }
        }
    }
}