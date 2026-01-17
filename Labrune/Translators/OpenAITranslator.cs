using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Labrune.Translators
{
    public class OpenAITranslator : ITranslator
    {
        private string ApiKey;

        public OpenAITranslator(string apiKey)
        {
            this.ApiKey = apiKey;
        }

        public bool RequiresApiKey { get { return true; } }
        public bool SupportsCustomPrompt { get { return true; } }
        public bool SupportsModelSelection { get { return true; } }

        public async Task<string> Translate(HttpClient client, string text, string customPrompt, string modelName, string extraParams)
        {
            string baseUrl = string.IsNullOrEmpty(extraParams) ? "https://api.openai.com/v1/chat/completions" : extraParams;
            string model = string.IsNullOrEmpty(modelName) ? "gpt-3.5-turbo" : modelName;
            
            // Build the JSON with the user's custom prompt and model
            string jsonContent = "{" +
                "\"model\": \"" + model + "\"," +
                "\"messages\": [" +
                    "{\"role\": \"system\", \"content\": \"" + EscapeJson(customPrompt) + "\"}," +
                    "{\"role\": \"user\", \"content\": \"" + EscapeJson(text) + "\"}]" +
            "}";

            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", this.ApiKey));
                
                // Allow for custom headers if needed in future, but for now just Auth
                
                var response = await client.PostAsync(baseUrl, content);
                string responseString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    // Parse "content": "..." using the same manual but robust logic
                    int contentIndex = responseString.IndexOf("\"content\":");
                    if (contentIndex > 0)
                    {
                        int startQuote = responseString.IndexOf("\"", contentIndex + 10);
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
                else
                {
                    // Log error if possible or just return original
                    // For now, we return text, but the caller will see it didn't change
                    // Ideally we should throw or return a visual error, but legacy code pattern returns text.
                    // Let's print to console for now
                    Console.WriteLine("API Error: " + response.StatusCode + " " + responseString);
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Exception: " + ex.Message);
            }

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
            return s.Replace("\\\"", "\"").Replace("\\n", "\n").Replace("\\\\",("\\"));
        }
    }
}
