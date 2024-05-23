using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CourseSummaryDesign
{
    public static class JsonUtils
    {
        // 序列化对象到文件  
        public static void SerializeToFile<T>(T obj, string filePath)
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(obj);
                File.WriteAllText(filePath, jsonString);
            }
            catch
            {
                throw new Exception("序列化失败");
            }
        }

        // 从文件反序列化对象  
        public static T DeserializeFromFile<T>(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("文件不存在", filePath);
            }
            try
            {
                string jsonString = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<T>(jsonString);
            }
            catch
            {
                throw new Exception("反序列化失败");
            }
        }
    }
}
