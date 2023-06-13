using System.Text.Json;

namespace Saas.Helpers
{
    public static class JSONHelper
    {
        private static JsonSerializerOptions _jsonSerializer;
        
        static JSONHelper()
        {
            _jsonSerializer = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true,
                AllowTrailingCommas = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            };
        }
        public static TModel Parse<TModel>(string json)
        {
            try
            {
                return JsonSerializer.Deserialize<TModel>(json, _jsonSerializer);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return default(TModel);
            }
        }
    }
}