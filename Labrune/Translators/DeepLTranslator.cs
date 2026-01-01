using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Labrune.Translators
{
    public class DeepLTranslator : ITranslator
    {
        private string ApiKey;

        public DeepLTranslator(string apiKey)
        {
            this.ApiKey = apiKey;
        }

        public async Task<string> Translate(HttpClient client, string text, string[] rules)
        {
            // Prepare the request body
            string jsonContent = "{"
                + "\"text\": [\"" + EscapeJson(text) + "\"],"
                + "\"target_lang\": \"TR\""
            +"}";

            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "DeepL-Auth-Key " + this.ApiKey);
                var response = await client.PostAsync("https://api-free.deepl.com/v2/translate", content);
                string responseString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    // Parse "text": "..."
                    // Very hacky parser for demo purposes
                    int contentIndex = responseString.IndexOf("\"text\":");
                    if (contentIndex > 0)
                    {
                        int startQuote = responseString.IndexOf("\"", contentIndex + 7);
                        if (startQuote > 0)
                        {
                            // Find end quote, watching for escapes
                            int endQuote = startQuote + 1;
                            while (endQuote < responseString.Length)
                            {
                                if (responseString[endQuote] == '"' && responseString[endQuote - 1] != '\\')
                                {
                                    break;
                                }
                                endQuote++;
                            }

                            if (endQuote < responseString.Length)
                            {
                                string result = responseString.Substring(startQuote + 1, endQuote - startQuote - 1);
                                return UnescapeJson(result);
                            }
                        }
                    }
                }
            }
            catch { }

            return text; // Return original on failure
        }

        private string EscapeJson(string s)
        {
            if (s == null) return "";
            return s.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\r", "").Replace("\n", "\\n");
        }

        private string UnescapeJson(string s)
        {
            if (s == null) return "";
            return s.Replace("\\\"", "\"").Replace("\\n", "\n").Replace("\\\\", "\\");
        }
    }
}
