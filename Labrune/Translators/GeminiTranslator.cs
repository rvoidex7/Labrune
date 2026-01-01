using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Labrune.Translators
{
    public class GeminiTranslator : ITranslator
    {
        private string ApiKey;

        public GeminiTranslator(string apiKey)
        {
            this.ApiKey = apiKey;
        }

        public async Task<string> Translate(HttpClient client, string text, string[] rules)
        {
            // Prepare the request body
            string jsonContent = "{"
                + "\"contents\": [{"
                    + "\"parts\": [{"
                        + "\"text\": \"Translate the following text to Turkish, following these rules: " + string.Join(", ", rules) + ". Text to translate: " + EscapeJson(text) + "\""
                    + "}]"
                + "}]"
            +"}";

            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("x-goog-api-key", this.ApiKey);
                var response = await client.PostAsync("https://generativelanguage.googleapis.com/v1beta/models/gemini-pro:generateContent", content);
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
