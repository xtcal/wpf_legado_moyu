using Newtonsoft.Json;
namespace wpf_legado_moyu {
    public static class Tools {
        public static string ToJson(this object _object) {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;  //不序列自我循环引用，循环引用的字段会丢失
            System.IO.TextReader tr = new System.IO.StringReader(JsonConvert.SerializeObject(_object, settings));
            JsonTextReader jtr = new JsonTextReader(tr);
            JsonSerializer serializer = new JsonSerializer();
            var obj = serializer.Deserialize(jtr);
            System.IO.StringWriter textWriter = new System.IO.StringWriter();
            JsonTextWriter jsonWriter = new JsonTextWriter(textWriter) {
                Formatting = Formatting.Indented,
                Indentation = 4,
                IndentChar = ' '
            };
            serializer.Serialize(jsonWriter, obj);
            return textWriter.ToString();
        }
        public static long DateTimeToLongTimeStamp() {
            DateTime dateTime = DateTime.Now;
            return (long)(dateTime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
        }
    }
}