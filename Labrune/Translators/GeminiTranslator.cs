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

        public bool RequiresApiKey { get { return true; } }
        public bool SupportsCustomPrompt { get { return true; } }
        public bool SupportsModelSelection { get { return true; } }

        public async Task<string> Translate(HttpClient client, string text, string customPrompt, string modelName, string extraParams)
        {
            // extraParams or modelName could specify model. Default: gemini-pro
            string model = string.IsNullOrEmpty(modelName) ? "gemini-pro" : modelName;
            
            // Prepare the request body
            // Gemini doesn't have a distinct "system" role in the generateContent API in the same way as Chat
            // But we can prepend instructions.
            string finalPrompt = string.IsNullOrEmpty(customPrompt) 
                ? "Translate the following text to Turkish: " + text 
                : customPrompt + "\n\nText to translate: " + text;

            string jsonContent = "{"
                + "\"contents\": [{"
                    + "\"parts\": [{"
                        + "\"text\": \"" + EscapeJson(finalPrompt) + "\""
                    + "}]"
                + "}]"
            +"}";

            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("x-goog-api-key", this.ApiKey);
                var response = await client.PostAsync("https://generativelanguage.googleapis.com/v1beta/models/" + model + ":generateContent", content);
                string responseString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    // Parse "text": "..."
                    int contentIndex = responseString.IndexOf("\"text\":");
                    if (contentIndex > 0)
                    {
                        int startQuote = responseString.IndexOf("\"", contentIndex + 7);
                        if (startQuote > 0)
                        {
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
