using System.Collections.Generic;
using System.Text;

namespace UnityEngine.AppylarSdkWrapper.Utilities.JSON{
    internal static class Json{
        public static string ConvertDictionaryToJson(Dictionary<string, string[]> dictionary){
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{");

            bool isFirstEntry = true;
            foreach (var keyValuePair in dictionary){
                if (!isFirstEntry)
                    jsonBuilder.Append(",");

                string key = keyValuePair.Key;
                string[] values = keyValuePair.Value;

                jsonBuilder.Append($"\"{key}\":[");

                if (values.Length > 0){
                    jsonBuilder.Append($"\"{EscapeString(values[0])}\"");

                    for (int i = 1; i < values.Length; i++){
                        jsonBuilder.Append($",\"{EscapeString(values[i])}\"");
                    }
                }

                jsonBuilder.Append("]");

                isFirstEntry = false;
            }

            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        private static string EscapeString(string input){
            return input.Replace("\"", "\\\"").Replace("\n", "\\n").Replace("\r", "\\r").Replace("\t", "\\t");
        }

    }

}